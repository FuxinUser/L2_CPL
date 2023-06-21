using Akka.Actor;
using MsgStruct;
using System.Collections.Generic;
using Controller.Coil;
using DataModel.HMIServerCom.Msg;
using MSMQ;
using MSMQ.Core.MSMQ;
using Controller;
using Core.Define;
using Core.Util;
using static DataMod.Common.MMSMsgProResultModel;
using Controller.Track;
using AkkaSysBase.Base;
using AkkaSysBase;
using static MsgStruct.MMSL2Rcv;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using static DBService.Repository.CoilScheduleEntity;
using static MsgStruct.L1L2Rcv;
using static Core.Define.DBParaDef;
using LogSender;
using DataMod.WMS.LogicModel;
using MsgConvert;
using Controller.Sys;
using DBService;
using System;
using DataMod.LabelPrint;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/03
 * Description: Coil Process 鋼捲狀態與內容變動相關處理
 * Reference: 
 * Modified: 
 */
namespace CoilManager
{
    public class CoilMgr : BaseActor
    {

        private IActorRef _selfActor;
        private ICoilController _coilController;
        private ITrackingController _trkController;
        private ISysController _sysController;

        public CoilMgr(ISysAkkaManager akkaManager, ITrackingController trkController, ICoilController coilController, ISysController sysController, ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);
            _trkController = trkController;
            _coilController = coilController;
            _sysController = sysController;

            _coilController.SetLog(log);
            _trkController.SetLog(log);
            _sysController.SetLog(log);

            //_sysController.SaveAPStatusToL25("SystemAP_7", "1");

            MQPool.GetMQ(nameof(CoilMgr)).Receive(x =>
            {
                var msg = (x as MQPool.MQMessage).Data;
                _selfActor.Tell(msg);
            });

            #region HMI-Server
            // 鋼捲生產排程順序調整與刪除作業
            Receive<CS03_ScheduleChange>(message => TryFlow(() => AdjustOrDeleteCoilSchedule(message)));
            // 上傳PDO給MMS
            Receive<CS06_SendMMSPDO>(message => TryFlow(() => UploadPDOToMMS(message)));
            // 要求MMS下發排程
            Receive<CS01_AckSchedule>(message => TryFlow(() => AskMMSSndCoilSchedule(message)));
            // PDI請求
            Receive<CS02_AckPDI>(message => TryFlow(() => AskMMSSndPDI(message)));
            // CLIENT通知SERVER更新鋼捲秤重資料（毛重）
            Receive<CS08_WeightInput>(message => TryFlow(() => UpdatePDOCoilWeight(message)));
            // HMI 完成匯入排程，通知Server發送Preset40筆
            Receive<CS16_FinishLoadSchedule>(message => TryFlow(() => SndPresetInfo()));
            // HMI 完成匯入PDI，通知Server發送Preset40筆
            Receive<CS17_FinishLoadPDI>(message => TryFlow(() => SndPresetInfo()));
            // HMI 通知修正PDI
            Receive<CS20_InfoPDIModify>(message => TryFlow(() => SavePDI(message)));
            // HMI 通知修改POR子捲號
            Receive<CS21_POR_StripBreakModify>(message => TryFlow(() => ModifyPORCoilID(message)));


            #endregion

            #region L1 Pro

            Receive<Msg_311_ExCoilCut>(message => TryFlow(() => SaveSplitCoil(message)));
            // 12.產線能源消耗作業
            Receive<Msg_316_Utility_Data>(message => TryFlow(() => SndEnergyConsumptionInfo(message)));
            // 結算PDO
            Receive<Msg_307_CoilDismount>(message => TryFlow(() => CalculatePDO(message)));
            // 308更新PDO淨重
            Receive<Msg_308_CoilWeightScale>(message => TryFlow(() => RecaulatePDOCoilWeight(message)));
            // 記錄EnCoilCut
            Receive<Msg_301_EnCoilCut>(message => TryFlow(() => SaveEnCoilCut(message)));
            // 查詢母鋼捲於PDI表格，若有，則記錄收捲時間；若沒有，不須處理
            Receive<Msg_312_NewCoilRec>(message => TryFlow(() => RecordPDICoilWindingTime(message)));

            #endregion

            #region MMS
            // 接收MMS鋼捲基本資料
            Receive<Msg_PDI>(message => TryFlow(() => SavePDI(message)));
            // 接收MMS鋼捲排程
            Receive<Msg_Coil_Schedule>(message => TryFlow(() => SaveCoilSchedule(message)));
            // 鋼捲回退回應（MMXX07）
            Receive<Msg_Res_For_Coil_Reject_Result>(message => TryFlow(() => DeleteCoilSchedule(message)));
            // 生產實積请求
            Receive<Msg_Product_Result_Request>(message => TryFlow(() => SndPDO(message)));
            // 鋼捲整計畫刪除請求
            Receive<Msg_Req_Delete_Schedule_Plan>(message => TryFlow(() => DeleteCoilScheduleByPlanNo(message)));
            // 套筒静态数据同步
            Receive<Msg_Sleeve_Value_Synchronize>(message => TryFlow(() => SynchronizeSleeveData(message)));
            // 墊紙静态数据同步
            Receive<Msg_Paper_Value_Synchronize>(message => TryFlow(() => SynchronizePaperData(message)));
            // 无钢卷PDI应答
            Receive<Msg_Res_For_No_Coil_PDI>(message => TryFlow(() => InfoHMINoPDI(message)));
            // 无钢卷生產应答
            Receive<Msg_Res_For_No_Coil_Schedule>(message => TryFlow(() => InfoHMINoCoil(message)));
            // 反馈PDO是否处理成功
            Receive<Msg_Res_For_PDO_Uploaded>(message => TryFlow(() => InfoHMIPdoUploadedReply(message)));
            #endregion

