using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller;
using Controller.Coil;
using Controller.MsgPro;
using Core.Define;
using Core.Util;
using DataModel.Common;
using LogSender;
using MMSComm.Service;
using MsgConvert;
using MsgStruct;
using MSMQ;
using MSMQ.Core.MSMQ;
using System;
using System.Collections.Generic;
using static DataMod.Common.MMSMsgProResultModel;
using static DataMod.Response.RespnseModel;
using static DataModel.HMIServerCom.Msg.SCCommMsg;

namespace MMSComm.Actor
{
    public class MMSSndEdit : BaseActor
    {

        private IActorRef _sndActor;
        private IActorRef _selfActor;

        private ICoilController _coilService;
        private IMsgProController _msgProService;       // Msg Process Service

        private AggregateService _agService;

        public MMSSndEdit(ISysAkkaManager akkaManager, ICoilController coilService, IMsgProController msgProService, AggregateService agService, ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);
            _sndActor = akkaManager.GetActor(nameof(MMSSnd));
            _coilService = coilService;
            _msgProService = msgProService;
            _agService = agService;

            _msgProService.SetLog(log);
            _coilService.SetLog(log);

            MQPool.ReceiveFromMMS(x =>
            {
                _selfActor.Tell(x);
            });

            Receive<MQPool.MQMessage>(message => TryFlow(()=>ProMQMsg(message)));


            ReceiveAny(message => RcvObject(message));
        }

