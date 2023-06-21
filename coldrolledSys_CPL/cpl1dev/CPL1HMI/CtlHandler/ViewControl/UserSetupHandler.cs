using System.Data;
using static CPL1HMI.DataBaseTableFactory;
using System.Windows.Forms;


namespace CPL1HMI
{
    public class UserSetupHandler
    {

        // 帳號、密碼、權限等級
        public string UserID;
        public string Authority_Class;
        public string Authority_Class_Show;
        public string Frm_0_2, Frm_1_1, Frm_1_2,Frm_1_3, Frm_2_1, Frm_2_2, Frm_3_1, Frm_3_2, Frm_3_3, Frm_3_4, Frm_4_1, Frm_4_2,Frm_4_3, Frm_5_1, Frm_5_2, Frm_5_3, Frm_5_4, Frm_5_5, Frm_5_6;


        // 權限等級
        public enum AuthorityClass
        {
            Administrator,
            Manager,
            Operator
        }

        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly UserSetupHandler INSTANCE = new UserSetupHandler();
        }

        public static UserSetupHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 搜尋帳號資料
        /// </summary>
        public void Fun_UserLogin(string getPassword)
        {
            //紀錄登入帳號密碼
            UserID = PublicForms.Login.Cob_User.Text;
            
            string strSql = Frm_0_1_SqlFactory.SQL_Select_UserLoginInfo(getPassword);
            DataTable dtGetUser = DataAccess.Fun_SelectDate(strSql, "登入查詢");
         

            // 登入成功
            if (!dtGetUser.IsNull())
            {
                PublicForms.Login.bolLogin = true;

                // 給預設值 都給N ,避免該使用者沒建到某畫面權限,後續執行會 Exception
                Frm_0_2 = Frm_1_1 = Frm_1_2 = Frm_1_3 = Frm_2_1 = Frm_2_2 = Frm_3_1 = Frm_3_2 = Frm_3_3 = Frm_3_4 = Frm_4_1 = Frm_4_2 = Frm_4_3 = Frm_5_1 = Frm_5_2 = Frm_5_3 = Frm_5_4 = Frm_5_5 = Frm_5_6 = "N";

                Authority_Class = dtGetUser.Rows[0][nameof(TBL_AuthorityData.Authority_Class)].ToString();
                Authority_Class_Show = Fun_Authority_ClassChangeShow(Authority_Class);

                for (int Index = 0; Index < dtGetUser.Rows.Count; Index++)
                {
                    switch (dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_ID)].ToString().Trim())
                    {
                        case "0-2":
                            Frm_0_2 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;

                        case "1-1":
                            Frm_1_1 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "1-2":
                            Frm_1_2 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "1-3":
                            Frm_1_3 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "2-1":
                            Frm_2_1 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "2-2":
                            Frm_2_2 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "3-1":
                            Frm_3_1 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "3-2":
                            Frm_3_2 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "3-3":
                            Frm_3_3 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "3-4":
                            Frm_3_4 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "4-1":
                            Frm_4_1 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "4-2":
                            Frm_4_2 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "4-3":
                            Frm_4_3 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "5-1":
                            Frm_5_1 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "5-2":
                            Frm_5_2 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "5-3":
                            Frm_5_3 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "5-4":
                            Frm_5_4 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "5-5":
                            Frm_5_5 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        case "5-6":
                            Frm_5_6 = dtGetUser.Rows[Index][nameof(TBL_AuthorityData_Frame.Frame_Function)].ToString();
                            break;
                        default:
                            break;
                    }
                }

            }
            // 登入失敗or查無帳號
            else if (dtGetUser.IsNull())
            {
                PublicForms.Login.bolLogin = false;
                return;
            }
        }

        private string Fun_Authority_ClassChangeShow(string strKey)
        {
            string strShow;
            switch (strKey)
            {
                case "1":
                    strShow = "Administrator";
                    break;
                case "2":
                    strShow = "Manager";
                    break;
                case "3":
                    strShow = "Operator";
                    break;
                default:
                    strShow = "";
                    break;
            }
            return strShow;
        }

        /// <summary>
        /// 設定權限禁止畫面
        /// </summary>
        public void Fun_SetupBanForm_Menu()
        {
            #region 'Menu'
            if (Frm_0_2.Equals("N"))
                PublicForms.Main.tsMenuItem_0.Enabled = false;
            else
                PublicForms.Main.tsMenuItem_0.Enabled = true;
            #endregion

            #region '1-1'
            if (Frm_1_1.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_1_1.Enabled = false;
                PublicForms.Menu.tsMenuItem_1_1.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_1_1.Enabled = true;
                PublicForms.Menu.tsMenuItem_1_1.Enabled = true;
            }
            #endregion

            #region '1-2'
            if (Frm_1_2.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_1_2.Enabled = false;
                PublicForms.Menu.tsMenuItem_1_2.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_1_2.Enabled = true;
                PublicForms.Menu.tsMenuItem_1_2.Enabled = true;
            }
            #endregion

            #region '1-3'
            if (Frm_1_3.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_1_3.Enabled = false;
                PublicForms.Menu.tsMenuItem_1_3.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_1_3.Enabled = true;
                PublicForms.Menu.tsMenuItem_1_3.Enabled = true;
            }
            #endregion

            #region '2-1'
            if (Frm_2_1.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_2_1.Enabled = false;
                PublicForms.Menu.tsMenuItem_2_1.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_2_1.Enabled = true;
                PublicForms.Menu.tsMenuItem_2_1.Enabled = true;
            }
            #endregion

            #region '2-2'
            if (Frm_2_2.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_2_2.Enabled = false;
                PublicForms.Menu.tsMenuItem_2_2.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_2_2.Enabled = true;
                PublicForms.Menu.tsMenuItem_2_2.Enabled = true;
            }
            #endregion

            #region '3-1'
            if (Frm_3_1.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_3_1.Enabled = false;
                PublicForms.Menu.tsMenuItem_3_1.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_3_1.Enabled = true;
                PublicForms.Menu.tsMenuItem_3_1.Enabled = true;
            }
            #endregion

            #region '3-2'
            if (Frm_3_2.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_3_2.Enabled = false;
                PublicForms.Menu.tsMenuItem_3_2.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_3_2.Enabled = true;
                PublicForms.Menu.tsMenuItem_3_2.Enabled = true;
            }
            #endregion

            #region '3-3'
            if (Frm_3_3.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_3_3.Enabled = false;
                PublicForms.Menu.tsMenuItem_3_3.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_3_3.Enabled = true;
                PublicForms.Menu.tsMenuItem_3_3.Enabled = true;
            }
            #endregion

            #region '3-4'
            if (Frm_3_4.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_3_4.Enabled = false;
                PublicForms.Menu.tsMenuItem_3_4.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_3_4.Enabled = true;
                PublicForms.Menu.tsMenuItem_3_4.Enabled = true;
            }
            #endregion

            #region '4-1'
            if (Frm_4_1.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_4_1.Enabled = false;
                PublicForms.Menu.tsMenuItem_4_1.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_4_1.Enabled = true;
                PublicForms.Menu.tsMenuItem_4_1.Enabled = true;
            }
            #endregion

            #region '4-2'
            if (Frm_4_2.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_4_2.Enabled = false;
                PublicForms.Menu.tsMenuItem_4_2.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_4_2.Enabled = true;
                PublicForms.Menu.tsMenuItem_4_2.Enabled = true;
            }
            #endregion

            #region '4-3'
            if (Frm_4_3.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_4_3.Enabled = false;
                PublicForms.Menu.tsMenuItem_4_3.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_4_3.Enabled = true;
                PublicForms.Menu.tsMenuItem_4_3.Enabled = true;
            }
            #endregion

            #region '5-1'
            if (Frm_5_1.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_5_1.Enabled = false;
                PublicForms.Menu.tsMenuItem_5_1.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_5_1.Enabled = true;
                PublicForms.Menu.tsMenuItem_5_1.Enabled = true;
            }
            #endregion

            #region '5-2'
            if (Frm_5_2.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_5_2.Enabled = false;
                PublicForms.Menu.tsMenuItem_5_2.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_5_2.Enabled = true;
                PublicForms.Menu.tsMenuItem_5_2.Enabled = true;
            }
            #endregion

            #region '5-3'
            if (Frm_5_3.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_5_3.Enabled = false;
                PublicForms.Menu.tsMenuItem_5_3.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_5_3.Enabled = true;
                PublicForms.Menu.tsMenuItem_5_3.Enabled = true;
            }
            #endregion

            #region '5-4'
            if (Frm_5_4.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_5_4.Enabled = false;
                PublicForms.Menu.tsMenuItem_5_4.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_5_4.Enabled = true;
                PublicForms.Menu.tsMenuItem_5_4.Enabled = true;
            }
            #endregion

            #region '5-5'
            if (Frm_5_5.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_5_5.Enabled = false;
                PublicForms.Menu.tsMenuItem_5_5.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_5_5.Enabled = true;
                PublicForms.Menu.tsMenuItem_5_5.Enabled = true;
            }
            #endregion

            #region '5-6'
            if (Frm_5_6.Equals("N"))
            {
                PublicForms.Main.tsMenuItem_5_6.Enabled = false;
                PublicForms.Menu.tsMenuItem_5_6.Enabled = false;
            }
            else
            {
                PublicForms.Main.tsMenuItem_5_6.Enabled = true;
                PublicForms.Menu.tsMenuItem_5_6.Enabled = true;
            }
            #endregion
        }

      
        /// <summary>
        /// 帳號清單
        /// </summary>
        public void UserID_List(ComboBox cboUser)
        {
            string strSql = Frm_0_1_SqlFactory.SQL_Select_UserList();

            DataTable dtUserList = DataAccess.Fun_SelectDate(strSql, "登入帳號清單查詢");

            if (dtUserList.IsNull())
            {
                cboUser.DataSource = null;
                return;
            }

            cboUser.DisplayMember = nameof(TBL_AuthorityData.User_ID);
            cboUser.ValueMember = nameof(TBL_AuthorityData.User_ID);
            cboUser.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUser.DataSource = dtUserList;
        }


        /// <summary>
        /// 畫面可讀不可寫設定
        /// </summary>
        /// <param name="ControlList"></param>
        /// <param name="FrameFuntion"></param>
        public void Fun_SetControlAuthority(Control[] ControlList, string FrameFuntion, Form Form = null)
        {
            bool bolEnable = true;

            if (FrameFuntion.Equals("R"))
            {
                bolEnable = false;
                if(PublicForms.Tracking != null && Form != null)
                    if (Form.Name.Equals("frm_2_1_Tracking"))
                        PublicForms.Tracking.AckPDIToolStripMenuItem.Enabled = bolEnable;
            }

            foreach (Control ctr in ControlList)
            {
                ctr.Enabled = bolEnable;
            }
        }

    }
}
