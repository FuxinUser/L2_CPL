using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CPL1HMI
{
   public enum ActionType
    {
        Insert,
        Update,
        Delete
    }

    public enum DeviceName
    {
        /// <summary>
        /// 整平机
        /// </summary>
        Flattener,
        /// <summary>
        /// 裁边机
        /// </summary>
        Trimmer,
        /// <summary>
        /// 张力机
        /// </summary>
        Tension
    }

    public partial class frm_4_3_ProductionParameters : Form
    {
        DataTable dt_Trimmer;
        DataTable dt_Tension;
        DataTable dt_Flattener;


        public frm_4_3_ProductionParameters()
        {
            InitializeComponent();
        }
        private void frm_4_3_ProductionParameters_Load(object sender, EventArgs e)
        {
            //PublicForms.ProductionParameters = this;

            Control[] Frm_4_1_Control = new Control[] {
            Btn_Tension_Insert, //Tension 新增
            Btn_Tension_Update, //Tension 修改
            Btn_Tension_Delete, //Tension 刪除

            Btn_Trimmer_Insert, //Trimmer 新增
            Btn_Trimmer_Update, //Trimmer 修改
            Btn_Trimmer_Delete, //Trimmer 刪除

            Btn_Flattener_Insert, //Flattener 新增
            Btn_Flattener_Update, //Flattener 修改
            Btn_Flattener_Delete //Flattener 刪除
            };

            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_4_1_Control, UserSetupHandler.Instance.Frm_4_1);
            dt_Trimmer = new DataTable();
            dt_Tension = new DataTable();
            dt_Flattener = new DataTable();

            Tab_Main_SideTrimmerPage.Parent = GlobalVariableHandler.proLine.Equals("CPL1") ? Tab_Main_SideTrimmerPage.Parent = Tab_MainControl : Tab_Main_SideTrimmerPage.Parent = null;
            
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Material(Cob_SteelGrade);
            
            Fun_GetDataGridViewData();
        }

        /// <summary>
        /// 重新整理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Reload_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Material(Cob_SteelGrade);

            Fun_GetDataGridViewData();
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            Fun_GetDataGridViewData(true);

            EventLogHandler.Instance.LogInfo("4-1", $"使用者:{PublicForms.Main.lblLoginUser.Text}设备参数查询" , "设备参数查询");
        }

        private void Fun_GetDataGridViewData(bool bolSearch = false)
        {
            if (Chk_SteelGrade.Checked && Cob_SteelGrade.SelectedIndex == -1)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选择钢种!", "查询提示", 0);
                return;
            }

            if (Chk_Thickness.Checked && string.IsNullOrEmpty(Txt_Thickness.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk("请输入厚度范围!", "查询提示", 0);
                Txt_Thickness.Focus();
                return;
            }

            Fun_DisplaySideTrimmerLookupTable(bolSearch);

            Fun_DisplayLineTensionLookupTable();

            Fun_DisplayFlattenerLookupTable(bolSearch);
        }

        /// <summary>
        /// 裁边机资料
        /// </summary>
        private void Fun_DisplaySideTrimmerLookupTable(bool bolSearch = false)
        {
            string strSql = Frm_4_3_SqlFactory_Trimmer.SQL_Select_SideTrimmer(bolSearch);
            dt_Trimmer = DataAccess.Fun_SelectDate(strSql, "裁边机LookupTable");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_SideTrimmer, dt_Trimmer);
            Frm_4_3_ColumnsHandler.Instance.Frm_4_3_Trimmer(Dgv_SideTrimmer);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_SideTrimmer);

            if (dt_Trimmer.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("查无裁边机LookupTable", "查询裁边机LookupTable", 0);
            }
        }

        /// <summary>
        /// 张力机资料
        /// </summary>
        private void Fun_DisplayLineTensionLookupTable()
        {
            string strSql = Frm_4_3_SqlFactory_Tension.SQL_Select_Tension();
            dt_Tension = DataAccess.Fun_SelectDate(strSql, "张力机LookupTable");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Tension, dt_Tension);
            Frm_4_3_ColumnsHandler.Instance.Frm_4_3_Tension(Dgv_Tension);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Tension);

            if (dt_Trimmer.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("查无张力机LookupTable", "查询张力机LookupTable", 0);
            }
        }
        
        /// <summary>
        /// 整平机资料
        /// </summary>
        private void Fun_DisplayFlattenerLookupTable(bool bolSearch = false)
        {
            string strSql = Frm_4_3_SqlFactory_Flattener.SQL_Select_Flattener(bolSearch);
            dt_Flattener = DataAccess.Fun_SelectDate(strSql, "整平机LookupTable");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Flattener, dt_Flattener);
            Frm_4_3_ColumnsHandler.Instance.Frm_4_3_Flattener(Dgv_Flattener);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Flattener);

            if (dt_Trimmer.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("查无整平机LookupTable", "查询整平机LookupTable", 0);
            }
        }

        #region Tension
        /// <summary>
        /// Tension-新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Tension_Insert_Click(object sender, EventArgs e)
        {
            Fun_DataGridViewColumnsSetting(Dgv_Tension, Dgv_Tension_Edit);
            Dgv_Tension_Edit.Rows.Add(1);


            Fun_SetBottonEnabled(Btn_Tension_Update,false);
            Fun_SetBottonEnabled(Btn_Tension_Delete,false);
            Btn_Tension_Save.Visible = true;
            Btn_Tension_Cancel.Visible = true;
            Pnl_Tension.Visible = true;
        }
        /// <summary>
        /// Tension-修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Tension_Update_Click(object sender, EventArgs e)
        {

            Fun_DataGridViewColumnsSetting(Dgv_Tension, Dgv_Tension_Edit);
            DataGridViewRow Row = Dgv_Tension.CurrentRow;
            Dgv_Tension_Edit.Rows.Add(Fun_CurrentRow(Row));

            Fun_SetBottonEnabled(Btn_Tension_Insert, false);
            Fun_SetBottonEnabled(Btn_Tension_Delete, false);
            Btn_Tension_Save.Visible = true;
            Btn_Tension_Cancel.Visible = true;
            Pnl_Tension.Visible = true;
        }
        /// <summary>
        /// Tension-储存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Tension_Save_Click(object sender, EventArgs e)
        {
            if (Btn_Tension_Insert.Enabled)
            {
                Fun_DataBaseAction(ActionType.Insert, DeviceName.Tension);
            }
            else if (Btn_Tension_Update.Enabled)
            {
                Fun_DataBaseAction(ActionType.Update, DeviceName.Tension);
            }

            Pnl_Tension.Visible = false;
            Btn_Tension_Save.Visible = false;
            Btn_Tension_Cancel.Visible = false;
            Fun_SetBottonEnabled(Btn_Tension_Insert, true);
            Fun_SetBottonEnabled(Btn_Tension_Update, true);
            Fun_SetBottonEnabled(Btn_Tension_Delete, true);
        }
        /// <summary>
        /// Tension-取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Tension_Cancel_Click(object sender, EventArgs e)
        {
            Pnl_Tension.Visible = false;
            Btn_Tension_Save.Visible = false;
            Btn_Tension_Cancel.Visible = false;
            Fun_SetBottonEnabled(Btn_Tension_Insert, true);
            Fun_SetBottonEnabled(Btn_Tension_Update, true);
            Fun_SetBottonEnabled(Btn_Tension_Delete, true);
        }
        /// <summary>
        /// Tension-刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Tension_Delete_Click(object sender, EventArgs e)
        {
            Fun_DataBaseAction(ActionType.Delete, DeviceName.Tension);
        }
       
        #endregion

        #region Flattener
        /// <summary>
        /// Flattener-新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Flattener_Insert_Click(object sender, EventArgs e)
        {
            Pnl_Flattener.Visible = true;

            Fun_DataGridViewColumnsSetting(Dgv_Flattener, Dgv_Flattener_Edit);
            Dgv_Flattener_Edit.Rows.Add(1);

            Fun_SetBottonEnabled(Btn_Flattener_Update, false);
            Fun_SetBottonEnabled(Btn_Flattener_Delete, false);
            Btn_Flattener_Save.Visible = true;
            Btn_Flattener_Cancel.Visible = true;
        }
        /// <summary>
        /// Flattener-修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Flattener_Update_Click(object sender, EventArgs e)
        {
            Pnl_Flattener.Visible = true;

            Fun_DataGridViewColumnsSetting(Dgv_Flattener, Dgv_Flattener_Edit);
            DataGridViewRow Row = Dgv_Flattener.CurrentRow;
            Dgv_Flattener_Edit.Rows.Add(Fun_CurrentRow(Row));

            Fun_SetBottonEnabled(Btn_Flattener_Insert, false);
            Fun_SetBottonEnabled(Btn_Flattener_Delete, false);
            Btn_Flattener_Save.Visible = true;
            Btn_Flattener_Cancel.Visible = true;
        }
        /// <summary>
        /// Flattener-储存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Flattener_Save_Click(object sender, EventArgs e)
        {
            if (Btn_Flattener_Insert.Enabled)
            {
                Fun_DataBaseAction(ActionType.Insert, DeviceName.Flattener);
            }
            else if (Btn_Flattener_Update.Enabled)
            {
                Fun_DataBaseAction(ActionType.Update, DeviceName.Flattener);
            }

            Fun_SetBottonEnabled(Btn_Flattener_Insert, true);
            Fun_SetBottonEnabled(Btn_Flattener_Update, true);
            Fun_SetBottonEnabled(Btn_Flattener_Delete, true);
            Btn_Flattener_Save.Visible = false;
            Btn_Flattener_Cancel.Visible = false;
            Pnl_Flattener.Visible = false;
        }
        /// <summary>
        /// Flattener-取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Flattener_Cancel_Click(object sender, EventArgs e)
        {

            Fun_SetBottonEnabled(Btn_Flattener_Insert, true);
            Fun_SetBottonEnabled(Btn_Flattener_Update, true);
            Fun_SetBottonEnabled(Btn_Flattener_Delete, true);
            Btn_Flattener_Save.Visible = false;
            Btn_Flattener_Cancel.Visible = false;
            Pnl_Flattener.Visible = false;
        }
        /// <summary>
        /// Flattener-刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Flattener_Delete_Click(object sender, EventArgs e)
        {
            Fun_DataBaseAction(ActionType.Delete, DeviceName.Flattener);
        }
       
        #endregion

        #region Trimmer
        /// <summary>
        /// Trimmer-新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Trimmer_Insert_Click(object sender, EventArgs e)
        {
            Fun_DataGridViewColumnsSetting(Dgv_SideTrimmer, Dgv_SideTrimmer_Edit);
            Dgv_SideTrimmer_Edit.Rows.Add(1);

            Fun_SetBottonEnabled(Btn_Trimmer_Update, false);
            Fun_SetBottonEnabled(Btn_Trimmer_Delete, false);
            Btn_Trimmer_Save.Visible = true;
            Btn_Trimmer_Cancel.Visible = true;
            Pnl_Trimmer.Visible = true;
        }
        /// <summary>
        /// Trimmer-修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Trimmer_Update_Click(object sender, EventArgs e)
        {
            Fun_DataGridViewColumnsSetting(Dgv_SideTrimmer, Dgv_SideTrimmer_Edit);
            DataGridViewRow Row = Dgv_SideTrimmer.CurrentRow;
            Dgv_SideTrimmer_Edit.Rows.Add(Fun_CurrentRow(Row));

            Fun_SetBottonEnabled(Btn_Trimmer_Update, false);
            Fun_SetBottonEnabled(Btn_Trimmer_Delete, false);
            Btn_Trimmer_Save.Visible = true;
            Btn_Trimmer_Cancel.Visible = true;
            Pnl_Trimmer.Visible = true;
        }
        /// <summary>
        /// Trimmer-储存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Trimmer_Save_Click(object sender, EventArgs e)
        {
            if (Btn_Trimmer_Insert.Enabled)
            {
                Fun_DataBaseAction(ActionType.Insert, DeviceName.Trimmer);
            }
            else if (Btn_Trimmer_Update.Enabled)
            {
                Fun_DataBaseAction(ActionType.Update, DeviceName.Trimmer);
            }
            

            Fun_SetBottonEnabled(Btn_Trimmer_Insert, true);
            Fun_SetBottonEnabled(Btn_Trimmer_Update, true);
            Fun_SetBottonEnabled(Btn_Trimmer_Delete, true);
            Btn_Trimmer_Save.Visible = false;
            Btn_Trimmer_Cancel.Visible = false;
            Pnl_Trimmer.Visible = false;
        }
        /// <summary>
        /// Trimmer-取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Trimmer_Cancel_Click(object sender, EventArgs e)
        {

            Fun_SetBottonEnabled(Btn_Trimmer_Insert, true);
            Fun_SetBottonEnabled(Btn_Trimmer_Update, true);
            Fun_SetBottonEnabled(Btn_Trimmer_Delete, true);
            Btn_Trimmer_Save.Visible = false;
            Btn_Trimmer_Cancel.Visible = false;
            Pnl_Trimmer.Visible = false;
        }
        /// <summary>
        /// Trimmer-删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Trimmer_Delete_Click(object sender, EventArgs e)
        {
            Fun_DataBaseAction(ActionType.Delete, DeviceName.Trimmer);
        }
       
        #endregion

       
        /// <summary>
        /// 新增/修改/刪除
        /// </summary>
        /// <param name="action"></param>
        /// <param name="device"></param>
        private void Fun_DataBaseAction(ActionType action ,DeviceName device )
        {
            string strSql = string.Empty;
            string str = string.Empty;
            DataRow dr;

            if (action == ActionType.Delete)
            {
                DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel("确认删除?", "删除", Properties.Resources.dialogQuestion, 1);
                if (dialogR == DialogResult.Cancel) return;
            }

            switch (action)
            {
                case ActionType.Insert:
                    str = "新增";
                    switch (device)
                    {
                        case DeviceName.Flattener:
                            strSql = Frm_4_3_SqlFactory_Flattener.SQL_Insert_Flattener();
                            break;

                        case DeviceName.Trimmer:
                            strSql = Frm_4_3_SqlFactory_Trimmer.SQL_Insert_SideTrimmer();
                            break;

                        case DeviceName.Tension:
                            strSql = Frm_4_3_SqlFactory_Tension.SQL_Insert_Tension();
                            break;

                        default:
                            break;
                    }
                    break;

                case ActionType.Update:
                    str = "修改";
                    switch (device)
                    {
                        case DeviceName.Flattener:
                            dr = dt_Flattener.Rows[Dgv_Flattener.CurrentRow.Index];
                            strSql = Frm_4_3_SqlFactory_Flattener.SQL_Update_Flattener(dr);
                            break;

                        case DeviceName.Trimmer:
                            dr = dt_Trimmer.Rows[Dgv_SideTrimmer.CurrentRow.Index];
                            strSql = Frm_4_3_SqlFactory_Trimmer.SQL_Update_SideTrimmer(dr);
                            break;

                        case DeviceName.Tension:
                            dr = dt_Tension.Rows[Dgv_Tension.CurrentRow.Index];
                            strSql = Frm_4_3_SqlFactory_Tension.SQL_Update_Tension(dr);
                            break;

                        default:
                            break;
                    }
                    break;

                case ActionType.Delete:
                    str = "删除";
                    switch (device)
                    {
                        case DeviceName.Flattener:
                            dr = dt_Flattener.Rows[Dgv_Flattener.CurrentRow.Index];
                            strSql = Frm_4_3_SqlFactory_Flattener.SQL_Delete_Flattener(dr);
                            break;

                        case DeviceName.Trimmer:
                            dr = dt_Trimmer.Rows[Dgv_SideTrimmer.CurrentRow.Index];
                            strSql = Frm_4_3_SqlFactory_Trimmer.SQL_Delete_SideTrimmer(dr);
                            break;

                        case DeviceName.Tension:
                            dr = dt_Tension.Rows[Dgv_Tension.CurrentRow.Index];
                            strSql = Frm_4_3_SqlFactory_Tension.SQL_Delete_Tension(dr);
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, $"{str}生产参数"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{str}生产参数失败", $"{str}生产参数",3);

                return;
            }

            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Material(Cob_SteelGrade);

            Fun_GetDataGridViewData();
        }
       
        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_MainControl, e);
        }

        private void Cbo_SteelGrade_Click(object sender, EventArgs e)
        {
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Material(Cob_SteelGrade);
        }

        //設定按鈕啟用狀態並改顏色
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //Color colorBack;
            btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

        /// <summary>
        /// dgv_LookupTable = LookupTable
        /// dgv_CurrentRow = 欲編輯資料行
        /// </summary>
        /// <param name="dgv_LookupTable"></param>
        /// <param name="dgv_CurrentRow"></param>
        private void Fun_DataGridViewColumnsSetting(DataGridView dgv_LookupTable,DataGridView dgv_CurrentRow)
        {
            dgv_CurrentRow.Rows.Clear();

            dgv_CurrentRow.Columns.Clear();

            foreach (DataGridViewColumn Column in dgv_LookupTable.Columns)
            {
                dgv_CurrentRow.Columns.Add(Column.Name, Column.HeaderText);
            }

        }

        private DataGridViewRow Fun_CurrentRow(DataGridViewRow Row)
        {
            DataGridViewRow Dgv_Row = (DataGridViewRow)Row.Clone();

            for (int Index = 0; Index < Row.Cells.Count; Index++)
            {
                Dgv_Row.Cells[Index].Value = Row.Cells[Index].Value;
            }
            return Dgv_Row;
        }
    }
}
