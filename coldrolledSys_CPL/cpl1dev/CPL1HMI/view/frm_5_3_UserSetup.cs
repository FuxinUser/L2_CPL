using System.Drawing;
using System.Data;
using System.Windows.Forms;
using static CPL1HMI.DataBaseTableFactory;
using System;

namespace CPL1HMI
{
    public partial class frm_5_3_UserSetup : Form
    {
        Point FristPoint_Lable = new Point(27, 215);
        Point FristPoint_TextBox = new Point(167, 214);
        Point ChangePoint_Lable = new Point(27, 287);
        Point ChangePoint_TextBox = new Point(167, 286);
        Point TeamPoint_Lable = new Point(27, 431);
        Point TeamPoint_ComboBox = new Point(167, 431);

        DataTable dtGetUser;

        bool bolHaveFrameData;

        string strStatus = "Read";
        bool bolEditStatus;
        Control[] CtrControlArray;

        DataTable dtSelectOne;//User Data //只取一笔 提供栏位填值 存值
        DataTable dtBeforeEdit;//User Data //编辑前 备份

        //DataTable dtSelectOne_A;//Authority Data //只取一笔 提供栏位填值 存值
        //DataTable dtBeforeEdit_A;//Authority Data //编辑前 备份
        //語系
        private LanguageHandler LanguageHand;

        public frm_5_3_UserSetup()
        {
            InitializeComponent();
        }

        private void Frm_5_3_UserSetup_Load(object sender, System.EventArgs e)
        {
            if (PublicForms.UserSetup == null) PublicForms.UserSetup = this;

            Control[] Frm_5_3_Control = new Control[] {
                Btn_Delete,
                Btn_Edit,
                Btn_New
            };
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_5_3_Control, UserSetupHandler.Instance.Frm_5_3);

            //// 密碼位置
            //Lbl_Password_Title.Location = FristPoint_Lable;
            //Txt_Password.Location = FristPoint_TextBox;
            ////班別欄位移動
            //Lbl_Team_Title.Location = ChangePoint_Lable;
            //Cob_Team.Location = ChangePoint_TextBox;

