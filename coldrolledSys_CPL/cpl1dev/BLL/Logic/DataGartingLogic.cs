using Core.Define;
using DBService.Level25Repository.L2L25_Alive;
using DBService.Level25Repository.L2L25_DownTime;
using DBService.Level25Repository.L2L25_ENGC;
using DBService.Level25Repository.L2L25_RECCurrentCT;
using DBService.Level25Repository.L2L25_RECTensionCT;
using DBService.Level25Repository.L2L25_SpeedCT;
using DBService.Level25Repository.L2L25_UNCCurrentCT;
using DBService.Level25Repository.L2L25_UNCTensionCT;
using DBService.Level25Repository.L2L25_WeldCT;
using DBService.Repository.LineFaultRecords;
using DBService.Repository.LineStatus;
using DBService.Repository.ProcessDataCT;
using DBService.Repository.SideTrimmerTmp;
using DBService.Repository.Utility;
using DBService.Repository.WieldRecord;
using DBService.Repository.WorkSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DBService.Repository.LineFaultRecords.LineFaultRecordsEntity;
using static DBService.Repository.LineStatus.ProcessDataEntity;
using static DBService.Repository.ProcessDataCT.ProcessDataCTEntity;
using static DBService.Repository.SideTrimmerTmp.SideTrimmerTmpEntity;
using static DBService.Repository.Utility.UtilityEntity;
using static DBService.Repository.WieldRecord.WeldRecordEntity;
using static DBService.Repository.WorkSchedule.WorkScheduleEntity;

namespace BLL.Logic
{
    public class DataGartingLogic
    {
        private WeldRecordsRepo _weldRecordRepo;
        private ProcessDataRepo _processDataRepo;
        private UtilityRepo _utilityRepo;
       
        private LineFaultRecordsRepo _lineFaultRecordsRepo;
        private WorkScheduleRepo _workScheduleRepo;
        private ProcessDataCTRepo _processDataCTRepo;
        private SideTrimmerTmpRepo _sideTrimmerTmpRepo;
      

        private L2L25_DownTimeRepo _l2l25_DownTimeRepo;
        private L2L25_ENGCRepo _l2l25EngcRepo;
        private L2L25_SpeedCTRepo _l2l25SpeedCTRepo;
        private L2L25_UNCCurrentCTRepo _l2l25UNCCurrentCTRepo;
        private L2L25_UNCTensionCTRepo _l2l25UNCTensionCTRepo;
        private L2L25_RECCurrentCTRepo _l2l25RECCurrentCTRepo;
        private L2L25_RECTensionCTRepo _l2l25RECTensionCTRepo;
        private L2L25_WeldCTRepo _l2l25WeldCTRepo;
        private L2L25_AliveRepo _l2l25AliveRepo;

        private L2L25_DownTimeRepo _l2l25_DownTimeHisRepo;
        private L2L25_ENGCRepo _l2l25EngcHisRepo;
        private L2L25_SpeedCTRepo _l2l25SpeedCTHisRepo;
        private L2L25_UNCCurrentCTRepo _l2l25UNCCurrentCTHisRepo;
        private L2L25_UNCTensionCTRepo _l2l25UNCTensionCTHisRepo;
        private L2L25_RECCurrentCTRepo _l2l25RECCurrentCTHisRepo;
        private L2L25_RECTensionCTRepo _l2l25RECTensionCTHisRepo;
        private L2L25_WeldCTRepo _l2l25WeldCTHisRepo;
        private L2L25_AliveRepo _l2l25AliveHisRepo;





