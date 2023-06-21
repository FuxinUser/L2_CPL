using Akka.Actor;
using MsgStruct;
using Core.Define;
using DataMod.WMS.LogicModel;
using Controller.Track;
using Controller.Coil;
using MSMQ;
using MSMQ.Core.MSMQ;
using Controller;
using Core.Util;
using DBService;
using static MsgStruct.L2L1Snd;
using AkkaSysBase;
using AkkaSysBase.Base;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using static DataMod.Response.RespnseModel;
using MsgConvert;
using LogSender;
using Controller.Sys;
using System;

namespace Tracking.Actor
{
    /**
     Author:ICSC 余士鵬
     Date:2019/11/5
     Desc:鋼捲追蹤與位置相關處理
    **/
    public class TrkMgr : BaseActor
    {
        private IActorRef _selfActor;
        private ITrackingController _trkController;
        private ICoilController _coilController;
        private ISysController _sysController;

        public TrkMgr(ISysAkkaManager akkaManager, 
                      ITrackingController trkController, 
                      ICoilController coilController, 
                      ISysController sysController, 
                      ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);
            _trkController = trkController;
            _coilController = coilController;
            _sysController = sysController;

            _coilController.SetLog(log);
            _trkController.SetLog(log);
            _sysController.SetLog(log);

            //_sysController.SaveAPStatusToL25("SystemAP_8", "1");

            MQPool.ReceiveFromTrk(x =>
            {
                var msg = (x as MQPool.MQMessage).Data;
                _selfActor.Tell(msg);
            });

            // 自動進料狀態切換
            Receive<CS10_Coil_AutoFeedModeChange>(message => TryFlow(() => ChekCoilProdLineEnterStatus(message)));
            // ID掃碼確認，入口段鋼捲ID更正[目前看起來已無此流程]
            //Receive<CS04_RenameCoil>(message => TryFlow(() => RenameEntryCoil(message)));
            // 手動入料 :  直接發入料要求
            Receive<CS11_Coil_ManualFeed>(message => TryFlow(() => SndEntryCoilReqMsg(message)));
            // 鋼捲生產入料(退料)作業 - 鋼捲生產[退料]作業
            Receive<CS05_RejectCoil>(message => TryFlow(() => RejectCoilProcess(message)));
            // 在鞍座上手動操作入料並通知Server
            Receive<CS12_Coil_SkidFeed>(message => TryFlow(() => PreSndPresetMsg(message)));
            // HMI手動刪除畫面鞍作上鋼捲，通知L1 
            Receive<CS13_DeleteSidCoil>(message => TryFlow(() => ProHMIDelCoilSkidNo(message)));
            // 出口段出料
            Receive<CS14_DeliveryCoilOut>(message => TryFlow(() => DeliveryCoilOut(message)));
            // 天車入料時選擇鋼捲ID
            Receive<CS18_CarneEntryCoilSelect>(message => TryFlow(()=>FinishEntryFlow(message.coilID, message.coilID, message.SKNo, true)));
            // 3.鋼捲生產入料(退料)作業 - 鋼捲生產入料作業
            Receive<L1L2Rcv.Msg_305_TrackMapEn>(message => TryFlow(() => UpdateEnTrkMap(message)));
            // 7.鋼捲生產追蹤作業
            Receive<L1L2Rcv.Msg_306_TrackMapEx>(message => TryFlow(() => UpdateExTrkMap(message)));
            // ReqTrackMap
            Receive<L1L2Rcv.Msg_303_ReqTrackMap>(message => TryFlow(() => SndCurrentCoilMap(message)));

            // 3鋼捲生產退料作業 - 鋼捲生產入料作業 (WP11)
            Receive<WMS_L2_Rcv.WPx1_CompleteOfFeeding>(message => TryFlow(() => WMSFinishMsgProcess(message)));
          
            // WMS 入料/出料/退料要求回復訊息
            Receive<WMS_L2_Rcv.WPx3_RequestResponse>(message => TryFlow(() => WMSResReqMsgProcess(message)));


        }

