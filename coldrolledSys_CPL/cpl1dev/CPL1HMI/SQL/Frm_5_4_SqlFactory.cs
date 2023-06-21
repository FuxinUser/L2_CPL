using DBService.Repository.WorkSchedule;
using System;
using System.Data;
using System.Text;

namespace CPL1HMI
{
    public class Frm_5_4_SqlFactory
    {


        public static string Frm_5_4_SelectShift(string strDate)
        {
            string strSql = $@"Select * 
                                From [{nameof(WorkScheduleEntity.TBL_WorkSchedule)}] 
                                Where [{nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftDate)}] like '{strDate}%'";
            return strSql;
        }


        public static string Frm_5_4_DeleteOldWorkSchedule(string strDate)
        {
            string strSql = $@"Delete From {nameof(WorkScheduleEntity.TBL_WorkSchedule)}
                                     Where {nameof(WorkScheduleEntity.TBL_WorkSchedule.ShiftDate)} Like '{strDate}%' ";
            return strSql;
        }


        /// <summary>
        /// 將DataTable組成InsertSql字串
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="strTableName">資料表名</param>
        /// <returns>Sql字串</returns>
        public static string Fun_GetInsertSqlFromDataTable(DataTable dt, string strTableName)
        {

            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine("BEGIN");

            foreach (DataRow dr in dt.Rows)
            {
                string strInsert = Fun_GetInsertSqlFromDataRow(dr, strTableName);

                sbSql.AppendLine(strInsert);
            }

            sbSql.AppendLine("END;");

            return sbSql.ToString();
        }



        /// <summary>
        /// 將DataRow組成InsertSql字串
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="strTableName">資料表名</param>
        /// <returns>字串</returns>
        private static string Fun_GetInsertSqlFromDataRow(DataRow dr, string strTableName)
        {
            try
            {
                // 取得dr欄位名稱.
                string strColumns = Fun_GetColumnNameFromDataRow(dr);
                // 取得dr欄位內容.
                string strValues = Fun_GetColumnValueFromDataRow(dr);

                StringBuilder sbSql = new StringBuilder();

                sbSql.AppendLine("INSERT INTO " + strTableName);
                sbSql.AppendLine("(");
                sbSql.AppendLine(strColumns);
                sbSql.AppendLine(") VALUES (");
                sbSql.AppendLine(strValues);
                sbSql.AppendLine("); ");

                return sbSql.ToString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 取得DataRow的ColumnName組成字串
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <returns>字串</returns>
        private static string Fun_GetColumnNameFromDataRow(DataRow dr)
        {

            DataTable dt = dr.Table;

            StringBuilder sb = new StringBuilder();

            foreach (DataColumn dc in dt.Columns)
            {
                //if (dc.DataType == typeof(int)) { continue; }
                sb.Append(" \"" + dc.ColumnName + "\", ");
            }

            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();

        }

        /// <summary>
        /// 取得DataRow的ColumnValue組成字串
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <returns>字串</returns>
        private static string Fun_GetColumnValueFromDataRow(DataRow dr)
        {

            DataTable dt = dr.Table;

            StringBuilder sb = new StringBuilder();

            foreach (DataColumn dc in dt.Columns)
            {
                //if (dc.DataType == typeof(int)) { continue; }
                if (dc.DataType.Name == "DateTime")
                {
                    DateTime dtTime = Convert.ToDateTime(dr[dc.ColumnName]);
                    string strDtime = dtTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    sb.Append("N'" + strDtime + "', ");
                }
                else
                {
                    sb.Append("N'" + dr[dc.ColumnName] + "', ");
                }

            }


            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();

        }

    }
}