        public DataGartingLogic()
        {
           
            _processDataRepo = new ProcessDataRepo(DBParaDef.DBConn);
            _weldRecordRepo = new WeldRecordsRepo(DBParaDef.DBConn);       
            _lineFaultRecordsRepo = new LineFaultRecordsRepo(DBParaDef.DBConn);
            _workScheduleRepo = new WorkScheduleRepo(DBParaDef.DBConn);
            _utilityRepo = new UtilityRepo(DBParaDef.DBConn);
            _processDataCTRepo = new ProcessDataCTRepo(DBParaDef.DBConn);
            _sideTrimmerTmpRepo = new SideTrimmerTmpRepo(DBParaDef.DBConn);

            _l2l25_DownTimeRepo = new L2L25_DownTimeRepo(DBParaDef.Level2_5DBConn);
            _l2l25EngcRepo = new L2L25_ENGCRepo(DBParaDef.Level2_5DBConn);

            _l2l25RECCurrentCTRepo = new L2L25_RECCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _l2l25RECTensionCTRepo = new L2L25_RECTensionCTRepo(DBParaDef.Level2_5DBConn);
            _l2l25SpeedCTRepo = new L2L25_SpeedCTRepo(DBParaDef.Level2_5DBConn);
            _l2l25UNCCurrentCTRepo = new L2L25_UNCCurrentCTRepo(DBParaDef.Level2_5DBConn);
            _l2l25UNCTensionCTRepo = new L2L25_UNCTensionCTRepo(DBParaDef.Level2_5DBConn);
            _l2l25WeldCTRepo = new L2L25_WeldCTRepo(DBParaDef.Level2_5DBConn);
            _l2l25AliveRepo = new L2L25_AliveRepo(DBParaDef.Level2_5DBConn);

            _l2l25RECCurrentCTHisRepo = new L2L25_RECCurrentCTRepo(DBParaDef.HisDBConn);
            _l2l25RECTensionCTHisRepo = new L2L25_RECTensionCTRepo(DBParaDef.HisDBConn);
            _l2l25SpeedCTHisRepo = new L2L25_SpeedCTRepo(DBParaDef.HisDBConn);
            _l2l25UNCCurrentCTHisRepo = new L2L25_UNCCurrentCTRepo(DBParaDef.HisDBConn);
            _l2l25UNCTensionCTHisRepo = new L2L25_UNCTensionCTRepo(DBParaDef.HisDBConn);
            _l2l25WeldCTHisRepo = new L2L25_WeldCTRepo(DBParaDef.HisDBConn);
            _l2l25AliveHisRepo = new L2L25_AliveRepo(DBParaDef.HisDBConn);
            _l2l25_DownTimeHisRepo = new L2L25_DownTimeRepo(DBParaDef.HisDBConn);
            _l2l25EngcHisRepo = new L2L25_ENGCRepo(DBParaDef.HisDBConn);
        }

        public int CreateCoilWeldRecord(TBL_WeldRecords entity)
        {
            try
            {         
                var insertNum = _weldRecordRepo.Insert(entity);
                return insertNum;
            }catch
            {
                throw;
            }
            
        }

        public int DeleteCoilWeldRecord(string coilID)
        {
            try
            {
                var deleteNum = _weldRecordRepo.Delete(new string[] { coilID });
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }

        public int CreateProcessData(TBL_ProcessData entity)
        {

            try
            {        
                var insertNum = _processDataRepo.Insert(entity);
                return insertNum;
            }
            catch
            {   
                throw;
            }

        }

        public IEnumerable<TBL_ProcessData> QueryProcessData(string starTime, string endTime)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ");
            strBuilder.Append(" * ");
            strBuilder.Append(" FROM ");
            strBuilder.Append($" {nameof(TBL_ProcessData)} ");
            strBuilder.Append(" Where ");
            strBuilder.Append($" {nameof(TBL_ProcessData.ReceiveTime)} ");
            strBuilder.Append(" Between ");
            strBuilder.Append($" '{starTime}' ");
            strBuilder.Append(" AND ");
            strBuilder.Append($" '{endTime}' ");
            strBuilder.Append(" AND ");
            strBuilder.Append($" {nameof(TBL_ProcessData.BuildTension)} = 1 ");
            strBuilder.Append(" Order by ");
            strBuilder.Append($" {nameof(TBL_ProcessData.ReceiveTime)} ");
            strBuilder.Append(" ASC ");

            var entitys = _processDataRepo.DBContext.Query<TBL_ProcessData>(strBuilder.ToString());
            return entitys;
        }

        public IEnumerable<TBL_ProcessData> QueryProcessDataWeld(string starTime, string endTime)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ");
            strBuilder.Append(" * ");
            strBuilder.Append(" FROM ");
            strBuilder.Append($" {nameof(TBL_ProcessData)} ");
            strBuilder.Append(" Where ");
            strBuilder.Append($" {nameof(TBL_ProcessData.ReceiveTime)} ");
            strBuilder.Append(" Between ");
            strBuilder.Append($" '{starTime}' ");
            strBuilder.Append(" AND ");
            strBuilder.Append($" '{endTime}' ");
            strBuilder.Append(" Order by ");
            strBuilder.Append($" {nameof(TBL_ProcessData.ReceiveTime)} ");
            strBuilder.Append(" ASC ");

            var entitys = _processDataRepo.DBContext.Query<TBL_ProcessData>(strBuilder.ToString());
            return entitys;
        }

