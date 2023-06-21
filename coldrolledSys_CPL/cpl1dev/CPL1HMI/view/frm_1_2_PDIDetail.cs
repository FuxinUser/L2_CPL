using Akka.Actor;
using AkkaSysBase;
using DataModel.HMIServerCom.Msg;
using DBService.Repository.DefectData;
using DBService.Repository.Leader;
using DBService.Repository.PDI;
using LabelPrint.Actor;
using static DataModel.HMIServerCom.Msg.SCCommMsg;
using static LabelPrint.Model.ZebraModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Text;

namespace CPL1HMI
{
    public partial class frm_1_2_PDIDetail : Form
    {
        #region 變數
        DataTable dtCbo;
        private bool bolDefectIsNull;
        private bool bolPdiIsNull;
        private bool bolLeader = false;

        DataTable dtSelectOne;
        DataTable dtSelectOne_Def;
        DataTable dtBeforeEdit;
        DataTable dtBeforeEdit_Def;

        string strEditStatus = "Read";
        bool bolEditStatus = false;
        //語系
        private LanguageHandler LanguageHand;
        #endregion

        public frm_1_2_PDIDetail()
        {
            InitializeComponent();
        }
        private void Frm_1_2_PDIDetail_Load(object sender, EventArgs e)
        {
            // LanguageHandler.Instance.Fun_CurrentLanguage(this.Name);

            if (PublicForms.PDIDetail == null) PublicForms.PDIDetail = this;
            
            Control[] Frm_1_2_Control = new Control[] {
                Btn_New,
                Btn_Delete,
                Btn_PdiDetEdit
            };

            ////一开始载入先都不启用
            ////新增
            //Fun_SetBottonEnabled(btn_New, false);
            ////修改
            //Fun_SetBottonEnabled(btn_PdiDetEdit, false);
            ////刪除
            //Fun_SetBottonEnabled(Btn_Delete, false);


            //// UserSetupHandler.Instance.Authority_Class : 1 = Administator ; 2 = Manager ; 3 : Operator 
            //if (UserSetupHandler.Instance.Authority_Class != "1"){}
            // 20211220 不管甚麼權限,都不開放編輯
            Btn_New.Visible = false;
            Btn_Delete.Visible = false;
            Btn_PdiDetEdit.Visible = false;
         
            //权限判断
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_1_2_Control, UserSetupHandler.Instance.Frm_1_2);

            //权限判断后,启用了元件但没改背景色,所以重新以启用状态变更背景色
            //新增
            Fun_SetBottonEnabled(Btn_New, Btn_New.Enabled);
            //修改
            Fun_SetBottonEnabled(Btn_PdiDetEdit, Btn_PdiDetEdit.Enabled);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, Btn_Delete.Enabled);

