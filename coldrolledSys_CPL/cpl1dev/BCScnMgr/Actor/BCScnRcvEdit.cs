using Akka.Actor;
using Akka.IO;
using AkkaSysBase;
using AkkaSysBase.Base;
using BCScnMgr.Service;
using Controller;
using Controller.Coil;
using Controller.Sys;
using Controller.Track;
using Core.Define;
using Core.Util;
using DataMod.BarCode.Msg;
using LogSender;
using MsgConvert.MsgFactory;
using MSMQ.Core.MSMQ;
using static DataMod.BarCode.BCSDataModel;
using static DataModel.HMIServerCom.Msg.SCCommMsg;

namespace BCScnMgr
{
    public class BCScnRcvEdit : BaseActor
    {
        //private IActorRef _selfActor;
        private IActorRef _sndEditActor;
        private ITrackingController _trkService;
        private ICoilController _coilService;
        private ISysController _sysController;
        private AggregateService _agService;

        public BCScnRcvEdit(ISysAkkaManager akkaManager, ICoilController coilService, ITrackingController trkService, ISysController sysController, AggregateService agService, ILog log) : base(log)
        {
            _sndEditActor = akkaManager.GetActor(nameof(BCScnSndEdit));
            _trkService = trkService;
            _coilService = coilService;
            _sysController = sysController;
            _agService = agService;
            _trkService.SetLog(log);
            _sysController.SetLog(log);
            _coilService.SetLog(log);

            //_sysController.SaveAPStatusToL25("SystemAP_11", "1");

            Receive<BarCodeScnContent>(message => TestUseMethod(message));
            Receive<byte[]>(message => ParsingByteData(message));
            ReceiveAny(message => RcvObject(message));
        }

        // 測試用
        private void TestUseMethod(BarCodeScnContent msg)
        {
            if (IsEntryPos(msg.GetScanPos().ToString()))
            {
                // 入口ID掃描
                //EntryScnPro(msg);
                return;
            }

            // 出口ID掃描
            //ExitScnPro(msg);
        }


        /// <summary>
        /// 解析Barcode掃描到的資料
        /// </summary>
        private void ParsingByteData(byte[] bytes)
        {
            var forwardSender = Sender;

            // 測試Debug用
            _agService.DumpDebugRawData(bytes);        

            // 抓取BarCode機訊號標頭
            var header = bytes.RawDeserialize(typeof(BCSModel.BCSHeader)) as BCSModel.BCSHeader;

            // 確定訊號標頭是否解析成功
            if (header == null)
            {
                _log.E("TCP接收資料", "解析BarCode機掃描資料標頭失敗");
                _agService.DumpRawData.DumpMsg(bytes, _agService.appSetting.FailMsgFilePath);
                return;
            }

            // 解析訊號
            var scanCoil = bytes.RawDeserialize(typeof(BCSModel.BCSScanCoil_BS01)) as BCSModel.BCSScanCoil_BS01;
            if (scanCoil == null)
            {
                _log.E("TCP接收資料", "解析BarCode機掃描資料失敗");
                _agService.DumpRawData.DumpMsg(bytes, _agService.appSetting.FailMsgFilePath);
                UseSenderSendMsg(forwardSender, new SC01_ScnBarcodeID(BCScanResult.Error, string.Empty, string.Empty, 0, false));
                return;
            }

            _agService.DumpRcvRawData(bytes);

            // 索取標頭訊號ID
            var msgID = header.Message_Id.ToStr();

            // 根據ID判斷事件
            if (msgID.Equals(DeviceParaDef.BCSScanCoil))
            {
                // 掃描位置
                var scnPos = scanCoil.CoilPos.ToStr();
                // 掃描剛卷
                var scnCoilNo = scanCoil.CoilNo.ToStr();

                _log.I("BarCode機掃描資料", $"CoilNo:{scnCoilNo} POS ID:{scnPos}");

                // 掃描內容
                var scanContent = GenScnContent(scnPos, scnCoilNo);

                if (IsEntryPos(scnPos))
                {
                    // 入口ID掃描
                    EntryScnPro(forwardSender, scanContent);
                    return;
                }

                // 出口ID掃描
                ExitScnPro(forwardSender, scanContent);
                return;
            }

        }


