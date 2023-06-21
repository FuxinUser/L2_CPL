using DBService.Repository;
using DBService.Repository.Leader;
using DBService.Repository.LineStatus;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using DBService.Repository.ScheduleDelete_CoilReject_Record_Temp;
using DBService.Repository.SystemSetting;
using DBService.Repository.UnmountRecord;
using System;
using System.Data;
using System.Text;
using static CPL1HMI.DataBaseTableFactory;

namespace CPL1HMI
{
    public class Frm_2_1_SqlFactory
    {

        #region --- Display ---

        /// <summary>
        /// 搜尋TrackingMap
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_TrackingMap()
        {
            string strSql = $@"Select * From [{nameof(CoilMapEntity.TBL_CoilMap)}]";

            return strSql;
        }


        /// <summary>
        /// 搜尋ProcessData
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_ProcessData()
        {
            string strSql = $@"Select Top 1
                                    [{nameof(ProcessDataEntity.TBL_ProcessData.LINE_Speed_Actual)}],
                                    [{nameof(ProcessDataEntity.TBL_ProcessData.POR_Tension_Actual)}],
                                    [{nameof(ProcessDataEntity.TBL_ProcessData.POR_Tension_Set)}],
                                    [{nameof(ProcessDataEntity.TBL_ProcessData.POR_Current_Actual)}],
                                    [{nameof(ProcessDataEntity.TBL_ProcessData.TR_Tension_Actual)}],
                                    [{nameof(ProcessDataEntity.TBL_ProcessData.TR_Tension_Set)}],
                                    [{nameof(ProcessDataEntity.TBL_ProcessData.TR_Current_Actual)}]
                        From [{nameof(ProcessDataEntity.TBL_ProcessData)}]
                        Order by [{nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime)}] desc";

            return strSql;
        }


