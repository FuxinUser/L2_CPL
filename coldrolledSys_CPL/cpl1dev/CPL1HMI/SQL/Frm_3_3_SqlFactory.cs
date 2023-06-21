using DBService.Repository.LineStatus;
using DBService.Repository.PDO;

namespace CPL1HMI
{
    public class Frm_3_3_SqlFactory
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_ProcessData(string Coil_ID)
        {
            string strSql = $@"Select Preset.*
                        From [{nameof(PDOEntity.TBL_PDO)}] pdo
                        RIGHT join [{nameof(ProcessDataEntity.TBL_ProcessData)}] Preset
                        On Preset.[{nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime)}] Between pdo.[{nameof(PDOEntity.TBL_PDO.StartTime)}] and pdo.[{nameof(PDOEntity.TBL_PDO.FinishTime)}]
                        Where pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = '{Coil_ID}'";

            return strSql;
        }
    }
}