        // 鋼捲生產入料作業
        private void UpdateEnTrkMap(L1L2Rcv.Msg_305_TrackMapEn L1EnTrkMsg)
        {

            var preCoilMap = _trkController.GetTrackMap();
            if (preCoilMap == null)
            {
                _log.I("撈取Tracking Map失敗", "抓Trk值失敗，請檢察DB連線");
                return;
            }
          
            // 更新資料庫Entry Track  Map           
            _trkController.UpdateEntryTrackMap(L1EnTrkMsg);

            var prePOR = preCoilMap.POR.Trim();
            var nowPOR = L1EnTrkMsg.POR;

            // 鋼捲到達POR : 記錄鋼捲開始生產時間           
            if (IsCoilIn(prePOR, nowPOR))
            {
                _log.I($"鋼捲到達POR", $"POR入鋼捲{nowPOR}");            
                _coilController.UpdatePDIStarTime(nowPOR, DateTime.Now);
                _coilController.UpdateScheduleStatuts(nowPOR, CoilDef.Producing_Statuts);

                //  將上開捲機紀錄更新到前一顆上開捲機紀錄
                var sysParam = _sysController.GetSystemParameter(DBColumnDef.LoadCoilTime);
                _sysController.UpdateSysParam(DBColumnDef.LoadCoilTimePre, sysParam.Value, sysParam.ValueDate);

                //  將最新上開捲機記錄更新
                _sysController.UpdateSysParam(DBColumnDef.LoadCoilTime, nowPOR, DateTime.Now);

                // 通知清空Sider Trimmer資料
                MQPoolService.SendToDtProGtr(InfoDtProGtr.DeleteAllSiderTrimmer.Data(MQCmdStr.DtProGtrCmd.DeleteAllSiderTrimmer));

            }
       
        }   

        // 鋼捲生產追蹤作業
        private void UpdateExTrkMap(L1L2Rcv.Msg_306_TrackMapEx msg)
        {

            var preCoilMap = _trkController.GetTrackMap();

            var preTR = preCoilMap.TR.Trim();
            var nowTR = msg.TR;
            
            var preDSK02 = preCoilMap.Delivery_SK02.Trim();
            var nowDSK02 = msg.DeliverySK02;

            var preTOP = preCoilMap.Delivery_TOP.Trim();
            var nowTOP = msg.DeliveryTOP;


            if (preCoilMap == null)
            {
                _log.I("撈取Tracking Map失敗", "抓Trk值失敗，請檢察DB連線");
                return;
            }

            // 更新Delivery Track Map
            _trkController.UpdateExitTrackMap(msg);

            
            //if(IsCoilOut(preTR, nowTR))
            //{
                /**
               * 鋼捲下TR
               * 1. 紀錄鋼捲生產結束
               */
                //_log.I($"鋼捲移出", $"鋼捲[{preTR}]重[TR]位置移出");
                //_coilController.UpdatePDIFinishTime(preTR);

                //var pdi = _coilService.GetPDI(preTR);
                //var cutRecord = _coilService.GetCutRecord(preTR);
                //if (pdi == null || cutRecord == null)
                //    return;
                              
                //var sampleCnt = cutRecord.Where(x => x.CutMode == CoilLogicDef.CutSampleCut && x.CutDevice.Equals("2")).Count();
                //if(pdi.SampleCnt <= sampleCnt)
                //{
                //    _coilService.UpdateSampleFlag(true, preTR);
           
                //}
                //else
                //{
                //    // 同試批號處理
                //}

            //}

            if (IsCoilIn(preTOP, nowTOP))
            {

                //_log.I($"鋼捲移入", $"鋼捲[{nowDSK02}]到達[DTOP]位置-發鋼捲PDO資料");
                //var pdo = _coilController.GetFinalPDO(nowTOP);
                //var wmsPdoInfo = pdo.ConvertWMSPdoInfo();
                //MQPoolService.SendToWMS(InfoWMS.InfoiCoilPDOMsg.Data(wmsPdoInfo));

                _log.I($"鋼捲移入", $"鋼捲[{nowDSK02}]到達[DTOP]位置-發WMS出料要求");
                MQPoolService.SendToWMS(InfoWMS.InfoCoilEntryOrDeliveryReq.Data(new ProdLineCoilReq(CoilDef.ReqWMSDeliveryCoil, nowTOP, WMSSysDef.SkPos.DTop.ToString())));
                
            }
            // - 更正在秤重時印出
            //if (IsCoilIn(preDSK02, nowDSK02))
            //{

            //    _log.I($"鋼捲移入", $"鋼捲[{nowDSK02}]到達[DSK02]位置-標籤列印通知");
            //    MQPoolService.SendToLpr(InfoLpr.CoilInExitSK2.Data(msg.DeliverySK02));
               
            //}
               
         
        }