        /// <summary>
        /// 搜尋入口段鋼卷頭/尾段導帶及掃描紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_EntrySkidCoilFlag(string Coil_ID)
        {
            string strSql = $@"Select
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Scaned_Coil_ID)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Scaned_Time)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID_Checked)}],
                            leader.*
                        From [{nameof(CoilPDIEntity.TBL_PDI)}] pdi
                        Left join [{nameof(LeaderTempEntity.TBL_Leader_Temp)}] leader 
                        On Leader.[{nameof(LeaderTempEntity.TBL_Leader_Temp.Coil_ID)}] = pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
                        Where pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }


        /// <summary>
        /// 搜尋出口段掃描及PDO上傳紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_DeliverySkidCoilFlag(string Coil_ID)
        {
            string strSql = $@"Select
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}],
                            [{nameof(PDOEntity.TBL_PDO.Exit_CoilID_Checked)}],
                            [{nameof(PDOEntity.TBL_PDO.Exit_Scaned_CoilID)}],
                            [{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}]
                        From [TBL_PDO]
                        Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }

        /// <summary>
        /// 搜尋出口段Out_Coil_Gross_WT
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_Out_Coil_Gross_WT(string Coil_ID)
        {
            string strSql = $@"Select
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT)}]                            
                        From [TBL_PDO]
                        Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }

        /// <summary>
        /// 未上線鋼卷
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_Top10_Schedule()
        {
            string strSql = $@"Select Top 10 
                                    ROW_NUMBER() OVER( ORDER BY   a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}]) AS No,
                                    a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}],
                                    b.[{nameof(CoilPDIEntity.TBL_PDI.Process_Code)}],
                                    b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}],
                                    b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)}],
                                    b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)}],
                                    b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)}],
                                    b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)}],
                                    b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)}]
                    From [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] a 
                    Left join [{nameof(CoilPDIEntity.TBL_PDI)}] b On a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
                    Where a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'N' or a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'R'
                    Order by a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] asc";

            return strSql;
        }


        /// <summary>
        /// 線上鋼卷
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_TrackingMapCoilInfo()
        {
            string strSql = $@"Select 
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Process_Code)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Sleeve_Type_Code)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Sleeve_diamter)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Paper_Req_Code)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Paper_Code)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Length)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Width)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Length)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Width)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Max)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Min)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.St_No)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Density)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.REPAIR_TYPE)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Surface_Finishing_Code)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Base_Surface)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Uncoiler_Direction)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Req_Code)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Diamter)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Out_Strap_Num)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Sample_Flag)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Sample_Frqn_Code)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Sample_Lot_No)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Coil_Origin)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Wholebacklog_Code)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Next_Wholebacklog_Code)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Inner)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Order_No)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Max)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Min)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Order_Wt)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Dividing_Num)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_1)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_2)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_3)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_4)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_5)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_6)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_1)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_2)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_3)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_4)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_5)}],
                            b.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_6)}]
                            
                From [TBL_CoilMap] a
                Left join [{nameof(CoilPDIEntity.TBL_PDI)}]  b on b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_SK01)}]
                Or b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_SK02)}]
                Or b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_TOP)}]
                Or b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.POR)}]
                Or b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK01)}]
                Or b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK02)}]
                Or b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_TOP)}]
                Or b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = a.[{nameof(CoilMapEntity.TBL_CoilMap.TR)}]
             Where ( a.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_SK02)}] <> ''
                  or a.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_TOP)}] <> ''
                  or a.[{nameof(CoilMapEntity.TBL_CoilMap.POR)}] <> ''
                  or a.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK01)}] <> ''
                  or a.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK02)}] <> ''
                  or a.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_TOP)}] <> ''
                  or a.[{nameof(CoilMapEntity.TBL_CoilMap.TR)}] <> '')";

            return strSql;
        }


        /// <summary>
        /// 自動入料狀態搜尋
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_AutoFeedStatus()
        {
            string strSql = $@"Select [{nameof(SystemSettingEntity.TBL_SystemSetting.Value)}] 
                        From [{nameof(SystemSettingEntity.TBL_SystemSetting)}] 
                        Where [{nameof(SystemSettingEntity.TBL_SystemSetting.Parameter_Group)}] ='{GlobalVariableHandler.proLine}'  
                        and [{nameof(SystemSettingEntity.TBL_SystemSetting.Parameter)}] = 'AutoCoilFeed'";

            return strSql;
        }

        #endregion


        #region --- Funtion ---

        /// <summary>
        /// 自動入料狀態變更
        /// </summary>
        /// <param name="feedStatus"></param>
        /// <returns></returns>
        public static string SQL_Update_AutoFeedStatus(AutoFeedStatus feedStatus)
        {
            string strSql = $@"UPDATE [{nameof(SystemSettingEntity.TBL_SystemSetting)}] set 
                             [{nameof(SystemSettingEntity.TBL_SystemSetting.Value)}] = '{(int)feedStatus}' 
                       Where [{nameof(SystemSettingEntity.TBL_SystemSetting.Parameter_Group)}] ='{ GlobalVariableHandler.proLine}' 
                       and [{nameof(SystemSettingEntity.TBL_SystemSetting.Parameter)}] = 'AutoCoilFeed' ";

            return strSql;
        }


        /// <summary>
        /// 檢查鋼卷註記
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_CoilFlagChecked(string Coil_ID)
        {
            string strSql = $@"Select 
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Dividing_Num)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_1)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_2)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_3)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_4)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_5)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_6)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)}],
                            pdi.[{nameof(CoilPDIEntity.TBL_PDI.Process_Code)}],leader.*
                    From [{nameof(CoilPDIEntity.TBL_PDI)}] pdi
                    Left Join [{nameof(LeaderTempEntity.TBL_Leader_Temp)}] leader 
                    On leader.[{nameof(LeaderTempEntity.TBL_Leader_Temp.Coil_ID)}] = pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
                    Where pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }


        /// <summary>
        /// 判斷PDO Table有沒有這筆鋼卷
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_PDOChecked(string Coil_ID)
        {
            string strSql = $@" select [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] From [{nameof(PDOEntity.TBL_PDO)}] Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] ='{Coil_ID}'";

            return strSql;
        }


        /// <summary>
        /// 要求PDI-檢查是否有PDI
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_PDIChecked(string Coil_ID)
        {
            string strSql = $@"select [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] from [{nameof(CoilPDIEntity.TBL_PDI)}] where  [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]='{Coil_ID}'";

            return strSql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_PDItoSkid(string Coil_ID)
        {           
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" Select Top(1) * ");
            sb.AppendLine($" From { nameof(CoilPDIEntity.TBL_PDI)} ");
            sb.AppendLine($" Where { nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)} = '{Coil_ID}' ");
            sb.AppendLine($" Order by { nameof(CoilPDIEntity.TBL_PDI.CreateTime)} DESC ");
            string strSql = sb.ToString();
            return strSql;
        }

        /// <summary>
        /// 刪除鞍座鋼捲號
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <param name="Skid"></param>
        /// <returns></returns>
        public static string SQL_Update_ClearSkid(string Coil_ID, string Skid)
        {
            string strSql = $@"Update [{nameof(CoilMapEntity.TBL_CoilMap)}] Set [{Skid}] = '' Where [{Skid}] = '{Coil_ID}'";

            return strSql;
        }


        #endregion


        #region --- [入] ---

        /// <summary>
        /// [入]-排程鋼捲清單搜尋
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_Schedule()
        {

            #region SQL
            string strSql = $@"Select Top 40 
                            a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}]
                        From [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] a
                     
                        Where a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'N' or a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'R'
                        Order by a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] asc";
            #endregion

            //   Left join [{nameof(CoilPDIEntity.TBL_PDI)}] b on a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
            return strSql;
        }


        /// <summary>
        /// [入]-修改TrackingMap
        /// </summary>
        /// <param name="Skid"></param>
        /// <param name="Coil"></param>
        /// <returns></returns>
        public static string SQL_Update_Tracking(string Skid, string Coil)
        {
            string strSql = $@"Update [{nameof(CoilMapEntity.TBL_CoilMap)}] Set [{Skid}] = '{Coil}'";
            return strSql;
        }


        #endregion


        #region --- [導] ---

        /// <summary>
        /// 修改導帶資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Update_LeaderData(string Coil_ID,string Out_Coil_ID)
        {
            string strSql = $@"Update [{nameof(LeaderTempEntity.TBL_Leader_Temp)}] set
                               [{nameof(LeaderTempEntity.TBL_Leader_Temp.OriPDI_Out_Coil_ID)}] = '{Out_Coil_ID}',
                               [{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_St_No)}] = '{PublicForms.frm_Leader.HST_NO.Text}',
                               [{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Length)}] = '{PublicForms.frm_Leader.HeadStrip_Length.Text}',
                               [{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Width)}] = '{PublicForms.frm_Leader.HeadStrip_Width.Text}',
                               [{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Thickness)}] = '{PublicForms.frm_Leader.HeadStrip_Thickness.Text}',
                               [{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_St_No)}] = '{PublicForms.frm_Leader.TST_NO.Text}',
                               [{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Length)}] = '{PublicForms.frm_Leader.TailStrip_Length.Text}',
                               [{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Width)}] = '{PublicForms.frm_Leader.TailStrip_Width.Text}',
                               [{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Thickness)}] = '{PublicForms.frm_Leader.TailStrip_Thickness.Text}'
                        Where [{nameof(LeaderTempEntity.TBL_Leader_Temp.Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }


        /// <summary>
        /// 新增導帶資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Insert_LeaderData(string Coil_ID,string Out_Coil_ID)
        {
            string strSql = $@"Insert into [{nameof(LeaderTempEntity.TBL_Leader_Temp)}] 
                                         ( [{nameof(LeaderTempEntity.TBL_Leader_Temp.Coil_ID)}],
                                           [{nameof(LeaderTempEntity.TBL_Leader_Temp.OriPDI_Out_Coil_ID)}],
                                           [{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_St_No)}],
                                           [{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Length)}],
                                           [{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Width)}],
                                           [{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Thickness)}],
                                           [{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_St_No)}],
                                           [{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Length)}],
                                           [{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Width)}],
                                           [{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Thickness)}],
                                           [{nameof(LeaderTempEntity.TBL_Leader_Temp.Create_UserID)}],
                                           [{nameof(LeaderTempEntity.TBL_Leader_Temp.CreateTime)}])
                                   Values ('{Coil_ID}',
                                           '{Out_Coil_ID}',
                                           '{PublicForms.frm_Leader.HST_NO.Text.Trim()}',
                                           '{PublicForms.frm_Leader.HeadStrip_Length.Text.Trim()}',
                                           '{PublicForms.frm_Leader.HeadStrip_Width.Text.Trim()}',
                                           '{PublicForms.frm_Leader.HeadStrip_Thickness.Text.Trim()}',
                                           '{PublicForms.frm_Leader.TST_NO.Text.Trim()}',
                                           '{PublicForms.frm_Leader.TailStrip_Length.Text.Trim()}',
                                           '{PublicForms.frm_Leader.TailStrip_Width.Text.Trim()}',
                                           '{PublicForms.frm_Leader.TailStrip_Thickness.Text.Trim()}',
                                           '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                           '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";

            return strSql;
        }


       /// <summary>
       /// 搜尋導帶資料
       /// </summary>
       /// <param name="Coil_ID"></param>
       /// <returns></returns>
        public static string SQL_Select_LeaderData(string Coil_ID)
        {
            string strSql = $@"Select leader.* ,pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)}]
                                 From [{nameof(CoilPDIEntity.TBL_PDI)}] pdi
                            Left Join [{nameof(LeaderTempEntity.TBL_Leader_Temp)}] leader
                                   On pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = leader.[{nameof(LeaderTempEntity.TBL_Leader_Temp.Coil_ID)}]
                                Where [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }


        /// <summary>
        /// 搜尋入口端的鋼捲
        /// </summary>
        /// <returns></returns>
        public static string Frm_Leader_CoilIDComboBoxItems_DB_Map_PDi()
        {
            string strSql = $@"Select distinct a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
                        From [{nameof(CoilMapEntity.TBL_CoilMap)}] b 
                        Left join [{nameof(CoilPDIEntity.TBL_PDI)}] a 
                        on b.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_SK01)}] = a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
                        or b.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_SK02)}] = a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
                        or b.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_TOP)}] = a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]";

            return strSql;
        }


        #endregion


        #region --- [斷帶] ---

        /// <summary>
        /// 取得POR斷帶鋼捲PDI
        /// </summary>
        /// <param name="POR_Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_StripBreakPDI(string POR_Coil_ID)
        {
            string strSql = $@"Select [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}],
                                      [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)}],
                                      [{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}],
                                      [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)}],
                                      [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)}],
                                      [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}],
                                      [{nameof(CoilPDIEntity.TBL_PDI.Density)}],
                                      [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)}]
                                 From [{nameof(CoilPDIEntity.TBL_PDI)}]
                                Where [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{POR_Coil_ID}'";

            return strSql;
        }


        /// <summary>
        /// 取得TR Unmount資料
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string SQL_Select_UnmountRecord(string OriPDI_Out_Coil_ID)
        {
            string strSql = $@"Select *
                                 From [{nameof(UnmountRecordEntity.TBL_UnmountRecord)}]
                                Where [{nameof(UnmountRecordEntity.TBL_UnmountRecord.OriPDI_Out_Coil_ID)}] = '{OriPDI_Out_Coil_ID}'";

            return strSql;
        }


        /// <summary>
        /// POR斷帶鋼捲存進TBL_ScheduleDelete_CoilReject_Temp
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Coil_ID"></param>
        /// <param name="Length"></param>
        /// <param name="Weight"></param>
        /// <returns></returns>
        public static string SQL_Insert_StripBreakCoil_CoilRejectTemp(DataTable dt , string Coil_ID , int Length , int Weight)
        {
            string strSql = $@"Insert into [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp)}]
                                         ( [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Coil_ID)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Record_Type)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Entry_Coil_ID)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.OriPDI_Out_Coil_ID)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Plan_No)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Mode_Of_Reject)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Length_Of_Rejected_Coil)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Weight_Of_Rejected_Coil)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Inner_Diameter_Of_RejectedCoil)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Outer_Diameter_Of_RejectedCoil)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Width_Of_RejectedCoil)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Create_UserID)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.CreateTime)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Reason_Of_Reject)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Time_Of_Reject)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Shift_Of_Reject)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Turn_Of_Reject)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Paper_exit_Code)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Paper_Type)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.FINAL_COIL_FLAG)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.HEAD_PAPER_LENGTH)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.HEAD_PAPER_WIDTH)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.TAIL_PAPER_LENGTH)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.TAIL_PAPER_WIDTH)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Reject_Skid)}],
                                           [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Remarks)}])
                                    Values
                                         ( '{Coil_ID}',
                                           'C',
                                           '{dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)]}',
                                           '{dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)]}',
                                           '{dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Plan_No)]}',
                                           '2',
                                           '{Length}',
                                           '{Weight}',
                                           '',
                                           '',
                                           '{dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)]}',
                                           '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                           '{DateTime.Now:yyyy-MM-dd HH:mm:ss}',
                                           '',
                                           '',
                                           '',
                                           '',
                                           '',
                                           '',
                                           '',
                                           '',
                                           '',
                                           '',
                                           '',
                                           '',
                                           '')";

            return strSql;
        }

        #endregion


        #region --- 保留 ---

        /// <summary>
        /// 輸入毛重
        /// </summary>
        /// <returns></returns>
        public static string SQL_Update_CoilWeight()
        {
            string strSql = $@"Update [{nameof(PDOEntity.TBL_PDO)}] set 
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Wt)}] = '{PublicForms.Tracking.Txt_Weight_X.Text}',
                            [{nameof(PDOEntity.TBL_PDO.CoilWeight_Time)}]='{GlobalVariableHandler.Instance.time}'
                        Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}]='{PublicForms.Tracking.Lbl_DSK02_CoilNo.Text}'";

            return strSql;

        }


        /// <summary>
        /// 斷帶事件-POR退料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string Frm_2_1_StripBrakeSignal_DB_TBL_StripBrakeSignal(string Coil_ID)
        {
            string strSql = $@"Select Top 1 strip.[{nameof(TBL_StripBrakeSignal.UncoilerCoil_Thick)}],
                               strip.[{nameof(TBL_StripBrakeSignal.UncoilerCoil_Width)}],
                               strip.[{nameof(TBL_StripBrakeSignal.UncoilerCoil_Length)}],
                               strip.[{nameof(TBL_StripBrakeSignal.UncoilerCoil_InnerDiameter)}],
                               strip.[{nameof(TBL_StripBrakeSignal.UncoilerCoil_OuterDiameter)}],
                               strip.[{nameof(TBL_StripBrakeSignal.UncoilerCoil_TheorticalWt)}],
                                 pdi.[{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}],
                                 pdi.[{nameof(CoilPDIEntity.TBL_PDI.Paper_Req_Code)}],
                                 pdi.[{nameof(CoilPDIEntity.TBL_PDI.Paper_Code)}]
                        From [{nameof(TBL_StripBrakeSignal)}] strip
                        Right join [{nameof(CoilPDIEntity.TBL_PDI)}] pdi on pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = strip.[{nameof(TBL_StripBrakeSignal.UncoilerCoil_No)}]
                        Where [{nameof(TBL_StripBrakeSignal.UncoilerCoil_No)}] = '{Coil_ID}'
                        Order by [{nameof(TBL_StripBrakeSignal.CreateTime)}] desc";

            return strSql;
        }

        #endregion

    }
}