            ReceiveAny(message => Handle_Any(message));
        }

        // 此流程暫議
        private void RecordPDICoilWindingTime(Msg_312_NewCoilRec msg)
        {

            //var coilID = msg.CoilID.ToStr();

            //// 撈取分切紀錄
            //var inCoilID = _coilController.GetCutRecordTempParentCoilID(coilID);
            //var parentCoilID = string.IsNullOrEmpty(inCoilID) ? coilID : inCoilID;
            //var hadPDI = _coilController.VaildHasPDI(parentCoilID);

            //if (hadPDI)
            //    //記錄收捲時間
            //    _coilController.UpdatePDIFinishTime(parentCoilID);

        }


        /// <summary>
        /// 鋼捲生產排程順序調整與刪除作業
        /// </summary>
        /// <param name="msg"></param>
        private void AdjustOrDeleteCoilSchedule(CS03_ScheduleChange msg)
        {
            // 鋼捲生產排程順序調整
            if (msg.SchStatus == ScheduleStatus.ADJUST)
            {
                // ConsoleOut.Info("收到HMI排程調整訊息");
                _log.I("鋼捲排程調整", $"調整鋼捲{msg.EntryCoilID}排程訊息");

                // 撈取40筆Pro Schedule
                List<string> coilScheduleIDs = _coilController.QueryUnscheduleCoils(CoilDef.DefaultGetScheduleCnts);
                if (coilScheduleIDs == null)
                {
                    _log.A("Server收到調整排程", "目前資料庫無排程資料");
                    MQPoolService.PushHMI("Server收到調整排程","目前資料庫無排程資料");
                    return;
                }
                

                // 通知WMS排程資訊
                MQPoolService.SendToWMS(InfoWMS.CoilScheduleInfoMsg.Data(coilScheduleIDs));

                // 通知MMS鋼捲排程改變 - 發送生產命令順序變更通知（P1MM10）
                MQPoolService.SendToMMS(InfoMMS.CoilSceduleChanged.Data(coilScheduleIDs));

                // 重發Preset給L1
                MQPoolService.SendToDtStp(InfoDataSetup.ScheduleIDsTo201.Data(coilScheduleIDs));

                MQPoolService.PushHMI("Server收到調整排程", "已通知物流與三級系統");

                return;
            }

            // 鋼捲生產排程刪除
            if (msg.SchStatus == ScheduleStatus.DELETE)
            {
                _log.I("鋼捲刪除訊息", $"刪除鋼捲{msg.EntryCoilID}排程訊息，人員 {msg.OperatorID},原因{msg.ReasonCode}");

                // 發送鋼捲刪除命令(P1MM18)           
                MQPoolService.SendToMMS(InfoMMS.CoilSceduleDeleted.Data(msg));

                var saveOk = _coilController.CreateCoilScheduleDelTempRecord(msg.EntryCoilID, CoilDef.ScheduleDelete, string.Empty, msg.OperatorID, msg.ReasonCode, MMSSysDef.Cmd.DelScheduleByHmiReq);

                if (!saveOk)
                {
                    _log.A("Server收到刪除排程", "已發送刪除要求，存取鋼捲刪除暫存失敗");
                    MQPoolService.PushHMI("Server收到刪除排程", "已發送刪除要求，但存取鋼捲刪除暫存失敗");
                }

                _log.I("Server收到刪除排程", "已發送刪除要求");
                MQPoolService.PushHMI("Server收到刪除排程", "已發送刪除要求");

            }
        }

        private void UploadPDOToMMS(CS06_SendMMSPDO msg)
        {
            // 告知MMSSndEdit發送PDO
            _log.I("要求PDO上傳MMS", $"要求上傳{msg.Coil_ID}的PDO");
            MQPoolService.SendToMMS(InfoMMS.ClientInfoSndPDO.Data(msg));

            var outCoilID = msg.Coil_ID.Trim();

            // 存取至L25
            _coilController.CreateL25PDO(msg.Plan_No, outCoilID);

            // 通知WMS
            SndPDOToWMS(outCoilID);

            _log.I("補刪除排程", $"補刪除排程{msg.In_Coil_ID}");
            var isDelOk = _coilController.DeleteCoilScheduleByCoilID(msg.In_Coil_ID);
            if(isDelOk)
                _coilController.CreateCoilScheduleDelRecords(msg.In_Coil_ID, "Server", "", "已產出PDO");

        }

        private void SaveEnCoilCut(Msg_301_EnCoilCut msg)
        {
            _log.I("入口捲切紀錄", "紀錄入口捲切割資料");
            _coilController.CreateEnCoilCutRecordTemp(msg);
        }

