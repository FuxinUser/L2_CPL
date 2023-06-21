using System.Data;
using static DBService.Repository.PDO.PDOEntity;
using static DBService.Repository.ProcessDataCT.ProcessDataCTEntity;

namespace CPL1HMI
{
    public class TrendDataProc
    {
        public TrendDataProc()
        {
        }

        public DataTable TransToCharData(DataRow dr, string colName)
        {
            var dt = new DataTable();
            dt.Columns.Add("passpos");
            dt.Columns.Add(colName);

            var items = dr["DataString"].ToString().Split(';');

            foreach (var item in items)
            {
                var vals = item.Split(':');

                dt.Rows.Add(new object[] { vals[0], vals[1] });
            }

            return dt;
        }

        public DataTable GetProcessDataCT(string exCoilNo)
        {
            var sql = $@"SELECT Ct.* 
                         FROM {nameof(TBL_PDO)} pdo 
                         RIGHT JOIN {nameof(TBL_ProcessDataCT)} ct 
                             CONVERT(varchar, pdo.{nameof(TBL_PDO.StartTime)}, 120) = 
                                 SUBSTRING(ct.{nameof(TBL_ProcessDataCT.BeginDate)}, 1, 4) + '-' + SUBSTRING(ct.{nameof(TBL_ProcessDataCT.BeginDate)}, 5, 2) + '-' + SUBSTRING(ct.{nameof(TBL_ProcessDataCT.BeginDate)}, 7, 2) + ' ' + 
                                 SUBSTRING(ct.{nameof(TBL_ProcessDataCT.BeginTime)}, 1, 2) + ':' + SUBSTRING(ct.{nameof(TBL_ProcessDataCT.BeginTime)}, 3, 2) + ':' + SUBSTRING(ct.{nameof(TBL_ProcessDataCT.BeginTime)}, 5, 2) AND 
                             CONVERT(varchar, pdo.{nameof(TBL_PDO.FinishTime)}, 120) = 
                                 SUBSTRING(ct.{nameof(TBL_ProcessDataCT.StopDate)}, 1, 4) + '-' + SUBSTRING(ct.{nameof(TBL_ProcessDataCT.StopDate)}, 5, 2) + '-' + SUBSTRING(ct.{nameof(TBL_ProcessDataCT.StopDate)}, 7, 2) + ' ' + 
                                 SUBSTRING(ct.{nameof(TBL_ProcessDataCT.StopTime)}, 1, 2) + ':' + SUBSTRING(ct.{nameof(TBL_ProcessDataCT.StopTime)}, 3, 2) + ':' + SUBSTRING(ct.{nameof(TBL_ProcessDataCT.StopTime)}, 5, 2) 
                         WHERE {nameof(TBL_PDO.Out_Coil_ID)} = '{exCoilNo}' ";

            var dt = new DataTable();

            return dt;
        }
    }
}
