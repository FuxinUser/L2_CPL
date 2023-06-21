using System;
using System.Data.SqlClient;
using AutoMapper;
using BLL.Logic;
using Core.Define;
using Core.Util;
using LogSender;
using MsgConvert.ObjectMapping;
using MsgConvert.EntityFactory;
using MsgStruct;
using static DataMod.Response.RespnseModel;
using static DBService.Repository.LineFaultRecords.LineFaultRecordsEntity;
using static DBService.Repository.LineStatus.ProcessDataEntity;
using static DBService.Repository.WieldRecord.WeldRecordEntity;
using System.Collections.Generic;
using DataMod.Common;
using System.Linq;
using static Core.Define.L25SysDef;
using DBService.Level25Repository.L2L25_Alive;
using static MsgStruct.L1L2Rcv;
using static DBService.Repository.SideTrimmerTmp.SideTrimmerTmpEntity;
using System.Data.SqlTypes;
using DBService.Level25Repository.L2L25_WeldCT;

namespace Controller.DtGtr
{
    public class DataGartheringController : IDataGatheringController
    {
        private DataGartingLogic _gartingLogic;
        private IMapper _mapper;
        private ILog _log;


        public DataGartheringController()
        {
            _gartingLogic = new DataGartingLogic();

            var resMapConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile(new DBMappingResModel());
                cfg.AddProfile(new MsgMappingDBModel());
            });

