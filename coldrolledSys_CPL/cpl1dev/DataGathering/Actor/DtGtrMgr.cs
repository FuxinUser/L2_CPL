using Akka.Actor;
using MsgStruct;
using Controller.DtGtr;
using MSMQ;
using AkkaSysBase;
using AkkaSysBase.Base;
using DataMod.Common;
using System;
using Core.Util;
using Controller.Coil;
using Controller.Track;
using static Core.Define.DBParaDef;
using LogSender;
using Core.Help;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using Controller;
using MSMQ.Core.MSMQ;
using static MsgStruct.L1L2Rcv;
using Controller.Sys;
using DataModel.HMIServerCom.Msg;

namespace DataSetup.Actor
{
    public class DtGtrMgr : BaseActor
    {
        private IActorRef _selfActor;
        private IDataGatheringController _dtgtrController;
        private ICoilController _coilController;
        private ITrackingController _trkController;
        private ISysController _sysController;
        private ICancelable _tmrCheckCorssShift;        // 跨班確認

        public DtGtrMgr(ISysAkkaManager akkaManager,
                        IDataGatheringController dtgtrController,
                        ICoilController coilController,
                        ITrackingController trkController,
                        ISysController sysController,
                        ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);
            _dtgtrController = dtgtrController;
            _coilController = coilController;
            _trkController = trkController;
            _sysController = sysController;

            _dtgtrController.SetLog(log);
            _coilController.SetLog(log);
            _trkController.SetLog(log);
            _sysController.SetLog(log);

            //_sysController.SaveAPStatusToL25("SystemAP_6", "1");
            _tmrCheckCorssShift = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(0, 10000, Self, new ChkCross(), Self);

            MQPool.GetMQ("DtGtrMgr").Receive(x =>
            {
                var msg = (x as MQPool.MQMessage).Data;
                _selfActor.Tell(msg);
            });

            // 上傳停復機資訊
            Receive<CS09_LineFaultData>(message => TryFlow(() => UploadLineFault(message)));

            // 5.鋼捲生產追蹤(Line)作業  - 接收焊接訊號
            Receive<Msg_302_CoilWeld>(message => TryFlow(() => SaveCoilWeld(message)));
            // 13 產線設備運作狀態接收作業
            Receive<Msg_309_EquipMaint>(message => TryFlow(() => SaveEquipMaintInDB(message)));
            // 12.產線停復機作業
            Receive<Msg_310_LineFault>(message => TryFlow(() => LineFaultPro(message)));

            // 11.產線即時狀態接受
            Receive<Msg_313_SpdTen>(message => TryFlow(() => SaveSpdTenInDB(message)));
            // 12.產線能源消耗作業
            Receive<Msg_316_Utility_Data>(message => TryFlow(() => SaveUtility(message)));
            // ReturnCoil Umount訊息紀錄
            Receive<Msg_317_ReturnCoilInfo>(message => TryFlow(() => SaveUmountRecord(message)));

            Receive<Msg_318_SideTrimmerInfo>(message => TryFlow(() => SaveSideTrimmer(message)));

            Receive<CheckCrossShiftModel>(message => TryFlow(() => ProCrossShift(message)));
            Receive<ChkCross>(message => TryFlow(() => ProcCross()));

            ReceiveAny(RcvObject);
        }

        private void UploadLineFault(CS09_LineFaultData msg)
        {
            var lineFault = _dtgtrController.GetLineFaultRecord(msg.prod_time, msg.stop_start_time);

            if (lineFault != null)
            {
                MQPoolService.SendToMMS(InfoMMS.UploadLineFaultRecord.Data(lineFault));
                MQPoolService.SendToPCCom(InfoHMI.RefreshLineFault.Data(new SC07_RefreshLineDefault()));
                _dtgtrController.UpdateLineFaultUploadFlag(lineFault.prod_time, lineFault.stop_start_time, true);
                _dtgtrController.CreateL25DownTime(lineFault);
            }


        }

        private void SaveCoilWeld(Msg_302_CoilWeld msg)
        {
            var coilID = msg.CoilID.ToStr();
            _log.I("CoilWeld 302資料蒐集", $"存取{coilID}報文至CoilWeld資料庫 ");
            var pdi = _coilController.GetPDI(coilID, PDISchema.EntryCoilID);
            _dtgtrController.CreateCoilWeld(msg, pdi.Out_Coil_ID);
        }

        private void SaveSpdTenInDB(Msg_313_SpdTen msg)
        {
            _log.I("SpdTen 313資料蒐集", "存取313報文至ProcessData資料庫");
            _dtgtrController.CreateProcessData(msg);
        }

