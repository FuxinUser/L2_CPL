using Core.Define;
using DBService.Level25Repository.L2L25_CoilPDI;
using DBService.Level25Repository.L2L25_CoilPDO;
using DBService.Level25Repository.L2L25_CoilRejectResult;
using DBService.Level25Repository.L2L25_CPL1PRESET;
using DBService.Level25Repository.L2L25_L2APStatus;
using DBService.Repository;
using DBService.Repository.CoilCutReocrd;
using DBService.Repository.CoilScheduleDelete;
using DBService.Repository.DefectData;
using DBService.Repository.Leader;
using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblFlattener;
using DBService.Repository.LookupTblLineTension;
using DBService.Repository.LookupTblPaper;
using DBService.Repository.LookupTblSideTrimmer;
using DBService.Repository.LookupTblSideTrimmer1;
using DBService.Repository.LookupTblTensionUnitDepth;
using DBService.Repository.LookupTblYieldStrength;
using DBService.Repository.LookupTbSleeve;
using DBService.Repository.MaterialGrade;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using DBService.Repository.PresetRecord;
using DBService.Repository.Sample;
using DBService.Repository.ScheduleDelete_CoilReject_Record_Temp;
using DBService.Repository.UnmountRecord;
using DBService.Repository.WieldRecord;
using MsgConvert.EntityFactory;
using MsgStruct;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static Core.Define.DBParaDef;
using static DBService.Repository.APStatus.APStatusEntity;
using static DBService.Repository.CoilRejResultEntity;
using static DBService.Repository.CoilScheduleDelete.CoilScheduleDeleteEntity;
using static DBService.Repository.CoilScheduleEntity;
using static DBService.Repository.CutReocrd.CoilCutRecordTempEntity;
using static DBService.Repository.DefectData.CoilDefectDataEntity;
using static DBService.Repository.Leader.LeaderTempEntity;
using static DBService.Repository.PDI.CoilPDIEntity;
using static DBService.Repository.PDO.PDOEntity;
using static DBService.Repository.PDO.PDOUploadedReplyEntity;
using static DBService.Repository.Sample.SampleEntity;
using static DBService.Repository.ScheduleDelete_CoilReject_Record_Temp.ScheduleDeleteRecordTempEntity;
using static DBService.Repository.UnmountRecord.UnmountRecordEntity;

/**
 Author: ICSC Spyua
 Desc: Coil Pro BLL
 Date: 2019/12/26
*/

namespace BLL.Coil
{
    public class CoilProLogic
    {
        #region Table Repo 

        private CoilPDIRepo _coilPDIRepo;
        private CoilPDORepo _coilPDORepo;
        private ProductionScheduleRepo _coilScheduleRepo;
        private CoilRejectResultRepo _coilRejectRepo;
        private WeldRecordsRepo _weldRecordsRepo;
        private PDOUploadedReplyRepo _pdoUploadedReplyRepo;

        private MaterialGradeRepo _materialGradeRepo;
        private LkUpTableYieldStrengthRepo _lktbYieldStrengthRepo;
        private LkUpTableFlattenerRepo _lktblFlattenerRepo;
        private LkUpTableLineTensionRepo _lktblLineTensionRepo;
        private LkUpTableSlideTrimmerRepo _lktblSlideTrimmerRepo;
        private LkUpTablePaperRepo _lktblPaperRepo;
        private LkUpTableSleeveRepo _lktblSleeveRepo;
        private LkUpTableTensionUnitDepthRepo _lkUpTableTensionUnitDepthRepo;

        private CoilCutRecordTempRepo _coilCutRecordTempRepo;
        private CoilScheduleDeleteRepo _coilScheduleDeleteRepo;
        private SchDelCoilRejectRecordTempRepo _schDelCoilRejectRecordTempRepo;
        private DefectDataRepo _defectDataRepo;
        private CoilSampleRepo _coilSampleRepo;
        private UmountRecordRepo _umountRecordRepo;
        private LeaderTempRepo _leaderTempRepo;

        private PresetRecordRepo _presetRecordRepo;

        private L2L25_CoilPDIRepo _l2l25CoiPDIRepo;
        private L2L25_CoilPDORepo _l2l25_CoilPDORepo;
        private L2L25_CPLPRESETRepo _l2l25_CPLPRESETRepo;
        private L2L25_CoilRejectResultRepo _l2l25_CoilRejectResultRepo;

        private L2L25_CoilPDIRepo _l2l25CoiPDIHisRepo;
        private L2L25_CoilPDORepo _l2l25_CoilPDOHisRepo;
        private L2L25_CPLPRESETRepo _l2l25_CPLPRESETHisRepo;
        private L2L25_CoilRejectResultRepo _l2l25_CoilRejectResultHisRepo;


        #endregion

        public CoilProLogic()
        {
            _coilCutRecordTempRepo = new CoilCutRecordTempRepo(DBConn);
            _coilPDORepo = new CoilPDORepo(DBConn);
            _coilPDIRepo = new CoilPDIRepo(DBConn);
            _coilScheduleRepo = new ProductionScheduleRepo(DBConn);
            _coilRejectRepo = new CoilRejectResultRepo(DBConn);
            _weldRecordsRepo = new WeldRecordsRepo(DBConn);
            _pdoUploadedReplyRepo = new PDOUploadedReplyRepo(DBConn);

            _materialGradeRepo = new MaterialGradeRepo(DBConn);
            _lktbYieldStrengthRepo = new LkUpTableYieldStrengthRepo(DBConn);
            _lktblFlattenerRepo = new LkUpTableFlattenerRepo(DBConn);
            _lktblLineTensionRepo = new LkUpTableLineTensionRepo(DBConn);
            _lktblSlideTrimmerRepo = new LkUpTableSlideTrimmerRepo(DBConn);
            _lktblPaperRepo = new LkUpTablePaperRepo(DBConn);
            _lktblSleeveRepo = new LkUpTableSleeveRepo(DBConn);
            _lkUpTableTensionUnitDepthRepo = new LkUpTableTensionUnitDepthRepo(DBConn);
            _coilSampleRepo = new CoilSampleRepo(DBConn);

            _coilScheduleDeleteRepo = new CoilScheduleDeleteRepo(DBConn);
            _schDelCoilRejectRecordTempRepo = new SchDelCoilRejectRecordTempRepo(DBConn);
            _umountRecordRepo = new UmountRecordRepo(DBConn);
            _defectDataRepo = new DefectDataRepo(DBConn);
            _leaderTempRepo = new LeaderTempRepo(DBConn);

            _presetRecordRepo = new PresetRecordRepo(DBConn);

            // 2.5
            _l2l25CoiPDIRepo = new L2L25_CoilPDIRepo(Level2_5DBConn);
            _l2l25_CoilPDORepo = new L2L25_CoilPDORepo(Level2_5DBConn);
            _l2l25_CPLPRESETRepo = new L2L25_CPLPRESETRepo(Level2_5DBConn);
            _l2l25_CoilRejectResultRepo = new L2L25_CoilRejectResultRepo(Level2_5DBConn);

            _l2l25CoiPDIHisRepo = new L2L25_CoilPDIRepo(HisDBConn);
            _l2l25_CoilPDOHisRepo = new L2L25_CoilPDORepo(HisDBConn);
            _l2l25_CPLPRESETHisRepo = new L2L25_CPLPRESETRepo(HisDBConn);
            _l2l25_CoilRejectResultHisRepo = new L2L25_CoilRejectResultRepo(HisDBConn);
        }