        public IEnumerable<TBL_SideTrimmerTmp> QuerySiderTrimmerTmp(string starTime, string endTime)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append(" SELECT ");
            strBuilder.Append(" * ");
            strBuilder.Append(" FROM ");
            strBuilder.Append($" {nameof(TBL_SideTrimmerTmp)} ");
            strBuilder.Append(" Where ");
            strBuilder.Append($" {nameof(TBL_SideTrimmerTmp.ReceiveTime)} ");
            strBuilder.Append(" Between ");
            strBuilder.Append($" '{starTime}' ");
            strBuilder.Append(" AND ");
            strBuilder.Append($" '{endTime}' ");
            strBuilder.Append(" Order by ");
            strBuilder.Append($" {nameof(TBL_SideTrimmerTmp.ReceiveTime)} ");
            strBuilder.Append(" ASC ");

            var entitys = _sideTrimmerTmpRepo.DBContext.Query<TBL_SideTrimmerTmp>(strBuilder.ToString());
            return entitys;
        }

        public int DeleteProcessDataByRecTime(string time)
        {
            try
            {
                var deleteNum = _processDataRepo.Delete(new string[] { time });
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }


        public int DeleteSiderTrimmerByRecTime(string time)
        {
            try
            {
                var deleteNum = _sideTrimmerTmpRepo.Delete(new string[] { time });
                return deleteNum;
            }
            catch
            {
                throw;
            }
        }

        public int CreateUtility(TBL_Utility entity)
        {
            try
            {       
                var insertNum = _utilityRepo.Insert(entity);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }


     
        public int CreateLineFaultRecord(TBL_LineFaultRecords entity)
        {
            try
            {             
                var insertNum = _lineFaultRecordsRepo.Insert(entity);
                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<TBL_WeldRecords> GetWeldRecords(string coilIDNo)
        {
            try
            {
                var data = _weldRecordRepo.GetAll($"{nameof(TBL_WeldRecords.Coil_ID)} = '{coilIDNo}'");
                return data;
            }
            catch
            {
                throw;
            }
        }

        public TBL_WorkSchedule GetScheduleByTime(DateTime time, string sqlOperator = "")
        {

            try
            {
                //var sql = new StringBuilder();
                //sql.Append("SELECT * ");
                //sql.Append(" FROM ");
                //sql.Append(nameof(TBL_WorkSchedule));
                //sql.Append(" Where ");
                //sql.Append($" {nameof(TBL_WorkSchedule.ShiftDate)} = '{time:yyyyMMdd}'");
                //sql.Append(" And ");
                //sql.Append($" {nameof(TBL_WorkSchedule.ShiftStartTime)} <= '{time:HH:mm}'");
                //sql.Append(" And ");
                //sql.Append($" {nameof(TBL_WorkSchedule.ShiftEndTime)} >= '{time:HH:mm}'");

                var sql = new StringBuilder();
                sql.Append($"SELECT * FROM {nameof(TBL_WorkSchedule)} ");
                sql.Append($"Where ");
                sql.Append($"{nameof(TBL_WorkSchedule.ShiftDate)} = '{time:yyyyMMdd}' ");

                if (!string.IsNullOrEmpty(sqlOperator))
                {
                    sql.Append($"And ");
                    sql.Append($"{nameof(TBL_WorkSchedule.ShiftStartTime)} {sqlOperator} '{time:HH:mm}' ");
                }
                
                sql.Append($"And ");
                sql.Append($"{nameof(TBL_WorkSchedule.ShiftStartTime)} != '' ");
                sql.Append($"Order by ");
                sql.Append($"{nameof(TBL_WorkSchedule.ShiftStartTime)} Desc ");

                var data = _workScheduleRepo.DBContext.Query<TBL_WorkSchedule>(sql.ToString()).FirstOrDefault();
                return data;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<TBL_WorkSchedule> GetSchedulesByTime(DateTime time, DateTime nextTime)
        {
            try
            {
                var data = _workScheduleRepo.GetAll(
                        $"{nameof(TBL_WorkSchedule.ShiftStartTime)} != '' And " +
                        $"{nameof(TBL_WorkSchedule.ShiftDate)} In ('{time:yyyyMMdd}', '{nextTime:yyyyMMdd}') ");
                return data;
            }
            catch
            {
                throw;
            }
        }

        // LineFault相關
        public TBL_LineFaultRecords GetLineFaultLastRecord()
        {

            try
            {
                var sql = new StringBuilder();
                sql.Append($" Select Top (1) * From {nameof(TBL_LineFaultRecords)} ");
                //sql.Append($" Where {nameof(TBL_LineFaultRecords.stop_end_time)} is Null ");
                sql.Append($" Where {nameof(TBL_LineFaultRecords.stop_end_time)} = '1970/01/01 00:00:00' ");
                sql.Append($" Order by {nameof(TBL_LineFaultRecords.CreateTime)} DESC ");
                var data = _lineFaultRecordsRepo.DBContext.QueryFirstOrDefault<TBL_LineFaultRecords>(sql.ToString());
                return data;
            }
            catch
            {
                throw;
            }
        }
        public TBL_LineFaultRecords GetLineFaultRecord(string prodTime, string stopStartTime)
        {
            try
            {
                return _lineFaultRecordsRepo.Get(new string[] { prodTime, stopStartTime });
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prodTime"> yyyy-MM-dd </param>
        /// <param name="endStarTime">yyyy-MM-dd HH:mm:ss</param>
        /// <param name="uploadStatuts">1:UploadDone 0:UDㄟUpload</param>
        /// <returns></returns>
        public int UpdateLineFaultUploadFlag(string prodTime, string endStarTime, string uploadStatuts)
        {
            try
            {
                var tbField = new
                {
                    UploadMMS = uploadStatuts,
               };

                return _lineFaultRecordsRepo.UpdateField(prodTime, endStarTime, tbField);
            }
            catch
            {
                throw;
            }
        }


        public int UpdateFaultRecord(TBL_LineFaultRecords entity)
        {
            try
            {
                var whereCondition = new string[] { entity.prod_time.ToString("yyyy-MM-dd"),
                                                    entity.stop_start_time.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                                    };

                var insertNum = _lineFaultRecordsRepo.Update(entity, whereCondition);

                return insertNum;
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25Engc(L2L25_ENGC entity)
        {
            try
            {
                return _l2l25EngcRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }


        public int CreateSideTrimmerTmp(TBL_SideTrimmerTmp entity)
        {
            try
            {
                return _sideTrimmerTmpRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
           
        }

        public int DeleteAllSideTrimmerTmp()
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append(" TRUNCATE ");
                sql.Append(" TABLE ");
                sql.Append(nameof(TBL_SideTrimmerTmp));
                return _sideTrimmerTmpRepo.DBContext.Execute(sql.ToString());
            }
            catch
            {
                throw;
            }

        }
        public int CreateL25EngcHis(L2L25_ENGC entity)
        {
            try
            {
                return _l2l25EngcHisRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }


        public int CreateL25Alive(L2L25_Alive entity)
        {
            try
            {
                return _l2l25AliveRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25AliveHis(L2L25_Alive entity)
        {
            try
            {
                return _l2l25AliveHisRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25DownTime(L2L25_DownTime entity)
        {
            try
            {
                return _l2l25_DownTimeRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25DownTimeHis(L2L25_DownTime entity)
        {
            try
            {
                return _l2l25_DownTimeHisRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }
    
        public int Create25SpeedCT(L2L25_SpeedCT entity)
        {
            try
            {
                return _l2l25SpeedCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int Create25SpeedCTHis(L2L25_SpeedCT entity)
        {
            try
            {
                return _l2l25SpeedCTHisRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int Create25UNCTenstionCT(L2L25_UNCTensionCT entity)
        {
            try
            {
                return _l2l25UNCTensionCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int Create25UNCTenstionCTHis(L2L25_UNCTensionCT entity)
        {
            try
            {
                return _l2l25UNCTensionCTHisRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int Create25UNCurrentCT(L2L25_UNCCurrentCT entity)
        {
            try
            {
                return _l2l25UNCCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int Create25UNCurrentCTHis(L2L25_UNCCurrentCT entity)
        {
            try
            {
                return _l2l25UNCCurrentCTHisRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25RECCurrentCT(L2L25_RECCurrentCT entity)
        {
            try
            {
                return _l2l25RECCurrentCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25RECCurrentCTHis(L2L25_RECCurrentCT entity)
        {
            try
            {
                return _l2l25RECCurrentCTHisRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25RECTensionCT(L2L25_RECTensionCT entity)
        {
            

            try
            {
                return _l2l25RECTensionCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int CreateL25RECTensionCTHis(L2L25_RECTensionCT entity)
        {


            try
            {
                return _l2l25RECTensionCTHisRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int Create25WeldCT(L2L25_WeldCT entity)
        {
            try
            {
                return _l2l25WeldCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int Create25WeldCTHis(L2L25_WeldCT entity)
        {
            try
            {
                return _l2l25WeldCTHisRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int CreateProcessDataCT(TBL_ProcessDataCT entity)
        {
            try
            {
                return _processDataCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

        public int CreateProcessDataCTHis(TBL_ProcessDataCT entity)
        {
            try
            {
                return _processDataCTRepo.Insert(entity);
            }
            catch
            {
                throw;
            }
        }

    }
}