        private void EntryScnPro(IActorRef tmpSender, BarCodeScnContent msg)
        {          
            var scnCoilNo = msg.ScanCoilNo;
            var POS = msg.ScanPosition;
            var coilMap = _trkService.GetTrackMap();

           // 判斷掃描位置是否為空

            // 空(無鋼捲號) : 天車入料
            if (coilMap.IsPosEmpty(msg.ScanPosition))
            {
                // 通知WMS
                InfoWMSScanID(msg);

                // 通知BarCode機
                //var scnResult = new SC01_ScnBarcodeID(BCScanResult.Sucess, scnCoilNo, coilMap.GetCoilNoFromPOS(POS), msg.GetScanPos());
                //_sndEditActor.Tell(scnResult);
                InfoHMIEntryScnResult(tmpSender, BCScanResult.Sucess, scnCoilNo, coilMap.GetCoilNoFromPOS(POS), msg.GetScanPos(), $"掃描位置[{msg.GetScanPosStr()}]鋼捲ID:{scnCoilNo}成功");

                return;
            }


            if (!coilMap.CompareMap(scnCoilNo, POS))
            {
                // 與CoilMap鋼捲不一致, 更新PDI Entry_CoilID_Checked 狀態為0              
                //var updateOK = _coilService.UpdateEntryScanCoilInfo(scnCoilNo, false);

                _log.E("掃描入口捲號失敗", $"此位置[{msg.GetScanPosStr()}]無此鋼捲ID:{scnCoilNo}");
                //InfoHMIEntryScnResult(BCScanResult.Error, scnCoilNo,  coilMap.GetCoilNoFromPOS(POS), msg.GetScanPos(), $"此位置[{msg.GetScanPosStr()}]無此鋼捲ID:{scnCoilNo}");
                InfoHMIEntryScnResult(tmpSender, BCScanResult.Error, scnCoilNo, coilMap.GetCoilNoFromPOS(POS), msg.GetScanPos(), $"此位置[{msg.GetScanPosStr()}]無此鋼捲ID:{scnCoilNo}");
            }
            else
            {
                // 與CoilMap鋼捲一致, 更新PDI Entry_CoilID_Checked 狀態為1 
                var updateOK = _coilService.UpdatePDIEntryScanCoilInfo(scnCoilNo, true);                
                var scanCheckOk = updateOK ? BCScanResult.Sucess : BCScanResult.Error;
                _coilService.UpdateScheduleStatuts(scnCoilNo, CoilDef.IdentifyOK_Statuts);
                // TODO 鋼捲Check資料庫更新失敗通知Barcode機
                _log.I($"掃描入口捲號", $"掃描入口捲號是否成功 => {scanCheckOk}");
                //InfoHMIEntryScnResult(scanCheckOk, scnCoilNo, coilMap.GetCoilNoFromPOS(POS), msg.GetScanPos(), $"掃描位置[{msg.GetScanPosStr()}]鋼捲ID:{scnCoilNo}成功");
                InfoHMIEntryScnResult(tmpSender, scanCheckOk, scnCoilNo, coilMap.GetCoilNoFromPOS(POS), msg.GetScanPos(), $"掃描位置[{msg.GetScanPosStr()}]鋼捲ID:{scnCoilNo}成功");
            }
          
        }

        private void InfoWMSScanID(BarCodeScnContent msg)
        {
            _log.I("通知WMS掃描ID", $"掃描位置為{msg.GetScanPosStr()}鋼捲ID為{msg.ScanCoilNo}");
            var scanResult = new ScanResult(msg.GetScanPos(), msg.ScanCoilNo);
            MQPoolService.SendToWMS(InfoWMS.InfoBCSScanID.Data(scanResult));
        }

