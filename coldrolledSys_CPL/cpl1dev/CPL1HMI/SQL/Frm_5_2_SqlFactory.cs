using DBService.Repository.DelayLocation;
using DBService.Repository.DelayReasonCode;
using DBService.Repository.MaterialGrade;
using DBService.Repository.ScheduleDelete_CoilReject_Code;

namespace CPL1HMI
{
    public class Frm_5_2_SqlFactory
    {

        #region --- Display ---

        /// <summary>
        /// 根據使用者選擇哪一個資料表搜尋
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static string SQL_Select_Table(string TableName)
        {
            string strSql = $@"Select * From [{TableName}]";

            return strSql;
        }

        #endregion


        #region --- Funtion ---


        /// <summary>
        /// 新增排程刪除、鋼捲回退代碼
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_ScheduleDelete_CoilReject_Code()
        {
            string strSql = $@"Insert into [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition)}]
                                          ([{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo)}],
                                           [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)}],
                                           [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)}],
                                           [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.Create_UserID)}],
                                           [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.CreateTime)}])
                                    Values('{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[0].Value}',
                                           '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[1].Value}',
                                           '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[2].Value}',
                                           '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                           '{GlobalVariableHandler.Instance.getTime}')";

            return strSql;
        }


        /// <summary>
        /// 新增停復機記錄位置
        /// </summary>
        /// <param name="Index_No"></param>
        /// <returns></returns>
        public static string SQL_Insert_DelayLocation()
        {
            string strSql = $@"Insert into [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition)}]
                                          ([{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}],
                                           [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)}],
                                           [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)}],
                                           [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Create_UserID)}],
                                           [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.CreateTime)}])
                                    Values('{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[0].Value}',
                                           '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[1].Value}',
                                           '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[2].Value}',
                                           '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                           '{GlobalVariableHandler.Instance.getTime}')";

            return strSql;
        }


        /// <summary>
        /// 新增停機代碼
        /// </summary>
        /// <param name="Index_No"></param>
        /// <returns></returns>
        public static string SQL_Insert_DelayReasonCode()
        {
            string strSql = $@"Insert into [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition)}]
                                          ([{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Create_UserID)}],
                                           [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.CreateTime)}])
                                    Values('{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[0].Value}',
                                           '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[1].Value}',
                                           '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[2].Value}',
                                           '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[3].Value}',
                                           '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[4].Value}',
                                           '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[5].Value}',
                                           '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                           '{GlobalVariableHandler.Instance.getTime}')";

            return strSql;
        }


        /// <summary>
        /// 新增鋼種大類
        /// </summary>
        /// <returns></returns>
        public static string SQL_Insert_Material()
        {
            string strSql = $@"Insert into [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade)}]
                                          ([{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)}],
                                           [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade)}],
                                           [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.CreateTime)}])
                                    Values('{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[0].Value}',
                                           '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[1].Value}',
                                           '{GlobalVariableHandler.Instance.getTime}')";

            return strSql;
        }


        /// <summary>
        /// 修改排程刪除、鋼捲回退原因代碼
        /// </summary>
        /// <returns></returns>
        public static string SQL_Update_ScheduleDelete_CoilReject_Code()
        {
            string strSql = $@"Update [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition)}]
                                  Set [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[0].Value}',
                                      [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[1].Value}',
                                      [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[2].Value}',
                                      [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.Create_UserID)}] = '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                      [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.CreateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                                Where [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[0].Value}'
                                  And [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[1].Value}'
                                  And [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[2].Value}'";

            return strSql;
        }


        /// <summary>
        /// 修改停復機位置
        /// </summary>
        /// <returns></returns>
        public static string SQL_Update_DelayLocation()
        {
            string strSql = $@"Update [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition)}]
                                  Set [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[0].Value}',
                                      [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[1].Value}',
                                      [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[2].Value}',
                                      [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Create_UserID)}] = '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                      [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.CreateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                                Where [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[0].Value}'
                                  And [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[1].Value}'
                                  And [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[2].Value}'";

            return strSql;
        }


        /// <summary>
        /// 修改停機原因代碼
        /// </summary>
        /// <returns></returns>
        public static string SQL_Update_DelayReasonCode()
        {
            string strSql = $@"Update [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition)}]
                                  Set [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[0].Value}',
                                      [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[1].Value}',
                                      [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[2].Value}',
                                      [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[3].Value}',
                                      [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[4].Value}',
                                      [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[5].Value}',
                                      [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Create_UserID)}] = '{PublicForms.Main.Lbl_LoginUser.Text.Trim()}',
                                      [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.CreateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                                Where [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[0].Value}'
                                  And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[1].Value}'
                                  And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[2].Value}'
                                  And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[3].Value}'
                                  And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[4].Value}'
                                  And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[5].Value}'";

            return strSql;
        }


        /// <summary>
        /// 修改鋼種大類
        /// </summary>
        /// <returns></returns>
        public static string SQL_Update_Material()
        {
            string strSql = $@"Update [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade)}]
                                  Set [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[0].Value}',
                                      [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade)}] = '{PublicForms.CodeMaintain.Dgv_CurrentRow.Rows[0].Cells[1].Value}',
                                      [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.CreateTime)}] = '{GlobalVariableHandler.Instance.getTime}'
                                Where [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[0].Value}'
                                  And [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[1].Value}'";

            return strSql;
        }


        /// <summary>
        /// 刪除排程刪除、鋼捲回退原因代碼
        /// </summary>
        /// <returns></returns>
        public static string SQL_Delete_ScheduleDelete_CoilReject_Code()
        {
            string strSql = $@"Delete From [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition)}]
                                     Where [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_GroupNo)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[0].Value}'
                                       And [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Code)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[1].Value}'
                                       And [{nameof(ScheduleDelete_CoilReject_CodeEntity.TBL_ScheduleDelete_CoilReject_CodeDefinition.ScheduleDelete_CoilReject_Name)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[2].Value}'";

            return strSql;
        }


        /// <summary>
        /// 刪除停機位置
        /// </summary>
        /// <returns></returns>
        public static string SQL_Delete_DelayLocation()
        {
            string strSql = $@"Delete From [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition)}]
                                     Where [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Index_No)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[0].Value}'
                                       And [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationCode)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[1].Value}'
                                       And [{nameof(DelayLocationEntity.TBL_DelayLocation_Definition.Delay_LocationName)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[2].Value}'";

            return strSql;
        }


        /// <summary>
        /// 刪除停機原因代碼
        /// </summary>
        /// <returns></returns>
        public static string SQL_Delete_DelayReasonCode()
        {
            string strSql = $@"Delete From [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition)}]
                                     Where [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Index_No)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[0].Value}'
                                       And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonCode)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[1].Value}'
                                       And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_ReasonName)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[2].Value}'
                                       And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupCode)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[3].Value}'
                                       And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Delay_GroupName)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[4].Value}'
                                       And [{nameof(DelayReasonCodeEntity.TBL_DelayReasonCode_Definition.Responsible_Department)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[5].Value}'";

            return strSql;
        }


        /// <summary>
        /// 刪除鋼種大類
        /// </summary>
        /// <returns></returns>
        public static string SQL_Delete_Material()
        {
            string strSql = $@"Delete From [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade)}]
                                     Where [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.St_No)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[0].Value}'
                                       And [{nameof(MaterialGradeEntity.TBL_SteelNoToMaterialGrade.Material_Grade)}] = '{PublicForms.CodeMaintain.Dgv_Table.CurrentRow.Cells[1].Value}'";

            return strSql;
        }

        #endregion

    }
}
