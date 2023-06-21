using DataMod.Common;
using DBService.L1Repository;
using LogSender;
using MsgStruct;
using System;
using System.Collections.Generic;
using static Core.Define.L25SysDef;
using static DataMod.Response.RespnseModel;
using static DBService.Repository.LineStatus.ProcessDataEntity;
using static DBService.Repository.SideTrimmerTmp.SideTrimmerTmpEntity;
using static DBService.Repository.WieldRecord.WeldRecordEntity;
using static MsgStruct.L1L2Rcv;

namespace Controller.DtGtr
{
    public interface IDataGatheringController
    {

        void SetLog(ILog log);
        bool CreateCoilWeld(L1L2Rcv.Msg_302_CoilWeld msg, string oriOutCoilID);
        bool DeleteCoilWeld(string coilID);
        bool CreateProcessData(L1L2Rcv.Msg_313_SpdTen msg);
        bool DeleteProcessDataByRecTime(DateTime time);
        bool DeleteSiderTrimmerByRecTime(DateTime time);
        bool CreateUtility(L1L2Rcv.Msg_316_Utility_Data msg, string shift, string team);
        bool CreateL25Engc(L1L2Rcv.Msg_316_Utility_Data msg);
        bool CreateStopLineFaultStart(L1L2Rcv.Msg_310_LineFault msg, string team, int shift);
        bool UpdateStopLineFaultEnd(L1L2Rcv.Msg_310_LineFault msg);
        bool CreateL25DownTime(LineFaultRecord dao);
        LineFaultRecord GetLineFaultRecord(string prodTime, string stopStartTime);
        bool UpdateLineFaultUploadFlag(DateTime prodTime, DateTime stopStartTime, bool uploadDone);
        WorkSchedule GetScheduleByTime(DateTime time);
        TBL_WeldRecords GetWeldRecordByEnCoilId(string enCoilId);
        IEnumerable<TBL_ProcessData> QueryProcessDatas(DateTime starTime, DateTime endTime);
        IEnumerable<TBL_ProcessData> QueryProcessDatasWeld(DateTime starTime, DateTime endTime);
        IEnumerable<TBL_SideTrimmerTmp> QuerySiderTrimmerTmp(DateTime starTime, DateTime endTime);

        ProcessCTModel CalculateProcessData(IEnumerable<TBL_ProcessData> datas);
        ProcessCTWeldModel CalculateProcessDataWeld_L25(IEnumerable<TBL_ProcessData> datas, int passNo);
        ProcessCTWeldModel CalculateProcessDataWeld_Chart(IEnumerable<TBL_ProcessData> datas);
        SideTrimmerAvgModel CalculateAvgSiderTrimmer(IEnumerable<TBL_SideTrimmerTmp> values);
        bool Create25ProcessCTData(PDO pdo, ProcessCTModel data, L25CTData dataClssify);

        bool Create25ProcessCTDataWeld(PDO pdo, ProcessCTWeldModel data, L25CTData dataClssify);

        bool CreateProcessCTData(PDO pdo, ProcessCTModel datas, L25CTData dataClssify);

        bool CreateProcessCTDataWeld(PDO pdo, ProcessCTWeldModel datas, L25CTData dataClssify);

        bool CreateL25Alive();

        bool CreateSideTrimmer(Msg_318_SideTrimmerInfo msg);

        float CalculateTRAvgTension(IEnumerable<TBL_ProcessData> datas);

        int DeleteAllSiderTrimmer();

        bool ProcCross(DateTime now, string shift, string team);

        bool ChkCrossTime(ref DateTime crossTime, DateTime now);
    }
}
