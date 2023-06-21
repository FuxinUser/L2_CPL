using AkkaSysBase.Base;
using Controller;
using Controller.MsgPro;
using Controller.Sys;
using Core.Define;
using Core.Util;
using DataModel.Common;
using LogSender;
using MsgStruct;
using MSMQ.Core.MSMQ;

namespace MMSComm.Actor
{
    public class MMSRcvEdit : BaseActor
    {
        private IMsgProController _msgProService;       // Msg Process Service
        private ISysController _sysController;

        public MMSRcvEdit(IMsgProController msgProService, ISysController sysController, ILog log) : base(log)
        {
            _msgProService = msgProService;
            _sysController = sysController;
            _msgProService.SetLog(log);
            _sysController.SetLog(log);

            //_sysController.SaveAPStatusToL25("SystemAP_1", "1");

            Receive<CommonMsg>(message => ProRcvMsg(message));
            ReceiveAny(message => RcvObject(message));

        }

        private void ProRcvMsg(CommonMsg message)
        {
            // 存取DB
            _msgProService.CreateMMSWMSMsg("TBL_MMS_ReceiveRecord", message);

             //MMP101: 鋼捲下發排程      
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.CoilSchedule))
            {

                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data, typeof(MMSL2Rcv.Msg_Coil_Schedule)) as MMSL2Rcv.Msg_Coil_Schedule;

