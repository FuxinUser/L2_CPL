using DataMod.Common;
using DataMod.PLC;
using DBService.Repository;
using DBService.Repository.APStatus;
using DBService.Repository.LookupTbSleeve;
using LogSender;
using MsgStruct;
using System;
using System.Collections.Generic;
using static Core.Define.DBParaDef;
using static DataMod.Response.RespnseModel;
using static DBService.Repository.CoilScheduleEntity;
using static MsgStruct.L1L2Rcv;
using static MsgStruct.L2L1Snd;
using static MsgStruct.MMSL2Rcv;
/**
* Author: ICSC Spyua
* Date: 2020/1/1
* Desc: 鋼捲生產處理API介面
*/

namespace Controller.Coil
{
    public interface ICoilController
    {
        void SetLog(ILog log);

        #region -- PDI相關 --

        bool CreateL25PDI(string planNo, string coilID);
        bool CreateL25PDI(Msg_PDI msg);

        /// <summary>
        /// 是否有PDI資訊
        /// </summary>
        /// <param name="entryCoilID">入口鋼捲號</param>
        /// <returns></returns>
        bool VaildHasPDI(string planNo, string entryCoilID);       
        string GetPDIPlanNo(string entryCoilNo);
        /// <summary>
        /// 更新PDI資訊
        /// </summary>
        /// <param name="entryCoilID">入口鋼捲號</param>
        /// <param name="msg">三級PDI資訊</param>
        /// <returns>是否更新成功</returns>
        bool UpdatePDI(string planNo, string entryCoilID, Msg_PDI mmsPDI);

        /// <summary>
        /// 存取PDI資訊
        /// </summary>
        /// <param name="msg">三級PDI資訊</param>
        /// <returns>是否存取成功</returns>
        bool CreatePDI(Msg_PDI mmsPDI);
        bool UpdatePDIEntryScanCoilInfo(string entryCoilID, bool entryCoilIDChecked);
        bool UpdatePDIStarTime(string entryCoilID, DateTime nowTime);
        bool UpdatePDIFinishTime(string outCoilID, DateTime nowTime);
        bool UpdatePDIEntryTime(string entryCoilID, DateTime nowTime);
        PDI GetPDI(string coilID, PDISchema pdiSchema);
        PDI GetPDI(string planNo, string coilID, PDISchema pdiSchema);
        bool DeletePDIByEntryCoilID(string planNo, string coilID);
        void UpdateSampleFlag(bool isSample, string coilIDNo);

        #endregion

        #region-- PDO 相關 --

        bool CreateL25PDO(string planNo, string outCoilID);
        PDO DismountCoilGenPDO(Msg_307_CoilDismount msg, string entryCoilID);
        bool UpdatePDOExCoilIDChecked(string exitCoilNo, string exitCoilIDChecked);
        bool UpdateUploadPDOUserID(string planNo, string coilID, string uploadUserID);
        bool UpdateUploadPDOCheck(string planNo, string coilID, bool uploadFlag);
        PDO GetFinalPDO(string outCoilID);
        PDO GetPDO(string planNo, string outMatNo, string finishTime="");
        bool CreatePDO(PDO pdoData);
        void RecaulateWeightAndUpdatePDO(string outCoilID, float coilWt);
        bool UpdatePDOTRAvgTenstion(string outCoilID, float trAvgTension);
        bool UpdatePDOSiderTrimmerAvgValue(string outCoilID, SideTrimmerAvgModel value);
        bool CreatePdoUploadedReply(Msg_Res_For_PDO_Uploaded msg);

        #endregion

        #region -- 排程相關 -- 
        CoilSchedule GetScheduleInfo(string coilID);
        List<TBL_Production_Schedule> QueryTopCoilSchedule(int num, string scheduleStatuts);
        bool UpdateScheduleStatuts(string coilNo, string statuts);
        bool VaildHasSchedule(string coilID);
        List<string> QueryUnscheduleCoils(int num);
        IEnumerable<TBL_Production_Schedule> GetCollScheduleByPlanNo(string planNo);
        void DeleteAllSchedule();
        int CreateCoilSchedule(string coilID, short SeqN0);
        int GetCoilScheduleNo(string coilID);

        /// <summary>
        /// 刪除CoilID以下的鋼捲排程
        /// </summary>
        /// <param name="coilID"></param>
        /// <returns></returns>
        bool DeleteAppendScheduleByCoilID(string coilID);
        /// <summary>
        /// 刪除狀態為新鋼捲(N),與已發入料(R)狀態鋼捲
        /// </summary>
        /// <returns></returns>
        bool DeleteAllIdleSchedule();
        /// <summary>
        /// 將MMS下發鋼捲生產命令鋼捲順序存至DB
        /// </summary>
        bool BatchInsertSchedule(string coilCluster, int coilNum);
        bool InVaildScheduleMsgCoilNum(string coilCluster, int coilNum);
        bool DeleteCoilScheduleByCoilID(string coilID);

        bool CreateCoilScheduleDelTempRecord(string coilID, string recordType, string planNo,string operatorId="", string reasonCode="", string remarks = "");