        private void ProMQMsg(MQPool.MQMessage message)
        {
            // 鋼捲排程調整通知
            if (message.ID == InfoMMS.CoilSceduleChanged.Event)
            {
                var eventMsg = MMSSysDef.SndMsg.CoilScheduleChanged;
                var coilScheduleIDs = message.Data as List<string>;
                var sndMsg = MMSMsgFactory.CoilScheduleChangedMsg(coilScheduleIDs);
                _log.I($"{eventMsg}:{sndMsg.MsgID}", sndMsg.MsgID + " " + sndMsg.CoilNo);               
                SendToSndActor(eventMsg, sndMsg.MsgID, sndMsg);           
                return;
            }

            // 鋼捲上鞍做通知
            if (message.ID == InfoMMS.CoilLoadedOnSk.Event)
            {
                var eventMsg = MMSSysDef.SndMsg.CoilLoadedSkid;
                var coiNo = message.Data as string;

                var planNo = _coilService.GetPDIPlanNo(coiNo);
                if (planNo.Equals(string.Empty))
                    return;

                var sndMsg = MMSMsgFactory.CoilLoadSkidMsg(planNo, coiNo);
                _log.I($"{eventMsg}:{sndMsg.MsgID}", $"{sndMsg.MsgID}，鋼捲{coiNo}上鞍座通知");
                _log.D(eventMsg, JsonUtil.ToJson(sndMsg));           
                SendToSndActor(eventMsg, sndMsg.MsgID, sndMsg);
               
                return;
            }

            // 鋼捲回退實績通知
            if (message.ID == InfoMMS.CoilRejectResult.Event)
            {
                var eventMsg = MMSSysDef.SndMsg.CoilRejectData;
                var coilReject = message.Data as CoilRejectInfo;
                var sndMsg = MMSMsgFactory.CoilRejectResult(coilReject);
                _log.I($"{eventMsg}:{sndMsg.MsgID}", sndMsg.MsgID);            
                SendToSndActor(eventMsg, sndMsg.MsgID, sndMsg);
                return;
            }

            // HMI要求PDO上傳MMS
            if (message.ID == InfoMMS.ClientInfoSndPDO.Event)
            {
                var eventMsg = MMSSysDef.SndMsg.CoilPDO;
                var msg = message.Data as CS06_SendMMSPDO;

                //  取得 PDO 資料
                var tblPdo = _coilService.GetPDO(msg.Plan_No ,msg.Coil_ID);
                if (tblPdo == null)
                {
                    _log.E("撈取PDO資料失敗", $"無出口捲號({msg.Coil_ID})的PDO");
                    return;
                }
                _log.I("撈取PDO資料成功", $"撈取出口捲號({msg.Coil_ID})的PDO成功");

                //  取得缺陷資料
                var defect = _coilService.GetDefect(msg.Plan_No, msg.Coil_ID);
                defect = defect == null ? new DefectData() : defect;

                var sndMsg = MMSMsgFactory.CoilPDO(tblPdo, defect);
                _log.I($"{eventMsg}:{sndMsg.MsgID}", $"上傳鋼捲PDO{msg.Coil_ID}資料");
                _log.D(eventMsg, JsonUtil.ToJson(sndMsg));
                SendToSndActor(eventMsg, sndMsg.MsgID, sndMsg);
                //_coilService.UpdateUploadPDOCheck(msg.Plan_No, msg.Coil_ID, true, msg.OperatorID);
                _coilService.UpdateUploadPDOUserID(msg.Plan_No, msg.Coil_ID, msg.OperatorID);

                return;
            }

            // P1MM03:回覆接收排程電文
            if (message.ID == InfoMMS.ResCoilSchedResult.Event)
            {
                var eventMsg = MMSSysDef.SndMsg.ResForCoilSched;
                var proResult = message.Data as ProResult;
                var sndMsg = proResult.ToCoilSchedRes();
                _log.I($"{eventMsg} :{sndMsg.MsgID}", $"處理結果{proResult.Result}, 原因{proResult.RejectCause}");
                _log.D($"{eventMsg} :{sndMsg.MsgID}", "回覆報文內容為" + JsonUtil.ToJson(sndMsg));
                SendToSndActor(eventMsg, sndMsg.MsgID, sndMsg);
                return;
            }

            // P1MM04:回復接收PDI電文
            if (message.ID == InfoMMS.SndPDIProResult.Event)
            {
                var eventMsg = MMSSysDef.SndMsg.ResForCoilPDI;
                var proResult = message.Data as ProResult;
                var sndMsg = MMSMsgFactory.CoilPDIProRes(proResult);
                _log.I($"{eventMsg}:{sndMsg.MsgID}", $"處理結果{proResult.Result}, 原因{proResult.RejectCause}");
                _log.D(eventMsg, JsonUtil.ToJson(sndMsg));
                SendToSndActor(eventMsg, sndMsg.MsgID, sndMsg);
                return;
            }

            // 生产实绩请求
            if (message.ID == InfoMMS.MMSInfoSndkPDO.Event)
            {

                var eventMsg = MMSSysDef.SndMsg.CoilPDO;
                var coilID = message.Data as string;
                
                //  取得 PDO 資料
                var tblPdo = _coilService.GetFinalPDO(coilID);
                if (tblPdo == null)
                {
                    _log.E("撈取PDO資料失敗", $"無出口捲號({coilID})的PDO");
                    return;
                }
                _log.I("撈取PDO資料成功", $"撈取出口捲號({coilID})的PDO成功");

                //  取得缺陷資料
                var defect = _coilService.GetDefect(string.Empty, coilID);

                var sndMsg = MMSMsgFactory.CoilPDO(tblPdo, defect);
                _log.I($"{eventMsg}:{sndMsg.MsgID}", $"Code:{sndMsg.MsgID}，上傳鋼捲PDO{coilID}資料");
                _log.D(eventMsg, JsonUtil.ToJson(sndMsg));
                SendToSndActor(eventMsg, sndMsg.MsgID, sndMsg);
                return;
            }

            // 要求生產排程
            if (message.ID == InfoMMS.AskCoilSchedule.Event)
            {

                var eventMsg = MMSSysDef.SndMsg.ReqForCoilSched;
                var coilID = message.Data as string;
                var sndMsg = MMSMsgFactory.ReqCoilSchedule(coilID);
                _log.I($"{eventMsg}:{sndMsg.MsgID}", $"Code:{sndMsg.MsgID}，要求鋼捲{coilID}生產排程資料");
                _log.D(eventMsg, JsonUtil.ToJson(sndMsg));
                SendToSndActor(eventMsg, sndMsg.MsgID, sndMsg);
            }

            // 鋼捲PDI請求
            if (message.ID == InfoMMS.AskPDI.Event)
            {
                var eventMsg = MMSSysDef.SndMsg.ReqForPDI;
                var coilID = message.Data as string;
                var sndMsg = MMSMsgFactory.ReqCoilPDI(coilID);
                _log.I($"{eventMsg}:{sndMsg.MsgID}", $"Code:{sndMsg.MsgID}，要求MMS下發鋼捲{coilID}PDI");
                _log.D(eventMsg, JsonUtil.ToJson(sndMsg));
                SendToSndActor(eventMsg, sndMsg.MsgID, sndMsg);
                return;
            }

            // P1MM25:回覆整計畫刪除電文
            if (message.ID == InfoMMS.ResPlanNoShedDelResult.Event)
            {
                var eventMsg = MMSSysDef.SndMsg.MMSResDeletePlanNoResult;
                var proResult = message.Data as ProResult;
                var sndMsg = MMSMsgFactory.ResPlanNoDelete(proResult);
                _log.I($"{eventMsg}:{sndMsg.MsgID}", $"處理結果{proResult.Result}, 原因{proResult.RejectCause}");
                _log.D(eventMsg, JsonUtil.ToJson(sndMsg));
                SendToSndActor(eventMsg, sndMsg.MsgID, sndMsg);
                return;
            }

            // 回復能源消耗訊息
            if (message.ID == InfoMMS.SndEnergyConsumptionInfo.Event)
            {
                var eventMsg = MMSSysDef.SndMsg.MMSEnergyConsumptionInfo;
                var energyInfo = message.Data as L1L2Rcv.Msg_316_Utility_Data;
                var sndMsg = MMSMsgFactory.EnergyConsumptionInfo(energyInfo);
                //_log.I($"回復能源消耗訊息:{sndMsg.MsgID}", JsonUtil.ToJson(sndMsg));
                _log.I($"{eventMsg}:{sndMsg.MsgID}", "");
                SendToSndActor(eventMsg,sndMsg.MsgID, sndMsg);
                return;
            }

            // 上傳能源消耗訊息
            if(message.ID == InfoMMS.UploadEnergyConsumptionInfo.Event)
            {
                var eventMsg = MMSSysDef.SndMsg.MMSEnergyConsumptionInfo;
                var msg = message.Data as CS15_Utility;
                var sndMsg = MMSMsgFactory.UploadEnergyConsumptionInfo(msg);
                SendToSndActor(eventMsg, sndMsg.MsgID, sndMsg);
                return;
            }


            // 發送鋼捲生產命令刪除通知
            if (message.ID == InfoMMS.CoilSceduleDeleted.Event)
            {
                var eventMsg = MMSSysDef.SndMsg.MMSCoilScheduleDelete;
                var scheduleDelete = message.Data as CS03_ScheduleChange;
                var sndMsg = MMSMsgFactory.CoilSchDelMsg(scheduleDelete);
                _log.I($"{eventMsg}:{sndMsg.MsgID}", $"刪除{sndMsg.CoilNo}");
                SendToSndActor(eventMsg, sndMsg.MsgID, sndMsg);
                return;
            }

            // 停復機上傳
            if(message.ID == InfoMMS.UploadLineFaultRecord.Event)
            {
                var eventMsg = MMSSysDef.SndMsg.EqDownResultCode;
                var msg = message.Data as LineFaultRecord;
                var sndMsg = MMSMsgFactory.ToEquipmentDownResult(msg,"1");
                _log.I($"{eventMsg}:{sndMsg.MsgID}", $"時間{msg.stop_start_time}-{msg.stop_end_time}");
                SendToSndActor(eventMsg,sndMsg.MsgID, sndMsg);
                return;
            }
          
        }

        private void SendToSndActor(string msg , string msgID, object data)
        {
            var bytes = MsgAnalUtil.RawSerialize(data);
            if (bytes == null)
            {
                // DumpFile              
                _log.E("發送報文序列化編碼失敗", $"MsgID : {msgID} 序列化失敗");
                return;
            }

            // 結尾符                
            var container = new List<byte>(bytes);
            container.Add(10);
            bytes = container.ToArray();
            var sndActualLength = bytes.Length.ToString();

            _log.D("發送報文序列化編碼成功", $"MsgID: {msgID}");
            var comMsg = new CommonMsg(length: sndActualLength, id: msgID, bytes: bytes);

            _sndActor.Tell(comMsg);

            _agService.DumpSndRawData(bytes);
      
            _msgProService.CreateMMSWMSMsg("TBL_MMS_SendRecord", comMsg);

            MQPoolService.PushHMI($"Server通知三級:{msgID}", msg);

            GC.Collect();
            
        }

        private void RcvObject(object msg)
        {
            _log.E("ATell接收資料", $"無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }
    }
}
