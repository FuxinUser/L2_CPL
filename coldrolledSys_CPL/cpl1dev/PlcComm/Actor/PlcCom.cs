using Akka.Actor;
using Akka.IO;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller.Sys;
using Core.Define;
using Core.Help.DumpRawDataHelp;
using Core.Util;
using DataMod.PLC;
using DataModel.Common;
using LogSender;
using PLCComm.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using static Core.Define.DBParaDef;
using static Core.Define.EventDef;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/5
 * Description: Plc 接收發送角色
 * Reference: 
 * Modified: 
 */
namespace PLCComm.Actor
{
    public class PlcCom : BaseClientActor
    {

        private ISysController _sysController;

        private AggregateService _agService;            // 聚合服務器
        private IActorRef _rcvEditActor;                // 資料解析角色

        private ICancelable _tmrAliveMsgSnd;            // L2 Alive 心跳發送
        private readonly bool _isSndHeartbeat = true;   // 是否發送L2 Alive心跳

        private bool _isSndFail;                        // 是否發送失敗


        // 解析PLC報文
        private ByteString _byteKeeper;
        private List<byte> _listByte;

  

        public PlcCom(ISysAkkaManager akkaManager, ISysController sysController, AkkaSysIP akkaSysIp, ILog log, AggregateService agService) : base(akkaSysIp, log)
        {


            _sysController = sysController;
            _sysController.SetLog(log);

            _agService = agService;       
            _rcvEditActor = akkaManager.GetActor(nameof(PlcRcvEdit));
            _agService.InitAliveMsg();

            _listByte = new List<byte>();

            // 開啟底層Queu發送機制
            InitSndQueuMachinesm(agService.appSetting.ReSndCnt, agService.appSetting.DetectSndQueeTime, CMDSET.DETECT_SND_QUEU);

            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectToPLC,
                                         _agService.appSetting.RemoteIp,
                                         _agService.appSetting.RemotePort.ToString(),
                                         ConnectionSysDef.UnConnect);

            //Receive<CommonMsg>(message => ProCommonMsg(message));
            Receive<CommonMsg>(message => _sndQueu.Enqueue(message));
            Receive<CMDSET>(message => ProEventCmd(message));

        }

        /// <summary>
        /// Tcp連線事件觸發
        /// </summary>
        protected override void TCPConnected(Tcp.Connected message)
        {
            base.TCPConnected(message);


            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectToPLC,
                                           _agService.appSetting.RemoteIp,
                                           _agService.appSetting.RemotePort.ToString(),
                                           ConnectionSysDef.Connect);

            _log.I("TCP連線成功", " Connected. message=" + message.ToString());
            _log.I("TCP連線成功", " LocalAddress=" + message.LocalAddress.ToString());
            _log.I("TCP連線成功", " RemoteAddress=" + message.RemoteAddress.ToString());

            //Send Alive Msg in 5s 
            if (_isSndHeartbeat) StartAliveMsgSndTmr(5, EventDef.CMDSET.SND_HEARTBEAT);

            //若傳送Connection為null 重新連線候補發送 
            if (!_isSndFail)
                return;