            //帳號清單 Cob設定
            UserSetupHandler.Instance.UserID_List(Cob_UserID_Search);
            //權限等級 Cob設定
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Authority_Class, Cob_Authority_Class);
            //班別 Cob設定
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Team,Cob_Team);

            Cob_UserID_Search.SelectedIndex = -1;
            Cob_Authority_Class.SelectedIndex = -1;
            Cob_Team.SelectedIndex = -1;
            Lbl_Updated.Text = "";
            CtrControlArray = new Control[] { Grb_User };
            //人員 欄位清空
            ReadOnlyHandler.Instance.ClearControl(CtrControlArray);
            //人員 欄位ReadOnly
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_User, true);
                      
            //Status = "Read",只顯示的欄位
            Fun_StatusToShowColumns("Read");
            //權限 RadioButton.Enabled = false;
            Fun_AuthorityEnabled(false);

            //一開始載入,沒有查詢資料,只提供新增

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);

            //先查一次空白,取得 dtGetUser.Clone()
            string strSql = Frm_5_3_SqlFactory.SQL_Select_User();
            dtSelectOne = DataAccess.Fun_SelectDate(strSql, "账号资讯查询");

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        /// <summary>
        /// 依狀態顯示欄位
        /// </summary>
        /// <param name="strStatus"></param>
        private void Fun_StatusToShowColumns(string strStatus)
        {
            Control[] ctlArr_Lbl;
            Control[] ctlArr_Txt;

            switch (strStatus)
            {
                case "Read":
                    ctlArr_Lbl = new Control[] { Lbl_UserID_Title, Lbl_Department_Title, Lbl_Team_Title, Lbl_Authority_Class_Title, Lbl_Password_Title };
                    ctlArr_Txt = new Control[] { Txt_UserID, Txt_Department, Cob_Team, Cob_Authority_Class, Txt_Password };
                    break;

                case  "New":
                    ctlArr_Lbl = new Control[] { Lbl_UserID_Title, Lbl_Department_Title, Lbl_Team_Title, Lbl_Authority_Class_Title, Lbl_Password_Title, Lbl_Password_Chk_Title };
                    ctlArr_Txt = new Control[] { Txt_UserID, Txt_Department, Cob_Team, Cob_Authority_Class, Txt_Password, Txt_Password_Chk };
                    break;

                case "Edit":
                    ctlArr_Lbl = new Control[] { Lbl_UserID_Title, Lbl_Department_Title, Lbl_Team_Title, Lbl_Authority_Class_Title, Lbl_Password_Old_Title, Lbl_Password_Title, Lbl_Password_Chk_Title };
                    ctlArr_Txt = new Control[] { Txt_UserID, Txt_Department, Cob_Team, Cob_Authority_Class, Txt_Password_Old, Txt_Password, Txt_Password_Chk };
                    break;

                default:
                    ctlArr_Lbl = new Control[] { Lbl_UserID_Title, Lbl_Department_Title, Lbl_Team_Title, Lbl_Password_Title };
                    ctlArr_Txt = new Control[] { Txt_UserID, Txt_Department, Cob_Team, Txt_Password };
                    break;
            }
            Fun_LocationShow(ctlArr_Lbl, ctlArr_Txt);
        }

        private void Fun_LocationShow(Control[] ctlArr_Lbl, Control[] ctlArr_Txt)
        {
            #region Before

            //Lbl_name.Location = new Point(50, 50);
            //Txt_name.Location = new Point(190, 50);
            //Lbl_password.Location = new Point(50, 91);
            //Txt_password.Location = new Point(190, 91);
            //Lbl_description.Location = new Point(50, 132);
            //Txt_description.Location = new Point(190, 132);

            //Lbl_name.Visible = true;
            //Txt_name.Visible = true;
            //Lbl_password.Visible = true;
            //Txt_password.Visible = true;
            //Lbl_description.Visible = true;
            //Txt_description.Visible = true;

            //Control[] ctlArr_L = new Control[] { Lbl_name , Txt_name ,
            //                                   Lbl_password , Txt_password ,
            //                                   Lbl_description , Txt_description };

            #endregion
            Fun_NotShow();
            int intLbl_X = 27;
            int intLbl_Y = 35;
            int intTxt_X = 167;
            int intTxt_Y = 35;

            foreach (Control ctl in ctlArr_Lbl)
            {
                ctl.Location = new Point(intLbl_X, intLbl_Y);
                ctl.Visible = true;
                //intLbl_X += 43;
                intLbl_Y += 43;
            }

            foreach (Control ctl in ctlArr_Txt)
            {
                ctl.Location = new Point(intTxt_X, intTxt_Y);
                ctl.Visible = true;
                //intTxt_X += 43;
                intTxt_Y += 43;
            }

            if (strStatus == "Edit")
            {
                /*"Edit"*/
                int intBtn_X = 0;
                int intBtn_Y = 0;

                intBtn_X = Lbl_Password_Title.Location.X + 109;
                intBtn_Y = Lbl_Password_Title.Location.Y + 1;

                Btn_EditPassWord.Location = new Point(intBtn_X, intBtn_Y);
            }

        }
        private void Fun_NotShow()
        {
            Control[] ctlArr_Lbl = new Control[] { /*Lbl_name,*/ Lbl_Password_Old_Title, /*Lbl_password,*/ Lbl_Password_Chk_Title/*, Lbl_description*/ };
            Control[] ctlArr_Txt = new Control[] {/* Txt_name,*/ Txt_Password_Old, /*Txt_password,*/ Txt_Password_Chk/*, Txt_description*/ };

            foreach (Control ctl in ctlArr_Lbl)
                ctl.Visible = false;

            foreach (Control ctl in ctlArr_Txt)
                ctl.Visible = false;

        }
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, System.EventArgs e)
        {
            //狀態為 New(新增) & Edit(編輯) 時，不執行後續程式
            if (strStatus == "New" || strStatus == "Edit")
            {
                /*"New"*/  /*"Edit"*/
                DialogHandler.Instance.Fun_DialogShowOk("資料編輯中，請先存檔或取消！", "账号资讯查询", 0);
                return;
            }

            //查詢條件空白，不執行後續程式
            //if (string.IsNullOrEmpty(Cob_UserID_Search.Text.Trim())) { return; }
            Fun_SelectUser();
            //權限 RadioButton.Enabled = false;
            Fun_AuthorityEnabled(bolEditStatus);
        }

        private void Fun_SelectUser()
        {
            string strSql = Frm_5_3_SqlFactory.SQL_Select_User();
            dtGetUser = DataAccess.Fun_SelectDate(strSql, "账号资讯查询");

            if (dtGetUser != null )
            {
                //人員 沒資料
                dtSelectOne = dtGetUser.Clone();

                if (dtGetUser.Rows.Count <= 0)
                {
                    DialogHandler.Instance.Fun_DialogShowOk("查无资料", "账号资讯查询", 0);

                    return;
                }
            }

            #region 检查有没有建权限
            string strSql_Frame = Frm_5_3_SqlFactory.SQL_Check_AuthorityData_Frame(Cob_UserID_Search.Text);
            DataTable dtFrame = DataAccess.Fun_SelectDate(strSql_Frame, "人员权限资讯查询");
            if (dtFrame == null || dtFrame.Rows.Count <= 0)
            {
                DialogHandler.Instance.Fun_DialogShowOk("查无人员权限资料，将给予预设值显示！", "人员权限资讯查询", 0);
                bolHaveFrameData = false;

                #region  无人员权限资料 给予预设
                rdoN1_1.Checked = true;
                rdoN1_2.Checked = true;
                rdoN1_3.Checked = true;
                rdoN2_1.Checked = true;
                rdoN2_2.Checked = true;
                rdoN3_1.Checked = true;
                rdoN3_2.Checked = true;
                rdoN3_3.Checked = true;
                rdoN3_3.Checked = true;
                rdoN4_1.Checked = true;
                rdoN4_2.Checked = true;
                rdoN4_3.Checked = true;
                rdoN5_1.Checked = true;
                rdoN5_2.Checked = true;
                rdoN5_3.Checked = true;
                rdoN5_4.Checked = true;
                rdoN5_5.Checked = true;
                rdoN5_6.Checked = true;
                #endregion
            }
            else
            {
                bolHaveFrameData = true;
            }
            #endregion

            //人員 有資料
            dtSelectOne = dtGetUser.Copy();
                       
            //人員資料 畫面權限
            Fun_SetUserData(dtSelectOne);    

            if (dtSelectOne.IsNull())
            {
                //修改
                Fun_SetBottonEnabled(Btn_Edit, false);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, false);
            }
            else
            {
                //修改
                Fun_SetBottonEnabled(Btn_Edit, true);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, true);
            }
            //新增
            Fun_SetBottonEnabled(Btn_New, true);
        }

        private void Fun_SetUserData(DataTable dt)
        {
            //帳號
            Txt_UserID.Text = dt.Rows[0][nameof(TBL_AuthorityData.User_ID)].ToString() ?? string.Empty;
            //密碼
            Txt_Password.Text = dt.Rows[0][nameof(TBL_AuthorityData.Password)].ToString() ?? string.Empty;
            //舊密碼
            Txt_Password_Old.Text = "";// Txt_Password.Text;
            Txt_Department.Text = dt.Rows[0][nameof(TBL_AuthorityData.Department)].ToString() ?? string.Empty;
            //權限等級
            Cob_Authority_Class.SelectedValue = dt.Rows[0][nameof(TBL_AuthorityData.Authority_Class)].ToString() ?? string.Empty;
            //班別
            Cob_Team.SelectedValue = dt.Rows[0][nameof(TBL_AuthorityData.Team)].ToString() ?? string.Empty;
            //異動時間
            Lbl_Updated.Text = dtGetUser.Rows[0][nameof(TBL_AuthorityData.Create_DateTime)].ToString() ?? string.Empty;
            for (int Index = 0; Index < dt.Rows.Count; Index++)
            {
                FrameSetup(dt.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_ID)].ToString().Trim(), Index);
            }
        }
         


        /// <summary>
        /// 畫面權限設定-查詢結果
        /// </summary>
        /// <param name="Frame"></param>
        /// <param name="Index"></param>
        private void FrameSetup(string Frame,int Index)
        {
            switch (Frame)
            {
                //case "0-2":
                //    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                //        rdoN0_2.Checked = true;
                //    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                //        rdoR0_2.Checked = true;
                //    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                //        rdoW0_2.Checked = true;
                //    break;
                case "1-1":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN1_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR1_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW1_1.Checked = true;
                    else
                        rdoN1_1.Checked = true;
                    break;
                case "1-2":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN1_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR1_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW1_2.Checked = true;
                    else
                        rdoN1_2.Checked = true;
                    break;
                case "1-3":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN1_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR1_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW1_3.Checked = true;
                    else
                        rdoN1_3.Checked = true;
                    break;
                case "2-1":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN2_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR2_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW2_1.Checked = true;
                    else
                        rdoN2_1.Checked = true;
                    break;
                case "2-2":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN2_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR2_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW2_2.Checked = true;
                    else
                        rdoN2_2.Checked = true;
                    break;
                case "3-1":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN3_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR3_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW3_1.Checked = true;
                    else
                        rdoN3_1.Checked = true;
                    break;
                case "3-2":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN3_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR3_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW3_2.Checked = true;
                    else
                        rdoN3_2.Checked = true;
                    break;
                case "3-3":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN3_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR3_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW3_3.Checked = true;
                    else
                        rdoN3_3.Checked = true;
                    break;
                //case "3-4":
                //    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                //        rdoN.Checked = true;
                //    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                //        rdoR.Checked = true;
                //    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                //        rdoW.Checked = true;
                //    break;
                case "4-1":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN4_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR4_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW4_1.Checked = true;
                    else
                        rdoN4_1.Checked = true;
                    break;
                case "4-2":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN4_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR4_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW4_2.Checked = true;
                    else
                        rdoN4_2.Checked = true;
                    break;
                case "4-3":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN4_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR4_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW4_3.Checked = true;
                    else
                        rdoN4_3.Checked = true;
                    break;
                case "5-1":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN5_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR5_1.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW5_1.Checked = true;
                    else
                        rdoN5_1.Checked = true;
                    break;
                case "5-2":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN5_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR5_2.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW5_2.Checked = true;
                    else
                        rdoN5_2.Checked = true;
                    break;
                case "5-3":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN5_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR5_3.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW5_3.Checked = true;
                    else
                        rdoN5_3.Checked = true;
                    break;
                case "5-4":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN5_4.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR5_4.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW5_4.Checked = true;
                    else
                        rdoN5_4.Checked = true;
                    break;
                case "5-5":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN5_5.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR5_5.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW5_5.Checked = true;
                    else
                        rdoN5_5.Checked = true;
                    break;
                case "5-6":
                    if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("N"))
                        rdoN5_6.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("R"))
                        rdoR5_6.Checked = true;
                    else if (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString().Equals("W"))
                        rdoW5_6.Checked = true;
                    else
                        rdoN5_6.Checked = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_New_Click(object sender, EventArgs e)
        {
            if (bolEditStatus) return;
            //先判断 原先有没有显示资料,有就Copy备份 , 没有就Clone栏位就好
            if (!dtSelectOne.IsNull())
            {
                dtBeforeEdit = dtSelectOne.Copy();
                //dtBeforeEdit_A = dtSelectOne_A.Copy();
            }
            else
            {
                dtBeforeEdit = dtSelectOne.Clone();
                //dtBeforeEdit_A = dtSelectOne_A.Clone();
            }

            //状态:New 顯示基本欄位 +  密码确认栏位
            strStatus = "New";
            bolEditStatus = true;
            Fun_StatusToShowColumns(strStatus);
            //權限 RadioButton.Enabled = false;
            Fun_AuthorityEnabled(bolEditStatus);

            //人員欄位 清空            
            ReadOnlyHandler.Instance.ClearControl(CtrControlArray);
            Lbl_Updated.Text = "";
            Cob_Team.SelectedIndex = -1;
            Cob_Authority_Class.SelectedIndex = -1;
           
            //人員 欄位ReadOnly
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_User, false);
            ////新增狀態-Key必填
            //Txt_UserID.ReadOnly = false;
            //Txt_UserID.BackColor = Color.White;
            //权限 全N
            Fun_AuthorityNewData();
          
            Btn_Save.Visible = true; //儲存
            Btn_Cancel.Visible = true; //取消
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);
          
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            if (bolEditStatus) return;
            //先判断 原先有没有显示资料,有就Copy备份 , 没有就Clone栏位就好
            if (!dtSelectOne.IsNull())
                dtBeforeEdit = dtSelectOne.Copy();
            else
                dtBeforeEdit = dtSelectOne.Clone();

            //状态:Edit 顯示基本欄位 + 旧密码 & 密码确认栏位
            strStatus = "Edit";
            bolEditStatus = true;
            Fun_StatusToShowColumns(strStatus);

            //欄位 人员 不唯讀  
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_User, false);
            //欄位 权限 启用
            Fun_AuthorityEnabled(true);


            //原  密碼 沒異動時 唯讀
            Txt_Password.Enabled = false;
            //Txt_Password.BackColor = Color.Gainsboro;
            Txt_Password_Old.Enabled = false;
            //Txt_Password_Old.BackColor = Color.Gainsboro;
            Txt_Password_Chk.Enabled = false;
            //Txt_Password_Chk.BackColor = Color.Gainsboro;

            //編輯狀態-Key不可修改
            Txt_UserID.Enabled = false;
            //Txt_UserID.BackColor = Color.Gainsboro;

            ////清空密碼
            //Txt_Password.Text = string.Empty;
            //Txt_Password.ReadOnly = false;
            //////密碼欄位移動
            ////Lbl_Password_Title.Location = ChangePoint_Lable;
            ////Txt_Password.Location = ChangePoint_TextBox;
            //////班別欄位移動
            ////Lbl_Team_Title.Location = TeamPoint_Lable;
            ////Cob_Team.Location = TeamPoint_ComboBox;

            //Lbl_Password_Old_Title.Visible = true;
            //Txt_Password_Old.Visible = true;
            //Lbl_Password_Chk_Title.Visible = true;
            //Txt_Password_Chk.Visible = true;


            //新增
            Fun_SetBottonEnabled(Btn_New, false);
            //編輯
            Fun_SetBottonEnabled(Btn_Edit, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);

            Btn_Save.Visible = true; //儲存
            Btn_Cancel.Visible = true; //取消
            Btn_EditPassWord.Visible = true;
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (strStatus == "Read") { return; }
            if (!bolEditStatus) return;

            string strPsd_before = "";
            if (!dtBeforeEdit.IsNull())
            {
                strPsd_before = dtBeforeEdit.Rows[0]["Password"].ToString().Trim();
            }

            string strSql = string.Empty;

            //新增
            if (Btn_New.Enabled)
            {
                //检查账号
                if (string.IsNullOrEmpty(Txt_UserID.Text.Trim()))
                {                   
                    DialogHandler.Instance.Fun_DialogShowOk("账号不可为空白！", "人员账号檢查", 0);
                    Txt_UserID.Focus();
                    return;
                }
                else
                {
                    //检查账号是否已存在
                    string strSqlOn = Frm_5_3_SqlFactory.SQL_Check_User();//name, password, description, Updated, UpdatedProc 

                    DataTable dtCheckUser = DataAccess.Fun_SelectDate(strSqlOn, "账号檢查");
                    if (!dtCheckUser.IsNull())
                    {                        
                        DialogHandler.Instance.Fun_DialogShowOk("账号已重复！", "人员账号檢查", 0);
                        Txt_UserID.Focus();
                        return;
                    }
                }

                //帳號密碼
                strSql = Frm_5_3_SqlFactory.SQL_Insert_User();
                Fun_InsertUpdateDataBase(strSql);
                //畫面權限
                strSql = Frm_5_3_SqlFactory.SQL_Insert_Frame();
                Fun_InsertUpdateDataBase(strSql);
            }
            //修改
            else if (Btn_Edit.Enabled)
            {
                //密碼如果有變更 
                if (strPsd_before != Txt_Password.Text.Trim())
                {
                    //检查 舊密码
                    if (string.IsNullOrEmpty(Txt_Password_Old.Text.Trim()))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk("旧密码请勿为空白！", "人员密码檢查", 0);                       
                        Txt_Password_Old.Focus();
                        return;
                    }
                    else
                    {
                        ////检查 舊密码 是否正確
                        //string strSqlOn = $"SELECT * FROM APLA_OperatorInfo WHERE   User_ID = '{Txt_Name.Text.Trim()}' AND Password = N'{Txt_Password_Old.Text.Trim()}' ";//name, password, description, Updated, UpdatedProc 

                        //object obj = PublicComm.portal.DbHand.Fun_GetObject(strSqlOn, PublicSystem.ConnectionString);
                        if (strPsd_before != Txt_Password_Old.Text)
                        {
                            DialogHandler.Instance.Fun_DialogShowOk("旧密码不正確，請重新輸入！", "人员密码檢查", 0);                          
                            Txt_Password_Old.Focus();
                            return;
                        }
                        else
                        {
                            //舊密碼正確 
                        }
                    }

                    if (!Fun_ChackPassword())
                    {
                        return;
                    }
                }
                ////新密碼與舊密碼不同且與密碼確認相同
                //if (Txt_Password.Text.Equals(Txt_Password_Chk.Text) && !Txt_Password.Text.Equals(Txt_Password_Old.Text))
                //{
                    //帳號密碼
                    strSql = Frm_5_3_SqlFactory.SQL_Update_User();
                    Fun_InsertUpdateDataBase(strSql);

                if(!bolHaveFrameData)
                {
                    //畫面權限
                    strSql = Frm_5_3_SqlFactory.SQL_Insert_Frame();
                    Fun_InsertUpdateDataBase(strSql);
                }
                else
                {
                    //畫面權限
                    Fun_UpdateFrameSetup();
                }
                

                //}
                //else if (Txt_Password.Text.Equals(Txt_Password_Old.Text))
                //{
                //    EventLogHandler.Instance.EventPush_Message($"与旧密码相同，请重新输入!");
                //}
                //else if (!Txt_Password.Text.Equals(Txt_Password_Chk.Text))
                //{
                //    EventLogHandler.Instance.EventPush_Message($"密码与旧密码确认不同，請重新輸入!");

                //}
            }

            //狀態:Read 只顯示基本欄位
            strStatus = "Read";
            bolEditStatus = false;
            Fun_StatusToShowColumns(strStatus);
            //人員 欄位ReadOnly
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_User, true);
            //權限 RadioButton.Enabled = false;
            Fun_AuthorityEnabled(bolEditStatus);

            //帳號清單
            UserSetupHandler.Instance.UserID_List(Cob_UserID_Search);
            //
            Cob_UserID_Search.SelectedValue = Txt_UserID.Text ?? string.Empty;
            Fun_SelectUser();

            Btn_Save.Visible = false; //儲存
            Btn_Cancel.Visible = false; //取消
            Btn_EditPassWord.Visible = false;
            //Btn_New.Visible = true; //新增
            //Btn_Edit.Visible = true; //修改
            //Btn_Delete.Visible = true; //刪除
            //Txt_UserID.ReadOnly = true;
            //Txt_Password.ReadOnly = true;
            //Txt_Password_Old.Text = Txt_Password.Text;
            //Lbl_Password_Old_Title.Visible = false;
            //Txt_Password_Old.Visible = false;
            //Lbl_Password_Chk_Title.Visible = false;
            //Txt_Password_Chk.Visible = false;

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);
          
        }

        private void Fun_InsertUpdateDataBase(string strSql)
        {
            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "人员设定新增修改资料库"))
            {
                DialogHandler.Instance.Fun_DialogShowOk("人员设定新增/修改资料库失败", "人员设定新增修改资料库", 3);

                return;
            }
        }

        private bool Fun_InsertUpdateDataBase(string strFrame_ID ,string strSql)
        {
            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "人员画面权限新增修改资料库"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"人员画面权限[{strFrame_ID}] 新增/修改资料库失败", "人员画面权限新增修改资料库", 3);

                return false;
            }
            return true;
        }

        private bool Fun_ChackPassword()
        {
            //bool bolChk = false;
            #region New & Edit 都要檢查密碼

            //检查 密码
            if (string.IsNullOrEmpty(Txt_Password.Text.Trim()))
            {               
                DialogHandler.Instance.Fun_DialogShowOk("密码请勿为空白！", "人员密码檢查", 0);
                Txt_Password.Focus();
                return false;
            }

            //检查 确认密码
            if (string.IsNullOrEmpty(Txt_Password_Chk.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk("确认密码请勿为空白！", "人员密码檢查", 0);
                Txt_Password_Chk.Focus();
                return false;
            }

            //检查密码 是否相符
            if (Txt_Password.Text.Trim() != Txt_Password_Chk.Text.Trim())
            {
                DialogHandler.Instance.Fun_DialogShowOk("密码不相符！", "人员密码檢查", 0);
                Txt_Password_Chk.Focus();
                return false;
            }
            else
            {
                return true;
            }

            #endregion

            //return bolChk;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {           
            if (!bolEditStatus) return;

            if (!dtBeforeEdit.IsNull())
            {
                //人員資料 畫面權限 資料填入
                Fun_SetUserData(dtBeforeEdit);
                //修改
                Fun_SetBottonEnabled(Btn_Edit, true);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, true);
            }
            else
            {
                //人員欄位 清空            
                ReadOnlyHandler.Instance.ClearControl(CtrControlArray);
                Lbl_Updated.Text = "";
                Cob_Team.SelectedIndex = -1;
                Cob_Authority_Class.SelectedIndex = -1;
                //权限 全W
                Fun_AuthorityNewData();
                //修改
                Fun_SetBottonEnabled(Btn_Edit, false);
                //刪除
                Fun_SetBottonEnabled(Btn_Delete, false);
            }           

            //狀態:Read 只顯示基本欄位
            strStatus = "Read";
            bolEditStatus = false;
            Fun_StatusToShowColumns(strStatus);
            //人員 欄位ReadOnly
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_User, true);
            //權限 RadioButton.Enabled = false;
            Fun_AuthorityEnabled(bolEditStatus);
           
            Btn_Save.Visible = false; //儲存
            Btn_Cancel.Visible = false; //取消
            Btn_EditPassWord.Visible = false;

            //新增
            Fun_SetBottonEnabled(Btn_New, true);

            /*"Edit"*/
            //原 密碼 異動時 不唯讀
          
            //Txt_Password.BackColor = SystemColors.Window;// Color.LightYellow;            
            Lbl_Password_Title.Text = "密码"; 
            
            //Txt_Password_Old.BackColor = SystemColors.Window;// Color.LightYellow;
            //Txt_Password_Chk.BackColor = SystemColors.Window;// Color.LightYellow;
            Lbl_Password_Chk_Title.Text = "密码确认";            
           
        }

        /// <summary>
        /// 修改畫面權限
        /// </summary>
        private void Fun_UpdateFrameSetup()
        {
            string Frame_Function = "";
            string strSql = "";
           
            #region Frame

            #region '0-2'
            //string Frame_Function = rdoN0_2.Checked ? "N" : rdoR0_2.Checked ? "R" : rdoW0_2.Checked ? "W" : string.Empty;
            //string strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("0-2", Frame_Function);
            //Fun_InsertUpdateDataBase(strSql);
            #endregion

            #region '1-1'
            Frame_Function = (rdoN1_1.Checked) ? "N" : (rdoR1_1.Checked) ? "R" : (rdoW1_1.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("1-1", Frame_Function);
            if (!Fun_InsertUpdateDataBase("1-1", strSql))
                Fun_InsertUpdateDataBase("1-1", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("1-1", Frame_Function));
            #endregion

                #region '1-2'
            Frame_Function = (rdoN1_2.Checked) ? "N" : (rdoR1_2.Checked) ? "R" : (rdoW1_2.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("1-2", Frame_Function);
            if (!Fun_InsertUpdateDataBase("1-2", strSql))
                Fun_InsertUpdateDataBase("1-2", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("1-2", Frame_Function));
            #endregion

            #region '1-3'
            Frame_Function = (rdoN1_3.Checked) ? "N" : (rdoR1_3.Checked) ? "R" : (rdoW1_3.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("1-3", Frame_Function);
            if (!Fun_InsertUpdateDataBase("1-3", strSql))
                Fun_InsertUpdateDataBase("1-3", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("1-3", Frame_Function));
            #endregion

            #region '2-1'
            Frame_Function = (rdoN2_1.Checked) ? "N" : (rdoR2_1.Checked) ? "R" : (rdoW2_1.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("2-1", Frame_Function);
            if (!Fun_InsertUpdateDataBase("2-1", strSql))
                Fun_InsertUpdateDataBase("2-1", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("2-1", Frame_Function));
            #endregion

            #region '2-2'
            Frame_Function = (rdoN2_2.Checked) ? "N" : (rdoR2_2.Checked) ? "R" : (rdoW2_2.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("2-2", Frame_Function);
            if (!Fun_InsertUpdateDataBase("2-2", strSql))
                Fun_InsertUpdateDataBase("2-2", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("2-2", Frame_Function));
            #endregion

            #region '3-1'
            Frame_Function = (rdoN3_1.Checked) ? "N" : (rdoR3_1.Checked) ? "R" : (rdoW3_1.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("3-1", Frame_Function);
            if (!Fun_InsertUpdateDataBase("3-1", strSql))
                Fun_InsertUpdateDataBase("3-1", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("3-1", Frame_Function));
            #endregion

            #region '3-2'
            Frame_Function = (rdoN3_2.Checked) ? "N" : (rdoR3_2.Checked) ? "R" : (rdoW3_2.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("3-2", Frame_Function);
            if (!Fun_InsertUpdateDataBase("3-2", strSql))
                Fun_InsertUpdateDataBase("3-2", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("3-2", Frame_Function));
            #endregion

            #region '3-3'
            Frame_Function = (rdoN3_3.Checked) ? "N" : (rdoR3_3.Checked) ? "R" : (rdoW3_3.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("3-3", Frame_Function);
            if (!Fun_InsertUpdateDataBase("3-3", strSql))
                Fun_InsertUpdateDataBase("3-3", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("3-3", Frame_Function));
            #endregion

            //#region '3-4'
            //Frame_Function = (rdoN3_4.Checked) ? "N" : (rdoR3_4.Checked) ? "R" : (rdoW3_4.Checked) ? "W" : string.Empty;
            //strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("3-4", Frame_Function);
            //if (!Fun_InsertUpdateDataBase("3-4", strSql))
            //    Frm_5_3_SqlFactory.SQL_Insert_Frame_One("3-4", Frame_Function);
            //#endregion

            #region '4-1'
            Frame_Function = (rdoN4_1.Checked) ? "N" : (rdoR4_1.Checked) ? "R" : (rdoW4_1.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("4-1", Frame_Function);
            if (!Fun_InsertUpdateDataBase("4-1", strSql))
                Fun_InsertUpdateDataBase("4-1", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("4-1", Frame_Function));
            #endregion

            #region '4-2'
            Frame_Function = (rdoN4_2.Checked) ? "N" : (rdoR4_2.Checked) ? "R" : (rdoW4_2.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("4-2", Frame_Function);
            if (!Fun_InsertUpdateDataBase("4-2", strSql))
                Fun_InsertUpdateDataBase("4-2", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("4-2", Frame_Function));
            #endregion

            #region '4-3'
            Frame_Function = (rdoN4_3.Checked) ? "N" : (rdoR4_3.Checked) ? "R" : (rdoW4_3.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("4-3", Frame_Function);
            if (!Fun_InsertUpdateDataBase("4-3", strSql))
                Fun_InsertUpdateDataBase("4-3", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("4-3", Frame_Function));
            #endregion

            #region '5-1'
            Frame_Function = (rdoN5_1.Checked) ? "N" : (rdoR5_1.Checked) ? "R" : (rdoW5_1.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("5-1", Frame_Function);
            if (!Fun_InsertUpdateDataBase("5-1", strSql))
                Fun_InsertUpdateDataBase("5-1", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("5-1", Frame_Function));
            #endregion

            #region '5-2'
            Frame_Function = (rdoN5_2.Checked) ? "N" : (rdoR5_2.Checked) ? "R" : (rdoW5_2.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("5-2", Frame_Function);
            if (!Fun_InsertUpdateDataBase("5-2", strSql))
                Fun_InsertUpdateDataBase("5-2", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("5-2", Frame_Function));
            #endregion

            #region '5-3'
            Frame_Function = (rdoN5_3.Checked) ? "N" : (rdoR5_3.Checked) ? "R" : (rdoW5_3.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("5-3", Frame_Function);
            if (!Fun_InsertUpdateDataBase("5-3", strSql))
                Fun_InsertUpdateDataBase("5-3", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("5-3", Frame_Function));
            #endregion

            #region '5-4'
            Frame_Function = (rdoN5_4.Checked) ? "N" : (rdoR5_4.Checked) ? "R" : (rdoW5_4.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("5-4", Frame_Function);
            if (!Fun_InsertUpdateDataBase("5-4", strSql))
                Fun_InsertUpdateDataBase("5-4", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("5-4", Frame_Function));
            #endregion

            #region '5-5'
            Frame_Function = (rdoN5_5.Checked) ? "N" : (rdoR5_5.Checked) ? "R" : (rdoW5_5.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("5-5", Frame_Function);
            if (!Fun_InsertUpdateDataBase("5-5", strSql))
                Fun_InsertUpdateDataBase("5-5", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("5-5", Frame_Function));
            #endregion

            #region '5-6'
            Frame_Function = (rdoN5_6.Checked) ? "N" : (rdoR5_6.Checked) ? "R" : (rdoW5_6.Checked) ? "W" : string.Empty;
            strSql = Frm_5_3_SqlFactory.SQL_Update_Frame("5-6", Frame_Function);
            if (!Fun_InsertUpdateDataBase("5-6", strSql))
                Fun_InsertUpdateDataBase("5-6", Frm_5_3_SqlFactory.SQL_Insert_Frame_One("5-6", Frame_Function));
            #endregion

            #endregion

        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            if (!Txt_UserID.Text.IsEmpty() && !Txt_Password.Text.IsEmpty())
            {
                //新增
                Fun_SetBottonEnabled(Btn_New, false);
                //修改
                Fun_SetBottonEnabled(Btn_Edit, false);

                DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel($"確認是否刪除該帳號:{Txt_UserID.Text}?", "删除", Properties.Resources.dialogQuestion, 1);
                
                if (dialogR.Equals(DialogResult.OK))
                {
                    //刪除帳號
                    string strSql = Frm_5_3_SqlFactory.SQL_Delete_User();
                    Fun_InsertUpdateDataBase(strSql);

                    //刪除畫面權限
                    strSql = Frm_5_3_SqlFactory.SQL_Delete_Frame();
                    Fun_InsertUpdateDataBase(strSql);

                    //帳號清單
                    UserSetupHandler.Instance.UserID_List(Cob_UserID_Search);
                    //                    
                    Cob_UserID_Search.SelectedIndex = -1;

                    //人員 欄位清空
                    ReadOnlyHandler.Instance.ClearControl(CtrControlArray);
                    Cob_Authority_Class.SelectedIndex = -1;
                    Cob_Team.SelectedIndex = -1;
                    Lbl_Updated.Text = "";
                   
                   
                    //修改
                    Fun_SetBottonEnabled(Btn_Edit, false);
                    //刪除
                    Fun_SetBottonEnabled(Btn_Delete, false);
                }
                else
                {                   
                    //修改
                    Fun_SetBottonEnabled(Btn_Edit, true);
                    //刪除
                    Fun_SetBottonEnabled(Btn_Delete, true);
                }
                //新增
                Fun_SetBottonEnabled(Btn_New, true);
            }
            else
            {
                DialogHandler.Instance.Fun_DialogShowOk("请查询欲删除之账号", "删除人员设定", 0);
            }

            
        }

        private void Fun_AuthorityNewData()
        {
            foreach (Control Ctl in Grb_AuthoritySetting.Controls)
            {
                if (Ctl is GroupBox)
                {
                    foreach (Control Ctl2 in Ctl.Controls)
                    {
                        if (Ctl2 is RadioButton)
                        {
                            RadioButton rdb = Ctl2 as RadioButton;
                            string strCtl2_FrameFun = rdb.Name.ToString().Substring(3, 1);
                            if (strCtl2_FrameFun.ToUpper() == "N")
                            {
                                rdb.Checked = true;
                            }
                        }
                    }
                }
            }
        }

        private void Fun_AuthorityEnabled(bool bolEn)
        {           
            foreach (Control Ctl in Grb_AuthoritySetting.Controls)
            {
                if (Ctl is GroupBox)
                {
                    foreach (Control Ctl2 in Ctl.Controls)
                    {
                        if (Ctl2 is RadioButton)
                        {
                            RadioButton rdb = Ctl2 as RadioButton;
                            if (rdb.Checked)
                            {
                                rdb.Enabled = true;
                            }
                            else
                            {
                                rdb.Enabled = bolEn;
                            }
                        }
                    }
                }
            }
        }

        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //Color colorBack;
            btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

        private void Btn_EditPassWord_Click(object sender, EventArgs e)
        {
            /*"Read"*/
            if (strStatus == "Read") { return; }
            /*"New"*/
            if (strStatus == "New")
            {
                return;
            }
            else if (strStatus == "Edit")
            {
                /*"Edit"*/
                //原 密碼 異動時 不唯讀
                Txt_Password_Old.Enabled = true;
                //Txt_Password_Old.BackColor = SystemColors.Window;
                Txt_Password_Old.Text = "";

                Lbl_Password_Title.Text = "新密码";
                Txt_Password.Enabled = true;
                //Txt_Password.BackColor = SystemColors.Window;
                Txt_Password.Text = "";         

                Lbl_Password_Chk_Title.Text = "新密码确认";
                Txt_Password_Chk.Enabled = true;
                //Txt_Password_Chk.BackColor = SystemColors.Window;
                Txt_Password_Chk.Text = "";
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

    }
}