        #region -- PDI邏輯 --

        public TBL_PDI GetPDI(string planNo, string coilID, PDISchema pdiSchema)
        {

            var conditionSchema = pdiSchema == PDISchema.EntryCoilID ? nameof(TBL_PDI.Entry_Coil_ID) : nameof(TBL_PDI.Out_Coil_ID);

            try
            {
                var whereStr = new StringBuilder();
                whereStr.Append($"{nameof(TBL_PDI.Plan_No)} = '{planNo}'");
                whereStr.Append(" AND ");
                whereStr.Append($"{conditionSchema} = '{coilID}'");
                return _coilPDIRepo.GetAll(whereStr.ToString()).OrderByDescending(pdi => pdi.CreateTime).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        public TBL_PDI GetPDI(string coilID, PDISchema pdiSchema)
        {

            var conditionSchema = pdiSchema == PDISchema.EntryCoilID ? nameof(TBL_PDI.Entry_Coil_ID) : nameof(TBL_PDI.Out_Coil_ID);

            try
            {
                // 改SQL?
                //var pdi = _coilPDIRepo.GetAll().Where(x => x.Out_Coil_ID.Equals(coilID)).FirstOrDefault();
                // return _coilPDIRepo.GetAll($"{conditionSchema} = '{coilID}'").FirstOrDefault();
                return _coilPDIRepo.GetAll($"{conditionSchema} = '{coilID}'").OrderByDescending(pdi => pdi.CreateTime).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        public int CreatePDI(TBL_PDI mmsPDI)
        {

            try
            {
                var insertNum = _coilPDIRepo.Insert(mmsPDI);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }
        public int UpdatePDI(string planNo, string coilID, TBL_PDI pdi)
        {

            try
            {
                var updateNum = _coilPDIRepo.Update(pdi, new string[] { planNo, coilID });
                return updateNum;
            }
            catch
            {
                throw;
            }

        }
        public int DeletePDIByEntryCoilID(string planNo, string coilID)
        {

            try
            {
                var deleteNum = _coilPDIRepo.Delete(new string[] { planNo, coilID });
                return deleteNum;
            }
            catch
            {
                throw;
            }

        }
        public bool VaildHasPDI(string planNo, string coilID)
        {
            return _coilPDIRepo.HasData(new string[] { planNo, coilID });
        }
        public string GetPDIPlanNoByEnCoilID(string entryCoilID)
        {
            try
            {
                return _coilPDIRepo.GetSpeicDataByEntryCoilID(nameof(CoilPDIEntity.TBL_PDI.Plan_No), entryCoilID);
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePDIEntryScanCoilInfo(string entryCoilNo, bool entryCoilIDChecked)
        {
            try
            {
                var isCheck = entryCoilIDChecked ? EventDef.CheckCoilNo : EventDef.UnCheckedCoilNo;

                var sql = new StringBuilder();
                sql.Append(" Update ");
                sql.Append($" {nameof(TBL_PDI)} ");
                sql.Append(" SET ");

                sql.Append($" {nameof(TBL_PDI.Entry_Coil_ID_Checked)} ");
                sql.Append($" = ");
                sql.Append($" '{isCheck}' ");

                sql.Append($" , ");
                sql.Append($" {nameof(TBL_PDI.Entry_Scaned_Coil_ID)} ");
                sql.Append($" = ");
                sql.Append($" '{entryCoilNo}' ");

                sql.Append($" , ");
                sql.Append($" {nameof(TBL_PDI.Entry_Scaned_Time)} ");
                sql.Append($" = ");
                sql.Append($" '{NowTime}' ");

                sql.Append(" WHERE ");
                sql.Append($" { nameof(TBL_PDI.CreateTime)} ");
                sql.Append($" = ");
                // 子查詢
                sql.Append(" ( ");
                sql.Append(" SELECT ");
                sql.Append(" Top(1) ");
                sql.Append($" {nameof(TBL_PDI.CreateTime)} ");
                sql.Append(" FROM ");
                sql.Append($" {nameof(TBL_PDI)} ");
                sql.Append(" WHERE ");
                sql.Append($" {nameof(TBL_PDI.Entry_Coil_ID)} ");
                sql.Append(" = ");
                sql.Append($" '{entryCoilNo}' ");
                sql.Append(" order by ");
                sql.Append($" {nameof(TBL_PDI.CreateTime)} ");
                sql.Append(" desc ");
                sql.Append(" ) ");

                return _coilPDIRepo.DBContext.Execute(sql.ToString());
                //var isCheck = entryCoilIDChecked ? EventDef.CheckCoilNo : EventDef.UnCheckedCoilNo;
                //return _coilPDIRepo.UpdateEntryScanCoilInfo(entryCoilNo, isCheck);
            }
            catch
            {
                throw;
            }
        }


        public int UpdatePDIStarTime(string entryCoilID, string nowTime)
        {
            try
            {

                var sql = new StringBuilder();
                sql.Append(" Update ");
                sql.Append($" {nameof(TBL_PDI)} ");
                sql.Append(" SET ");

                sql.Append($" {nameof(TBL_PDI.Start_Time)} ");
                sql.Append($" = ");
                sql.Append($" '{nowTime}' ");

                sql.Append(" WHERE ");
                sql.Append($" { nameof(TBL_PDI.CreateTime)} ");
                sql.Append($" = ");
                // 子查詢
                sql.Append(" ( ");
                sql.Append(" SELECT ");
                sql.Append(" Top(1) ");
                sql.Append($" {nameof(TBL_PDI.CreateTime)} ");
                sql.Append(" FROM ");
                sql.Append($" {nameof(TBL_PDI)} ");
                sql.Append(" WHERE ");
                sql.Append($" {nameof(TBL_PDI.Entry_Coil_ID)} ");
                sql.Append(" = ");
                sql.Append($" '{entryCoilID}' ");
                sql.Append(" order by ");
                sql.Append($" {nameof(TBL_PDI.CreateTime)} ");
                sql.Append(" desc ");
                sql.Append(" ) ");

                return _coilPDIRepo.DBContext.Execute(sql.ToString());


                //return _coilPDIRepo.UpdateStarTime(entryCoilID);
            }
            catch
            {
                throw;
            }
        }
        public int UpdatePDIFinishTime(string outCoilID, string nowTime)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append(" Update ");
                sql.Append($" {nameof(TBL_PDI)} ");
                sql.Append(" SET ");

                sql.Append($" {nameof(TBL_PDI.Finish_Time)} ");
                sql.Append($" = ");
                sql.Append($" '{nowTime}' ");

                sql.Append(" WHERE ");
                sql.Append($" { nameof(TBL_PDI.CreateTime)} ");
                sql.Append($" = ");
                // 子查詢
                sql.Append(" ( ");
                sql.Append(" SELECT ");
                sql.Append(" Top(1) ");
                sql.Append($" {nameof(TBL_PDI.CreateTime)} ");
                sql.Append(" FROM ");
                sql.Append($" {nameof(TBL_PDI)} ");
                sql.Append(" WHERE ");
                sql.Append($" {nameof(TBL_PDI.Out_Coil_ID)} ");
                sql.Append(" = ");
                sql.Append($" '{outCoilID}' ");
                sql.Append(" order by ");
                sql.Append($" {nameof(TBL_PDI.CreateTime)} ");
                sql.Append(" desc ");
                sql.Append(" ) ");

                return _coilPDIRepo.DBContext.Execute(sql.ToString());

                //return _coilPDIRepo.UpdateFinishTime(coilID);
            }
            catch
            {
                throw;
            }
        }
        public int UpdatePDIEntryTime(string entryCoilID, string nowTime)
        {
            try
            {

                var sql = new StringBuilder();
                sql.Append(" Update ");
                sql.Append($" {nameof(TBL_PDI)} ");
                sql.Append(" SET ");

                sql.Append($" {nameof(TBL_PDI.Entry_Arrive_Time)} ");
                sql.Append($" = ");
                sql.Append($" '{nowTime}' ");

                sql.Append(" WHERE ");
                sql.Append($" { nameof(TBL_PDI.CreateTime)} ");
                sql.Append($" = ");
                // 子查詢
                sql.Append(" ( ");
                sql.Append(" SELECT ");
                sql.Append(" Top(1) ");
                sql.Append($" {nameof(TBL_PDI.CreateTime)} ");
                sql.Append(" FROM ");
                sql.Append($" {nameof(TBL_PDI)} ");
                sql.Append(" WHERE ");
                sql.Append($" {nameof(TBL_PDI.Entry_Coil_ID)} ");
                sql.Append(" = ");
                sql.Append($" '{entryCoilID}' ");
                sql.Append(" order by ");
                sql.Append($" {nameof(TBL_PDI.CreateTime)} ");
                sql.Append(" desc ");
                sql.Append(" ) ");

                return _coilPDIRepo.UpdateEntryTime(entryCoilID);
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePDISampleFlag(bool isSample, string coilIDNo)
        {
            try
            {
                var done = isSample ? "1" : "0";
                return _coilPDIRepo.UpdateSampleFlag(done, coilIDNo);
            }
            catch
            {
                throw;
            }
        }
        public float CalculatePDICoilWeightByLength(float thick, float width, float length, float density)
        {
            try
            {
                var coilWeight = thick * 0.001 * width * 0.001 * length * density;  //密度 Kg/m^3
                return (float)coilWeight;
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25PDIHis(L2L25_CoilPDI tb)
        {

            try
            {
                return _l2l25CoiPDIHisRepo.Insert(tb);
            }
            catch
            {
                throw;
            }

        }


        public int CreateL25PDI(L2L25_CoilPDI tb)
        {

            try
            {
                return _l2l25CoiPDIRepo.Insert(tb);
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region  -- PDO 邏輯 -- 

        public int CreateL25PDOHis(L2L25_CoilPDO tb)
        {
            try
            {
                return _l2l25_CoilPDOHisRepo.Insert(tb);
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25PDO(L2L25_CoilPDO tb)
        {
            try
            {
                return _l2l25_CoilPDORepo.Insert(tb);
            }
            catch
            {
                throw;
            }
        }
        public TBL_PDO GetPDO(string planNo, string outMatNo, string finishTime = "")
        {
            try
            {



                var whereStr = new StringBuilder();
                whereStr.Append($"{nameof(TBL_PDO.Out_Coil_ID)} = '{outMatNo}'");
                whereStr.Append(" AND ");
                whereStr.Append($"{nameof(TBL_PDO.Plan_No)} = '{planNo}'");

                if (!finishTime.Equals(string.Empty))
                {
                    whereStr.Append(" AND ");
                    whereStr.Append($"{nameof(TBL_PDO.FinishTime)} = '{finishTime}'");
                }

                return _coilPDORepo.GetAll(whereStr.ToString()).OrderByDescending(pdo => pdo.FinishTime).FirstOrDefault();

                //return _coilPDORepo.Get(new string[] { planNo, outMatNo});
            }
            catch
            {
                throw;
            }
        }

        public TBL_PDO GetFinalPDO(string outMatNo)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{nameof(TBL_PDO.Out_Coil_ID)} = '{outMatNo}'");

                return _coilPDORepo.GetAll(stringBuilder.ToString()).OrderByDescending(pdo => pdo.FinishTime).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public TBL_PDO GetFinalPDO(string planNo, string outMatNo)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{nameof(TBL_PDO.Plan_No)} = '{planNo}'");
                stringBuilder.Append(" AND ");
                stringBuilder.Append($"{nameof(TBL_PDO.Out_Coil_ID)} = '{outMatNo}'");

                return _coilPDORepo.GetAll(stringBuilder.ToString()).OrderByDescending(pdo => pdo.FinishTime).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePDOIploadUserID(string planNo, string coilID, string uploadUserID)
        {
            try
            {
                return _coilPDORepo.UpdateUploadPDOUserID(planNo, coilID, uploadUserID);
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePDOIploadFlag(string planNo, string coilID, string uploadFlag)
        {
            try
            {
                return _coilPDORepo.UpdateUploadPDOCheck(planNo, coilID, uploadFlag);
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePDOExitCoilIDChecked(string exitCoilNo, string exitCoilIDChecked)
        {
            try
            {
                return _coilPDORepo.UpdateExitCoilIDChecked(exitCoilNo, exitCoilIDChecked);
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePDO(string planNo, string outCoilID, TBL_PDO pdo)
        {
            try
            {
                return _coilPDORepo.Update(pdo, new string[] { planNo, outCoilID });
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePDOExitCoilWt(string exitCoilNo, float coilPureWt, float coilGrossWt)
        {
            try
            {
                return _coilPDORepo.UpdatePDOCoilWt(exitCoilNo, coilPureWt, coilGrossWt);
            }
            catch
            {
                throw;
            }
        }

        public int CreatePDO(TBL_PDO pdo)
        {
            try
            {
                return _coilPDORepo.Insert(pdo);
            }
            catch
            {
                throw;
            }
        }

        public int CreatePDOUploadedReply(TBL_PDOUploadedReply pdoUploadedReply)
        {
            try
            {
                return _pdoUploadedReplyRepo.Insert(pdoUploadedReply);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region -- Schedule邏輯 --

        public IEnumerable<TBL_Production_Schedule> QueryTopSchedule(int number, string scheduleStatuts)
        {

            try
            {
                var sql = new StringBuilder();
                sql.Append(" Select ");
                sql.Append(" TOP ");
                sql.Append($" {number} * ");
                sql.Append(" FROM ");
                sql.Append($"{nameof(TBL_Production_Schedule)}");
                sql.Append($" Where {nameof(TBL_Production_Schedule.Schedule_Status)} = '{scheduleStatuts}' ");
                sql.Append($" Order by {nameof(TBL_Production_Schedule.Seq_No)} ASC ");
                return _coilScheduleRepo.DBContext.Query<TBL_Production_Schedule>(sql.ToString());
            }
            catch
            {
                throw;
            }
        }
        public List<string> QueryUnScheduleCoilIDs(int num)
        {
            try
            {
                var strBuilder = new StringBuilder();
                strBuilder.Append(" Select ");
                strBuilder.Append(" TOP ");
                strBuilder.Append($" {num} ");
                strBuilder.Append(" * ");

                strBuilder.Append(" FROM ");
                strBuilder.Append($" {nameof(TBL_Production_Schedule)} ");

                strBuilder.Append(" WHERE ");
                strBuilder.Append($" {nameof(TBL_Production_Schedule.Schedule_Status)} = '{CoilDef.NewCoil_Statuts}' ");
                strBuilder.Append(" OR ");
                strBuilder.Append($" {nameof(TBL_Production_Schedule.Schedule_Status)} = '{CoilDef.RequestEntryCoil_Statuts}' ");

                strBuilder.Append(" Order by ");
                strBuilder.Append($" {nameof(TBL_Production_Schedule.Seq_No)} ");
                strBuilder.Append(" ASC ");
                return _coilScheduleRepo.DBContext.Query<string>(strBuilder.ToString()).ToList();
            }
            catch
            {
                throw;
            }

        }
        public IEnumerable<TBL_Production_Schedule> QueryScheduleByPlanNo(string planNo)
        {
            var scheduleJoinTbl = "schedule";
            var pdiJoinTbl = "pdi";

            var sql = new StringBuilder();
            sql.Append($" Select ");
            sql.Append($" {scheduleJoinTbl}.*from {nameof(TBL_Production_Schedule)} {scheduleJoinTbl} ");
            sql.Append($" LEFT JOIN ");
            sql.Append($" {nameof(TBL_PDI)} {pdiJoinTbl} ");
            sql.Append($" on {scheduleJoinTbl}.{nameof(TBL_Production_Schedule.Coil_ID)} = {pdiJoinTbl}.{nameof(TBL_PDI.Entry_Coil_ID)} ");
            sql.Append($" Where {nameof(TBL_PDI.Plan_No)} = '{planNo}' ");

            try
            {
                var coilSchedules = _coilPDIRepo.DBContext.Query<TBL_Production_Schedule>(sql.ToString());
                //var pdis = _coilPDIRepo.GetAll($"{nameof(L3L2_PDI.Plan_No)} = '{planNo}'");
                return coilSchedules;
            }
            catch
            {
                throw;

            }
        }
        public TBL_Production_Schedule GetSchedule(string coilID)
        {
            try
            {
                var coilSchedule = _coilScheduleRepo.Get(new string[] { coilID });
                return coilSchedule;

            }
            catch
            {
                throw;
            }
        }
        public bool VaildHasSchedule(string coilID)
        {
            try
            {
                return _coilScheduleRepo.HasData(new string[] { coilID });
            }
            catch
            {
                throw;
            }
        }
        public int CreateSchedule(string coilid, short seqNo, string updateSource = "0")
        {
            var coilSchedule = new TBL_Production_Schedule
            {
                Coil_ID = coilid,
                Seq_No = seqNo,
                Seq_No_L3 = seqNo,
                Schedule_Status = CoilDef.NewCoil_Statuts,
                Update_Source = updateSource,
                UpdateTime = DateTime.Now,
            };

            try
            {
                return _coilScheduleRepo.Insert(coilSchedule);
            }
            catch
            {
                throw;
            }

        }
        public int GetScheduleTotalCnt()
        {
            try
            {
                //var sqlBuilder = new StringBuilder();
                //sqlBuilder.Append(" select ");
                //sqlBuilder.Append(" count(*) ");
                //sqlBuilder.Append(" FROM ");
                //sqlBuilder.Append($" {nameof(TBL_Production_Schedule)} ");
                //sqlBuilder.Append(" Where ");
                //sqlBuilder.Append($" {nameof(TBL_Production_Schedule.Schedule_Status)} = '{CoilDef.NewCoil_Statuts}' ");
                //sqlBuilder.Append(" OR ");
                //sqlBuilder.Append($" {nameof(TBL_Production_Schedule.Schedule_Status)} = '{CoilDef.RequestEntryCoil_Statuts}' ");

                //return _coilScheduleRepo.DBContext.ExecuteScalar<int>(sqlBuilder.ToString());
                return _coilScheduleRepo.GetTotalNum();
            }
            catch
            {
                throw;
            }
        }

        public int UpdateScheduleSeqNo(string coilNo, short seqNo)
        {
            try
            {
                return _coilScheduleRepo.UpdateScheduleSeqNo(coilNo, seqNo);
            }
            catch
            {
                throw;
            }
        }
        public int UpdateScheduleStatus(string coilNo, string statuts)
        {
            try
            {
                return _coilScheduleRepo.UpdateScheduleStatus(coilNo, statuts);
            }
            catch
            {
                throw;
            }
        }
        public int GetSeqNo(string coilID)
        {
            string sql = $"SELECT {nameof(TBL_Production_Schedule.Seq_No)} FROM {nameof(TBL_Production_Schedule)} WHERE {nameof(TBL_Production_Schedule.Coil_ID)} = '{coilID}'";

            try
            {
                var seqNo = _coilPDIRepo.DBContext.QuerySingleOrDefault<int>(sql);
                return seqNo;
            }
            catch
            {
                throw;
            }
        }

        public string GetScheduleStatuts(string coilID)
        {
            string sql = $"SELECT {nameof(TBL_Production_Schedule.Schedule_Status)} FROM {nameof(TBL_Production_Schedule)} WHERE {nameof(TBL_Production_Schedule.Coil_ID)} = '{coilID}'";

            try
            {
                var statuts = _coilPDIRepo.DBContext.QuerySingleOrDefault<string>(sql);
                return statuts;
            }
            catch
            {
                throw;
            }
        }


        public int GetL3SeqNo(string coilID)
        {
            string sql = $"SELECT {nameof(TBL_Production_Schedule.Seq_No_L3)} FROM {nameof(TBL_Production_Schedule)} WHERE {nameof(TBL_Production_Schedule.Coil_ID)} = '{coilID}'";

            try
            {
                var seqNo = _coilPDIRepo.DBContext.QuerySingleOrDefault<int>(sql);
                return seqNo;
            }
            catch
            {
                throw;
            }
        }

        public int DeleteCoildScheduleBySeqNo(int seqNo)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" Delete ");
            sqlBuilder.Append($"  {nameof(TBL_Production_Schedule)} ");
            sqlBuilder.Append(" where ");
            sqlBuilder.Append($" {nameof(TBL_Production_Schedule.Seq_No)} >= {seqNo} ");
            sqlBuilder.Append(" AND ");
            sqlBuilder.Append(" ( ");
            sqlBuilder.Append($" {nameof(TBL_Production_Schedule.Schedule_Status)} = '{CoilDef.NewCoil_Statuts}' ");
            sqlBuilder.Append(" OR ");
            sqlBuilder.Append($" {nameof(TBL_Production_Schedule.Schedule_Status)} = '{CoilDef.RequestEntryCoil_Statuts}' ");
            sqlBuilder.Append(" ) ");

            //string sql = $"DELETE  {nameof(TBL_Production_Schedule)} where {nameof(TBL_Production_Schedule.Seq_No)} >= {seqNo}";
            try
            {
                var deleteNum = _coilPDIRepo.DBContext.Execute(sqlBuilder.ToString());
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }

        public void DeleteAllSchedule()
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append(" TRUNCATE ");
                sql.Append(" TABLE ");
                sql.Append(nameof(TBL_Production_Schedule));
                _coilScheduleRepo.DBContext.Execute(sql.ToString());
            }
            catch
            {
                throw;
            }
        }

        public int DeleteAllIdleSchedule()
        {
            try
            {
                var sqlBuilder = new StringBuilder();
                sqlBuilder.Append(" DELETE ");
                sqlBuilder.Append(" FROM ");
                sqlBuilder.Append($" {nameof(TBL_Production_Schedule)}  ");
                sqlBuilder.Append(" Where ");
                sqlBuilder.Append($" {nameof(TBL_Production_Schedule.Schedule_Status)} = '{CoilDef.NewCoil_Statuts}' ");
                sqlBuilder.Append(" OR ");
                sqlBuilder.Append($" {nameof(TBL_Production_Schedule.Schedule_Status)} = '{CoilDef.RequestEntryCoil_Statuts}' ");


                //var deleteNum = _coilScheduleRepo.Delete(null);
                var deleteNum = _coilScheduleRepo.DBContext.Execute(sqlBuilder.ToString());
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }
        public int DeleteScheduleByCoilID(string coilID)
        {
            try
            {
                var delNum = _coilScheduleRepo.Delete(new string[] { coilID });
                return delNum;
            }
            catch
            {
                throw;
            }
        }
        public int DeleteBatchScheduleByPlanNo(string planNo)
        {
            var scheduleJoinTbl = "schedule";
            var pdiJoinTbl = "pdi";

            var sql = new StringBuilder();
            sql.Append($" Delete {scheduleJoinTbl}");
            sql.Append($" from {nameof(TBL_Production_Schedule)} {scheduleJoinTbl} ");
            sql.Append($" LEFT JOIN ");
            sql.Append($" {nameof(TBL_PDI)} {pdiJoinTbl} ");
            sql.Append($" on {scheduleJoinTbl}.{nameof(TBL_Production_Schedule.Coil_ID)} = {pdiJoinTbl}.{nameof(TBL_PDI.Entry_Coil_ID)} ");
            sql.Append($" Where {nameof(TBL_PDI.Plan_No)} = '{planNo}' ");

            try
            {
                var deleteNum = _coilScheduleRepo.DBContext.Execute(sql.ToString());
                //var pdis = _coilPDIRepo.GetAll($"{nameof(L3L2_PDI.Plan_No)} = '{planNo}'");
                return deleteNum;
            }
            catch
            {
                throw;

            }
        }
        public void CreateSchedules(DataTable dt)
        {
            try
            {
                _coilScheduleRepo.DBContext.SqlBulkCopy(dt, nameof(TBL_Production_Schedule));
            }
            catch
            {
                throw;
            }
        }
        public int CreateDelScheduleRecord(TBL_CoilScheduleDelete delDecord)
        {
            try
            {
                var insertNum = _coilScheduleDeleteRepo.Insert(delDecord);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }
        public int DeleteDelScheduleRecordByCoilID(string coilID)
        {
            try
            {
                var deleteNum = _coilScheduleDeleteRepo.Delete(new string[] { coilID });
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 更新排程跳軋/鋼捲退料暫存記錄
        /// </summary>
        /// <param name="tb">更新資料</param>
        /// <returns>更新成功與否</returns>
        public int UpdateSchDelCoilRejectRecordTemp(TBL_ScheduleDelete_CoilReject_Temp tb)
        {
            try
            {
                var coilID = tb.Coil_ID;
                var updateNum = _schDelCoilRejectRecordTempRepo.Update(tb, new string[] { coilID });
                return updateNum;
            }
            catch
            {
                throw;
            }
        }
        public bool VaildHasSchDelCoilRejectRecordTemp(string coilID)
        {
            try
            {
                //var sqlBuilder = new StringBuilder();
                //sqlBuilder.Append("Select");
                //sqlBuilder.Append(" * ");
                //sqlBuilder.Append(" FROM ");
                //sqlBuilder.Append($" {nameof(TBL_ScheduleDelete_CoilReject_Temp)} ");
                //sqlBuilder.Append(" Where ");
                //sqlBuilder.Append(nameof(TBL_ScheduleDelete_CoilReject_Temp.Coil_ID) + "=" + "'" + coilID + "'");
                //sqlBuilder.Append(" AND ");
                //sqlBuilder.Append("(");
                //sqlBuilder.Append(nameof(TBL_ScheduleDelete_CoilReject_Temp.Record_Type) + "=" + "'" + DBParaDef.CutModeSplitCut + "'");
                //sqlBuilder.Append(" OR ");
                //sqlBuilder.Append(nameof(TBL_ScheduleDelete_CoilReject_Temp.Record_Type) + "=" + "'" + DBParaDef.CutModeStripBreak + "'");
                //sqlBuilder.Append(")");
                //return _schDelCoilRejectRecordTempRepo.DBContext.Query<TBL_Coil_CutRecord_Temp>(sqlBuilder.ToString()).Count()>0;
                var tb = _schDelCoilRejectRecordTempRepo.Get(new string[] { coilID });
                return tb != null;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 存取排程跳軋/鋼捲退料暫存記錄
        /// </summary>
        /// <param name="tb">存取資料</param>
        /// <returns>存取成功與否</returns>
        public int CreateSchDelCoilRejectRecordTemp(TBL_ScheduleDelete_CoilReject_Temp tb)
        {
            try
            {
                var insertNum = _schDelCoilRejectRecordTempRepo.Insert(tb);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }
        public int DeleteSchDelCoilRejectTempRecordByCoilID(string coilID)
        {
            try
            {
                var insertNum = _schDelCoilRejectRecordTempRepo.Delete(new string[] { coilID });
                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        public TBL_ScheduleDelete_CoilReject_Temp GetDelScheduleTempRecord(string coilID)
        {
            try
            {

                var whereStr = new StringBuilder();
                whereStr.Append($"{nameof(TBL_ScheduleDelete_CoilReject_Temp.Coil_ID)} = '{coilID}'");

                return _schDelCoilRejectRecordTempRepo.GetAll(whereStr.ToString()).OrderByDescending(x => x.CreateTime).FirstOrDefault();

            }
            catch
            {
                throw;
            }
        }

        public TBL_ScheduleDelete_CoilReject_Temp GetDelScheduleTempRecordByOutCoilID(string outCoilID)
        {
            try
            {
                var record = _schDelCoilRejectRecordTempRepo.GetAll($"{nameof(TBL_ScheduleDelete_CoilReject_Temp.OriPDI_Out_Coil_ID)} = '{outCoilID}'").FirstOrDefault();
                return record;
            }
            catch
            {
                throw;
            }
        }

        public bool InvaildHasSchedule(string coilID)
        {
            return _coilScheduleRepo.HasData(new string[] { coilID });
        }

        #endregion

        #region --  Lok Up Table --

        // 鋼種大類
        public string GetMatericalGrade(string stNo)
        {
            try
            {
                var grade = _materialGradeRepo.Get(new string[] { stNo });

                return grade != null ? grade.Material_Grade : string.Empty;
            }
            catch
            {
                throw;
            }
        }

        //  屈服強度
        public float GetYieldStrength(string steelGrade)
        {
            try
            {
                var data = _lktbYieldStrengthRepo.Get(new string[] { steelGrade });

                return data != null ? data.YS : default;
            }
            catch
            {
                throw;
            }
        }

        // 整平機
        public LkUpTableFlattenerEntity.TBL_LookupTable_Flattener GetFlatterBySYandThick(float coilThickness)
        {
            try
            {
                var sql = new StringBuilder();

                sql.Append("Select");
                sql.Append(" * ");
                sql.Append(" FROM ");
                sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener));
                sql.Append(" Where ");
                sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Max) + ">=" + coilThickness);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Coil_Thickness_Min) + "<=" + coilThickness);
                //sql.Append(" and ");
                //sql.Append(nameof(LkUpTableFlattenerModel.TBL_LookupTable_Flattener.Strip_Yield_Stress_Max) + ">=" + stripYieldStress);
                //sql.Append(" and ");
                //sql.Append(nameof(LkUpTableFlattenerModel.TBL_LookupTable_Flattener.Strip_Yield_Stress_Min) + "<=" + stripYieldStress);
                //sql.Append(" and ");
                //sql.Append(nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener.Material_Grade) + "=" + $"'{matericalGrade}'");

                return _lktblFlattenerRepo.DBContext.Query<LkUpTableFlattenerEntity.TBL_LookupTable_Flattener>(sql.ToString()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        // 裁邊機
        public LkUpTableLineTensionEntity.TBL_LookupTable_LineTension GetLineTensionByGradeAndThick(float coilThickness)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append("Select");
                sql.Append(" * ");
                sql.Append(" FROM ");
                sql.Append(nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension));
                sql.Append(" Where ");
                sql.Append(nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max) + ">=" + coilThickness);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min) + "<=" + coilThickness);
                //sql.Append(" and ");
                //sql.Append(nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade) + "=" + $"'{matericalGrade}'");

                return _lktblLineTensionRepo.DBContext.Query<LkUpTableLineTensionEntity.TBL_LookupTable_LineTension>(sql.ToString()).FirstOrDefault();

            }
            catch
            {
                throw;
            }
        }
        //public LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer GetLineSideTrimmerByGradeAndThick(string matericalGrade, int tsStandAvr, float coilThickness)
        //{
        //    try
        //    {
        //        var sql = new StringBuilder();
        //        sql.Append("Select");
        //        sql.Append(" * ");
        //        sql.Append(" FROM ");
        //        sql.Append(nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer));
        //        sql.Append(" Where ");
        //        sql.Append(nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Max) + ">=" + coilThickness);
        //        sql.Append(" and ");
        //        sql.Append(nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Min) + "<=" + coilThickness);
        //        //sql.Append(" and ");
        //        ////sql.Append(nameof(LkUpTableSideTrimmerModel.TBL_LookupTable_SideTrimmer.SteelGrade) + "=" + "'GradeA'");
        //        //sql.Append(nameof(LkUpTableSideTrimmerModel.TBL_LookupTable_SideTrimmer.TS_STAND_MAX) + ">=" + tsStandAvr);
        //        //sql.Append(" and ");
        //        //sql.Append(nameof(LkUpTableSideTrimmerModel.TBL_LookupTable_SideTrimmer.TS_STAND_MIN) + "<=" + tsStandAvr);
        //        sql.Append(" and ");
        //        sql.Append(nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade) + "=" + $"'{matericalGrade}'");


        //        return _lktblSlideTrimmerRepo.DBContext.Query<LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer>(sql.ToString()).FirstOrDefault();

        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        public LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1 GetLineSideTrimmerByYsAndThick(float yieldStrength, float coilThickness)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append($"Select");
                sql.Append($" * ");
                sql.Append($" FROM ");
                sql.Append($"{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1)}");
                sql.Append($" Where ");
                sql.Append($"({nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Max)} >= {coilThickness}");
                sql.Append($" and ");
                sql.Append($"{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.Coil_Thickness_Min)} <= {coilThickness})");
                sql.Append($" and ");
                sql.Append($"({nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Max)} >= {yieldStrength}");
                sql.Append($" and ");
                sql.Append($"{nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1.YS_Min)} <= {yieldStrength})");

                return _lktblSlideTrimmerRepo.DBContext.Query<LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1>(sql.ToString()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        // 墊紙資料
        public LkUpTablePaperEntity.TBL_LookupTable_Paper GetPaperData(string Paper_Code)
        {
            try
            {
                return _lktblPaperRepo.Get(new string[] { Paper_Code });
            }
            catch
            {
                throw;
            }
        }
        // 套筒資料
        public LkUpTableSleeveEntity.TBL_LookupTable_Sleeve GetSleeveData(string Paper_Code)
        {
            try
            {
                return _lktblSleeveRepo.Get(new string[] { Paper_Code });
            }
            catch
            {
                throw;
            }
        }
        // 三輥下壓量
        public LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth GetTensionUnitDepth(float coilThickness)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append("Select");
                sql.Append(" * ");
                sql.Append(" FROM ");
                sql.Append(nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth));
                sql.Append(" Where ");
                sql.Append(nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Max) + ">=" + coilThickness);
                sql.Append(" and ");
                sql.Append(nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Coil_Thickness_Min) + "<=" + coilThickness);
                //sql.Append(" and ");
                ////sql.Append(nameof(LkUpTableSideTrimmerModel.TBL_LookupTable_SideTrimmer.SteelGrade) + "=" + "'GradeA'");
                //sql.Append(nameof(LkUpTableSideTrimmerModel.TBL_LookupTable_SideTrimmer.TS_STAND_MAX) + ">=" + tsStandAvr);
                //sql.Append(" and ");
                //sql.Append(nameof(LkUpTableSideTrimmerModel.TBL_LookupTable_SideTrimmer.TS_STAND_MIN) + "<=" + tsStandAvr);
                //sql.Append(" and ");
                //sql.Append(nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth.Material_Grade) + "=" + $"'{matericalGrade}'");


                return _lkUpTableTensionUnitDepthRepo.DBContext.Query<LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth>(sql.ToString()).FirstOrDefault();

            }
            catch
            {
                throw;
            }
        }



        #endregion

        #region -- 鋼捲分切,廢料 --

        public TBL_Coil_CutRecord_Temp GetCoilCutRecordTemp(string parentCoilID, string mode, CutTempSchema schema)
        {
            try
            {
                var conditionSchema = schema == CutTempSchema.OriPDI_Out_Coil_ID ? nameof(TBL_Coil_CutRecord_Temp.OriPDI_Out_Coil_ID) : nameof(TBL_Coil_CutRecord_Temp.In_Coil_ID);


                var whereStr = new StringBuilder();
                whereStr.Append($"{conditionSchema} = '{parentCoilID}'");
                whereStr.Append(" AND ");
                whereStr.Append($"{nameof(TBL_Coil_CutRecord_Temp.CutMode)} = '{mode}'");

                var tb = _coilCutRecordTempRepo.GetAll(whereStr.ToString())
                    .OrderByDescending(x => x.CreateTime)
                    .FirstOrDefault();
                return tb;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 撈取目前分切比數(分切 斷帶 退料紀錄)
        /// </summary>
        /// <param name="entryCoilID">In Coil ID</param>
        /// <returns></returns>
        public int GetParentCnt(string entryCoilID)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append("Select");
                sql.Append(" * ");
                sql.Append(" FROM ");
                sql.Append(nameof(TBL_Coil_CutRecord_Temp));
                sql.Append(" Where ");
                sql.Append(nameof(TBL_Coil_CutRecord_Temp.OriPDI_Out_Coil_ID) + "=" + "'" + entryCoilID + "'");
                sql.Append(" AND ");
                sql.Append("(");
                sql.Append(nameof(TBL_Coil_CutRecord_Temp.CutMode) + "=" + "'" + DBParaDef.CutModeSplitCut + "'");
                sql.Append(" OR ");
                sql.Append(nameof(TBL_Coil_CutRecord_Temp.CutMode) + "=" + "'" + DBParaDef.CutModeStripBreak + "'");
                sql.Append(" OR ");
                sql.Append(nameof(TBL_Coil_CutRecord_Temp.CutMode) + "=" + "'" + DBParaDef.CutModeReturnCoil + "'");
                sql.Append(")");
                return _coilCutRecordTempRepo.DBContext.Query<TBL_Coil_CutRecord_Temp>(sql.ToString()).Count();
            }
            catch
            {
                throw;
            }
        }

        public string SplitHotRolledCoilID(string parentCoilID, int childrenCoilCnt)
        {
            var pcoilStrBuilder = new StringBuilder(parentCoilID);
            var childNo = (childrenCoilCnt + 1).ToString();
           
            // 第一次分切
            if (parentCoilID[parentCoilID.Length - 2].Equals('0'))
            {
                pcoilStrBuilder.Remove(parentCoilID.Length - 2, 1);
                pcoilStrBuilder.Insert(parentCoilID.Length - 2, childNo);
                return pcoilStrBuilder.ToString();
            }
            // 第二次分切
            if (parentCoilID[parentCoilID.Length - 1].Equals('0'))
            {
                pcoilStrBuilder.Remove(parentCoilID.Length - 1, 1);
                pcoilStrBuilder.Insert(parentCoilID.Length - 1, childNo);
                return pcoilStrBuilder.ToString();
            }
            return string.Empty;

        }


        public string SplitColdRolledCoilID(string parentCoilID, int childrenCoilCnt)
        {
            var pcoilStrBuilder = new StringBuilder(parentCoilID);
            var childNo = (childrenCoilCnt + 1).ToString();
            // 第一次分切
            if (pcoilStrBuilder[parentCoilID.Length - 4].Equals('0'))
            {
                pcoilStrBuilder.Remove(parentCoilID.Length - 4, 1);
                pcoilStrBuilder.Insert(parentCoilID.Length - 4, childNo);
                return pcoilStrBuilder.ToString();
            }
            // 第二次分切
            if (pcoilStrBuilder[parentCoilID.Length - 3].Equals('0'))
            {
                pcoilStrBuilder.Remove(parentCoilID.Length - 3, 1);
                pcoilStrBuilder.Insert(parentCoilID.Length - 3, childNo);
                return pcoilStrBuilder.ToString();
            }
            // 第三次分切
            if (parentCoilID[parentCoilID.Length - 2].Equals('0'))
            {
                pcoilStrBuilder.Remove(parentCoilID.Length - 2, 1);
                pcoilStrBuilder.Insert(parentCoilID.Length - 2, childNo);
                return pcoilStrBuilder.ToString();
            }
            // 第四次分切
            if (parentCoilID[parentCoilID.Length - 1].Equals('0'))
            {
                pcoilStrBuilder.Remove(parentCoilID.Length - 1, 1);
                pcoilStrBuilder.Insert(parentCoilID.Length - 1, childNo);
                return pcoilStrBuilder.ToString();
            }
            return string.Empty;

        }
        /// <summary>
        /// 存取分切暫存紀錄
        /// </summary>
        /// <param name="tb">資料</param>
        /// <returns>存取成功與否</returns>
        public int CreateCoilCutRecordTemp(TBL_Coil_CutRecord_Temp tb)
        {
            try
            {
                var insertNum = _coilCutRecordTempRepo.Insert(tb);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 刪除分切暫存紀錄
        /// </summary>
        /// <param name="coilID">分切鋼卷號</param>
        /// <returns>刪除成功與否</returns>
        public int DeleteCutRecordTempByCoilID(string coilID)
        {
            try
            {
                var deleteNum = _coilCutRecordTempRepo.Delete(new string[] { coilID });
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 撈取分切暫存紀錄
        /// </summary>
        /// <param name="coilID">分切捲號</param>
        /// <returns>分切暫存紀錄</returns>
        public IEnumerable<TBL_Coil_CutRecord_Temp> QueryCutRecordsTempsByEntryCoilID(string coilID)
        {
            try
            {
                var tb = _coilCutRecordTempRepo.GetAll($"{nameof(TBL_Coil_CutRecord_Temp.In_Coil_ID)} = '{coilID}'");
                return tb;
            }
            catch
            {
                throw;
            }
        }
        public int DeleteUmountRecordByCoilID(string coilID)
        {
            try
            {
                var deleteNum = _umountRecordRepo.Delete(new string[] { coilID });
                return deleteNum;
            }
            catch
            {
                throw;
            }

        }

        public TBL_Coil_CutRecord_Temp GetCoilCutRecordTemp(string coilID)
        {
            try
            {
                var tb = _coilCutRecordTempRepo.GetAll($"{nameof(TBL_Coil_CutRecord_Temp.Coil_ID)} = '{coilID}'")
                    .OrderByDescending(x=>x.CreateTime)
                    .FirstOrDefault();
                return tb;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region -- 鋼捲斷帶 --
        public int SaveUmounRecord(TBL_UnmountRecord record)
        {

            try
            {
                var insertNum = _umountRecordRepo.Insert(record);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        public int DeleteUnountRecord(string coilID)
        {
            try
            {
                var deleteNum = _umountRecordRepo.Delete(new string[] { coilID });
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region -- 套筒墊紙同步處理 --
        public int CreateSleeveValue(MMSL2Rcv.Msg_Sleeve_Value_Synchronize sleeveValue)
        {
            var value = sleeveValue.ToTblLKSleeveEntity();
            try
            {
                var insertNum = _lktblSleeveRepo.Insert(value);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }
        public int UpdateSleeveValue(MMSL2Rcv.Msg_Sleeve_Value_Synchronize sleeveValue)
        {
            var value = sleeveValue.ToTblLKSleeveEntity();
            try
            {
                var updateNum = _lktblSleeveRepo.Update(value, new string[] { value.Sleeve_Code });
                return updateNum;
            }
            catch
            {
                throw;
            }
        }
        public int DeleteSleeveValueByCode(string code)
        {

            try
            {
                var deleteNum = _lktblSleeveRepo.Delete(new string[] { code });
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }
        public int CreatePaperValue(MMSL2Rcv.Msg_Paper_Value_Synchronize paperValue)
        {
            var value = paperValue.ToTblLKPaperEntity();
            try
            {
                var insertNum = _lktblPaperRepo.Insert(value);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }
        public int UpdatePaperValue(MMSL2Rcv.Msg_Paper_Value_Synchronize paperValue)
        {
            var value = paperValue.ToTblLKPaperEntity();
            try
            {
                var updateNum = _lktblPaperRepo.Update(value, new string[] { value.Paper_Code });
                return updateNum;
            }
            catch
            {
                throw;
            }
        }
        public int DeletePaperValueByCode(string code)
        {

            try
            {
                var deleteNum = _lktblPaperRepo.Delete(new string[] { code });
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region -- Defect -- 

        public bool VaildHasDefect(string planNo, string coilID)
        {
            return _defectDataRepo.HasData(new string[] { planNo, coilID });
        }


        public TBL_Coil_Defect GetDefect(string planNo, string coilID)
        {
            try
            {
                //return _defectDataRepo.Get(new string[] { planNo, coilID });
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{nameof(TBL_Coil_Defect.Coil_ID)} = '{coilID}'");
                stringBuilder.Append(" AND ");
                stringBuilder.Append($"{nameof(TBL_Coil_Defect.Plan_No)} = '{planNo}'");
                //return _defectDataRepo.GetAll($"{nameof(TBL_Coil_Defect.Coil_ID)} = '{coilID}'").FirstOrDefault();
                return _defectDataRepo.GetAll(stringBuilder.ToString()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        public int CreateDefect(TBL_Coil_Defect defect)
        {
            try
            {
                defect.ModifyTime = DateTime.Now;
                return _defectDataRepo.Insert(defect);
            }
            catch
            {
                throw;
            }
        }
        public int DeleteDefect(string planNo, string coilID)
        {
            try
            {
                return _defectDataRepo.Delete(new string[] { planNo, coilID });
            }
            catch
            {
                throw;
            }
        }
        public int UpdateDefect(string planNo, string coilID, TBL_Coil_Defect defect)
        {
            try
            {
                defect.ModifyTime = DateTime.Now;
                return _defectDataRepo.Update(defect, new string[] { planNo, coilID });
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region -- 回退 --
        public TBL_CoilRejectResult GetCoilRejectResult(string coilNo)
        {
            try
            {
                var coilReject = _coilRejectRepo.Get(new string[] { coilNo });
                return coilReject;

            }
            catch
            {
                throw;
            }
        }
        public int CreateCoilReject(TBL_CoilRejectResult tb)
        {
            try
            {
                return _coilRejectRepo.Insert(tb);
            }
            catch
            {
                throw;
            }
        }
        public int DeleteCoilRejectResultByCoilID(string coilID)
        {
            try
            {
                return _coilRejectRepo.Delete(new string[] { coilID });
            }
            catch
            {
                throw;
            }
        }
        public int CreateL25CoilRejectResult(L2L25_CoilRejectResult tb)
        {
            try
            {
                return _l2l25_CoilRejectResultRepo.Insert(tb);
            }
            catch
            {
                throw;
            }
        }
        public int CreateL25CoilRejectResultHis(L2L25_CoilRejectResult tb)
        {
            try
            {
                return _l2l25_CoilRejectResultHisRepo.Insert(tb);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region -- 取樣 --        
        public int CreateCoilSample(TBL_Sample tbData)
        {
            try
            {
                return _coilSampleRepo.Insert(tbData);
            }
            catch
            {
                throw;
            }
        }
        public TBL_Sample GetCoilSample(string planNo, int matSeqNo, string planSort, string sampleID)
        {
            try
            {
                return _coilSampleRepo.Get(new string[] { planNo, matSeqNo.ToString(), planSort, sampleID });
            }
            catch
            {
                throw;
            }
        }

        public int DeleteCoilSample(string planNo, int matSeqNo, string planSort, string sampleID)
        {
            try
            {
                return _coilSampleRepo.Delete(new string[] { planNo, matSeqNo.ToString(), planSort, sampleID });
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region -- 導帶 --


        public TBL_Leader_Temp GetLeaderData(string entryCoilID)
        {
            try
            {
                var tb = _leaderTempRepo.GetAll().Where(x => x.Coil_ID.Equals(entryCoilID)).FirstOrDefault();
                return tb;
            }
            catch
            {
                throw;
            }

        }

        public int DeleteLeaderTempByCoilID(string coilID)
        {
            try
            {
                return _leaderTempRepo.Delete(new string[] { coilID });
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Preset

        public int CreateL25PresetRecordHis(L2L25_CPLPRESET tb)
        {
            try
            {
                return _l2l25_CPLPRESETHisRepo.Insert(tb);
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25PresetRecord(L2L25_CPLPRESET tb)
        {
            try
            {
                return _l2l25_CPLPRESETRepo.Insert(tb);
            }
            catch
            {
                throw;
            }
        }

        public int CreatePresetRecord(PresetRecordEntity.TBL_PresetRecord tb)
        {
            try
            {
                return _presetRecordRepo.Insert(tb);
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePresetReocrd(PresetRecordEntity.TBL_PresetRecord tb)
        {
            try
            {
                return _presetRecordRepo.Update(tb, new string[] { tb.Coil_ID });
            }
            catch
            {
                throw;
            }
        }

        public bool VaildHasPresetRecord(string coilID)
        {
            try
            {
                return _presetRecordRepo.HasData(new string[] { coilID });
            }
            catch
            {
                throw;
            }
        }

        #endregion

    }
}
