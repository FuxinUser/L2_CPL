using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller.MsgPro;
using Core.Util;
using DataMod.BarCode;
using DataMod.BarCode.Msg;
using DataMod.WMS.LogicModel;
using DataModel.Common;
using LogSender;
using MsgConvert;
using MSMQ;
using MSMQ.Core.MSMQ;
using System.Collections.Generic;
using WMSComm.Service;
using static DBService.Repository.CoilMapEntity;

namespace WMSComm.Actor
{
    public class WMSSndEdit : BaseActor
    {

        private IActorRef _sndActor;
        private IActorRef _selfActor;

        private IMsgProController _msgProService;       // Msg Process Service

        private AggregateService _agService;

        public WMSSndEdit(ISysAkkaManager akkaManager, IMsgProController msgProService, AggregateService agService, ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);
            _sndActor = akkaManager.GetActor(nameof(WMSSnd));

            _msgProService = msgProService;
            _agService = agService;

            _msgProService.SetLog(log);

            MQPool.ReceiveFromWMS(x => _selfActor.Tell(x));

            Receive<MQPool.MQMessage>(x => ProMQMsg(x));
            ReceiveAny(message => RcvObject(message));
        }

        private void ProMQMsg(MQPool.MQMessage msg)
        {

            // PWX1 傳送排程資訊
            if (msg.ID == InfoWMS.CoilScheduleInfoMsg.Event)
            {

                var coilScheduleIDs = msg.Data as List<string>;
                var pw11 = WMSMsgFactory.PWX1ScheduleMsg(coilScheduleIDs);
                _log.I($"傳送排程資訊:{pw11.MsgID}", $" {pw11.MsgID}, {pw11.CoilNos}");
                SendToSndActor(pw11.MsgID, pw11);
                return;
            }

            // PWX2 接收產線入口/出口 Tracking
            if (msg.ID == InfoWMS.InfoTrackMap.Event)
            {
                var trkInfo = msg.Data as TBL_CoilMap;
                var pw12 = trkInfo.PWX2TrackInfo();
                _log.I($"傳送鋼卷追蹤:{pw12.MsgID}", $" {pw12.MsgID}");
                SendToSndActor(pw12.MsgID, pw12);
                return;
            }

            // PWX3 傳送鋼卷PDO
            if (msg.ID == InfoWMS.InfoiCoilPDOMsg.Event)
            {

                var wmsPDO = msg.Data as WMSPdoInfomation;
                var pwx3 = wmsPDO.PWX3CoilInfo();
                _log.I($"傳送排程資訊", $"MsgID : {pwx3.MsgID}, CoilNo : {pwx3.CoilID}");
                SendToSndActor(pwx3.MsgID, pwx3);
                return;
            }


            // PWX5 傳送產線入料/出料要求
            if (msg.ID == InfoWMS.InfoCoilEntryOrDeliveryReq.Event)
            {

                var prodCoilReq = msg.Data as ProdLineCoilReq;
                var pwx5 = WMSMsgFactory.PWX5ReqMsg(prodCoilReq);
                _log.I($"傳送產線{prodCoilReq.ActionStr}要求", $"{pwx5.MsgID}, {pwx5.CoilID}");
                SendToSndActor(pwx5.MsgID, pwx5);
                return;
            }

            // PWX5 傳送產線退料要求
            if (msg.ID == InfoWMS.RejectCoilReqMsg.Event)
            {
                var prodCoilReq = msg.Data as ProdLineCoilReq;
                var pwx5 = WMSMsgFactory.PWX5ReqMsg(prodCoilReq);
                _log.I($"傳送產線退料要求{pwx5.MsgID}", $"{pwx5.MsgID}, {pwx5.CoilID}");
                SendToSndActor(pwx5.MsgID, pwx5);
                return;
            }

            // PWx6 
            if (msg.ID == InfoWMS.InfoBCSScanID.Event)
            {
                var scanResult = msg.Data as ScanResult;
                var pwx6 = WMSMsgFactory.PW06ScanCoil(scanResult.SKID, scanResult.CoilID);
                _log.I($"傳送產線掃描通知", $"MsgID : {pwx6.MsgID}, CoilNo: {pwx6.CoilIDNo}");
                SendToSndActor(pwx6.MsgID, pwx6);
                return;

            }
        }
        private void SendToSndActor(string msgID, object data)
        {
            var bytes = MsgAnalUtil.RawSerialize(data);

            if (bytes == null)
            {
                _log.E("報文序列化編碼失敗", $"MsgID : {msgID} 序列化失敗");
                return;
            }

            _log.D("報文序列化編碼成功", $"Msg ID{msgID}, Length : {bytes.Length}");
            var comMsg = new CommonMsg(id: msgID, bytes: bytes);
            _sndActor.Tell(comMsg);
            _agService.DumpSndRawData(bytes);
            _msgProService.CreateMMSWMSMsg("TBL_WMS_SendRecord", comMsg);

        }

        private void RcvObject(object msg)
        {
            _log.E("ATell接收資料", $"無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }

    }
}
