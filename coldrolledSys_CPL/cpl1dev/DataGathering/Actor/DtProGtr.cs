using Akka.Actor;
using AkkaSysBase;
using AkkaSysBase.Base;
using Controller.Coil;
using Controller.DtGtr;
using Controller.Sys;
using Core.Define;
using DBService;
using LogSender;
using MSMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using static Core.Define.L25SysDef;
using static DataMod.Response.RespnseModel;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using static DBService.Repository.LineStatus.ProcessDataEntity;
using static DBService.Repository.SystemSetting.SystemParameterEntity;

namespace DataGathering.Actor
{
    /// <summary>
    /// For 處理Process 生產資料
    /// </summary>
    public class DtProGtr : BaseActor
    {
        private IActorRef _selfActor;
        private IDataGatheringController _dtgtrController;
        private ICoilController _coilController;
        private ISysController _sysController;

        private ICancelable _tmrl25Alive;

        public DtProGtr(ISysAkkaManager akkaManager, 
                        IDataGatheringController dtgtrController,
                        ICoilController coilController,
                        ISysController sysController,
                        ILog log) : base(log)
        {
            _selfActor = akkaManager.GetActor(GetType().Name);

            _dtgtrController = dtgtrController;
            _coilController = coilController;
            _sysController = sysController;

            _dtgtrController.SetLog(log);
            _coilController.SetLog(log);
            _sysController.SetLog(log);

            StartTmr(1, EventDef.CMDSET.L25Alive, _tmrl25Alive);

            MQPool.GetMQ(nameof(DtProGtr)).Receive(x =>
            {
                var msg = (x as MQPool.MQMessage).Data;
                _selfActor.Tell(msg);
            });


            // 上傳PDO給MMS-紀錄生產資訊
            Receive<string>(message => TryFlow(()=> ProCmd(message)));
            Receive<CS06_SendMMSPDO>(message => TryFlow(() => CreateProDataToL25(message)));
            Receive<EventDef.CMDSET>(msg => TryFlow(() => ProEventCmd(msg)));
            
        }



        private void CreateProDataToL25(CS06_SendMMSPDO msg)
        {
            _log.I("新增2.5生產資料", $"新增{msg.Coil_ID}資料至L25");

            var pdo = _coilController.GetPDO(msg.Plan_No, msg.Coil_ID, msg.FinishTime);
            var processData = _dtgtrController.QueryProcessDatas(pdo.StartTime, pdo.FinishTime);
            var data = _dtgtrController.CalculateProcessData(processData);

            _dtgtrController.Create25ProcessCTData(pdo, data, L25CTData.Speed);
            _dtgtrController.Create25ProcessCTData(pdo, data, L25CTData.UNCTensionActCT);
            _dtgtrController.Create25ProcessCTData(pdo, data, L25CTData.UNCCurrentCT);
            _dtgtrController.Create25ProcessCTData(pdo, data, L25CTData.UNCTensionRefCT);
            _dtgtrController.Create25ProcessCTData(pdo, data, L25CTData.RECTensionActCT);
            _dtgtrController.Create25ProcessCTData(pdo, data, L25CTData.RECCurrentCT);
            _dtgtrController.Create25ProcessCTData(pdo, data, L25CTData.RECTensionRefCT);

            _dtgtrController.CreateProcessCTData(pdo, data, L25CTData.Speed);
            _dtgtrController.CreateProcessCTData(pdo, data, L25CTData.UNCTensionActCT);
            _dtgtrController.CreateProcessCTData(pdo, data, L25CTData.UNCCurrentCT);
            _dtgtrController.CreateProcessCTData(pdo, data, L25CTData.UNCTensionRefCT);
            _dtgtrController.CreateProcessCTData(pdo, data, L25CTData.RECTensionActCT);
            _dtgtrController.CreateProcessCTData(pdo, data, L25CTData.RECCurrentCT);
            _dtgtrController.CreateProcessCTData(pdo, data, L25CTData.RECTensionRefCT);

            //  處理 weld
            CreateProDataWeld(msg);
        }

