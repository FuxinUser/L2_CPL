using DBService.Repository.CutReocrd;
using DBService.Repository.DefectData;
using DBService.Repository.PDO;
using System;
using System.Data;
using System.Text;
using static CPL1HMI.DataBaseTableFactory;

namespace CPL1HMI
{
    public class Frm_3_2_SqlFactory
    {

        #region --- Display ---

        /// <summary>
        /// PDO詳細資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_PDODetail(string Coil_ID, string strFinishTime = null, string strIn_Coil_ID = null, string strPlan_No = null)
        {
            string strSql = $@"Select 
                            [{nameof(PDOEntity.TBL_PDO.OrderNo)}],
                            [{nameof(PDOEntity.TBL_PDO.Plan_No)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}],
                            [{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}],
                            [{nameof(PDOEntity.TBL_PDO.StartTime)}],
                            [{nameof(PDOEntity.TBL_PDO.FinishTime)}],
                            [{nameof(PDOEntity.TBL_PDO.Shift)}],
                            [{nameof(PDOEntity.TBL_PDO.Team)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Outer_Diameter)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Inner)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Wt)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Coil_Length)}],
                            [{nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Entry)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Entry)}],
                            [{nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Exit)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Exit)}],
                            [{nameof(PDOEntity.TBL_PDO.Paper_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Paper_Req_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Head_Paper_Length)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Head_Paper_Width)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Tail_Paper_Length)}],
                            [{nameof(PDOEntity.TBL_PDO.Out_Tail_Paper_Width)}],
                            [{nameof(PDOEntity.TBL_PDO.Sleeve_Inner_Exit_Diamter)}],
                            [{nameof(PDOEntity.TBL_PDO.Sleeve_Type_Exit_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Hole_Position)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Leader_Length)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Leader_Width)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Leader_Thickness)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_PunchHole_Position)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Length)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Width)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Thickness)}],
                            [{nameof(PDOEntity.TBL_PDO.Scraped_Length_Entry)}],
                            [{nameof(PDOEntity.TBL_PDO.Scraped_Length_Exit)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Leader_St_No)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Leader_St_No)}],
                            [{nameof(PDOEntity.TBL_PDO.Winding_Direction)}],
                            [{nameof(PDOEntity.TBL_PDO.Base_Surface)}],
                            [{nameof(PDOEntity.TBL_PDO.Inspector)}],
                            [{nameof(PDOEntity.TBL_PDO.Hold_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.Hold_Cause_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Sample_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.Trim_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.Fixed_WT_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.End_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.Scrap_Flag)}],
                            [{nameof(PDOEntity.TBL_PDO.Sample_Frqn_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.No_Leader_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Surface_Accuracy_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Head_Off_Gauge)}],
                            [{nameof(PDOEntity.TBL_PDO.Tail_Off_Gauge)}],
                            [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_In)}],
                            [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_Out)}],
                            [{nameof(PDOEntity.TBL_PDO.Process_Code)}],
                            [{nameof(PDOEntity.TBL_PDO.Flip_Tag)}],
                            [{nameof(PDOEntity.TBL_PDO.Decoiler_Direction)}],
                            [{nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Gap)}],
                            [{nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Lap)}],
                            [{nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Width)}],
                            [{nameof(PDOEntity.TBL_PDO.Avg_Trimming_OperateSide)}],
                            [{nameof(PDOEntity.TBL_PDO.Avg_Trimming_DriveSide)}],
                            [{nameof(PDOEntity.TBL_PDO.Recoiler_Actten_Avg)}]
                          , CONVERT(VARCHAR(25), [{ nameof(PDOEntity.TBL_PDO.FinishTime)}], 121) as FinishTime_Str 
                    From [{nameof(PDOEntity.TBL_PDO)}]
                    Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{Coil_ID}'";
            //convert(char(19), [{nameof(PDOEntity.TBL_PDO.StartTime)}], 120) StartTime,
            //convert(char(19), [{ nameof(PDOEntity.TBL_PDO.FinishTime)}], 120) FinishTime,
            if (strFinishTime != null)
            {
                //strSql += $@" AND [{ nameof(PDOEntity.TBL_PDO.FinishTime)}] =  '{strFinishTime}'";
                strSql += $@" AND CONVERT(VARCHAR(25), [{ nameof(PDOEntity.TBL_PDO.FinishTime)}], 121) LIKE  '{strFinishTime}%'";
            }
            if (strIn_Coil_ID != null)
                strSql += $@" AND [{ nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] = '{strIn_Coil_ID}'";
            if (strPlan_No != null)
                strSql += $@" AND [{ nameof(PDOEntity.TBL_PDO.Plan_No)}] = '{strPlan_No}'";
          

            return strSql;
        }


        /// <summary>
        /// 查詢PDO缺陷，使用PDO欄位內入口捲號為條件進行查詢
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_DefectData(string Coil_ID , string strPlan_No = null)
        {
            string strSql = $@"Select *
                               From [{nameof(CoilDefectDataEntity.TBL_Coil_Defect)}]
                               Where [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Coil_ID)}] = '{Coil_ID}'";
                             
                                
            if(!string.IsNullOrEmpty( strPlan_No))
                strSql += $@" AND [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Plan_No)}] = '{strPlan_No}'
                                ";
            return strSql;
        }



        #endregion


        #region --- Funtion ---

        /// <summary>
        /// 上傳MMS並將鋼捲的TBL_CutRecord_Temp清掉
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Delete_CoilCutRecordTemp(string Coil_ID)
        {
            string strSql = $"Delete From [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp)}] " +
                $"Where [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.Coil_ID)}] = '{Coil_ID}' " +
           
                $"";

            return strSql;
        }

        /// <summary>
        /// 檢查PDO是否已上傳MMS
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_UploadedChecked(string Coil_ID, string FinishTime, string In_Coil_ID, string Plan_No)
        {
            string strSql = $"Select [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.FinishTime)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Plan_No)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] " +

                            $",[{nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Entry)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Entry)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Exit)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Exit)}] " +

                            $",[{nameof(PDOEntity.TBL_PDO.Scraped_Length_Entry)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Scraped_Length_Exit)}] " +

                            $", CONVERT(VARCHAR(25), [{nameof(PDOEntity.TBL_PDO.FinishTime)}], 121) as FinishTime_Str " +

                            $"From [{nameof(PDOEntity.TBL_PDO)}] " +
                            $"Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] ='{Coil_ID}' " +
                            $"AND  CONVERT(VARCHAR(25), [{ nameof(PDOEntity.TBL_PDO.FinishTime)}], 121) LIKE  '{FinishTime}%' " +
                            $"AND [{nameof(PDOEntity.TBL_PDO.Plan_No)}] ='{Plan_No}' " +
                            $"AND [{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] ='{In_Coil_ID}' ";
            //
            //$"AND  [{ nameof(PDOEntity.TBL_PDO.FinishTime)}] = '{FinishTime}'" +
            return strSql;
        }
        /// <summary>
        /// // MWW 2023/3/8 已上傳最終卷
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <param name="FinishTime"></param>
        /// <param name="In_Coil_ID">母捲號</param>
        /// <param name="Plan_No">計劃號</param>
        /// <returns></returns>
        public static string SQL_Select_UploadEndFlag(string In_Coil_ID, string Plan_No)
        {
            string strSql = $"Select [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.FinishTime)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Plan_No)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.End_Flag)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Fixed_WT_Flag)}] " +
                            $"From [{nameof(PDOEntity.TBL_PDO)}] " +
                            $"Where [{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] ='{In_Coil_ID}' " +
                            $"AND [{nameof(PDOEntity.TBL_PDO.Plan_No)}] ='{Plan_No}' " +
                            $"AND [{nameof(PDOEntity.TBL_PDO.End_Flag)}] ='{1}'" +
                            $"AND [{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] ='{1}'";
            //+
            //$"AND[{ nameof(PDOEntity.TBL_PDO.Fixed_WT_Flag)}] = '{1}' ";
            //
            //$"AND  [{ nameof(PDOEntity.TBL_PDO.FinishTime)}] = '{FinishTime}'" +
            return strSql;
        }


        /// <summary>
        /// MWW 2023/3/8 已上抛過的子卷
        /// </summary>
        /// <param name="In_Coil_ID">母捲號</param>
        /// <param name="Plan_No">計劃號</param>
        /// <returns></returns>
        public static string SQL_Select_SubvolumeCoil(string In_Coil_ID, string Plan_No)
        {
            string strSql = $"Select [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.FinishTime)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Plan_No)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.End_Flag)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] " +
                            $",[{nameof(PDOEntity.TBL_PDO.Fixed_WT_Flag)}] " +
                            $"From [{nameof(PDOEntity.TBL_PDO)}] " +
                            $"Where [{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] ='{In_Coil_ID}' " +
                            $"AND [{nameof(PDOEntity.TBL_PDO.Plan_No)}] ='{Plan_No}' " +
                            $"AND [{nameof(PDOEntity.TBL_PDO.Fixed_WT_Flag)}] ='{1}' " +
                            $"AND [{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] ='{1}' ";
            //
            //$"AND  [{ nameof(PDOEntity.TBL_PDO.FinishTime)}] = '{FinishTime}'" +
            return strSql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="feedStatus"></param>
        /// <returns></returns>
        public static string SQL_Update_Pdo_Scraped_Length(DataTable dt, string strCol, string FinishTime)
        {


            string strSql = $@"UPDATE [{nameof(PDOEntity.TBL_PDO)}] set " +
                            $"[{strCol}] = '{dt.Rows[0][strCol]}' " +
                            $" Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] ='{ dt.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)]}'" +
                            $" AND [{nameof(PDOEntity.TBL_PDO.Plan_No)}] ='{ dt.Rows[0][nameof(PDOEntity.TBL_PDO.Plan_No)]}'" +
                            $" AND CONVERT(VARCHAR(25), [{ nameof(PDOEntity.TBL_PDO.FinishTime)}], 121) LIKE  '{FinishTime}%'" +
                            $" AND [{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] ='{ dt.Rows[0][nameof(PDOEntity.TBL_PDO.In_Coil_ID)]}'" ;
            //
            // $" AND [{ nameof(PDOEntity.TBL_PDO.FinishTime)}] =  '{FinishTime}'" +
            return strSql;
        }


        /// <summary>
        /// 新增PDO
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_PDO()
        {
            string strSql = $@"Insert into [{nameof(PDOEntity.TBL_PDO)}]
                                    ([{nameof(PDOEntity.TBL_PDO.OrderNo)}],
                                     [{nameof(PDOEntity.TBL_PDO.Plan_No)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}],
                                     [{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}],
                                     [{nameof(PDOEntity.TBL_PDO.StartTime)}],

                                     [{nameof(PDOEntity.TBL_PDO.FinishTime)}],
                                     [{nameof(PDOEntity.TBL_PDO.Shift)}],
                                     [{nameof(PDOEntity.TBL_PDO.Team)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Outer_Diameter)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Inner)}],

                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Wt)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Coil_Length)}],

                                     [{nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Entry)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Entry)}],
                                     [{nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Exit)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Exit)}],
                                     [{nameof(PDOEntity.TBL_PDO.Paper_Code)}],

                                     [{nameof(PDOEntity.TBL_PDO.Paper_Req_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Head_Paper_Length)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Head_Paper_Width)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Tail_Paper_Length)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Tail_Paper_Width)}],

                                     [{nameof(PDOEntity.TBL_PDO.Sleeve_Inner_Exit_Diamter)}],
                                     [{nameof(PDOEntity.TBL_PDO.Sleeve_Type_Exit_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Hole_Position)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Leader_Length)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Leader_Width)}],

                                     [{nameof(PDOEntity.TBL_PDO.Head_Leader_Thickness)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_PunchHole_Position)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Length)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Width)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Thickness)}],

                                     [{nameof(PDOEntity.TBL_PDO.Scraped_Length_Entry)}],
                                     [{nameof(PDOEntity.TBL_PDO.Scraped_Length_Exit)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Leader_St_No)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Leader_St_No)}],
                                     [{nameof(PDOEntity.TBL_PDO.Winding_Direction)}],

                                     [{nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Gap)}],
                                     [{nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Lap)}],
                                     [{nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Width)}],
                                     [{nameof(PDOEntity.TBL_PDO.Avg_Trimming_OperateSide)}],
                                     [{nameof(PDOEntity.TBL_PDO.Avg_Trimming_DriveSide)}],

                                     [{nameof(PDOEntity.TBL_PDO.Base_Surface)}],
                                     [{nameof(PDOEntity.TBL_PDO.Inspector)}],
                                     [{nameof(PDOEntity.TBL_PDO.Hold_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.Hold_Cause_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Sample_Flag)}],

                                     [{nameof(PDOEntity.TBL_PDO.Trim_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.Fixed_WT_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.End_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.Scrap_Flag)}],
                                     [{nameof(PDOEntity.TBL_PDO.Sample_Frqn_Code)}],

                                     [{nameof(PDOEntity.TBL_PDO.No_Leader_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Surface_Accuracy_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Head_Off_Gauge)}],
                                     [{nameof(PDOEntity.TBL_PDO.Tail_Off_Gauge)}],
                                     [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_In)}],

                                     [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_Out)}],
                                     [{nameof(PDOEntity.TBL_PDO.CreateTime)}],
                                     [{nameof(PDOEntity.TBL_PDO.Process_Code)}],
                                     [{nameof(PDOEntity.TBL_PDO.Flip_Tag)}],
                                     [{nameof(PDOEntity.TBL_PDO.Decoiler_Direction)}],
                                     [{nameof(PDOEntity.TBL_PDO.Out_Theory_Wt)}], /* 理论重 */
                                     [{nameof(PDOEntity.TBL_PDO.Recoiler_Actten_Avg)}], /* 卷取实际张力平均值 */
                                     [{nameof(PDOEntity.TBL_PDO.Exit_Scaned_CoilID)}], /* 出口掃描鋼卷號 */
                                     [{nameof(PDOEntity.TBL_PDO.Exit_Scaned_UserID)}], /* 出口掃描人員 */
                                     [{nameof(PDOEntity.TBL_PDO.Exit_CoilID_Checked)}], /* 出口掃描標記 */
                                     [{nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)}] /* PDO上傳註記 */ )
                            Values('{PublicForms.PDODetail.Txt_Order_No.Text}',
                                    '{PublicForms.PDODetail.Txt_Plan_No.Text}',
                                    '{PublicForms.PDODetail.Txt_Out_Coil_ID.Text}',
                                    '{PublicForms.PDODetail.Txt_In_Coil_ID.Text}',
                                    '{PublicForms.PDODetail.Dtp_StartTime.Value:yyyy-MM-dd HH:mm:ss}',

                                    '{PublicForms.PDODetail.Dtp_FinishTime.Value:yyyy-MM-dd HH:mm:ss}',
                                    '{PublicForms.PDODetail.Cob_Shift.SelectedValue}',
                                    '{PublicForms.PDODetail.Cob_Team.SelectedValue}',
                                    '{PublicForms.PDODetail.Txt_Out_Coil_Outer_Diameter.Text}',
                                    '{PublicForms.PDODetail.Txt_Out_Coil_Inner.Text}',

                                    '{PublicForms.PDODetail.Txt_Out_Coil_Wt.Text}',
                                    '{PublicForms.PDODetail.Txt_Out_Coil_Gross_WT.Text}',
                                    '{PublicForms.PDODetail.Txt_Out_Coil_Thick.Text}',
                                    '{PublicForms.PDODetail.Txt_Out_Coil_Width.Text}',
                                    '{PublicForms.PDODetail.Txt_Out_Coil_Length.Text}',

                                     '{PublicForms.PDODetail.Txt_Header_Cut_Length_Entry.Text}',
                                     '{PublicForms.PDODetail.Txt_Tail_Cut_Length_Entry.Text}',
                                    '{PublicForms.PDODetail.Txt_Header_Cut_Length_Exit.Text}',
                                     '{PublicForms.PDODetail.Txt_Tail_Cut_Length_Exit.Text}',
                                    '{PublicForms.PDODetail.Cob_Paper_Code.SelectedValue}',

                                    '{PublicForms.PDODetail.Cob_Paper_Req_Code.SelectedValue}',
                                    '{PublicForms.PDODetail.Txt_Out_Head_Paper_Length.Text}',
                                    '{PublicForms.PDODetail.Txt_Out_Head_Paper_Width.Text}',
                                    '{PublicForms.PDODetail.Txt_Out_Tail_Paper_Length.Text}',
                                    '{PublicForms.PDODetail.Txt_Out_Tail_Paper_Width.Text}',

                                    '{PublicForms.PDODetail.Txt_Sleeve_Inner_Exit_Diamter.Text}',
                                    '{PublicForms.PDODetail.Cob_Sleeve_Type_Exit_Code.SelectedValue}',
                                    '{PublicForms.PDODetail.Txt_Head_Hole_Position.Text}',
                                    '{PublicForms.PDODetail.Txt_Head_Leader_Length.Text}',
                                    '{PublicForms.PDODetail.Txt_Head_Leader_Width.Text}',

                                    '{PublicForms.PDODetail.Txt_Head_Leader_Thickness.Text}',
                                    '{PublicForms.PDODetail.Txt_Tail_PunchHole_Position.Text}',
                                    '{PublicForms.PDODetail.Txt_Tail_Leader_Length.Text}',
                                    '{PublicForms.PDODetail.Txt_Tail_Leader_Width.Text}',
                                    '{PublicForms.PDODetail.Txt_Tail_Leader_Thickness.Text}',

                                    '{PublicForms.PDODetail.Txt_Scraped_Length_Entry.Text}',
                                    '{PublicForms.PDODetail.Txt_Scraped_Length_Exit.Text}',
                                    '{PublicForms.PDODetail.Txt_Head_Leader_St_No.Text}',
                                    '{PublicForms.PDODetail.Txt_Tail_Leader_St_No.Text}',
                                    '{PublicForms.PDODetail.Cob_Winding_Direction.SelectedValue}',

                                    '{PublicForms.PDODetail.Txt_Avg_Side_Trimmer_Gap.Text}',
                                    '{PublicForms.PDODetail.Txt_Avg_Side_Trimmer_Lap.Text}',
                                    '{PublicForms.PDODetail.Txt_Avg_Side_Trimmer_Width.Text}',
                                    '{PublicForms.PDODetail.Txt_Avg_Trimming_OperateSide.Text}',
                                    '{PublicForms.PDODetail.Txt_Avg_Trimming_DriveSide.Text}',

                                    '{PublicForms.PDODetail.Cob_Base_Surface.SelectedValue}',
                                    '{PublicForms.PDODetail.Txt_Inspector.Text}',
                                    '{PublicForms.PDODetail.Cob_Hold_Flag.SelectedValue}',
                                    '{PublicForms.PDODetail.Txt_Hold_Cause_Code.Text}',
                                    '{PublicForms.PDODetail.Cob_Sample_Flag.SelectedValue}',

                                    '{PublicForms.PDODetail.Cob_Trim_Flag.SelectedValue}',
                                    '{PublicForms.PDODetail.Cob_Fixed_WT_Flag.SelectedValue}',
                                    '{PublicForms.PDODetail.Cob_End_Flag.SelectedValue}',
                                    '{PublicForms.PDODetail.Cob_Scrap_Flag.SelectedValue}',
                                    '{PublicForms.PDODetail.Cob_Sample_Frqn_Code.SelectedValue}',

                                    '{PublicForms.PDODetail.Txt_No_Leader_Code.Text}',
                                    '{PublicForms.PDODetail.Cob_Surface_Accuracy_Code.SelectedValue}',
                                    '{PublicForms.PDODetail.Txt_Head_Off_Gauge.Text}',
                                    '{PublicForms.PDODetail.Txt_Tail_Off_Gauge.Text}',
                                    '{PublicForms.PDODetail.Cob_Surface_Accu_Code_In.SelectedValue}',

                                    '{PublicForms.PDODetail.Cob_Surface_Accu_Code_Out.SelectedValue}',
                                    '{GlobalVariableHandler.Instance.time}',
                                    '{PublicForms.PDODetail.Cob_ProcessCode.SelectedValue}',
                                    '{PublicForms.PDODetail.Cob_Flip_Tag.SelectedValue}',
                                    '{PublicForms.PDODetail.Cob_Decoiler_Direction.SelectedValue}',
                                    '0', /* 理论重 */
                                    '{PublicForms.PDODetail.Txt_Recoiler_Actten_Avg.Text}', /*'0', 卷取实际张力平均值 */
                                    '',  /* 出口掃描鋼卷號 */
                                    '',  /* 出口掃描人員 */
                                    '',  /* 出口掃描標記 */
                                    '0'  /* PDO上傳註記 */)";
            // '{PublicForms.PDODetail.Txt_Recoiler_Actten_Avg.Text}',
            //.txtStarttime.Text
            //.txtFinishtime.Text
            return strSql;
        }


        /// <summary>
        /// 新增缺陷
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_DefectData()
        {
            string strSql = $@"Insert into [{nameof(CoilDefectDataEntity.TBL_Coil_Defect)}] (
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Plan_No)}],
                                    [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Coil_ID)}],
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
                             Values ('{PublicForms.PDODetail.Txt_Plan_No.Text.Trim()}',
                                    '{PublicForms.PDODetail.Txt_Out_Coil_ID.Text.Trim()}',
                                    '{PublicForms.PDODetail.Txt_In_Coil_ID.Text.Trim()}',
                                    '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                    '{DateTime.Now:yyyy-MM-dd HH:mm:ss}',
                                    '{PublicForms.PDODetail.Txt_Code_D01.Text}',
                                    '{PublicForms.PDODetail.Txt_Code_D02.Text}',
                                    '{PublicForms.PDODetail.Txt_Code_D03.Text}',
                                    '{PublicForms.PDODetail.Txt_Code_D04.Text}',
                                    '{PublicForms.PDODetail.Txt_Code_D05.Text}',
                                    '{PublicForms.PDODetail.Txt_Code_D06.Text}',
                                    '{PublicForms.PDODetail.Txt_Code_D07.Text}',
                                    '{PublicForms.PDODetail.Txt_Code_D08.Text}',
                                    '{PublicForms.PDODetail.Txt_Code_D09.Text}',
                                    '{PublicForms.PDODetail.Txt_Code_D10.Text}',
                                    '{PublicForms.PDODetail.Txt_Origin_D01.Text}',
                                    '{PublicForms.PDODetail.Txt_Origin_D02.Text}',
                                    '{PublicForms.PDODetail.Txt_Origin_D03.Text}',
                                    '{PublicForms.PDODetail.Txt_Origin_D04.Text}',
                                    '{PublicForms.PDODetail.Txt_Origin_D05.Text}',
                                    '{PublicForms.PDODetail.Txt_Origin_D06.Text}',
                                    '{PublicForms.PDODetail.Txt_Origin_D07.Text}',
                                    '{PublicForms.PDODetail.Txt_Origin_D08.Text}',
                                    '{PublicForms.PDODetail.Txt_Origin_D09.Text}',
                                    '{PublicForms.PDODetail.Txt_Origin_D10.Text}',
                                    '{PublicForms.PDODetail.Cob_Sid_D01.Text}',
                                    '{PublicForms.PDODetail.Cob_Sid_D02.Text}',
                                    '{PublicForms.PDODetail.Cob_Sid_D03.Text}',
                                    '{PublicForms.PDODetail.Cob_Sid_D04.Text}',
                                    '{PublicForms.PDODetail.Cob_Sid_D05.Text}',
                                    '{PublicForms.PDODetail.Cob_Sid_D06.Text}',
                                    '{PublicForms.PDODetail.Cob_Sid_D07.Text}',
                                    '{PublicForms.PDODetail.Cob_Sid_D08.Text}',
                                    '{PublicForms.PDODetail.Cob_Sid_D09.Text}',
                                    '{PublicForms.PDODetail.Cob_Sid_D10.Text}',
                                    '{PublicForms.PDODetail.Cob_PosW_D01.Text}',
                                    '{PublicForms.PDODetail.Cob_PosW_D02.Text}',
                                    '{PublicForms.PDODetail.Cob_PosW_D03.Text}',
                                    '{PublicForms.PDODetail.Cob_PosW_D04.Text}',
                                    '{PublicForms.PDODetail.Cob_PosW_D05.Text}',
                                    '{PublicForms.PDODetail.Cob_PosW_D06.Text}',
                                    '{PublicForms.PDODetail.Cob_PosW_D07.Text}',
                                    '{PublicForms.PDODetail.Cob_PosW_D08.Text}',
                                    '{PublicForms.PDODetail.Cob_PosW_D09.Text}',
                                    '{PublicForms.PDODetail.Cob_PosW_D10.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_Start_D01.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_Start_D02.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_Start_D03.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_Start_D04.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_Start_D05.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_Start_D06.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_Start_D07.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_Start_D08.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_Start_D09.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_Start_D10.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_End_D01.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_End_D02.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_End_D03.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_End_D04.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_End_D05.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_End_D06.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_End_D07.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_End_D08.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_End_D09.Text}',
                                    '{PublicForms.PDODetail.Txt_Pos_L_End_D10.Text}',
                                    '{PublicForms.PDODetail.Cob_Level_D01.Text}',
                                    '{PublicForms.PDODetail.Cob_Level_D02.Text}',
                                    '{PublicForms.PDODetail.Cob_Level_D03.Text}',
                                    '{PublicForms.PDODetail.Cob_Level_D04.Text}',
                                    '{PublicForms.PDODetail.Cob_Level_D05.Text}',
                                    '{PublicForms.PDODetail.Cob_Level_D06.Text}',
                                    '{PublicForms.PDODetail.Cob_Level_D07.Text}',
                                    '{PublicForms.PDODetail.Cob_Level_D08.Text}',
                                    '{PublicForms.PDODetail.Cob_Level_D09.Text}',
                                    '{PublicForms.PDODetail.Cob_Level_D10.Text}',
                                    '{PublicForms.PDODetail.Txt_Percent_D01.Text}',
                                    '{PublicForms.PDODetail.Txt_Percent_D02.Text}',
                                    '{PublicForms.PDODetail.Txt_Percent_D03.Text}',
                                    '{PublicForms.PDODetail.Txt_Percent_D04.Text}',
                                    '{PublicForms.PDODetail.Txt_Percent_D05.Text}',
                                    '{PublicForms.PDODetail.Txt_Percent_D06.Text}',
                                    '{PublicForms.PDODetail.Txt_Percent_D07.Text}',
                                    '{PublicForms.PDODetail.Txt_Percent_D08.Text}',
                                    '{PublicForms.PDODetail.Txt_Percent_D09.Text}',
                                    '{PublicForms.PDODetail.Txt_Percent_D10.Text}',
                                    '{PublicForms.PDODetail.Txt_QGRADE_D01.Text}',
                                    '{PublicForms.PDODetail.Txt_QGRADE_D02.Text}',
                                    '{PublicForms.PDODetail.Txt_QGRADE_D03.Text}',
                                    '{PublicForms.PDODetail.Txt_QGRADE_D04.Text}',
                                    '{PublicForms.PDODetail.Txt_QGRADE_D05.Text}',
                                    '{PublicForms.PDODetail.Txt_QGRADE_D06.Text}',
                                    '{PublicForms.PDODetail.Txt_QGRADE_D07.Text}',
                                    '{PublicForms.PDODetail.Txt_QGRADE_D08.Text}',
                                    '{PublicForms.PDODetail.Txt_QGRADE_D09.Text}',
                                    '{PublicForms.PDODetail.Txt_QGRADE_D10.Text}')";

            return strSql;
        }


        /// <summary>
        /// PDO資料儲存
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Update_PDO(string Coil_ID , string strFinishTime, string strIn_Coil_ID, string strPlan_No)
        {
            string strStartTime = PublicForms.PDODetail.Dtp_StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            //strStartTime += $".{PublicForms.PDODetail.Dtp_StartTime.Value.Millisecond}";
            //[{nameof(PDOEntity.TBL_PDO.FinishTime)}] = '{PublicForms.PDODetail.Dtp_FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss")}',
            string strSql = $@"Update [{nameof(PDOEntity.TBL_PDO)}] Set
                                [{nameof(PDOEntity.TBL_PDO.OrderNo)}] = '{PublicForms.PDODetail.Txt_Order_No.Text}',
                                --[{nameof(PDOEntity.TBL_PDO.Plan_No)}] = '{PublicForms.PDODetail.Txt_Plan_No.Text}',
                                --[{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{PublicForms.PDODetail.Txt_Out_Coil_ID.Text}',
                                [{nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] = '{PublicForms.PDODetail.Txt_In_Coil_ID.Text}',
                                [{nameof(PDOEntity.TBL_PDO.StartTime)}] = '{strStartTime}',
                                --[{nameof(PDOEntity.TBL_PDO.FinishTime)}] = '{strFinishTime}',
                                [{nameof(PDOEntity.TBL_PDO.Shift)}] = '{PublicForms.PDODetail.Cob_Shift.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Team)}]= '{PublicForms.PDODetail.Cob_Team.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Outer_Diameter)}] = '{PublicForms.PDODetail.Txt_Out_Coil_Outer_Diameter.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Inner)}] = '{PublicForms.PDODetail.Txt_Out_Coil_Inner.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Wt)}] = '{PublicForms.PDODetail.Txt_Out_Coil_Wt.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT)}] = '{PublicForms.PDODetail.Txt_Out_Coil_Gross_WT.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)}] = '{PublicForms.PDODetail.Txt_Out_Coil_Thick.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Width)}] = '{PublicForms.PDODetail.Txt_Out_Coil_Width.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Coil_Length)}] = '{PublicForms.PDODetail.Txt_Out_Coil_Length.Text}',

                                [{nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Entry)}] = '{PublicForms.PDODetail.Txt_Header_Cut_Length_Entry.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Entry)}]   = '{PublicForms.PDODetail.Txt_Tail_Cut_Length_Entry.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Exit)}]  = '{PublicForms.PDODetail.Txt_Header_Cut_Length_Exit.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Exit)}]    = '{PublicForms.PDODetail.Txt_Tail_Cut_Length_Exit.Text}',

                                [{nameof(PDOEntity.TBL_PDO.Paper_Code)}] = '{PublicForms.PDODetail.Cob_Paper_Code.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Paper_Req_Code)}] = '{PublicForms.PDODetail.Cob_Paper_Req_Code.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Head_Paper_Length)}] = '{PublicForms.PDODetail.Txt_Out_Head_Paper_Length.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Head_Paper_Width)}] = '{PublicForms.PDODetail.Txt_Out_Head_Paper_Width.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Tail_Paper_Length)}] = '{PublicForms.PDODetail.Txt_Out_Tail_Paper_Length.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Out_Tail_Paper_Width)}] = '{PublicForms.PDODetail.Txt_Out_Tail_Paper_Width.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Sleeve_Inner_Exit_Diamter)}] = '{PublicForms.PDODetail.Txt_Sleeve_Inner_Exit_Diamter.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Sleeve_Type_Exit_Code)}] = '{PublicForms.PDODetail.Cob_Sleeve_Type_Exit_Code.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Hole_Position)}] = '{PublicForms.PDODetail.Txt_Head_Hole_Position.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Leader_Length)}] = '{PublicForms.PDODetail.Txt_Head_Leader_Length.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Leader_Width)}] = '{PublicForms.PDODetail.Txt_Head_Leader_Width.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Leader_Thickness)}] = '{PublicForms.PDODetail.Txt_Head_Leader_Thickness.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_PunchHole_Position)}] = '{PublicForms.PDODetail.Txt_Tail_PunchHole_Position.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Length)}] = '{PublicForms.PDODetail.Txt_Tail_Leader_Length.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Width)}] = '{PublicForms.PDODetail.Txt_Tail_Leader_Width.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Leader_Thickness)}] = '{PublicForms.PDODetail.Txt_Tail_Leader_Thickness.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Scraped_Length_Entry)}] = '{PublicForms.PDODetail.Txt_Scraped_Length_Entry.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Scraped_Length_Exit)}] = '{PublicForms.PDODetail.Txt_Scraped_Length_Exit.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Leader_St_No)}] = '{PublicForms.PDODetail.Txt_Head_Leader_St_No.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Leader_St_No)}] = '{PublicForms.PDODetail.Txt_Tail_Leader_St_No.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Winding_Direction)}] = '{PublicForms.PDODetail.Cob_Winding_Direction.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Base_Surface)}] = '{PublicForms.PDODetail.Cob_Base_Surface.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Inspector)}] = '{PublicForms.PDODetail.Txt_Inspector.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Hold_Flag)}] = '{PublicForms.PDODetail.Cob_Hold_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Hold_Cause_Code)}] = '{PublicForms.PDODetail.Txt_Hold_Cause_Code.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Sample_Flag)}] = '{PublicForms.PDODetail.Cob_Sample_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Trim_Flag)}] = '{PublicForms.PDODetail.Cob_Trim_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Fixed_WT_Flag)}] = '{PublicForms.PDODetail.Cob_Fixed_WT_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.End_Flag)}] = '{PublicForms.PDODetail.Cob_End_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Scrap_Flag)}] = '{PublicForms.PDODetail.Cob_Scrap_Flag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Sample_Frqn_Code)}] = '{PublicForms.PDODetail.Cob_Sample_Frqn_Code.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.No_Leader_Code)}] = '{PublicForms.PDODetail.Txt_No_Leader_Code.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Surface_Accuracy_Code)}] = '{PublicForms.PDODetail.Cob_Surface_Accuracy_Code.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Head_Off_Gauge)}] = '{PublicForms.PDODetail.Txt_Head_Off_Gauge.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Tail_Off_Gauge)}] = '{PublicForms.PDODetail.Txt_Tail_Off_Gauge.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_In)}] = '{PublicForms.PDODetail.Cob_Surface_Accu_Code_In.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_Out)}] = '{PublicForms.PDODetail.Cob_Surface_Accu_Code_Out.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Process_Code)}] = '{PublicForms.PDODetail.Cob_ProcessCode.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Flip_Tag)}] = '{PublicForms.PDODetail.Cob_Flip_Tag.SelectedValue}',
                                [{nameof(PDOEntity.TBL_PDO.Decoiler_Direction)}] = '{PublicForms.PDODetail.Cob_Decoiler_Direction.SelectedValue}',
                              [{nameof(PDOEntity.TBL_PDO.Recoiler_Actten_Avg)}] = '{PublicForms.PDODetail.Txt_Recoiler_Actten_Avg.Text}',

                                [{nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Gap)}] = '{PublicForms.PDODetail.Txt_Avg_Side_Trimmer_Gap.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Lap)}] = '{PublicForms.PDODetail.Txt_Avg_Side_Trimmer_Lap.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Width)}] = '{PublicForms.PDODetail.Txt_Avg_Side_Trimmer_Width.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Avg_Trimming_OperateSide)}] = '{PublicForms.PDODetail.Txt_Avg_Trimming_OperateSide.Text}',
                                [{nameof(PDOEntity.TBL_PDO.Avg_Trimming_DriveSide)}] = '{PublicForms.PDODetail.Txt_Avg_Trimming_DriveSide.Text}',

                                [{nameof(PDOEntity.TBL_PDO.CreateTime)}] = '{GlobalVariableHandler.Instance.getTime}'

                          Where [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{Coil_ID}'                           
                            AND CONVERT(VARCHAR(25), [{ nameof(PDOEntity.TBL_PDO.FinishTime)}], 121) LIKE  '{strFinishTime}%'
                            AND [{ nameof(PDOEntity.TBL_PDO.In_Coil_ID)}] = '{strIn_Coil_ID}'
                            AND [{ nameof(PDOEntity.TBL_PDO.Plan_No)}] = '{strPlan_No}'
                          ";

            return strSql;
            // AND [{ nameof(PDOEntity.TBL_PDO.FinishTime)}] = '{strFinishTime}'
            //  $" AND CONVERT(VARCHAR(19), [{ nameof(PDOEntity.TBL_PDO.FinishTime)}], 120) LIKE  '{FinishTime}%'" +
        }


        /// <summary>
        /// 修改PDO缺陷资料
        /// </summary>
        /// <returns></returns>
        public static string SQL_Update_DefectData(string Coil_ID, string strPlan_No)
        {
            string strSql = $@"Update [{nameof(CoilDefectDataEntity.TBL_Coil_Defect)}]
                           Set [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Code)}] = '{PublicForms.PDODetail.Txt_Code_D01.Text.Trim()}',         
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Origin)}] = '{PublicForms.PDODetail.Txt_Origin_D01.Text.Trim()}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Sid)}] = '{PublicForms.PDODetail.Cob_Sid_D01.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_W)}] = '{PublicForms.PDODetail.Cob_PosW_D01.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_Start)}] = '{PublicForms.PDODetail.Txt_Pos_L_Start_D01.Text.Trim()}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_End)}] = '{PublicForms.PDODetail.Txt_Pos_L_End_D01.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Level)}] = '{PublicForms.PDODetail.Cob_Level_D01.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Percent)}] = '{PublicForms.PDODetail.Txt_Percent_D01.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_QGRADE)}] = '{PublicForms.PDODetail.Txt_QGRADE_D01.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Code)}] = '{PublicForms.PDODetail.Txt_Code_D02.Text.Trim()}', 
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Origin)}] = '{PublicForms.PDODetail.Txt_Origin_D02.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Sid)}] = '{PublicForms.PDODetail.Cob_Sid_D02.Text}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_W)}] = '{PublicForms.PDODetail.Cob_PosW_D02.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_Start)}] = '{PublicForms.PDODetail.Txt_Pos_L_Start_D02.Text.Trim()}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_End)}] = '{PublicForms.PDODetail.Txt_Pos_L_End_D02.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Level)}] = '{PublicForms.PDODetail.Cob_Level_D02.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Percent)}] = '{PublicForms.PDODetail.Txt_Percent_D02.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_QGRADE)}] = '{PublicForms.PDODetail.Txt_QGRADE_D02.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Code)}] = '{PublicForms.PDODetail.Txt_Code_D03.Text.Trim()}', 
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Origin)}] = '{PublicForms.PDODetail.Txt_Origin_D03.Text.Trim()}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Sid)}] = '{PublicForms.PDODetail.Cob_Sid_D03.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_W)}] = '{PublicForms.PDODetail.Cob_PosW_D03.Text}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_Start)}] = '{PublicForms.PDODetail.Txt_Pos_L_Start_D03.Text.Trim()}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_End)}] = '{PublicForms.PDODetail.Txt_Pos_L_End_D03.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Level)}] = '{PublicForms.PDODetail.Cob_Level_D03.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Percent)}] = '{PublicForms.PDODetail.Txt_Percent_D03.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_QGRADE)}] = '{PublicForms.PDODetail.Txt_QGRADE_D03.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Code)}] = '{PublicForms.PDODetail.Txt_Code_D04.Text.Trim()}',         
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Origin)}] = '{PublicForms.PDODetail.Txt_Origin_D04.Text.Trim()}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Sid)}] = '{PublicForms.PDODetail.Cob_Sid_D04.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_W)}] = '{PublicForms.PDODetail.Cob_PosW_D04.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_Start)}] = '{PublicForms.PDODetail.Txt_Pos_L_Start_D04.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_End)}] = '{PublicForms.PDODetail.Txt_Pos_L_End_D04.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Level)}] = '{PublicForms.PDODetail.Cob_Level_D04.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Percent)}] = '{PublicForms.PDODetail.Txt_Percent_D04.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_QGRADE)}] = '{PublicForms.PDODetail.Txt_QGRADE_D04.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Code)}] = '{PublicForms.PDODetail.Txt_Code_D05.Text.Trim()}',       
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Origin)}] = '{PublicForms.PDODetail.Txt_Origin_D05.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Sid)}] = '{PublicForms.PDODetail.Cob_Sid_D05.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_W)}] = '{PublicForms.PDODetail.Cob_PosW_D05.Text}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_Start)}] = '{PublicForms.PDODetail.Txt_Pos_L_Start_D05.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_End)}] = '{PublicForms.PDODetail.Txt_Pos_L_End_D05.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Level)}] = '{PublicForms.PDODetail.Cob_Level_D05.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Percent)}] = '{PublicForms.PDODetail.Txt_Percent_D05.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_QGRADE)}] = '{PublicForms.PDODetail.Txt_QGRADE_D05.Text.Trim()}', 

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Code)}] = '{PublicForms.PDODetail.Txt_Code_D06.Text.Trim()}',        
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Origin)}] = '{PublicForms.PDODetail.Txt_Origin_D06.Text.Trim()}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Sid)}] = '{PublicForms.PDODetail.Cob_Sid_D06.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_W)}] = '{PublicForms.PDODetail.Cob_PosW_D06.Text}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_Start)}] = '{PublicForms.PDODetail.Txt_Pos_L_Start_D06.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_End)}] = '{PublicForms.PDODetail.Txt_Pos_L_End_D06.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Level)}] = '{PublicForms.PDODetail.Cob_Level_D06.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Percent)}] = '{PublicForms.PDODetail.Txt_Percent_D06.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_QGRADE)}] = '{PublicForms.PDODetail.Txt_QGRADE_D06.Text.Trim()}',   

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Code)}] = '{PublicForms.PDODetail.Txt_Code_D07.Text.Trim()}',        
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Origin)}] = '{PublicForms.PDODetail.Txt_Origin_D07.Text.Trim()}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Sid)}] = '{PublicForms.PDODetail.Cob_Sid_D07.Text}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_W)}] = '{PublicForms.PDODetail.Cob_PosW_D07.Text}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_Start)}] = '{PublicForms.PDODetail.Txt_Pos_L_Start_D07.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_End)}] = '{PublicForms.PDODetail.Txt_Pos_L_End_D07.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Level)}] = '{PublicForms.PDODetail.Cob_Level_D07.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Percent)}] = '{PublicForms.PDODetail.Txt_Percent_D07.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_QGRADE)}] = '{PublicForms.PDODetail.Txt_QGRADE_D07.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Code)}] = '{PublicForms.PDODetail.Txt_Code_D08.Text.Trim()}',      
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Origin)}] = '{PublicForms.PDODetail.Txt_Origin_D08.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Sid)}] = '{PublicForms.PDODetail.Cob_Sid_D08.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_W)}] = '{PublicForms.PDODetail.Cob_PosW_D08.Text}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_Start)}] = '{PublicForms.PDODetail.Txt_Pos_L_Start_D08.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_End)}] = '{PublicForms.PDODetail.Txt_Pos_L_End_D08.Text.Trim()}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Level)}] = '{PublicForms.PDODetail.Cob_Level_D08.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Percent)}] = '{PublicForms.PDODetail.Txt_Percent_D08.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_QGRADE)}] = '{PublicForms.PDODetail.Txt_QGRADE_D08.Text.Trim()}',  

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Code)}] = '{PublicForms.PDODetail.Txt_Code_D09.Text.Trim()}',         
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Origin)}] = '{PublicForms.PDODetail.Txt_Origin_D09.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Sid)}] = '{PublicForms.PDODetail.Cob_Sid_D09.Text}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_W)}] = '{PublicForms.PDODetail.Cob_PosW_D09.Text}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_Start)}] = '{PublicForms.PDODetail.Txt_Pos_L_Start_D09.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_End)}] = '{PublicForms.PDODetail.Txt_Pos_L_End_D09.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Level)}] = '{PublicForms.PDODetail.Cob_Level_D09.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Percent)}] = '{PublicForms.PDODetail.Txt_Percent_D09.Text.Trim()}', 
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_QGRADE)}] = '{PublicForms.PDODetail.Txt_QGRADE_D09.Text.Trim()}',  

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Code)}] = '{PublicForms.PDODetail.Txt_Code_D10.Text.Trim()}',          
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Origin)}] = '{PublicForms.PDODetail.Txt_Origin_D10.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Sid)}] = '{PublicForms.PDODetail.Cob_Sid_D10.Text}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_W)}] = '{PublicForms.PDODetail.Cob_PosW_D10.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_Start)}] = '{PublicForms.PDODetail.Txt_Pos_L_Start_D10.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_End)}] = '{PublicForms.PDODetail.Txt_Pos_L_End_D10.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Level)}] = '{PublicForms.PDODetail.Cob_Level_D10.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Percent)}] = '{PublicForms.PDODetail.Txt_Percent_D10.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_QGRADE)}] = '{PublicForms.PDODetail.Txt_QGRADE_D10.Text.Trim()}'
                         Where [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Coil_ID)}] = '{Coil_ID}'
                           AND [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Plan_No)}] = '{strPlan_No}'";
            return strSql;
        }

        #endregion


        #region --- 切廢長度 ---

        /// <summary>
        /// 搜尋切割訊號暫存記錄
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_CutRecordTemp(string Coil_ID, string CutMode)
        {
            string strSql = $@"Select 
                                     [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.Coil_ID)}] ,
                                     [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.In_Coil_ID)}] ,
                                     [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.OriPDI_Out_Coil_ID)}],
                                     [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutDevice)}] ,
                                     [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutMode)}] ,
                                     [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutLength)}] ,
                                     convert(char(19), [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutTime)}], 120) CutTime,
                                     [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.Coil_OutDiam)}] ,
                                     [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.Coil_Length)}] ,
                                     [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.Coil_CalcWeight)}] ,
                                     [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.Coil_PaperFlag)}] 
                                From [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp)}] 
                               Where [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.OriPDI_Out_Coil_ID)}] = '{Coil_ID}' ";
                                 //And [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutMode)}] = '{CutMode}'
            if(!string.IsNullOrEmpty(CutMode))
                strSql += $@" And [{nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutMode)}] = '{CutMode}'";

            return strSql;
        }


        #endregion


        #region --- ComboBoxItems ---

        /// <summary>
        /// 出口鋼卷號ComboBox
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_Out_Coil_ID_List()
        {
            string strSql = $"Select [{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] from [{nameof(PDOEntity.TBL_PDO)}]";

            return strSql;
        }


        #endregion

        public static string SQL_Select_LangSwitch_Ctr_List(string strFormName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($" SELECT [{nameof(TBL_LangSwitch_Ctr.CtrName)}] ");         
            sb.Append($" ,[{nameof(TBL_LangSwitch_Ctr.ZH)}] ");
            sb.Append($" ,[{nameof(TBL_LangSwitch_Ctr.EN)}] ");
            sb.Append($" ,[{nameof(TBL_LangSwitch_Ctr.ColumnName)}] ");
            sb.Append($" FROM  [{nameof(TBL_LangSwitch_Ctr)}] ");
            sb.Append($" WHERE [{nameof(TBL_LangSwitch_Ctr.FormName)}] = '{strFormName}' ");
            sb.Append($" AND   [{nameof(TBL_LangSwitch_Ctr.ColumnName)}] IS NOT NULL ");

            string strSql = sb.ToString();

            return strSql;
        }

    }
}
