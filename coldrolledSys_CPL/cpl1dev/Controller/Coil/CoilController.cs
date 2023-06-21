using AutoMapper;
using BLL.Coil;
using BLL.Logic;
using Core.Define;
using Core.Util;
using DataMod.PLC;
using DBService;
using LogSender;
using MsgConvert.ObjectMapping;
using MsgConvert.EntityFactory;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Core.Define.DBParaDef;
using static DataMod.Response.RespnseModel;
using static DBService.Repository.CoilRejResultEntity;
using static DBService.Repository.CoilScheduleEntity;
using static DBService.Repository.CutReocrd.CoilCutRecordTempEntity;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using static DBService.Repository.PDI.CoilPDIEntity;
using static DBService.Repository.PDO.PDOEntity;
using static DBService.Repository.Sample.SampleEntity;
using static DBService.Repository.ScheduleDelete_CoilReject_Record_Temp.ScheduleDeleteRecordTempEntity;
using static DBService.Repository.UnmountRecord.UnmountRecordEntity;
using static DBService.Repository.WieldRecord.WeldRecordEntity;
using static MsgStruct.L1L2Rcv;
using static MsgStruct.MMSL2Rcv;
using static MsgStruct.L2L1Snd;
using DataMod.Common;
using DBService.Repository.APStatus;
using DBService.Level25Repository.L2L25_L2APStatus;
using DBService.Level25Repository.L2L25_CoilRejectResult;
using static DBService.Repository.LookupTbSleeve.LkUpTableSleeveEntity;
using static DBService.Repository.PDO.PDOUploadedReplyEntity;

/**
 * Author: ICSC Spyua
 * Date : 2019/12/31
 * Desc : Coil API (Controller)
 **/

namespace Controller.Coil
{
    public class CoilController : ICoilController
    {
        private DataGartingLogic _dataGartingLogic;
        private CoilProLogic _coilLogic;
        private IMapper _mapper;
        private ILog _log;