        private void CreateProDataWeld(CS06_SendMMSPDO msg)
        {
            _log.I("新增2.5生產資料(Weld)", $"新增{msg.Coil_ID}資料");

            var pdo = _coilController.GetPDO(msg.Plan_No, msg.Coil_ID, msg.FinishTime);
            var weld = _dtgtrController.GetWeldRecordByEnCoilId(pdo.In_Coil_ID);
            var sTime = pdo.StartTime;

            //  取得鋼捲上開捲機的時間
            var inCoilId = pdo.In_Coil_ID;
            var sysParam = GetSystemParameter(DBColumnDef.LoadCoilTime, inCoilId);
            //  若結果為空則再找前一顆紀錄
            if (sysParam == null)
                sysParam = GetSystemParameter(DBColumnDef.LoadCoilTimePre, inCoilId);
            //  若結果不為空就以上開捲機的時間為起始時間
            if (sysParam != null)
                sTime = sysParam.ValueDate;

            var processData = _dtgtrController.QueryProcessDatasWeld(sTime, weld.ReceiveTime);
            CreateProDataWeld_L25(processData, pdo);
            CreateProDataWeld_Chart(processData, pdo);
        }

        private void CreateProDataWeld_L25(IEnumerable<TBL_ProcessData> processData, PDO pdo)
        {
            _log.I("新增2.5生產資料(Weld)", $"新增{pdo.Out_Coil_ID}資料至L25");

            if (processData == null)
                return;

            var list = processData.ToList();
            var cnt = list.Count;
            var idx = 0;
            var passNo = 0;
            
            while (idx < cnt)
            {
                //  以 WELD_Speed_Actual 為準, 處理大於 0 的
                if (list[idx].WELD_Speed_Actual > 0)
                {
                    //  從大於 0 的數據開始到小於等於 0 的數據算一次
                    var calculateDatas = new List<TBL_ProcessData>();
                    while (idx < cnt)
                    {
                        calculateDatas.Add(list[idx]);
                        idx++;

                        if (idx >= cnt || list[idx].WELD_Speed_Actual <= 0)
                        {
                            idx++;
                            break;
                        }
                    }

                    //  計算給 L2.5 的數據
                    if (calculateDatas.Count > 0)
                    {
                        passNo++;

                        var data = _dtgtrController.CalculateProcessDataWeld_L25(calculateDatas, passNo);

                        _dtgtrController.Create25ProcessCTDataWeld(pdo, data, L25CTData.WELD_Current_Actual_Front);
                        _dtgtrController.Create25ProcessCTDataWeld(pdo, data, L25CTData.WELD_Current_Actual_Rear);
                        _dtgtrController.Create25ProcessCTDataWeld(pdo, data, L25CTData.WELD_Speed_Actual);
                        _dtgtrController.Create25ProcessCTDataWeld(pdo, data, L25CTData.WELD_PlanishWheelForce_Actual);
                        _dtgtrController.Create25ProcessCTDataWeld(pdo, data, L25CTData.WELD_Temperture);
                    }
                }
                else
                {
                    idx++;
                }
            }
        }

        private void CreateProDataWeld_Chart(IEnumerable<TBL_ProcessData> processData, PDO pdo)
        {
            _log.I("新增2.5生產資料(Weld)", $"新增{pdo.Out_Coil_ID}資料至Chart");

            var data = _dtgtrController.CalculateProcessDataWeld_Chart(processData);

            _dtgtrController.CreateProcessCTDataWeld(pdo, data, L25CTData.WELD_Current_Actual_Front);
            _dtgtrController.CreateProcessCTDataWeld(pdo, data, L25CTData.WELD_Current_Actual_Rear);
            _dtgtrController.CreateProcessCTDataWeld(pdo, data, L25CTData.WELD_Speed_Actual);
            _dtgtrController.CreateProcessCTDataWeld(pdo, data, L25CTData.WELD_PlanishWheelForce_Actual);
            _dtgtrController.CreateProcessCTDataWeld(pdo, data, L25CTData.WELD_Temperture);
        }