        public void SaveSplitCoil(Msg_311_ExCoilCut msg)
        {
            _log.I("切割訊號", $"[{msg.CutModeStr}]切割");

            var coilID = msg.CoilID.ToStr();

            var pdi = _coilController.GetPDI(coilID, PDISchema.EntryCoilID);
            var outCoilID = string.Empty;
            var inCoilID = string.Empty;

            if (pdi != null)
            {
                // 第一次分切 
                // EX: msg給入口捲HE00. 找到PDI. 索取出口捲號CE00.
                //     作第一次分切為CE01存進分切暫存表 SplitCoil:CE01 In_Coil:HE00  OriPDIOut:CE00
                inCoilID = pdi.Entry_Coil_ID;
                outCoilID = pdi.Out_Coil_ID;
            }
            else
            {
                // 第二次分切以上, 故311給的捲號非原捲號, PDI查詢不到,到分切暫存撈取原出口卷號
                // Ex: msg給定捲號為CE01, 找不到PDI.代表分切過，用CE01去查詢分切暫存表索取原出口捲號
                //     作第二次分切CE02 存進分切暫存表 SplitCoil:CE02 In_Coil:HE00  OriPDIOut:CE00
                var cutRecordTemp = _coilController.GetCutRecordTemp(coilID);
                inCoilID = cutRecordTemp.In_Coil_ID;
                outCoilID = cutRecordTemp.OriPDI_Out_Coil_ID;
            }

            // 取樣處理
            if (msg.CutMode == CoilDef.CutSampleCut)
            {
                var samplePos = pdi.SamplePosStr;
                var sampleCoilID = coilID + samplePos;
                var saveOK = _coilController.CreateSampleCoil(msg, pdi, sampleCoilID);
                _log.I("取樣切", $"列印標籤");
                //MQPoolService.SendToLpr(InfoLpr.CoilInExitSK2.Data(coilID));

                var stNo = pdi.St_No;
                var thick = pdi.Entry_Coil_Thick;
                var sampleNo = pdi.Sample_Lot_No;
                MQPoolService.SendToLpr(InfoLpr.SampleCut.Data(new SampleInfo(coilID, stNo, thick, sampleNo, samplePos)));
                return;
            }

            // 頭廢切 尾廢切
            if (msg.CutMode == CoilDef.CutHeadScrapCut || msg.CutMode == CoilDef.CutTailScrapCut)
            {
                _coilController.CreateExitCoilScrapCutRecordTemp(msg);
                return;
            }

            // 所有要成捲切的都要判斷重量
            var coilWeight = msg.CalculateWeightRec;
            if(coilWeight <= CoilDef.ProductCoilWeight)
            {
                _log.A("重量不足無法成捲", $"目前重量{coilWeight}不足{CoilDef.ProductCoilWeight}");
                return;
            }

            // 分切訊號處理
            if (msg.CutMode == CoilDef.CutModeSplitCut)
            {
                var childrenCoil = _coilController.GenSplitChildrenCoilID(outCoilID);
                var saveOK = _coilController.CreateExitCoilCutRecordTemp(msg, childrenCoil, inCoilID, outCoilID);
                if (saveOK)
                    MQPoolService.SendToL1(InfoL1.SndSplitId.Data(childrenCoil));

                return;
            }

            // 斷帶紀錄(目前廢料未處裡記錄)
            if (msg.CutMode == CoilDef.CutModeStripBrake)
            {
                // 做分切
                var childrenCoil = _coilController.GenSplitChildrenCoilID(outCoilID);
                _coilController.CreateExitCoilCutRecordTemp(msg, childrenCoil, inCoilID, outCoilID);
                _coilController.CreateUmontRecord(msg, childrenCoil, outCoilID);
                _coilController.CreateStripBrekInScheduleDeleteCoilRejectTemp(childrenCoil, pdi);
                //if(saveOK)
                MQPoolService.SendToL1(InfoL1.SndSplitId.Data(childrenCoil));

                return;
            }

            // 虛擬切:全卷正常產出直接給定出口捲給一級
            if (msg.CutMode == CoilDef.CutModeVirtualCut)
            {
                var cutNum = _coilController.GetParentCnt(outCoilID);
                var genCoilID = pdi.Out_Coil_ID;

                // > 0 有做過分切, 再做一次分切並傳給一級
                if (cutNum > 0)
                {
                    genCoilID = _coilController.GenSplitChildrenCoilID(outCoilID);
                    _coilController.CreateExitCoilCutRecordTemp(msg, genCoilID, inCoilID, outCoilID);
                }

                MQPoolService.SendToL1(InfoL1.SndSplitId.Data(genCoilID));

                return;
            }
        }

        /// <summary>
        /// 操作通知修改PDI，結果存取至L25
        /// </summary>
        private void SavePDI(CS20_InfoPDIModify msg)
        {
            var entryCoilID = msg.CoilID;
            var planNo = msg.PlanNo;
            _coilController.CreateL25PDI(planNo, entryCoilID);
        }


        /// <summary>
        /// 操作通知修改POR鋼捲號，並傳送結果給L1
        /// </summary>
        private void ModifyPORCoilID(CS21_POR_StripBreakModify msg)
        {
            var entryCoilID = msg.Coil_ID;
            var pdi = _coilController.GetPDI(entryCoilID, PDISchema.EntryCoilID);


            // 先判斷有無此回退資料
            var coilTemp = _coilController.GetCutRecordTempFromParentCoilID(pdi.Entry_Coil_ID, DBParaDef.CutModeReturnCoil, CutTempSchema.In_Coil_ID);

            if (coilTemp == null)
            {              
                var childrenCoil = _coilController.GenSplitChildrenCoilID(entryCoilID);
                var saveOK = _coilController.CreatePORSplitCoilRecordTemp(childrenCoil, pdi.Entry_Coil_ID, pdi.Out_Coil_ID);
                if (saveOK)
                {
                    _log.I("發送New POR捲號", $"發送過New POR捲號 {childrenCoil} 給L1");
                    MQPoolService.SendToL1(InfoL1.SndNewPORId.Data(childrenCoil));
                }


                return;
            }

            _log.I("已發送過New POR捲號", $"已發送過New POR捲號 {coilTemp.Coil_ID} 重新發送給L1");
            MQPoolService.SendToL1(InfoL1.SndNewPORId.Data(coilTemp.Coil_ID));

        }


