using DBService.Repository;
using DBService.Repository.DefectData;
using DBService.Repository.PDI;
using DBService.Repository.ScheduleDelete_CoilReject_Record_Temp;
using DBService.Repository.UnmountRecord;
using System;

namespace CPL1HMI
{
    public class Frm_DialogReject_SqlFactory
    {

        #region --- 搜尋 ---

        /// <summary>
        /// 檢查退料紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_CoilRejectResultCheck(string Coil_ID)
        {
            string strSql = $@"Select [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Reject_Coil_ID)}] 
                        From [{nameof(CoilRejResultEntity.TBL_CoilRejectResult)}] 
                        Where [{nameof(CoilRejResultEntity.TBL_CoilRejectResult.Reject_Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }


        /// <summary>
        /// 取得出口捲號
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_Out_Coil_ID(string Coil_ID)
        {
            string strSql = $@"Select [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)}]
                                     ,[{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}]
                                 From [{nameof(CoilPDIEntity.TBL_PDI)}] 
                                Where [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{Coil_ID}'
                             ORDER BY [{ nameof(CoilPDIEntity.TBL_PDI.CreateTime)}] DESC";
            return strSql;
        }


        /// <summary>
        /// 搜尋母捲是否有斷帶鋼卷資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_BrakeStripRecordCheck(string Coil_ID)
        {
            string strSql = $@"Select Unm.*,  pdi.*
                                 From [{nameof(UnmountRecordEntity.TBL_UnmountRecord)}] Unm
                            Left Join [{nameof(CoilPDIEntity.TBL_PDI)}] pdi 
                                   On Unm.[{nameof(UnmountRecordEntity.TBL_UnmountRecord.Coil_ID)}] = pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)}]
                                Where Unm.[{nameof(UnmountRecordEntity.TBL_UnmountRecord.Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }


        /// <summary>
        /// 母捲有斷帶，取得TBL_ScheduleDelete_CoilReject_Temp资料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_BrakeStripRejectData(string Coil_ID)
        {
            string strSql = $@"Select CoilReject.[{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Length_Of_Rejected_Coil)}], 
                                      CoilReject.[{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Weight_Of_Rejected_Coil)}], 
                                      CoilReject.[{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Inner_Diameter_Of_RejectedCoil)}], 
                                      CoilReject.[{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Outer_Diameter_Of_RejectedCoil)}], 
                                      pdi.*
                                 From [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp)}] CoilReject
                            Left Join [{nameof(CoilPDIEntity.TBL_PDI)}] pdi 
                                   On CoilReject.[{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.OriPDI_Out_Coil_ID)}] = pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)}]                      

                                Where CoilReject.[{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Coil_ID)}] = '{Coil_ID}'
                                ORDER BY [{ nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.UpdateTime)}] DESC";
            return strSql;
        }


        /// <summary>
        /// 無斷帶，搜尋退料鋼卷PDI
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_Coil_PDI_Data(string Coil_ID)
        {
            string strSql = $@"Select pdi.*
                        From [{nameof(CoilPDIEntity.TBL_PDI)}] pdi                        
                        Where pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{Coil_ID}'
                        ORDER BY pdi.[{ nameof(CoilPDIEntity.TBL_PDI.CreateTime)}] DESC";
            return strSql;
            //分開Select比較快
            //string strSql = $@"Select pdi.*,Defect.*
            //            From [{nameof(CoilPDIEntity.TBL_PDI)}] pdi 
            //            Left Join [{nameof(CoilDefectDataEntity.TBL_Coil_Defect)}] Defect 
            //            On Defect.[{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Entry_Coil_ID)}] = pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]
            //            And Defect.[{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Plan_No)}] = pdi.[{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}]
            //            Where pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{Coil_ID}'";


        }

        /// <summary>
        /// 無斷帶，搜尋退料鋼卷PDI
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_Coil_Defect_Data(string strCoil_ID,string strPlan_no)
        {
            string strSql = $@"Select Defect.*
                        From [{nameof(CoilDefectDataEntity.TBL_Coil_Defect)}] Defect                      
                        Where Defect.[{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Entry_Coil_ID)}] = '{strCoil_ID}'
                        And Defect.[{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Plan_No)}] =  '{strPlan_no}' ";

            return strSql;
        }

        /// <summary>
        /// 搜尋退料暫存紀錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_CoilRejectTemp(string Coil_ID)
        {
            //不設主鍵,以 [Coil_ID] 最新一筆[CreateTime] 為主
            string strSql = $@"Select * 
                               From [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp)}] 
                              Where [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Coil_ID)}] = '{Coil_ID }'
                           Order by [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.CreateTime)}] DESC";

            return strSql;
        }


        /// <summary>
        /// 搜尋退料鋼捲缺陷
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_RejectCoilDefectData(string Coil_ID)
        {
            string strSql = $@"Select * From [{nameof(CoilDefectDataEntity.TBL_Coil_Defect)}] Where [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }

        #endregion


        #region --- Funtion ---

        /// <summary>
        /// 新增退料暫存記錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Insert_CoilRejectTemp(string Skid)
        {
            string strSql = $@"Insert into [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp)}] (
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Coil_ID)}],
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.OriPDI_Out_Coil_ID)}],
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Record_Type)}],
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Entry_Coil_ID)}],
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Plan_No)}],
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Mode_Of_Reject)}],
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Length_Of_Rejected_Coil)}],
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Weight_Of_Rejected_Coil)}],
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Inner_Diameter_Of_RejectedCoil)}],
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Outer_Diameter_Of_RejectedCoil)}],
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Width_Of_RejectedCoil)}],
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
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Create_UserID)}],
                                    [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.CreateTime)}] )
                              Values('{PublicForms.DialogReject.Txt_Coil_ID.Text.Trim()}',
                                    '{PublicForms.DialogReject.OriPDI_Out_Coil_ID}',
                                    'C',
                                    '{PublicForms.DialogReject.Txt_Entry_Coil_ID.Text.Trim()}',
                                    '{PublicForms.DialogReject.Txt_Plan_No.Text.Trim()}',
                                    '{PublicForms.DialogReject.Cob_Mode_Of_Reject.SelectedValue}',
                                    '{PublicForms.DialogReject.Txt_Length_Of_Rejected_Coil.Text.Trim()}',
                                    '{PublicForms.DialogReject.Txt_Weight_Of_Rejected_Coil.Text.Trim()}',
                                    '{PublicForms.DialogReject.Txt_Inner_Diameter_Of_RejectedCoil.Text.Trim()}',
                                    '{PublicForms.DialogReject.Txt_Outer_Diameter_Of_RejectedCoil.Text.Trim()}',
                                    '',
                                    '{PublicForms.DialogReject.Cob_Reason_Of_Reject.SelectedValue}',
                                    '{PublicForms.DialogReject.Dtp_Time_Of_Reject.Value:yyyyMMddHHmmss}',
                                    '{PublicForms.DialogReject.Cob_Shift_Of_Reject.SelectedValue}',
                                    '{PublicForms.DialogReject.Cob_Turn_Of_Reject.SelectedValue}',
                                    '{PublicForms.DialogReject.Cob_Paper_exit_Code.SelectedValue}',
                                    '{PublicForms.DialogReject.Cob_Paper_Type.SelectedValue}',
                                    '{PublicForms.DialogReject.Cob_FINAL_COIL_FLAG.SelectedValue}',

                                    '{PublicForms.DialogReject.Txt_HEAD_PAPER_LENGTH.Text.Trim()}',
                                    '{PublicForms.DialogReject.Txt_HEAD_PAPER_WIDTH.Text.Trim()}',
                                    '{PublicForms.DialogReject.Txt_TAIL_PAPER_LENGTH.Text.Trim()}',
                                    '{PublicForms.DialogReject.Txt_TAIL_PAPER_WIDTH.Text.Trim()}',

                                    '{Skid}',
                                    '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                    '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";

            return strSql;
        }


        /// <summary>
        /// 新增退料鋼捲缺陷
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_RejectCoilDefectData()
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
                             Values ('{PublicForms.DialogReject.Txt_Coil_ID.Text.Trim()}',
                                     '{PublicForms.DialogReject.Txt_Plan_No.Text.Trim()}',
                                    '{PublicForms.DialogReject.Txt_Entry_Coil_ID.Text.Trim()}',
                                    '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                    '{DateTime.Now:yyyy-MM-dd HH:mm:ss}',
                                    '{PublicForms.DialogReject.txtCode_D01.Text}',
                                    '{PublicForms.DialogReject.txtCode_D02.Text}',
                                    '{PublicForms.DialogReject.txtCode_D03.Text}',
                                    '{PublicForms.DialogReject.txtCode_D04.Text}',
                                    '{PublicForms.DialogReject.txtCode_D05.Text}',
                                    '{PublicForms.DialogReject.txtCode_D06.Text}',
                                    '{PublicForms.DialogReject.txtCode_D07.Text}',
                                    '{PublicForms.DialogReject.txtCode_D08.Text}',
                                    '{PublicForms.DialogReject.txtCode_D09.Text}',
                                    '{PublicForms.DialogReject.txtCode_D10.Text}',
                                    '{PublicForms.DialogReject.txtOrigin_D01.Text}',
                                    '{PublicForms.DialogReject.txtOrigin_D02.Text}',
                                    '{PublicForms.DialogReject.txtOrigin_D03.Text}',
                                    '{PublicForms.DialogReject.txtOrigin_D04.Text}',
                                    '{PublicForms.DialogReject.txtOrigin_D05.Text}',
                                    '{PublicForms.DialogReject.txtOrigin_D06.Text}',
                                    '{PublicForms.DialogReject.txtOrigin_D07.Text}',
                                    '{PublicForms.DialogReject.txtOrigin_D08.Text}',
                                    '{PublicForms.DialogReject.txtOrigin_D09.Text}',
                                    '{PublicForms.DialogReject.txtOrigin_D10.Text}',
                                    '{PublicForms.DialogReject.cbo_Sid_D01.Text}',
                                    '{PublicForms.DialogReject.cbo_Sid_D02.Text}',
                                    '{PublicForms.DialogReject.cbo_Sid_D03.Text}',
                                    '{PublicForms.DialogReject.cbo_Sid_D04.Text}',
                                    '{PublicForms.DialogReject.cbo_Sid_D05.Text}',
                                    '{PublicForms.DialogReject.cbo_Sid_D06.Text}',
                                    '{PublicForms.DialogReject.cbo_Sid_D07.Text}',
                                    '{PublicForms.DialogReject.cbo_Sid_D08.Text}',
                                    '{PublicForms.DialogReject.cbo_Sid_D09.Text}',
                                    '{PublicForms.DialogReject.cbo_Sid_D10.Text}',
                                    '{PublicForms.DialogReject.cbo_PosW_D01.Text}',
                                    '{PublicForms.DialogReject.cbo_PosW_D02.Text}',
                                    '{PublicForms.DialogReject.cbo_PosW_D03.Text}',
                                    '{PublicForms.DialogReject.cbo_PosW_D04.Text}',
                                    '{PublicForms.DialogReject.cbo_PosW_D05.Text}',
                                    '{PublicForms.DialogReject.cbo_PosW_D06.Text}',
                                    '{PublicForms.DialogReject.cbo_PosW_D07.Text}',
                                    '{PublicForms.DialogReject.cbo_PosW_D08.Text}',
                                    '{PublicForms.DialogReject.cbo_PosW_D09.Text}',
                                    '{PublicForms.DialogReject.cbo_PosW_D10.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_Start_D01.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_Start_D02.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_Start_D03.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_Start_D04.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_Start_D05.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_Start_D06.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_Start_D07.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_Start_D08.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_Start_D09.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_Start_D10.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_End_D01.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_End_D02.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_End_D03.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_End_D04.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_End_D05.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_End_D06.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_End_D07.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_End_D08.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_End_D09.Text}',
                                    '{PublicForms.DialogReject.txtPos_L_End_D10.Text}',
                                    '{PublicForms.DialogReject.cbo_Level_D01.Text}',
                                    '{PublicForms.DialogReject.cbo_Level_D02.Text}',
                                    '{PublicForms.DialogReject.cbo_Level_D03.Text}',
                                    '{PublicForms.DialogReject.cbo_Level_D04.Text}',
                                    '{PublicForms.DialogReject.cbo_Level_D05.Text}',
                                    '{PublicForms.DialogReject.cbo_Level_D06.Text}',
                                    '{PublicForms.DialogReject.cbo_Level_D07.Text}',
                                    '{PublicForms.DialogReject.cbo_Level_D08.Text}',
                                    '{PublicForms.DialogReject.cbo_Level_D09.Text}',
                                    '{PublicForms.DialogReject.cbo_Level_D10.Text}',
                                    '{PublicForms.DialogReject.txtPercent_D01.Text}',
                                    '{PublicForms.DialogReject.txtPercent_D02.Text}',
                                    '{PublicForms.DialogReject.txtPercent_D03.Text}',
                                    '{PublicForms.DialogReject.txtPercent_D04.Text}',
                                    '{PublicForms.DialogReject.txtPercent_D05.Text}',
                                    '{PublicForms.DialogReject.txtPercent_D06.Text}',
                                    '{PublicForms.DialogReject.txtPercent_D07.Text}',
                                    '{PublicForms.DialogReject.txtPercent_D08.Text}',
                                    '{PublicForms.DialogReject.txtPercent_D09.Text}',
                                    '{PublicForms.DialogReject.txtPercent_D10.Text}',
                                    '{PublicForms.DialogReject.txtQGRADE_D01.Text}',
                                    '{PublicForms.DialogReject.txtQGRADE_D02.Text}',
                                    '{PublicForms.DialogReject.txtQGRADE_D03.Text}',
                                    '{PublicForms.DialogReject.txtQGRADE_D04.Text}',
                                    '{PublicForms.DialogReject.txtQGRADE_D05.Text}',
                                    '{PublicForms.DialogReject.txtQGRADE_D06.Text}',
                                    '{PublicForms.DialogReject.txtQGRADE_D07.Text}',
                                    '{PublicForms.DialogReject.txtQGRADE_D08.Text}',
                                    '{PublicForms.DialogReject.txtQGRADE_D09.Text}',
                                    '{PublicForms.DialogReject.txtQGRADE_D10.Text}')";

            return strSql;
        }


        /// <summary>Txt_Entry_Coil_ID
        /// 修改退料暫存記錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Update_CoilRejectTemp(string Coil_ID, string Skid)
        {
            string strSql = $@"Update [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp)}]
                           Set [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Entry_Coil_ID)}] = '{PublicForms.DialogReject.Txt_Entry_Coil_ID.Text.Trim()}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.OriPDI_Out_Coil_ID)}] = '{PublicForms.DialogReject.OriPDI_Out_Coil_ID}',

                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Plan_No)}] = '{PublicForms.DialogReject.Txt_Plan_No.Text.Trim()}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Mode_Of_Reject)}] = '{PublicForms.DialogReject.Cob_Mode_Of_Reject.SelectedValue}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Length_Of_Rejected_Coil)}] = '{PublicForms.DialogReject.Txt_Length_Of_Rejected_Coil.Text.Trim()}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Weight_Of_Rejected_Coil)}] = '{PublicForms.DialogReject.Txt_Weight_Of_Rejected_Coil.Text.Trim()}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Inner_Diameter_Of_RejectedCoil)}] = '{PublicForms.DialogReject.Txt_Inner_Diameter_Of_RejectedCoil.Text.Trim()}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Outer_Diameter_Of_RejectedCoil)}] = '{PublicForms.DialogReject.Txt_Outer_Diameter_Of_RejectedCoil.Text.Trim()}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Width_Of_RejectedCoil)}] = '',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Reason_Of_Reject)}] = '{PublicForms.DialogReject.Cob_Reason_Of_Reject.SelectedValue}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Time_Of_Reject)}] = '{PublicForms.DialogReject.Dtp_Time_Of_Reject.Value:yyyyMMddHHmmss}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Shift_Of_Reject)}] = '{PublicForms.DialogReject.Cob_Shift_Of_Reject.SelectedValue}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Turn_Of_Reject)}] = '{PublicForms.DialogReject.Cob_Turn_Of_Reject.SelectedValue}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Paper_exit_Code)}] = '{PublicForms.DialogReject.Cob_Paper_exit_Code.SelectedValue}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Paper_Type)}] = '{PublicForms.DialogReject.Cob_Paper_Type.SelectedValue}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.FINAL_COIL_FLAG)}] = '{PublicForms.DialogReject.Cob_FINAL_COIL_FLAG.SelectedValue}',

                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.HEAD_PAPER_LENGTH)}] = '{PublicForms.DialogReject.Txt_HEAD_PAPER_LENGTH.Text.Trim()}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.HEAD_PAPER_WIDTH)}] = '{PublicForms.DialogReject.Txt_HEAD_PAPER_WIDTH.Text.Trim()}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.TAIL_PAPER_LENGTH)}] = '{PublicForms.DialogReject.Txt_TAIL_PAPER_LENGTH.Text.Trim()}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.TAIL_PAPER_WIDTH)}] = '{PublicForms.DialogReject.Txt_TAIL_PAPER_WIDTH.Text.Trim()}',

                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Reject_Skid)}] = '{Skid}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Create_UserID)}] = '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                               [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.UpdateTime)}] = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'
                         Where [{nameof(ScheduleDeleteRecordTempEntity.TBL_ScheduleDelete_CoilReject_Temp.Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }


        /// <summary>
        /// 修改退料鋼捲缺陷資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Update_RejectCoilDefectData(string Coil_ID,string strPlan_No = "")
        {
            string strSql = $@"Update [{nameof(CoilDefectDataEntity.TBL_Coil_Defect)}] 
                           Set [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Code)}] = '{PublicForms.DialogReject.txtCode_D01.Text.Trim()}',         
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Origin)}] = '{PublicForms.DialogReject.txtOrigin_D01.Text.Trim()}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Sid)}] = '{PublicForms.DialogReject.cbo_Sid_D01.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_W)}] = '{PublicForms.DialogReject.cbo_PosW_D01.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_Start)}] = '{PublicForms.DialogReject.txtPos_L_Start_D01.Text.Trim()}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_End)}] = '{PublicForms.DialogReject.txtPos_L_End_D01.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Level)}] = '{PublicForms.DialogReject.cbo_Level_D01.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Percent)}] = '{PublicForms.DialogReject.txtPercent_D01.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_QGRADE)}] = '{PublicForms.DialogReject.txtQGRADE_D01.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Code)}] = '{PublicForms.DialogReject.txtCode_D02.Text.Trim()}', 
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Origin)}] = '{PublicForms.DialogReject.txtOrigin_D02.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Sid)}] = '{PublicForms.DialogReject.cbo_Sid_D02.Text}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_W)}] = '{PublicForms.DialogReject.cbo_PosW_D02.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_Start)}] = '{PublicForms.DialogReject.txtPos_L_Start_D02.Text.Trim()}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_End)}] = '{PublicForms.DialogReject.txtPos_L_End_D02.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Level)}] = '{PublicForms.DialogReject.cbo_Level_D02.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Percent)}] = '{PublicForms.DialogReject.txtPercent_D02.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_QGRADE)}] = '{PublicForms.DialogReject.txtQGRADE_D02.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Code)}] = '{PublicForms.DialogReject.txtCode_D03.Text.Trim()}', 
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Origin)}] = '{PublicForms.DialogReject.txtOrigin_D03.Text.Trim()}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Sid)}] = '{PublicForms.DialogReject.cbo_Sid_D03.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_W)}] = '{PublicForms.DialogReject.cbo_PosW_D03.Text}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_Start)}] = '{PublicForms.DialogReject.txtPos_L_Start_D03.Text.Trim()}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_End)}] = '{PublicForms.DialogReject.txtPos_L_End_D03.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Level)}] = '{PublicForms.DialogReject.cbo_Level_D03.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Percent)}] = '{PublicForms.DialogReject.txtPercent_D03.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_QGRADE)}] = '{PublicForms.DialogReject.txtQGRADE_D03.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Code)}] = '{PublicForms.DialogReject.txtCode_D04.Text.Trim()}',         
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Origin)}] = '{PublicForms.DialogReject.txtOrigin_D04.Text.Trim()}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Sid)}] = '{PublicForms.DialogReject.cbo_Sid_D04.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_W)}] = '{PublicForms.DialogReject.cbo_PosW_D04.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_Start)}] = '{PublicForms.DialogReject.txtPos_L_Start_D04.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_End)}] = '{PublicForms.DialogReject.txtPos_L_End_D04.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Level)}] = '{PublicForms.DialogReject.cbo_Level_D04.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Percent)}] = '{PublicForms.DialogReject.txtPercent_D04.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_QGRADE)}] = '{PublicForms.DialogReject.txtQGRADE_D04.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Code)}] = '{PublicForms.DialogReject.txtCode_D05.Text.Trim()}',       
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Origin)}] = '{PublicForms.DialogReject.txtOrigin_D05.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Sid)}] = '{PublicForms.DialogReject.cbo_Sid_D05.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_W)}] = '{PublicForms.DialogReject.cbo_PosW_D05.Text}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_Start)}] = '{PublicForms.DialogReject.txtPos_L_Start_D05.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_End)}] = '{PublicForms.DialogReject.txtPos_L_End_D05.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Level)}] = '{PublicForms.DialogReject.cbo_Level_D05.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Percent)}] = '{PublicForms.DialogReject.txtPercent_D05.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_QGRADE)}] = '{PublicForms.DialogReject.txtQGRADE_D05.Text.Trim()}', 

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Code)}] = '{PublicForms.DialogReject.txtCode_D06.Text.Trim()}',        
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Origin)}] = '{PublicForms.DialogReject.txtOrigin_D06.Text.Trim()}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Sid)}] = '{PublicForms.DialogReject.cbo_Sid_D06.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_W)}] = '{PublicForms.DialogReject.cbo_PosW_D06.Text}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_Start)}] = '{PublicForms.DialogReject.txtPos_L_Start_D06.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_End)}] = '{PublicForms.DialogReject.txtPos_L_End_D06.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Level)}] = '{PublicForms.DialogReject.cbo_Level_D06.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Percent)}] = '{PublicForms.DialogReject.txtPercent_D06.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_QGRADE)}] = '{PublicForms.DialogReject.txtQGRADE_D06.Text.Trim()}',   

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Code)}] = '{PublicForms.DialogReject.txtCode_D07.Text.Trim()}',        
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Origin)}] = '{PublicForms.DialogReject.txtOrigin_D07.Text.Trim()}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Sid)}] = '{PublicForms.DialogReject.cbo_Sid_D07.Text}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_W)}] = '{PublicForms.DialogReject.cbo_PosW_D07.Text}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_Start)}] = '{PublicForms.DialogReject.txtPos_L_Start_D07.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_End)}] = '{PublicForms.DialogReject.txtPos_L_End_D07.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Level)}] = '{PublicForms.DialogReject.cbo_Level_D07.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Percent)}] = '{PublicForms.DialogReject.txtPercent_D07.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_QGRADE)}] = '{PublicForms.DialogReject.txtQGRADE_D07.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Code)}] = '{PublicForms.DialogReject.txtCode_D08.Text.Trim()}',      
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Origin)}] = '{PublicForms.DialogReject.txtOrigin_D08.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Sid)}] = '{PublicForms.DialogReject.cbo_Sid_D08.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_W)}] = '{PublicForms.DialogReject.cbo_PosW_D08.Text}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_Start)}] = '{PublicForms.DialogReject.txtPos_L_Start_D08.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_End)}] = '{PublicForms.DialogReject.txtPos_L_End_D08.Text.Trim()}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Level)}] = '{PublicForms.DialogReject.cbo_Level_D08.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Percent)}] = '{PublicForms.DialogReject.txtPercent_D08.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_QGRADE)}] = '{PublicForms.DialogReject.txtQGRADE_D08.Text.Trim()}',  

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Code)}] = '{PublicForms.DialogReject.txtCode_D09.Text.Trim()}',         
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Origin)}] = '{PublicForms.DialogReject.txtOrigin_D09.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Sid)}] = '{PublicForms.DialogReject.cbo_Sid_D09.Text}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_W)}] = '{PublicForms.DialogReject.cbo_PosW_D09.Text}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_Start)}] = '{PublicForms.DialogReject.txtPos_L_Start_D09.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_End)}] = '{PublicForms.DialogReject.txtPos_L_End_D09.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Level)}] = '{PublicForms.DialogReject.cbo_Level_D09.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Percent)}] = '{PublicForms.DialogReject.txtPercent_D09.Text.Trim()}', 
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_QGRADE)}] = '{PublicForms.DialogReject.txtQGRADE_D09.Text.Trim()}',  

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Code)}] = '{PublicForms.DialogReject.txtCode_D10.Text.Trim()}',          
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Origin)}] = '{PublicForms.DialogReject.txtOrigin_D10.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Sid)}] = '{PublicForms.DialogReject.cbo_Sid_D10.Text}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_W)}] = '{PublicForms.DialogReject.cbo_PosW_D10.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_Start)}] = '{PublicForms.DialogReject.txtPos_L_Start_D10.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_End)}] = '{PublicForms.DialogReject.txtPos_L_End_D10.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Level)}] = '{PublicForms.DialogReject.cbo_Level_D10.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Percent)}] = '{PublicForms.DialogReject.txtPercent_D10.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_QGRADE)}] = '{PublicForms.DialogReject.txtQGRADE_D10.Text.Trim()}'
                              ,[{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Modify_UserID)}] = '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}'
                              ,[{nameof(CoilDefectDataEntity.TBL_Coil_Defect.ModifyTime)}] = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'
                         Where [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Entry_Coil_ID)}] = '{Coil_ID}'";

            if (!string.IsNullOrEmpty(strPlan_No))
                strSql += $"  And  [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Plan_No)}] = '{strPlan_No}' ";

            return strSql;
        }



        #endregion
    }
}