        private TBL_SystemParameter GetSystemParameter(string name, string inCoilId)
        {
            var sysParam = _sysController.GetSystemParameter(DBColumnDef.LoadCoilTime);

            if (sysParam == null)
            {
                _log.E("新增2.5生產資料(Weld)失敗", $"撈取SystemParameter為空,name={name}");
                return null;
            }

            var sysCoilId = sysParam.Value.Trim();
            if (sysCoilId != inCoilId)
            {
                _log.E("新增2.5生產資料(Weld)失敗", $"當前處理捲號({inCoilId})與紀錄捲號{sysCoilId}不符,SysKeyName={name}");
                return null;
            }

            return sysParam;
        }

        // Cmd Def : MQCmdStr.DtProGtrCmd
        private void ProCmd(string cmd)
        {

            if (cmd.Equals(MQCmdStr.DtProGtrCmd.DeleteAllSiderTrimmer))
            {
                _dtgtrController.DeleteAllSiderTrimmer();
                return;
            }

            // 計算平均收卷張力
            var outCoilID = cmd;
            var pdo = _coilController.GetFinalPDO(outCoilID);

            //var processData = _dtgtrController.QueryProcessDatas(pdo.StartTime, pdo.FinishTime);
            //var trAvgTension = _dtgtrController.CalculateTRAvgTension(processData);
            //  從 LUT 取得張力平均值
            if (!GetRecoilerActtenAvg(out var trAvgTension, pdo))
                _log.E("未從LUT取得張力平均值", $"值={trAvgTension},鋼捲號={outCoilID}");

            _coilController.UpdatePDOTRAvgTenstion(pdo.Out_Coil_ID, trAvgTension);

            // 計算圓盤卷平均值
            if (L2SystemDef.SystemIDCode.Equals(L2SystemDef.CPL1))
            {
                var siderTrimmerValues = _dtgtrController.QuerySiderTrimmerTmp(pdo.StartTime, pdo.FinishTime);
                var avgSiderTrimmerValue = _dtgtrController.CalculateAvgSiderTrimmer(siderTrimmerValues);
                _coilController.UpdatePDOSiderTrimmerAvgValue(pdo.Out_Coil_ID, avgSiderTrimmerValue);
            }
          

        }

        private bool GetRecoilerActtenAvg(out float recoilerActtenAvg, PDO tblPdo)
        {
            recoilerActtenAvg = 0f;

            var tbiPdi = _coilController.GetPDI(tblPdo.Plan_No, tblPdo.In_Coil_ID, DBParaDef.PDISchema.EntryCoilID);
            if (tbiPdi == null)
            {
                _log.E("撈取PDI資料失敗", $"無計畫號({tblPdo.Plan_No})入口捲號({tblPdo.In_Coil_ID})的PDO");
                return false;
            }
            _log.I("撈取PDI資料成功", $"撈取計畫號({tblPdo.Plan_No})入口捲號({tblPdo.In_Coil_ID})的PDO成功");

            var lutTbInfo = _coilController.GetPreset201LkTableData(tbiPdi.St_No, tbiPdi.Entry_Coil_Thick);
            if (lutTbInfo == null)
            {
                _log.E("撈取LUT資料失敗", $"無St_No({tbiPdi.St_No})Entry_Coil_Thick({tbiPdi.Entry_Coil_Thick})的資料");
                return false;
            }
            _log.I("撈取LUT資料成功", $"撈取St_No({tbiPdi.St_No})Entry_Coil_Thick({tbiPdi.Entry_Coil_Thick})的資料成功");

            //  KN 總張力=单位张力*入料鋼捲厚度*入料鋼捲寬度
            recoilerActtenAvg = (lutTbInfo.RecoilerTension * tbiPdi.Entry_Coil_Thick * tbiPdi.Entry_Coil_Width) / 1000;

            return true;
        }

        // Scan觸發Pro
        private void ProEventCmd(EventDef.CMDSET msg)
        {

            switch (msg)
            {
                case EventDef.CMDSET.L25Alive:
                    _dtgtrController.CreateL25Alive();
                    break;

            }
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

    }
}