            _mapper = resMapConfig.CreateMapper();
        }


        public void SetLog(ILog log)
        {
            _log = log;
        }

        public bool CreateCoilWeld(Msg_302_CoilWeld msg, string oriOutCoilID)
        {
            msg.VaildObjectNull("msg", $"存取WeldRecord資料至失敗");

            try
            {
                var tb = _mapper.Map<TBL_WeldRecords>(msg);
                tb.ReceiveTime = DateTime.Now;
                tb.OriPDI_Out_Coil_ID = oriOutCoilID;

                var insertNum = _gartingLogic.CreateCoilWeldRecord(tb);
                var insertOK = insertNum > 0;
                _log.I($"存取{msg.CoilNoID}WeldRecord資料至", $"新增資料{insertNum}筆成功");
                return insertOK;
           
            }
            catch(Exception e)
            {
                _log.E($"存取{msg.CoilNoID}WeldRecord資料至失敗" ,  e.ToString().CleanInvalidChar());
                return false;
            }   
        }

        public bool DeleteCoilWeld(string coilID)
        {
            coilID.VaildStrNullOrEmpty("coilID", "移除WeldRecord資料至失敗");

            try
            {
                var deleteNum = _gartingLogic.DeleteCoilWeldRecord(coilID);
                var deleteOK = deleteNum > 0;
                _log.I("移除WeldRecord資料至成功", $"移除{coilID}WeldRecord資料至成功");
                return deleteOK;
            }
            catch(Exception e)
            {
                _log.E($"移除{coilID}WeldRecord資料至失敗" , e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool CreateProcessData(Msg_313_SpdTen msg)
        {
            msg.VaildObjectNull("msg", "存取ProcessData資料");

            try
            {
                //var budTen = msg.BuildTension;
                //if (budTen == 0)
                //{
                //    _log.D("存取ProcessData資料", $"BuildTension={budTen}, 尚未建張不列入計算");
                //    return false;
                //}

                ////var tblProcessData = TblProcessDataFactory.TblProcessData(msg);
                //var tb = _mapper.Map<TBL_ProcessData>(msg);
                //var insertNum = _gartingLogic.CreateProcessData(tb);
                //_log.I("存取ProcessData資料", $"BuildTension={budTen}, 建張列入計算, 新增資料{insertNum}筆成功");
                //var insertOK = insertNum > 0;
                //return insertOK;

                var tb = _mapper.Map<TBL_ProcessData>(msg);
                var insertNum = _gartingLogic.CreateProcessData(tb);
                var insertOK = insertNum > 0;

                _log.I("存取ProcessData資料", $"是否建張={msg.BuildTension}, 新增資料{insertNum}筆成功");
                return insertOK;
            }
            catch (Exception e)
            {
                _log.E("存取ProcessData資料失敗" , e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public IEnumerable<TBL_ProcessData> QueryProcessDatas(DateTime starTime, DateTime endTime)
        {

            try
            {
                var starTimeStr = starTime.ToString(DBParaDef.DBDateTimeFromat);
                var endTimeStr = endTime.ToString(DBParaDef.DBDateTimeFromat);                
              
                var entitys = _gartingLogic.QueryProcessData(starTimeStr, endTimeStr);
                _log.I("撈取生產參數ProcessData", $"{starTimeStr} - {endTimeStr} 共 {entitys.Count()}筆資料");


                return entitys.Count()>0 ? entitys : null;

            }
            catch(Exception e)
            {
                _log.E("撈取生產參數ProcessData錯誤", e.ToString().CleanInvalidChar());
                return null;
            }
            
        }

        public IEnumerable<TBL_ProcessData> QueryProcessDatasWeld(DateTime starTime, DateTime endTime)
        {
            try
            {
                var starTimeStr = starTime.ToString(DBParaDef.DBDateTimeFromat);
                var endTimeStr = endTime.ToString(DBParaDef.DBDateTimeFromat);

                var entitys = _gartingLogic.QueryProcessDataWeld(starTimeStr, endTimeStr);
                _log.I("撈取生產參數ProcessDataWeld", $"{starTimeStr} - {endTimeStr} 共 {entitys.Count()}筆資料");


                return entitys.Count() > 0 ? entitys : null;

            }
            catch (Exception e)
            {
                _log.E("撈取生產參數ProcessData錯誤", e.ToString().CleanInvalidChar());
                return null;
            }
        }

        public IEnumerable<TBL_SideTrimmerTmp> QuerySiderTrimmerTmp(DateTime starTime, DateTime endTime)
        {

            try
            {
                var starTimeStr = starTime.ToString(DBParaDef.DBDateTimeFromat);
                var endTimeStr = endTime.ToString(DBParaDef.DBDateTimeFromat);

                var entitys = _gartingLogic.QuerySiderTrimmerTmp(starTimeStr, endTimeStr);
                _log.I("撈取生產參數SiderTrimmer", $"{starTimeStr} - {endTimeStr} 共 {entitys.Count()}筆資料");


                return entitys.Count() > 0 ? entitys : null;

            }
            catch (Exception e)
            {
                _log.E("撈取生產參數SiderTrimmer錯誤", e.ToString().CleanInvalidChar());
                return null;
            }

        }

        public SideTrimmerAvgModel CalculateAvgSiderTrimmer(IEnumerable<TBL_SideTrimmerTmp> values)
        {
            values.VaildObjectNull("values", "計算圓盤剪平均參數錯誤");

            var dao = new SideTrimmerAvgModel();
            var totalCnt = values.Count();

            foreach (TBL_SideTrimmerTmp data in values)
            {
                dao.Avg_Side_Trimmer_Gap += data.Side_Trimmer_Gap;
                dao.Avg_Side_Trimmer_Lap += data.Side_Trimmer_Lap;
                dao.Avg_Side_Trimmer_Width += data.Side_Trimmer_Width;
                dao.Avg_Trimming_OperateSide += data.Trimming_OperateSide;
                dao.Avg_Trimming_DriveSide += data.Trimming_DriveSide;
            }

            dao.Avg_Side_Trimmer_Gap = dao.Avg_Side_Trimmer_Gap / totalCnt;
            dao.Avg_Side_Trimmer_Lap = dao.Avg_Side_Trimmer_Lap / totalCnt;
            dao.Avg_Side_Trimmer_Width = dao.Avg_Side_Trimmer_Width / totalCnt;
            dao.Avg_Trimming_OperateSide = dao.Avg_Trimming_OperateSide / totalCnt;
            dao.Avg_Trimming_DriveSide = dao.Avg_Trimming_DriveSide / totalCnt;

            return dao;
        }



        public ProcessCTModel CalculateProcessData(IEnumerable<TBL_ProcessData> datas)
        {
            datas.VaildObjectNull("datas", "計算處裡生產參數錯誤");

            var dataModel = new ProcessCTModel();

            var coilLength = 0.0;
            var speedStr = string.Empty;
            var porTensionStr = string.Empty;
            var porCurrentStr = string.Empty;
            var trTensionStr = string.Empty;
            var trCurrentStr = string.Empty;
            var porTensionSetStr = string.Empty;
            var trTensionSetStr = string.Empty;

            var dataCnt = 0;

            foreach (TBL_ProcessData data in datas)
            {

                if (data.LINE_Speed_Actual == 0)
                    continue;

                var preCoilLength = coilLength;
                coilLength += data.LINE_Speed_Actual / 60;

                var prePos = Convert.ToInt32(preCoilLength);
                var pos = Convert.ToInt32(coilLength);

                if (prePos == pos)
                    continue;

                dataCnt++;

                //var pos = coilLength.GetPoint(4).ToString();
                speedStr = speedStr + pos + ":" + data.LINE_Speed_Actual.GetPoint(4).ToString() + ",";
                porTensionStr = porTensionStr + pos + ":" + data.POR_Tension_Actual.GetPoint(4).ToString() + ",";
                porCurrentStr = porCurrentStr + pos + ":" + data.POR_Current_Actual.GetPoint(4).ToString() + ",";
                porTensionSetStr = porTensionSetStr + pos + ":" + data.POR_Tension_Set.GetPoint(4).ToString() + ",";
                trTensionStr = trTensionStr + pos + ":" + data.TR_Tension_Actual.GetPoint(4).ToString() + ",";
                trCurrentStr = trCurrentStr + pos + ":" + data.TR_Current_Actual.GetPoint(4).ToString() + ",";
                trTensionSetStr = trTensionSetStr + pos + ":" + data.TR_Tension_Set.GetPoint(4).ToString() + ",";
            }

            dataModel.TotalLength = coilLength.GetPoint(4).ToString();
            dataModel.DataCnt = dataCnt;
            dataModel.SpeedStr = speedStr.TrimEnd(',');
            dataModel.PorTensionActStr = porTensionStr.TrimEnd(',');
            dataModel.PorCurrentStr = porCurrentStr.TrimEnd(',');
            dataModel.PorTensionRefStr = porTensionSetStr.TrimEnd(',');
            dataModel.TrTensionActStr = trTensionStr.TrimEnd(',');
            dataModel.TrCurrentStr = trCurrentStr.TrimEnd(',');
            dataModel.TrTensionRefStr = trTensionSetStr.TrimEnd(',');

            _log.D("計算處裡生產參數", $"處理後有效資料總比數為{dataModel.DataCnt}筆");

            return dataModel;
        }

        public ProcessCTWeldModel CalculateProcessDataWeld_L25(IEnumerable<TBL_ProcessData> datas, int passNo)
        {
            datas.VaildObjectNull("datas", "計算處裡焊機生產參數(給L25)錯誤");

            var dataModel = new ProcessCTWeldModel();
            var weldSpeedActStr = string.Empty;
            var weldCurrentActFStr = string.Empty;
            var weldCurrentActRStr = string.Empty;
            var weldPlanishWFActStr = string.Empty;
            var weldTempertureStr = string.Empty;

            var dataCnt = 0;
            var standTime = datas.ToList()[0].ReceiveTime;

            foreach (TBL_ProcessData data in datas)
            {
                var second = $"{(int)(data.ReceiveTime - standTime).TotalSeconds}";

                dataCnt++;

                weldSpeedActStr = weldSpeedActStr + second + ":" + data.WELD_Speed_Actual.GetPoint(4).ToString() + ",";
                weldCurrentActFStr = weldCurrentActFStr + second + ":" + data.WELD_Current_Actual_Front.GetPoint(4).ToString() + ",";
                weldCurrentActRStr = weldCurrentActRStr + second + ":" + data.WELD_Current_Actual_Rear.GetPoint(4).ToString() + ",";
                weldPlanishWFActStr = weldPlanishWFActStr + second + ":" + data.WELD_PlanishWheelForce_Actual.GetPoint(4).ToString() + ",";
                weldTempertureStr = weldTempertureStr + second + ":" + data.WELD_Temperture.GetPoint(4).ToString() + ",";
            }

            dataModel.DataCnt = dataCnt;
            dataModel.PassNo = passNo;
            dataModel.WeldSpeedActStr = weldSpeedActStr.TrimEnd(',');
            dataModel.WeldCurrentActFStr = weldCurrentActFStr.TrimEnd(',');
            dataModel.WeldCurrentActRStr = weldCurrentActRStr.TrimEnd(',');
            dataModel.WeldPlanishWFActStr = weldPlanishWFActStr.TrimEnd(',');
            dataModel.WeldTempertureStr = weldTempertureStr.TrimEnd(',');

            _log.D("計算處裡焊機生產參數(給L25)", $"處理後有效資料總比數為{dataModel.DataCnt}筆");

            return dataModel;
        }

        public ProcessCTWeldModel CalculateProcessDataWeld_Chart(IEnumerable<TBL_ProcessData> datas)
        {
            datas.VaildObjectNull("datas", "計算處裡焊機生產參數(給Chart)錯誤");

            var dataModel = new ProcessCTWeldModel();
            var weldSpeedActStr = string.Empty;
            var weldCurrentActFStr = string.Empty;
            var weldCurrentActRStr = string.Empty;
            var weldPlanishWFActStr = string.Empty;
            var weldTempertureStr = string.Empty;

            var dataCnt = 0;

            foreach (TBL_ProcessData data in datas)
            {
                var time = $"{data.ReceiveTime:yyyyMMddHHmmss}";

                dataCnt++;

                weldSpeedActStr = weldSpeedActStr + time + ":" + data.WELD_Speed_Actual.GetPoint(4).ToString() + ",";
                weldCurrentActFStr = weldCurrentActFStr + time + ":" + data.WELD_Current_Actual_Front.GetPoint(4).ToString() + ",";
                weldCurrentActRStr = weldCurrentActRStr + time + ":" + data.WELD_Current_Actual_Rear.GetPoint(4).ToString() + ",";
                weldPlanishWFActStr = weldPlanishWFActStr + time + ":" + data.WELD_PlanishWheelForce_Actual.GetPoint(4).ToString() + ",";
                weldTempertureStr = weldTempertureStr + time + ":" + data.WELD_Temperture.GetPoint(4).ToString() + ",";
            }

            dataModel.DataCnt = dataCnt;
            dataModel.WeldSpeedActStr = weldSpeedActStr.TrimEnd(',');
            dataModel.WeldCurrentActFStr = weldCurrentActFStr.TrimEnd(',');
            dataModel.WeldCurrentActRStr = weldCurrentActRStr.TrimEnd(',');
            dataModel.WeldPlanishWFActStr = weldPlanishWFActStr.TrimEnd(',');
            dataModel.WeldTempertureStr = weldTempertureStr.TrimEnd(',');

            _log.D("計算處裡焊機生產參數(給Chart)", $"處理後有效資料總比數為{dataModel.DataCnt}筆");

            return dataModel;
        }

        public float CalculateTRAvgTension(IEnumerable<TBL_ProcessData> values)
        {
            values.VaildObjectNull("values", "計算收卷平均張力錯誤");

            var trTension = 0.0;

            foreach (TBL_ProcessData data in values)
                trTension += data.TR_Tension_Actual;

            var avgTRTension = trTension / values.Count();

            return (float)avgTRTension;
        }

        // 測試(Unit Test)用
        public bool DeleteProcessDataByRecTime(DateTime time)
        {
            time.VaildObjectNull("time", "刪除ProcessData資料失敗");

            try
            {
                var timeSqlStr = time.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var deleteNum = _gartingLogic.DeleteProcessDataByRecTime(timeSqlStr);
                var deleteOK = deleteNum > 0;
                _log.I("刪除ProcessData資料成功", $"刪除{timeSqlStr}時間點的ProcessData資料成功");
                return deleteOK;
            }catch(Exception e)
            {
                _log.E("刪除ProcessData資料失敗" , e.ToString().CleanInvalidChar());
                return false;
            }
        }

        // 測試(Unit Test)用
        public bool DeleteSiderTrimmerByRecTime(DateTime time)
        {
            time.VaildObjectNull("time", "刪除Sider Trimmer資料失敗");

            try
            {
                var timeSqlStr = time.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var deleteNum = _gartingLogic.DeleteSiderTrimmerByRecTime(timeSqlStr);
                var deleteOK = deleteNum > 0;
                _log.I("刪除Sider Trimmer資料成功", $"刪除{timeSqlStr}時間點的Sider Trimmer資料成功");
                return deleteOK;
            }
            catch (Exception e)
            {
                _log.E("刪除Sider Trimmer資料失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool CreateUtility(L1L2Rcv.Msg_316_Utility_Data msg, string shift, string team)
        {
            msg.VaildObjectNull("msg", "存取Utility資料失敗");

            try
            {
    
                var tbUtility = msg.ToTblUtilityEntity(shift, team);
                var insertNum = _gartingLogic.CreateUtility(tbUtility);
                var insertOK = insertNum > 0;
                _log.I("存取Utility資料", $"新增資料{insertNum}筆成功");
                return insertOK;
            }
            catch (SqlException e)
            {
                _log.E("存取Utility資料失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }
       
        
        public bool CreateL25Engc(L1L2Rcv.Msg_316_Utility_Data msg)
        {
            msg.VaildObjectNull("msg", "存取 2.5 Utility資料失敗");

            try
            {
                var entity = msg.ToL25EngcEntity();
                var insertNum = _gartingLogic.CreateL25Engc(entity);
                var insertOK = insertNum > 0;
                _log.I("存取2.5 Utility資料", $"新增資料=>{insertOK}");


                insertNum = _gartingLogic.CreateL25EngcHis(entity);
                insertOK = insertNum > 0;
                _log.I("存取2.5 Utility歷史資料", $"新增資料=>{insertOK}");
                return insertOK;

            }catch(Exception e)
            {
                _log.E("存取 2.5 Utility資料失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }


        public bool CreateStopLineFaultStart(L1L2Rcv.Msg_310_LineFault msg, string team, int shift)
        {           
            try
            {
                var tblCoulCutRecortd = msg.TblLineFaultRecordsEntity(team, shift);
                tblCoulCutRecortd.stop_end_time = DBParaDef.DefaultTime;
                var insertNum = _gartingLogic.CreateLineFaultRecord(tblCoulCutRecortd);
                _log.I("存取Line Fault資料", $"新增資料{insertNum}筆{(insertNum > 0).ToStr()}");
                return insertNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"存取Line Fault資料錯誤", e.Message.CleanInvalidChar());
                return false;
            }
        }

        public WorkSchedule GetScheduleByTime(DateTime time)
        {
            try
            {
                var tb = _gartingLogic.GetScheduleByTime(time);
                if (tb == null)
                {
                    _log.E($"撈取班表失敗", $"日期{time}無對應班次股別可撈取 ");
                    return null;
                }

                if (tb.Mode == 1)
                {
                    tb = _gartingLogic.GetScheduleByTime(time, "<=");
                    if (tb == null)
                    {
                        _log.E($"撈取班表失敗", $"再次搜尋日期{time}無對應班次股別可撈取 ");
                        return null;
                    }
                }
                else if (tb.Mode == 2 || tb.Mode == 3)
                {
                    if (time.Hour < _startTimeSpan2nd_2.Hours)
                    {
                        tb = _gartingLogic.GetScheduleByTime(time, ">=");
                        if (tb == null)
                        {
                            _log.E($"撈取班表失敗", $"再次搜尋日期{time}無對應班次股別可撈取 ");
                            return null;
                        }
                    }
                    else if (time.Hour >= _startTimeSpan1st_2.Hours)
                    {
                        var reSearchTime = time.AddDays(1);
                        tb = _gartingLogic.GetScheduleByTime(reSearchTime, "<=");
                        if (tb == null)
                        {
                            _log.E($"撈取班表失敗", $"再次搜尋日期{reSearchTime}無對應班次股別可撈取 ");
                            return null;
                        }
                    }
                    else
                    {
                        tb = _gartingLogic.GetScheduleByTime(time, "<=");
                        if (tb == null)
                        {
                            _log.E($"撈取班表失敗", $"再次搜尋日期{time}無對應班次股別可撈取 ");
                            return null;
                        }
                    }
                }

                _log.I("撈取班表", $"撈取{time}班表 Shift:{tb.Shift}  Team:{tb.Team}");
                var res = _mapper.Map<WorkSchedule>(tb);
                return res;
            }
            catch (Exception e)
            {
                _log.E($"撈取該日班次股別", e.Message.CleanInvalidChar());
                return null;
            }


            //try
            //{
            //    var tb = _gartingLogic.GetScheduleByTime(time);
            //    if (tb == null)
            //    {
            //        _log.E($"撈取班表失敗", $"日期{time}無對應班次股別可撈取 ");
            //        return null;
            //    }

            //    _log.I("撈取班表", $"撈取{time}班表 Shift:{tb.Shift}  Team:{tb.Team}");
            //    var res = _mapper.Map<WorkSchedule>(tb);
            //    return res;
            //}
            //catch (Exception e)
            //{
            //    _log.E($"撈取目前班次股別", e.Message.CleanInvalidChar());
            //    return null;
            //}
        }

        public TBL_WeldRecords GetWeldRecordByEnCoilId(string enCoilId)
        {
            try
            {
                var tb = _gartingLogic.GetWeldRecords(enCoilId)?
                                      .OrderByDescending(x => x.ReceiveTime)
                                      .FirstOrDefault();

                if (tb == null)
                    _log.E($"撈取焊接完成紀錄失敗", $"查無最新的入口捲{enCoilId}紀錄 ");

                return tb;
            }
            catch (Exception e)
            {
                _log.E($"撈取焊接完成紀錄失敗", e.Message.CleanInvalidChar());
                return null;
            }
        }

        public LineFaultRecord GetLineFaultRecord(string prodTime, string stopStartTime)
        {
            try
            {
                var tb = _gartingLogic.GetLineFaultRecord(prodTime, stopStartTime);
                _log.I("撈取停復機紀錄", $"撈取{stopStartTime}停復機紀錄");
                var res = _mapper.Map<LineFaultRecord>(tb);
                return res;
            }
            catch (Exception e)
            {
                _log.E($"撈取目前班次股別", e.Message.CleanInvalidChar());
                return null;
            }
        }

        public bool UpdateLineFaultUploadFlag(DateTime prodTime, DateTime stopStartTime, bool uploadDone)
        {
            try
            {
                var uploadFinish = uploadDone ? DBParaDef.YES : DBParaDef.NO;
                var prodTimeStr = prodTime.ToString("yyyy-MM-dd");
                var stopStartTimeStr = stopStartTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var updateNum = _gartingLogic.UpdateLineFaultUploadFlag(prodTimeStr, stopStartTimeStr, uploadFinish);
                var updateOK = updateNum > 0;
                _log.I("更新停復機紀錄上傳旗標", $"更新停復機紀錄上傳旗標=>{updateOK}");  
                return updateOK;
            }
            catch (Exception e)
            {
                _log.E($"更新停復機紀錄上傳旗標失敗", e.Message.CleanInvalidChar());
                return false;
            }
        }

        public bool CreateL25DownTime(LineFaultRecord dao)
        {
            try
            {
                var entity = dao.ToL25DownTimeEntity();
                var inserNum = _gartingLogic.CreateL25DownTime(entity);
                var insertOK = inserNum > 0;
                _log.I("新L25停復機紀錄資料", $"新增=>{insertOK}");


                inserNum = _gartingLogic.CreateL25DownTimeHis(entity);
                insertOK = inserNum > 0;
                _log.I("新L25停復機歷史紀錄資料", $"新增=>{insertOK}");

                return insertOK;

            }
            catch(Exception e)
            {

                _log.E("新增L25停復機資料錯誤", e.ToString().CleanInvalidChar());
                return false;
            }

        }

        public bool UpdateStopLineFaultEnd(L1L2Rcv.Msg_310_LineFault msg)
        {

            var lineFaultRecord = new TBL_LineFaultRecords();

            // 查停機記錄
            try
            {
                lineFaultRecord = _gartingLogic.GetLineFaultLastRecord();
            }
            catch (Exception e)
            {
                _log.E($"撈取Line Fault資料失敗", e.Message.CleanInvalidChar());
                return false;
            }

            if (lineFaultRecord == null)
            {
                _log.A($"無此停復機紀錄", "未有未結算完成停復機紀錄");
                return false;
            }

         
        
            
            // 更新
            try
            {
                lineFaultRecord.stop_end_time = msg.EndTime.ConverDateTime(); ;
                lineFaultRecord.stop_elased_timey = GetElasedTimey(lineFaultRecord.stop_end_time, lineFaultRecord.stop_start_time, 7);

                var updateNum = _gartingLogic.UpdateFaultRecord(lineFaultRecord);
                _log.I("更新Line Fault資料", $"更新資料{updateNum}筆成功");
                return updateNum > 0;
            }
            catch (Exception e)
            {
                _log.E($"存取Line Fault資料", e.Message.CleanInvalidChar());
                return false;
            }

        }

        public bool CreateProcessCTData(PDO pdo, ProcessCTModel datas, L25CTData dataClssify)
        {
            pdo.VaildObjectNull("pdo", "pdo參數錯誤");
            datas.VaildObjectNull("datas", "計算處裡生產參數錯誤");
            try
            {
                var entity = pdo.ToProcessDataCTEntity(datas, dataClssify);
                var insertNum = _gartingLogic.CreateProcessDataCT(entity);
                var insertOK = insertNum > 0;
                _log.I($"存取ProcessCT資料=>{insertOK}", $"存取ProcessCT資料-{dataClssify.L25CTDataClassifyToStr()}");
                return true;
            }
            catch(Exception e)
            {
                _log.E($"存取{nameof(dataClssify)}失敗", e.ToString().CleanInvalidChar());
                return false;
            }

        }

        public bool CreateProcessCTDataWeld(PDO pdo, ProcessCTWeldModel datas, L25CTData dataClssify)
        {
            pdo.VaildObjectNull("pdo", "pdo參數錯誤");
            datas.VaildObjectNull("datas", "計算處裡生產參數錯誤");
            try
            {
                var entity = pdo.ToProcessDataCTWeldEntity(datas, dataClssify);
                var insertNum = _gartingLogic.CreateProcessDataCT(entity);
                var insertOK = insertNum > 0;
                _log.I($"存取ProcessCT資料=>{insertOK}", $"存取ProcessCT資料-{dataClssify.L25CTDataClassifyToStr()}");
                return true;
            }
            catch (Exception e)
            {
                _log.E($"存取{nameof(dataClssify)}失敗", e.ToString().CleanInvalidChar());
                return false;
            }

        }

        public bool CreateL25Alive()
        {
            try
            {
                var entity = new L2L25_Alive();
                entity.Message_Id = L25SysDef.MsgCode.Msg114Alive;
                entity.Message_Length = L25SysDef.MsgLength.Msg114Alive;
                entity.Date = DateTime.Now.ToString("yyyyMMdd");
                entity.Time = DateTime.Now.ToString("HHmmss");
                var insertNum = _gartingLogic.CreateL25Alive(entity);
                var insertOK = insertNum > 0;
                _log.D("存取L25 Alive訊息", "存取L25 Alive訊息");

                insertNum = _gartingLogic.CreateL25AliveHis(entity);
                insertOK = insertNum > 0;
                _log.D("存取L25 Alive歷史訊息", "存取L25 Alive訊息");

                return insertOK;
            }
            catch (Exception e)
            {
                _log.E($"存取L25 Alive失敗", e.ToString().CleanInvalidChar());
                return false;
            }

        }

        public bool Create25ProcessCTData(PDO pdo, ProcessCTModel datas, L25CTData dataClssify)
        {
            pdo.VaildObjectNull("pdo", "pdo參數錯誤");
            datas.VaildObjectNull("datas", "計算處裡生產參數錯誤");

            int inserNum = 0;

            try
            {
                switch (dataClssify)
                {
                    case L25CTData.Speed:
                        inserNum = _gartingLogic.Create25SpeedCT(pdo.ToL25SpeedCTEntity(datas));
                        inserNum = _gartingLogic.Create25SpeedCTHis(pdo.ToL25SpeedCTEntity(datas));
                        break;
                    case L25CTData.UNCTensionActCT:
                        inserNum = _gartingLogic.Create25UNCTenstionCT(pdo.ToL25UNCTensionActCTEntity(datas));
                        inserNum = _gartingLogic.Create25UNCTenstionCTHis(pdo.ToL25UNCTensionActCTEntity(datas));
                        break;
                    case L25CTData.UNCTensionRefCT:
                        inserNum = _gartingLogic.Create25UNCTenstionCT(pdo.ToL25UNCTensionRefCTEntity(datas));
                        inserNum = _gartingLogic.Create25UNCTenstionCTHis(pdo.ToL25UNCTensionRefCTEntity(datas));
                        break;
                    case L25CTData.UNCCurrentCT:
                        inserNum = _gartingLogic.Create25UNCurrentCT(pdo.ToL25UNCCurrentCTEntity(datas));
                        inserNum = _gartingLogic.Create25UNCurrentCTHis(pdo.ToL25UNCCurrentCTEntity(datas));
                        break;
                    case L25CTData.RECTensionActCT:
                        inserNum = _gartingLogic.CreateL25RECTensionCT(pdo.ToL25RECTensionActCTEntity(datas));
                        inserNum = _gartingLogic.CreateL25RECTensionCTHis(pdo.ToL25RECTensionActCTEntity(datas));
                        break;
                    case L25CTData.RECTensionRefCT:
                        inserNum = _gartingLogic.CreateL25RECTensionCT(pdo.ToL25RECTensionRefCTEntity(datas));
                        inserNum = _gartingLogic.CreateL25RECTensionCTHis(pdo.ToL25RECTensionRefCTEntity(datas));
                        break;
                    case L25CTData.RECCurrentCT:
                        inserNum = _gartingLogic.CreateL25RECCurrentCT(pdo.ToL25RECCurrentCTEntity(datas));
                        inserNum = _gartingLogic.CreateL25RECCurrentCTHis(pdo.ToL25RECCurrentCTEntity(datas));
                        break;
                }

                var insetOK = inserNum > 0;

                _log.I($"存取L25=>{insetOK}", $"存取L25-{dataClssify.L25CTDataClassifyToStr()}");

                return insetOK;
            }
            catch(Exception e)
            {
                _log.E($"存取L25 {dataClssify.L25CTDataClassifyToStr()}失敗", e.ToString().CleanInvalidChar());
                return false;
            }


           
        }

        public bool Create25ProcessCTDataWeld(PDO pdo, ProcessCTWeldModel datas, L25CTData dataClssify)
        {
            pdo.VaildObjectNull("pdo", "pdo參數錯誤");
            datas.VaildObjectNull("datas", "計算處裡焊機生產參數錯誤");

            int inserNum = 0;
            L2L25_WeldCT obj = null;

            try
            {
                switch (dataClssify)
                {
                    case L25CTData.WELD_Current_Actual_Front:
                        obj = pdo.ToL25WeldCTEntity_WeldCurrActF(datas);
                        break;
                    case L25CTData.WELD_Current_Actual_Rear:
                        obj = pdo.ToL25WeldCTEntity_WeldCurrActR(datas);
                        break;
                    case L25CTData.WELD_Speed_Actual:
                        obj = pdo.ToL25WeldCTEntity_WeldSpeedAct(datas);
                        break;
                    case L25CTData.WELD_PlanishWheelForce_Actual:
                        obj = pdo.ToL25WeldCTEntity_WeldPlanushWFAct(datas);
                        break;
                    case L25CTData.WELD_Temperture:
                        obj = pdo.ToL25WeldCTEntity_WeldTempertureAct(datas);
                        break;
                }

                inserNum = _gartingLogic.Create25WeldCT(obj);
                inserNum = _gartingLogic.Create25WeldCTHis(obj);

                var insetOK = inserNum > 0;

                _log.I($"存取L25=>{insetOK}", $"存取L25-{dataClssify.L25CTDataClassifyToStr()}");

                return insetOK;
            }
            catch (Exception e)
            {
                _log.E($"存取L25 {dataClssify.L25CTDataClassifyToStr()}失敗", e.ToString().CleanInvalidChar());
                return false;
            }
        }


        public bool CreateSideTrimmer(Msg_318_SideTrimmerInfo msg)
        {
            var entity = new TBL_SideTrimmerTmp();
            entity.Side_Trimmer_Gap = msg.SideTrimmerGap;
            entity.Side_Trimmer_Lap = msg.SideTrimmerLap;
            entity.Side_Trimmer_Width = msg.SideTrimmerWidth;
            entity.Trimming_OperateSide = msg.Trimming_OperateSide;
            entity.Trimming_DriveSide = msg.Trimming_DriveSide;
            entity.ReceiveTime = msg.DateTime;

            try
            {
                var inserNum = _gartingLogic.CreateSideTrimmerTmp(entity);
                var inserOK = inserNum > 0;
                _log.I("存取SiderTrimmer", $"存取Sider Trimmer {inserOK}");
                return inserOK;
            }
            catch (Exception e)
            {
                _log.E("存取SiderTrimmer錯誤", e.ToString().CleanInvalidChar());
                return false;
            }
        
        }

        public int DeleteAllSiderTrimmer()
        {
            try
            {
                var deleteNum = _gartingLogic.DeleteAllSideTrimmerTmp();
                _log.I("刪除所有Sider Trimmer資料", $"刪除Sider Trimmer所有資料");
                return deleteNum;

            }catch(Exception e)
            {
                _log.I("刪除所有Sider Trimmer資料錯誤", e.Message.ToString());
                return 0;
            }
        }

        private readonly TimeSpan _startTimeSpan1st_1 = new TimeSpan(8, 0, 0);
        private readonly TimeSpan _startTimeSpan2nd_1 = new TimeSpan(16, 0, 0);
        private readonly TimeSpan _startTimeSpan3th_1 = new TimeSpan(0, 0, 0);
        private readonly TimeSpan _startTimeSpan1st_2 = new TimeSpan(20, 0, 0);
        private readonly TimeSpan _startTimeSpan2nd_2 = new TimeSpan(8, 0, 0);

        public bool ProcCross(DateTime now, string shift, string team)
        {
            try
            {
                var edr = _gartingLogic.GetLineFaultLastRecord();
                if (edr == null)
                {
                    _log.I($"跨班處理", "當前無停機紀錄需要處理");
                    return false;
                }
                if (edr.stop_end_time != DBParaDef.DefaultTime)
                {
                    _log.I($"跨班處理", "當前無停機紀錄需要處理");
                    return false;
                }

                //  將取得的紀錄結算
                edr.stop_end_time = now;
                edr.stop_elased_timey = GetElasedTimey(edr.stop_end_time, edr.stop_start_time, 7);
                if (_gartingLogic.UpdateFaultRecord(edr) <= 0)
                {
                    _log.E($"跨班處理", "結算記錄失敗");
                    return false;
                }

                //  再新增一筆紀錄
                edr.prod_shift_no = shift;
                edr.prod_shift_group = team;
                edr.stop_start_time = edr.stop_end_time;
                edr.stop_end_time = DBParaDef.DefaultTime;
                edr.stop_elased_timey = string.Empty;
                if (_gartingLogic.CreateLineFaultRecord(edr) <= 0)
                {
                    _log.E($"跨班處理", "新增記錄失敗");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                _log.E($"跨班處理例外", e.ToString().CleanInvalidChar());
                return false;
            }
        }

        public bool ChkCrossTime(ref DateTime crossTime, DateTime now)
        {
            var tb = _gartingLogic.GetScheduleByTime(now);
            if (tb == null)
            {
                _log.E($"檢查跨班時間失敗", $"日期{now}無對應班次股別可撈取 ");
                return false;
            }

            TimeSpan statrTimeSpan1st;
            TimeSpan statrTimeSpan2nd;
            TimeSpan statrTimeSpan3th;

            switch (tb.Mode)
            {
                case 1:
                    statrTimeSpan1st = _startTimeSpan1st_1;
                    statrTimeSpan2nd = _startTimeSpan2nd_1;
                    statrTimeSpan3th = _startTimeSpan3th_1;
                    break;
                case 2:
                case 3:
                    statrTimeSpan1st = _startTimeSpan1st_2;
                    statrTimeSpan2nd = _startTimeSpan2nd_2;
                    statrTimeSpan3th = default;
                    break;
                default:
                    _log.E($"檢查跨班時間", $"未定義的 mode({tb.Mode})");
                    return false;
            }

            if (now.Hour == statrTimeSpan1st.Hours && now.Minute >= statrTimeSpan1st.Minutes && now.Minute <= statrTimeSpan1st.Minutes + 1)
            {
                if (crossTime != DateTime.MinValue)
                    if (crossTime > new DateTime(now.Year, now.Month, now.Day) + statrTimeSpan1st)
                        return false;
            }
            else if (now.Hour == statrTimeSpan2nd.Hours && now.Minute >= statrTimeSpan2nd.Minutes && now.Minute <= statrTimeSpan2nd.Minutes + 1)
            {
                if (crossTime != DateTime.MinValue)
                    if (crossTime > new DateTime(now.Year, now.Month, now.Day) + statrTimeSpan2nd)
                        return false;
            }
            else if (tb.Mode == 1 && now.Hour == statrTimeSpan3th.Hours && now.Minute >= statrTimeSpan3th.Minutes && now.Minute <= statrTimeSpan3th.Minutes + 1)
            {
                if (crossTime != DateTime.MinValue)
                    if (crossTime > new DateTime(now.Year, now.Month, now.Day) + statrTimeSpan3th)
                        return false;
            }
            else
            {
                return false;
            }

            crossTime = now;

            return true;
        }

        private string GetElasedTimey(DateTime endTime, DateTime startTime, int fixedLen)
        {
            var elasedTimey = endTime.Subtract(startTime).TotalMinutes.ToString().FixedLen(fixedLen);

            //  檢查有無小數點在最後
            if (elasedTimey.Length >= 2)
                elasedTimey = elasedTimey.Right(1) == "." ? elasedTimey.Left(elasedTimey.Length - 1) : elasedTimey;

            return elasedTimey;
        }
    }
}
