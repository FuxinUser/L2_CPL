using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using DBService.Repository.Utility;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace CPL1HMI
{
    public partial class frm_4_2_Utility : Form
    {
        private DataTable dtGetUtilityList;

        //語系
        private LanguageHandler LanguageHand;

        public frm_4_2_Utility()
        {
            InitializeComponent();
        }

        private void Frm_4_2_Utility_Load(object sender, EventArgs e)
        {
           // LanguageHandler.Instance.Fun_CurrentLanguage(this.Name);
            
            if (PublicForms.Utility == null) PublicForms.Utility = this;

            Control[] Frm_4_2_Control = new Control[] {
                Btn_Cancel,
                Btn_Delete,
                Btn_Edit,
                Btn_New,
                Btn_SendToMMS,
            };

            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_4_2_Control, UserSetupHandler.Instance.Frm_4_2);

            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol, true);
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_Total, true);

            //班次
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Shift, Cob_Shift);
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Shift, Cob_Shift_S);

            //班別
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Team, Cob_Team);
            Dtp_DateStart.Value = Dtp_DateStart.Value.AddDays(-7);

            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);

            Btn_SendToMMS.Enabled = false;

            Btn_SendToMMS.BackColor = Color.LightGray;

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);

        }
        private void Btn_Query_Click(object sender, EventArgs e)
        {
            Fun_GetUtilityData();
        }

        /// <summary>
        /// 查詢能源耗用
        /// </summary>
        private void Fun_GetUtilityData()
        {
            
            //執行班次搜尋時，才可以上傳MMS
            Btn_SendToMMS.Enabled = Rdb_Shitf.Checked;

            if (Rdb_Shitf.Checked) ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_Total, false);

            //變色，增加辨識度
            Btn_SendToMMS.BackColor = Rdb_Shitf.Checked ? Color.Gold : Color.LightGray;

            if (Rdb_DateTime.Checked)
            {
                if (Dtp_DateStart.DateTimeRangeIsFail(Dtp_DateEnd.Value))
                {
                    EventLogHandler.Instance.EventPush_Message($"日期区间起讫时间不正确，请重新确认");
                    return;
                }
            }


            string strSql = Rdb_DateTime.Checked ? Frm_4_2_SqlFactory.SQL_Select_UnityWithDate() : Frm_4_2_SqlFactory.SQL_Select_UnityWithTeam();
            dtGetUtilityList = DataAccess.Fun_SelectDate(strSql, "能源耗用");

            Fun_GetUtilityDataTotal(dtGetUtilityList);

            // 把資料導入DataGridView
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Utility, dtGetUtilityList);
            Frm_4_2_ColumnsHandler.Instance.Frm_4_2_Utility(Dgv_Utility);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Utility);

            if (dtGetUtilityList.IsNull())
            {
                //修改
                Fun_SetBottonEnabled(Btn_Edit, false);

                //刪除
                Fun_SetBottonEnabled(Btn_Delete, false);
            }
        }


        /// <summary>
        /// 能源耗用統計 : 查詢之後才會統計
        /// </summary>
        /// <param name="dtGetUtility"></param>
        private void Fun_GetUtilityDataTotal(DataTable dtGetUtility)
        {
            float flo_CompressedAir = 0, flo_CoolingWater = 0;

            //if (!dtGetUtility.IsNull())
            //{
            //    foreach (DataRow dr in dtGetUtility.Rows)
            //    {
            //        flo_CompressedAir += Convert.ToSingle(dr[nameof(UtilityEntity.TBL_Utility.CompressedAir)].ToString());

            //        flo_CoolingWater += Convert.ToSingle(dr[nameof(UtilityEntity.TBL_Utility.IndirectCollingWater)].ToString());
            //    }
            //}

            if (!dtGetUtility.IsNull())
            {
                var colNameAir = nameof(UtilityEntity.TBL_Utility.CompressedAir);
                var colNameWater = nameof(UtilityEntity.TBL_Utility.IndirectCollingWater);
                var rowCnt = dtGetUtility.Rows.Count;
                var minAir = Convert.ToSingle(dtGetUtility.Rows[rowCnt - 1][colNameAir].ToString());
                var maxAir = Convert.ToSingle(dtGetUtility.Rows[0][colNameAir].ToString());
                var minWater = Convert.ToSingle(dtGetUtility.Rows[rowCnt - 1][colNameWater].ToString());
                var maxWater = Convert.ToSingle(dtGetUtility.Rows[0][colNameWater].ToString());

                flo_CompressedAir = maxAir - minAir;
                flo_CoolingWater = maxWater - minWater;
            }

            Txt_CompressedAir_Tol.Text = flo_CompressedAir.ToString();
            Txt_CoolingWater_Tol.Text = flo_CoolingWater.ToString();
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_New_Click(object sender, EventArgs e)
        {
           
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);

            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);

            //儲存
            Btn_Save.Visible = true;

            //取消
            Btn_Cancel.Visible = true;

            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol, false);

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            
            //新增
            Fun_SetBottonEnabled(Btn_New, false);

            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);

            //儲存
            Btn_Save.Visible = true;

            //取消
            Btn_Cancel.Visible = true;

            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol, false);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);

            //新增
            Fun_SetBottonEnabled(Btn_New, false);

            DialogResult actionResult = MessageBox.Show($"确定删除[{Dtp_UtilityDate.Value:yyyy-MM-dd HH:mm:ss}]能源耗用记录？", "刪除能源耗用",
             MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (actionResult == DialogResult.No) return;

            DateTime Dt = DateTime.Parse(Dgv_Utility.CurrentRow.Cells[0].Value.ToString());

            string strSql = Frm_4_2_SqlFactory.SQL_Delete_Utility(Dt);

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "删除能源耗用记录"))
            {
                EventLogHandler.Instance.EventPush_Message($"删除能源耗用记录失败");
                return;
            }

            EventLogHandler.Instance.EventPush_Message($"能源耗用删除成功");

            PublicComm.ClientLog.Info($"能源耗用紀錄刪除成功");


            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);
            //新增
            Fun_SetBottonEnabled(Btn_New, true);
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            DateTime Dt = DateTime.Parse(Dgv_Utility.CurrentRow.Cells[0].Value.ToString());

            string strSql = Btn_New.Enabled ? Frm_4_2_SqlFactory.SQL_Insert_Utility() : Frm_4_2_SqlFactory.SQL_Update_Utility(Dt);

            string strMessage = Btn_New.Enabled ? "新增能源耗用记录" : "修改能源耗用记录";

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, $"{strMessage}"))
            {
                EventLogHandler.Instance.EventPush_Message($"{strMessage}失败");
                return;
            }
         
            EventLogHandler.Instance.EventPush_Message($"能源耗用{strMessage}成功");

            PublicComm.ClientLog.Info($"能源耗用紀錄{strMessage}成功");

           
            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);
            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);
            //儲存
            Btn_Save.Visible = false;
            //取消
            Btn_Cancel.Visible = false;
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol, true);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            
            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);
            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);
            //儲存
            Btn_Save.Visible = false;
            //取消
            Btn_Cancel.Visible = false;
            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_DataCol, true);
            Fun_DataGridViewCellsClick();
        }

        private void Btn_SendToMMS_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel("是否要上传MMS?", "上传MMS", Properties.Resources.dialogQuestion, 1);

            if (dialogR.Equals(DialogResult.OK))
            {

                SCCommMsg.CS15_Utility Utility = new SCCommMsg.CS15_Utility
                {
                    TotalCompressedAir = Txt_CompressedAir_Tol.Text.Trim(),
                    TotalCoolingWater = Txt_CoolingWater_Tol.Text.Trim(),
                    Shift_Date = Dtp_DateShitf.Value.ToString("yyyyMMdd"),
                    Shift_No = Dgv_Utility.CurrentRow.Cells[1].Value.ToString(),
                    Group_No = Dgv_Utility.CurrentRow.Cells[2].Value.ToString(),
                    Unit_code = ""
                };

                PublicComm.Client.Tell(Utility);
                EventLogHandler.Instance.LogInfo("4-2", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}能源耗用上传MMS", "能源耗用上传MMS");

               // DialogHandler.Instance.Fun_DialogShowOk("已通知Server上传至MMS", "能源耗用上传MMS", 4);

                PublicComm.ClientLog.Info($"已通知Server能源耗用上傳MMS");
                PublicComm.AkkaLog.Info($"已通知Server能源耗用上傳MMS");

                DialogHandler.Instance.Fun_DialogShowOk("已通知Server能源耗用上傳MMS", "能源耗用上传MMS", 4);
            }


            ReadOnlyHandler.Instance.ReadOnlyGroupBox(Grb_Total, true);

        }

        private void DgvUtility_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Dgv_Utility.CurrentIsNull()) return;
            if (Dgv_Utility.DgvIsNull()) return;

            Fun_DataGridViewCellsClick();

            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);
        }

        /// <summary>
        /// 選取資料列出
        /// </summary>
        private void Fun_DataGridViewCellsClick()
        {
            if (dtGetUtilityList.IsNull()) return;

            DataRow drGetCurrentRow = dtGetUtilityList.Rows[Dgv_Utility.CurrentRow.Index];

            Dtp_UtilityDate.Value = DateTime.Parse(Convert.ToString(drGetCurrentRow[nameof(UtilityEntity.TBL_Utility.Receive_Time)]));

            //班次
            Cob_Shift.SelectedValue = drGetCurrentRow[nameof(UtilityEntity.TBL_Utility.Shift)].ToString().Trim() ?? string.Empty;
            //班別
            Cob_Team.SelectedValue = drGetCurrentRow[nameof(UtilityEntity.TBL_Utility.Team)].ToString().Trim() ?? string.Empty;
            //压缩空气
            Txt_ComeAir.Text = drGetCurrentRow[nameof(UtilityEntity.TBL_Utility.CompressedAir)].ToString() ?? string.Empty;
            //间接冷却水
            Txt_CoolingWater.Text = drGetCurrentRow[nameof(UtilityEntity.TBL_Utility.IndirectCollingWater)].ToString() ?? string.Empty;
        }

        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //Color colorBack;
            btn.BackColor = bolE ? Color.Gold : Color.LightGray;
        }

        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
        }
    }
}
