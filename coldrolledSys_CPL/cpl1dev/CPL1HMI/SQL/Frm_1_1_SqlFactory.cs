using DBService.Repository;
using DBService.Repository.DefectData;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using DBService.Repository.ScheduleDelete_CoilReject_Record_Temp;
using DBService.Repository.SystemSetting;
using System;
using System.Data;

namespace CPL1HMI
{
    public class Frm_1_1_SqlFactory
    {

        #region ---排程---

        /// <summary>
        /// 排程TabPage-排程DGV資料
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_Production_Schedule(int intTop = 0)
        {
            string strTop = "";
            if (intTop > 0)
                strTop += $"Top {intTop}";

            string strSql = $@" Select {strTop}
                                ROW_NUMBER() OVER( ORDER BY   a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}]) AS No,
                                b.[{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}],
                                a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}],
                                a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}],
                                b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}],
                                b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)}],
                                b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)}],
                                b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)}],
                                b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)}],
                                b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)}]
                        From [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] a
                        
                        Left join (Select MAX({nameof(CoilPDIEntity.TBL_PDI.CreateTime)}) as LastCreateTime ,{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)} 
                                   From {nameof(CoilPDIEntity.TBL_PDI)} Group by {nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}) as  temp
                             on a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] =  temp.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] 					
                        Left join  [{nameof(CoilPDIEntity.TBL_PDI)}] b on b.[{nameof(CoilPDIEntity.TBL_PDI.CreateTime)}] = temp.[LastCreateTime] AND b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = temp.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]

                        Where a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'N' or a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'R'
                        Order by a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] asc";
            // Left join  [TBL_PDI] b on a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
            return strSql;
        }


        /// <summary>
        /// 排程TabPage-紀錄原有的Seq_No
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_KeepScheduleSeqNo()
        {
            string strSql = $@"Select [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] 
                        From  [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] 
                        Where [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'N' or [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'R'
                        Order by [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}]  asc";

            return strSql;
        }


        /// <summary>
        /// 排程TabPage-紀錄原有修改時間
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_KeepScheduleUpdateTime()
        {
            string strSql = $@"Select [{nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)}] , [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] 
                        From [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}]
                        Where [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'N' or [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'R'
                        Order by [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}]  asc";

            return strSql;
        }


        /// <summary>
        /// 排程異動-修改時間比對
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_ScheduleMove_CheckUpdateTime(string Coil_ID)
        {
            string strSql = $@"Select [{nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)}] 
                        From [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}]
                        Where [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = '{Coil_ID }'";

            return strSql;
        }


        /// <summary>
        /// 修改排程的順序編號
        /// </summary>
        /// <param name="Seq_No"></param>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Update_ScheduleSeqNo(string Seq_No, string Coil_ID)
        {
            string strSql = $@"Update [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] 
                        Set [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}] = '{Seq_No}' ,
                            [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Update_Source)}] = '1',
                            [{nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)}] = '{PublicForms.Main.Lbl_Clock.Text}'
                        Where [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }

        public static string SQL_Select_SystemSetting_TopScheduleLock()
        {
            string strSql = $@"Select [{nameof(SystemSettingEntity.TBL_SystemSetting.Value)}] 
                        From [{nameof(SystemSettingEntity.TBL_SystemSetting)}]
                        Where [{nameof(SystemSettingEntity.TBL_SystemSetting.Parameter_Group)}] = '{GlobalVariableHandler.proLine}'
                        AND [{nameof(SystemSettingEntity.TBL_SystemSetting.Parameter)}] = 'TopScheduleLock' ";

            return strSql;
        }

        public static string SQL_Update_SystemSetting_TopScheduleLock(string strValue)
        {           
            string strSql = $@"Update [{nameof(SystemSettingEntity.TBL_SystemSetting)}] 
                        Set [{nameof(SystemSettingEntity.TBL_SystemSetting.Value)}] = '{strValue}'  
                        Where [{nameof(SystemSettingEntity.TBL_SystemSetting.Parameter_Group)}] = '{GlobalVariableHandler.proLine}'
                        AND [{nameof(SystemSettingEntity.TBL_SystemSetting.Parameter)}] = 'TopScheduleLock' ";
            return strSql;
        }

        #endregion


        #region ---匯入排程---

        /// <summary>
        /// 清空Schedule
        /// </summary>
        /// <returns></returns>
        public static string SQL_Clear_Schedule()
        {
            string strSql = $"Truncate table [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] ";

            return strSql;
        }

        /// <summary>
        /// 匯入排程
        /// </summary>
        /// <param name="Seq_No"></param>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Insert_ImportSchedule(int Seq_No, string Coil_ID)
        {
            string strSql = $@"Insert into [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}]
                                   ([{nameof(CoilScheduleEntity.TBL_Production_Schedule.Seq_No)}],
                                    [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}],
                                    [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Update_Source)}],
                                    [{nameof(CoilScheduleEntity.TBL_Production_Schedule.UpdateTime)}],
                                    [{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}])
                             Values('{Seq_No}','{Coil_ID}','1','{GlobalVariableHandler.Instance.getTime}','N')";
            return strSql;
        }

        #endregion


        #region ---匯入PDI---

        /// <summary>
        /// 匯入PDI-檢查是否已存在PDI
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_ImportPDI_CheckPDI(string Coil_ID,string Plan_No)
        {
            string strSql = $@"Select [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] 
                        From [{nameof(CoilPDIEntity.TBL_PDI)}] 
                        Where [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{Coil_ID}'
                        AND [{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}] = '{Plan_No}'";

            return strSql;
        }
        public static string SQL_Select_ImportPDI_CheckPDI_Def(string Coil_ID, string Plan_No)
        {
            string strSql = $@"Select [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Entry_Coil_ID)}] 
                        From [{nameof(CoilDefectDataEntity.TBL_Coil_Defect)}] 
                        Where [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Entry_Coil_ID)}] = '{Coil_ID}'
                        AND [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Plan_No)}] = '{Plan_No}'";

            return strSql;
        }

        /// <summary>
        /// 匯入PDI-修改
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="RowIndex"></param>
        /// <returns></returns>
        public static string SQL_Update_ImportPDI_PDI(DataTable dt, int RowIndex)
        {
            string strSql = $@"Update [{nameof(CoilPDIEntity.TBL_PDI)}] Set
                               [{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Plan_No)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Mat_Seq_No)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Mat_Seq_No)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Plan_Sort)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Plan_Sort)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Sleeve_Type_Code)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Sleeve_Type_Code)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Sleeve_diamter)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Sleeve_diamter)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Paper_Req_Code)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Paper_Req_Code)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Paper_Code)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Paper_Code)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Length)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Length)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Width)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Width)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Length)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Length)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Width)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Width)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Max)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Max)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Min)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Min)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.St_No)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.St_No)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Density)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Density)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.REPAIR_TYPE)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.REPAIR_TYPE)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Surface_Finishing_Code)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Finishing_Code)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Base_Surface)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Base_Surface)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Uncoiler_Direction)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Uncoiler_Direction)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Req_Code)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Req_Code)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Diamter)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Diamter)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Strap_Num)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Strap_Num)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Sample_Flag)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Sample_Flag)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Sample_Frqn_Code)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Sample_Frqn_Code)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Sample_Lot_No)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Sample_Lot_No)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Coil_Origin)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Coil_Origin)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Wholebacklog_Code)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Wholebacklog_Code)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Next_Wholebacklog_Code)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Next_Wholebacklog_Code)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Inner)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Inner)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Max)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Max)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Min)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Min)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Order_Wt)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_Wt)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Dividing_Num)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Dividing_Num)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_1)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Orderwt_1)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_2)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Orderwt_2)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_3)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Orderwt_3)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_4)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Orderwt_4)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_5)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Orderwt_5)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_6)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Orderwt_6)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No_1)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No_1)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No_2)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No_2)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No_3)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No_3)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No_4)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No_4)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No_5)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No_5)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No_6)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No_6)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Test_Plan_No)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Test_Plan_No)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Qc_Remark)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Qc_Remark)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Head_Off_Gauge)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Head_Off_Gauge)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Tail_Off_Gauge)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Tail_Off_Gauge)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Surface_Accu_Code_In)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Accu_Code_In)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Surface_Accu_Code_Out)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Accu_Code_Out)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Sg_Sign)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Sg_Sign)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Process_Code)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Process_Code)]}',

                               [{nameof(CoilPDIEntity.TBL_PDI.CustomerCode)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.CustomerCode)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.CustomerName_E)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.CustomerName_E)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.CustomerName_C)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.CustomerName_C)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Surface_Acc_Desc)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Acc_Desc)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy_Desc)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy_Desc)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Surface_Acc_Desc_In)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Acc_Desc_In)]}',
                               [{nameof(CoilPDIEntity.TBL_PDI.Surface_Acc_Desc_Out)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Acc_Desc_Out)]}'

                          Where [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)]}'
                          AND   [{ nameof(CoilPDIEntity.TBL_PDI.Plan_No)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Plan_No)]}'";

            return strSql;
        }


        /// <summary>
        /// 匯入PDI-缺陷修改
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="RowIndex"></param>
        /// <returns></returns>
        public static string SQL_Update_ImportPDI_DefectData(DataTable dt, int RowIndex)
        {
            string strSql = $@"Update [{nameof(CoilDefectDataEntity.TBL_Coil_Defect)}]
                           Set [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Code)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Code)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Code)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Code)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Code)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Code)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Code)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Code)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Code)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Code)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Code)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Code)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Code)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Code)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Code)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Code)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Code)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Code)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Code)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Code)]}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Origin)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Origin)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Origin)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Origin)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Origin)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Origin)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Origin)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Origin)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Origin)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Origin)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Origin)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Origin)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Origin)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Origin)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Origin)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Origin)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Origin)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Origin)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Origin)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Origin)]}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Sid)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Sid)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Sid)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Sid)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Sid)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Sid)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Sid)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Sid)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Sid)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Sid)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Sid)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Sid)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Sid)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Sid)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Sid)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Sid)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Sid)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Sid)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Sid)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Sid)]}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_W)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_W)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_W)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_W)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_W)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_W)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_W)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_W)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_W)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_W)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_W)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_W)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_W)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_W)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_W)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_W)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_W)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_W)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_W)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_W)]}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_Start)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_Start)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_Start)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_Start)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_Start)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_Start)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_Start)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_Start)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_Start)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_Start)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_Start)]}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_End)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_End)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_End)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_End)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_End)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_End)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_End)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_End)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_End)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_End)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_End)]}',
                              


                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Level)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Level)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Level)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Level)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Level)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Level)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Level)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Level)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Level)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Level)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Level)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Level)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Level)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Level)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Level)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Level)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Level)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Level)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Level)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Level)]}',

                              

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Percent)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Percent)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Percent)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Percent)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Percent)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Percent)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Percent)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Percent)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Percent)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Percent)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Percent)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Percent)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Percent)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Percent)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Percent)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Percent)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Percent)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Percent)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Percent)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Percent)]}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_QGRADE)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_QGRADE)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_QGRADE)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_QGRADE)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_QGRADE)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_QGRADE)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_QGRADE)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_QGRADE)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_QGRADE)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_QGRADE)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_QGRADE)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_QGRADE)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_QGRADE)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_QGRADE)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_QGRADE)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_QGRADE)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_QGRADE)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_QGRADE)]}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_QGRADE)}] = '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_QGRADE)]}',
                                
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Modify_UserID)}] = '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.ModifyTime)}] = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'
                         Where [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Coil_ID)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)]}'
                           AND [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Plan_No)}] = '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Plan_No)]}'";

            return strSql;
        }


        /// <summary>
        /// 匯入PDI-新增
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="RowIndex"></param>
        /// <returns></returns>
        public static string SQL_Insert_ImportPDI_PDI(DataTable dt, int RowIndex)
        {
            string strSql = $@" Insert into [{nameof(CoilPDIEntity.TBL_PDI)}] (
                                     [{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Mat_Seq_No)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Plan_Sort)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)}] ,
                                     [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)}] ,
                                     [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Sleeve_Type_Code)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Sleeve_diamter)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Paper_Req_Code)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Paper_Code)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Length)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Width)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Length)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Width)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Max)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Min)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.St_No)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Density)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.REPAIR_TYPE)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Surface_Finishing_Code)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Base_Surface)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Uncoiler_Direction)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Req_Code)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Diamter)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Out_Strap_Num)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Sample_Flag)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Sample_Frqn_Code)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Sample_Lot_No)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Coil_Origin)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Wholebacklog_Code)}] ,
                                     [{nameof(CoilPDIEntity.TBL_PDI.Next_Wholebacklog_Code)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)}] ,
                                     [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)}] ,
                                     [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness)}] ,
                                     [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Inner)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Order_No)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Max)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Min)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Order_Wt)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Dividing_Num)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_1)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_2)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_3)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_4)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_5)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_6)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Order_No_1)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Order_No_2)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Order_No_3)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Order_No_4)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Order_No_5)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Order_No_6)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Test_Plan_No)}] ,
                                     [{nameof(CoilPDIEntity.TBL_PDI.Qc_Remark)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Head_Off_Gauge)}] ,
                                     [{nameof(CoilPDIEntity.TBL_PDI.Tail_Off_Gauge)}] ,
                                     [{nameof(CoilPDIEntity.TBL_PDI.Surface_Accu_Code_In)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Surface_Accu_Code_Out)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Sg_Sign)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Process_Code)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.CustomerCode)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.CustomerName_E)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.CustomerName_C)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Surface_Acc_Desc)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy_Desc)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Surface_Acc_Desc_In)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Surface_Acc_Desc_Out)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Is_Delete)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.Create_UserID)}],
                                     [{nameof(CoilPDIEntity.TBL_PDI.CreateTime)}])
                                Values(
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Plan_No)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Mat_Seq_No)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Plan_Sort)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Sleeve_Type_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Sleeve_diamter)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Paper_Req_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Paper_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Length)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Width)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Length)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Width)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Max)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Min)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.St_No)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Density)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.REPAIR_TYPE)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Finishing_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Base_Surface)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Uncoiler_Direction)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Req_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Diamter)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Strap_Num)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Sample_Flag)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Sample_Frqn_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Sample_Lot_No)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Coil_Origin)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Wholebacklog_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Next_Wholebacklog_Code)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Inner)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Max)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Min)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_Wt)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Dividing_Num)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Orderwt_1)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Orderwt_2)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Orderwt_3)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Orderwt_4)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Orderwt_5)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Orderwt_6)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No_1)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No_2)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No_3)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No_4)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No_5)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Order_No_6)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Test_Plan_No)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Qc_Remark)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Head_Off_Gauge)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Tail_Off_Gauge)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Accu_Code_In)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Accu_Code_Out)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Sg_Sign)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Process_Code)]}',
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.CustomerCode)]}',
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.CustomerName_E)]}',
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.CustomerName_C)]}',
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Acc_Desc)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy_Desc)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Acc_Desc_In)]}' ,
                                 '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Surface_Acc_Desc_Out)]}' ,
                                 '0' , 
                                 '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                 '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";

            return strSql;
        }


        /// <summary>
        /// 新增缺陷資料
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="RowIndex"></param>
        /// <returns></returns>
        public static string SQL_Insert_ImportPDI_DefectData(DataTable dt, int RowIndex)
        {
            string strSql = $@"Insert into [{nameof(CoilDefectDataEntity.TBL_Coil_Defect)}] (

                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Coil_ID)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Plan_No)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Entry_Coil_ID)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Modify_UserID)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.ModifyTime)}],

                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Code)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Code)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Code)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Code)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Code)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Code)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Code)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Code)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Code)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Code)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Origin)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Origin)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Origin)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Origin)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Origin)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Origin)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Origin)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Origin)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Origin)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Origin)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Sid)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Sid)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Sid)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Sid)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Sid)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Sid)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Sid)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Sid)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Sid)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Sid)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_W)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_W)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_W)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_W)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_W)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_W)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_W)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_W)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_W)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_W)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_Start)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_Start)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_Start)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_Start)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_Start)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_Start)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_Start)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_Start)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_Start)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_Start)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_End)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_End)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_End)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_End)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_End)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_End)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_End)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_End)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_End)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_End)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Level)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Level)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Level)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Level)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Level)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Level)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Level)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Level)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Level)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Level)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Percent)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Percent)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Percent)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Percent)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Percent)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Percent)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Percent)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Percent)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Percent)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Percent)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_QGRADE)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_QGRADE)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_QGRADE)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_QGRADE)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_QGRADE)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_QGRADE)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_QGRADE)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_QGRADE)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_QGRADE)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_QGRADE)}])
                             Values ('{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Plan_No)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)]}',
                                    '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                    '{DateTime.Now:yyyy-MM-dd HH:mm:ss}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Code)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Code)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Code)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Code)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Code)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Code)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Code)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Code)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Code)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Code)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Origin)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Origin)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Origin)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Origin)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Origin)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Origin)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Origin)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Origin)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Origin)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Origin)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Sid)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Sid)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Sid)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Sid)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Sid)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Sid)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Sid)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Sid)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Sid)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Sid)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_W)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_W)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_W)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_W)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_W)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_W)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_W)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_W)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_W)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_W)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_Start)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_Start)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_Start)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_Start)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_Start)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_Start)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_Start)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_Start)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_Start)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_Start)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_End)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_End)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_End)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_End)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_End)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_End)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_End)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_End)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_End)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_End)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Level)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Level)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Level)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Level)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Level)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Level)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Level)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Level)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Level)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Level)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Percent)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Percent)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Percent)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Percent)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Percent)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Percent)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Percent)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Percent)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Percent)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Percent)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_QGRADE)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_QGRADE)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_QGRADE)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_QGRADE)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_QGRADE)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_QGRADE)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_QGRADE)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_QGRADE)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_QGRADE)]}',
                                    '{dt.Rows[RowIndex][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_QGRADE)]}')";

            return strSql;
        }


        #endregion


        #region ---钢卷资讯查询---

        /// <summary>
        /// 查詢TabPage-條件查詢
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_PageCoilInfo_CoilData()
        {
            string strSql = $@"Select 
                        ROW_NUMBER() OVER( ORDER BY   a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]) AS No,
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Sleeve_Type_Code)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Sleeve_diamter)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Paper_Req_Code)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Paper_Code)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Length)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Width)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Length)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Width)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Max)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Min)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.St_No)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Density)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.REPAIR_TYPE)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Surface_Finishing_Code)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Base_Surface)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Uncoiler_Direction)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Req_Code)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Diamter)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Strap_Num)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Sample_Flag)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Sample_Frqn_Code)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Sample_Lot_No)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Wholebacklog_Code)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Next_Wholebacklog_Code)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Inner)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Max)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Min)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Order_Wt)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Dividing_Num)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_1)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_2)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_3)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_4)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_5)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Orderwt_6)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_1)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_2)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_3)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_4)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_5)}],
                        a.[{nameof(CoilPDIEntity.TBL_PDI.Order_No_6)}]";

            //查詢生產完成
            if (PublicForms.PDISchl.Chk_Status.Checked)
            {
                strSql += $@"From  [{nameof(CoilPDIEntity.TBL_PDI)}] a
                             Right Join [TBL_PDO] b 
                             On a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = b.[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] ";
            }
            //查詢已上線
            else if (PublicForms.PDISchl.Chk_Online.Checked)
            {
                #region SQL
                strSql += $@"From  [TBL_CoilMap]  b 
                             Left join  [{nameof(CoilPDIEntity.TBL_PDI)}] a on  a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_SK01)}]
                             Or a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_SK02)}]
                             Or a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.Entry_TOP)}]
                             Or a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.POR)}]
                             Or a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK01)}]
                             Or a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK02)}]
                             Or a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.Delivery_TOP)}]
                             Or a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = b.[{nameof(CoilMapEntity.TBL_CoilMap.TR)}] ";
                #endregion
            }
            else
            {
                strSql += $" from  [{nameof(CoilPDIEntity.TBL_PDI)}] a";
            }

            if (PublicForms.PDISchl.Ckb_Plan_No.Checked && !PublicForms.PDISchl.Ckb_Entry_Coil_No.Checked)
            {
                strSql += $@" where a.[{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}] = '{PublicForms.PDISchl.Cob_Plan_No.Text }' "; //單選計畫號
            }
            else if (PublicForms.PDISchl.Ckb_Entry_Coil_No.Checked && !PublicForms.PDISchl.Ckb_Plan_No.Checked)
            {
                strSql += $@" where a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] ='{PublicForms.PDISchl.Cob_Entry_Coil_No.Text}'"; //單選入口鋼卷號
            }
            else if (PublicForms.PDISchl.Ckb_Entry_Coil_No.Checked && PublicForms.PDISchl.Ckb_Plan_No.Checked)
            {
                strSql += $@" where a.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] ='{PublicForms.PDISchl.Cob_Entry_Coil_No.Text}'
                             And a.[{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}] = '{PublicForms.PDISchl.Cob_Plan_No.Text }'"; //計畫號及入口捲號
            }

            return strSql;
        }


        #endregion


        #region ---ComboBox---


        /// <summary>
        /// 查詢TabPage-ComboBox選項
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static string SQL_Select_ComboBoxItems(string columns)
        {
            string strSql = $@"select {columns} from [{nameof(CoilPDIEntity.TBL_PDI)}] ";

            return strSql;
        }



        #endregion


        #region --- Form_SleevePaper ---

        /// <summary>
        /// strCount = Out_Paper_Code or Out_Sleeve_Type_Code
        /// </summary>
        /// <param name="strCount"></param>
        /// <returns></returns>
        public static string SQL_Select_SleevePaperCount(string strCount)
        {
            string strSql = "";
            strSql = $@"Select 
                        {strCount},Count(*) As 'Count'                        
                         From [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] a
                        --
                        Left join (Select MAX({nameof(CoilPDIEntity.TBL_PDI.CreateTime)}) as LastCreateTime ,{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)} 
                                   From {nameof(CoilPDIEntity.TBL_PDI)} Group by {nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}) as  temp
                             on a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] =  temp.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] 					
                        Left join  [{nameof(CoilPDIEntity.TBL_PDI)}] b on b.[{nameof(CoilPDIEntity.TBL_PDI.CreateTime)}] = temp.[LastCreateTime] AND b.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = temp.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]

                        Where a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'N' or a.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Schedule_Status)}] = 'R'
                        AND {strCount} != '00'
                       --
                        Group by {strCount} 
                        ";


            return strSql;
        }
        /// <summary>
        /// 套筒種類統計
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_SleeveCount()
        {
            string strSql = $@"Select pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}],Count(*) As 'Count' 
                               From [{nameof(CoilPDIEntity.TBL_PDI)}] pdi
                               Left Join [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] schedule 
                               On schedule.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
                               Where pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}] is not null
                               And pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}] <> ''
                               Group by  pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}]";

            return strSql;
        }

        /// <summary>
        /// 墊紙種類統計
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_PaperCount()
        {
            string strSql = $@"Select pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code)}],Count(*) As 'Count' 
                               From [{nameof(CoilPDIEntity.TBL_PDI)}] pdi
                               Left Join [{nameof(CoilScheduleEntity.TBL_Production_Schedule)}] schedule 
                               On schedule.[{nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)}] = pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
                               Where pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code)}] is not null
                               And pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code)}] <> ''
                               Group by  pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code)}]";

            return strSql;
        }


        #endregion


        #region ---留存---

        public static string Frm_1_1_SelectDeleteScheduleRecord(string Coil_ID)
        {
            string strSql = $@"Select [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Coil_ID)}] 
                                 From [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp)}] 
                                Where [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }


        #endregion

    }
}