            if (!string.IsNullOrEmpty(Txt_EntryCoil.Text.Trim()))
            {
                //新增
                Fun_SetBottonEnabled(Btn_New, true);
                //修改
                Fun_SetBottonEnabled(Btn_PdiDetEdit, true);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, true);
            }
            else
            {
                //新增
                Fun_SetBottonEnabled(Btn_New, true);
                //修改
                Fun_SetBottonEnabled(Btn_PdiDetEdit, false);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, false);
            }

            Fun_EntryCoilList();
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDataPage,true);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDefectPage, true);

            //詳細資料ComboBox欄位
            Fun_ComboBox();

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        #region 查詢基本資料

        public void Fun_SelectCoilPDI(string Coil_ID, string strPlan_No = "")
        {
            string strSql = Frm_1_2_SqlFactory.SQL_Select_PDI_Detail(Coil_ID,  strPlan_No );
            DataTable dtGetPDI = DataAccess.Fun_SelectDate(strSql, "PDI详细资料");

            if(dtGetPDI != null && dtGetPDI.Rows.Count > 1)
            {
                DataTable dtSelectBack = new DataTable();

                Frm_SelectDataOpen frm_SelectOpen = new Frm_SelectDataOpen
                {
                    dtSelectData = dtGetPDI.Copy()
                   ,
                    strDataType = "PDI"
                };
                frm_SelectOpen.ShowDialog();
                frm_SelectOpen.Dispose();

                if (frm_SelectOpen.DialogResult == DialogResult.OK)
                {
                    dtSelectBack = frm_SelectOpen.dtSelectData.Copy();

                    dtGetPDI = dtSelectBack.Copy();
                }
            }


            if (!dtGetPDI.IsNull())
            {
                //缺陷資料
                Fun_SelectedDefectData(Coil_ID, dtGetPDI.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Plan_No)].ToString());

                //詳細資料欄位填入
                Fun_DisplayCoilPDI(dtGetPDI);

                //導帶資料
                Fun_DisplayCoilLeader(Coil_ID);

                //查询到资料 启用 //入口卷號
                
                if (!string.IsNullOrEmpty( Txt_EntryCoil.Text.Trim()))
                {
                    //新增
                    Fun_SetBottonEnabled(Btn_New, true);
                    //修改
                    Fun_SetBottonEnabled(Btn_PdiDetEdit, true);
                    //刪除
                    Fun_SetBottonEnabled(Btn_Delete, true);
                }
                else
                {
                    //新增
                    Fun_SetBottonEnabled(Btn_New, true);
                    //修改
                    Fun_SetBottonEnabled(Btn_PdiDetEdit, false);
                    //刪除
                    Fun_SetBottonEnabled(Btn_Delete, false);
                }
                

                EventLogHandler.Instance.EventPush_Message("查询完成");
            }
            else
            {
                
                DialogHandler.Instance.Fun_DialogShowOk("查询无资料", $"[{Coil_ID}]PDI查询", 0);

                Fun_ClearText(Tab_PDIDefectPage);
                Lbl_EntryCoil_Defect.Text = "";
                Fun_ClearText(Tab_PDIDataPage);

                Fun_ClearGroupBoxText(Grb_LeaderHead);

                Fun_ClearGroupBoxText(Grb_LeaderTail);

                if (string.IsNullOrEmpty(Txt_EntryCoil.Text.Trim()))
                {
                    //新增
                    Fun_SetBottonEnabled(Btn_New, true);
                    //修改
                    Fun_SetBottonEnabled(Btn_PdiDetEdit, false);
                    //刪除
                    Fun_SetBottonEnabled(Btn_Delete, false);
                }
            }
        }

        private void Fun_ClearText(TabPage Page)
        {
            foreach (Control control in Page.Controls.OfType<Control>())
            {
                if (control is TextBox)
                {
                    control.Text = string.Empty;
                }

                if (Page.Name.Equals(nameof(Tab_PDIDefectPage)))
                {
                    if (control is ComboBox)
                    {
                        control.Text = string.Empty;
                    }
                }
            }

        }

        private void Fun_ClearGroupBoxText(GroupBox group)
        {
            foreach (Control control in group.Controls)
            {
                 if (control is TextBox)
                {
                    control.Text = string.Empty;
                }

            }
        }
      
        #endregion

        #region 查詢缺陷資料

        private void Fun_SelectedDefectData(string Coil_ID, string strPlan_No = "")
        {
            Fun_ClearText(Tab_PDIDefectPage);
            Lbl_EntryCoil_Defect.Text = "";
          
            //钢卷缺陷
            string strSql = Frm_1_2_SqlFactory.SQL_Select_DefectData(Coil_ID,strPlan_No);
            DataTable dt_Defect = DataAccess.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_CPL);

            if (dt_Defect.IsNull()) return;

            #region 填入资料

            #region  - Code -
            Txt_Code_D01.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Code)].ToString();
            Txt_Code_D02.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Code)].ToString();
            Txt_Code_D03.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Code)].ToString();
            Txt_Code_D04.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Code)].ToString();
            Txt_Code_D05.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Code)].ToString();
            Txt_Code_D06.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Code)].ToString();
            Txt_Code_D07.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Code)].ToString();
            Txt_Code_D08.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Code)].ToString();
            Txt_Code_D09.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Code)].ToString();
            Txt_Code_D10.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Code)].ToString();
            #endregion

            #region - Origin -
            Txt_Origin_D01.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Origin)].ToString();
            Txt_Origin_D02.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Origin)].ToString();
            Txt_Origin_D03.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Origin)].ToString();
            Txt_Origin_D04.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Origin)].ToString();
            Txt_Origin_D05.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Origin)].ToString();
            Txt_Origin_D06.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Origin)].ToString();
            Txt_Origin_D07.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Origin)].ToString();
            Txt_Origin_D08.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Origin)].ToString();
            Txt_Origin_D09.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Origin)].ToString();
            Txt_Origin_D10.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Origin)].ToString();
            #endregion

            #region - Sid (ComboBox)-
            Cob_Sid_D01.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Sid)].ToString();
            Cob_Sid_D02.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Sid)].ToString();
            Cob_Sid_D03.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Sid)].ToString();
            Cob_Sid_D04.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Sid)].ToString();
            Cob_Sid_D05.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Sid)].ToString();
            Cob_Sid_D06.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Sid)].ToString();
            Cob_Sid_D07.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Sid)].ToString();
            Cob_Sid_D08.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Sid)].ToString();
            Cob_Sid_D09.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Sid)].ToString();
            Cob_Sid_D10.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Sid)].ToString();
            #endregion

            #region - Pos_W (ComboBox)-
            Cob_PosW_D01.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_W)].ToString();
            Cob_PosW_D02.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_W)].ToString();
            Cob_PosW_D03.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_W)].ToString();
            Cob_PosW_D04.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_W)].ToString();
            Cob_PosW_D05.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_W)].ToString();
            Cob_PosW_D06.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_W)].ToString();
            Cob_PosW_D07.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_W)].ToString();
            Cob_PosW_D08.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_W)].ToString();
            Cob_PosW_D09.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_W)].ToString();
            Cob_PosW_D10.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_W)].ToString();
            #endregion

            #region - Pos_L_Start - 
            Txt_Pos_L_Start_D01.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D02.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D03.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D04.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D05.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D06.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D07.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D08.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D09.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D10.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_Start)].ToString();
            #endregion

            #region - Pos_L_End -
            Txt_Pos_L_End_D01.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_End)].ToString();
            Txt_Pos_L_End_D02.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_End)].ToString();
            Txt_Pos_L_End_D03.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_End)].ToString();
            Txt_Pos_L_End_D04.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_End)].ToString();
            Txt_Pos_L_End_D05.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_End)].ToString();
            Txt_Pos_L_End_D06.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_End)].ToString();
            Txt_Pos_L_End_D07.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_End)].ToString();
            Txt_Pos_L_End_D08.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_End)].ToString();
            Txt_Pos_L_End_D09.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_End)].ToString();
            Txt_Pos_L_End_D10.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_End)].ToString();
            #endregion

            #region - Level (ComboBox)-
            Cob_Level_D01.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Level)].ToString();
            Cob_Level_D02.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Level)].ToString();
            Cob_Level_D03.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Level)].ToString();
            Cob_Level_D04.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Level)].ToString();
            Cob_Level_D05.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Level)].ToString();
            Cob_Level_D06.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Level)].ToString();
            Cob_Level_D07.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Level)].ToString();
            Cob_Level_D08.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Level)].ToString();
            Cob_Level_D09.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Level)].ToString();
            Cob_Level_D10.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Level)].ToString();
            #endregion

            #region - Percent -
            Txt_Percent_D01.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Percent)].ToString();
            Txt_Percent_D02.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Percent)].ToString();
            Txt_Percent_D03.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Percent)].ToString();
            Txt_Percent_D04.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Percent)].ToString();
            Txt_Percent_D05.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Percent)].ToString();
            Txt_Percent_D06.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Percent)].ToString();
            Txt_Percent_D07.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Percent)].ToString();
            Txt_Percent_D08.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Percent)].ToString();
            Txt_Percent_D09.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Percent)].ToString();
            Txt_Percent_D10.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Percent)].ToString();
            #endregion

            #region - QGrade - 
            Txt_QGRADE_D01.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_QGRADE)].ToString();
            Txt_QGRADE_D02.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_QGRADE)].ToString();
            Txt_QGRADE_D03.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_QGRADE)].ToString();
            Txt_QGRADE_D04.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_QGRADE)].ToString();
            Txt_QGRADE_D05.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_QGRADE)].ToString();
            Txt_QGRADE_D06.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_QGRADE)].ToString();
            Txt_QGRADE_D07.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_QGRADE)].ToString();
            Txt_QGRADE_D08.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_QGRADE)].ToString();
            Txt_QGRADE_D09.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_QGRADE)].ToString();
            Txt_QGRADE_D10.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_QGRADE)].ToString();
            #endregion

            #endregion

        }

        #endregion

        #region 查詢ComboBox清單

        public void Fun_ComboBox()
        {
            //計畫種類
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Plan_Sort, Cob_Plan_Sort);
            //入口套筒類型
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Sleeve(Cob_Sleeve_Type_Entry);
            //入口墊紙方式
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.PAPER_REQ_CODE, Cob_PAPER_REQ_CODE);
            //入口墊紙類型
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Paper(Cob_In_Paper_Type_Entry);
            //反修類型
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Rework_Type, Cob_Rework_Type);
            //訂單表面精度
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Surface_Accuracy, Cob_Surface_Finishing_Code);
            //實際表面精度
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Surface_Accuracy, Cob_Surface_Accuracy_Code);
            //好面朝向
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Base_Surface, Cob_Base_Surface);
            //開卷方向
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Uncoiler_Direction, Cob_Uncoiler_Direction);
            //出口墊紙方式
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.PAPER_REQ_CODE, Cob_Out_Paper_Req_Code);
            //出口墊紙類型
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Paper(Cob_Paper_Type_Exit);
            //出口套筒類型
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Sleeve(Cob_Sleeve_Type_Exit);
            //導帶使用方式
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Leader_Usage, Cob_Leader_Usage);
            //取樣要求
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Samp, Cob_Samp);
            //取樣位置
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.SAMPLE_FRQN_CODE, Cob_SAMPLE_FRQN_CODE);
            //鋼捲來源
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Origin, Cob_CoilOrigin);
            //切邊要求
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Trim, Cob_Trim);
            //分卷標記
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Trim, Cob_Dividing_Flag);

            #region -缺陷資料ComboBox设定-

            #region - Level -
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D01);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D02);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D03);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D04);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D05);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D06);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D07);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D08);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D09);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D10);
            #endregion

            #region - Sid -
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D01);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D02);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D03);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D04);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D05);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D06);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D07);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D08);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D09);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D10);
            #endregion

            #region - PosW -
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D01);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D02);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D03);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D04);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D05);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D06);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D07);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D08);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D09);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D10);
            #endregion

            #endregion
        }

        /// <summary>
        /// 資料欄ComboBox值
        /// </summary>
        public void Fun_EntryCoilList()
        {

            string strSql = Frm_1_2_SqlFactory.SQL_Select_Entry_Coil_No_ComboBoxItems();
            dtCbo = DataAccess.Fun_SelectDate(strSql, "入口卷号清单");

            if (!dtCbo.IsNull())
            {
                Cob_EntryCoilID.DisplayMember = nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID);
                Cob_EntryCoilID.ValueMember = nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID);
                Cob_EntryCoilID.DataSource = dtCbo;
            }
            else
            {
                Cob_EntryCoilID.DataSource = null;
            }
        }
           
        private void Cbo_EntryCoilID_Click(object sender, EventArgs e)
        {
            Fun_EntryCoilList();
        }

        #endregion

        #region 基本資料填入

        /// <summary>
        /// 填入PDI基本資料
        /// </summary>
        /// <param name="dt"></param>
        private void Fun_DisplayCoilPDI(DataTable dt)
        {
            Fun_ClearText(Tab_PDIDataPage);
            Lbl_EntryCoil_Defect.Text = "";

            #region 填資料
            //計畫號
            Txt_PlanNo.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Plan_No)].ToString();

            //順序號
            Txt_SeqNo.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Mat_Seq_No)].ToString();

            //計畫種類
            Cob_Plan_Sort.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Plan_Sort)].ToString();

            //入口卷號
            Txt_EntryCoil.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].ToString();
            Lbl_EntryCoil_Defect.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].ToString();
            //入料厚度
            Txt_EntryThick.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Thick)].ToString();

            //入料寬度
            Txt_EntryWidth.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Width)].ToString();

            //入料重量
            Txt_EntryWeight.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Weight)].ToString();

            //入料長度
            Txt_EntryLen.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Length)].ToString();

            //入料內徑
            Txt_EntryInner.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Inner)].ToString();

            //入料外徑
            Txt_EntryDcos.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_Dcos)].ToString();

            //入口套筒類型
            Cob_Sleeve_Type_Entry.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Sleeve_Type_Code)].ToString();

            //套筒內徑
            Txt_Sleeve_Inner_Entry.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Sleeve_diamter)].ToString();

            //入口墊紙方式
            Cob_PAPER_REQ_CODE.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Paper_Req_Code)].ToString();

            //入口墊紙類型
            Cob_In_Paper_Type_Entry.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Paper_Code)].ToString();

            //入口頭部墊紙長度
            Txt_In_Paper_Head_Length.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Length)].ToString();

            //入口頭部墊紙寬度
            Txt_In_Paper_Head_Width.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Head_Paper_Width)].ToString();

            //入口尾部墊紙長度
            Txt_In_Paper_Tail_Length.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Length)].ToString();

            //入口尾部墊紙寬度
            Txt_In_Paper_Tail_Width.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Tail_Paper_Width)].ToString();

            //抗拉最大值
            Txt_STAND_MAX.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Max)].ToString();

            //抗拉最小值
            Txt_STAND_MIN.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Ts_Stand_Min)].ToString();

            //內部鋼種
            Txt_St_no.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.St_No)].ToString();

            //密度
            Txt_Density.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Density)].ToString();

            //返修類型
            Cob_Rework_Type.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.REPAIR_TYPE)].ToString();

            //訂單表面精度代碼
            Cob_Surface_Finishing_Code.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Surface_Finishing_Code)].ToString();

            //實際表面精度代碼
            Cob_Surface_Accuracy_Code.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Surface_Accuracy)].ToString();

            //好面朝向
            Cob_Base_Surface.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Base_Surface)].ToString();

            //開卷方向
            Cob_Uncoiler_Direction.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Uncoiler_Direction)].ToString();
            
            //出口鋼卷號
            Txt_OutCoil.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)].ToString();

            //出口墊紙方式
            Cob_Out_Paper_Req_Code.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Req_Code)].ToString();

            //出口墊紙種類
            Cob_Paper_Type_Exit.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Paper_Code)].ToString();
            
            //出口套筒內徑
            Txt_Sleeve_Inner_Exit.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Diamter)].ToString();

            //出口套筒類型
            Cob_Sleeve_Type_Exit.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Sleeve_Type_Code)].ToString();
            
            //出口綁帶方式
            Txt_Strap.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Strap_Num)].ToString();

            //導帶使用方式
            Cob_Leader_Usage.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)].ToString();

            //取樣要求
            Cob_Samp.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Sample_Flag)].ToString();

            //取樣位置
            Cob_SAMPLE_FRQN_CODE.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Sample_Frqn_Code)].ToString();
            
            //試批號
            Txt_LotNo.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Sample_Lot_No)].ToString();

            //鋼卷來源
            Cob_CoilOrigin.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Coil_Origin)].ToString();
            
            //上游
            Txt_Wholebacklog.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Wholebacklog_Code)].ToString();

            //下游
            Txt_NWholebacklog.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Next_Wholebacklog_Code)].ToString();

            //切邊需求
            Cob_Trim.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)].ToString();
            
            //目標寬度
            Txt_OutWidth.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)].ToString();

            //目標寬度最大值
            Txt_WdMax.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)].ToString();

            //目標寬度最小值
            Txt_WdMin.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)].ToString();

            //目標出口厚度
            Txt_Out_mat_thickness.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Thickness)].ToString();

            //出口鋼卷內徑
            Txt_OutInner.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Inner)].ToString();

            //合同號
            Txt_Order_no.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Order_No)].ToString();

            //目標訂單重最大值
            Txt_WtMax.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Max)].ToString();

            //目標訂單重最小值
            Txt_WtMin.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Order_Wt_Min)].ToString();

            //目標訂單重
            Txt_OrderWt.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Order_Wt)].ToString();

            //分卷標記
            Cob_Dividing_Flag.SelectedValue = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)].ToString();
            
            //分卷數
            Txt_Dividing_num.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Dividing_Num)].ToString();

            //合同訂貨重量1
            Txt_Order_wt_1.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Orderwt_1)].ToString();

            //合同訂貨重量2
            Txt_Order_wt_2.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Orderwt_2)].ToString();

            //合同訂貨重量3
            Txt_Order_wt_3.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Orderwt_3)].ToString();

            //合同訂貨重量4
            Txt_Order_wt_4.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Orderwt_4)].ToString();

            //合同訂貨重量5
            Txt_Order_wt_5.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Orderwt_5)].ToString();

            //合同訂貨重量6
            Txt_Order_wt_6.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Orderwt_6)].ToString();

            //合同號1
            Txt_Order_no_1.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Order_No_1)].ToString();

            //合同號2
            Txt_Order_no_2.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Order_No_2)].ToString();

            //合同號3
            Txt_Order_no_3.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Order_No_3)].ToString();

            //合同號4
            Txt_Order_no_4.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Order_No_4)].ToString();

            //合同號5
            Txt_Order_no_5.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Order_No_5)].ToString();

            //合同號6
            Txt_Order_no_6.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Order_No_6)].ToString();

            //頭部未壓製區域
            Txt_Head_Off_Gauge.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Head_Off_Gauge)].ToString();

            //尾部未壓製區域
            Txt_Tail_off_gauge.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Tail_Off_Gauge)].ToString();

            //牌号
            Txt_SG_SIGN.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Sg_Sign)].ToString();

            //Process Code
            Txt_ProcessCode.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Process_Code)].ToString();

            //客戶代碼
            Txt_CustomerNo.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.CustomerCode)].ToString();

            //客戶中文名稱
            Txt_CustomerName_C.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.CustomerName_C)].ToString();

            //客戶英文名稱
            Txt_CustomerName_E.Text = dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.CustomerName_E)].ToString();

            #endregion
        }


        private void Fun_DisplayCoilLeader(string Coil_ID)
        {
            Fun_ClearGroupBoxText(Grb_LeaderHead);
            Fun_ClearGroupBoxText(Grb_LeaderTail);

            string strSql = Frm_1_2_SqlFactory.SQL_Select_LeaderData(Coil_ID);
            DataTable dt_Leader = DataAccess.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_CPL);

            if (!dt_Leader.IsNull())
            {
                bolLeader = true;
            } 
            else
            {
                bolLeader = false;
                return;
            }

            #region 填入導帶資料
            //頭段導帶
            //鋼種
            Txt_LeaderHeadSt_no.Text = dt_Leader.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_St_No)].ToString();

            //長度
            Txt_LeaderHeadLen.Text = dt_Leader.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Length)].ToString();

            //寬度
            Txt_LeaderHeadWd.Text = dt_Leader.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Width)].ToString();

            //厚度
            Txt_LeaderHeadThickness.Text = dt_Leader.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Thickness)].ToString();

            //尾段導帶
            //鋼種
            Txt_LeaderTailSt_no.Text = dt_Leader.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_St_No)].ToString();

            //長度
            Txt_LeaderTailLen.Text = dt_Leader.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Length)].ToString();

            //寬度
            Txt_LeaderTailWd.Text = dt_Leader.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Width)].ToString();

            //厚度
            Txt_LeaderTailThickness.Text = dt_Leader.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Thickness)].ToString();
            #endregion

        }

        #endregion

        #region 查詢
        /// <summary>
        /// 查詢按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, System.EventArgs e)
        {
            if (strEditStatus != "Read")
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再查询", "查询", 0);
                return;
            }
            if (bolEditStatus == true)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再查询", "查询", 0);
                return;
            }
            Fun_SelectCoilPDI(Cob_EntryCoilID.Text);
        }
        #endregion

        #region 新增
        private void Btn_New_Click(object sender, EventArgs e)
        {
            //已是编辑状态 return
            if (bolEditStatus) { return; }
            //紀錄是否編輯,狀態
            strEditStatus = "New";
            bolEditStatus = true;

            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDataPage, false);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDefectPage, false);

            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);
            //修改
            Fun_SetBottonEnabled(Btn_PdiDetEdit, false);
            Btn_PdiDetCancel.Visible = true; //取消
            Btn_PdiDetSave.Visible = true; //儲存

            Txt_EntryCoil.Text = string.Empty;
            Txt_OutCoil.Text = string.Empty;
            Txt_PlanNo.Text = string.Empty;
            Txt_SeqNo.Text = string.Empty;

            //if (txtEntryCoil.Text.IsEmpty()) return;
            //else if (!txtEntryCoil.Text.IsEmpty())
            //{
               
            //}
        }
        #endregion

        #region 修改
        private void Btn_PdiDetEdit_Click(object sender, EventArgs e)
        {
            //已是编辑状态 return
            if (bolEditStatus) { return; }

            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDataPage, false);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDefectPage, false);

            Txt_EntryCoil.ReadOnly = true;
            Txt_PlanNo.ReadOnly = true;

            //新增
            Fun_SetBottonEnabled(Btn_New, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);


            Btn_PdiDetCancel.Visible = true; //取消
            Btn_PdiDetSave.Visible = true; //儲存
            
            //紀錄是否編輯,狀態
            strEditStatus = "Edit";
            bolEditStatus = true;
        }
        #endregion

        #region 刪除
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            string strMessage = $"是否删除钢卷编号[{Txt_EntryCoil.Text.Trim()}]PDI";

            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "删除PDI", Properties.Resources.dialogQuestion, 1);
            if (dialogR.Equals(DialogResult.OK) )
            {

                //新增
                Fun_SetBottonEnabled(Btn_New, false);
                //修改
                Fun_SetBottonEnabled(Btn_PdiDetEdit, false);


                //判斷是否已被刪除
                string strSql = Frm_1_2_SqlFactory.SQL_Select_CheckDeleteFlag(Txt_EntryCoil.Text);
                DataTable dtGetCoilIsDelete = DataAccess.Fun_SelectDate(strSql, "删除注记");

                //如果IsDelete = 1 表示已被刪除
                if (dtGetCoilIsDelete.IsNull())
                {
                    EventLogHandler.Instance.EventPush_Message($"钢卷号[{ Txt_EntryCoil.Text.Trim()}]已被删除，请重新确认");
                }
                else
                {
                    strSql = Frm_1_2_SqlFactory.SQL_Update_PDI_IsDelete(Txt_PlanNo.Text, Txt_SeqNo.Text, Txt_EntryCoil.Text);

                    if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "删除PDI"))
                    {
                        EventLogHandler.Instance.EventPush_Message($"钢卷号[{ Txt_EntryCoil.Text.Trim()}]删除失敗");

                        //新增
                        Fun_SetBottonEnabled(Btn_New, true);
                        //修改
                        Fun_SetBottonEnabled(Btn_PdiDetEdit, true);

                        return;
                    }

                    Fun_ReadOnlyClearData();

                    Fun_EntryCoilList();

                    Cob_EntryCoilID.Text = string.Empty;
                    Lbl_EntryCoil_Defect.Text = string.Empty;
                    //新增
                    Fun_SetBottonEnabled(Btn_New, true);
                    //修改
                    Fun_SetBottonEnabled(Btn_PdiDetEdit, false);
                    //刪除
                    Fun_SetBottonEnabled(Btn_Delete, false);
                }
            }

           
        }
        #endregion

        #region 儲存

        private bool Fun_CheckLeaderData(string strCoil_ID)
        {
            string strSql = Frm_1_2_SqlFactory.SQL_Select_LeaderData(strCoil_ID);
            DataTable dt_Leader = DataAccess.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_CPL);

            if (!dt_Leader.IsNull())
            {
                //已有資料
                return  true;
            }
            else
            {          
                //查無資料
                return false;
            }
        }
       
        //檢查必填欄位是否空白
        private bool Fun_IsColumnsEmpty()
        {
            if (string.IsNullOrEmpty(Txt_EntryCoil.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"入口卷号 请勿空白! ", "提示资讯", 0);
                Txt_EntryCoil.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(Txt_PlanNo.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"计划号 请勿空白! ", "提示资讯", 0);
                Txt_PlanNo.Focus();
                return false;
            }
          
            return true;
        }
        private void Btn_PdiDetSave_Click(object sender, EventArgs e)
        {
            //檢查必填欄位            
            if (!Fun_IsColumnsEmpty()) { return; }

            string strSql = string.Empty;

            string strSql_Defect = string.Empty;

            string strSql_Leader = string.Empty;

            #region Server: L25功能新增-HMI修改PDI時 通知Server存取資料至L25資料庫            
            string entryCoilID = Txt_EntryCoil.Text.Trim();
            string planNo = Txt_PlanNo.Text.Trim();
            PublicComm.Client.Tell(new CS20_InfoPDIModify(planNo, entryCoilID));
            #endregion
            //if (cbo_In_Paper_Type_Entry.Text.IsEmpty() || cbo_Paper_Type_Exit.Text.IsEmpty())
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("尚未选择入/出口垫纸种类", "储存入料钢卷详细资料", 2);
            //    PublicComm.ClientLog.Info($"尚未選擇入/出口墊紙種類");
            //    return;
            //}

            string strEntry_Coil_ID = Txt_EntryCoil.Text.Trim();//  
            string strPlan_No = Txt_PlanNo.Text.Trim();//

            string strCheckSql_PDI = Frm_1_2_SqlFactory.SQL_Select_PDIDetail(strEntry_Coil_ID,  strPlan_No);
            DataTable dt_Check_PDI = DataAccess.Fun_SelectDate(strCheckSql_PDI, "PDI资料");
            bolPdiIsNull = dt_Check_PDI.IsNull();
            if (Btn_New.Enabled)
            {
                if (bolPdiIsNull)
                {
                    //無此PDI 可新增
                }
                else
                {
                    //已有PDI 不可新增
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"新增PDI资料");
                    sb.AppendLine($"    计划号:{strPlan_No}");                 
                    sb.AppendLine($"    入口钢卷号:{strEntry_Coil_ID}");               
                    sb.AppendLine($"资料已重复，请重新确认！");

                    DialogHandler.Instance.Fun_DialogShowOk(sb.ToString(), "新增PDI", 3);
                    return;
                }
            }
            else if (Btn_PdiDetEdit.Enabled)
            {
                if (bolPdiIsNull)
                {
                    //無此PDI 無法更新資料
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"修改PDI资料");
                    sb.AppendLine($"    计划号:{strPlan_No}");                    
                    sb.AppendLine($"    入口钢卷号:{strEntry_Coil_ID}");                    
                    sb.AppendLine($"查无资料，请重新确认！");

                    DialogHandler.Instance.Fun_DialogShowOk(sb.ToString(), "修改PDI", 3);
                    return;
                }
                else
                {
                    //有此PDI 可修改
                }
            }
            else { }

            string strCheckSql = Frm_3_2_SqlFactory.SQL_Select_DefectData(strEntry_Coil_ID, strPlan_No);
            DataTable dt_Check_Defect = DataAccess.Fun_SelectDate(strCheckSql, "PDO缺陷资料");

            bolDefectIsNull = dt_Check_Defect.IsNull();


            //钢卷资料修改
            if (Btn_PdiDetEdit.Enabled)
            {
                //PDI资料
                strSql = Frm_1_2_SqlFactory.SQL_Update_CoilDetailData();

                if (Cob_Leader_Usage.SelectedValue.Equals("1"))
                {
                    //檢查是否已有導帶資料
                    bolLeader = Fun_CheckLeaderData(Txt_EntryCoil.Text.Trim());


                    //導帶資料 如果是Ture = 修改 False = 新增
                    strSql_Leader = bolLeader ? Frm_1_2_SqlFactory.SQL_Update_LeaderData() : Frm_1_2_SqlFactory.SQL_Insert_LeaderData();
                }

                //缺陷
                strSql_Defect = bolDefectIsNull ? Frm_1_2_SqlFactory.SQL_Insert_DefectData() : Frm_1_2_SqlFactory.SQL_Update_DefectData();
            }
            //钢卷PDI资料新增
            else if (Btn_New.Enabled)
            {
                
                //PDI资料
                strSql = Frm_1_2_SqlFactory.SQL_Insert_PDI_Detail();

                if (Cob_Leader_Usage.SelectedValue.Equals("1"))
                {
                    //導帶資料
                    strSql_Leader = Frm_1_2_SqlFactory.SQL_Insert_LeaderData();
                }

                //缺陷
                strSql_Defect = bolDefectIsNull? Frm_1_2_SqlFactory.SQL_Insert_DefectData() : Frm_1_2_SqlFactory.SQL_Update_DefectData();
            }

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "儲存PDI"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"钢卷号[{ Txt_EntryCoil.Text.Trim()}]PDI储存失敗", "储存入料钢卷详细资料", 3);

                //有錯誤就還原
                //PDI
                Fun_SelectCoilPDI(Cob_EntryCoilID.Text);

                return;
            }


            if (Cob_Leader_Usage.SelectedValue.Equals("1"))
            {

                if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql_Leader, "儲存PDI导带资料"))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"钢卷号[{ Txt_EntryCoil.Text.Trim()}]导带资料储存失敗", "储存入料钢卷详细资料", 3);

                    //有錯誤就還原
                    //PDI
                    Fun_SelectCoilPDI(Cob_EntryCoilID.Text);

                    return;
                }

            }

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql_Defect, "儲存缺陷"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"钢卷号[{ Txt_EntryCoil.Text.Trim()}]缺陷资料储存失敗", "储存入料钢卷详细资料", 3);

                //有錯誤就還原
                //缺陷資料
                Fun_SelectedDefectData(Cob_EntryCoilID.Text);

               // return;
            }

            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDataPage, true);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDefectPage, true);

            //新增
            Fun_SetBottonEnabled(Btn_New, true);

            //修改
            Fun_SetBottonEnabled(Btn_PdiDetEdit, true);

            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);

            //隱藏確認按鈕
            Btn_PdiDetSave.Visible = false;

            //隱藏取消按鈕
            Btn_PdiDetCancel.Visible = false;

            if (Btn_New.Enabled) Fun_EntryCoilList();

            strEditStatus = "Read";
            bolEditStatus = false;

            if (Pnl_Spare.Visible == true)
                Pnl_Spare.Visible = false;
        }

        #endregion

        #region 取消

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PdiDetCancel_Click(object sender, EventArgs e)
        {
            //不是编辑状态 return
            if (!bolEditStatus) { return; }

            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDataPage, true);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDefectPage, true);
            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //修改
            Fun_SetBottonEnabled(Btn_PdiDetEdit, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);
            Btn_PdiDetCancel.Visible = false; //取消
            Btn_PdiDetSave.Visible = false; //儲存

            if (!Txt_EntryCoil.Text.IsEmpty()) 
            { 
                Fun_SelectCoilPDI(Cob_EntryCoilID.Text);
            }

            if (!string.IsNullOrEmpty(Txt_EntryCoil.Text.Trim()))
            {
                //新增
                Fun_SetBottonEnabled(Btn_New, true);
                //修改
                Fun_SetBottonEnabled(Btn_PdiDetEdit, true);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, true);
            }
            else
            {
                //新增
                Fun_SetBottonEnabled(Btn_New, true);
                //修改
                Fun_SetBottonEnabled(Btn_PdiDetEdit, false);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, false);
            }

            //取消编辑后,恢复唯读状态
            strEditStatus = "Read";
            bolEditStatus = false;

            if (Pnl_Spare.Visible == true)
                Pnl_Spare.Visible = false;
        }

        #endregion

        //設定按鈕啟用狀態並改顏色
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //Color colorBack;
            btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

        #region 返回

        /// <summary>
        /// 返回1-1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Schedule_Click(object sender, EventArgs e)
        {
            frm_0_0_Main FatherForm = Parent.Parent as frm_0_0_Main;
            FatherForm.tsMenuItem_1_1.PerformClick();

            EventLogHandler.Instance.LogInfo("1-2", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()}返回1-1入料钢卷排程资讯", "返回1-1入料钢卷排程资讯");
            PublicComm.ClientLog.Info($"返回入料鋼卷排程資訊成功");
        }

        #endregion

        #region 清空欄位並唯讀

        /// <summary>
        /// 清空欄位並唯讀
        /// </summary>
        private void Fun_ReadOnlyClearData()
        {
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDataPage, true);
            ReadOnlyHandler.Instance.ReadOnly(Tab_PDIDefectPage, true);

            #region 清空詳細資料
            Txt_EntryCoil.Text = string.Empty;
            Txt_PlanNo.Text = string.Empty;
            Txt_SeqNo.Text = string.Empty;
            Cob_Plan_Sort.SelectedIndex = 0;
            
            Txt_EntryThick.Text = string.Empty;
            Txt_EntryWidth.Text = string.Empty;
            Txt_EntryLen.Text = string.Empty;
            Txt_EntryWeight.Text = string.Empty;
            Txt_EntryDcos.Text = string.Empty;
            Txt_EntryInner.Text = string.Empty;
           
            Cob_Sleeve_Type_Entry.SelectedIndex = 0;
            Txt_Sleeve_Inner_Entry.Text = string.Empty;

            Cob_PAPER_REQ_CODE.SelectedIndex = 0;
            Cob_In_Paper_Type_Entry.SelectedIndex = 0;
            Txt_In_Paper_Head_Length.Text = string.Empty;
            Txt_In_Paper_Head_Width.Text =string.Empty;
            Txt_In_Paper_Tail_Length.Text =string.Empty;
            Txt_In_Paper_Tail_Width.Text =string.Empty;

            Txt_STAND_MAX.Text =string.Empty;
            Txt_STAND_MIN.Text =string.Empty;
            Txt_Density.Text = string.Empty;
            Cob_Rework_Type.SelectedIndex = 0;

            Cob_Surface_Finishing_Code.SelectedIndex = 0;
            Cob_Surface_Accuracy_Code.SelectedIndex = 0;
            Cob_Base_Surface.SelectedIndex = 0;
            Cob_Uncoiler_Direction.SelectedIndex = 0;
                        
            Cob_Samp.SelectedIndex = 0;
            Cob_SAMPLE_FRQN_CODE.SelectedIndex = 0;
            Txt_LotNo.Text = string.Empty;
            Cob_CoilOrigin.SelectedIndex = 0;            
            Txt_Wholebacklog.Text =string.Empty;
            Txt_NWholebacklog.Text =string.Empty;
            Cob_Trim.SelectedIndex = 0;

            Txt_ProcessCode.Text = string.Empty;
            Txt_St_no.Text = string.Empty;
            Txt_SG_SIGN.Text = string.Empty;

            Txt_OutCoil.Text = string.Empty;
            Cob_Out_Paper_Req_Code.SelectedIndex = 0;
            Cob_Paper_Type_Exit.SelectedIndex = 0;
            Txt_Sleeve_Inner_Exit.Text = string.Empty;
            Cob_Sleeve_Type_Exit.SelectedIndex = 0;
            Txt_Strap.Text = string.Empty;

            Txt_Order_no.Text = string.Empty;
            Txt_OutWidth.Text = string.Empty;
            Txt_WdMax.Text = string.Empty;
            Txt_WdMin.Text = string.Empty;
            Txt_Out_mat_thickness.Text = string.Empty;
            Txt_OutInner.Text = string.Empty;
            Txt_OrderWt.Text = string.Empty;
            Txt_WtMax.Text = string.Empty;
            Txt_WtMin.Text = string.Empty;

            Txt_Head_Off_Gauge.Text = string.Empty;
            Txt_Tail_off_gauge.Text = string.Empty;
                       
            Cob_Dividing_Flag.SelectedIndex = 0;
            Txt_Dividing_num.Text =string.Empty;

            Txt_Order_wt_1.Text =string.Empty;
            Txt_Order_wt_2.Text =string.Empty;
            Txt_Order_wt_3.Text =string.Empty;
            Txt_Order_wt_4.Text =string.Empty;
            Txt_Order_wt_5.Text =string.Empty;
            Txt_Order_wt_6.Text =string.Empty;

            Txt_Order_no_1.Text =string.Empty;
            Txt_Order_no_2.Text =string.Empty;
            Txt_Order_no_3.Text =string.Empty;
            Txt_Order_no_4.Text =string.Empty;
            Txt_Order_no_5.Text =string.Empty;
            Txt_Order_no_6.Text =string.Empty;

            Cob_Leader_Usage.SelectedIndex = 0;

            Txt_LeaderHeadSt_no.Text =string.Empty;
            Txt_LeaderHeadLen.Text =string.Empty;
            Txt_LeaderHeadWd.Text =string.Empty;
            Txt_LeaderHeadThickness.Text =string.Empty;

            Txt_LeaderTailSt_no.Text =string.Empty;
            Txt_LeaderTailLen.Text =string.Empty;
            Txt_LeaderTailWd.Text =string.Empty;
            Txt_LeaderTailThickness.Text =string.Empty;

            Txt_CustomerNo.Text = string.Empty;
            Txt_CustomerName_E.Text = string.Empty;
            Txt_CustomerName_C.Text = string.Empty;

            #endregion

            #region 清空缺陷資料

            #region  - Code -
            Txt_Code_D01.Text = string.Empty;
            Txt_Code_D02.Text = string.Empty;
            Txt_Code_D03.Text = string.Empty;
            Txt_Code_D04.Text = string.Empty;
            Txt_Code_D05.Text = string.Empty;
            Txt_Code_D06.Text = string.Empty;
            Txt_Code_D07.Text = string.Empty;
            Txt_Code_D08.Text = string.Empty;
            Txt_Code_D09.Text = string.Empty;
            Txt_Code_D10.Text = string.Empty;
            #endregion

            #region - Origin -
            Txt_Origin_D01.Text = string.Empty;
            Txt_Origin_D02.Text = string.Empty;
            Txt_Origin_D03.Text = string.Empty;
            Txt_Origin_D04.Text = string.Empty;
            Txt_Origin_D05.Text = string.Empty;
            Txt_Origin_D06.Text = string.Empty;
            Txt_Origin_D07.Text = string.Empty;
            Txt_Origin_D08.Text = string.Empty;
            Txt_Origin_D09.Text = string.Empty;
            Txt_Origin_D10.Text = string.Empty;
            #endregion

            #region - Sid (ComboBox)-
            Cob_Sid_D01.SelectedIndex = -1;
            Cob_Sid_D02.SelectedIndex = -1;
            Cob_Sid_D03.SelectedIndex = -1;
            Cob_Sid_D04.SelectedIndex = -1;
            Cob_Sid_D05.SelectedIndex = -1;
            Cob_Sid_D06.SelectedIndex = -1;
            Cob_Sid_D07.SelectedIndex = -1;
            Cob_Sid_D08.SelectedIndex = -1;
            Cob_Sid_D09.SelectedIndex = -1;
            Cob_Sid_D10.SelectedIndex = -1;
            #endregion

            #region - Pos_W (ComboBox)-
            Cob_PosW_D01.SelectedIndex = -1;
            Cob_PosW_D02.SelectedIndex = -1;
            Cob_PosW_D03.SelectedIndex = -1;
            Cob_PosW_D04.SelectedIndex = -1;
            Cob_PosW_D05.SelectedIndex = -1;
            Cob_PosW_D06.SelectedIndex = -1;
            Cob_PosW_D07.SelectedIndex = -1;
            Cob_PosW_D08.SelectedIndex = -1;
            Cob_PosW_D09.SelectedIndex = -1;
            Cob_PosW_D10.SelectedIndex = -1;
            #endregion

            #region - Pos_L_Start - 
            Txt_Pos_L_Start_D01.Text = string.Empty;
            Txt_Pos_L_Start_D02.Text = string.Empty;
            Txt_Pos_L_Start_D03.Text = string.Empty;
            Txt_Pos_L_Start_D04.Text = string.Empty;
            Txt_Pos_L_Start_D05.Text = string.Empty;
            Txt_Pos_L_Start_D06.Text = string.Empty;
            Txt_Pos_L_Start_D07.Text = string.Empty;
            Txt_Pos_L_Start_D08.Text = string.Empty;
            Txt_Pos_L_Start_D09.Text = string.Empty;
            Txt_Pos_L_Start_D10.Text = string.Empty;
            #endregion

            #region - Pos_L_End -
            Txt_Pos_L_End_D01.Text = string.Empty;
            Txt_Pos_L_End_D02.Text = string.Empty;
            Txt_Pos_L_End_D03.Text = string.Empty;
            Txt_Pos_L_End_D04.Text = string.Empty;
            Txt_Pos_L_End_D05.Text = string.Empty;
            Txt_Pos_L_End_D06.Text = string.Empty;
            Txt_Pos_L_End_D07.Text = string.Empty;
            Txt_Pos_L_End_D08.Text = string.Empty;
            Txt_Pos_L_End_D09.Text = string.Empty;
            Txt_Pos_L_End_D10.Text = string.Empty;
            #endregion

            #region - Level (ComboBox)-
            Cob_Level_D01.SelectedIndex = -1;
            Cob_Level_D02.SelectedIndex = -1;
            Cob_Level_D03.SelectedIndex = -1;
            Cob_Level_D04.SelectedIndex = -1;
            Cob_Level_D05.SelectedIndex = -1;
            Cob_Level_D06.SelectedIndex = -1;
            Cob_Level_D07.SelectedIndex = -1;
            Cob_Level_D08.SelectedIndex = -1;
            Cob_Level_D09.SelectedIndex = -1;
            Cob_Level_D10.SelectedIndex = -1;
            #endregion

            #region - Percent -
            Txt_Percent_D01.Text = string.Empty;
            Txt_Percent_D02.Text = string.Empty;
            Txt_Percent_D03.Text = string.Empty;
            Txt_Percent_D04.Text = string.Empty;
            Txt_Percent_D05.Text = string.Empty;
            Txt_Percent_D06.Text = string.Empty;
            Txt_Percent_D07.Text = string.Empty;
            Txt_Percent_D08.Text = string.Empty;
            Txt_Percent_D09.Text = string.Empty;
            Txt_Percent_D10.Text = string.Empty;
            #endregion

            #region - QGrade - 
            Txt_QGRADE_D01.Text = string.Empty;
            Txt_QGRADE_D02.Text = string.Empty;
            Txt_QGRADE_D03.Text = string.Empty;
            Txt_QGRADE_D04.Text = string.Empty;
            Txt_QGRADE_D05.Text = string.Empty;
            Txt_QGRADE_D06.Text = string.Empty;
            Txt_QGRADE_D07.Text = string.Empty;
            Txt_QGRADE_D08.Text = string.Empty;
            Txt_QGRADE_D09.Text = string.Empty;
            Txt_QGRADE_D10.Text = string.Empty;
            #endregion

            #endregion
        }

        #endregion

        private void Tab_PDIControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_PDIControl, e);
        }

        #region 说明栏
        private void Fun_DefectSpare()
        {
            #region 缺陷代码
            Txt_Code_Desc.Text = "第一位：缺陷种类（1st Byte，Defect Sort）" + Environment.NewLine
                         + "   S_表面缺陷（Surface）" + Environment.NewLine
                         + "   E_边缘缺陷 (Edge)" + Environment.NewLine
                         + "   F_板形缺陷 (Shape)" + Environment.NewLine
                         + "   W_盘卷缺陷 (Coil)" + Environment.NewLine
                         + "   D_尺寸/重量缺陷 (Size/Weight)" + Environment.NewLine
                         + "   M_物性缺陷 (Physics)" + Environment.NewLine
                         + "   C_化性/耐蚀性缺陷 (Chemistry/ Corrosion Resistance)" + Environment.NewLine
                         + "   其它缺陷(Others)" + Environment.NewLine
                         + "第二位与第三位：缺陷种类序号 01,02，……" + Environment.NewLine
                         + "(2nd  Byte And 3rd Byte,Sequence No,01,02, …)" + Environment.NewLine;
            #endregion

            #region 缺陷来源
            Txt_Origin_Desc.Text = "Sm1:1#炼钢厂(Steel Plant)" + Environment.NewLine
                         + "Rf1:1#Rf" + Environment.NewLine
                         + "Hm1:1#Hsm" + Environment.NewLine
                         + "Ba1:1#Baf" + Environment.NewLine
                         + "Hp1:1#Hapl" + Environment.NewLine
                         + "Rc1:1#Rcl" + Environment.NewLine
                         + "Pa1:1#Pak" + Environment.NewLine
                         + "Rs1:1#Roll_Shop" + Environment.NewLine
                         + "Wh1:1#Warehouse" + Environment.NewLine;
            #endregion

            #region 表面区分
            Txt_Sid_Desc.Text = "T:上(Top)"+Environment.NewLine
                         + "B:下(Bottom)" + Environment.NewLine
                         + "A:上下都有(Both)" + Environment.NewLine;
            #endregion

            #region 宽向位置
            Txt_Pos_Desc.Text = "W:操作側1/4板宽 " + Environment.NewLine
                            + "  (1/4 Strip Width Of Operate Side)" + Environment.NewLine
                            + "D:驅動側1/4板宽 " + Environment.NewLine
                            + "  (1/4 Strip Width Of Driver Side)" + Environment.NewLine
                            + "C:中央1/2位置(Center)" + Environment.NewLine
                            + "B:兩側皆有 (Both Side)" + Environment.NewLine
                            + "A:宽度方向全面皆有(All Side)" + Environment.NewLine;
            #endregion

            #region 长向位置
            Txt_Pos_L_Desc.Text = "比如(Example):"+Environment.NewLine
                         + "缺陷于距带钢头部800m，则为0800" + Environment.NewLine
                         + "Distance Of Strip Head 800m,so the Item is 0800." + Environment.NewLine;
            #endregion

            #region 缺陷程度
            Txt_Level_Desc.Text = "L：轻微(Light)" + Environment.NewLine
                             + "M：中等(Middle)" + Environment.NewLine
                             + "H：严重(Heavy)" + Environment.NewLine
                             + "S：极严重(Serious)" + Environment.NewLine;
            #endregion

            #region 缺陷比例
            Txt_Percent_Desc.Text = "比如(Example):"+Environment.NewLine
                             + "13.14% Record To 131;"+Environment.NewLine
                             + "3.16% Record To 032;"+Environment.NewLine
                             + "100% Record To 000"+Environment.NewLine;
            #endregion

        }

        private void Btn_Description_Click(object sender, EventArgs e)
        {
            if (!Pnl_Description.Visible)
            {
                //缺陷说明
                Fun_DefectSpare();
                Pnl_Description.Size = new Size(1010, 571);
                Pnl_Description.Location = new Point(403, 117);
                Pnl_Description.Visible = true;
            }
            else
            {
                Pnl_Description.Visible = false;
            }
            
        }

        private void Btn_Close_Desc_Click(object sender, EventArgs e)
        {
            Pnl_Description.Visible = false;
        }

        #endregion
        
        #region ComboBox說明
        private void Fun_ComboBoxDescription(Button btnName)
        {
            string strLblText = "";
            Cbo_Type cbo_Type = 0;
            Point point = new Point();
            Txt_Spare.ScrollBars = ScrollBars.None;
            switch (btnName.Name)
            {
                case nameof(Btn_Sleeve_Type_Entry):
                    strLblText = "入口套筒类型";
                    cbo_Type = Cbo_Type.Sleeve_Type;
                    point = new Point(341, 395);
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    break;

                case nameof(Btn_PAPER_REQ_CODE):
                    strLblText = "入口垫纸方式";
                    cbo_Type = Cbo_Type.PAPER_REQ_CODE;
                    point = new Point(341, 395);
                    break;

                case nameof(Btn_In_Paper_Type_Entry):
                    strLblText = "入口垫纸类型";
                    cbo_Type = Cbo_Type.Paper_Type;
                    point = new Point(341, 395);
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    break;
                    
                case nameof(Btn_Out_Paper_Req_Code):
                    strLblText = "出口垫纸方式";
                    cbo_Type = Cbo_Type.PAPER_REQ_CODE;
                    point = new Point(1147, 55);                   
                    break;
                   
                case nameof(Btn_Paper_Type_Exit):
                    strLblText = "出口垫纸类型";
                    cbo_Type = Cbo_Type.Paper_Type;
                    point = new Point(1147, 55);
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    break;
                   
                case nameof(Btn_Sleeve_Type_Exit):
                    strLblText = "出口套筒类型";
                    cbo_Type = Cbo_Type.Sleeve_Type;
                    point = new Point(1147, 55);
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    break;                 
               
                default:                   
                    return;

            }
            Lbl_ComboName_Spare.Text = strLblText;
            Pnl_Spare.Visible = true;
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
            Pnl_Spare.Location = point;
            Pnl_Spare.BringToFront();
            //ComboBox 的說明Panel 預設大小
            Pnl_Spare.Size = new Size(435, 225);
        }

        /// <summary>
        /// 關閉說明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ClosePanel_Click(object sender, EventArgs e)
        {
            Pnl_Spare.Visible = false;
        }

        /// <summary>
        /// 入口套筒類型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Sleeve_Type_Entry_Click(object sender, EventArgs e)
        {
            if (!Pnl_Spare.Visible)
            {
                Fun_ComboBoxDescription(((Button)(sender)));                
                Pnl_Spare.Visible = true;                           
            }
            else
            {
                Pnl_Spare.Visible = false;
            }           
        }

        /// <summary>
        /// 入口墊紙方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PAPER_REQ_CODE_Click(object sender, EventArgs e)
        {
            //Lbl_ComboName_Spare.Text = "入口垫纸方式";
            //Pnl_Spare.Visible = true;
            //ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(Cbo_Type.PAPER_REQ_CODE, Txt_Spare);
            //Pnl_Spare.Location = new Point(341, 395);

            if (!Pnl_Spare.Visible)
            {
                Fun_ComboBoxDescription(((Button)(sender)));
                Pnl_Spare.Visible = true;
            }
            else
            {
                Pnl_Spare.Visible = false;
            }
        }

        /// <summary>
        /// 入口墊紙類型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_In_Paper_Type_Entry_Click(object sender, EventArgs e)
        {
            //Lbl_ComboName_Spare.Text = "入口垫纸类型";
            //Pnl_Spare.Visible = true;
            //ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(Cbo_Type.Paper_Type, Txt_Spare);
            //Pnl_Spare.Location = new Point(341, 395);
            if (!Pnl_Spare.Visible)
            {
                Fun_ComboBoxDescription(((Button)(sender)));
                Pnl_Spare.Visible = true;
            }
            else
            {
                Pnl_Spare.Visible = false;
            }
        }

        /// <summary>
        /// 出口套筒類型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Sleeve_Type_Exit_Click(object sender, EventArgs e)
        {
            //lbComboName.Text = "出口套筒类型";
            //panel_Spare.Visible = true;
            //ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(Cbo_Type.Sleeve_Type, txtSpare);
            //panel_Spare.Location = new Point(1147, 55);
            if (!Pnl_Spare.Visible)
            {
                Fun_ComboBoxDescription(((Button)(sender)));
                Pnl_Spare.Visible = true;
            }
            else
            {
                Pnl_Spare.Visible = false;
            }
        }

        /// <summary>
        /// 出口墊紙方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_OUT_PAPER_REQ_CODE_Click(object sender, EventArgs e)
        {
            //lbComboName.Text = "出口垫纸方式";
            //panel_Spare.Visible = true;
            //ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(Cbo_Type.PAPER_REQ_CODE, txtSpare);
            //panel_Spare.Location = new Point(1147, 55);
            if (!Pnl_Spare.Visible)
            {
                Fun_ComboBoxDescription(((Button)(sender)));
                Pnl_Spare.Visible = true;
            }
            else
            {
                Pnl_Spare.Visible = false;
            }
        }

        /// <summary>
        /// 出口墊紙種類
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Paper_Type_Exit_Click(object sender, EventArgs e)
        {
            //lbComboName.Text = "出口垫纸类型";
            //panel_Spare.Visible = true;
            //ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(Cbo_Type.Paper_Type, txtSpare);
            //panel_Spare.Location = new Point(1147, 55);
            if (!Pnl_Spare.Visible)
            {
                Fun_ComboBoxDescription(((Button)(sender)));
                Pnl_Spare.Visible = true;
            }
            else
            {
                Pnl_Spare.Visible = false;
            }
        }

        #endregion

        private void Cob_Sleeve_Type_Entry_Click(object sender, EventArgs e)
        {
            //入口套筒類型
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Sleeve(Cob_Sleeve_Type_Entry);
        }

        private void Cob_In_Paper_Type_Entry_Click(object sender, EventArgs e)
        {
            //入口墊紙類型
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Paper(Cob_In_Paper_Type_Entry);
        }

        private void Cob_Paper_Type_Exit_Click(object sender, EventArgs e)
        {
            //出口墊紙類型
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Paper(Cob_Paper_Type_Exit);
        }

        private void Cob_Sleeve_Type_Exit_Click(object sender, EventArgs e)
        {
            //出口套筒類型
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Sleeve(Cob_Sleeve_Type_Exit);
        }
       
        private void Btn_PrintTag_Click(object sender, EventArgs e)
        {         
            //if (Txt_EntryCoil.Text.Trim().IsEmpty())
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("请查询欲打印标签之钢卷", "列印标签",0);
            //    return;
            //}

            Frm_PrintLabels frm_Print = new Frm_PrintLabels
            {
                Str_Coil_No = Txt_EntryCoil.Text.Trim()
            };
            frm_Print.ShowDialog();
            frm_Print.Dispose();

            if (frm_Print.DialogResult == DialogResult.OK)
            {
                string strShowText = $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()} 打印钢卷号:[{frm_Print.Str_Coil_No}]标签";
                EventLogHandler.Instance.LogInfo("1-2", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()} 打印标签作业", $"打印钢卷号:[{frm_Print.Str_Coil_No}]标签");
                EventLogHandler.Instance.EventPush_Message(strShowText);
                PublicComm.ClientLog.Info(strShowText);

            }

            ////改由HMI直接列印
            //SCCommMsg.CS07_PrintLabel _PrintLabel = new SCCommMsg.CS07_PrintLabel
            //{
            //    Source = "CPL_HMI",
            //    ID = "PrintLabel",
            //    CoilID = Txt_EntryCoil.Text.Trim()
            //};
            //PublicComm.Client.Tell(_PrintLabel);           
        }

        private void Fun_OnlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //&& (e.KeyChar != (char)Keys.Space) 
            //          數字                                            //backspace                    //Enter             
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Enter)))
            {
                e.Handled = true;
            }
        }

        private void Fun_LanguageIsEn_Font14_10(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
        }
        private void Fun_LanguageIsEn_Font12_10(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font12_9(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)9, fs);
        }
    }
}
