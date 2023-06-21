using DBService.Repository.PDI;
using DBService.Repository.DefectData;
using System;
using DBService.Repository.Leader;
using System.Text;

namespace CPL1HMI
{
    public class Frm_1_2_SqlFactory
    {

        #region ---Display---

        /// <summary>
        /// 搜尋PDI詳細資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_PDI_Detail(string Coil_ID, string strPlan_No = "")
        {
            string strSql = $@"Select 
                            [{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Mat_Seq_No)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Plan_Sort)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)}],
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
                            [{nameof(CoilPDIEntity.TBL_PDI.Wholebacklog_Code)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Next_Wholebacklog_Code)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness)}],
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
                            [{nameof(CoilPDIEntity.TBL_PDI.Head_Off_Gauge)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Tail_Off_Gauge)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Sg_Sign)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Process_Code)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.CustomerCode)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.CustomerName_C)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.CustomerName_E)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.Sg_Sign)}],
                            [{nameof(CoilPDIEntity.TBL_PDI.CreateTime)}]
                    From [{nameof(CoilPDIEntity.TBL_PDI)}]
                    Where [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{Coil_ID}'";

            if (!string.IsNullOrEmpty(strPlan_No))
                strSql += $@" AND  [{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}] = '{strPlan_No}'";

            return strSql;
        }


        /// <summary>
        /// 搜尋缺陷資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_DefectData(string Coil_ID, string strPlan_No = "")
        {
            string strSql = $@"Select *
                         From [{nameof(CoilDefectDataEntity.TBL_Coil_Defect)}]
                        Where [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Entry_Coil_ID)}] = '{Coil_ID}'";
            if (!string.IsNullOrEmpty(strPlan_No))
                strSql += $@" AND [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Plan_No)}] = '{strPlan_No}'";
            return strSql;
        }


        /// <summary>
        /// 搜尋導帶資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Select_LeaderData(string Coil_ID)
        {
            string strSql = $@"Select * From [{nameof(LeaderTempEntity.TBL_Leader_Temp)}] Where [{nameof(LeaderTempEntity.TBL_Leader_Temp.Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }


        #endregion


        #region --- Funtion ---

        public static string SQL_Select_PDIDetail(string strEntry_Coil_ID, string strPlan_No)
        {          
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" Select * ");
            sb.AppendLine($" From [{nameof(CoilPDIEntity.TBL_PDI)}]");
            sb.AppendLine($" Where [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{strEntry_Coil_ID}'");
            sb.AppendLine($" And  [{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}] = '{strPlan_No}'");
            string strSql = sb.ToString();
            return strSql;
        }

        /// <summary>
        /// 新增-詳細資料
        /// </summary>
        /// <param name="Qc_Remark"></param>
        /// <returns></returns>
        public static string SQL_Insert_PDI_Detail()
        {
            string strSql = $@"Insert into [{nameof(CoilPDIEntity.TBL_PDI)}] (
                                                [{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Mat_Seq_No)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Plan_Sort)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)}],
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
                                                [{nameof(CoilPDIEntity.TBL_PDI.Wholebacklog_Code)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Next_Wholebacklog_Code)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness)}],
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
                                                [{nameof(CoilPDIEntity.TBL_PDI.Qc_Remark)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Head_Off_Gauge)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Tail_Off_Gauge)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.CreateTime)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Sg_Sign)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Process_Code)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.CustomerCode)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.CustomerName_C)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.CustomerName_E)}],
                                                [{nameof(CoilPDIEntity.TBL_PDI.Is_Delete)}])
                                        Values ('{PublicForms.PDIDetail.Txt_PlanNo.Text}',
                                                '{PublicForms.PDIDetail.Txt_SeqNo.Text}',
                                                '{PublicForms.PDIDetail.Cob_Plan_Sort.SelectedValue}',
                                                '{PublicForms.PDIDetail.Txt_EntryCoil.Text}',
                                                '{PublicForms.PDIDetail.Txt_EntryThick.Text}',
                                                '{PublicForms.PDIDetail.Txt_EntryWidth.Text}',
                                                '{PublicForms.PDIDetail.Txt_EntryWeight.Text}',
                                                '{PublicForms.PDIDetail.Txt_EntryLen.Text}',
                                                '{PublicForms.PDIDetail.Txt_EntryInner.Text}',
                                                '{PublicForms.PDIDetail.Txt_EntryDcos.Text}',
                                                '{PublicForms.PDIDetail.Cob_Sleeve_Type_Entry.SelectedValue}',
                                                '{PublicForms.PDIDetail.Txt_Sleeve_Inner_Entry.Text}',
                                                '{PublicForms.PDIDetail.Cob_PAPER_REQ_CODE.SelectedValue}',
                                                '{PublicForms.PDIDetail.Cob_In_Paper_Type_Entry.SelectedValue}',
                                                '{PublicForms.PDIDetail.Txt_In_Paper_Head_Length.Text}',
                                                '{PublicForms.PDIDetail.Txt_In_Paper_Head_Width.Text}',
                                                '{PublicForms.PDIDetail.Txt_In_Paper_Tail_Length.Text}',
                                                '{PublicForms.PDIDetail.Txt_In_Paper_Tail_Width.Text}',
                                                '{PublicForms.PDIDetail.Txt_STAND_MAX.Text}',
                                                '{PublicForms.PDIDetail.Txt_STAND_MIN.Text}',
                                                '{PublicForms.PDIDetail.Txt_St_no.Text}',
                                                '{PublicForms.PDIDetail.Txt_Density.Text}',
                                                '{PublicForms.PDIDetail.Cob_Rework_Type.SelectedValue}',
                                                '{PublicForms.PDIDetail.Cob_Surface_Finishing_Code.SelectedValue}',
                                                '{PublicForms.PDIDetail.Cob_Surface_Accuracy_Code.SelectedValue}',
                                                '{PublicForms.PDIDetail.Cob_Base_Surface.SelectedValue}',
                                                '{PublicForms.PDIDetail.Cob_Uncoiler_Direction.SelectedValue}',
                                                '{PublicForms.PDIDetail.Txt_OutCoil.Text}',
                                                '{PublicForms.PDIDetail.Cob_Out_Paper_Req_Code.SelectedValue}',
                                                '{PublicForms.PDIDetail.Cob_Paper_Type_Exit.SelectedValue}',
                                                '{PublicForms.PDIDetail.Txt_Sleeve_Inner_Exit.Text}',
                                                '{PublicForms.PDIDetail.Cob_Sleeve_Type_Exit.SelectedValue}',
                                                '{PublicForms.PDIDetail.Txt_Strap.Text}',
                                                '{PublicForms.PDIDetail.Cob_Leader_Usage.SelectedValue}',
                                                '{PublicForms.PDIDetail.Cob_Samp.SelectedValue}',
                                                '{PublicForms.PDIDetail.Cob_SAMPLE_FRQN_CODE.SelectedValue}',
                                                '{PublicForms.PDIDetail.Txt_LotNo.Text}',
                                                '{PublicForms.PDIDetail.Cob_CoilOrigin.SelectedValue}',
                                                '{PublicForms.PDIDetail.Txt_Wholebacklog.Text}',
                                                '{PublicForms.PDIDetail.Txt_NWholebacklog.Text}',
                                                '{PublicForms.PDIDetail.Cob_Trim.SelectedValue}',
                                                '{PublicForms.PDIDetail.Txt_OutWidth.Text}',
                                                '{PublicForms.PDIDetail.Txt_WdMax.Text}',
                                                '{PublicForms.PDIDetail.Txt_WdMin.Text}',
                                                '{PublicForms.PDIDetail.Txt_Out_mat_thickness.Text}',
                                                '{PublicForms.PDIDetail.Txt_OutInner.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_no.Text}',
                                                '{PublicForms.PDIDetail.Txt_WtMax.Text}',
                                                '{PublicForms.PDIDetail.Txt_WtMin.Text}',
                                                '{PublicForms.PDIDetail.Txt_OrderWt.Text}',
                                                '{PublicForms.PDIDetail.Cob_Dividing_Flag.SelectedValue}',
                                                '{PublicForms.PDIDetail.Txt_Dividing_num.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_wt_1.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_wt_2.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_wt_3.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_wt_4.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_wt_5.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_wt_6.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_no_1.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_no_2.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_no_3.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_no_4.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_no_5.Text}',
                                                '{PublicForms.PDIDetail.Txt_Order_no_6.Text}',
                                                '',
                                                '{PublicForms.PDIDetail.Txt_Head_Off_Gauge.Text}',
                                                '{PublicForms.PDIDetail.Txt_Tail_off_gauge.Text}',
                                                '{GlobalVariableHandler.Instance.time}',
                                                '{PublicForms.PDIDetail.Txt_SG_SIGN.Text}',
                                                '{PublicForms.PDIDetail.Txt_ProcessCode.Text}',
                                                '{PublicForms.PDIDetail.Txt_CustomerNo.Text}',
                                                '{PublicForms.PDIDetail.Txt_CustomerName_C.Text}',
                                                '{PublicForms.PDIDetail.Txt_CustomerName_E.Text}',
                                                '0')";

            return strSql;
        }

       
        /// <summary>
        /// 新增-導帶資料
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_LeaderData()
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
                                   Values ( '{PublicForms.PDIDetail.Txt_EntryCoil.Text.Trim()}',
                                           '{PublicForms.PDIDetail.Txt_OutCoil.Text.Trim()}',
                                           '{PublicForms.PDIDetail.Txt_LeaderHeadSt_no.Text.Trim()}',
                                           '{PublicForms.PDIDetail.Txt_LeaderHeadLen.Text.Trim()}',
                                           '{PublicForms.PDIDetail.Txt_LeaderHeadWd.Text.Trim()}',
                                           '{PublicForms.PDIDetail.Txt_LeaderHeadThickness.Text.Trim()}',
                                           '{PublicForms.PDIDetail.Txt_LeaderTailSt_no.Text.Trim()}',
                                           '{PublicForms.PDIDetail.Txt_LeaderTailLen.Text.Trim()}',
                                           '{PublicForms.PDIDetail.Txt_LeaderTailWd.Text.Trim()}',
                                           '{PublicForms.PDIDetail.Txt_LeaderTailThickness.Text.Trim()}',
                                           '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                           '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";

            return strSql;
        }


        /// <summary>
        /// 新增-缺陷資料
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_DefectData()
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
                             Values ('{PublicForms.PDIDetail.Txt_EntryCoil.Text.Trim()}',
                                    '{PublicForms.PDIDetail.Txt_PlanNo.Text.Trim()}',
                                    '{PublicForms.PDIDetail.Txt_EntryCoil.Text.Trim()}',
                                    '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                    '{DateTime.Now:yyyy-MM-dd HH:mm:ss}',
                                    '{PublicForms.PDIDetail.Txt_Code_D01.Text}',
                                    '{PublicForms.PDIDetail.Txt_Code_D02.Text}',
                                    '{PublicForms.PDIDetail.Txt_Code_D03.Text}',
                                    '{PublicForms.PDIDetail.Txt_Code_D04.Text}',
                                    '{PublicForms.PDIDetail.Txt_Code_D05.Text}',
                                    '{PublicForms.PDIDetail.Txt_Code_D06.Text}',
                                    '{PublicForms.PDIDetail.Txt_Code_D07.Text}',
                                    '{PublicForms.PDIDetail.Txt_Code_D08.Text}',
                                    '{PublicForms.PDIDetail.Txt_Code_D09.Text}',
                                    '{PublicForms.PDIDetail.Txt_Code_D10.Text}',
                                    '{PublicForms.PDIDetail.Txt_Origin_D01.Text}',
                                    '{PublicForms.PDIDetail.Txt_Origin_D02.Text}',
                                    '{PublicForms.PDIDetail.Txt_Origin_D03.Text}',
                                    '{PublicForms.PDIDetail.Txt_Origin_D04.Text}',
                                    '{PublicForms.PDIDetail.Txt_Origin_D05.Text}',
                                    '{PublicForms.PDIDetail.Txt_Origin_D06.Text}',
                                    '{PublicForms.PDIDetail.Txt_Origin_D07.Text}',
                                    '{PublicForms.PDIDetail.Txt_Origin_D08.Text}',
                                    '{PublicForms.PDIDetail.Txt_Origin_D09.Text}',
                                    '{PublicForms.PDIDetail.Txt_Origin_D10.Text}',
                                    '{PublicForms.PDIDetail.Cob_Sid_D01.Text}',
                                    '{PublicForms.PDIDetail.Cob_Sid_D02.Text}',
                                    '{PublicForms.PDIDetail.Cob_Sid_D03.Text}',
                                    '{PublicForms.PDIDetail.Cob_Sid_D04.Text}',
                                    '{PublicForms.PDIDetail.Cob_Sid_D05.Text}',
                                    '{PublicForms.PDIDetail.Cob_Sid_D06.Text}',
                                    '{PublicForms.PDIDetail.Cob_Sid_D07.Text}',
                                    '{PublicForms.PDIDetail.Cob_Sid_D08.Text}',
                                    '{PublicForms.PDIDetail.Cob_Sid_D09.Text}',
                                    '{PublicForms.PDIDetail.Cob_Sid_D10.Text}',
                                    '{PublicForms.PDIDetail.Cob_PosW_D01.Text}',
                                    '{PublicForms.PDIDetail.Cob_PosW_D02.Text}',
                                    '{PublicForms.PDIDetail.Cob_PosW_D03.Text}',
                                    '{PublicForms.PDIDetail.Cob_PosW_D04.Text}',
                                    '{PublicForms.PDIDetail.Cob_PosW_D05.Text}',
                                    '{PublicForms.PDIDetail.Cob_PosW_D06.Text}',
                                    '{PublicForms.PDIDetail.Cob_PosW_D07.Text}',
                                    '{PublicForms.PDIDetail.Cob_PosW_D08.Text}',
                                    '{PublicForms.PDIDetail.Cob_PosW_D09.Text}',
                                    '{PublicForms.PDIDetail.Cob_PosW_D10.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_Start_D01.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_Start_D02.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_Start_D03.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_Start_D04.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_Start_D05.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_Start_D06.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_Start_D07.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_Start_D08.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_Start_D09.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_Start_D10.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_End_D01.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_End_D02.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_End_D03.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_End_D04.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_End_D05.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_End_D06.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_End_D07.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_End_D08.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_End_D09.Text}',
                                    '{PublicForms.PDIDetail.Txt_Pos_L_End_D10.Text}',
                                    '{PublicForms.PDIDetail.Cob_Level_D01.Text}',
                                    '{PublicForms.PDIDetail.Cob_Level_D02.Text}',
                                    '{PublicForms.PDIDetail.Cob_Level_D03.Text}',
                                    '{PublicForms.PDIDetail.Cob_Level_D04.Text}',
                                    '{PublicForms.PDIDetail.Cob_Level_D05.Text}',
                                    '{PublicForms.PDIDetail.Cob_Level_D06.Text}',
                                    '{PublicForms.PDIDetail.Cob_Level_D07.Text}',
                                    '{PublicForms.PDIDetail.Cob_Level_D08.Text}',
                                    '{PublicForms.PDIDetail.Cob_Level_D09.Text}',
                                    '{PublicForms.PDIDetail.Cob_Level_D10.Text}',
                                    '{PublicForms.PDIDetail.Txt_Percent_D01.Text}',
                                    '{PublicForms.PDIDetail.Txt_Percent_D02.Text}',
                                    '{PublicForms.PDIDetail.Txt_Percent_D03.Text}',
                                    '{PublicForms.PDIDetail.Txt_Percent_D04.Text}',
                                    '{PublicForms.PDIDetail.Txt_Percent_D05.Text}',
                                    '{PublicForms.PDIDetail.Txt_Percent_D06.Text}',
                                    '{PublicForms.PDIDetail.Txt_Percent_D07.Text}',
                                    '{PublicForms.PDIDetail.Txt_Percent_D08.Text}',
                                    '{PublicForms.PDIDetail.Txt_Percent_D09.Text}',
                                    '{PublicForms.PDIDetail.Txt_Percent_D10.Text}',
                                    '{PublicForms.PDIDetail.Txt_QGRADE_D01.Text}',
                                    '{PublicForms.PDIDetail.Txt_QGRADE_D02.Text}',
                                    '{PublicForms.PDIDetail.Txt_QGRADE_D03.Text}',
                                    '{PublicForms.PDIDetail.Txt_QGRADE_D04.Text}',
                                    '{PublicForms.PDIDetail.Txt_QGRADE_D05.Text}',
                                    '{PublicForms.PDIDetail.Txt_QGRADE_D06.Text}',
                                    '{PublicForms.PDIDetail.Txt_QGRADE_D07.Text}',
                                    '{PublicForms.PDIDetail.Txt_QGRADE_D08.Text}',
                                    '{PublicForms.PDIDetail.Txt_QGRADE_D09.Text}',
                                    '{PublicForms.PDIDetail.Txt_QGRADE_D10.Text}')";

            return strSql;
        }


        /// <summary>
        /// 搜尋鋼捲PDI刪除註記
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_CheckDeleteFlag(string Coil_ID)
        {
            string strSql = $@" Select [{nameof(CoilPDIEntity.TBL_PDI.Is_Delete)}] From [{nameof(CoilPDIEntity.TBL_PDI)}] Where [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }


        /// <summary>
        /// 刪除PDI-修改鋼捲PDI刪除註記
        /// </summary>
        /// <param name="Plan_No"></param>
        /// <param name="Seq_No"></param>
        /// <param name="Coil_ID"></param>
        /// <returns></returns>
        public static string SQL_Update_PDI_IsDelete(string Plan_No, string Seq_No, string Coil_ID)
        {
            string strSql = $@"Update [{nameof(CoilPDIEntity.TBL_PDI)}] set [{nameof(CoilPDIEntity.TBL_PDI.Is_Delete)}] = '1' ,
                                              [{nameof(CoilPDIEntity.TBL_PDI.Delete_DateTime)}] = '{GlobalVariableHandler.Instance.time }',
                                              [{nameof(CoilPDIEntity.TBL_PDI.Delete_UserID)}] = '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}'
                        Where [{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}] = '{Plan_No} '
                        and [{nameof(CoilPDIEntity.TBL_PDI.Mat_Seq_No)}] = {Seq_No}
                        and [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }


        /// <summary>
        /// 修改鋼捲PDI詳細資料
        /// </summary>
        /// <param name="Qc_Remark"></param>
        /// <returns></returns>
        public static string SQL_Update_CoilDetailData()
        {
            string strSql = $@"Update [{nameof(CoilPDIEntity.TBL_PDI)}]  set 
                                               [{nameof(CoilPDIEntity.TBL_PDI.Plan_No)}]='{PublicForms.PDIDetail.Txt_PlanNo.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Mat_Seq_No)}]='{PublicForms.PDIDetail.Txt_SeqNo.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Plan_Sort)}]='{PublicForms.PDIDetail.Cob_Plan_Sort.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]='{PublicForms.PDIDetail.Txt_EntryCoil.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)}]='{PublicForms.PDIDetail.Txt_EntryThick.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)}]='{PublicForms.PDIDetail.Txt_EntryWidth.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)}]='{PublicForms.PDIDetail.Txt_EntryWeight.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)}]='{PublicForms.PDIDetail.Txt_EntryLen.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)}]='{PublicForms.PDIDetail.Txt_EntryInner.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)}]='{PublicForms.PDIDetail.Txt_EntryDcos.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Sleeve_Type_Code)}]='{PublicForms.PDIDetail.Cob_Sleeve_Type_Entry.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Sleeve_diamter)}]='{PublicForms.PDIDetail.Txt_Sleeve_Inner_Entry.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Paper_Req_Code)}]='{PublicForms.PDIDetail.Cob_PAPER_REQ_CODE.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Paper_Code)}]='{PublicForms.PDIDetail.Cob_In_Paper_Type_Entry.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Length)}]='{PublicForms.PDIDetail.Txt_In_Paper_Head_Length.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Width)}]='{PublicForms.PDIDetail.Txt_In_Paper_Head_Width.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Length)}]='{PublicForms.PDIDetail.Txt_In_Paper_Tail_Length.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Width)}]='{PublicForms.PDIDetail.Txt_In_Paper_Tail_Width.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Max)}]='{PublicForms.PDIDetail.Txt_STAND_MAX.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Min)}]='{PublicForms.PDIDetail.Txt_STAND_MIN.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.St_No)}]='{PublicForms.PDIDetail.Txt_St_no.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Density)}]='{PublicForms.PDIDetail.Txt_Density.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.REPAIR_TYPE)}]='{PublicForms.PDIDetail.Cob_Rework_Type.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Surface_Finishing_Code)}]='{PublicForms.PDIDetail.Cob_Surface_Finishing_Code.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy)}]='{PublicForms.PDIDetail.Cob_Surface_Accuracy_Code.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Base_Surface)}]='{PublicForms.PDIDetail.Cob_Base_Surface.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Uncoiler_Direction)}]='{PublicForms.PDIDetail.Cob_Uncoiler_Direction.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)}]='{PublicForms.PDIDetail.Txt_OutCoil.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Req_Code)}]='{PublicForms.PDIDetail.Cob_Out_Paper_Req_Code.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code)}]='{PublicForms.PDIDetail.Cob_Paper_Type_Exit.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Diamter)}]='{PublicForms.PDIDetail.Txt_Sleeve_Inner_Exit.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)}]='{PublicForms.PDIDetail.Cob_Sleeve_Type_Exit.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Strap_Num)}]='{PublicForms.PDIDetail.Txt_Strap.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)}]='{PublicForms.PDIDetail.Cob_Leader_Usage.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Sample_Flag)}]='{PublicForms.PDIDetail.Cob_Samp.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Sample_Frqn_Code)}]='{PublicForms.PDIDetail.Cob_SAMPLE_FRQN_CODE.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Sample_Lot_No)}]='{PublicForms.PDIDetail.Txt_LotNo.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Coil_Origin)}]='{PublicForms.PDIDetail.Cob_CoilOrigin.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Wholebacklog_Code)}]='{PublicForms.PDIDetail.Txt_Wholebacklog.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Next_Wholebacklog_Code)}]='{PublicForms.PDIDetail.Txt_NWholebacklog.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)}]='{PublicForms.PDIDetail.Cob_Trim.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)}]='{PublicForms.PDIDetail.Txt_OutWidth.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)}]='{PublicForms.PDIDetail.Txt_WdMax.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)}]='{PublicForms.PDIDetail.Txt_WdMin.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness)}]='{PublicForms.PDIDetail.Txt_Out_mat_thickness.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Inner)}]='{PublicForms.PDIDetail.Txt_OutInner.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No)}]='{PublicForms.PDIDetail.Txt_Order_no.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Max)}]='{PublicForms.PDIDetail.Txt_WdMax.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Min)}]='{PublicForms.PDIDetail.Txt_WtMin.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Order_Wt)}]='{PublicForms.PDIDetail.Txt_OrderWt.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)}]='{PublicForms.PDIDetail.Cob_Dividing_Flag.SelectedValue}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Dividing_Num)}]='{PublicForms.PDIDetail.Txt_Dividing_num.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_1)}]='{PublicForms.PDIDetail.Txt_Order_wt_1.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_2)}]='{PublicForms.PDIDetail.Txt_Order_wt_2.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_3)}]='{PublicForms.PDIDetail.Txt_Order_wt_3.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_4)}]='{PublicForms.PDIDetail.Txt_Order_wt_4.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_5)}]='{PublicForms.PDIDetail.Txt_Order_wt_5.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Orderwt_6)}]='{PublicForms.PDIDetail.Txt_Order_wt_6.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No_1)}]='{PublicForms.PDIDetail.Txt_Order_no_1.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No_2)}]='{PublicForms.PDIDetail.Txt_Order_no_2.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No_3)}]='{PublicForms.PDIDetail.Txt_Order_no_3.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No_4)}]='{PublicForms.PDIDetail.Txt_Order_no_4.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No_5)}]='{PublicForms.PDIDetail.Txt_Order_no_5.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Order_No_6)}]='{PublicForms.PDIDetail.Txt_Order_no_6.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Qc_Remark)}]='',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Head_Off_Gauge)}]='{PublicForms.PDIDetail.Txt_Head_Off_Gauge.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Tail_Off_Gauge)}]='{PublicForms.PDIDetail.Txt_Tail_off_gauge.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Process_Code)}]='{PublicForms.PDIDetail.Txt_ProcessCode.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.CustomerCode)}]='{PublicForms.PDIDetail.Txt_CustomerNo.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.CustomerName_C)}]='{PublicForms.PDIDetail.Txt_CustomerName_C.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.CustomerName_E)}]='{PublicForms.PDIDetail.Txt_CustomerName_E.Text}',
                                               [{nameof(CoilPDIEntity.TBL_PDI.Sg_Sign)}]='{PublicForms.PDIDetail.Txt_SG_SIGN.Text}'
                            Where [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}]='{PublicForms.PDIDetail.Txt_EntryCoil.Text}'";

            return strSql;
        }


        /// <summary>
        /// 修改導帶資料
        /// </summary>
        /// <returns></returns>
        public static string SQL_Update_LeaderData()
        {
            string strSql = $@"Update [{nameof(LeaderTempEntity.TBL_Leader_Temp)}]
                                  Set [{nameof(LeaderTempEntity.TBL_Leader_Temp.OriPDI_Out_Coil_ID)}] = '{PublicForms.PDIDetail.Txt_OutCoil.Text.Trim()}',
                                      [{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_St_No)}] = '{PublicForms.PDIDetail.Txt_LeaderHeadSt_no.Text.Trim()}',
                                      [{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Length)}] = '{PublicForms.PDIDetail.Txt_LeaderHeadLen.Text.Trim()}',
                                      [{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Width)}] = '{PublicForms.PDIDetail.Txt_LeaderHeadWd.Text.Trim()}',
                                      [{nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Thickness)}] = '{PublicForms.PDIDetail.Txt_LeaderHeadThickness.Text.Trim()}',
                                      [{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_St_No)}] = '{PublicForms.PDIDetail.Txt_LeaderTailSt_no.Text.Trim()}',
                                      [{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Length)}] = '{PublicForms.PDIDetail.Txt_LeaderTailLen.Text.Trim()}',
                                      [{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Width)}] = '{PublicForms.PDIDetail.Txt_LeaderTailWd.Text.Trim()}',
                                      [{nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Thickness)}] = '{PublicForms.PDIDetail.Txt_LeaderTailThickness.Text.Trim()}',
                                      [{nameof(LeaderTempEntity.TBL_Leader_Temp.CreateTime)}] = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'
                                Where [{nameof(LeaderTempEntity.TBL_Leader_Temp.Coil_ID)}] = '{PublicForms.PDIDetail.Txt_EntryCoil.Text.Trim()}'";

            return strSql;
        }


        /// <summary>
        /// 修改缺陷资料
        /// </summary>
        /// <returns></returns>
        public static string SQL_Update_DefectData()
        {
            string strSql = $@"Update [{nameof(CoilDefectDataEntity.TBL_Coil_Defect)}]
                           Set [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Code)}] = '{PublicForms.PDIDetail.Txt_Code_D01.Text.Trim()}',         
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Origin)}] = '{PublicForms.PDIDetail.Txt_Origin_D01.Text.Trim()}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Sid)}] = '{PublicForms.PDIDetail.Cob_Sid_D01.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_W)}] = '{PublicForms.PDIDetail.Cob_PosW_D01.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_Start)}] = '{PublicForms.PDIDetail.Txt_Pos_L_Start_D01.Text.Trim()}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_End)}] = '{PublicForms.PDIDetail.Txt_Pos_L_End_D01.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Level)}] = '{PublicForms.PDIDetail.Cob_Level_D01.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Percent)}] = '{PublicForms.PDIDetail.Txt_Percent_D01.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_QGRADE)}] = '{PublicForms.PDIDetail.Txt_QGRADE_D01.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Code)}] = '{PublicForms.PDIDetail.Txt_Code_D02.Text.Trim()}', 
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Origin)}] = '{PublicForms.PDIDetail.Txt_Origin_D02.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Sid)}] = '{PublicForms.PDIDetail.Cob_Sid_D02.Text}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_W)}] = '{PublicForms.PDIDetail.Cob_PosW_D02.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_Start)}] = '{PublicForms.PDIDetail.Txt_Pos_L_Start_D02.Text.Trim()}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_End)}] = '{PublicForms.PDIDetail.Txt_Pos_L_End_D02.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Level)}] = '{PublicForms.PDIDetail.Cob_Level_D02.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Percent)}] = '{PublicForms.PDIDetail.Txt_Percent_D02.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_QGRADE)}] = '{PublicForms.PDIDetail.Txt_QGRADE_D02.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Code)}] = '{PublicForms.PDIDetail.Txt_Code_D03.Text.Trim()}', 
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Origin)}] = '{PublicForms.PDIDetail.Txt_Origin_D03.Text.Trim()}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Sid)}] = '{PublicForms.PDIDetail.Cob_Sid_D03.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_W)}] = '{PublicForms.PDIDetail.Cob_PosW_D03.Text}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_Start)}] = '{PublicForms.PDIDetail.Txt_Pos_L_Start_D03.Text.Trim()}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_End)}] = '{PublicForms.PDIDetail.Txt_Pos_L_End_D03.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Level)}] = '{PublicForms.PDIDetail.Cob_Level_D03.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Percent)}] = '{PublicForms.PDIDetail.Txt_Percent_D03.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_QGRADE)}] = '{PublicForms.PDIDetail.Txt_QGRADE_D03.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Code)}] = '{PublicForms.PDIDetail.Txt_Code_D04.Text.Trim()}',         
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Origin)}] = '{PublicForms.PDIDetail.Txt_Origin_D04.Text.Trim()}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Sid)}] = '{PublicForms.PDIDetail.Cob_Sid_D04.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_W)}] = '{PublicForms.PDIDetail.Cob_PosW_D04.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_Start)}] = '{PublicForms.PDIDetail.Txt_Pos_L_Start_D04.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_End)}] = '{PublicForms.PDIDetail.Txt_Pos_L_End_D04.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Level)}] = '{PublicForms.PDIDetail.Cob_Level_D04.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Percent)}] = '{PublicForms.PDIDetail.Txt_Percent_D04.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_QGRADE)}] = '{PublicForms.PDIDetail.Txt_QGRADE_D04.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Code)}] = '{PublicForms.PDIDetail.Txt_Code_D05.Text.Trim()}',       
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Origin)}] = '{PublicForms.PDIDetail.Txt_Origin_D05.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Sid)}] = '{PublicForms.PDIDetail.Cob_Sid_D05.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_W)}] = '{PublicForms.PDIDetail.Cob_PosW_D05.Text}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_Start)}] = '{PublicForms.PDIDetail.Txt_Pos_L_Start_D05.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_End)}] = '{PublicForms.PDIDetail.Txt_Pos_L_End_D05.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Level)}] = '{PublicForms.PDIDetail.Cob_Level_D05.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Percent)}] = '{PublicForms.PDIDetail.Txt_Percent_D05.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_QGRADE)}] = '{PublicForms.PDIDetail.Txt_QGRADE_D05.Text.Trim()}', 

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Code)}] = '{PublicForms.PDIDetail.Txt_Code_D06.Text.Trim()}',        
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Origin)}] = '{PublicForms.PDIDetail.Txt_Origin_D06.Text.Trim()}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Sid)}] = '{PublicForms.PDIDetail.Cob_Sid_D06.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_W)}] = '{PublicForms.PDIDetail.Cob_PosW_D06.Text}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_Start)}] = '{PublicForms.PDIDetail.Txt_Pos_L_Start_D06.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_End)}] = '{PublicForms.PDIDetail.Txt_Pos_L_End_D06.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Level)}] = '{PublicForms.PDIDetail.Cob_Level_D06.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Percent)}] = '{PublicForms.PDIDetail.Txt_Percent_D06.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_QGRADE)}] = '{PublicForms.PDIDetail.Txt_QGRADE_D06.Text.Trim()}',   

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Code)}] = '{PublicForms.PDIDetail.Txt_Code_D07.Text.Trim()}',        
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Origin)}] = '{PublicForms.PDIDetail.Txt_Origin_D07.Text.Trim()}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Sid)}] = '{PublicForms.PDIDetail.Cob_Sid_D07.Text}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_W)}] = '{PublicForms.PDIDetail.Cob_PosW_D07.Text}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_Start)}] = '{PublicForms.PDIDetail.Txt_Pos_L_Start_D07.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_End)}] = '{PublicForms.PDIDetail.Txt_Pos_L_End_D07.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Level)}] = '{PublicForms.PDIDetail.Cob_Level_D07.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Percent)}] = '{PublicForms.PDIDetail.Txt_Percent_D07.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_QGRADE)}] = '{PublicForms.PDIDetail.Txt_QGRADE_D07.Text.Trim()}',

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Code)}] = '{PublicForms.PDIDetail.Txt_Code_D08.Text.Trim()}',      
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Origin)}] = '{PublicForms.PDIDetail.Txt_Origin_D08.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Sid)}] = '{PublicForms.PDIDetail.Cob_Sid_D08.Text}',     
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_W)}] = '{PublicForms.PDIDetail.Cob_PosW_D08.Text}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_Start)}] = '{PublicForms.PDIDetail.Txt_Pos_L_Start_D08.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_End)}] = '{PublicForms.PDIDetail.Txt_Pos_L_End_D08.Text.Trim()}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Level)}] = '{PublicForms.PDIDetail.Cob_Level_D08.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Percent)}] = '{PublicForms.PDIDetail.Txt_Percent_D08.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_QGRADE)}] = '{PublicForms.PDIDetail.Txt_QGRADE_D08.Text.Trim()}',  

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Code)}] = '{PublicForms.PDIDetail.Txt_Code_D09.Text.Trim()}',         
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Origin)}] = '{PublicForms.PDIDetail.Txt_Origin_D09.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Sid)}] = '{PublicForms.PDIDetail.Cob_Sid_D09.Text}',    
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_W)}] = '{PublicForms.PDIDetail.Cob_PosW_D09.Text}',  
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_Start)}] = '{PublicForms.PDIDetail.Txt_Pos_L_Start_D09.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_End)}] = '{PublicForms.PDIDetail.Txt_Pos_L_End_D09.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Level)}] = '{PublicForms.PDIDetail.Cob_Level_D09.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Percent)}] = '{PublicForms.PDIDetail.Txt_Percent_D09.Text.Trim()}', 
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_QGRADE)}] = '{PublicForms.PDIDetail.Txt_QGRADE_D09.Text.Trim()}',  

                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Code)}] = '{PublicForms.PDIDetail.Txt_Code_D10.Text.Trim()}',          
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Origin)}] = '{PublicForms.PDIDetail.Txt_Origin_D10.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Sid)}] = '{PublicForms.PDIDetail.Cob_Sid_D10.Text}',   
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_W)}] = '{PublicForms.PDIDetail.Cob_PosW_D10.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_Start)}] = '{PublicForms.PDIDetail.Txt_Pos_L_Start_D10.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_End)}] = '{PublicForms.PDIDetail.Txt_Pos_L_End_D10.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Level)}] = '{PublicForms.PDIDetail.Cob_Level_D10.Text}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Percent)}] = '{PublicForms.PDIDetail.Txt_Percent_D10.Text.Trim()}',
                               [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_QGRADE)}] = '{PublicForms.PDIDetail.Txt_QGRADE_D10.Text.Trim()}'
                         Where [{nameof(CoilDefectDataEntity.TBL_Coil_Defect.Entry_Coil_ID)}] = '{PublicForms.PDIDetail.Txt_EntryCoil.Text.Trim()}'";

            return strSql;
        }

        #endregion


        #region ---ComboBox---

        /// <summary>
        /// 入口鋼卷號下拉式選單
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_Entry_Coil_No_ComboBoxItems()
        {
            string strSql = $@"Select [{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] From [{nameof(CoilPDIEntity.TBL_PDI)}] Where ([{nameof(CoilPDIEntity.TBL_PDI.Is_Delete)}] <> '1' or [{nameof(CoilPDIEntity.TBL_PDI.Is_Delete)}] is null )";

            return strSql;
        }


        #endregion

    }
}