                _log.I($"鋼捲排程接收:{rcvMsg.MsgID}", $"接收資料{rcvMsg.ScheduleCnt}筆鋼捲排程資料");
                _log.D("鋼捲排程接收", JsonUtil.ToJson(rcvMsg));
                MQPoolService.SendToCoil(InfoCoil.SaveSchedule.Data(rcvMsg));
                return;
            }

            // MMP102: 三級下發PDI
            if (message.Message_Id.Equals(MMSSysDef.RcvMsgCode.CoilPDI))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data, typeof(MMSL2Rcv.Msg_PDI)) as MMSL2Rcv.Msg_PDI;

                var byteData = rcvMsg.RawSerialize();
                //var container = new List<byte>(message.Data);
                //container.Add(10);
                //var dat = container.ToArray();
                //byteData.DumpDataToFile("D:\\", byteData.GetMMSMsgID());

                _log.I($"接收鋼捲PDI:{rcvMsg.MsgID}", $"【鋼捲】:{rcvMsg.EntryCoilNo},【計畫】:{rcvMsg.PlanNo}");
                _log.D("鋼捲PDI接收", JsonUtil.ToJson(rcvMsg));
                MQPoolService.SendToCoil(InfoCoil.SaveCoilPDI.Data(rcvMsg));
                return;
            }
         
            // MMP106:生產實績要求(要PDO)
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.ReqProResult))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data, typeof(MMSL2Rcv.Msg_Product_Result_Request)) as MMSL2Rcv.Msg_Product_Result_Request;

                _log.I($"要求生產實績(PDO):{rcvMsg.MsgID}", $"要求鋼捲{rcvMsg.CoilNoID}PDO資料");
                _log.D("要求生產實績(PDO)", JsonUtil.ToJson(rcvMsg));
                MQPoolService.SendToCoil(InfoCoil.ReqPDO.Data(rcvMsg));
                return;
            }

            // MMP107:接收鋼捲刪除/回退回應
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.CoilRejectResult))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data, typeof(MMSL2Rcv.Msg_Res_For_Coil_Reject_Result)) as MMSL2Rcv.Msg_Res_For_Coil_Reject_Result;

                _log.I($"鋼捲刪除/回退回應接收:{rcvMsg.MsgID}", $"接收鋼捲{rcvMsg.RequestedCoilNoID}回退回應, 處理結果{rcvMsg.ProResult}, 原因{rcvMsg.RejectReason}");
                _log.D("鋼捲刪除/回退回應接收", JsonUtil.ToJson(rcvMsg));
                MQPoolService.SendToCoil(InfoCoil.CoilRejectResult.Data(rcvMsg));
                return;
            }

            // MMG108:無鋼捲生產命令回覆
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.ResForNoCoil))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_Res_For_No_Coil_Schedule))
                                                            as MMSL2Rcv.Msg_Res_For_No_Coil_Schedule;

                _log.I($"無鋼捲生產命令回覆:{rcvMsg.Code.ToStr()}", $"無鋼捲生產命令回覆:{rcvMsg.Mat_No.ToStr()}");
                _log.D("無鋼捲生產命令回覆", JsonUtil.ToJson(rcvMsg));
                MQPoolService.SendToCoil(InfoCoil.ResNoCoil.Data(rcvMsg));
                return;
            }

            // MMP109:無鋼捲PDI回覆
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.ResForNoCoilPDI))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_Res_For_No_Coil_PDI))
                                                            as MMSL2Rcv.Msg_Res_For_No_Coil_PDI;

                _log.I($"無鋼捲PDI回覆接收:{rcvMsg.Code.ToStr()}", $"無鋼捲PDI回覆接收:{rcvMsg.Mat_No.ToStr()}");
                _log.D("無鋼捲PDI回覆接收", JsonUtil.ToJson(rcvMsg));
                MQPoolService.SendToCoil(InfoCoil.ResNoPDI.Data(rcvMsg));
                return;
            }

            // MMP110:上傳PDO的回覆
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.PdoUploadedReply))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_Res_For_PDO_Uploaded))
                                                            as MMSL2Rcv.Msg_Res_For_PDO_Uploaded;

                _log.I($"上傳PDO的回覆接收:{rcvMsg.Code.ToStr()}", $"上傳PDO的回覆接收:{rcvMsg.Mat_No.ToStr()}");
                _log.D("上傳PDO的回覆接收", JsonUtil.ToJson(rcvMsg));
                MQPoolService.SendToCoil(InfoCoil.PdoUploadedReply.Data(rcvMsg));
                return;
            }

            // 作業計畫刪除請求
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.ReqDeletePlanNo))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data, typeof(MMSL2Rcv.Msg_Req_Delete_Schedule_Plan)) as MMSL2Rcv.Msg_Req_Delete_Schedule_Plan;

                _log.I($"PDI整計畫刪除{rcvMsg.MsgID}", $"計畫號{rcvMsg.PlanNo}刪除要求");
                _log.D("PDI整計畫刪除", JsonUtil.ToJson(rcvMsg));
                MQPoolService.SendToCoil(InfoCoil.DeleteShcedPlanNo.Data(rcvMsg));

                return;
            }

            // MMP115:套筒静态数据同步
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.SleeveValueSyn))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_Sleeve_Value_Synchronize))
                                                            as MMSL2Rcv.Msg_Sleeve_Value_Synchronize;

                _log.I($"套筒資料同步訊息:{rcvMsg.Code.ToStr()}", $"CODE:{rcvMsg.SleeveCode.ToStr()}");
                _log.D("套筒資料同步訊息", JsonUtil.ToJson(rcvMsg));
                MQPoolService.SendToCoil(InfoCoil.SyncSleeveValue.Data(rcvMsg));
                return;
            }

            // MMP116:垫纸静态数据同步
            if (message.Message_Id.Contains(MMSSysDef.RcvMsgCode.PaperValueSync))
            {
                var rcvMsg = MsgAnalUtil.RawDeserialize(message.Data,
                                                            typeof(MMSL2Rcv.Msg_Paper_Value_Synchronize))
                                                            as MMSL2Rcv.Msg_Paper_Value_Synchronize;

                _log.I($"墊紙資料同步訊息:{rcvMsg.Code.ToStr()}", $"CODE:{rcvMsg.PaperCode.ToStr()}");
                _log.D("墊紙資料同步訊息", JsonUtil.ToJson(rcvMsg));
                MQPoolService.SendToCoil(InfoCoil.SyncPaperValue.Data(rcvMsg));
                return;
            }


        }


        private void RcvObject(object msg)
        {
            _log.E("AThread接收資料-RcvObject", $"無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }
    }
}