        private void ExitScnPro(IActorRef tmpSender, BarCodeScnContent msg)
        {
            var scnCoilNo = msg.ScanCoilNo;
            var POS = msg.ScanPosition;
            var coilMap = _trkService.GetTrackMap();

            if (!coilMap.CompareMap(scnCoilNo, POS))
            {
                // 與CoilMap鋼捲不一致, 更新PDO Exit_CoilID_Checked 狀態為0              
                var updateOK = _coilService.UpdatePDOExCoilIDChecked(scnCoilNo, EventDef.UnCheckedCoilNo);

                _log.E("掃描出口捲號失敗", $"此位置無此鋼捲ID:{scnCoilNo}");
                var eventPush = new SC03_EventPush("掃描失敗", $"此位置無此鋼捲ID:{scnCoilNo}");
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

                var scnResult = new SC01_ScnBarcodeID(BCScanResult.Error, scnCoilNo, coilMap.GetCoilNoFromPOS(POS), msg.GetScanPos());
                //_sndEditActor.Tell(scnResult);
                UseSenderSendMsg(tmpSender, scnResult);
            }
            else
            {
                // 與CoilMap鋼捲一致, 更新PDO Exit_CoilID_Checked 狀態為1 
                var updateOK = _coilService.UpdatePDOExCoilIDChecked(scnCoilNo, EventDef.CheckCoilNo);

                // 更新鋼捲狀態為已產出
                if(updateOK)
                    _coilService.UpdateScheduleStatuts(scnCoilNo, CoilDef.ProduceDone_Statuts);

                // TODO 鋼捲Check資料庫更新失敗通知Barcode機
                var eventMsg = updateOK ? "掃描成功" : "掃描失敗";
                var eventPush = new SC03_EventPush(eventMsg, $"{scnCoilNo}"+ eventMsg);
                var scanOK = updateOK ? BCScanResult.Sucess : BCScanResult.Error;
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));

                var scnResult = new SC01_ScnBarcodeID(scanOK, scnCoilNo, coilMap.GetCoilNoFromPOS(POS), msg.GetScanPos());
                //_sndEditActor.Tell(scnResult);
                UseSenderSendMsg(tmpSender, scnResult);
            }
        }       
        private void InfoHMIEntryScnResult(IActorRef forwardSender, BCScanResult scanResult, string coilNo, string coilNoOnMap, int pos, string eventMsg = "")
        {
            var scnResult = new SC01_ScnBarcodeID(scanResult, coilNo, coilNoOnMap, pos);
            MQPoolService.SendToPCCom(InfoHMI.BarcodeScanResult.Data(scnResult));

            // 錯誤 Event
            if (scanResult == BCScanResult.Error)
            {
                var eventPush = new SC03_EventPush( "掃描失敗", eventMsg);
                MQPoolService.SendToPCCom(InfoHMI.EventPush.Data(eventPush));
            }

            //_sndEditActor.Tell(scnResult);
            UseSenderSendMsg(forwardSender, scnResult);
        }

        // 隔離用  pos 1:ESK01 2:ESK02 3:ETOP 4:DSK01 5:DSK02 6:DTOP
        private BarCodeScnContent GenScnContent(string pos, string coilNo)
        {
            var scnContent = new BarCodeScnContent();
            scnContent.ScanCoilNo = coilNo;
            scnContent.SetScanPos(pos);
            return scnContent;
        }
        private bool IsEntryPos(string pos)
        {
            return pos.Equals(DeviceParaDef.BCSDefPOS_ESK01) || pos.Equals(DeviceParaDef.BCSDefPOS_ESK02) || pos.Equals(DeviceParaDef.BCSDefPOS_ETOP);
        }
        private void RcvObject(object msg)
        {
            _log.E("【AThread接收資料-RcvObject】", $"VaildActor 無法解析資料! Type:{msg.GetType()} From Sender:{Sender.Path}");
        }
        private void UseSenderSendMsg(IActorRef tmpSender, SC01_ScnBarcodeID scnResult)
        {
            var result = BCSFactory.ScanResult(scnResult.ScanResult == BCScanResult.Sucess, scnResult.CoilNoOnMap, scnResult.ParsingSuccess);
            tmpSender.Tell(Tcp.Write.Create(ByteString.FromBytes(result.RawSerialize())));
        }
    }
}
