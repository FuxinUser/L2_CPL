using System;
using System.Data;
using System.Windows.Forms;
using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using static CPL1HMI.DataBaseTableFactory;


namespace CPL1HMI
{
    public partial class frm_PDOConfirm : Form
    {
        #region 變數
        string strSql = "";
        DataTable dt_Out_mat_No = new DataTable();
        DataTable dtPDO = new DataTable();
        DataTable dtDefect = new DataTable();
        #endregion

        public frm_PDOConfirm()
        {
            InitializeComponent();
        }
        private void frm_PDOConfirm_Load(object sender, EventArgs e)
        {
            ComboBoxLoad();
        }
        #region ComboBox
        /// <summary>
        /// ComboBox Items
        /// </summary>
        private void ComboBoxLoad()
        {
            //班次
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Shift, cbo_Shift);
            //班別
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Team, cbo_Team);
            //取樣要求
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Samp, cbo_Sample_flag);
            //切邊標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Trim, cbo_trim_flag);
            //分卷标记
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Trim, cbo_Segement_Flag);
            //最终卷标记
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.End, cbo_end_flag);
            //廢品標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Scrap, cbo_SCRAP_FLAG);
            //出口墊紙種類
            ComboBoxIndexHandler.Instance.SelectPaper(cbo_Out_Paper_Type);
            //出口墊紙方式
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.PAPER_REQ_CODE, Cbo_OUT_PAPER_REQ_CODE);
            //卷曲方向
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Winding_Direction, cbo_Curly_Direction);
            //好面朝向
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Base_Surface, cbo_Base_Surface);
            //出口套筒類型
            ComboBoxIndexHandler.Instance.SelectSleeve(cbo_Sleeve_Type_Exit);
            //表面精度
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, cbo_SURFACE_ACCU_CODE);
            //内表面精度
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, cbo_SURFACE_ACCU_CODE_IN);
            //外表面精度
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, cbo_SURFACE_ACCU_CODE_OUT);
            //封鎖標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Hold, cbo_Hold_flag);
            //翻面標記
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.FLIP, cboFlip_Tag);
            //取樣位置 
            ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.SAMPLE_FRQN_CODE, cbo_SAMPLE_FRQN_CODE);

            strSql = SqlFactory.frm_PDOConfirm_CoilIDComboBoxItems_DB_Map_PDO();
            try
            {
                dt_Out_mat_No = DataAccess.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_CPL);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.Log(System_ID.Client, "PDO確認", Event_Type.Debug, "使用者:" + PublicForms.Main.lblLoginUser.Text + "出口卷號清單查询", $"搜尋出口號語法:{strSql}有錯誤:{ex.ToString()}");
            }
            cbo_Out_mat_NO.DisplayMember = nameof(L2L3_PDO.Out_mat_No);
            cbo_Out_mat_NO.ValueMember = nameof(L2L3_PDO.Out_mat_No);
            cbo_Out_mat_NO.DataSource = dt_Out_mat_No;
        }
        #endregion 

        #region PDO資料
        /// <summary>
        /// PDO資料組成
        /// </summary>
        public void PDOselect_Table(string Coil_ID)
        {
            strSql = SqlFactory.frm_PDOConfirm_CoilPDO_DB_PDO(Coil_ID);
            try
            {
                dtPDO = DataAccess.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_CPL);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.Log(System_ID.Client, "PDO確認", Event_Type.Debug, "使用者:" + PublicForms.Main.lblLoginUser.Text + "出口卷PDO查询", $"搜尋PDO語法:{strSql}有錯誤:{ex.ToString()}");

            }
            #region - 填入資料 -
            //合同號
            txtOrderNo.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Order_No)].ToString().Equals(null) ?
                txtOrderNo.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Order_No)].ToString() : txtOrderNo.Text = "";
            //計畫號
            txtPlanNo.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Plan_No)].ToString().Equals(null) ?
                txtPlanNo.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Plan_No)].ToString() : txtPlanNo.Text = "";
            //出口卷號
            txtOut_mat_No.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Out_mat_No)].ToString().Equals(null) ?
                txtOut_mat_No.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Out_mat_No)].ToString() : txtOut_mat_No.Text = "";
            //入口卷號
            txtIn_mat_No.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.In_mat_No)].ToString().Equals(null) ?
                 txtIn_mat_No.Text = dtPDO.Rows[0][nameof(L2L3_PDO.In_mat_No)].ToString() : txtIn_mat_No.Text = "";
            //生產開始時間
            txtStartTime.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.StartTime)].ToString().Equals(null) ?
                txtStartTime.Text = dtPDO.Rows[0][nameof(L2L3_PDO.StartTime)].ToString() : txtStartTime.Text = "";
            //生產結束時間
            txtEndTime.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.FinishTime)].ToString().Equals(null) ?
                txtEndTime.Text = dtPDO.Rows[0][nameof(L2L3_PDO.FinishTime)].ToString() : txtEndTime.Text = "";
            //班次
            cbo_Shift.SelectedValue = dtPDO.Rows[0][nameof(L2L3_PDO.Shift)].ToString() ?? string.Empty;
            //班別
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Team(cbo_Team, dtPDO.Rows[0][nameof(L2L3_PDO.Team)].ToString());
            //取樣標記
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(cbo_Sample_flag, dtPDO.Rows[0][nameof(L2L3_PDO.Sample_Flag)].ToString());
            //取樣位置
            ComboBoxIndexHandler.Instance.ComboBoxIndex_SAMPLE_FRQN_CODE(cbo_SAMPLE_FRQN_CODE, dtPDO.Rows[0][nameof(L2L3_PDO.Sample_Frqn_Code)].ToString());
            //切邊標記
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(cbo_trim_flag, dtPDO.Rows[0][nameof(L2L3_PDO.Trim_Flag)].ToString());
            //分卷標記
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(cbo_Segement_Flag, dtPDO.Rows[0][nameof(L2L3_PDO.Fixed_WT_Flag)].ToString());
            //最終卷標記
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(cbo_end_flag, dtPDO.Rows[0][nameof(L2L3_PDO.End_Flag)].ToString());
            //廢品標記
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(cbo_SCRAP_FLAG, dtPDO.Rows[0][nameof(L2L3_PDO.Scrap_Flag)].ToString());
            //卷曲方向
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(cbo_Curly_Direction, dtPDO.Rows[0][nameof(L2L3_PDO.Winding_Direction)].ToString());
            //好面朝向
            ComboBoxIndexHandler.Instance.ComboBoxIndex_SURFACE(cbo_Base_Surface, dtPDO.Rows[0][nameof(L2L3_PDO.Base_Surface)].ToString());
            //翻面標記
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(cbo_Base_Surface, dtPDO.Rows[0][nameof(L2L3_PDO.Base_Surface)].ToString());
            //出口外徑
            txtOuterDiameter.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Outer_Diameter)].ToString().Equals(null) ?
                txtOuterDiameter.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Outer_Diameter)].ToString() : txtOuterDiameter.Text = "";
            //出口內徑
            txtInner.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Inner)].ToString().Equals(null) ?
                txtInner.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Inner)].ToString() : txtInner.Text = "";
            //出口淨重
            txtWt.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Wt)].ToString().Equals(null) ?
                txtWt.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Wt)].ToString() : txtWt.Text = "";
            //出口毛重
            txtGs.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Gross_WT)].ToString().Equals(null) ?
                txtGs.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Gross_WT)].ToString() : txtGs.Text = "";
            //出口厚度
            txtThick.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Thick)].ToString().Equals(null) ?
                txtThick.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Thick)].ToString() : txtThick.Text = "";
            //出口寬度
            txtWidth.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Width)].ToString().Equals(null) ?
                txtWidth.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Width)].ToString() : txtWidth.Text = "";
            //出口長度
            txtLen.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Length)].ToString().Equals(null) ?
                txtLen.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Out_Mat_Length)].ToString() : txtLen.Text = "";
            //出口頭部墊紙長度
            txtOut_Paper_Head_Length.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Out_Head_Paper_Length)].ToString().Equals(null) ?
                txtOut_Paper_Head_Length.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Out_Head_Paper_Length)].ToString() : txtOut_Paper_Head_Length.Text = "";
            //出口頭部墊紙寬度
            txtOut_Paper_Head_Width.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Out_Head_Paper_Width)].ToString().Equals(null) ?
                txtOut_Paper_Head_Width.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Out_Head_Paper_Width)].ToString() : txtOut_Paper_Head_Width.Text = "";
            //出口尾部墊紙長度
            txtOut_Paper_Tail_Length.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Out_Tail_Paper_Length)].ToString().Equals(null) ?
                txtOut_Paper_Tail_Length.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Out_Tail_Paper_Length)].ToString() : txtOut_Paper_Tail_Length.Text = "";
            //出口尾部墊紙寬度
            txtOut_Paper_Tail_Width.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Out_Tail_Paper_Width)].ToString().Equals(null) ?
                txtOut_Paper_Tail_Width.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Out_Tail_Paper_Width)].ToString() : txtOut_Paper_Tail_Width.Text = "";
            //出口套筒內徑
            txt_PDI_Sleeve_Inner_Exit.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Sleeve_Inner_Exit_Diamter)].ToString().Equals(null) ?
                txt_PDI_Sleeve_Inner_Exit.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Sleeve_Inner_Exit_Diamter)].ToString() : txt_PDI_Sleeve_Inner_Exit.Text = "";
            //出口套筒類型
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(cbo_Sleeve_Type_Exit, dtPDO.Rows[0][nameof(L2L3_PDO.Sleeve_Type_Exit_Code)].ToString());
            //出口墊紙方式
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(Cbo_OUT_PAPER_REQ_CODE, dtPDO.Rows[0][nameof(L2L3_PDO.Paper_Req_Code)].ToString());
            //出口墊紙種類
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Paper(cbo_Out_Paper_Type, dtPDO.Rows[0][nameof(L2L3_PDO.Paper_Code)].ToString());//出口墊紙種類
            //頭部打孔位置
            txtHead_PunchHole_Position.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Head_Hole_Position)].ToString().Equals(null) ?
                txtHead_PunchHole_Position.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Head_Hole_Position)].ToString() : txtHead_PunchHole_Position.Text = "";
            //頭部導帶長度
            txtHead_LeaderLength.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Head_Leader_Length)].ToString().Equals(null) ?
                txtHead_LeaderLength.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Head_Leader_Length)].ToString() : txtHead_LeaderLength.Text = "";
            //頭部導帶寬度
            txtHead_Leader_Width.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Head_Leader_Width)].ToString().Equals(null) ?
                txtHead_Leader_Width.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Head_Leader_Width)].ToString() : txtHead_Leader_Width.Text = "";
            //頭部導帶厚度
            txtHead_Leader_Thickness.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Head_Leader_Thickness)].ToString().Equals(null) ?
                txtHead_Leader_Thickness.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Head_Leader_Thickness)].ToString() : txtHead_Leader_Thickness.Text = "";
            //頭部切廢長度
            txtScraped_Length_Entry.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Scraped_Length_Entry)].ToString().Equals(null) ?
                txtScraped_Length_Entry.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Scraped_Length_Entry)].ToString() : txtScraped_Length_Entry.Text = "";
            //頭部導帶鋼種
            txtHead_leader_st_no.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Head_Leader_St_No)].ToString().Equals(null) ?
                txtHead_leader_st_no.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Head_Leader_St_No)].ToString() : txtHead_leader_st_no.Text = "";
            //尾部打孔位置
            txtTail_PunchHole_Position.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Tail_PunchHole_Position)].ToString().Equals(null) ?
                txtTail_PunchHole_Position.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Tail_PunchHole_Position)].ToString() : txtTail_PunchHole_Position.Text = "";
            //尾部導帶長度
            txtTail_LeaderLength.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Tail_Leader_Length)].ToString().Equals(null) ?
                txtTail_LeaderLength.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Tail_Leader_Length)].ToString() : txtTail_LeaderLength.Text = "";
            //尾部導帶寬度
            txtTail_Leader_Width.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Tail_Leader_Width)].ToString().Equals(null) ?
                txtTail_Leader_Width.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Tail_Leader_Width)].ToString() : txtTail_Leader_Width.Text = "";
            //尾部導帶厚度
            txtTail_Leader_Thickness.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Tail_Leader_Thickness)].ToString().Equals(null) ?
                txtTail_Leader_Thickness.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Tail_Leader_Thickness)].ToString() : txtTail_Leader_Thickness.Text = "";
            //尾部切廢長度
            txtScraped_Length_Exit.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Scraped_Length_Exit)].ToString().Equals(null) ?
                txtScraped_Length_Exit.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Scraped_Length_Exit)].ToString() : txtScraped_Length_Exit.Text = "";
            //尾部導帶鋼種
            txtTail_Leader_St_No.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Tail_Leader_St_No)].ToString().Equals(null) ?
                txtTail_Leader_St_No.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Tail_Leader_St_No)].ToString() : txtTail_Leader_St_No.Text = "";
            //頭部未軋製區域
            txtHEAD_OFF_GAUGE.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Head_Off_Gauge)].ToString().Equals(null) ?
                txtHEAD_OFF_GAUGE.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Head_Off_Gauge)].ToString() : txtHEAD_OFF_GAUGE.Text = "";
            //尾部未軋製區域
            txtTail_off_gauge.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Tail_Off_Gauge)].ToString().Equals(null) ?
                txtTail_off_gauge.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Tail_Off_Gauge)].ToString() : txtTail_off_gauge.Text = "";
            //內表面精度代碼
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(cbo_SURFACE_ACCU_CODE_IN, dtPDO.Rows[0][nameof(L2L3_PDO.Surface_Accu_Code_In)].ToString());
            //外表面精度代碼
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(cbo_SURFACE_ACCU_CODE_OUT, dtPDO.Rows[0][nameof(L2L3_PDO.Surface_Accu_Code_Out)].ToString());
            //未焊引帶代碼
            txtNo_Leader_code.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.No_Leader_Code)].ToString().Equals(null) ?
                txtNo_Leader_code.Text = dtPDO.Rows[0][nameof(L2L3_PDO.No_Leader_Code)].ToString() : txtNo_Leader_code.Text = "";
            //表面精度代碼
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(cbo_SURFACE_ACCU_CODE, dtPDO.Rows[0][nameof(L2L3_PDO.Surface_Accuracy_Code)].ToString());
            //工序代碼
            txtProcessCode.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Process_Code)].ToString().Equals(null) ?
                txtProcessCode.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Process_Code)].ToString() : txtProcessCode.Text = "";
            //封鎖責任者
            txtInspector.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Inspector)].ToString().Equals(null) ?
                txtInspector.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Inspector)].ToString() : txtInspector.Text = "";
            //封鎖標記
            ComboBoxIndexHandler.Instance.ComboBoxIndex_Origin(cbo_Hold_flag, dtPDO.Rows[0][nameof(L2L3_PDO.Hold_Flag)].ToString());//封鎖標記
            //封鎖原因代碼
            txtHold_cause_code.Text = !dtPDO.Rows[0][nameof(L2L3_PDO.Hold_Cause_Code)].ToString().Equals(null) ?
                txtHold_cause_code.Text = dtPDO.Rows[0][nameof(L2L3_PDO.Hold_Cause_Code)].ToString() : txtHold_cause_code.Text = "";
            #endregion

            strSql = SqlFactory.frm_PDOConfirm_CoilPDODefect_DB_PDO(Coil_ID);
            try
            {
                dtDefect = DataAccess.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_CPL);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.Log(System_ID.Client, "PDO確認", Event_Type.Debug, "使用者:" + PublicForms.Main.lblLoginUser.Text + "出口卷缺陷查询", $"搜尋PDO缺陷語法:{strSql}有錯誤:{ex.ToString()}");
            }
            #region - 填入缺陷資料 -
            txtCode_D1.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D01_Code)].ToString().Equals(null) ?
                txtCode_D1.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D01_Code)].ToString() : txtCode_D1.Text = "";
            txtCode_D2.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D02_Code)].ToString().Equals(null) ?
                txtCode_D2.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D02_Code)].ToString() : txtCode_D2.Text = "";
            txtCode_D3.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D03_Code)].ToString().Equals(null) ?
                txtCode_D3.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D03_Code)].ToString() : txtCode_D3.Text = "";
            txtCode_D4.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D04_Code)].ToString().Equals(null) ?
                txtCode_D4.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D04_Code)].ToString() : txtCode_D4.Text = "";
            txtCode_D5.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D05_Code)].ToString().Equals(null) ?
                txtCode_D5.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D05_Code)].ToString() : txtCode_D5.Text = "";
            txtCode_D6.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D06_Code)].ToString().Equals(null) ?
                txtCode_D6.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D06_Code)].ToString() : txtCode_D6.Text = "";
            txtCode_D7.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D07_Code)].ToString().Equals(null) ?
                txtCode_D7.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D07_Code)].ToString() : txtCode_D7.Text = "";
            txtCode_D8.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D08_Code)].ToString().Equals(null) ?
                txtCode_D8.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D08_Code)].ToString() : txtCode_D8.Text = "";
            txtCode_D9.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D09_Code)].ToString().Equals(null) ?
                txtCode_D9.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D09_Code)].ToString() : txtCode_D9.Text = "";
            txtCode_D10.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D10_Code)].ToString().Equals(null) ?
                txtCode_D10.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D10_Code)].ToString() : txtCode_D10.Text = "";

            txtOrigin_D1.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D01_Origin)].ToString().Equals(null) ?
                txtOrigin_D1.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D01_Origin)].ToString() : txtOrigin_D1.Text = "";
            txtOrigin_D2.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D02_Origin)].ToString().Equals(null) ?
                txtOrigin_D2.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D02_Origin)].ToString() : txtOrigin_D2.Text = "";
            txtOrigin_D3.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D03_Origin)].ToString().Equals(null) ?
                txtOrigin_D3.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D03_Origin)].ToString() : txtOrigin_D3.Text = "";
            txtOrigin_D4.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D04_Origin)].ToString().Equals(null) ?
                txtOrigin_D4.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D04_Origin)].ToString() : txtOrigin_D4.Text = "";
            txtOrigin_D5.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D05_Origin)].ToString().Equals(null) ?
                txtOrigin_D5.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D05_Origin)].ToString() : txtOrigin_D5.Text = "";
            txtOrigin_D6.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D06_Origin)].ToString().Equals(null) ?
                txtOrigin_D6.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D06_Origin)].ToString() : txtOrigin_D6.Text = "";
            txtOrigin_D7.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D07_Origin)].ToString().Equals(null) ?
                txtOrigin_D7.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D07_Origin)].ToString() : txtOrigin_D7.Text = "";
            txtOrigin_D8.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D08_Origin)].ToString().Equals(null) ?
                txtOrigin_D8.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D08_Origin)].ToString() : txtOrigin_D8.Text = "";
            txtOrigin_D9.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D09_Origin)].ToString().Equals(null) ?
                txtOrigin_D9.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D09_Origin)].ToString() : txtOrigin_D9.Text = "";
            txtOrigin_D10.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D10_Origin)].ToString().Equals(null) ?
                txtOrigin_D10.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D10_Origin)].ToString() : txtOrigin_D10.Text = "";

            txtSid_D1.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D01_Sid)].ToString().Equals(null) ?
                txtSid_D1.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D01_Sid)].ToString() : txtSid_D1.Text = "";
            txtSid_D2.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D02_Sid)].ToString().Equals(null) ?
                txtSid_D2.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D02_Sid)].ToString() : txtSid_D2.Text = "";
            txtSid_D3.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D03_Sid)].ToString().Equals(null) ?
                txtSid_D3.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D03_Sid)].ToString() : txtSid_D3.Text = "";
            txtSid_D4.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D04_Sid)].ToString().Equals(null) ?
                txtSid_D4.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D04_Sid)].ToString() : txtSid_D4.Text = "";
            txtSid_D5.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D05_Sid)].ToString().Equals(null) ?
                txtSid_D5.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D05_Sid)].ToString() : txtSid_D5.Text = "";
            txtSid_D6.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D06_Sid)].ToString().Equals(null) ?
                txtSid_D6.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D06_Sid)].ToString() : txtSid_D6.Text = "";
            txtSid_D7.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D07_Sid)].ToString().Equals(null) ?
                txtSid_D7.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D07_Sid)].ToString() : txtSid_D7.Text = "";
            txtSid_D8.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D08_Sid)].ToString().Equals(null) ?
                txtSid_D8.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D08_Sid)].ToString() : txtSid_D8.Text = "";
            txtSid_D9.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D09_Sid)].ToString().Equals(null) ?
                txtSid_D9.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D09_Sid)].ToString() : txtSid_D9.Text = "";
            txtSid_D10.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D10_Sid)].ToString().Equals(null) ?
                txtSid_D10.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D10_Sid)].ToString() : txtSid_D10.Text = "";

            txtPos_W_D1.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D01_Pos_W)].ToString().Equals(null) ?
                txtPos_W_D1.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D01_Pos_W)].ToString() : txtPos_W_D1.Text = "";
            txtPos_W_D2.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D02_Pos_W)].ToString().Equals(null) ?
                txtPos_W_D2.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D02_Pos_W)].ToString() : txtPos_W_D2.Text = "";
            txtPos_W_D3.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D03_Pos_W)].ToString().Equals(null) ?
                txtPos_W_D3.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D03_Pos_W)].ToString() : txtPos_W_D3.Text = "";
            txtPos_W_D4.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D04_Pos_W)].ToString().Equals(null) ?
                txtPos_W_D4.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D04_Pos_W)].ToString() : txtPos_W_D4.Text = "";
            txtPos_W_D5.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D05_Pos_W)].ToString().Equals(null) ?
                txtPos_W_D5.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D05_Pos_W)].ToString() : txtPos_W_D5.Text = "";
            txtPos_W_D6.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D06_Pos_W)].ToString().Equals(null) ?
                txtPos_W_D6.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D06_Pos_W)].ToString() : txtPos_W_D6.Text = "";
            txtPos_W_D7.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D07_Pos_W)].ToString().Equals(null) ?
                txtPos_W_D7.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D07_Pos_W)].ToString() : txtPos_W_D7.Text = "";
            txtPos_W_D8.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D08_Pos_W)].ToString().Equals(null) ?
                txtPos_W_D8.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D08_Pos_W)].ToString() : txtPos_W_D8.Text = "";
            txtPos_W_D9.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D09_Pos_W)].ToString().Equals(null) ?
                txtPos_W_D9.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D09_Pos_W)].ToString() : txtPos_W_D9.Text = "";
            txtPos_W_D10.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D10_Pos_W)].ToString().Equals(null) ?
                txtPos_W_D10.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D10_Pos_W)].ToString() : txtPos_W_D10.Text = "";

            txtPos_L_Start_D1.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D01_Pos_L_Start)].ToString().Equals(null) ?
                txtPos_L_Start_D1.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D01_Pos_L_Start)].ToString() : txtPos_L_Start_D1.Text = "";
            txtPos_L_Start_D2.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D02_Pos_L_Start)].ToString().Equals(null) ?
                txtPos_L_Start_D2.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D02_Pos_L_Start)].ToString() : txtPos_L_Start_D2.Text = "";
            txtPos_L_Start_D3.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D03_Pos_L_Start)].ToString().Equals(null) ?
                txtPos_L_Start_D3.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D03_Pos_L_Start)].ToString() : txtPos_L_Start_D3.Text = "";
            txtPos_L_Start_D4.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D04_Pos_L_Start)].ToString().Equals(null) ?
                txtPos_L_Start_D4.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D04_Pos_L_Start)].ToString() : txtPos_L_Start_D4.Text = "";
            txtPos_L_Start_D5.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D05_Pos_L_Start)].ToString().Equals(null) ?
                txtPos_L_Start_D5.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D05_Pos_L_Start)].ToString() : txtPos_L_Start_D5.Text = "";
            txtPos_L_Start_D6.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D06_Pos_L_Start)].ToString().Equals(null) ?
                txtPos_L_Start_D6.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D06_Pos_L_Start)].ToString() : txtPos_L_Start_D6.Text = "";
            txtPos_L_Start_D7.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D07_Pos_L_Start)].ToString().Equals(null) ?
                txtPos_L_Start_D7.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D07_Pos_L_Start)].ToString() : txtPos_L_Start_D7.Text = "";
            txtPos_L_Start_D8.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D08_Pos_L_Start)].ToString().Equals(null) ?
                txtPos_L_Start_D8.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D08_Pos_L_Start)].ToString() : txtPos_L_Start_D8.Text = "";
            txtPos_L_Start_D9.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D09_Pos_L_Start)].ToString().Equals(null) ?
                txtPos_L_Start_D9.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D09_Pos_L_Start)].ToString() : txtPos_L_Start_D9.Text = "";
            txtPos_L_Start_D10.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D10_Pos_L_Start)].ToString().Equals(null) ?
                txtPos_L_Start_D10.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D10_Pos_L_Start)].ToString() : txtPos_L_Start_D10.Text = "";

            txtPos_L_End_D1.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D01_Pos_L_End)].ToString().Equals(null) ?
                txtPos_L_End_D1.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D01_Pos_L_End)].ToString() : txtPos_L_End_D1.Text = "";
            txtPos_L_End_D2 .Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D02_Pos_L_End)].ToString().Equals(null) ?
                txtPos_L_End_D2.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D02_Pos_L_End)].ToString() : txtPos_L_End_D2.Text = "";
            txtPos_L_End_D3 .Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D03_Pos_L_End)].ToString().Equals(null) ?
                txtPos_L_End_D3.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D03_Pos_L_End)].ToString() : txtPos_L_End_D3.Text = "";
            txtPos_L_End_D4 .Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D04_Pos_L_End)].ToString().Equals(null) ?
                txtPos_L_End_D4.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D04_Pos_L_End)].ToString() : txtPos_L_End_D4.Text = "";
            txtPos_L_End_D5 .Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D05_Pos_L_End)].ToString().Equals(null) ?
                txtPos_L_End_D5.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D05_Pos_L_End)].ToString() : txtPos_L_End_D5.Text = "";
            txtPos_L_End_D6 .Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D06_Pos_L_End)].ToString().Equals(null) ?
                txtPos_L_End_D6.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D06_Pos_L_End)].ToString() : txtPos_L_End_D6.Text = "";
            txtPos_L_End_D7 .Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D07_Pos_L_End)].ToString().Equals(null) ?
                txtPos_L_End_D7.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D07_Pos_L_End)].ToString() : txtPos_L_End_D7.Text = "";
            txtPos_L_End_D8 .Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D08_Pos_L_End)].ToString().Equals(null) ?
                txtPos_L_End_D8.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D08_Pos_L_End)].ToString() : txtPos_L_End_D8.Text = "";
            txtPos_L_End_D9 .Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D09_Pos_L_End)].ToString().Equals(null) ?
                txtPos_L_End_D9.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D09_Pos_L_End)].ToString() : txtPos_L_End_D9.Text = "";
            txtPos_L_End_D10.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D10_Pos_L_End)].ToString().Equals(null) ?
                txtPos_L_End_D10.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D10_Pos_L_End)].ToString() : txtPos_L_End_D10.Text = "";

            txtLevel_D1.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D01_Level)].ToString().Equals(null) ?
                txtLevel_D1.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D01_Level)].ToString() : txtLevel_D1.Text = "";
            txtLevel_D2.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D02_Level)].ToString().Equals(null) ?
                txtLevel_D2.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D02_Level)].ToString() : txtLevel_D2.Text = "";
            txtLevel_D3.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D03_Level)].ToString().Equals(null) ?
                txtLevel_D3.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D03_Level)].ToString() : txtLevel_D3.Text = "";
            txtLevel_D4.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D04_Level)].ToString().Equals(null) ?
                txtLevel_D4.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D04_Level)].ToString() : txtLevel_D4.Text = "";
            txtLevel_D5.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D05_Level)].ToString().Equals(null) ?
                txtLevel_D5.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D05_Level)].ToString() : txtLevel_D5.Text = "";
            txtLevel_D6.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D06_Level)].ToString().Equals(null) ?
                txtLevel_D6.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D06_Level)].ToString() : txtLevel_D6.Text = "";
            txtLevel_D7.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D07_Level)].ToString().Equals(null) ?
                txtLevel_D7.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D07_Level)].ToString() : txtLevel_D7.Text = "";
            txtLevel_D8.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D08_Level)].ToString().Equals(null) ?
                txtLevel_D8.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D08_Level)].ToString() : txtLevel_D8.Text = "";
            txtLevel_D9.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D09_Level)].ToString().Equals(null) ?
                txtLevel_D9.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D09_Level)].ToString() : txtLevel_D9.Text = "";
            txtLevel_D10.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D10_Level)].ToString().Equals(null) ?
                txtLevel_D10.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D10_Level)].ToString() : txtLevel_D10.Text = "";

            txtPercent_D1.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D01_Percent)].ToString().Equals(null) ?
                txtPercent_D1.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D01_Percent)].ToString() : txtPercent_D1.Text = "";
            txtPercent_D2.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D02_Percent)].ToString().Equals(null) ?
                txtPercent_D2.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D02_Percent)].ToString() : txtPercent_D2.Text = "";
            txtPercent_D3.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D03_Percent)].ToString().Equals(null) ?
                txtPercent_D3.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D03_Percent)].ToString() : txtPercent_D3.Text = "";
            txtPercent_D4.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D04_Percent)].ToString().Equals(null) ?
                txtPercent_D4.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D04_Percent)].ToString() : txtPercent_D4.Text = "";
            txtPercent_D5.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D05_Percent)].ToString().Equals(null) ?
                txtPercent_D5.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D05_Percent)].ToString() : txtPercent_D5.Text = "";
            txtPercent_D6.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D06_Percent)].ToString().Equals(null) ?
                txtPercent_D6.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D06_Percent)].ToString() : txtPercent_D6.Text = "";
            txtPercent_D7.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D07_Percent)].ToString().Equals(null) ?
                txtPercent_D7.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D07_Percent)].ToString() : txtPercent_D7.Text = "";
            txtPercent_D8.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D08_Percent)].ToString().Equals(null) ?
                txtPercent_D8.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D08_Percent)].ToString() : txtPercent_D8.Text = "";
            txtPercent_D9.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D09_Percent)].ToString().Equals(null) ?
                txtPercent_D9.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D09_Percent)].ToString() : txtPercent_D9.Text = "";
            txtPercent_D10.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D10_Percent)].ToString().Equals(null) ?
                txtPercent_D10.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D10_Percent)].ToString() : txtPercent_D10.Text = "";

            txtQGRADE_D1.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D01_QGRADE)].ToString().Equals(null) ?
                txtQGRADE_D1.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D01_QGRADE)].ToString() : txtQGRADE_D1.Text = "";
            txtQGRADE_D2.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D02_QGRADE)].ToString().Equals(null) ?
                txtQGRADE_D2.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D02_QGRADE)].ToString() : txtQGRADE_D2.Text = "";
            txtQGRADE_D3.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D03_QGRADE)].ToString().Equals(null) ?
                txtQGRADE_D3.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D03_QGRADE)].ToString() : txtQGRADE_D3.Text = "";
            txtQGRADE_D4.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D04_QGRADE)].ToString().Equals(null) ?
                txtQGRADE_D4.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D04_QGRADE)].ToString() : txtQGRADE_D4.Text = "";
            txtQGRADE_D5.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D05_QGRADE)].ToString().Equals(null) ?
                txtQGRADE_D5.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D05_QGRADE)].ToString() : txtQGRADE_D5.Text = "";
            txtQGRADE_D6.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D06_QGRADE)].ToString().Equals(null) ?
                txtQGRADE_D6.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D06_QGRADE)].ToString() : txtQGRADE_D6.Text = "";
            txtQGRADE_D7.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D07_QGRADE)].ToString().Equals(null) ?
                txtQGRADE_D7.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D07_QGRADE)].ToString() : txtQGRADE_D7.Text = "";
            txtQGRADE_D8.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D08_QGRADE)].ToString().Equals(null) ?
                txtQGRADE_D8.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D08_QGRADE)].ToString() : txtQGRADE_D8.Text = "";
            txtQGRADE_D9.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D09_QGRADE)].ToString().Equals(null) ?
                txtQGRADE_D9.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D09_QGRADE)].ToString() : txtQGRADE_D9.Text = "";
            txtQGRADE_D10.Text = !dtDefect.Rows[0][nameof(L2L3_PDO.D10_QGRADE)].ToString().Equals(null) ?
                txtQGRADE_D10.Text = dtDefect.Rows[0][nameof(L2L3_PDO.D10_QGRADE)].ToString() : txtQGRADE_D10.Text = "";

            #endregion

        }
        //private void SelectDT(DataTable dt)
        //{
        //    try
        //    {
        //        dt = DataAccess.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_CPL);
        //    }
        //    catch (Exception ex)
        //    {
        //        EventLogHandler.Instance.EventPush_Message("", $"警告! 語法:{strSql}有錯誤:{ex.ToString()}");
        //    }
        //}
        #endregion
       
        #region Button
        /// <summary>
        /// 上傳PDO -> MMS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            //檢查是否上傳過MMS
            DataTable dtUploadFlag = new DataTable();
            strSql = SqlFactory.Frm_3_2_UpdatePDO_Check_DB_PDO(txtOut_mat_No.Text);
            try
            {
                dtUploadFlag = DataAccess.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_CPL);
                EventLogHandler.Instance.Log(System_ID.Client, "3-2", Event_Type.Debug, "使用者:" + PublicForms.Main.lblLoginUser.Text + "PDO上传记录查询:" + txtOut_mat_No.Text, "PDO上传记录查询:" + txtOut_mat_No.Text + " 成功");
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.Log(System_ID.Client, "3-2", Event_Type.Debug, "使用者:" + PublicForms.Main.lblLoginUser.Text + "PDO上传记录查询:" + txtOut_mat_No.Text, "PDO上传记录查询:" + txtOut_mat_No.Text + " 错误:" + ex.ToString());
            }
            if (dtUploadFlag.Rows.Count.Equals(0) || !dtUploadFlag.Rows[0][nameof(L2L3_PDO.PDO_Uploaded_Flag)].ToString().Equals("1"))
            {
                DialogResult dialogR = MessageBox.Show("PDO只能上传一次，请确定是否要传送?", "提示", MessageBoxButtons.OKCancel);
                if (dialogR == DialogResult.OK)
                {
                    SCCommMsg.CS06_SendMMSPDO _SendMMSPDO = new SCCommMsg.CS06_SendMMSPDO
                    {
                        Source = "CPL1_HMI",
                        ID = "SendMMSPDO",
                        Coil_ID = txtOut_mat_No.Text
                    };
                    PublicComm.client.Tell(_SendMMSPDO);
                    EventLogHandler.Instance.Log(System_ID.Client, "2-1", Event_Type.Info, "使用者:" + PublicForms.Main.lblLoginUser.Text + "PDO资料确认上传 钢卷编号:" + txtOut_mat_No.Text, "PDO资料确认上传 钢卷编号:" + txtOut_mat_No.Text);
                    Close();
                    EventLogHandler.Instance.EventPush_Message("", $"提示! {txtOut_mat_No.Text} PDO上傳至MMS!");
                }
            }
            else
            {
                EventLogHandler.Instance.EventPush_Message("", $"提示! {txtOut_mat_No.Text} 已上傳過MMS!");
            }

        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            EventLogHandler.Instance.Log(System_ID.Client, "2-1",Event_Type.Info, "使用者:" + PublicForms.Main.lblLoginUser.Text + "PDO资料确认取消", "PDO资料确认取消");
            Close();
        }
        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            strSql = SqlFactory.frm_PDOConfirm_UpdatePDO_DB_PDO();
            DataAccess.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_CPL);
            EventLogHandler.Instance.EventPush_Message("", "提示! PDO儲存成功!");
            EventLogHandler.Instance.Log(System_ID.Client, "2-1",Event_Type.Info, "使用者:" + PublicForms.Main.lblLoginUser.Text + "PDO资料储存", "PDO资料储存 钢卷编号:" + txtOut_mat_No);
            btn_Confirm.Visible = true;
        }
        #endregion

        private void tabPDO_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(tabPDO, e);
        }

        private void btn_PDO_Select_Click(object sender, EventArgs e)
        {
            PDOselect_Table(cbo_Out_mat_NO.SelectedValue.ToString());
            EventLogHandler.Instance.Log(System_ID.Client, "2-1",Event_Type.Info, "使用者:" + PublicForms.Main.lblLoginUser.Text + "PDO资料查询 钢卷编号:"+ cbo_Out_mat_NO.Text, "PDO资料查询 钢卷编号:" + cbo_Out_mat_NO.Text);
        }

        private void btn_Sleeve_Type_Exit_Click(object sender, EventArgs e)
        {
            lbComboName.Text = "出口套筒类型";
            panel_Spare.Visible = true;
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsSpare(Cbo_Type.Sleeve_Type, txtSpare);
        }

        private void btn_Paper_Type_Exit_Click(object sender, EventArgs e)
        {
            lbComboName.Text = "出口垫纸类型";
            panel_Spare.Visible = true;
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsSpare(Cbo_Type.Paper_Type, txtSpare);
        }

        private void btn_OUT_PAPER_REQ_CODE_Click(object sender, EventArgs e)
        {
            lbComboName.Text = "出口垫纸方式";
            panel_Spare.Visible = true;
            ComboBoxIndexHandler.Instance.SelectComboBoxItemsSpare(Cbo_Type.PAPER_REQ_CODE, txtSpare);
        }
    }
}
