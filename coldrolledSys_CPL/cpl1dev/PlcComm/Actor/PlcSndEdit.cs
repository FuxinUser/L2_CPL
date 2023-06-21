using Akka.Actor;
using Core.Util;
using DataModel.Common;
using System;
using Core.Define;
using MsgConvert;
using DBService.Repository;
using MSMQ;
using MSMQ.Core.MSMQ;
using Controller.MsgPro;
using AkkaSysBase.Base;
using AkkaSysBase;
using System.Threading;
using MsgConvert.EntityFactory;
using LogSender;
using PLCComm.Service;

/**
 * Author: ICSC 余士鵬
 * Date: 2019/9/19
 * Description: 序列化解析L2發送資料.並轉發Communication角色
 * Reference: 
 * Modified: 
 */

namespace PLCComm.Actor
{
    class PlcSndEdit : BaseActor
    {
        private AggregateService _agService;            // 聚合服務器
        private IActorRef _communicationActor;                    // Snd發送角色
        private IActorRef _selfActor;                   // PlcSndEdit 角色

        private IMsgProController _msgProService;

        public PlcSndEdit(ISysAkkaManager akkaManager, IMsgProController msgProService, ILog log, AggregateService agService) : base(log)
        {
            _selfActor = akkaManager.GetActor(nameof(PlcSndEdit));
            _communicationActor = akkaManager.GetActor(nameof(PlcCom));

            _agService = agService;
            _msgProService = msgProService;
            _msgProService.SetLog(log);

            //從MQ接收訊息，Tell給自己(有通過Tell，才能擁有Akka的機制):
            MQPool.ReceiveFromL1(x => {
                // 接收到資料轉接給SndEdit處理
                _selfActor.Tell(x);
            });

            Receive<MQPool.MQMessage>(msg => TryFlow(() => ProMQMsg(msg)));
            ReceiveAny(message => RcvObject(message));
        }

      
        private void ProMQMsg(MQPool.MQMessage msg)
        {
            // 201 Preset
            if (msg.ID == InfoL1.SndPresetMsg.Event)
            {
                _log.I($"Prset通知:{PlcSysDef.SndMsgCode.L1201Preset}", "發送Preset通知");
                _log.D("Prset通知", JsonUtil.ToJson(msg.Data));
                SendToCommunicationSnd(PlcSysDef.SndMsgCode.L1201Preset, msg.Data);
                return;
            }
            // 202 TrackMap
            if (msg.ID == InfoL1.SndTrackMap.Event)
            {
                var coilMap = msg.Data as CoilMapEntity.TBL_CoilMap;

               
                _log.I($"TrackMap通知{PlcSysDef.SndMsgCode.L1202TrackMapL2}", JsonUtil.ToJson(coilMap));
                _log.D("TrackMap通知", JsonUtil.ToJson(msg.Data));

                var L2TrackMapMsg = L1MsgFactory.L2TrackMap202Msg(coilMap);      
                SendToCommunicationSnd(PlcSysDef.SndMsgCode.L1202TrackMapL2, L2TrackMapMsg);
                return;
            }
            // 203
            if (msg.ID == InfoL1.SndSplitId.Event)
            {
                var coilID = msg.Data as string;

                _log.I($"SplitId通知{PlcSysDef.SndMsgCode.L1203SplitID}", $"[{coilID}]");
                _log.D("SplitId通知", JsonUtil.ToJson(msg.Data));


                var splitIDMsg = L1MsgFactory.SplitID203Msg(coilID);
             
                SendToCommunicationSnd(PlcSysDef.SndMsgCode.L1203SplitID, splitIDMsg);
                return;
            }
            // 204
            if (msg.ID == InfoL1.SndDelSkEntryID.Event)
            {
                var delPosID = msg.Data as string;
                var delSkid204Msg = L1MsgFactory.DelSkid204Msg(delPosID);
                _log.I($"DelSkid通知{PlcSysDef.SndMsgCode.L1204DelSkID}", $"{delPosID}");
                SendToCommunicationSnd(PlcSysDef.SndMsgCode.L1204DelSkID, delSkid204Msg);
                return;
            }
            // 205
            if (msg.ID == InfoL1.SndNewPORId.Event)
            {
                var coilID = msg.Data as string;
                _log.I($"New POR通知{PlcSysDef.SndMsgCode.L1205NewPOR}", $"[{coilID}]");
                _log.D("New POR通知", JsonUtil.ToJson(msg.Data));
                var porCoilID = L1MsgFactory.NewPORMsg(coilID);
                SendToCommunicationSnd(PlcSysDef.SndMsgCode.L1205NewPOR, porCoilID);
                return;
            }

        }

        /// <summary>
        /// 發送給Communication Actor
        /// </summary>
        private void SendToCommunicationSnd(string msgID, object msgObject)
        {

            var bytes = MsgAnalUtil.RawSerialize(msgObject, true);
            if (bytes == null)
            {
                _log.E("轉ByteCode", " Message Encode 失敗");
                return;
            }

            _agService.DumpSndRawData(bytes);

            var sndMsg = new CommonMsg(bytes.Length.ToString(), msgID, bytes);
            //_log.Debug("轉ByteCode", $" 發送訊息Msg ID{msgID} 長度為{bytes.Length}");
            _communicationActor.Tell(sndMsg);

            // 存Log
            try
            {
                Thread.Sleep(10);
                var L1DBModel = msgObject.ConvertL1DBModel(msgID);
                _msgProService.CreateMsgToL1HistoryDB("L2L1_" + msgID, L1DBModel);
            }
            catch (Exception e)
            {
                _log.E($"報文{msgID}存取Log失敗", e.Message.CleanInvalidChar());
            }
        }

        /// <summary>
        /// 角色接收無法解析資料事件
        /// </summary>
        private void RcvObject(object msg)
        {
            _log.E("ATell接收資料", $"無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }

    }
}
