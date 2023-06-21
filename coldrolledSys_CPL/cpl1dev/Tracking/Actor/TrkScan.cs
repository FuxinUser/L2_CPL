using Akka.Actor;
using AkkaSysBase.Base;
using Controller;
using Controller.Coil;
using Controller.Sys;
using Controller.Track;
using Core.Define;
using DataMod.WMS.LogicModel;
using DBService;
using LogSender;
using MSMQ.Core.MSMQ;
using System;
using static Core.Define.DBParaDef;

namespace Tracking.Actor
{
    public class TrkScan : BaseActor
    {
        private ITrackingController _trkController;
        private ICoilController _coilController;
        private ISysController _sysController;

        private ICancelable _tmrTrkScan;
        private ICancelable _tmrTrkInfo;
        public TrkScan(ITrackingController trkController, ICoilController coilController, ISysController sysController, ILog log) : base(log)
        {
            _trkController = trkController;
            _coilController = coilController;
            _sysController = sysController;

            _coilController.SetLog(log);
            _trkController.SetLog(log);
            _sysController.SetLog(log);

            StartTmr(5, EventDef.CMDSET.TRKSCAN, _tmrTrkScan);
            StartTmr(4, EventDef.CMDSET.TRKINFO, _tmrTrkInfo);

            Receive<EventDef.CMDSET>(msg => TryFlow(() => ProEventCmd(msg)));
        }

        /// <summary>
        /// 開始Scan
        /// </summary>
        private void StartTmr(int second, object message, ICancelable timer)
        {
            timer?.Cancel();

            var interval = TimeSpan.FromSeconds(second);

            timer = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(interval, interval, Self, message, Self);
        }

        // Scan觸發Pro
        private void ProEventCmd(EventDef.CMDSET msg)
        {

            switch (msg)
            {
                case EventDef.CMDSET.TRKSCAN:
                    AutoEntryCoil();
                    break;
                case EventDef.CMDSET.TRKINFO:
                    InfoTrkMap();
                    break;
            }       
        }

        private void AutoEntryCoil()
        {

            var isAutoMode = _sysController.VaildSystemAutoValueOn(DBColumnDef.SysParaGroup, 
                                                             DBColumnDef.SysParaAutoInputFlag, 
                                                             "是否為自動進料");
            if (!isAutoMode)
                return;

            // 索取TrkMap
            var coilMap = _trkController.GetTrackMap();
            if (coilMap == null)
            {
                _log.E("抓取Trck失敗", "Track資料為空");
                return;
            }
            var topCoil = coilMap.Entry_TOP.Trim();

            // 判斷Top是否為空 &&　第一筆鋼捲狀態非為Ｒ
            if (string.IsNullOrEmpty(topCoil) && _coilController.QueryTopCoilSchedule(1, CoilDef.RequestEntryCoil_Statuts).Count==0)
            {

                // Top為空索取第一筆鋼捲
                var schedules = _coilController.QueryTopCoilSchedule(1, CoilDef.NewCoil_Statuts);
                if (schedules.Count == 0)
                {
                    _log.D("目前無鋼捲生產排程", "目前無鋼捲生產排程");
                    return;
                }
                var scheduleInfo = schedules[0];

                // 比對狀態是否不為新捲
                if (!scheduleInfo.Schedule_Status.Equals(CoilDef.NewCoil_Statuts))
                    return;

                var pdi = _coilController.GetPDI(scheduleInfo.Coil_ID, PDISchema.EntryCoilID);
            
                // 通知WMS
                _log.I("TOP點為空", $"通知WMS{scheduleInfo.Coil_ID}入料要求");
                MQPoolService.SendToWMS(InfoWMS.InfoCoilEntryOrDeliveryReq.Data(new ProdLineCoilReq(CoilDef.ReqWMSEntryCoil, scheduleInfo.Coil_ID, WMSSysDef.SkPos.ETop.ToString(), direction:pdi.Uncoiler_Direction)));
            };
        }

        private void InfoTrkMap()
        {
            // 索取TrkMap
            var coilMap = _trkController.GetTrackMap();
            if (coilMap == null)
            {
                _log.E("抓取Trck失敗", "Track資料為空");
                return;
            }

            MQPoolService.SendToWMS(InfoWMS.InfoTrackMap.Data(coilMap));

            // 存至2.5
            _trkController.Create25CoilMap(coilMap);
        }

    }
}