        /// <summary>
        /// 接收MMS鋼捲基本資料 
        /// </summary>
        private void SavePDI(Msg_PDI msg)
        {
            var coilNo = msg.EntryCoilNo;
            bool saveOK;
            var planNo = msg.PlanNo;
            var matSeqNo = msg.Mat_Seq_No.ToStr().ToNullable<int>() ?? 0;


            if(_coilController.VaildHasPDI(planNo, coilNo))
            {
                // 有:更新PDI
                saveOK = _coilController.UpdatePDI(planNo, coilNo, msg);
            }
            else
            {
                // 無:新增PDI
                saveOK = _coilController.CreatePDI(msg);
            }


            if (_coilController.VaildHasDefect(planNo, coilNo))
            {
               // 有:更新Defect
                saveOK = _coilController.UpdateDefect(msg);
            }
            else
            {
                // 無:新增Defect
                saveOK = _coilController.CreateDefect(msg);
            }

            // PDI接收回應
            var proOk = saveOK ? EventDef.ProOK : EventDef.ProError;
            var rejCode = proOk == EventDef.ProOK ? "" : "PDI執行操作失敗";
            MQPoolService.SendToMMS(InfoMMS.SndPDIProResult.Data(new ProResult(msg.EntryCoilNo, proOk, rejCode)));

            // 通知HMI已接收
            var proStr = proOk.Equals(EventDef.ProOK) ? "成功" : "失敗" + "因為" + rejCode;
            var proContent = "計畫號:" + planNo + "|材料命令順序號:" + matSeqNo + "|鋼捲ID:" + msg.EntryCoilNo;
            var eventPush = new SC03_EventPush(EventDef.ReceiveMMSPDI + proStr, proContent);
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

            // 2.5 存取
            _coilController.CreateL25PDI(msg);


        }
        /// <summary>
        /// 接收MMS鋼捲排程
        /// </summary>
        private void SaveCoilSchedule(Msg_Coil_Schedule mmsSchedule)
        {
            var coilID = mmsSchedule.CoilNo.ToStr();
            var coilShchedules = mmsSchedule.CoilSchedules;
            var inserCnt = mmsSchedule.ScheduleCnt;
            var processOK = false;

            //  刪除所有鋼捲, 插入所有鋼捲
            if (coilID.Equals(CoilDef.InsertAllCoil))
            {
                // 若排程原本為空-更新System TopScheduleLock為0                
                //if (_coilController.QueryUnscheduleCoils(40).Count == 0)
                _sysController.UpdateSysValue(L2SystemDef.CPLGroup, DBColumnDef.SysTopScheduleLock, DBParaDef.NOTUSE);

                processOK = _coilController.DeleteAllIdleSchedule();

                if (processOK)
                {
                    //processOK = _coilController.BatchInsertSchedule(coilShchedules, inserCnt);
                    processOK = _coilController.SequenceCreateSchedule(coilShchedules, inserCnt);
                }

                // 回應處理結果
                InfoOuterSystemProSceduleDone(coilID,
                                              processOK ? EventDef.ProOK : EventDef.ProError,
                                              processOK ? "" : EventDef.ProSchedFail);

                return;

            }

            // 排程有此Coil，包含此Coil以下刪除，並重新插入鋼捲
            if (_coilController.VaildHasSchedule(coilID))
            {

                // 刪除此CoilID以下的資料
                processOK = _coilController.DeleteAppendScheduleByCoilID(coilID);

                // 順序插入
                if (processOK)
                {
                    //processOK = _coilController.BatchInsertSchedule(coilShchedules, inserCnt);
                    processOK = _coilController.SequenceCreateSchedule(coilShchedules, inserCnt);
                }

                // 回應處理結果
                InfoOuterSystemProSceduleDone(coilID,
                                              processOK ? EventDef.ProOK : EventDef.ProError,
                                              processOK ? "" : EventDef.ProSchedFail);


            }

        }


