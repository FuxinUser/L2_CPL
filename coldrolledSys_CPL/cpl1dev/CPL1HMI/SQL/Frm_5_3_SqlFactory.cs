using System;
using static CPL1HMI.DataBaseTableFactory;

namespace CPL1HMI
{
    public class Frm_5_3_SqlFactory
    {


        #region --- Display ---

        /// <summary>
        /// 查詢帳號權限
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_User()
        {
            string strSql = $@"Select AuthorityData.* , 
                                      Frame.[{nameof(TBL_AuthorityData_Frame.Frame_ID)}],
                                      Frame.[{nameof(TBL_AuthorityData_Frame.Frame_Function)}]
                               From [{nameof(TBL_AuthorityData)}] AuthorityData
                               Left Join [{nameof(TBL_AuthorityData_Frame)}] Frame
                               On Frame.[{nameof(TBL_AuthorityData_Frame.User_ID)}] = AuthorityData.[{nameof(TBL_AuthorityData.User_ID)}]
                               Where AuthorityData.[{nameof(TBL_AuthorityData.User_ID)}] = '{PublicForms.UserSetup.Cob_UserID_Search.Text}'";
            return strSql;
        }

        /// <summary>
        /// 檢查帳號
        /// </summary>
        /// <returns></returns>
        public static string SQL_Check_User()
        {
            string strSql = $@"Select User_ID
                               From [{nameof(TBL_AuthorityData)}]
                               Where [{nameof(TBL_AuthorityData.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}'";
            return strSql;
        }

        /// <summary>
        /// 檢查權限
        /// </summary>
        /// <returns></returns>
        public static string SQL_Check_AuthorityData_Frame(string strUserID)
        {
            string strSql = $@"Select User_ID
                               From [{nameof(TBL_AuthorityData_Frame)}]
                               Where [{nameof(TBL_AuthorityData_Frame.User_ID)}] = '{strUserID}'";
            return strSql;
        }
        #endregion


        #region --- Funtion ---


        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_User()
        {
            string strSql = $@"Insert into [{nameof(TBL_AuthorityData)}]
                                          ([{nameof(TBL_AuthorityData.User_ID)}],
                                           [{nameof(TBL_AuthorityData.Password)}],
                                           [{nameof(TBL_AuthorityData.Department)}],
                                           [{nameof(TBL_AuthorityData.Team)}],
                                           [{nameof(TBL_AuthorityData.Authority_Class)}],
                                           [{nameof(TBL_AuthorityData.Create_DateTime)}])
                                     Values
                                          ('{PublicForms.UserSetup.Txt_UserID.Text}',
                                           '{PublicForms.UserSetup.Txt_Password.Text}',
                                           '{PublicForms.UserSetup.Txt_Department.Text}',
                                           '{PublicForms.UserSetup.Cob_Team.SelectedValue}',
                                           '{PublicForms.UserSetup.Cob_Authority_Class.SelectedValue}',
                                           '{GlobalVariableHandler.Instance.getTime}')";

            return strSql;
        }

        /// <summary>
        /// 新增權限
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_Frame_One(string strFrame_ID, string strFrame_Function)
        {
            string strSql = $@"Insert into [{nameof(TBL_AuthorityData_Frame)}]
                                          ([{nameof(TBL_AuthorityData_Frame.User_ID)}],
                                           [{nameof(TBL_AuthorityData_Frame.Frame_ID)}],
                                           [{nameof(TBL_AuthorityData_Frame.Frame_Function)}],
                                           [{nameof(TBL_AuthorityData_Frame.Create_DateTime)}]) Values ";           
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','{strFrame_ID}','{strFrame_Function}','{GlobalVariableHandler.Instance.getTime}')";

            return strSql;
        }

        /// <summary>
        /// 新增權限
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_Frame()
        {
            string strSql = $@"Insert into [{nameof(TBL_AuthorityData_Frame)}]
                                          ([{nameof(TBL_AuthorityData_Frame.User_ID)}],
                                           [{nameof(TBL_AuthorityData_Frame.Frame_ID)}],
                                           [{nameof(TBL_AuthorityData_Frame.Frame_Function)}],
                                           [{nameof(TBL_AuthorityData_Frame.Create_DateTime)}]) Values ";

            string Frame_Function = string.Empty;

            #region Frame

            #region '0-2'
            //Frame_Function = (PublicForms.UserSetup.rdoN0_2.Checked == true) ? Frame_Function = "N" :
            //    (PublicForms.UserSetup.rdoR0_2.Checked == true) ? Frame_Function = "R" :
            //    (PublicForms.UserSetup.rdoW0_2.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            //strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','0-2','{Frame_Function}'),";
            #endregion

            #region '1-1'
            Frame_Function = (PublicForms.UserSetup.rdoN1_1.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR1_1.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW1_1.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','1-1','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '1-2'
            Frame_Function = (PublicForms.UserSetup.rdoN1_2.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR1_2.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW1_2.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','1-2','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '1-3'
            Frame_Function = (PublicForms.UserSetup.rdoN1_3.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR1_3.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW1_3.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','1-3','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '2-1'
            Frame_Function = (PublicForms.UserSetup.rdoN2_1.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR2_1.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW2_1.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','2-1','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '2-2'
            Frame_Function = (PublicForms.UserSetup.rdoN2_2.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR2_2.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW2_2.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','2-2','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '3-1'
            Frame_Function = (PublicForms.UserSetup.rdoN3_1.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR3_1.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW3_1.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','3-1','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '3-2'
            Frame_Function = (PublicForms.UserSetup.rdoN3_2.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR3_2.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW3_2.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','3-2','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '3-3'
            Frame_Function = (PublicForms.UserSetup.rdoN3_3.Checked) ? "N" :
                (PublicForms.UserSetup.rdoR3_3.Checked) ? "R" :
                (PublicForms.UserSetup.rdoW3_3.Checked) ? "W" : string.Empty;
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','3-3','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '3-4'
            Frame_Function = (PublicForms.UserSetup.rdoN3_4.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR3_4.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW3_4.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','3-4','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '4-1'
            Frame_Function = (PublicForms.UserSetup.rdoN4_1.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR4_1.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW4_1.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','4-1','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '4-2'
            Frame_Function = (PublicForms.UserSetup.rdoN4_2.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR4_2.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW4_2.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','4-2','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '4-3'
            Frame_Function = (PublicForms.UserSetup.rdoN4_3.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR4_3.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW4_3.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','4-3','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '5-1'
            Frame_Function = (PublicForms.UserSetup.rdoN5_1.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR5_1.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW5_1.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','5-1','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '5-2'
            Frame_Function = (PublicForms.UserSetup.rdoN5_2.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR5_2.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW5_2.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','5-2','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '5-3'
            Frame_Function = (PublicForms.UserSetup.rdoN5_3.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR5_3.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW5_3.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','5-3','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '5-4'
            Frame_Function = (PublicForms.UserSetup.rdoN5_4.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR5_4.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW5_4.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','5-4','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '5-5'
            Frame_Function = (PublicForms.UserSetup.rdoN5_5.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR5_5.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW5_5.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','5-5','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}'),";
            #endregion

            #region '5-6'
            Frame_Function = (PublicForms.UserSetup.rdoN5_6.Checked == true) ? Frame_Function = "N" :
                (PublicForms.UserSetup.rdoR5_6.Checked == true) ? Frame_Function = "R" :
                (PublicForms.UserSetup.rdoW5_6.Checked == true) ? Frame_Function = "W" : Frame_Function = "";
            strSql += $@"('{PublicForms.UserSetup.Txt_UserID.Text}','5-6','{Frame_Function}','{GlobalVariableHandler.Instance.getTime}')";
            #endregion

            #endregion

            return strSql;
        }


        /// <summary>
        /// 修改帳號
        /// </summary>
        /// <returns></returns>
        public static string SQL_Update_User()
        {
            string strSql = $@"Update [{nameof(TBL_AuthorityData)}]
                                  Set [{nameof(TBL_AuthorityData.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}',
                                      [{nameof(TBL_AuthorityData.Password)}] = '{PublicForms.UserSetup.Txt_Password.Text}',
                                      [{nameof(TBL_AuthorityData.Department)}] = '{PublicForms.UserSetup.Txt_Department.Text}',
                                      [{nameof(TBL_AuthorityData.Team)}] = '{PublicForms.UserSetup.Cob_Team.SelectedValue}',
                                      [{nameof(TBL_AuthorityData.Authority_Class)}] = '{PublicForms.UserSetup.Cob_Authority_Class.SelectedValue}',
                                      [{nameof(TBL_AuthorityData.Create_DateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                                Where [{nameof(TBL_AuthorityData.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}'";

            return strSql;
        }


        /// <summary>
        /// 修改畫面權限
        /// </summary>
        /// <returns></returns>
        public static string SQL_Update_Frame(string Frame_ID, string Frame_Function)
        {
            string strSql = $@"Update [{nameof(TBL_AuthorityData_Frame)}]
                                  Set [{nameof(TBL_AuthorityData_Frame.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}',
                                      [{nameof(TBL_AuthorityData_Frame.Frame_ID)}] = '{Frame_ID}',
                                      [{nameof(TBL_AuthorityData_Frame.Frame_Function)}] = '{Frame_Function}',
                                      [{nameof(TBL_AuthorityData_Frame.Create_DateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                                Where [{nameof(TBL_AuthorityData_Frame.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}'
                                  And [{nameof(TBL_AuthorityData_Frame.Frame_ID)}] = '{Frame_ID}'";

            return strSql;
        }


        /// <summary>
        /// 刪除帳號
        /// </summary>
        /// <returns></returns>
        public static string SQL_Delete_User()
        {
            string strSql = $@"Delete From [{nameof(TBL_AuthorityData)}] Where [{nameof(TBL_AuthorityData.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}' ";

            return strSql;
        }


        /// <summary>
        /// 刪除畫面權限
        /// </summary>
        /// <returns></returns>
        public static string SQL_Delete_Frame()
        {
            string strSql = $@"Delete From [{nameof(TBL_AuthorityData_Frame)}] Where [{nameof(TBL_AuthorityData_Frame.User_ID)}] = '{PublicForms.UserSetup.Txt_UserID.Text}'";

            return strSql;
        }

        #endregion

    }
}
