using DBService.Repository.CoilScheduleDelete;

namespace CPL1HMI
{
    public class Frm_1_3_SqlFactory
    {

        #region --- Display ---

        /// <summary>
        /// 搜尋刪除排程記錄
        /// </summary>
        /// <returns></returns>
        public static string SQL_Select_ScheduleDeleteRecord()
        {
            string strSql = $@"Select * From [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete)}] ";

            int CheckedCount = 0;

            //鋼捲號
            if (PublicForms.DeleteScheduleRecord.Chk_Coil.Checked.Equals(true))
            {
                strSql += $@"Where [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Coil_ID)}] = '{PublicForms.DeleteScheduleRecord.Cob_Entry_Coil_No.Text}'";

                CheckedCount += 1;
            }

            //刪除原因
            if (PublicForms.DeleteScheduleRecord.Chk_DeleteCode.Checked.Equals(true) && CheckedCount > 0)
            {
                strSql += $@"And [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.ReasonCode)}] = '{PublicForms.DeleteScheduleRecord.Cob_DeleteCode.SelectedValue}'";

                CheckedCount += 1;
            }
            else if (PublicForms.DeleteScheduleRecord.Chk_DeleteCode.Checked.Equals(true) && CheckedCount == 0)
            {
                strSql += $@"Where [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.ReasonCode)}] = '{PublicForms.DeleteScheduleRecord.Cob_DeleteCode.SelectedValue}'";

                CheckedCount += 1;
            }

            //時間區間
            if (PublicForms.DeleteScheduleRecord.Chk_Time.Checked.Equals(true) && CheckedCount > 0)
            {
                strSql += $@"And [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CreateTime)}] Between '{PublicForms.DeleteScheduleRecord.Dtp_Start_Time.Value:yyyy-MM-dd HH}:00:00' and '{PublicForms.DeleteScheduleRecord.Dtp__Finish_Time.Value:yyyy-MM-dd HH}:59:59'";

                CheckedCount += 1;
            }
            else if (PublicForms.DeleteScheduleRecord.Chk_Time.Checked.Equals(true) && CheckedCount == 0)
            {
                strSql += $@" Where [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CreateTime)}] Between '{PublicForms.DeleteScheduleRecord.Dtp_Start_Time.Value:yyyy-MM-dd HH}:00:00' and '{PublicForms.DeleteScheduleRecord.Dtp__Finish_Time.Value:yyyy-MM-dd HH}:59:59'";

                CheckedCount += 1;
            }

            //删除记录类型
            if (PublicForms.DeleteScheduleRecord.Chk_Remarks_Type.Checked.Equals(true) && CheckedCount > 0)
            {
                strSql += $@"And [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Remarks)}] = '{PublicForms.DeleteScheduleRecord.Cob_Remarks_Type.Text}'";

                CheckedCount += 1;
            }
            else if (PublicForms.DeleteScheduleRecord.Chk_Remarks_Type.Checked.Equals(true) && CheckedCount == 0)
            {
                strSql += $@"Where [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Remarks)}] = '{PublicForms.DeleteScheduleRecord.Cob_Remarks_Type.Text}'";

                CheckedCount += 1;
            }

            ////使用者
            //if (PublicForms.DeleteScheduleRecord.Chk_User.Checked.Equals(true) && CheckedCount > 0)
            //{
            //    strSql += $@" And [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.OperatorId)}] = '{PublicForms.DeleteScheduleRecord.Cob_User.Text}' ";

            //    CheckedCount += 1;
            //}
            //else if (PublicForms.DeleteScheduleRecord.Chk_User.Checked.Equals(true) && CheckedCount == 0)
            //{
            //    strSql += $@" Where [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.OperatorId)}] = '{PublicForms.DeleteScheduleRecord.Cob_User.Text}' ";

            //    CheckedCount += 1;
            //}

            strSql += $@" Order by [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.CreateTime)}] desc";


            return strSql;
        }


        #endregion


        #region --- ComboBoxItems ---
        public static string SQL_Select_ScheduleDeleteRecordCoilList()
        {
            string strSql = $@"Select [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Coil_ID)}] From [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete)}]";

            return strSql;
        }

        #endregion


        #region --- 保留 ---
        public static string SQL_Update_ScheduleDeleteRecordSpare()
        {
            string strSql = $@"Update [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete)}] Set
                                      [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Remarks)}] = N'{PublicForms.DeleteScheduleRecord.Txt_Spare.Text}'
                                Where [{nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Coil_ID)}] = '{PublicForms.DeleteScheduleRecord.Txt_Coil.Text}'";

            return strSql;
        }

        #endregion

    }
}
