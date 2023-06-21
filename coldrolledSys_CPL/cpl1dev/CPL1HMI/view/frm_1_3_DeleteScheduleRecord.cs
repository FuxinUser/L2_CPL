using DBService.Repository.CoilScheduleDelete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class Frm_1_3_DeleteScheduleRecord : Form
    {
        DataTable dtGetRecord;
        //語系
        private LanguageHandler LanguageHand;
        public Frm_1_3_DeleteScheduleRecord()
        {
            InitializeComponent();
        }
        private void Frm_1_3_DeleteScheduleRecord_Load(object sender, EventArgs e)
        {
            //LanguageHandler.Instance.Fun_CurrentLanguage(this.Name);

            if (PublicForms.DeleteScheduleRecord == null) PublicForms.DeleteScheduleRecord = this;

            Control[] Frm_1_3_Control = new Control[] {
                Btn_Update
            };
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_1_3_Control, UserSetupHandler.Instance.Frm_1_3);

            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_DeleteCode(Cob_DeleteCode);
            Fun_InitComboBox_Cob_Remarks_Type();
           // ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_DeleteUser(Cob_User);
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_DeleteRecordCoilList(Cob_Entry_Coil_No);


            Dtp_Start_Time.Value = DateTime.Now.AddHours(-1);
            Dtp__Finish_Time.Value = DateTime.Now.AddHours(1);

            //搜尋刪除記錄
            try
            {
                Fun_SelectDeleteRecord();
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"删除记录查询资料库失败:{ex}", "删除记录查询", 3);

                EventLogHandler.Instance.LogDebug("1-3", $"删除记录", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"刪除記錄查詢失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"刪除記錄查詢失敗:{ex}");
                return;
            }

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);

        }
        private void Fun_InitComboBox_Cob_Remarks_Type()
        {
            //ComboBox Data 
            Dictionary<string, string> dicLUT = new Dictionary<string, string>();
            dicLUT.Add("单卷删除", "单卷删除");//

            dicLUT.Add("整计划删除", "整计划删除");//

            dicLUT.Add("回退记录", "回退记录");//

            dicLUT.Add("已产出PDO", "已产出PDO");//

            DataTable dtLUT = Fun_DictionaryToDataTable(dicLUT);
            //Combobox取得资料 
            ComboBoxIndexHandler.Instance.Fun_CobDataFromTable(Cob_Remarks_Type, "SelectKey", "SelectShow", dtLUT, false, false);


        }

        public DataTable Fun_DictionaryToDataTable(Dictionary<string, string> dicAry)
        {
            DataTable dt = new DataTable();
            DataRow dr;

            // 建立欄位
            dt.Columns.Add("SelectKey", typeof(string));
            dt.Columns.Add("SelectShow", typeof(string));

            // 新增資料到DataTable
            foreach (KeyValuePair<string, string> item in dicAry)
            {
                dr = dt.NewRow();
                dr["SelectKey"] = item.Key;
                dr["SelectShow"] = item.Value;
                dt.Rows.Add(dr);
            }

            return dt;
        }
        /// <summary>
        /// 搜寻删除记录
        /// </summary>
        /// <param name="bolCondition"></param>
        public void Fun_SelectDeleteRecord()
        {
            if (Chk_Time.Checked)
            {
                if (Dtp_Start_Time.DateTimeRangeIsFail(Dtp__Finish_Time.Value))
                {
                    EventLogHandler.Instance.EventPush_Message($"日期区间起讫时间不正确，请重新确认");
                    return;
                }
            }

            string strSql = Frm_1_3_SqlFactory.SQL_Select_ScheduleDeleteRecord();
            dtGetRecord = DataAccess.Fun_SelectDate(strSql, "删除排程记录");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_DeleteSchedule, dtGetRecord);
            Frm_1_3_ColumnsHandler.Instance.Frm_1_3_ScheduleDeleteRecord(Dgv_DeleteSchedule);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_DeleteSchedule);

            if (dtGetRecord.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("查询无资料", "删除排程记录查询", 0);
            }
            else
            {
                EventLogHandler.Instance.EventPush_Message("查询完成");
                PublicComm.ClientLog.Info($"DataGridView删除记录清單");
            }
        }

        
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Update_Click(object sender, EventArgs e)
        {

            if (dtGetRecord.IsNull()) return;
            if (Dgv_DeleteSchedule.CurrentIsNull()) return;

            DataRow dr = dtGetRecord.Rows[Dgv_DeleteSchedule.CurrentRow.Index];

            Txt_Coil.Text = dr[nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Coil_ID)].ToString() ?? string.Empty;
            Txt_Spare.Text = dr[nameof(CoilScheduleDeleteEntity.TBL_CoilScheduleDelete.Remarks)].ToString() ?? string.Empty;

            Btn_Save.Visible = true;
            Btn_Cancel.Visible = true;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Txt_Coil.Text = Txt_Spare.Text = string.Empty;
        }
        /// <summary>
        /// 修改备注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            string strSql = Frm_1_3_SqlFactory.SQL_Update_ScheduleDeleteRecordSpare();

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "修改删除排程记录备注"))
            {
                EventLogHandler.Instance.EventPush_Message($"备注修改失败");
                return;
            }

            EventLogHandler.Instance.LogInfo("1-3", "修改删除排程记录备注", $"修改删除排程记录备注成功");
            EventLogHandler.Instance.EventPush_Message($"备注修改成功!");
            PublicComm.ClientLog.Info($"修改備註成功");

            //搜尋刪除記錄
            try
            {
                Fun_SelectDeleteRecord();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message($"删除记录查询资料库失败:{ex}");
                EventLogHandler.Instance.LogDebug("1-3", $"删除记录", $"查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"刪除記錄查詢失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"刪除記錄查詢失敗:{ex}");

                return;
            }
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Fun_SelectDeleteRecord();
        }


        private void Cob_Entry_Coil_No_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_DeleteRecordCoilList(Cob_Entry_Coil_No);

        }
    }
}