            _isSndFail = false;


        }


        /// <summary>
        /// Tcp接收事件
        /// </summary>
        protected override void TcpReceivedData(Tcp.Received msg)
        {
            base.TcpReceivedData(msg);
            ProTCPRcvData(msg);
        }

        protected override void TcpConnectionClosed(Tcp.ConnectionClosed message)
        {

            if (_tmrAliveMsgSnd != null)
                _tmrAliveMsgSnd.Cancel();

            base.TcpConnectionClosed(message);


            _sysController.UpdateConnectionStatuts(ConnectionSysDef.ConnectionType.L2ConnectToPLC,
                                                   _agService.appSetting.RemoteIp,
                                                   _agService.appSetting.RemotePort.ToString(),
                                                   ConnectionSysDef.UnConnect);
        }


        //  Cmd事件觸發
        private void ProEventCmd(EventDef.CMDSET msg)
        {
            switch (msg)
            {
                case CMDSET.ACK_TIMEOUT:
                    //L1沒有ACK機制.
                    break;
                case CMDSET.SND_HEARTBEAT:
                    // 發送Alive
                    SndAliveMsg();
                    break;

                // 心跳開關
                case CMDSET.HEART_BEAT_OPEN:
                    // 開啟心跳發送
                    StartAliveMsgSndTmr(2, CMDSET.SND_HEARTBEAT);
                    break;
                case CMDSET.HEART_BEAT_CLOSE:
                    // 取消心跳發送
                    CloseAliveMsgSndTmr();
                    break;
                case CMDSET.DETECT_SND_QUEU:
                    DeQueuSnd();
                    break;
            }
        }

      

        /// <summary>
        /// 發送L2 Alive
        /// </summary>
        private void SndAliveMsg()
        {
            if (_connection == null)
                return;
            var alive = _agService.GetNowAliveMsg().RawSerialize(true);
            _connection.Tell(Tcp.Write.Create(ByteString.FromBytes(alive)));
        }

        /// <summary>
        /// 開始Aive
        /// </summary>
        private void StartAliveMsgSndTmr(int second, object message)
        {
            var interval = TimeSpan.FromSeconds(second);
            _tmrAliveMsgSnd = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(interval, interval, Self, message, Self);

        }

        /// <summary>
        /// 關閉L2 Alive發送Timer
        /// </summary>
        private void CloseAliveMsgSndTmr()
        {
            if (_tmrAliveMsgSnd != null)
                _tmrAliveMsgSnd.Cancel();
        }


        private void ProTCPRcvData(Tcp.Received msg)
        {

            // 紀錄接收資料      
            var bytes = msg.Data.ToArray();
      
            // 測試用
            _agService.DumpDebugRawData(bytes);

            // 加入Buffer  
            _listByte.AddRange(bytes);

            while (_listByte.Count() > 0)
            {

                var header = MsgAnalUtil.RawDeserialize(_listByte.ToArray(), typeof(PLCHeader), true) as PLCHeader;

                if (header == null)
                    break;

                var msgLen = header.MessageLength;
                var msgID = header.MessageId;

                if (!CheckMsgLenByDoc(msgLen, msgID))
                {
                    _log.E("接收報文", " Length doesn't meet specifications");

                    _agService.DumpFailRawData(bytes);
                    
                    _listByte.Clear();
     
                    break;
                }

                if (_listByte.Count() < msgLen)
                    break;


                _log.I("TCP接收資料-驗證報文成功", $"接收報文驗證成功 MsgID : {msgID}, Length : {msgLen}");
                
                var data = _listByte.GetRange(0, msgLen).ToArray();

                _agService.DumpRcvRawData(bytes);
              
                _rcvEditActor.Tell(data);
                
                _listByte.RemoveRange(0, msgLen);

            }


        }


        /// <summary>
        /// 處理CommonMsg事件
        /// </summary>
        private void ProCommonMsg(CommonMsg message)
        {

            _log.D("資料發送", $" 資料發送. Message ID [{message.Message_Id} Message.Length={message.Data.Length}");
            SendData(ByteString.FromBytes(message.Data));
        }
        /// <summary>
        /// 發送資料給一級
        /// </summary>
        private void SendData(ByteString bs)
        {

            if (_connection == null)
            {
                _isSndFail = true;
                Connect();
                return;
            }
            _connection.Tell(Tcp.Write.Create(bs));

        }

        
        protected override void DeQueuSnd()
        {
            //  若 queue 沒資料了就結束
            if (_sndQueu.Count <= 0)
                return;

            if (_connection == null)
            {
                _log.E("無法發送", "無法發送,未連線成功");
                return;
            }

            // 取出不移除
            var msg = SlefTryPeek();
            if (msg == null)
            {
                _log.E("取SndQueu失敗", $"目前Queu資料數目 = $`{_sndQueu.Count}");
                return;
            }
      
            _log.I($"使用 Queue 發送電文", $"MsgID = {msg.Message_Id} MsgLength = {msg.Message_Length} 實際Cnt = {msg.Data.Length}");
            _log.D("Dump列印", msg.Data.PrintRawData());

            _connection.Tell(Tcp.Write.Create(ByteString.FromBytes(msg.Data)));
            SlefTryDequeue();
        }

        /// <summary>
        /// 確定接收訊息長度
        /// </summary>
        private bool CheckMsgLenByDoc(short message_len, short message_id)
        {
            bool isOk = _agService.MsgLengthAndTypeDic.PlcMsgLen.ContainsKey(message_id) && _agService.MsgLengthAndTypeDic.PlcMsgLen[message_id] == message_len;

            if (!isOk)
                _log.E("TCP接收資料 - 解析失敗", $"CheckMsgLenByDoc. The length({message_len}) of message {message_id} is different from the length({message_id}) of document.");

            return isOk;
        }

    }


}