        public CoilController()
        {
            _coilLogic = new CoilProLogic();
            _dataGartingLogic = new DataGartingLogic();

            var resMapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DBMappingResModel());
                cfg.AddProfile(new MsgMappingDBModel());
            });

            _mapper = resMapConfig.CreateMapper();

        }

        public void SetLog(ILog log)
        {
            _log = log;
        }


        #region -- PDI相關 --

        public bool CreateL25PDI(Msg_PDI msg)
        {
            msg.VaildObjectNull("msg", "存取L25PDI錯誤");
            try
            {
                var tb = msg.ToL25PDIEntity();
                var insertNum = _coilLogic.CreateL25PDI(tb);
                var insertOK = insertNum > 0;
                _log.I($"新增鋼捲PDI至L25:{msg.EntryCoilNo}", $"【鋼捲】:{msg.EntryCoilNo.Trim()}新增PDI{(insertOK).ToStr()}");

                insertNum = _coilLogic.CreateL25PDIHis(tb);
                insertOK = insertNum > 0;
                _log.I($"新增鋼捲PDI至L25歷史資料庫:{msg.EntryCoilNo}", $"【鋼捲】:{msg.EntryCoilNo.Trim()}新增PDI{(insertOK).ToStr()}");

                return insertOK;
            }
            catch (Exception e)
            {
                _log.E($"新增鋼捲PDI至L25錯誤:{msg.EntryCoilNo}", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool CreateL25PDI(string planNo, string coilID)
        {
            coilID.VaildObjectNull("coilID", "存取L25PDI錯誤");

            try
            {
                var pdi = _coilLogic.GetPDI(planNo, coilID, PDISchema.EntryCoilID);
                var defect = _coilLogic.GetDefect(planNo, coilID);
                var tb = pdi.ToL25PDIEntity(defect);

                var insertNum = _coilLogic.CreateL25PDI(tb);
                var insertOK = insertNum > 0;
                _log.I($"修改鋼捲PDI新增資料至L25:{coilID}", $"【鋼捲】:{coilID}新增PDI{(insertOK).ToStr()}");

                insertNum = _coilLogic.CreateL25PDIHis(tb);
                insertOK = insertNum > 0;
                _log.I($"新增鋼捲PDI至L25歷史資料庫:{coilID}", $"【鋼捲】:{coilID}新增PDI{(insertOK).ToStr()}");

                return insertOK;

            }
            catch (Exception e)
            {
                throw new Exception($"修改鋼捲PDI新增資料至L25錯誤:{coilID}" + e.ToString().CleanInvalidChar());
            }

        }

        public bool VaildHasPDI(string planNo, string entryCoilID)
        {
            entryCoilID.VaildStrNullOrEmpty("entryCoilID", "是否有PDI查詢錯誤");

            try
            {
                var hasPDI = _coilLogic.VaildHasPDI(planNo, entryCoilID);
                _log.I("是否有PDI查詢成功", $"【鋼卷】: {entryCoilID}{hasPDI.ToHasStr()}PDI");
                return hasPDI;
            }
            catch (Exception e)
            {
                _log.E($"是否有PDI查詢錯誤，【鋼卷】{entryCoilID}", e.Message.CleanInvalidChar());
                return false;

            }
        }

        public bool UpdatePDI(string planNo, string entryCoilID, Msg_PDI msg)
        {

            entryCoilID.VaildStrNullOrEmpty("entryCoilID", "更新鋼捲PDI錯誤");
            msg.VaildObjectNull("msg", "更新鋼捲PDI錯誤");

            try
            {
                //var tblPDI = TblPDIFactory.TblCoilPDI(mmsPDI);
                var tb = _mapper.Map<TBL_PDI>(msg);
                tb.Is_Delete = "0";
                var updateNum = _coilLogic.UpdatePDI(planNo, entryCoilID, tb);
                var updateOK = updateNum > 0;
                _log.I($"更新鋼捲PDI成功:{msg.EntryCoilNo} => {updateOK}", $"【鋼捲】:{entryCoilID} 更新PDI{(updateOK).ToStr()}");
                return updateOK;
            }
            catch (Exception e)
            {
                _log.E($"更新鋼捲PDI錯誤:{msg.EntryCoilNo}", e.ToString().CleanInvalidChar());
                return false;
            }

        }


        public bool CreatePDI(Msg_PDI msg)
        {

            msg.VaildObjectNull("msg", "新增鋼捲PDI錯誤");

            try
            {
                var tb = _mapper.Map<TBL_PDI>(msg);
                var insertNum = _coilLogic.CreatePDI(tb);
                var insertOK = insertNum > 0;
                _log.I($"新增鋼捲PDI:{msg.EntryCoilNo}=>{insertOK}", $"【鋼捲】:{msg.EntryCoilNo.Trim()}新增PDI{(insertOK).ToStr()}");
                return insertOK;
            }
            catch (Exception e)
            {
                _log.E($"新增鋼捲PDI錯誤:{msg.EntryCoilNo}", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool DeletePDIByEntryCoilID(string planNo, string coilID)
        {
            coilID.VaildStrNullOrEmpty("coilID", "刪除鋼捲PDI失敗");

            try
            {
                var deleteNum = _coilLogic.DeletePDIByEntryCoilID(planNo, coilID);
                var deleteOK = deleteNum > 0;
                _log.I("刪除鋼捲PDI", $"刪除鋼捲號{coilID}PDI資訊");
                return deleteOK;
            }
            catch (Exception e)
            {
                throw new Exception("刪除鋼捲PDI" + e.Message.CleanInvalidChar());
            }
        }

        public string GetPDIPlanNo(string entryCoilNo)
        {

            var planNo = string.Empty;

            try
            {
                planNo = _coilLogic.GetPDIPlanNoByEnCoilID(entryCoilNo);
                _log.I("撈取PDI的PlanNo", $"撈取{entryCoilNo}PDI Plan No {planNo != null}");
                return planNo != null ? planNo : "";
            }
            catch (SqlException e)
            {
                _log.E("撈取PDI的PlanNo失敗", $"撈取{entryCoilNo}PDI Plan No失敗");
                _log.E("撈取PDI的PlanNo失敗", e.Message.CleanInvalidChar());
                return "";
            }
        }

        /// <summary>
        /// 更新鋼捲PDI開始時間
        /// </summary>
        /// <param name="entryCoilID">鋼捲號</param>
        /// <returns></returns>
        public bool UpdatePDIStarTime(string entryCoilID, DateTime nowTime)
        {

            entryCoilID.VaildStrNullOrEmpty("coilID", "更新鋼捲PDI開始時間失敗");

            try
            {
                var nowTimeStr = nowTime.ToString(DBDateTimeFromat);
                var updateNum = _coilLogic.UpdatePDIStarTime(entryCoilID, nowTimeStr);
                var updateOK = updateNum > 0;
                _log.I("更新鋼捲PDI開始時間成功", $"鋼捲{entryCoilID}紀錄生產時間:{updateOK}");
                return updateOK;

            }
            catch (Exception e)
            {
                throw new Exception("更新鋼捲PDI開始時間失敗" + e.ToString().CleanInvalidChar());
            };
        }

        /// <summary>
        /// 更新鋼捲PDI結束時間
        /// </summary>
        /// <param name="outCoilID">出口鋼捲號</param>
        public bool UpdatePDIFinishTime(string outCoilID, DateTime nowTime)
        {
            outCoilID.VaildStrNullOrEmpty("outCoilID", "更新鋼捲PDI結束時間失敗");

            try
            {
                var nowTimeStr = nowTime.ToString(DBDateTimeFromat);
                var updateNum = _coilLogic.UpdatePDIFinishTime(outCoilID, nowTimeStr);
                var updateOK = updateNum > 0;
                _log.I("更新鋼捲PDI結束時間成功", $"鋼捲{outCoilID}紀錄生產結束時間:{updateOK}");
                return updateOK;
            }
            catch (Exception e)
            {
                _log.E("更新鋼捲PDI結束時間失敗", e.ToString().CleanInvalidChar());
                return false;
            };
        }



        /// <summary>
        /// 更新鋼捲入料時間
        /// </summary>
        /// <param name="entryCoilID">鋼捲號</param>
        /// <returns></returns>
        public bool UpdatePDIEntryTime(string entryCoilID, DateTime nowTime)
        {
            entryCoilID.VaildStrNullOrEmpty("coilID", "更新鋼捲PDI入料時間失敗");

            try
            {
                var nowTimeStr = nowTime.ToString(DBDateTimeFromat);
                var updateNum = _coilLogic.UpdatePDIEntryTime(entryCoilID, nowTimeStr);
                var updateOK = updateNum > 0;
                _log.I("更新鋼捲PDI入料時間成功", $"鋼捲{entryCoilID}紀錄生產入料時間:{updateOK}");
                return updateOK;
            }
            catch (Exception e)
            {
                throw new Exception("更新鋼捲PDI入料時間失敗" + e.ToString().CleanInvalidChar());
            };
        }



        public PDI GetPDI(string coilID, PDISchema pdiSchema)
        {
            coilID.VaildStrNullOrEmpty("coilID", "撈取PDI失敗");

            try
            {

                var tb = _coilLogic.GetPDI(coilID, pdiSchema);
                var res = _mapper.Map<PDI>(tb);
                _log.I("撈取PDI成功", $"撈取coilID({coilID})的PDI資料");
                return res;
            }
            catch (Exception e)
            {
                _log.E("撈取PDI失敗", $"coilID({coilID}),{e.ToString().CleanInvalidChar()}");
                return null;
            }
        }

        public PDI GetPDI(string planNo, string coilID, PDISchema pdiSchema)
        {
            planNo.VaildStrNullOrEmpty("planNo", "撈取PDI失敗");
            coilID.VaildStrNullOrEmpty("coilID", "撈取PDI失敗");

            try
            {
                var tb = _coilLogic.GetPDI(planNo, coilID, pdiSchema);
                var res = _mapper.Map<PDI>(tb);
                _log.I("撈取PDI成功", $"撈取planNo({planNo})coilID({coilID})的PDI資料");
                return res;
            }
            catch (Exception e)
            {
                _log.E("撈取PDI失敗", $"planNo({planNo})coilID({coilID}),{e.ToString().CleanInvalidChar()}");
                return null;
            }
        }


        public bool UpdatePDIEntryScanCoilInfo(string entryCoilNo, bool entryCoilIDChecked)
        {
            entryCoilNo.VaildStrNullOrEmpty("entryCoilNo", "鋼捲身分確定失敗");

            try
            {
                var updateNum = _coilLogic.UpdatePDIEntryScanCoilInfo(entryCoilNo, entryCoilIDChecked);
                _log.I("鋼捲身分確定", $"鋼捲{entryCoilNo}身分確定:{updateNum > 0}");
                return updateNum > 0;

            }
            catch (Exception e)
            {
                throw new Exception("鋼捲身分確定失敗" + e.ToString().CleanInvalidChar());
            }
        }


        public void UpdateSampleFlag(bool isSample, string coilIDNo)
        {
            try
            {
                var updateNum = _coilLogic.UpdatePDISampleFlag(isSample, coilIDNo);
                _log.I("更新PDI取樣旗標", $"鋼捲{coilIDNo} 更新取樣旗標 => {updateNum > 0}");
            }
            catch (Exception e)
            {
                _log.E("更新PDI取樣旗標", e.Message.CleanInvalidChar());
            };
        }


        #endregion

        #region -- 排程相關 --

        public CoilSchedule GetScheduleInfo(string coilID)
        {
            coilID.VaildStrNullOrEmpty("coilID", "撈取鋼捲排程資料失敗");

            try
            {
                var tb = _coilLogic.GetSchedule(coilID);
                var res = _mapper.Map<CoilSchedule>(tb);
                _log.I("撈取鋼捲排程資料成功", $"撈取鋼捲{coilID}排程資料成功");
                return res;

            }
            catch (Exception e)
            {
                throw new Exception("撈取鋼捲排程資料失敗" + e.ToString().CleanInvalidChar());
            }


        }
        public List<TBL_Production_Schedule> QueryTopCoilSchedule(int num, string scheduleStatuts)
        {
            num.VaildIntValueZero("num", $"撈取前{num}筆鋼捲排程資訊失敗");

            try
            {
                var schedules = _coilLogic.QueryTopSchedule(num, scheduleStatuts);
                _log.I($"撈取前{num}筆鋼捲成功", $"已撈取{schedules.Count()}筆排程");
                return schedules.ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"撈取前{num}筆鋼捲失敗" + e.ToString().CleanInvalidChar());
            }

        }

        public bool UpdateScheduleStatuts(string coilNo, string statuts)
        {
            coilNo.VaildStrNullOrEmpty("coilNo", "更新鋼捲狀態失敗");
            statuts.VaildStrNullOrEmpty("statuts", "更新鋼捲狀態失敗");

            try
            {
                var updateNum = _coilLogic.UpdateScheduleStatus(coilNo, statuts);
                _log.I("更新鋼捲狀態成功", $"更新鋼捲{coilNo}狀態為{statuts}");

                //if (statuts.Equals(CoilDef.EntryCoilDone_Statuts))
                //    _coilLogic.UpdateScheduleSeqNo(coilNo, CoilDef.ScheduleDoneSeqDef);

                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E("更新鋼捲狀態失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public List<string> QueryUnscheduleCoils(int num)
        {
            num.VaildIntValueZero("num", "撈取鋼捲排程失敗");

            try
            {
                var coilIDs = _coilLogic.QueryUnScheduleCoilIDs(num);
                _log.I("撈取鋼捲排程成功", $"撈取{num}筆鋼捲排程");
                return coilIDs;
            }
            catch (Exception e)
            {
                _log.E("撈取鋼捲排程失敗", e.ToString().CleanInvalidChar());
                return null;
            }


        }

        public bool VaildHasSchedule(string coilID)
        {
            bool hasCoilSchedule = false;

            try
            {
                hasCoilSchedule = _coilLogic.VaildHasSchedule(coilID);
                _log.I("查詢是否有此排程", $"是否有{coilID}排程=>{hasCoilSchedule}");
            }
            catch (Exception e)
            {
                _log.E("查詢是否有排程資訊失敗", e.Message.CleanInvalidChar());
            }

            return hasCoilSchedule;
        }

        /// <summary>
        /// 批次插入鋼捲排程資料
        /// </summary>
        public bool BatchInsertSchedule(string coilCluster, int coilNum)
        {

            coilCluster.VaildStrNullOrEmpty("coilCluster", "鋼捲插入排程失敗");
            coilNum.VaildIntValueZero("coilNum", "鋼捲插入排程失敗");

            // 鋼捲號分切 
            var coiIDs = coilCluster.StrSplitBySpecificLength(CoilDef.UnitCoilIDMsgCharLen);
            // 資料庫鋼捲筆數
            var SeqN0 = (Int16)_coilLogic.GetScheduleTotalCnt();

            // 將資料載入Dt
            DataTable dt = new DataTable(DBColumnDef.SchedTbl);
            dt.Columns.Add(DBColumnDef.SchedCoilID);          // Coil_ID
            dt.Columns.Add(DBColumnDef.SchedSeqNo);           //Seq_No
            dt.Columns.Add(DBColumnDef.SchedSeqNoL3);           //Seq_No_L3
            dt.Columns.Add(DBColumnDef.ScheduleStatus);           //Schedule_Status
            dt.Columns.Add(DBColumnDef.SchedUpdateSource);    //Update_Source
            dt.Columns.Add(DBColumnDef.SchedUpdateTime);      //UpdateTime
            int insertDoneCnt = 1;

            foreach (string coilID in coiIDs)
            {

                SeqN0++;

                var row = dt.NewRow();
                row[DBColumnDef.SchedCoilID] = coilID;
                row[DBColumnDef.SchedSeqNo] = SeqN0;
                row[DBColumnDef.SchedSeqNoL3] = SeqN0;
                row[DBColumnDef.ScheduleStatus] = CoilDef.NewCoil_Statuts;
                row[DBColumnDef.SchedUpdateSource] = CoilDef.UpdateSourceMMS;
                row[DBColumnDef.SchedUpdateTime] = DateTime.Now;
                dt.Rows.Add(row);

                insertDoneCnt++;
                // 根據下發鋼捲數(CoilNum)判定 (第1筆到第Num筆)
                if (insertDoneCnt > coilNum)
                    break;

            }

            // 插入
            try
            {

                _coilLogic.CreateSchedules(dt);
                _log.I("鋼捲排程批次插入", $"目前共有{SeqN0}筆鋼捲在排程中, 新增插入{coilNum}筆鋼捲排程");
                return true;

            }
            catch (Exception e)
            {
                throw new Exception("鋼捲插入排程失敗" + e.ToString().CleanInvalidChar());
            }

        }

        /// <summary>
        /// 順序插入鋼捲排程資料
        /// </summary>
        public bool SequenceCreateSchedule(string coilCluster, int coilNum)
        {

            var productSched = new List<TBL_Production_Schedule>();

            // 鋼捲號分切 
            var coiIDs = StringUtil.StrSplitBySpecificLength(coilCluster, CoilDef.UnitCoilIDMsgCharLen);

            //TotalCount
            var SeqN0 = (Int16)_coilLogic.GetScheduleTotalCnt();
            _log.D("鋼捲生產命令", $"目前共有{SeqN0}筆鋼捲在排程中");

            // TODO 暫時先用For處理, 之後轉RX
            int count = 1;

            foreach (string coilID in coiIDs)
            {


                try
                {
                    var insertNum = _coilLogic.CreateSchedule(coilID, SeqN0);

                    if (insertNum > 0)
                    {
                        SeqN0++;
                        _log.I("鋼捲生產命令", $"新增鋼捲{coilID}至排成資料庫");

                    }
                }
                catch (Exception e)
                {
                    _log.E("鋼捲插入失敗", e.Message.CleanInvalidChar());
                }

                count++;
                // 根據下發鋼捲數(CoilNum)判定 (第1筆到第Num筆)
                if (count > coilNum)
                    break;
            }

            // 鋼捲插入成功失敗待議
            return true;
        }

        public int GetUnScheduleTotalCount()
        {
            try
            {
                var SeqN0 = (short)_coilLogic.GetScheduleTotalCnt();
                return SeqN0;
            }
            catch (SqlException e)
            {
                _log.E("撈取鋼卷排程數失敗", e.Message.CleanInvalidChar());
                return -1;
            }

        }
        public int CreateCoilSchedule(string coilID, short SeqN0)
        {
            try
            {
                var insertNum = _coilLogic.CreateSchedule(coilID, SeqN0);
                _log.I("新增鋼卷排程", $"新增鋼卷{coilID} Seq:{SeqN0} => {insertNum > 0}");
                return insertNum;
            }
            catch (SqlException e)
            {
                _log.E("新增鋼捲排程失敗", e.Message.CleanInvalidChar());
                return -1;
            }
        }

        public int GetCoilScheduleNo(string coilID)
        {
            try
            {
                var seqNo = _coilLogic.GetSeqNo(coilID);
                return seqNo;
            }
            catch (Exception e)
            {
                _log.E("獲取鋼捲排程順序號", e.ToString());
                return -1;
            }
        }

        public bool DeleteAppendScheduleByCoilID(string coilID)
        {
            int deleteNum = 0;
            var seqNo = _coilLogic.GetSeqNo(coilID);
            var scheduleStatuts = _coilLogic.GetScheduleStatuts(coilID);

            //if (seqNo <= 3 && (scheduleStatuts.Equals(CoilDef.NewCoil_Statuts) || scheduleStatuts.Equals(CoilDef.RequestEntryCoil_Statuts)))
            //{
            //    _log.E("刪除鋼捲排程失敗", $"{coilID}為第{seqNo}筆鋼捲，前三筆鋼捲不可刪除");
            //    return false;
            //}

            try
            {
                deleteNum = _coilLogic.DeleteCoildScheduleBySeqNo(seqNo);
                _log.I("刪除鋼捲排程", $"已刪除{coilID}以下鋼捲{deleteNum}筆");
            }
            catch (Exception e)
            {
                _log.E("刪除鋼捲排程", e.Message.CleanInvalidChar());
            }

            return deleteNum > 0;
        }

        public void DeleteAllSchedule()
        {
            try
            {

                _coilLogic.DeleteAllSchedule();
                _log.I("強制刪除所有排程", "強制刪除所有排程");

            }catch(Exception e)
            {
                _log.E("強制刪除所有排程失敗", e.ToString().CleanInvalidChar());
            }


        }

        public bool DeleteAllIdleSchedule()
        {
            try
            {
                var deleteNum = _coilLogic.DeleteAllIdleSchedule();
                _log.I("刪除所有排程成功", $"共刪除{deleteNum}筆");
                return deleteNum >= 0;

            }
            catch (Exception e)
            {
                _log.E("刪除所有排程失敗", e.ToString().CleanInvalidChar());
                return false;
            }

        }
        public IEnumerable<TBL_Production_Schedule> GetCollScheduleByPlanNo(string planNo)
        {
            planNo.VaildStrNullOrEmpty("planNo", "根據計劃號撈取排程失敗");

            try
            {
                var coilSchedules = _coilLogic.QueryScheduleByPlanNo(planNo);
                _log.I("根據計劃號撈取排程成功", $"根據計劃號{planNo}撈取排程成功");
                return coilSchedules;
            }
            catch (Exception e)
            {
                throw new Exception("根據計劃號撈取排程失敗" + e.ToString().CleanInvalidChar());
            }
        }

        /// <summary>
        /// 存取Return Coil資訊
        /// </summary>
        /// <param name="msg">L1 317報文</param>
        /// <returns></returns>

        public bool CreateCoilScheduleDelTempRecord(string entryCoilID, string recordType, string planNo, string operatorId = "", string reasonCode = "", string remarks = "")
        {
            entryCoilID.VaildStrNullOrEmpty("entryCoilID", "紀錄暫存刪除鋼捲排程資訊失敗");

            try
            {
                var pdi = planNo.Equals(string.Empty) ? _coilLogic.GetPDI(entryCoilID, PDISchema.EntryCoilID) : _coilLogic.GetPDI(entryCoilID, planNo, PDISchema.EntryCoilID);

                var tb = new TBL_ScheduleDelete_CoilReject_Temp();
                tb.Coil_ID = entryCoilID;
                tb.Record_Type = recordType;
                tb.Create_UserID = operatorId;
                tb.Reason_Of_Reject = reasonCode;
                tb.Remarks = remarks;
                tb.Plan_No = pdi.Plan_No;
                tb.OriPDI_Out_Coil_ID = pdi.Out_Coil_ID;

                var insertNum = _coilLogic.CreateSchDelCoilRejectRecordTemp(tb);
                var isSaveOk = insertNum > 0;
                _log.I("紀錄暫存刪除鋼捲排程資訊", $"暫存鋼捲排程{entryCoilID}{(isSaveOk).ToStr()}");
                return isSaveOk;
            }
            catch (Exception e)
            {
                _log.E("暫存刪除鋼捲排程", e.ToString().CleanInvalidChar());
                return false;
            }
        }



        public bool DeleteSchDelCoilRejectTempRecord(string coilID)
        {
            coilID.VaildStrNullOrEmpty("coilID", "刪除鋼捲刪除暫存紀錄表");

            try
            {
                var deleteNum = _coilLogic.DeleteSchDelCoilRejectTempRecordByCoilID(coilID);
                var isDeleteOk = deleteNum > 0;
                _log.I("刪除鋼捲刪除暫存紀錄表", $"暫存鋼捲排程{coilID}{(isDeleteOk).ToStr()}");
                return isDeleteOk;
            }
            catch (Exception e)
            {
                _log.E("刪除鋼捲刪除暫存紀錄表", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public ScheduleDeleteCoilRejectTempInfo GetCoilScheduleDelTempRecord(string coilID)
        {
            coilID.VaildStrNullOrEmpty("coilID", "撈取暫存鋼捲紀錄失敗");

            try
            {
                var repoData = _coilLogic.GetDelScheduleTempRecord(coilID);
                var response = _mapper.Map<ScheduleDeleteCoilRejectTempInfo>(repoData);
                return response;
            }
            catch (Exception e)
            {
                _log.E("撈取暫存鋼捲紀錄", e.ToString().CleanInvalidChar());
                return null;
            }


        }



        public bool CreateCoilScheduleDelRecords(string coilID, string operatorId = "", string reasonCode = "", string remarks = "")
        {
            coilID.VaildStrNullOrEmpty("coilID", "存取刪除排程鋼捲紀錄失敗");

            try
            {
                var delCoilSchedData = coilID.ToTblCoilScheduleDeleteEntity(operatorId, reasonCode, remarks);
                var insertNum = _coilLogic.CreateDelScheduleRecord(delCoilSchedData);
                var isDeleteOk = insertNum > 0;
                _log.I("存取刪除排程鋼捲紀錄成功", $"存取刪除排程鋼捲{coilID}{(isDeleteOk).ToStr()}");
                return isDeleteOk;
            }
            catch (Exception e)
            {
                _log.E("存取刪除排程鋼捲紀錄失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool DeleteDelScheduleRecord(string coilID)
        {

            coilID.VaildStrNullOrEmpty("coilID", "刪除刪除排成鋼捲紀錄");


            try
            {
                var delNum = _coilLogic.DeleteDelScheduleRecordByCoilID(coilID);
                _log.I("刪除刪除排成鋼捲紀錄", $"刪除刪除鋼捲{coilID}排程紀錄成功");
                return delNum > 0;

            }
            catch (Exception e)
            {
                throw new Exception("刪除刪除排成鋼捲紀錄失敗" + e.ToString().CleanInvalidChar());
            }

        }



        public bool InVaildScheduleMsgCoilNum(string coilCluster, int coilNum)
        {
            // Split 
            try
            {
                var coiIDs = StringUtil.StrSplitBySpecificLength(coilCluster, CoilDef.UnitCoilIDMsgCharLen).ToList();

                return false;
            }
            catch (Exception e)
            {
                _log.E("分捲操作失敗", e.Message.CleanInvalidChar());
                return false;
            }
        }
        public bool DeleteCoilScheduleByCoilID(string coilID)
        {

            coilID.VaildStrNullOrEmpty("coilID", "刪除鋼捲排程失敗");

            try
            {
                var deleteOk = _coilLogic.DeleteScheduleByCoilID(coilID);
                _log.I("刪除鋼捲排程", $"刪除鋼捲號{coilID}排程資訊=>{deleteOk>0}");
                return deleteOk > 0;
            }
            catch (Exception e)
            {
                _log.E("刪除鋼捲排程失敗", e.ToString().CleanInvalidChar());
                return false;
            }

        }

        public bool DeleteBatchScheduleByPlanNo(string planNo)
        {
            try
            {
                var deleteNum = _coilLogic.DeleteBatchScheduleByPlanNo(planNo);
                _log.I("批次刪除排程", $"根據計畫號{planNo}刪除{deleteNum}筆排程資訊");
                return deleteNum > 0;
            }
            catch (SqlException e)
            {
                _log.E("刪除鋼捲排程", e.Message.CleanInvalidChar());
                return false;
            }

        }


        #endregion

        #region -- PDO相關 --


        public bool CreateL25PDO(string planNo, string outCoilID)
        {
            outCoilID.VaildStrNullOrEmpty("coilID", "存取PDO至L25失敗");


            try
            {
                var tblPdo = _coilLogic.GetPDO(planNo, outCoilID);
                var defect = _coilLogic.GetDefect(tblPdo.Plan_No, outCoilID);
                defect = defect == null ? new TBL_Coil_Defect() : defect;

                var paper = _coilLogic.GetPaperData(tblPdo.Paper_Code);
                var sleeve = _coilLogic.GetSleeveData(tblPdo.Sleeve_Type_Exit_Code);

                var tb = tblPdo.ToL25PDOEntity(defect, paper, sleeve);
                var insetNum = _coilLogic.CreateL25PDO(tb);
                var insertOK = insetNum > 0;
                _log.I($"鋼捲PDO新增資料至L25:{outCoilID}", $"【鋼捲】:{outCoilID} 新增L25_PDO{(insertOK).ToStr()}");

                insetNum = _coilLogic.CreateL25PDOHis(tb);
                insertOK = insetNum > 0;
                _log.I($"鋼捲PDO新增資料至L25歷史資料庫:{outCoilID}", $"【鋼捲】:{outCoilID} 新增L25_PDO{(insertOK).ToStr()}");

                return insertOK;

            }
            catch (Exception e)
            {
                _log.E("存取PDO至L25失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool UpdatePDOTRAvgTenstion(string outCoilID, float trAvgTension)
        {
            try
            {
                var pdo = _coilLogic.GetFinalPDO(outCoilID);
                pdo.Recoiler_Actten_Avg = trAvgTension;
                var updateNum = _coilLogic.UpdatePDO(pdo.Plan_No, pdo.Out_Coil_ID, pdo);
                var updateOK = updateNum > 0;
                _log.I($"更新PDO平均收卷張力", $"更新PDO平均收卷張力 => {updateOK}");
                return updateOK;

            }
            catch(Exception e)
            {
                _log.E("更新PDO平均收卷張力錯誤", e.Message.CleanInvalidChar());
                return false;
            }
        }

        public bool UpdatePDOSiderTrimmerAvgValue(string outCoilID, SideTrimmerAvgModel value)
        {
            try
            {
                var pdo = _coilLogic.GetFinalPDO(outCoilID);
                pdo.Avg_Side_Trimmer_Gap = value.Avg_Side_Trimmer_Gap.GetPoint(2);
                pdo.Avg_Side_Trimmer_Lap = value.Avg_Side_Trimmer_Lap.GetPoint(2);
                pdo.Avg_Side_Trimmer_Width = value.Avg_Side_Trimmer_Width.GetPoint(1);
                pdo.Avg_Trimming_OperateSide = value.Avg_Trimming_OperateSide.GetPoint(1);
                pdo.Avg_Trimming_DriveSide = value.Avg_Trimming_DriveSide.GetPoint(1);
                var updateNum = _coilLogic.UpdatePDO(pdo.Plan_No, pdo.Out_Coil_ID, pdo);
                var updateOK = updateNum > 0;
                _log.I($"更新PDO平均圓盤資訊", $"更新PDO平均圓盤資訊 => {updateOK}");
                return updateOK;

            }
            catch (Exception e)
            {
                _log.E("更新PDO平均圓盤資訊錯誤", e.Message.CleanInvalidChar());
                return false;
            }
        }


        public bool UpdatePDOExCoilIDChecked(string outCoilID, string exitCoilIDChecked)
        {
            try
            {
                var pdo = _coilLogic.GetFinalPDO(outCoilID);
                pdo.Exit_CoilID_Checked = exitCoilIDChecked;
                var updateNum = _coilLogic.UpdatePDO(pdo.Plan_No, pdo.Out_Coil_ID, pdo);

                //var updateNum = _coilLogic.UpdatePDOExitCoilIDChecked(exitCoilNo, exitCoilIDChecked);
                var msg = updateNum > 0 ? $"更新PDO Exit Coil Checked { exitCoilIDChecked}成功" : "無此PDO";
                _log.I($"更新PDO ECoilCheck => {updateNum > 0}", msg);
                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E("更新PDO ECoilCheck失敗", e.Message.CleanInvalidChar());
                return false;
            }
        }

        public bool UpdateUploadPDOUserID(string planNo, string coilID, string uploadUserID)
        {
            try
            {
                var updateNum = _coilLogic.UpdatePDOIploadUserID(planNo, coilID, uploadUserID);
                var msg = updateNum > 0 ? $"更新Upload PDO UserID {coilID}成功" : "無此PDO";
                _log.I($"更新Upload PDO UserID => {updateNum > 0}", msg);
                return updateNum > 0;
            }
            catch (Exception e)
            {
                throw new Exception("更新Upload PDO UserID失敗" + e.ToString().CleanInvalidChar());
            }
        }

        public bool UpdateUploadPDOCheck(string planNo, string coilID, bool uploadFlag)
        {
            try
            {
                var uploadOK = uploadFlag ? TRUE : FALSE;
                var updateNum = _coilLogic.UpdatePDOIploadFlag(planNo, coilID, uploadOK);
                var msg = updateNum > 0 ? $"更新Upload PDO Checked {coilID}成功" : "無此PDO";
                _log.I($"更新Upload PDO Checked => {updateNum > 0}", msg);
                return updateNum > 0;
            }
            catch (Exception e)
            {
                throw new Exception("更新Upload PDO Checked失敗" + e.ToString().CleanInvalidChar());
            }
        }

        public PDO GetFinalPDO(string outCoilID)
        {
            outCoilID.VaildStrNullOrEmpty("outCoilID", "撈取最後一筆PDO資訊");

            try
            {
                var tb = _coilLogic.GetFinalPDO(outCoilID);
                var res = _mapper.Map<PDO>(tb);
                _log.I("撈取最後一筆PDO", $"撈取{outCoilID}最後一筆PDO資訊");
                return res;
            }
            catch (Exception e)
            {
                throw new Exception("撈取最後一筆PDO失敗" + e.ToString().CleanInvalidChar());
            }
        }

        public PDO GetPDO(string planNo, string outCoilID, string finishTime = "")
        {
            outCoilID.VaildStrNullOrEmpty("outCoilID", "撈取PDO資訊");

            try
            {
                var tb = _coilLogic.GetPDO(planNo, outCoilID, finishTime);
                var res = _mapper.Map<PDO>(tb);
                _log.I("撈取PDO資訊", $"撈取{outCoilID}PDO資訊");
                return res;
            }
            catch (Exception e)
            {
                throw new Exception("撈取PDO資訊失敗" + e.ToString().CleanInvalidChar());
            }
        }

        public PDO DismountCoilGenPDO(Msg_307_CoilDismount msg, string outCoilID)
        {

            msg.VaildObjectNull("msg", "Dismount產PDO失敗");
            outCoilID.VaildStrNullOrEmpty("outCoilID", "Dismount產PDO失敗");

            try
            {
                
                var pdi = _coilLogic.GetPDI(outCoilID, PDISchema.OutCoilID);
                var weld = _dataGartingLogic.GetWeldRecords(pdi.Entry_Coil_ID)?
                                            .OrderByDescending(x => x.ReceiveTime)
                                            .FirstOrDefault();

                // Gen PDO
                var coilCutRecord = GenPDODataPara(pdi.Entry_Coil_ID, ref pdi);
                var pdo = msg.TblCoilPDO(pdi, coilCutRecord, weld);

                _log.I("Dismount產PDO成功", $"產{outCoilID}PDO成功");

                var res = _mapper.Map<PDO>(pdo);

                return res;

            }
            catch (Exception e)
            {
                throw new Exception("Dismount產PDO失敗" + e.ToString().CleanInvalidChar());
            }

        }


        private GenCoilInfoModel.GenPDODataPara GenPDODataPara(string coilID, ref TBL_PDI pdi)
        {

            IEnumerable<TBL_Coil_CutRecord_Temp> cutRecords = null;
            IEnumerable<TBL_WeldRecords> weldRecords = null;

            // CutRecord (以子捲ID查詢)
            try
            {
                cutRecords = _coilLogic.QueryCutRecordsTempsByEntryCoilID(coilID);
            }
            catch (Exception e)
            {
                _log.E("撈取CutRecord", $"{coilID} 撈取CutRecord失敗," + e.Message.CleanInvalidChar());
            }
            // WeldRecord (以子捲ID查詢)
            try
            {
                weldRecords = _dataGartingLogic.GetWeldRecords(coilID);
            }
            catch (Exception e)
            {
                _log.E("撈取WeldRecord", $"{coilID} 撈取WeldRecord失敗," + e.Message.CleanInvalidChar());
            }

            var genPDOPara = new GenCoilInfoModel.GenPDODataPara();

            // 計算[導帶]切廢
            cutRecords.ToList().ForEach(x =>
            {

                if (!x.CutDevice.Equals(DeviceParaDef.ExitShear))
                    return;

                if (x.CutDevice.Equals(CutModeHeadCut))
                    genPDOPara.ScrapedLengthEntry += (int)x.CutLength;
                else
                    genPDOPara.ScrapedLengthExit += (int)x.CutLength;

            });

            // 判斷是否有焊接 
            genPDOPara.NoLeaderCode = weldRecords.Count() > 0 ? USE : NOTUSE;

            // 索取各個重量
            if (pdi != null)
            {
                //墊紙重量
                if (!pdi.Paper_Code.Equals(string.Empty)) { }
                genPDOPara.PaperWt = GetPaperWt(pdi.Paper_Code);

                //套筒重量
                if (!pdi.Out_Sleeve_Type_Code.Equals(string.Empty))
                    genPDOPara.SleeveWt = (int)GetSleeveWt(pdi.Out_Paper_Req_Code);

            }

            // 撈取班別班股

            try
            {
                var time = pdi.Finish_Time;
                var workschedule = _dataGartingLogic.GetScheduleByTime(pdi.Finish_Time);
                if (workschedule.Mode == 1)
                {
                    workschedule = _dataGartingLogic.GetScheduleByTime(time, "<=");
                }
                else if (workschedule.Mode == 2 || workschedule.Mode == 3)
                {
                    if (time.Hour < 8)
                    {
                        workschedule = _dataGartingLogic.GetScheduleByTime(time, ">=");
                    }
                    else if (time.Hour > 20)
                    {
                        var reSearchTime = time.AddDays(1);
                        workschedule = _dataGartingLogic.GetScheduleByTime(reSearchTime, "<=");
                    }
                    else
                    {
                        workschedule = _dataGartingLogic.GetScheduleByTime(time, "<=");
                    }
                }
                genPDOPara.Shift = workschedule.Shift;
                genPDOPara.Team = workschedule.Team;
            }
            catch (Exception e)
            {
                _log.E("撈取排班資料錯誤", $"{coilID} 撈取排班資料錯誤失敗," + e.Message.CleanInvalidChar());
            }

            //try
            //{
            //    var workschedule = _dataGartingLogic.GetScheduleByTime(pdi.Finish_Time);
            //    genPDOPara.Shift = workschedule.Shift;
            //    genPDOPara.Team = workschedule.Team;
            //}
            //catch (Exception e)
            //{
            //    _log.E("撈取排班資料錯誤", $"{coilID} 撈取排班資料錯誤失敗," + e.Message.CleanInvalidChar());
            //}


            // 索取導帶資料
            if (pdi.Leader_Flag.Equals(USE))
                try
                {
                    var leaderTemp = _coilLogic.GetLeaderData(pdi.Entry_Coil_ID);

                    genPDOPara.Head_Leader_St_No = leaderTemp.Head_Leader_St_No;
                    genPDOPara.Head_Leader_Length = leaderTemp.Head_Leader_Length;
                    genPDOPara.Head_Leader_Width = leaderTemp.Head_Leader_Width;
                    genPDOPara.Head_Leader_Thickness = leaderTemp.Head_Leader_Thickness;

                    genPDOPara.Tail_Leader_St_No = leaderTemp.Tail_Leader_St_No;
                    genPDOPara.Tail_Leader_Length = leaderTemp.Tail_Leader_Length;
                    genPDOPara.Tail_Leader_Width = leaderTemp.Tail_Leader_Width;
                    genPDOPara.Tail_Leader_Thickness = leaderTemp.Tail_Leader_Thickness;

                    // 導帶重量
                    if (pdi.Leader_Flag.Equals(USE))
                    {
                        // 查詢導帶密度
                        var headDensity = GetLeaderDensity(genPDOPara.Head_Leader_St_No);
                        var tailDensity = GetLeaderDensity(genPDOPara.Tail_Leader_St_No);
                        // 計算重量
                        genPDOPara.TailLeaderWt = headDensity != -1f ? leaderTemp.TailStripVolume * headDensity : 0;
                        genPDOPara.HeadLeaderWt = tailDensity != -1f ? leaderTemp.HeadStripVolume * tailDensity : 0;
                    }

                }
                catch (Exception e)
                {
                    _log.E("撈取導帶資料錯誤", $"{coilID} 撈取導帶資料錯誤失敗," + e.Message.CleanInvalidChar());
                }

            // 索取墊紙

            return genPDOPara;


        }
        public void RecaulateWeightAndUpdatePDO(string outCoilID, float coilWt)
        {

            var pdo = new TBL_PDO();
            IEnumerable<TBL_Coil_CutRecord_Temp> cutRecords = null;
            IEnumerable<TBL_WeldRecords> weldRecords = null;

            // PDO
            try
            {
                pdo = _coilLogic.GetFinalPDO(outCoilID);
            }
            catch (Exception e)
            {
                _log.E("撈取PDO失敗", $"{outCoilID} PDO撈取失敗," + e.Message.CleanInvalidChar());
            }


            var entryCoil = pdo.In_Coil_ID.Trim();

            // CutRecord 
            try
            {
                cutRecords = _coilLogic.QueryCutRecordsTempsByEntryCoilID(entryCoil);
            }
            catch (Exception e)
            {
                _log.E("撈取CutRecord", $"{entryCoil} 撈取CutRecord失敗," + e.Message.CleanInvalidChar());
            }
            // WeldRecord 
            try
            {
                weldRecords = _dataGartingLogic.GetWeldRecords(entryCoil);
            }
            catch (Exception e)
            {
                _log.E("撈取WeldRecord", $"{entryCoil} 撈取WeldRecord失敗," + e.Message.CleanInvalidChar());
            }

            var coilCutRecord = new GenCoilInfoModel.GenPDODataPara();

            // 計算[導帶]切廢
            if (coilCutRecord != null)
                cutRecords.ToList().ForEach(x =>
                {
                    if (!x.CutDevice.Equals(DeviceParaDef.ExitShear))
                        return;

                    if (x.CutDevice.Equals(CutModeHeadCut))
                        coilCutRecord.ScrapedLengthEntry += (int)x.CutLength;
                    else
                        coilCutRecord.ScrapedLengthExit += (int)x.CutLength;
                });

            //判斷是否焊接
            if (weldRecords != null)
                coilCutRecord.NoLeaderCode = weldRecords.Count() > 1 ? "0" : "1";

            // 索取各個重量
            if (pdo != null)
            {
                //墊紙
                if (!pdo.Paper_Code.Equals(string.Empty))
                    coilCutRecord.PaperWt = GetPaperWt(pdo.Paper_Code);

                //套筒
                if (!pdo.Sleeve_Type_Exit_Code.Equals(string.Empty))
                    coilCutRecord.SleeveWt = (int)GetSleeveWt(pdo.Sleeve_Type_Exit_Code);

                // 導帶重量
                if (pdo.No_Leader_Code.Equals(USE))
                {
                    // 查詢導帶密度
                    var headDensity = GetLeaderDensity(pdo.Head_Leader_St_No);
                    var tailDensity = GetLeaderDensity(pdo.Tail_Leader_St_No);
                    // 計算重量
                    coilCutRecord.TailLeaderWt = headDensity != -1f ? pdo.TailStripVolume * headDensity : 0;
                    coilCutRecord.HeadLeaderWt = tailDensity != -1f ? pdo.HeadStripVolume * tailDensity : 0;
                }
            }

            // 更新重量 - 淨重
            var coilPureWt = coilWt - coilCutRecord.TotalWt;
            // 更新PDO
            try
            {

                pdo.Out_Coil_Gross_WT = coilWt;
                pdo.Out_Coil_Wt = (int)coilPureWt;

                var updateNum = _coilLogic.UpdatePDO(pdo.Plan_No, pdo.Out_Coil_ID, pdo);
                //var updateNum = _coilLogic.UpdatePDOExitCoilWt(outCoilID, coilPureWt, coilWt);
                _log.I("更新PDO重量", $"更新PDO毛重{coilWt}， 淨重{coilPureWt}=>{updateNum > 0} ");
            }
            catch (Exception e)
            {
                _log.E("更新PDO淨重失敗", $"{outCoilID} 更新PDO重量," + e.Message.CleanInvalidChar());
            }
        }

        public bool CreatePDO(PDO data)
        {
            data.VaildObjectNull("data", "PDO存取失敗");
            try
            {
                var tb = _mapper.Map<TBL_PDO>(data);
                var insertNum = _coilLogic.CreatePDO(tb);
                var insertOK = insertNum > 0;
                _log.I($"PDO存取成功", $"存取{tb.Out_Coil_ID}PDO成功");
                return insertOK;
            }
            catch (Exception e)
            {

                _log.E("PDO存取失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool CreatePdoUploadedReply(Msg_Res_For_PDO_Uploaded msg)
        {
            msg.VaildObjectNull("msg", "新增上傳PDO的回覆失敗");

            try
            {
                var tb = _mapper.Map<TBL_PDOUploadedReply>(msg);
                var insertNum = _coilLogic.CreatePDOUploadedReply(tb);
                var insertOK = insertNum > 0;
                _log.I($"新增上傳PDO的回覆成功", $"新增上傳PDO的回覆成功，計畫號={tb.Plan_No}、材料號={tb.Out_Coil_ID}");
                return insertOK;
            }
            catch (Exception e)
            {
                _log.E("新增上傳PDO的回覆失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        #endregion

        #region 墊紙套筒導帶密度
        public int GetPaperWt(string code)
        {
            try
            {
                var paperData = _coilLogic.GetPaperData(code);
                var wt = paperData != null ? paperData.Paper_Base_Weight : 0;
                _log.I("撈取墊紙重量", $"撈取{code}-Paper重量 => {paperData != null} 重量為{wt}");
                return wt;
            }
            catch (Exception e)
            {
                _log.E("撈取墊紙重量錯誤", e.Message.CleanInvalidChar());
                return 0;
            }
        }
        public float GetSleeveWt(string code)
        {
            try
            {
                var SleeveData = _coilLogic.GetSleeveData(code);
                var wt = SleeveData != null ? SleeveData.Sleeve_Weight : 0;
                _log.I("撈取套筒重量", $"撈取{code}-Paper重量 => {SleeveData != null} 重量為{wt}");
                return wt;
            }
            catch (Exception e)
            {
                _log.E("撈取套筒重量錯誤", e.Message.CleanInvalidChar());
                return 0;
            }
        }
        public TBL_LookupTable_Sleeve GetSleeveData(string code)
        {
            try
            {
                var Sleeve = _coilLogic.GetSleeveData(code);
                _log.I("撈取套筒資訊", $"撈取代碼{code}的資訊");
                return Sleeve;
            }
            catch (Exception e)
            {
                _log.E("撈取套筒資訊錯誤", e.Message.CleanInvalidChar());
                return null;
            }
        }
        public float GetLeaderDensity(string stNo)
        {
            _log.D($"取導帶密度", $"鋼種={stNo}");

            var density = 0f;
            switch (stNo)
            {
                case "301":
                case "304": density = 7930f; break;
                case "443": density = 7740f; break;
                case "430": density = 7700f; break;
            }
            return density;     //  m^3

            //// 資料表待建立  
            //return CoilDef.LeaderDensity * 1000;   // m^3 -> * 1000
        }

        #endregion

        #region -- 鋼捲分切與斷帶 -- 



        /// <summary>
        /// 撈取分切過幾次紀錄(分切 or 廢料切)
        /// </summary>
        /// <param name="entryCoilID">母捲ID</param>
        /// <returns></returns>
        public int GetParentCnt(string entryCoilID)
        {
            try
            {
                var cnt = _coilLogic.GetParentCnt(entryCoilID);
                _log.I("撈取分切紀錄", $"撈取母捲之子捲筆數=>{cnt}");
                return cnt;
            }
            catch (Exception e)
            {
                _log.I("撈取分切紀錄失敗", e.Message.CleanInvalidChar());
                return -1;
            }
        }

        public string GenSplitChildrenCoilID(string entryCoilID, int childrenCoilCnt = -1)
        {
            //var pdi = GetPDI(inCoilID, PDISchema.OutCoilID);
            //var childrenCoilID = string.Empty;

            //if (pdi == null)
            //{
            //    // 直接作分切(第一次)
            //    childrenCoilID = _coilLogic.SplitCoilPro(inCoilID, 0);
            //    return childrenCoilID;
            //}

            var coilID = entryCoilID.Trim();
            var splitCoilID = string.Empty;

            // 分切處理
            if (childrenCoilCnt == -1)
                childrenCoilCnt = _coilLogic.GetParentCnt(entryCoilID);

            if (coilID.Length == CoilDef.ColdRolledCoilLength)
            {

                splitCoilID = _coilLogic.SplitColdRolledCoilID(entryCoilID, childrenCoilCnt);
                _log.I("冷軋捲分切", $"分切號為{splitCoilID}");
            }
            else
            {
                splitCoilID = _coilLogic.SplitHotRolledCoilID(entryCoilID, childrenCoilCnt);
                _log.I("熱軋捲分切", $"分切號為{splitCoilID}");
            }

            return splitCoilID;
        }


        public bool CreateEnCoilCutRecordTemp(Msg_301_EnCoilCut msg)
        {
            msg.VaildObjectNull("msg", "存取分切暫存入口裁剪資料失敗");

            var entryCoilID = msg.CoilID.ToStr();

            try
            {
                var pdi = GetPDI(entryCoilID, PDISchema.EntryCoilID);
                var weight = _coilLogic.CalculatePDICoilWeightByLength(pdi.Entry_Coil_Thick, pdi.Entry_Coil_Width, msg.CutLength, (float)pdi.Density);
                var tb = _mapper.Map<TBL_Coil_CutRecord_Temp>(msg);
                tb.CutMode = msg.CutMode == DeviceParaDef.HeaderCut ? DBParaDef.CutModeHeadCut : DBParaDef.CutModeTailCut;
                tb.Coil_CalcWeight = weight;
                tb.OriPDI_Out_Coil_ID = pdi.Out_Coil_ID;

                var insertNum = _coilLogic.CreateCoilCutRecordTemp(tb);
                var insertOK = insertNum > 0;
                _log.I("存取分切暫存入口裁剪資料成功", $"新增資料{insertNum}筆成功");
                return insertOK;
            }
            catch (Exception e)
            {
                _log.E("存取分切暫存入口裁剪資料失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool CreateExitCoilScrapCutRecordTemp(Msg_311_ExCoilCut msg)
        {
            msg.VaildObjectNull("msg", "存取分切暫存出口裁剪資料失敗");

            var entryCoilID = msg.CoilID.ToStr();
            try
            {
                var pdi = GetPDI(entryCoilID, PDISchema.EntryCoilID);
                var weight = _coilLogic.CalculatePDICoilWeightByLength(pdi.Entry_Coil_Thick, pdi.Entry_Coil_Width, msg.CutLength, pdi.Density);
                //var weight = _coilLogic.CalculatePDICoilWeightByLength(entryCoilID, msg.CutLength);
                var tb = _mapper.Map<TBL_Coil_CutRecord_Temp>(msg);
                tb.CutMode = msg.CutMode == CoilDef.CutHeadScrapCut ? DBParaDef.CutModeHeadCut : DBParaDef.CutModeTailCut;
                tb.Coil_CalcWeight = weight;
                tb.OriPDI_Out_Coil_ID = pdi.Out_Coil_ID;

                var insertNum = _coilLogic.CreateCoilCutRecordTemp(tb);
                var insertOK = insertNum > 0;
                _log.I("存取分切暫存出口裁剪資料成功", $"新增資料{insertNum}筆成功");
                return insertOK;
            }
            catch (Exception e)
            {
                _log.E("存取分切暫存出口裁剪資料失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }


        public bool DeleteCoilCutRecordTemp(string coilID)
        {
            coilID.VaildStrNullOrEmpty("coilID", "刪除分切暫存紀錄失敗");

            try
            {
                var deleteNum = _coilLogic.DeleteCutRecordTempByCoilID(coilID);
                var delteOK = deleteNum > 0;
                _log.I("刪除分切暫存紀錄成功", $"刪除鋼捲{coilID}分切暫存紀錄");

                return delteOK;

            }
            catch (Exception e)
            {
                throw new Exception("刪除分切暫存紀錄失敗" + e.ToString().CleanInvalidChar());
            }

        }

        public bool CreatePORSplitCoilRecordTemp(string splitCoilID, string entryCoilID, string oriOutCoilID)
        {
            splitCoilID.VaildObjectNull("msg", "存取POR　Split Coil Temp資料失敗");

            try
            {

                var dao = new TBL_Coil_CutRecord_Temp();
                dao.Coil_ID = splitCoilID;
                dao.In_Coil_ID = entryCoilID;
                dao.OriPDI_Out_Coil_ID = oriOutCoilID;
                dao.CutMode = DBParaDef.CutModeReturnCoil;
                dao.CutTime = DateTime.Now;

                var insertNum = _coilLogic.CreateCoilCutRecordTemp(dao);
                var insertOK = insertNum > 0;
                _log.I("存取POR　Split Coil Temp資料成功", $"新增資料{insertNum}筆成功");
                return insertOK;

            }
            catch (Exception e)
            {
                _log.E("存取POR　Split Coil Temp資料失敗", e.ToString().CleanInvalidChar());
                return false;
            }

        }



        public bool CreateExitCoilCutRecordTemp(Msg_311_ExCoilCut msg, string splitCoilID, string enCoilID, string oriOutCoilID)
        {
            msg.VaildObjectNull("msg", "存取Exit Coil Cut Record Temp資料失敗");

            try
            {
                var pdi = GetPDI(msg.ParentCoilID, PDISchema.EntryCoilID);
                //var weight = _coilLogic.CalculatePDICoilWeightByLength(pdi.Entry_Coil_Thick, pdi.Entry_Coil_Width, msg.CutLength, pdi.Density);
                //msg.CalculateWeightRec = msg.CutMode == CoilDef.CutModeSplitCut ? msg.CalculateWeightRec : weight;
                var tbl = _mapper.Map<TBL_Coil_CutRecord_Temp>(msg);

                // 在CPL，目前模式只有 1=斷帶(StripBreak) 2=分切(SplitCut) 4=取樣切(SampleCut) a=入口裁切頭部 b=入口裁切尾部

                tbl.CutMode = msg.CutMode == CoilDef.CutModeVirtualCut ? CoilDef.CutModeSplitCut.ToString() : msg.CutMode.ToString();
                tbl.Coil_ID = splitCoilID;
                tbl.In_Coil_ID = enCoilID;
                tbl.OriPDI_Out_Coil_ID = oriOutCoilID;

                var insertNum = _coilLogic.CreateCoilCutRecordTemp(tbl);
                var insertOK = insertNum > 0;
                _log.I("存取Exit Coil Cut Record Temp資料成功", $"新增資料{insertNum}筆成功");
                return insertOK;

            }
            catch (Exception e)
            {
                _log.E("存取Exit Coil Cut Record Temp資料失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool CreateUmontRecord(Msg_311_ExCoilCut msg, string coilID, string oriOutCoilID)
        {
            msg.VaildObjectNull("msg", "存取斷帶鋼捲記錄失敗");
            coilID.VaildObjectNull("coilID", "存取斷帶鋼捲記錄失敗");

            try
            {
                var pdi = GetPDI(msg.ParentCoilID, PDISchema.EntryCoilID);
                var weight = _coilLogic.CalculatePDICoilWeightByLength(pdi.Entry_Coil_Thick, pdi.Entry_Coil_Width, msg.CutLength, pdi.Density);
                msg.CalculateWeightRec = msg.CutMode == CoilDef.CutModeSplitCut ? msg.CalculateWeightRec : weight;
                var tb = _mapper.Map<TBL_UnmountRecord>(msg);
                tb.Coil_ID = coilID;
                tb.CoilWeight = msg.CalculateWeightRec;
                tb.OriPDI_Out_Coil_ID = oriOutCoilID;

                var insertNum = _coilLogic.SaveUmounRecord(tb);
                var insertOK = insertNum > 0;
                _log.I("存取斷帶鋼捲記錄成功", $"存取斷帶鋼捲{coilID}記錄成功");

                return insertOK;

            }
            catch (Exception e)
            {
                _log.E("存取斷帶鋼捲記錄失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool DeleteUmountRecord(string coilID)
        {
            coilID.VaildObjectNull("coilID", "刪除斷帶鋼捲記錄失敗");

            try
            {
                var deleteNum = _coilLogic.DeleteUmountRecordByCoilID(coilID);
                var deleteOK = deleteNum > 0;
                _log.I("刪除斷帶鋼捲記錄成功", $"刪除斷帶鋼捲{coilID}記錄成功");
                return deleteOK;
            }
            catch (Exception e)
            {
                throw new Exception("刪除斷帶鋼捲記錄失敗" + e.ToString().CleanInvalidChar());
            }
        }

        #endregion

        #region -- 套筒墊紙同步處理 --

        public bool SyncSleeveValue(MMSL2Rcv.Msg_Sleeve_Value_Synchronize msg)
        {
            if (msg.Deal_Flag.ToStr().Equals(MMSSysDef.Cmd.SyncValueInsert))
            {

                try
                {
                    var insertNum = _coilLogic.CreateSleeveValue(msg);
                    _log.I($"套筒資料同步訊息:{msg.Code.ToStr()}", $"新增資料 CODE:{msg.SleeveCode.ToStr()}=>{insertNum > 0}");
                    return insertNum > 0;

                }
                catch (Exception e)
                {
                    _log.E($"套筒資料同步訊息:{msg.Code.ToStr()}", "新增失敗:" + e.Message.CleanInvalidChar());
                    return false;
                }


            }

            if (msg.Deal_Flag.ToStr().Equals(MMSSysDef.Cmd.SyncValueUpdate))
            {
                try
                {
                    var updateNum = _coilLogic.UpdateSleeveValue(msg);
                    _log.I($"套筒資料同步訊息:{msg.Code.ToStr()}", $"更新資料 CODE:{msg.SleeveCode.ToStr()}=>{updateNum > 0}");
                    return updateNum > 0;

                }
                catch (Exception e)
                {
                    _log.E($"套筒資料同步訊息:{msg.Code.ToStr()}", "更新失敗:" + e.Message.CleanInvalidChar());
                    return false;
                }


            }

            if (msg.Deal_Flag.ToStr().Equals(MMSSysDef.Cmd.SyncValueDelete))
            {
                try
                {
                    var code = msg.SleeveCode.ToStr();
                    var delNum = _coilLogic.DeleteSleeveValueByCode(code);
                    _log.I($"套筒資料同步訊息:{msg.Code.ToStr()}", $"刪除資料 CODE:{code}=>{delNum > 0}");

                    return delNum > 0;

                }
                catch (Exception e)
                {
                    _log.E($"套筒資料同步訊息:{msg.Code.ToStr()}", "刪除失敗:" + e.Message.CleanInvalidChar());
                    return false;
                }
            }

            return false;

        }


        public bool SyncPaperValue(MMSL2Rcv.Msg_Paper_Value_Synchronize msg)
        {

            if (msg.Deal_Flag.ToStr().Equals(MMSSysDef.Cmd.SyncValueInsert))
            {

                try
                {
                    var insertNum = _coilLogic.CreatePaperValue(msg);
                    _log.I($"墊紙資料同步訊息:{msg.Code.ToStr()}", $"新增資料 CODE:{msg.PaperCode.ToStr()}=>{insertNum > 0}");
                    return insertNum > 0;

                }
                catch (Exception e)
                {
                    _log.E($"墊紙資料同步訊息:{msg.Code.ToStr()}", "新增失敗:" + e.Message.CleanInvalidChar());
                    return false;
                }


            }

            if (msg.Deal_Flag.ToStr().Equals(MMSSysDef.Cmd.SyncValueUpdate))
            {
                try
                {
                    var updateNum = _coilLogic.UpdatePaperValue(msg);
                    _log.I($"墊紙資料同步訊息:{msg.Code.ToStr()}", $"更新資料 CODE:{msg.PaperCode.ToStr()}=>{updateNum > 0}");
                    return updateNum > 0;

                }
                catch (Exception e)
                {
                    _log.E($"墊紙資料同步訊息:{msg.Code.ToStr()}", "更新失敗:" + e.Message.CleanInvalidChar());
                    return false;
                }
            }

            if (msg.Deal_Flag.ToStr().Equals(MMSSysDef.Cmd.SyncValueDelete))
            {
                try
                {
                    var code = msg.PaperCode.ToStr();
                    var delNum = _coilLogic.DeletePaperValueByCode(code);
                    _log.I($"墊紙資料同步訊息:{msg.Code.ToStr()}", $"刪除資料 CODE:{code}=>{delNum > 0}");
                    return delNum > 0;

                }
                catch (Exception e)
                {
                    _log.E($"墊紙資料同步訊息:{msg.Code.ToStr()}", "刪除失敗" + e.Message.CleanInvalidChar());
                    return false;
                }


            }

            return false;
        }

        #endregion

        #region -- 退料 --

        /// <summary>
        /// L1 Coil Dismount 存取 排程跳軋/鋼捲退料暫存記錄資料
        /// </summary>
        /// <param name="msg">308報文 CoilWeightScale</param>
        /// <param name="entryCoilID">出口捲(母捲)</param>
        /// <returns></returns>
        public bool CreateCoilWeightScaleInScheduleDeleteCoilRejectTemp(Msg_308_CoilWeightScale msg, string entryCoilID)
        {
            msg.VaildObjectNull("msg", "CoilWeightScale 存取 排程跳軋/鋼捲退料暫存記錄資料失敗");

            try
            {
                var coilID = msg.CoilID.ToStr();
                var oldtb = _coilLogic.GetDelScheduleTempRecordByOutCoilID(coilID);

                if (oldtb == null)
                {
                    var tb = new TBL_ScheduleDelete_CoilReject_Temp();
                    tb.Coil_ID = coilID;
                    tb.Record_Type = CoilDef.ScheduleReject;
                    tb.Entry_Coil_ID = entryCoilID;
                    tb.Weight_Of_Rejected_Coil = msg.CoilWeight.ToString();
                    tb.Remarks = MMSSysDef.Cmd.DelScheduleByReject;

                    var insertNum = _coilLogic.CreateSchDelCoilRejectRecordTemp(tb);
                    var insertOK = insertNum > 0;
                    _log.I("存取 排程跳軋/鋼捲退料暫存記錄資料成功", $"CoilWeightScale 存取{coilID}排程跳軋/鋼捲退料暫存記錄資料成功");
                    return insertOK;

                }
                else
                {
                    // Update
                    oldtb.Weight_Of_Rejected_Coil = msg.CoilWeight.ToString();
                    var updateNum = _coilLogic.UpdateSchDelCoilRejectRecordTemp(oldtb);
                    var updateOK = updateNum > 0;
                    _log.I("更新 排程跳軋/鋼捲退料暫存記錄資料成功", $" CoilWeightScale更新{coilID}排程跳軋/鋼捲退料暫存記錄資料成功");
                    return updateOK;
                }

            }
            catch (Exception e)
            {
                throw new Exception("CoilWeightScale 存取 排程跳軋/鋼捲退料暫存記錄資料失敗" + e.ToString().CleanInvalidChar());
            }


        }

        /// <summary>
        /// L1 Coil Dismount 存取 排程跳軋/鋼捲退料暫存記錄資料
        /// </summary>
        /// <param name="msg">307報文 CoilDismount</param>
        /// <param name="entryCoilID">入口捲(母捲)</param>
        /// <returns></returns>
        public bool CreateCoilDismountInfoInScheduleDeleteCoilRejectTemp(Msg_307_CoilDismount msg, string entryCoilID)
        {
            msg.VaildObjectNull("msg", "CoilDismount 存取 排程跳軋/鋼捲退料暫存記錄資料失敗");

            try
            {
                var coilID = msg.CoilID.ToStr();
                var oldtb = _coilLogic.GetDelScheduleTempRecord(coilID);

                if (oldtb == null)
                {
                    var tb = new TBL_ScheduleDelete_CoilReject_Temp();
                    tb.Coil_ID = coilID;
                    tb.Record_Type = CoilDef.ScheduleReject;
                    tb.Entry_Coil_ID = entryCoilID;
                    tb.Weight_Of_Rejected_Coil = msg.CoilWeight.ToString();
                    tb.Length_Of_Rejected_Coil = msg.CoilLength.ToString();
                    tb.Outer_Diameter_Of_RejectedCoil = msg.Diameter.ToString();
                    tb.Inner_Diameter_Of_RejectedCoil = msg.CoiInsideDiam.ToString();

                    var insertNum = _coilLogic.CreateSchDelCoilRejectRecordTemp(tb);
                    var insertOK = insertNum > 0;
                    _log.I("存取 排程跳軋/鋼捲退料暫存記錄資料成功", $"CoilDismount 存取{coilID}排程跳軋/鋼捲退料暫存記錄資料成功");
                    return insertOK;

                }
                else
                {
                    // Update
                    oldtb.Weight_Of_Rejected_Coil = msg.CoilWeight.ToString();
                    oldtb.Length_Of_Rejected_Coil = msg.CoilLength.ToString();
                    oldtb.Outer_Diameter_Of_RejectedCoil = msg.Diameter.ToString();
                    oldtb.Inner_Diameter_Of_RejectedCoil = msg.CoiInsideDiam.ToString();
                    oldtb.Paper_Type = msg.PaperCode.ToStr();

                    var updateNum = _coilLogic.UpdateSchDelCoilRejectRecordTemp(oldtb);
                    var updateOK = updateNum > 0;
                    _log.I("更新 排程跳軋/鋼捲退料暫存記錄資料成功", $" CoilDismount更新{coilID}排程跳軋/鋼捲退料暫存記錄資料成功");
                    return updateOK;
                }

            }
            catch (Exception e)
            {
                throw new Exception("CoilDismount 存取 排程跳軋/鋼捲退料暫存記錄資料失敗" + e.ToString().CleanInvalidChar());
            }


        }

        public bool VaildHasScheduleDeleteCoilRejectTemp(string coilID)
        {
            try
            {
                var hasData = _coilLogic.VaildHasSchDelCoilRejectRecordTemp(coilID);
                _log.I("CoilDismount 判定是否有 排程跳軋/鋼捲退料暫存記錄資料成功", $"判定是否有{coilID}排程跳軋/鋼捲退料暫存記錄資料成功=>{hasData}");
                return hasData;
            }
            catch (Exception e)
            {
                throw new Exception("CoilDismount 判定是否有 排程跳軋/鋼捲退料暫存記錄資料失敗" + e.ToString().CleanInvalidChar());
            }
        }

        /// <summary>
        /// 斷帶資訊 存取 存取 排程跳軋/鋼捲退料暫存記錄資料
        /// </summary>
        /// <param name="coilID">斷帶鋼捲號</param>
        /// <param name="entryCoilID">入口捲(母捲)</param>
        /// <param name="planNo">鋼捲計畫號</param>
        /// <returns></returns>
        public bool CreateStripBrekInScheduleDeleteCoilRejectTemp(string coilID, PDI pdi)
        {
            coilID.VaildStrNullOrEmpty("coilID", "存取 排程跳軋/鋼捲退料暫存記錄資料失敗");
            pdi.VaildObjectNull("pdi", "存取 排程跳軋/鋼捲退料暫存記錄資料失敗");


            try
            {
                var tb = new TBL_ScheduleDelete_CoilReject_Temp();
                tb.Coil_ID = coilID;
                tb.Entry_Coil_ID = pdi.Entry_Coil_ID;
                tb.Plan_No = pdi.Plan_No;
                tb.Record_Type = CoilDef.ScheduleReject;
                tb.OriPDI_Out_Coil_ID = pdi.Out_Coil_ID;
                var insertNum = _coilLogic.CreateSchDelCoilRejectRecordTemp(tb);
                var insertOK = insertNum > 0;
                _log.I("存取 排程跳軋/鋼捲退料暫存記錄資料 成功", $"新增資料{insertNum}筆成功");
                return insertOK;
            }
            catch (Exception e)
            {
                _log.E("存取 排程跳軋/鋼捲退料暫存記錄資料失敗", e.ToString().CleanInvalidChar());
                return false;
            }

        }

        public bool CreateL1RetrunCoil(Msg_317_ReturnCoilInfo msg)
        {
            msg.VaildObjectNull("msg", "存取 排程跳軋/鋼捲退料暫存記錄資料 失敗");

            try
            {
                var coilID = msg.CoilID.ToStr();

                // 撈取母鋼捲出口ID
                var splitRecord = GetCutRecordTemp(coilID);
                coilID = splitRecord == null ? coilID : splitRecord.In_Coil_ID;
                var pdi = GetPDI(coilID, PDISchema.EntryCoilID);
                var record = msg.ToTblScheduleDeleteCoilRejectTempEntity(pdi.Entry_Coil_ID);
                record.Record_Type = CoilDef.ScheduleReject;
                record.OriPDI_Out_Coil_ID = pdi.Out_Coil_ID;
                var insertNum = _coilLogic.CreateSchDelCoilRejectRecordTemp(record);
                var insertOK = insertNum > 0;
                _log.I("存取 排程跳軋/鋼捲退料暫存記錄資料 成功", $"新增資料{insertNum}筆成功");
                return insertOK;
            }
            catch (Exception e)
            {
                throw new Exception("存取 排程跳軋/鋼捲退料暫存記錄資料 失敗" + e.ToString().CleanInvalidChar());
            }
        }

        public ReturnCoilInfo GetReturnCoilTemp(string coilID)
        {
            coilID.VaildStrNullOrEmpty("coilID", "撈取 排程跳軋/鋼捲退料暫存記錄資料 失敗");

            try
            {
                var repoData = _coilLogic.GetDelScheduleTempRecord(coilID);
                var response = _mapper.Map<ReturnCoilInfo>(repoData);
                _log.I("撈取 排程跳軋/鋼捲退料暫存記錄資料 成功", $"撈取 {coilID} 排程跳軋/鋼捲退料暫存記錄資料成功");
                return response;
            }
            catch (Exception e)
            {
                _log.E("撈取 排程跳軋/鋼捲退料暫存記錄資料 失敗", e.ToString().CleanInvalidChar());
                return null;
            }
        }

        public bool CreateTempToCoilReject(ReturnCoilInfo returnInfo)
        {
            returnInfo.VaildObjectNull("returnInfo", "存取鋼捲回退實績失敗");

            try
            {
                var tb = _mapper.Map<TBL_CoilRejectResult>(returnInfo);
                var insertNum = _coilLogic.CreateCoilReject(tb);
                var insertOK = insertNum > 0;
                _log.I("存取鋼捲回退實績成功", $"存取鋼捲{returnInfo.Coil_ID}回退實績");
                return insertOK;

            }
            catch (Exception e)
            {
                throw new Exception("存取鋼捲回退實績失敗" + e.ToString().CleanInvalidChar());
            }
        }

        public bool DeleteCoilRejectByCoilID(string coilID)
        {
            coilID.VaildStrNullOrEmpty("coilID", "刪除鋼捲回退實績失敗");

            try
            {
                var deleteNum = _coilLogic.DeleteCoilRejectResultByCoilID(coilID);
                var deleteOK = deleteNum > 0;
                _log.I("刪除鋼捲回退實績成功", $"刪除鋼捲{coilID}回退實績成功");

                return deleteOK;

            }
            catch (Exception e)
            {
                throw new Exception("刪除鋼捲回退實績失敗" + e.ToString().CleanInvalidChar());
            }
        }

        public bool DelectLeaderTemp(string coilID)
        {
            coilID.VaildStrNullOrEmpty("Leader", "刪除暫存導帶資訊");

            try
            {
                var deleteNum = _coilLogic.DeleteLeaderTempByCoilID(coilID);
                var deleteOK = deleteNum > 0;
                _log.I("刪除暫存導帶資訊成功", $"刪除鋼捲{coilID}回退實績成功");

                return deleteOK;

            }
            catch (Exception e)
            {
                throw new Exception("刪除鋼捲回退實績失敗" + e.ToString().CleanInvalidChar());
            }
        }

        public bool CreateL25CoilRejectResult(ReturnCoilInfo returnCoilInfo)
        {
            returnCoilInfo.VaildObjectNull("returnCoilInfo", "存取L2.5鋼捲回退實績失敗");

            try
            {
                var tb = returnCoilInfo.ToL25CoilRejectResult();
                var insertNum = _coilLogic.CreateL25CoilRejectResult(tb);
                var insertOK = insertNum > 0;

                if (insertOK)
                    _coilLogic.CreateL25CoilRejectResultHis(tb);

                _log.I("存取鋼捲回退實績成功", $"存取鋼捲{returnCoilInfo.Coil_ID}回退實績");
                
                return insertOK;
            }
            catch (Exception e)
            {
                throw new Exception("存取鋼捲回退實績失敗" + e.ToString().CleanInvalidChar());
            }
        }


        public TBL_CoilRejectResult GetCoilRejectResult(string coilNo)
        {

            try
            {
                var coilReject = _coilLogic.GetCoilRejectResult(coilNo);
                _log.I("取得鋼捲回退實績", $"取得鋼捲{coilNo}回退實績 => {coilReject != null}");
                return coilReject;
            }
            catch (SqlException e)
            {
                _log.E("取得鋼捲回退實績失敗", e.Message.CleanInvalidChar());
                return null;
            }


        }
        public void SaveCoilWeldInDB(L1L2Rcv.Msg_302_CoilWeld msg)
        {
        }
        //public GenCoilInfoModel.GenPreset201LkTableInfo GetPreset201LkTableData(string stNo, float coilThickness, int tsStandAvr)
        public GenCoilInfoModel.GenPreset201LkTableInfo GetPreset201LkTableData(string stNo, float coilThickness)
        {
            var lk201 = new GenCoilInfoModel.GenPreset201LkTableInfo();
            //var materialGrade = string.Empty;
            var yieldStrength = 0.0f;

            _log.D("LUT 查表條件", $"stNo={stNo}, 厚度Key={coilThickness}, 屈服強度Key={yieldStrength}");

            //// 撈取鋼種
            //try
            //{
            //    materialGrade = _coilLogic.GetMatericalGrade(stNo);
            //}
            //catch (Exception e)
            //{
            //    _log.E("撈取鋼種等級失敗", e.Message.CleanInvalidChar());
            //    return lk201;
            //}

            //  撈取屈服強度
            try
            {
                var steelGrade = new string(stNo.Skip(1).Take(5).ToArray());

                _log.D("LUT 查表條件", $"steelGrade={steelGrade}");

                yieldStrength = _coilLogic.GetYieldStrength(steelGrade);
            }
            catch (Exception e)
            {
                _log.E("撈取屈服強度失敗", e.Message.CleanInvalidChar());
                return lk201;
            }

            // Flattener 只根據厚度區間查詢
            try
            {
                var flattener = _coilLogic.GetFlatterBySYandThick(coilThickness);
                if (flattener != null)
                {
                    lk201.FlatenerDepth1 = flattener.Intermesh_Num1;
                    lk201.FlatenerDepth2 = flattener.Intermesh_Num2;
                }

            }
            catch (Exception e)
            {
                _log.E("QueryFlatter", e.Message.CleanInvalidChar());
            }

            // LineTension 只根據厚度區間查詢
            try
            {
                var lineTension = _coilLogic.GetLineTensionByGradeAndThick(coilThickness);
                if (lineTension != null)
                {
                    //lk201.Tensionunitdepth = lineTension.UnitTension;
                    lk201.RecoilerTension = lineTension.TRTension;
                    lk201.UncoilerTension = lineTension.PORTension;
                }
            }
            catch (Exception e)
            {
                _log.E("QueryLineTens", e.Message.CleanInvalidChar());
            }

            // SideTrimmer
            try
            {
                var sideTrimmer = _coilLogic.GetLineSideTrimmerByYsAndThick(yieldStrength, coilThickness);
                if (sideTrimmer != null)
                {
                    lk201.SideTrimmerGap = sideTrimmer.KnifeGap;
                    lk201.SideTrimmerLap = sideTrimmer.KnifeLap;
                }
            }
            catch (Exception e)
            {
                _log.E("QueryLineSideTrimmer", e.Message.CleanInvalidChar());
            }

            // 三輥下壓量 只根據厚度區間查詢           
            try
            {
                var lineTension = _coilLogic.GetTensionUnitDepth(coilThickness);
                if (lineTension != null)
                    lk201.Tensionunitdepth = lineTension.TensionUnitDepth;

            }
            catch (Exception e)
            {
                _log.E("QueryTensionUnitDepth", e.Message.CleanInvalidChar());
            }


            return lk201;

        }



        public CoilCutRecordTemp GetCutRecordTempFromParentCoilID(string parentCoilID, string mode, CutTempSchema schema)
        {
            try
            {
                var tb = _coilLogic.GetCoilCutRecordTemp(parentCoilID, mode, schema);
                var res = _mapper.Map<CoilCutRecordTemp>(tb);
                _log.I("撈取CutRecrd成功", $"撈取{parentCoilID} CutRecord成功");
                return res;
            }
            catch (Exception e)
            {
                _log.E("撈取CutRecord", $"{parentCoilID} 撈取CutRecord失敗," + e.Message.CleanInvalidChar());
                return null;
            }
        }

        public CoilCutRecordTemp GetCutRecordTemp(string coilID)
        {
            try
            {
                var tb = _coilLogic.GetCoilCutRecordTemp(coilID);
                var res = _mapper.Map<CoilCutRecordTemp>(tb);
                _log.I("撈取CutRecrd成功", $"撈取{coilID}CutRecord成功");
                return res;
            }
            catch (Exception e)
            {
                _log.E("撈取CutRecord", $"{coilID} 撈取CutRecord失敗," + e.Message.CleanInvalidChar());
                return null;
            }
        }

        #endregion

        #region -- Defect Data --

        public bool VaildHasDefect(string planNo, string entryCoilID)
        {
            entryCoilID.VaildStrNullOrEmpty("entryCoilID", "是否有Defect查詢錯誤");

            try
            {
                var hasDefect = _coilLogic.VaildHasDefect(planNo, entryCoilID);
                _log.I("是否有Defect查詢成功", $"【鋼卷】: {entryCoilID}{hasDefect.ToHasStr()}Defect");
                return hasDefect;
            }
            catch (Exception e)
            {
                _log.E($"是否有Defect查詢成功，【鋼卷】{entryCoilID}", e.Message.CleanInvalidChar());
                return false;

            }
        }

        public bool CreateDefect(Msg_PDI msg)
        {
            msg.VaildObjectNull("msg", "新增Defect失敗");

            try
            {
                var tb = _mapper.Map<TBL_Coil_Defect>(msg);
                tb.Modify_UserID = "System";

                var insertNum = _coilLogic.CreateDefect(tb);
                var insertOK = insertNum > 0;
                _log.I($"新增Defect成功=>{insertOK}", $"新增{msg.EntryCoilNo}Defect成功");
                return insertOK;
            }
            catch (Exception e)
            {
                _log.E("新增Defect失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool DeleteDefect(string planNo, string coilID)
        {
            coilID.VaildStrNullOrEmpty("coilID", "刪除Defect資料失敗");

            try
            {
                var deleteNum = _coilLogic.DeleteDefect(planNo, coilID);
                var deleteOK = deleteNum > 0;
                _log.I("刪除Defect資料成功", $"刪除{coilID}Defect資料成功");
                return deleteOK;
            }
            catch (Exception e)
            {
                throw new Exception("刪除Defect資料失敗" + e.ToString().CleanInvalidChar());
            }
        }

        public DefectData GetDefect(string planNo, string coilID)
        {
            coilID.VaildStrNullOrEmpty("coilID", "撈取Defect資料失敗");

            try
            {
                var tb = _coilLogic.GetDefect(planNo, coilID);
                var res = _mapper.Map<DefectData>(tb);
                _log.I("撈取Defect資料成功", $"撈取{coilID}Defect資料成功");
                return res;

            }
            catch (Exception e)
            {
                throw new Exception($"撈取{coilID}Defect資料失敗" + e.ToString().CleanInvalidChar());
            }
        }

        /// <summary>
        /// 更新Defect
        /// </summary>
        /// <param name="msg">三級PDI資訊</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateDefect(Msg_PDI msg)
        {
            msg.VaildObjectNull("msg", "更新Defect資料失敗");
            var coilID = msg.Entry_Coil_No.ToStr();

            try
            {
                var tb = _mapper.Map<TBL_Coil_Defect>(msg);
                var updateNum = _coilLogic.UpdateDefect(tb.Plan_No, coilID, tb);
                var updateOK = updateNum > 0;
                _log.I($"更新Defect資料成功 => {updateOK}", $"更新{coilID}Defect資料成功");
                return updateOK;
            }
            catch (Exception e)
            {
                _log.E($"更新{coilID}Defect資料失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        #endregion

        #region -- 取樣 --

        public bool CreateSampleCoil(Msg_311_ExCoilCut msg, PDI pdi, string sampleCoilID)
        {
            msg.VaildObjectNull("msg", "存取鋼捲取樣失敗");
            pdi.VaildObjectNull("pdi", "存取鋼捲取樣失敗");
            sampleCoilID.VaildStrNullOrEmpty("sampleCoilID", "存取鋼捲取樣失敗");

            try
            {
                var msgMaptb = _mapper.Map<TBL_Sample>(msg);
                var tb = _mapper.Map(pdi, msgMaptb);

                tb.Sample_Position = sampleCoilID.Last().ToString();
                tb.Sample_ID = sampleCoilID;
                tb.SampleTime = DateTime.Now;
                tb.OriPDI_Out_Coil_ID = pdi.Out_Coil_ID;

                var insertNum = _coilLogic.CreateCoilSample(tb);
                var insertOK = insertNum > 0;
                return insertOK;
            }
            catch (Exception e)
            {
                _log.E("存取鋼捲取樣失敗", e.ToString().CleanInvalidChar());
                return false;
            }

        }


        public SampleCoil GetCoilSampleInfo(string planNo, int matSeqNo, string planSort, string sampleID)
        {
            planNo.VaildStrNullOrEmpty("planNo", "撈取取樣鋼捲資訊失敗");
            matSeqNo.VaildIntValueZero("matSeqNo", "撈取取樣鋼捲資訊失敗");
            planSort.VaildStrNullOrEmpty("planSort", "撈取取樣鋼捲資訊失敗");
            sampleID.VaildStrNullOrEmpty("sampleID", "撈取取樣鋼捲資訊失敗");

            try
            {
                var tb = _coilLogic.GetCoilSample(planNo, matSeqNo, planSort, sampleID);
                var res = _mapper.Map<SampleCoil>(tb);
                _log.I("撈取取樣鋼捲資訊成功", $"撈取取樣鋼捲{sampleID}資訊失敗");
                return res;
            }
            catch (Exception e)
            {
                throw new Exception("撈取取樣鋼捲資訊失敗" + e.ToString().CleanInvalidChar());
            }
        }

        public bool DeleteCoilSampleInfo(string planNo, int matSeqNo, string planSort, string sampleID)
        {
            planNo.VaildStrNullOrEmpty("planNo", "刪除取樣鋼捲資訊失敗");
            matSeqNo.VaildIntValueZero("matSeqNo", "刪除取樣鋼捲資訊失敗");
            planSort.VaildStrNullOrEmpty("planSort", "刪除取樣鋼捲資訊失敗");
            sampleID.VaildStrNullOrEmpty("sampleID", "刪除取樣鋼捲資訊失敗");

            try
            {
                var deleteNum = _coilLogic.DeleteCoilSample(planNo, matSeqNo, planSort, sampleID);
                var deleteOK = deleteNum > 0;
                _log.I("刪除取樣鋼捲資成功", $"刪除取樣鋼捲{sampleID}資訊失敗");
                return deleteOK;

            }
            catch (Exception e)
            {
                throw new Exception("刪除取樣鋼捲資訊失敗" + e.ToString().CleanInvalidChar());
            }
        }

        //public bool DeleteSampleCoil(string sampleID)
        //{

        //}

        #endregion

        #region -- 導帶 --

        public LedaerData GetLeaderData(string entryCoilID)
        {
            try
            {
                _log.I("撈取導帶資料", $"撈取{entryCoilID}導帶資料");
                var tb = _coilLogic.GetLeaderData(entryCoilID);
                var res = _mapper.Map<LedaerData>(tb);
                return res;

            }
            catch (Exception e)
            {
                throw new Exception("撈取導帶資料失敗" + e.ToString().CleanInvalidChar());
            }
        }


        #endregion

        #region Preset

        public bool CreatePresetRecord(Msg_201_Preset msg)
        {
            msg.VaildObjectNull("msg", "存取Preset失敗");

            try
            {
                var tb = msg.ToTblPresetRecordEntity();
                var hasData = _coilLogic.VaildHasPresetRecord(tb.Coil_ID);

                if (!hasData)
                {
                    var insertNum = _coilLogic.CreatePresetRecord(tb);
                    var insertOK = insertNum > 0;
                    _log.I($"新增Preset紀錄:{tb.Coil_ID}", $"【鋼捲】:{tb.Coil_ID} 新增Preset Record{(insertOK).ToStr()}");
                    return insertOK;
                }


                var updateNum = _coilLogic.UpdatePresetReocrd(tb);
                var updateOK = updateNum > 0;
                _log.I($"更新Preset紀錄:{tb.Coil_ID}", $"【鋼捲】:{tb.Coil_ID} 更新Preset Record{(updateOK).ToStr()}");
                return updateOK;

            }
            catch (Exception e)
            {
                _log.E($"新增Preset紀錄失敗:{msg.CoilIDNo}", e.ToString().CleanInvalidChar());
                return false;
            };
        }

        public bool CreateL25PresetRecord(Msg_201_Preset msg)
        {
            msg.VaildObjectNull("msg", "存取L25 Preset Record失敗");

            try
            {
                var tb = msg.ToL25PresetRecordEntity();
                var insertNum = _coilLogic.CreateL25PresetRecord(tb);
                var insertOK = insertNum > 0;
                _log.I($"新增L25 Preset紀錄:{tb.CoilID}", $"【鋼捲】:{tb.CoilID} 新增Preset Record{(insertOK).ToStr()}");

                insertNum = _coilLogic.CreateL25PresetRecordHis(tb);
                insertOK = insertNum > 0;
                _log.I($"新增L25 Preset歷史紀錄:{tb.CoilID}", $"【鋼捲】:{tb.CoilID} 新增Preset Record{(insertOK).ToStr()}");

                return insertOK;

            }
            catch (Exception e)
            {
                _log.E($"新增L25 Preset紀錄失敗:{msg.CoilIDNo}", e.ToString().CleanInvalidChar());
                return false;
            };
        }

        #endregion
    }
}
