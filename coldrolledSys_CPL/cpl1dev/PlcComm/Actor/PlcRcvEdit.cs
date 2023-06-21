using System;
using Core.Util;
using Core.Help;
using Core.Define;
using MSMQ.Core.MSMQ;
using Controller;
using Controller.Sys;
using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using Controller.MsgPro;
using AkkaSysBase.Base;
using MsgConvert.EntityFactory;
using LogSender;
using Core.Help.DumpRawDataHelp;

/**
 * Author: ICSC 余士鵬
 * Date: 2019/9/19
 * Description: 負責解析Plc發送資料為Model. 並轉發其他App處理(Msg Edit角色)
 * Reference: 
 * Modified: 
 */

namespace PLCComm.Actor
{
    public class PlcRcvEdit : BaseActor
    {
        private ISysController _sysService;             // System Process Service
        private IMsgProController _msgProService;       // Msg Process Service

        private ICancelable _tmrDetectL1Alive;          // L2 Alive 發送Timer
        private DateTime _preAliveMsgRcvTime;           // 前一筆Alive接收時間

        public PlcRcvEdit(
                        ISysController sysService,
                        IMsgProController msgProService,
                        ILog log
                        ) : base(log)
        {
             _sysService = sysService;
            _sysService.SetLog(log);
            _msgProService = msgProService;
            _msgProService.SetLog(log);

            //_sysService.SaveAPStatusToL25("SystemAP_3", "1");

            Receive<byte[]>(message => ProReceiveMessage(message));
            Receive<EventDef.CMDSET>(message => ProEventCmd(message));
            ReceiveAny(message => RcvObject(message));
        }

        private void ProReceiveMessage(byte[] message)
        {            
   
            var msgID = MsgAnalUtil.GetMsgID(message);           
            var ty = MsgRefToObjHelp.Instance.GetPlcStructClassType(msgID);
            object msgObject = null;
            bool insertLogOK = false;   //存Msg歷史DB


            // 反序列化
            try
            {
                msgObject = MsgAnalUtil.RawDeserialize(message, ty, true);
                if (msgObject == null)
                {
                    //Analysis Fail Dump File
                    _log.E("報文解析失敗", $"解析接收訊號失敗 MsgID為{msgID}");
                    _log.E("報文解析失敗", $"解析接收訊號失敗 MsgID為{msgID}");
                    return;
                }
            }
            catch(Exception e)
            {
                _log.E("報文解析失敗", e.Message.CleanInvalidChar());               
            }
            if(msgObject == null)
            {
                return;
            }

            // 存DB Log && DumpFile(399不存)
            if (msgID != PlcSysDef.RcvMsgCode.L1399Alive)
            {                          
                // DB Log
                try
                {
                    var L1DBModel = msgObject.ConvertL1DBModel(msgID);
                    insertLogOK = _msgProService.CreateMsgToL1HistoryDB("L1L2_" + msgID, L1DBModel);
                }
                catch (Exception e)
                {
                    _log.E($"報文{msgID}存取Log失敗", e.Message.CleanInvalidChar());

                }
            }

            // Router轉發其他APP
            if (msgObject == null)
                return;

            _log.D("報文解析成功", $"解析接收訊號成功 MsgID為{msgID}");
            _log.D("Dump列印", message.PrintRawData());

            Router(msgID, msgObject);                   
        }
  
