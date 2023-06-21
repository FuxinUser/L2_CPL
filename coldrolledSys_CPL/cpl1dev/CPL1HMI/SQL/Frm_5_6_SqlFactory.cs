using static CPL1HMI.DataBaseTableFactory;

namespace CPL1HMI
{
    public class Frm_5_6_SqlFactory
    {

        /// <summary>
        /// 搜尋連線狀態
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_SystemStatus()
        {
            string strSql = $@"Select * From [{nameof(TBL_ConnectionStatus)}]";

            return strSql;
        }

    }
}