        bool DeleteSchDelCoilRejectTempRecord(string coilID);

        bool CreateCoilScheduleDelRecords(string coilID, string operatorId = "", string reasonCode = "", string remarks = "");

        bool DeleteBatchScheduleByPlanNo(string planNo);
        bool DeleteDelScheduleRecord(string coilID);

        ScheduleDeleteCoilRejectTempInfo GetCoilScheduleDelTempRecord(string coilID);

        bool SequenceCreateSchedule(string coilCluster, int coilNum);
        #endregion

        #region -- 退料相關 --
        //L1 Coil Weight Scale 存取 排程跳軋/鋼捲退料暫存記錄資料
        bool CreateCoilWeightScaleInScheduleDeleteCoilRejectTemp(Msg_308_CoilWeightScale msg, string entryCoilID);

        // L1 Coil Dismount 存取 排程跳軋/鋼捲退料暫存記錄資料
        bool CreateCoilDismountInfoInScheduleDeleteCoilRejectTemp(Msg_307_CoilDismount msg, string entryCoilID);
        
        bool VaildHasScheduleDeleteCoilRejectTemp(string coilID);
        bool CreateStripBrekInScheduleDeleteCoilRejectTemp(string coilID, PDI pdi);
        bool CreateL1RetrunCoil(Msg_317_ReturnCoilInfo msg317);

        ReturnCoilInfo GetReturnCoilTemp(string coilID);

        bool CreateTempToCoilReject(ReturnCoilInfo returnInfo);

        bool DeleteCoilRejectByCoilID(string coilID);

        bool DelectLeaderTemp(string coilID);

        bool CreateL25CoilRejectResult(ReturnCoilInfo returnCoilInfo);

        #endregion

        #region -- LookUp Table --
        //GenCoilInfoModel.GenPreset201LkTableInfo GetPreset201LkTableData(string stNo, float coilThickness, int tsStandAvr);
        GenCoilInfoModel.GenPreset201LkTableInfo GetPreset201LkTableData(string stNo, float coilThickness);

        LkUpTableSleeveEntity.TBL_LookupTable_Sleeve GetSleeveData(string code);
        #endregion

        #region -- 鋼捲分切 --
        CoilCutRecordTemp GetCutRecordTempFromParentCoilID(string parentCoilID, string mode, CutTempSchema schema);
        bool CreatePORSplitCoilRecordTemp(string splitCoilID, string entryCoilID, string oriOutCoilID);
        int GetParentCnt(string parentCoilID);
        string GenSplitChildrenCoilID(string parentCoilID, int childrenCoilCnt = -1);
        bool CreateEnCoilCutRecordTemp(Msg_301_EnCoilCut msg);
        bool CreateExitCoilScrapCutRecordTemp(Msg_311_ExCoilCut msg);
        bool CreateExitCoilCutRecordTemp(Msg_311_ExCoilCut msg, string splitCoilID, string inCoilID, string oriOutCoilID);
        bool DeleteCoilCutRecordTemp(string coilID);
        CoilCutRecordTemp GetCutRecordTemp(string coilID);
        bool CreateUmontRecord(Msg_311_ExCoilCut msg, string coilID, string oriOutCoilID);
        bool DeleteUmountRecord(string coilID);
        #endregion

        #region -- 套筒墊紙同步處理 --

        bool SyncSleeveValue(MMSL2Rcv.Msg_Sleeve_Value_Synchronize msg);
        bool SyncPaperValue(MMSL2Rcv.Msg_Paper_Value_Synchronize msg);

        #endregion

        #region -- DefectData -- 

        /// <summary>
        /// 是否有Defect查詢
        /// </summary>
        /// <param name="planNo">計畫號</param>
        /// <param name="entryCoilID">入口鋼捲號</param>
        /// <returns></returns>
        bool VaildHasDefect(string planNo, string entryCoilID);

        /// <summary>
        /// 存取Defect資訊
        /// </summary>
        /// <param name="msg">三級PDI資訊</param>
        /// <returns></returns>
        bool CreateDefect(Msg_PDI msg);
        bool DeleteDefect(string planNo, string coilID);
        DefectData GetDefect(string planNO,string coilID);

        /// <summary>
        /// 更新Defect
        /// </summary>
        /// <param name="msg">三級PDI資訊</param>
        /// <returns>是否更新成功</returns>
        bool UpdateDefect(Msg_PDI msg);

        #endregion

        #region -- 取樣 --
        bool CreateSampleCoil(Msg_311_ExCoilCut msg, PDI pdi, string sampleCoilID);

        SampleCoil GetCoilSampleInfo(string planNo, int matSeqNo, string planSort, string sampleID);
        bool DeleteCoilSampleInfo(string planNo, int matSeqNo, string planSort, string sampleID);
        #endregion

        #region Preset Record
        bool CreatePresetRecord(Msg_201_Preset msg);

        bool CreateL25PresetRecord(Msg_201_Preset msg);
        #endregion

        CoilRejResultEntity.TBL_CoilRejectResult GetCoilRejectResult(string coilNo);
    }
}
