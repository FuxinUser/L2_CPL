using static CPL1HMI.DataBaseTableFactory;

namespace CPL1HMI
{
    public class Frm_0_1_SqlFactory
    {

        /// <summary>
        /// 搜尋使用者權限
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static string SQL_Select_UserLoginInfo(string Password)
        {
            string strSql = $@"Select AuthorityData.* ,Frame.*
                               From [{nameof(TBL_AuthorityData)}] AuthorityData
                               Left Join [{nameof(TBL_AuthorityData_Frame)}] Frame 
                               On Frame.[{nameof(TBL_AuthorityData_Frame.User_ID)}] = AuthorityData.[{nameof(TBL_AuthorityData.User_ID)}]
                               Where AuthorityData.[{nameof(TBL_AuthorityData.User_ID)}] = '{UserSetupHandler.Instance.UserID}'
                               And AuthorityData.[{nameof(TBL_AuthorityData.Password)}] = '{Password}'";

            return strSql;
        }

        /// <summary>
        /// 使用者帳號下拉式選單
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_UserList()
        {
            string strSql = $@"Select [{nameof(TBL_AuthorityData.User_ID)}]  From [{nameof(TBL_AuthorityData)}] ";

            return strSql;
        }

    }
}