        // 自動進料狀態切換
        private void ChekCoilProdLineEnterStatus(CS10_Coil_AutoFeedModeChange msg)
        {
            
            // 判定Auto Flag是否為自動進料
            if (_sysController.VaildSystemAutoValueOn(L2SystemDef.CPLGroup, DBColumnDef.SysParaAutoInputFlag, "是否為自動進料"))
            {             
                //// 判定EntryTop 是否有鋼捲 
                //if (!_trkService.HasEntryTopCoilID())
                //    // 無:發PW15 To WMS 入料要求
                //    MQPoolUtil.SendToWMS(InfoWMS.EntryCoilReqMsg.Data(new ProdLineCoilReq(CoilLogicDef.ReqWMSEntryCoil, "")));

                return;
            }

                          
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush( EventDef.SystemAutoFitChange, "")));

        }
        
        // 手動入料 :  直接發入料要求
        private void SndEntryCoilReqMsg(CS11_Coil_ManualFeed msg)
        {
         
            _log.I("HMI手動入料要求", "發送PW15 Entry Req給WMS");
            // 發PW15 To WMS 入料要求
            var pdi = _coilController.GetPDI(msg.CoilID, DBParaDef.PDISchema.EntryCoilID);
            MQPoolService.SendToWMS(InfoWMS.InfoCoilEntryOrDeliveryReq.Data(new ProdLineCoilReq(CoilDef.ReqWMSEntryCoil, msg.CoilID, WMSSysDef.SkPos.ETop.ToString(), direction: pdi.Uncoiler_Direction)));
        }
        
        // 物流[入料/出料/退料] 完成通知 
        private void WMSFinishMsgProcess(WMS_L2_Rcv.WPx1_CompleteOfFeeding msg)
        {

            var flag = msg.Flag.ToStr();
            var coilNo = msg.CoilNoID;
            var skID = msg.ScanNoID;

            switch (flag)
            {
                case CoilDef.EntryCoil:                    
                    FinishEntryCoil(msg);
                    break;
                case CoilDef.DeliveryCoil:
                    FinishDeliveryCoil(msg);
                    break;
                case CoilDef.RejectCoil:
                    FinishRetrunCoil(msg);
                    break;
            }


        }


        // [入料]完成
        private void FinishEntryCoil(WMS_L2_Rcv.WPx1_CompleteOfFeeding msg)
        {

            var coilNo = msg.CoilNoID;
            var scanNoID = msg.ScanNoID;
 
            // 台車入料
            if (scanNoID.Equals(string.Empty))
            {
                _log.I("台車入料", $"鋼捲{msg.CoilNoID}由台車入料點");
                FinishEntryFlow(coilNo, scanNoID, msg.L1201PresetPos, false);
                return;
            }

            // 天車入料
            if (coilNo.Equals(scanNoID))
            {
                _log.I("天車入料", $"鋼捲{msg.CoilNoID}由天車入SK1點");
                FinishEntryFlow(coilNo, scanNoID, msg.L1201PresetPos, true);
                return;
            }

            // 天車入料 但ID比對不對通知User確認
            //_log.I("天車入料", $"鋼捲{msg.CoilNoID}由天車入SK1點, 掃描鋼捲與實際鋼捲編號不同,通知操作確認");
            //var craneEntryCoil = new SC06_CraneEntryCoil(scanNoID, msg.CoilSKPos());
            //MQPoolService.SendToPCCom(InfoHMI.CraneEntryCoil.Data(craneEntryCoil));

            // 天車入料 但ID比對不對 直接入料
            FinishEntryFlow(coilNo, scanNoID, msg.L1201PresetPos, true);

        }
        private void FinishEntryFlow(string coilID, string scanID, int presetSKNo, bool isCraneEntry) {


            // TODO 通知HMI
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush(EventDef.EntryCoilDone, "鋼捲入料完成")));
    
            // 通知MMS 鋼捲上鞍座               
            MQPoolService.SendToMMS(InfoMMS.CoilLoadedOnSk.Data(coilID));

            // 通知L1 Preset201
            var specificPreset = new SpecificPreset(coilID, presetSKNo);
            MQPoolService.SendToDtStp(InfoDataSetup.SpecificIDTo201.Data(specificPreset));

            // 紀錄鋼捲入料時間
            _coilController.UpdatePDIEntryTime(coilID, DateTime.Now);

            //// 從排程移出已入料鋼捲 - For排程狀態先不刪除
            //var isDelOK = _coilService.DeleteCoilSchedule(coilID);
            //_log.I("鋼捲入料完成", $"從棑程刪除鋼捲{coilID} 是否成功=>{isDelOK}");

            // 更新鋼捲狀態為已入料
            _coilController.UpdateScheduleStatuts(coilID, CoilDef.EntryCoilDone_Statuts);


            // 紀錄鋼捲掃描成功
            if (isCraneEntry)
            {
                _coilController.UpdatePDIEntryScanCoilInfo(scanID, true);
                _coilController.UpdateScheduleStatuts(coilID, CoilDef.IdentifyOK_Statuts);
            }
           
        }

        //[出料]完成
        private void FinishDeliveryCoil(WMS_L2_Rcv.WPx1_CompleteOfFeeding msg)
        {
            var coilID = msg.CoilNo.ToStr();

            _log.I($"WMS通知{coilID}出料完成", "接收出料完成訊息");

            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush(EventDef.DeliveryCoilDone, $"WMS通知{coilID}出料完成")));

            // 告知L1發204
            MQPoolService.SendToL1(InfoL1.SndDelSkEntryID.Data(msg.SkidNo.ToStr()));
            return;
        }

        // [退料]完成
        private void FinishRetrunCoil(WMS_L2_Rcv.WPx1_CompleteOfFeeding msg)
        {
            var coilID = msg.CoilNoID;

            // 更新CoilMap
            _log.I("物流退料完成 ", "接收退料完成訊息");

            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush(EventDef.DeliveryCoilDone, $"WMS通知{coilID}退料完成")));

            // 告至L1發204
            MQPoolService.SendToL1(InfoL1.SndDelSkEntryID.Data(msg.SkidNo.ToStr()));
        }


        // Client通知鋼捲生產退料作業
        private void RejectCoilProcess(CS05_RejectCoil msg)
        {
            var returnCoilID = msg.CoilID;

            // 發鋼捲回退實績
            var returnCoilInfo = _coilController.GetReturnCoilTemp(returnCoilID);
            if (returnCoilInfo == null)
            {
                _log.E("發鋼捲回退實績失敗", $"無此筆退料實績資料");             
                return;
            }

            var retrunCoilDefect = _coilController.GetDefect(returnCoilInfo.Plan_No, returnCoilID);
            if(retrunCoilDefect == null)
                retrunCoilDefect = new DefectData();
            
    
            _log.I("發鋼捲回退實績", $"通知MMS鋼捲{returnCoilID}退料實績");
            MQPoolService.SendToMMS(InfoMMS.CoilRejectResult.Data(new CoilRejectInfo(returnCoilInfo, retrunCoilDefect, CoilDef.FinalCoil)));

            // 存取至L25
            _coilController.CreateL25CoilRejectResult(returnCoilInfo);
        }
        
        // ID掃碼確認，入口段鋼捲ID更正
        //private void RenameEntryCoil(CS04_RenameCoil msg)
        //{
        //    var entryCoilID = msg.Coil_ID;

        //    // 更新PDI Entry CheckID   && Original Coil      
        //    _coilController.UpdateEntryScanCoilInfo(entryCoilID, true);

        //    // 存取更名前的CoilID
        //    _coilController.UpdatePDIOriginalCoilNo(entryCoilID, msg.Coil_ID);


        //    // 重發201
        //    var specificPreset = new SpecificPreset(entryCoilID, msg.Postion);
        //    MQPoolService.SendToDtStp(InfoDataSetup.SpecificIDTo201.Data(specificPreset));
    

        //}

        // Client通知出口段出料
        private void DeliveryCoilOut(CS14_DeliveryCoilOut msg)
        {
         

            _log.I("鋼捲生產出料作業", $"通知WMS出料{msg.CoilID}");
            var reqMsg = new ProdLineCoilReq(CoilDef.ReqWMSDeliveryCoil, msg.CoilID, msg.Pos.ToString());
            MQPoolService.SendToWMS(InfoWMS.InfoCoilEntryOrDeliveryReq.Data(reqMsg));


            //// 告知L1發204
            //_log.I("操作通知出口段出料", $"通知一級刪除出口段位置{msg.PosStr}之鋼捲{msg.CoilID}");
            //MQPoolService.SendToL1(InfoL1.SndDelSkEntryID.Data(msg.Pos.ToString()));
        }

        // ReqTrackMap
        private void SndCurrentCoilMap(L1L2Rcv.Msg_303_ReqTrackMap msg)
        {
            var coilMap = _trkController.GetTrackMap();
            if(coilMap!=null)
                MQPoolService.SendToL1(InfoL1.SndTrackMap.Data(coilMap));
        }
        
        // 在鞍座上手動操作入料並通知Server
        private void PreSndPresetMsg(CS12_Coil_SkidFeed msg)
        {
            var coilID = msg.Coil_ID;

            var specificPreset = new SpecificPreset(coilID, msg.Skid);

            //// 從排程移出已入料鋼捲
            //var isDelOK = _coilController.DeleteCoilSchedule(coilNo);
            //_log.I("鋼捲入料完成", $"從棑程刪除鋼捲{coilNo} 是否成功=>{isDelOK}");

            // 紀錄鋼捲入料時間
            _coilController.UpdatePDIEntryTime(coilID, DateTime.Now);

            // 更新鋼捲狀態為已入料
            _coilController.UpdateScheduleStatuts(coilID, CoilDef.EntryCoilDone_Statuts);

            // 通知MMS 鋼捲上鞍座
            MQPoolService.SendToMMS(InfoMMS.CoilLoadedOnSk.Data(coilID));

            // 通知Setup 組201
            MQPoolService.SendToDtStp(InfoDataSetup.SpecificIDTo201.Data(specificPreset));
        }
       

        // HMI手動刪除畫面鞍作上鋼捲，通知L1 
        private void ProHMIDelCoilSkidNo(CS13_DeleteSidCoil msg)
        {
            var coilID = msg.Coil_ID.Trim();

            _log.I("HMI操作", $"刪除鞍作上鋼捲號");

            // 鋼捲狀態轉Idle
            _coilController.UpdateScheduleStatuts(coilID, CoilDef.NewCoil_Statuts);
            
            // 告知L1發204
            MQPoolService.SendToL1(InfoL1.SndDelSkEntryID.Data(msg.DelPos.ToString()));

            // 刪除暫存導帶資訊
            _coilController.DelectLeaderTemp(coilID);
        }

        // WMS 入料/出料/退料要求回復訊息
        private void WMSResReqMsgProcess(WMS_L2_Rcv.WPx3_RequestResponse msg)
        {

        
            var coilNo = msg.CoilNo.ToStr();

       
            if (msg.PosFlag.ToStr().Equals(CoilDef.EntryCoil)){

                   
                if (msg.ProcessFlag.ToStr().Equals(WMSSysDef.Cmd.WMSInfoNo))
                {
                    //MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPushShowDialog(EventDef.ReceiveWMSCancelMsg, $"{coilNo}無法入料原因:{msg.Reason.ToStr()}", DialogType.Error)));
                    MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush(EventDef.ReceiveWMSCancelMsg, $"{coilNo}無法入料原因:{msg.Reason.ToStr()}")));
                    return;
                }

                // 更新狀態旗標為要求入料 R  
                _coilController.UpdateScheduleStatuts(msg.CoilNo.ToStr(), CoilDef.RequestEntryCoil_Statuts);
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush(EventDef.ReceiveWMSCancelMsg, $"接收{coilNo}入料要求")));

                return;
            }
            
            // 無法出料通知HMI
            if (msg.PosFlag.ToStr().Equals(CoilDef.DeliveryCoil))
            {
                if (msg.ProcessFlag.ToStr().Equals(WMSSysDef.Cmd.WMSInfoNo))
                {
                    MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush(EventDef.ReceiveWMSCancelMsg, "無法出料")));
                    return;
                }
            }

            // 退料回應通知
            if (msg.PosFlag.ToStr().Equals(CoilDef.RejectCoil))
            {
                if (msg.ProcessFlag.ToStr().Equals(WMSSysDef.Cmd.WMSInfoNo))
                {
                    MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(new SC03_EventPush(EventDef.ReceiveWMSCancelMsg, "無法退料")));
                    return;
                }
            }


            var eventMsg = msg.ProcessFlag.Equals(WMSSysDef.Cmd.WMSInfoYes) ? $"WMS回復" : $"{msg.ActionResultStr}{coilNo}要求";
            var eventPush = new SC03_EventPush( EventDef.ReceiveWMSCancelMsg, eventMsg);
            MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));



        }

        // 前筆有鋼捲 後筆為空 CoilOut
        private bool IsCoilOut(string preCoil, string NowCoil)
        {          
            return (!preCoil.Equals(string.Empty) && NowCoil.Equals(string.Empty));
        }
        // 前筆為空 後筆有鋼捲 CoilIn
        private bool IsCoilIn(string preCoil, string NowCoil)
        {
            return (preCoil.Equals(string.Empty) && !NowCoil.Equals(string.Empty));
        }



    }
}
