using DBService.Repository.PDI;
using DBService.Repository.PDO;

namespace CPL1HMI
{
    public class Frm_3_1_SqlFactory
    {
        /// <summary>
        /// PDO列表
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_PDOList()
        {
            string strSql = $@" Select pdo.* 
                                     , CONVERT(VARCHAR(25), [{ nameof(PDOEntity.TBL_PDO.FinishTime)}], 121) as FinishTime_Str 
                           From [{nameof(PDOEntity.TBL_PDO)}] pdo 
                          
                           Where (pdo.[{nameof(PDOEntity.TBL_PDO.FinishTime)}] <> ''
                              Or pdo.[{nameof(PDOEntity.TBL_PDO.FinishTime)}] <> null)";

//            Left join[{ nameof(CoilPDIEntity.TBL_PDI)}] pdi
//On pdo.[{ nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = pdi.[{ nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)}]
            return strSql;
        }

        /// <summary>
        /// ComboBoxItems
        /// </summary>
        /// <param name="Columns"></param>
        /// <param name="bolPDI"></param>
        /// <returns></returns>
        public static string SQL_Select_ComboBoxItems(string Columns, bool bolPDI = false)
        {
            string strSql = string.Empty;

            if (bolPDI == false)
            {
                strSql = $@" Select distinct [{Columns}] 
                                From [{nameof(PDOEntity.TBL_PDO)}] 
                                Where [{nameof(PDOEntity.TBL_PDO.FinishTime)}] <> '' 
                                   Or [{nameof(PDOEntity.TBL_PDO.FinishTime)}] <> null";
            }
            else if (bolPDI == true)
            {
                strSql = $" select distinct pdi.[{Columns}] from [{nameof(PDOEntity.TBL_PDO)}] pdo left join [{nameof(CoilPDIEntity.TBL_PDI)}] pdi on pdo.[{nameof(PDOEntity.TBL_PDO.Out_Coil_ID)}] = pdi.[{nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)}] ";
            }

            return strSql;
        }

    }
}