        private void SaveEquipMaintInDB(Msg_309_EquipMaint msg)
        {
            _log.I("EquipMaint 309資料蒐集", "更新309報文至LineStatus資料庫");
            _trkController.UpdateEqupMaint(msg);
        }

        private void LineFaultPro(Msg_310_LineFault msg)
        {

            // 關閉CrossShift檢查
            //_tmrCheckCorssShift?.Cancel();

            // 停機
            if (IsStarTime(msg.StartTime))
            {
                var workSchedule = _dtgtrController.GetScheduleByTime(DateTime.Now);

                if (workSchedule == null)
                {
                    //_log.A($"無班次股別可撈取", $"日期{DateTime.Now}無對應班次股別可撈取 ");
                    //_log.E($"撈取目前班次股別", $"【班次】: Error 【班次】: Error");
                    _dtgtrController.CreateStopLineFaultStart(msg, string.Empty, 0);
                }
                else
                {
                    //_log.I($"撈取目前班次股別", $"【班次】: {workSchedule.Shift} 【班次】:{workSchedule.Team}");
                    _dtgtrController.CreateStopLineFaultStart(msg, workSchedule.Team, workSchedule.Shift);
                }



                // 開啟跨班Timer檢查
                //var shiftInfo = new CheckCrossShiftModel
                //{
                //    FaultCode = msg.FaultCode,
                //    Shift = nowShift,
                //    StopStartTime = msg.DateTime,
                //};

                //SetCrossShiftCheckTmr(shiftInfo);

                return;
            }

            // 復機
            _dtgtrController.UpdateStopLineFaultEnd(msg);


        }

        private bool IsStarTime(byte[] dateTime)
        {
            return !dateTime.ToStr().Equals(string.Empty);
        }

        private void SaveUtility(Msg_316_Utility_Data msg)
        {
            _log.I("Utility_Data 316資料蒐集", "新增316報文至Utility資料庫");

            _dtgtrController.CreateL25Engc(msg);

            var workSchedule = _dtgtrController.GetScheduleByTime(DateTime.Now);

            if (workSchedule == null)
            {
                _log.A($"無班次股別可撈取", $"日期{DateTime.Now}無對應班次股別可撈取 ");
                _log.E($"撈取目前班次股別", $"【班次】: Error 【班次】: Error");

                _dtgtrController.CreateUtility(msg, string.Empty, string.Empty);
                return;
            }

            _dtgtrController.CreateUtility(msg, workSchedule.Shift.ToString(), workSchedule.Team);
        }


        private void SaveUmountRecord(Msg_317_ReturnCoilInfo msg)
        {
            _log.I("資料蒐集", $"存取斷帶資料至Umount資料庫 ");
            _coilController.CreateL1RetrunCoil(msg);
        }

        private void SaveSideTrimmer(Msg_318_SideTrimmerInfo msg)
        {
            _log.I("資料蒐集", $"存取圓盤剪訊息 ");
            _dtgtrController.CreateSideTrimmer(msg);
        }

        private void RcvObject(object message)
        {

            _log.E("接收資料", $"Received an unhandled message!!! type:{message.GetType()} from Sender:{Sender.Path}");
        }


        private void ProCrossShift(CheckCrossShiftModel checkCrossShift)
        {

            var nowShift = ShiftHelp.NowShift();

            //// 發生跨班
            //if (checkCrossShift.Shift != nowShift)
            //{
            //    // 結算
            //    var upOk = _dtgtrService.UpdateStopLineFaultEnd(checkCrossShift);

            //    // 重新新增一筆
            //    if (upOk)
            //        _dtgtrService.SaveStopLineFaultStart(checkCrossShift, nowShift, DateTime.Now.Date.ToString("yyyyMMdd"));

            //};

        }

        private void SetCrossShiftCheckTmr(CheckCrossShiftModel checkCrossShift)
        {
            _tmrCheckCorssShift?.Cancel();
            var interval = TimeSpan.FromSeconds(1);
            var initDelay = TimeSpan.FromSeconds(1);
            _tmrCheckCorssShift = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(initDelay, interval, Self, checkCrossShift, Self);
        }

        private DateTime _crossLineFaultTime = default;

        private void ProcCross()
        {
            var now = DateTime.Now;
            
            if (_dtgtrController.ChkCrossTime(ref _crossLineFaultTime, now))
            {
                var shift = string.Empty;
                var team = string.Empty;
                var workSchedule = _dtgtrController.GetScheduleByTime(DateTime.Now);

                if (workSchedule != null)
                {
                    shift = workSchedule.Shift.ToString();
                    team = workSchedule.Team.ToString();
                }

                _dtgtrController.ProcCross(now, shift, team);
            }
        }

        public class ChkCross { }
    }
}
