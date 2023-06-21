using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblPaper;
using DBService.Repository.LookupTbSleeve;
using static CPL1HMI.DataBaseTableFactory;
using static DBService.Repository.ScheduleDelete_CoilReject_Code.ScheduleDelete_CoilReject_CodeEntity;

namespace CPL1HMI
{
    public class ComboBoxHandler_SqlFactory
    {

        /// <summary>
        /// 一般ComboBox選項
        /// </summary>
        /// <param name="_Type"></param>
        /// <returns></returns>
        public static string SQL_Select_ComboBoxItems(Cbo_Type _Type)
        {
            string strSql = $@" Select [{nameof(TBL_ComboBoxItems.Cbo_Value)}],[{nameof(TBL_ComboBoxItems.Cbo_Text)}]
                                From [{nameof(TBL_ComboBoxItems)}] 
                                Where [{nameof(TBL_ComboBoxItems.Cbo_Type)}] = {(int)_Type} 
                                Order by [{nameof(TBL_ComboBoxItems.Cbo_Index)}] asc";

            return strSql;
        }


        /// <summary>
        /// 套筒下拉式選項
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_SleeveComboBoxItems()
        {
            string strSql = $@"Select * From [{nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve)}] ORDER BY CAST({nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Code)} AS int ) ";

            return strSql;
        }


        /// <summary>
        /// 墊紙下拉式選項
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_PaperComboBoxItems()
        {
            string strSql = $@"Select * From [{nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper)}] ORDER BY CAST({nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Code)} AS int ) ";

            return strSql;
        }


        /// <summary>
        /// 下拉式選項說明
        /// </summary>
        /// <param name="_Type"></param>
        /// <returns></returns>
        public static string SQL_Select_ComboBoxSpare(Cbo_Type _Type)
        {
            string strSql = $@" Select [{nameof(TBL_ComboBoxItems.Cbo_Text)}]
                                From [{nameof(TBL_ComboBoxItems)}] 
                                Where [{nameof(TBL_ComboBoxItems.Cbo_Type)}] = {(int)_Type} 
                                Order by [{nameof(TBL_ComboBoxItems.Cbo_Index)}] asc";

            return strSql;
        }


        /// <summary>
        /// 排程刪除、鋼捲回退代碼下拉式選項
        /// </summary>
        /// <returns></returns>
        public static string SQLSelect_ScjeduleDelete_CoilReject_CodeCombBoxItems()
        {
            string strSql = $@"Select [{nameof(TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)}],
                                      [{nameof(TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)}]
                                 From [{nameof(TBL_ScheduleDelete_CoilReject_CodeDefinition)}]";

            return strSql;
        }


        /// <summary>
        /// 鋼種大類下拉式選項
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_Material_ComboBoxItems()
        {
            string strSql = $@" Select DISTINCT [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)}]
                        From [{nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension)}] ";

            return strSql;
        }

    }
}