        /// <summary>
        /// 鋼捲回退回應
        /// </summary>       
        public void DeleteCoilSchedule(Msg_Res_For_Coil_Reject_Result msg)
        {
            var coilID = msg.RequestedCoilNo.ToStr();

            //通知HMI  
            var isSucess = msg.ProResult.Equals(MMSSysDef.Cmd.ProNG) ? EventDef.CoilRejectFail : EventDef.CoilRejectSuccess;
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("[刪除排程鋼捲]三級回應" + isSucess, coilID + " " + msg.RejectReason)));

            if (msg.ProResult.Equals(MMSSysDef.Cmd.ProNG))
            {
                // NG : 清除「排程鋼卷刪除暫存記錄(TBL_ScheduleDelete_CoilReject_Temp)」表格
                //_coilController.DeleteSchDelCoilRejectTempRecord(coilID);
                return;
            }

            // 撈取刪除與回退暫存紀錄
            var returnCoilTemp = _coilController.GetReturnCoilTemp(coilID);
            if (returnCoilTemp == null)
            {
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("[刪除排程鋼捲]失敗，Server找不到這顆刪除暫存")));
                return;
            }
            

            // 刪除排程流程
            if (returnCoilTemp.Record_Type.Equals(CoilDef.ScheduleDelete))
            {
                // 刪除流程
                var isDelOk = _coilController.DeleteCoilScheduleByCoilID(coilID);
                if (!isDelOk)
                {

                    return;
                }
                   

                _coilController.CreateCoilScheduleDelRecords(coilID, returnCoilTemp.Create_UserID, returnCoilTemp.Reason_Of_Reject, returnCoilTemp.Remarks);
                _coilController.DeleteSchDelCoilRejectTempRecord(coilID);


                // 撈取40筆Pro Schedule
                var coilScheduleIDs = _coilController.QueryUnscheduleCoils(CoilDef.DefaultGetScheduleCnts);
                if (coilScheduleIDs == null)
                    return;


                // 通知HMI排程已更新
                MQPoolService.SendToPCCom(InfoHMI.ScheduleChangeNotice.Data(new SC04_ScheduleChangeNotice("Server", "")));
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("鋼捲排程刪除", $"已處理完MMS排程訊息,鋼捲號{coilID}")));

                SndPresetAndUnscheduleCoilToWMS();

                return;
            }

            // 回退流程
            var saveOK = _coilController.CreateTempToCoilReject(returnCoilTemp);
            if (saveOK)
            {
                _coilController.DeleteCoilScheduleByCoilID(coilID);
                _coilController.DeleteSchDelCoilRejectTempRecord(coilID);
                _coilController.CreateCoilScheduleDelRecords(coilID, returnCoilTemp.Create_UserID, returnCoilTemp.Reason_Of_Reject, returnCoilTemp.Remarks);
                _coilController.DelectLeaderTemp(coilID);


                // 通知HMI排程已更新
                MQPoolService.SendToPCCom(InfoHMI.ScheduleChangeNotice.Data(new SC04_ScheduleChangeNotice("Server", "")));
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("鋼捲排程刪除", $"已處理完MMS排程訊息,鋼捲號{coilID}")));

                SndPresetAndUnscheduleCoilToWMS();
            }



            _log.I("發鋼捲回退實績", $"通知WMS鋼捲{coilID}退料要求");
            var reqMsg = new ProdLineCoilReq(CoilDef.ReqWMSRejectCoil, coilID, returnCoilTemp.Reject_Skid);
            MQPoolService.SendToWMS(InfoWMS.RejectCoilReqMsg.Data(reqMsg));

        }
        /// <summary>
        /// 生產實積請求
        /// </summary>
        public void SndPDO(Msg_Product_Result_Request msg)
        {
            _log.I("要求生產實績(PDO)", $"要求鋼捲{msg.CoilNoID}PDO資料");
            MQPoolService.SendToMMS(InfoMMS.MMSInfoSndkPDO.Data(msg.CoilNoID));
        }
        /// <summary>
        /// 要求MMS下發排程
        /// </summary>
        public void AskMMSSndCoilSchedule(CS01_AckSchedule msg)
        {
            _log.I("要求MMS下發排程", $"要求MMS下發鋼捲{msg.CoilID}排程");
            MQPoolService.SendToMMS(InfoMMS.AskCoilSchedule.Data(msg.CoilID));
        }
        /// <summary>
        /// PDI請求
        /// </summary>
        public void AskMMSSndPDI(CS02_AckPDI msg)
        {
            _log.I("要求MMS下發PDI", $"要求MMS下發鋼捲{msg.Coil_ID}PDI");
            MQPoolService.SendToMMS(InfoMMS.AskPDI.Data(msg.Coil_ID));
        }
        /// <summary>
        /// 整計畫刪除請求
        /// </summary>
        public void DeleteCoilScheduleByPlanNo(Msg_Req_Delete_Schedule_Plan msg)
        {
            var canDeleteAllSchedule = true;
            var coilSchedules = _coilController.GetCollScheduleByPlanNo(msg.PlanNo);
            var planNo = msg.PlanNo;

            if (coilSchedules == null)
            {
                // 回覆整計畫刪除電文 : 失敗
                _log.E("PDI整計畫刪除失敗", "無此計畫");
                MQPoolService.SendToMMS(InfoMMS.ResPlanNoShedDelResult.Data(new ProResult("", MMSSysDef.Cmd.ProNG, "No Plan Number")));
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("三即通知整計畫刪除失敗", "無此鋼卷鋼卷計畫")));
                return;
            }

            // 判定鋼捲狀態是否有 非為N新鋼捲 或 非為R要求入料 狀態的鋼捲
            foreach (TBL_Production_Schedule schedule in coilSchedules)
            {
                if (!IsScheduleStatutsIdle(schedule))
                {
                    canDeleteAllSchedule = false;
                    continue;
                }
            }


            if (!canDeleteAllSchedule)
            {
                MQPoolService.SendToMMS(InfoMMS.ResPlanNoShedDelResult.Data(new ProResult(msg.PlanNo, EventDef.ProError, MMSSysDef.Cmd.DelSchedulePlanNoReject)));
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("整計畫刪除失敗", $"計畫號{planNo}部份鋼捲已要求入料或已上鋼捲,執行逐筆刪除排程要求")));

                foreach (TBL_Production_Schedule schedule in coilSchedules)
                {
                    var coilID = schedule.Coil_ID.Trim();

                    if (!IsScheduleStatutsIdle(schedule))
                        continue;

                    var saveOk = _coilController.CreateCoilScheduleDelTempRecord(coilID, CoilDef.ScheduleDelete, planNo, msg.OperatorID, msg.ReasonCode, MMSSysDef.Cmd.DelScheduleByPlanNo);

                    //if (saveOk)
                    //{
                    var infoDelCoilSchedule = new CS03_ScheduleChange(L2SystemDef.L2, ScheduleStatus.DELETE, coilID, msg.OperatorID, msg.ReasonCode);
                        // 發送鋼捲刪除命令(G1MM18)           
                    MQPoolService.SendToMMS(InfoMMS.CoilSceduleDeleted.Data(infoDelCoilSchedule));
                    //}
                }

                return;
            }


            var deleteOk = _coilController.DeleteBatchScheduleByPlanNo(planNo);

            if (deleteOk)
            {
                foreach (TBL_Production_Schedule coil in coilSchedules)
                    _coilController.CreateCoilScheduleDelRecords(coil.Coil_ID, msg.OperatorID, msg.ReasonCode, MMSSysDef.Cmd.DelScheduleByPlanNo);

                // 回覆整計畫刪除電文
                MQPoolService.SendToMMS(InfoMMS.ResPlanNoShedDelResult.Data(new ProResult(msg.PlanNo, EventDef.ProOK, MMSSysDef.Cmd.DelScheduleByPlanNo)));
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("整計畫刪除成功", $"已刪除計畫號{planNo}全部排程")));
                MQPoolService.SendToPCCom(InfoHMI.ScheduleChangeNotice.Data(new SC04_ScheduleChangeNotice("Server", "")));
                SndPresetAndUnscheduleCoilToWMS();
            }
        }

        private bool IsScheduleStatutsIdle(TBL_Production_Schedule schedule)
        {
            return schedule.Schedule_Status.Equals(CoilDef.NewCoil_Statuts)
                || schedule.Schedule_Status.Equals(CoilDef.RequestEntryCoil_Statuts);
        }

        /// <summary>
        /// 能源消耗訊息發送(暫寫)
        /// </summary>
        public void SndEnergyConsumptionInfo(Msg_316_Utility_Data msg)
        {
            _log.I("能源消耗訊息", JsonUtil.ToJson(msg));
            MQPoolService.SendToMMS(InfoMMS.SndEnergyConsumptionInfo.Data(msg));

        }

        /// <summary>
        /// 結算PDO - 更新理論重-> 淨重
        /// </summary>
        public void CalculatePDO(Msg_307_CoilDismount msg)
        {
            _log.I("L1通知Dismount資訊", "結算PDO");

            // L1所帶鋼捲號為出口捲號
            var outCoilID = msg.CoilID.ToStr();

            // 檢查「鋼捲退料暫存記錄」，若有資料存至鋼捲退料暫存記錄
            //if (_coilController.VaildHasScheduleDeleteCoilRejectTemp(outCoilID))
            //{
            //    // 退料無生產結束時間
            //    var saveOK = _coilController.CreateCoilDismountInfoInScheduleDeleteCoilRejectTemp(msg, outCoilID);
            //    return;
            //}


            // 撈取母鋼捲出口ID
            var splitRecord = _coilController.GetCutRecordTemp(outCoilID);
            outCoilID = splitRecord == null ? outCoilID : splitRecord.OriPDI_Out_Coil_ID;

            // 更新PDI 鋼捲生產結束時間
            _coilController.UpdatePDIFinishTime(outCoilID, DateTime.Now);

            // Gen PDO
            var pdo = _coilController.DismountCoilGenPDO(msg, outCoilID);
            var savePDOOk = _coilController.CreatePDO(pdo);


            // 更新鋼捲狀態 - Done
            _coilController.UpdateScheduleStatuts(pdo.In_Coil_ID, CoilDef.ProduceDone_Statuts);

            // 刪除此鋼捲流程
            if (!savePDOOk)
                return;
            var entryCoilID = pdo.In_Coil_ID;
            var isDelOk = _coilController.DeleteCoilScheduleByCoilID(entryCoilID);
            _coilController.CreateCoilScheduleDelRecords(entryCoilID, "Server", "", "已產出PDO");

            // 通知計算平均收卷張力
            MQPoolService.SendToDtProGtr(InfoDtProGtr.CalculateProcessAvgData.Data(pdo.Out_Coil_ID));

            // 通知WMS
            SndPDOToWMS(outCoilID);

            // 通知HMI刷新畫面
            MQPoolService.SendToPCCom(InfoHMI.ScheduleChangeNotice.Data(new SC04_ScheduleChangeNotice("Server", "")));
        }

        /// <summary>
        /// 更新PDO毛重. 並重新計算淨重 
        /// </summary>
        private void RecaulatePDOCoilWeight(Msg_308_CoilWeightScale msg)
        {
            var outCoilID = msg.CoilID.ToStr();
            var coilWeight = msg.CoilWeight;

            _log.I("接收CoilWeightScale", "接收到CoilWeightScale重新結算PDO重量");

            //// 檢查「鋼捲退料暫存記錄」，若有資料存至鋼捲退料暫存記錄
            //if (_coilController.VaildHasScheduleDeleteCoilRejectTemp(outCoilID))
            //{
            //    // 退料無生產結束時間
            //    var saveOK = _coilController.CreateCoilWeightScaleInScheduleDeleteCoilRejectTemp(msg, outCoilID);
            //    return;
            //}


            // 更新鋼捲襯重
            _coilController.RecaulateWeightAndUpdatePDO(outCoilID, coilWeight);
            _log.I($"標籤機通知列印", $"鋼捲[{outCoilID}]襯重完成，標籤列印通知");
            MQPoolService.SendToLpr(InfoLpr.CoilInExitSK2.Data(outCoilID));


            SndPDOToWMS(outCoilID);
        }

        /// <summary>
        /// 307, 308,  HMI更改時
        /// </summary>
        /// <param name="outCoilID"></param>
        private void SndPDOToWMS(string outCoilID)
        {
            //撈PDO發送給WMS
            var pdo = _coilController.GetFinalPDO(outCoilID);
            var sleeve = _coilController.GetSleeveData(pdo.Sleeve_Type_Exit_Code);
            _log.I($"鋼捲產出", $"發送鋼捲({outCoilID})PDO資料給WMS");
            var wmsPdoInfo = pdo.ConvertWMSPdoInfo(sleeve);
            MQPoolService.SendToWMS(InfoWMS.InfoiCoilPDOMsg.Data(wmsPdoInfo));
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush(EventDef.ReceiveWMSCancelMsg, $"發送{pdo.Out_Coil_ID}產出資訊給WMS")));
        }

        private void UpdatePDOCoilWeight(CS08_WeightInput msg)
        {
            _log.I("更新鋼捲秤重資料通知", $"CLIENT通知SERVER更新鋼捲{msg.OutCoilID}毛重{msg.WeightInput}");
            if (msg.CoilWt == -1.0)
            {
                _log.E("更新鋼捲秤重資料錯誤", $"使用者輸入資料錯誤，輸入資料{msg.WeightInput} 無法轉型");
                return;
            }
            _coilController.RecaulateWeightAndUpdatePDO(msg.OutCoilID, msg.CoilWt);


            SndPDOToWMS(msg.OutCoilID);
        }

        /// <summary>
        /// 通知
        ///     MMS:回覆接收排程電文
        ///     HMI:通知排程已更新
        ///     L1: 通知Preset
        /// </summary>
        /// <param name="coilID">三級下發排程所帶CoilIDNo</param>
        /// <param name="isProOk">排程處裡是否成功</param>
        /// <param name="rejectCause">排程處裡失敗原因</param>
        private void InfoOuterSystemProSceduleDone(string coilID, string isProOk, string rejectCause = "")
        {

            // 回覆接收排程電文 
            MQPoolService.SendToMMS(InfoMMS.ResCoilSchedResult.Data(new ProResult(coilID, isProOk, rejectCause)));

            // 失敗
            if (isProOk.Equals(EventDef.ProError))
            {
                MQPoolService.PushHMI("排程未更新", $"MMS排程處裡訊息失敗，原因為{rejectCause}");
                //MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush("排程未更新", $"MMS排程處裡訊息失敗，原因為{rejectCause}")));
                return;
            }

            // 成功
         
            MQPoolService.SendToPCCom(InfoHMI.ScheduleChangeNotice.Data(new SC04_ScheduleChangeNotice("Server", "")));
            MQPoolService.PushHMI("排程已更新", $"MMS排程處裡訊息成功");

            SndPresetAndUnscheduleCoilToWMS();

        }

        /// <summary>
        /// 將未上線排程鋼捲ID傳送至DataSetup && 並傳送鋼卷排程給WMS
        /// </summary>
        private void SndPresetAndUnscheduleCoilToWMS()
        {

            var coilIDs = _coilController.QueryUnscheduleCoils(CoilDef.DefaultGetScheduleCnts);

            if (coilIDs == null)
            {
                _log.E("通知L1 Preset資訊失敗", "撈取40筆鋼捲失敗");
                _log.E("通知WMS 排程資訊失敗", "撈取40筆鋼捲失敗");
                return;
            }


            // 傳送40筆給Stp
            _log.I("通知DataSetup", "通知DataSetup組40筆Preset資料發送給L1");
            MQPoolService.SendToDtStp(InfoDataSetup.ScheduleIDsTo201.Data(coilIDs));
            // 傳送排程給WMS
            _log.I("傳送排程資訊給WMS", "通知WMS排程資訊");
            MQPoolService.SendToWMS(InfoWMS.CoilScheduleInfoMsg.Data(coilIDs));

        }


        /// <summary>
        /// 將未上線排程鋼捲ID傳送至DataSetup
        /// </summary>
        private void SndPresetInfo()
        {
            // 將未上線排程鋼捲ID傳送至DataSetup
            //var coilIDs = _coilController.GetUnscheduleCoils(40);

            //if (coilIDs.Count == 0)
            //{
            //    _log.E("撈取40筆鋼捲", "無未上線鋼卷");
            //    return;
            //}

            //if (coilIDs == null)
            //{
            //    _log.E("通知DataSetup", "撈取40筆鋼捲失敗");
            //    return;
            //}

            _log.I("通知DataSetup", "通知DataSetup組40筆Preset資料發送給L1");

            SndPresetAndUnscheduleCoilToWMS();

            //MQPoolService.SendToDtStp(InfoDataSetup.ScheduleIDsTo201.Data(coilIDs));
        }

        /// <summary>
        /// 无钢卷PDI应答
        /// </summary>
        private void InfoHMINoPDI(MMSL2Rcv.Msg_Res_For_No_Coil_PDI noPdiMsg)
        {
            var eventPush = new SCCommMsg.SC03_EventPush(EventDef.MMSNoPDI, $"三級回覆無{noPdiMsg.Mat_No.ToStr()}鋼卷PDI");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
        }

        /// <summary>
        /// 无钢卷生產应答
        /// </summary>
        private void InfoHMINoCoil(Msg_Res_For_No_Coil_Schedule msg)
        {
            var eventPush = new SC03_EventPush(EventDef.MMSNoCoil, $"三級回覆無鋼卷生產命令 {msg.Mat_No.ToStr()}");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

            var coilNo = msg.Mat_No.ToStr();
            
            if (coilNo.Equals(MMSSysDef.Cmd.NoCoilSchedule))
                _coilController.DeleteAllSchedule();
           
        }

        private void InfoHMIPdoUploadedReply(Msg_Res_For_PDO_Uploaded msg)
        {
            var planNo = msg.Plan_No.ToStr();
            var exCoilID = msg.Mat_No.ToStr();
            var succFlag = msg.Succ_Flag.ToStr();

            var flagStr = succFlag == "1" ? "成功" : "失败";
            var errMsg = $"{(string.IsNullOrEmpty(msg.Err_Msg.ToStr()) ? "" : ",")}{msg.Err_Msg.ToStr()}";
            var reply = new SC08_PdoUploadedReply($"三级回覆上传PDO({exCoilID},{planNo}){flagStr}{errMsg}");
            MQPoolService.SendToPCCom(InfoHMI.PdoUploadedReply.Data(reply));

            //  新增上傳 PDO 的回覆到資料表
            if (_coilController.CreatePdoUploadedReply(msg))
                if (succFlag == "1")
                {
                    //  更新 PDO upload flag
                    _coilController.UpdateUploadPDOCheck(planNo, exCoilID, true);

                    //  通知 DataGathering 的 DtProGtr 處理給 L2.5 生產數據
                    var pdo = _coilController.GetPDO(planNo, exCoilID);
                    MQPoolService.SendToDtProGtr(
                        InfoDtProGtr.ProProcessData.Data(
                            //  這邊沿用原本的方式呼叫，原本是從 HMI 觸發，因此在這邊模擬組 CS06_SendMMSPDO 訊息
                            new CS06_SendMMSPDO
                            {
                                Source = "CPL1_HMI",
                                ID = "SendMMSPDO",
                                Coil_ID = exCoilID,
                                In_Coil_ID = pdo.In_Coil_ID,
                                OperatorID = pdo.PDO_Uploaded_UserID,
                                Plan_No = planNo,
                                FinishTime = $"{pdo.FinishTime:yyyy-MM-dd HH:mm:ss.fff}"
                            }));
                }
        }

        /// <summary>
        /// 套筒同步
        /// </summary>
        private void SynchronizeSleeveData(Msg_Sleeve_Value_Synchronize msg)
        {
            var syncOk = _coilController.SyncSleeveValue(msg);
            var eventPush = new SC03_EventPush($"套筒資料同步{msg.Action}{syncOk.ToStr()}");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
        }

        /// <summary>
        /// 墊紙同步
        /// </summary>
        private void SynchronizePaperData(Msg_Paper_Value_Synchronize msg)
        {
            var syncOk = _coilController.SyncPaperValue(msg);
            var eventPush = new SCCommMsg.SC03_EventPush($"墊紙資料同步{msg.Action}{syncOk.ToStr()}");
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
        }

        // 順序一筆筆刪除 (暫時棄用)
        //[Obsolete]
        //public void SequentialInsertInDB(IEnumerable<string> coilIDs, int coilNum)
        //{

        //    var SeqN0 = (short)_coilController.GetCoilScheduleTotalCount();
        //    if (SeqN0 == -1)
        //        return;

        //    int count = 1;
        //    string processResult;

        //    foreach (string coilID in coilIDs)
        //    {
        //        SeqN0++;

        //        var insertNum = _coilController.InsertCoilSchedule(coilID, SeqN0);

        //        if (insertNum > 0)
        //        {
        //            processResult = EventDef.processOk;
        //            _log.D("鋼捲生產命令", $"新增鋼捲{coilID}至排成資料庫");
        //        }
        //        else
        //        {
        //            processResult = EventDef.processError;
        //            _log.E("鋼捲生產命令", $"新增鋼捲{coilID}至排成資料庫失敗");
        //            continue;
        //        }


        //        // 發送钢卷生产命令应答              
        //        MQPoolService.SendToMMS(InfoMMS.ResCoilSchedResult.Data(new ProResult(coilID, processResult)));

        //        count++;
        //        // 根據下發鋼捲數(CoilNum)判定 (第1筆到第Num筆)
        //        if (count > coilNum)
        //            break;
        //    }
        //}

        private void Handle_Any(object message)
        {
            _log.E("Akka Receive", $"Received an unhandled message!!! type:{message.GetType().ToString()} from Sender:{Sender.Path.ToString()}");
        }
    }
}