        private void Router(string msgID, dynamic message)
        {
            switch (msgID)
            {
                case PlcSysDef.RcvMsgCode.L1301EnCoilCut:
                    MQPoolService.SendToCoil(InfoCoil.RecordEnCoilCut.Data(message));
                    break;
                case PlcSysDef.RcvMsgCode.L1302WieldRecord:
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1302CoilWeldMsg.Data(message));
                    break;
                case PlcSysDef.RcvMsgCode.L1303ReqTrackMap:
                    MQPoolService.SendToTrk(InfoTrk.SndCurCoilMap.Data(message));
                    break;
                case PlcSysDef.RcvMsgCode.L1305TrackMapEn:
                    MQPoolService.SendToTrk(InfoTrk.TrackMapEnCoilNo.Data(message));
                    break;
                case PlcSysDef.RcvMsgCode.L1306TrackMapEx:
                    MQPoolService.SendToTrk(InfoTrk.TrackMapExCoilNo.Data(message));
                    break;
                case PlcSysDef.RcvMsgCode.L1307CoilDismount:
                    MQPoolService.SendToCoil(InfoCoil.AccountPDO.Data(message));
                    break;
                case PlcSysDef.RcvMsgCode.L1308CoilWeightScale:
                    MQPoolService.SendToCoil(InfoCoil.UpdateOutMatPureWT.Data(message));
                    break;
                case PlcSysDef.RcvMsgCode.L1309EquipMaint:
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1309EquipMaint.Data(message));
                    break;
                case PlcSysDef.RcvMsgCode.L1310LineFault:
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1310LineFault.Data(message));
                    break;
                case PlcSysDef.RcvMsgCode.L1311ExCoilCut:
                    MQPoolService.SendToCoil(InfoCoil.DetExCoilCut.Data(message));                  
                    break;
                case PlcSysDef.RcvMsgCode.L1312NewCoilRec:
                    MQPoolService.SendToCoil(InfoCoil.NewCoilRec.Data(message));
                    break;


                case PlcSysDef.RcvMsgCode.L1313SpdTen:
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1313SpdTen.Data(message));
                    break;
                case PlcSysDef.RcvMsgCode.L1316Utility:
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1316Utility.Data(message));
                    //MQPoolService.SendToCoil(InfoCoil.SndEnergyConsumpInfo.Data(message));
                    break;

                case PlcSysDef.RcvMsgCode.L1317ReturnCoilInfo:
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1317ReturnCoilInfo.Data(message));
                    break;

                case PlcSysDef.RcvMsgCode.L1318SideTrimmerInfo:
                    MQPoolService.SendToDtGtr(InfoDtGtr.SaveL1318SideTrimmerInfo.Data(message));
                    break;

                case PlcSysDef.RcvMsgCode.L1399Alive:
                    _preAliveMsgRcvTime = DateTime.Now;
                    _sysService.UpdateL1LastAliveTime(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    break;

        
            }

        }

       

        /// <summary>
        /// 每5秒檢查一次，若最後更新時間與本次檢查的時間差超過10秒
        /// 則判斷為一級斷線，須紀錄EVENTLOG並通知CLIENT
        /// </summary>
        private void DetectL1ConnectStatusTmr(int second, object message)
        {
            var interval = TimeSpan.FromSeconds(second);
            _tmrDetectL1Alive = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(
                    interval, interval, Self, message, Self);
        }

        /// <summary>
        /// Cmd事件觸發
        /// </summary>  
        private void ProEventCmd(EventDef.CMDSET cmd)
        {
            switch (cmd)
            {
                case EventDef.CMDSET.DETECT_L1_ALIVE:
                    MonitorL1Alive();
                    break;
            }

        }

        /// <summary>
        /// 監控是否收到Alive
        /// </summary> 
        private void MonitorL1Alive()
        {

            // 未收到第一筆
            if (_preAliveMsgRcvTime == DateTime.MinValue)
            {

                _log.E("L1 Alive連線檢查", "未收到第一筆L1 Alive資訊");
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SCCommMsg.SC03_EventPush(
                                                                                            EventDef.L1DisConn,
                                                                                            "未收到第一筆L1 Alive資訊")));
                return;
            }
            // 收到第一筆後做比較
            var diffInSeconds = (DateTime.Now - _preAliveMsgRcvTime).TotalSeconds;
            if (diffInSeconds > 10)
            {
                // 紀錄Log
                _log.E("L1 Alive連線檢查", "L1已斷線!!!");
                // 通知HMI             
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SCCommMsg.SC03_EventPush(
                                                                                           EventDef.L1DisConn,
                                                                                           "L1 Alive連線檢查 ,L1已斷線!!!")));
            }
        }

        /// <summary>
        /// 角色接收無法解析資料事件
        /// </summary>
        private void RcvObject(object message)
        {
            _log.E("AThread接收資料-RcvObject", $"無法解析資料! Type:{message.GetType()} From Sender:{Sender.Path}");
        }


    }
 


}
