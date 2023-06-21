using System;
using System.Windows.Forms;
using System.Data;
using DBService.Repository.ScheduleDelete_CoilReject_Code;
using DBService.Repository.DelayLocation;
using DBService.Repository.DelayReasonCode;
using DBService.Repository.MaterialGrade;
using System.Drawing;

using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblFlattener;
using DBService.Repository.LookupTblTensionUnitDepth;
using System.Text;
using DBService.Repository.LookupTblSideTrimmer1;
using DBService.Repository.LookupTblYieldStrength;

namespace CPL1HMI
{
    public partial class Frm_4_3_PresetTable : Form
    {

        string strTableName = string.Empty;
        DataTable dtPresetTable;
        DataTable dtSelectOne;
        DataTable dtBeforEdit;

        bool bolEditStatus;

        int Index_No = 0;

        //語系
        private LanguageHandler LanguageHand;

        public enum Action
        { 
            Insert,
            Update,
            Delete
        }

        public Frm_4_3_PresetTable()
        {
            InitializeComponent();
        }

        private void Frm_4_3_PresetTable_Load(object sender, EventArgs e)
        {
           // LanguageHandler.Instance.Fun_CurrentLanguage(this.Name);
            if (PublicForms.PresetTable == null) PublicForms.PresetTable = this;

            Fun_ComboBoxItemsDisplay();

            Control[] Frm_4_3_Control = new Control[] {
                Btn_New,
                Btn_Edit,
                Btn_Delete
            };

            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_4_3_Control, UserSetupHandler.Instance.Frm_4_3);

            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Material(Cob_SteelGrade);

            //新增
            Fun_SetBottonEnabled(Btn_New, true);

            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);

            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);


        }

        /// <summary>
        /// 维护项目ComboBox
        /// </summary>
        private void Fun_ComboBoxItemsDisplay()
        {
            DataTable dtGetTable = new DataTable();
            dtGetTable.Columns.Add(new DataColumn("Table"));
            dtGetTable.Columns.Add(new DataColumn("TableName"));
            DataRow dr = null;

            #region - Table Name -

            dr = dtGetTable.NewRow();
            dr[0] = string.Empty;
            dr[1] = string.Empty;
            dtGetTable.Rows.Add(dr);

            if( GlobalVariableHandler.proLine.Equals("CPL1"))
            {
                //CPL2沒有圓盤剪
                dr = dtGetTable.NewRow();
                dr[0] = nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1);
                dr[1] = "圆盘剪参数查询表";
                dtGetTable.Rows.Add(dr);
            }

            dr = dtGetTable.NewRow();
            dr[0] = nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength);
            dr[1] = "屈服强度查询表";
            dtGetTable.Rows.Add(dr);

            dr = dtGetTable.NewRow();
            dr[0] = nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension);
            dr[1] = "单位张力参数查询表";
            dtGetTable.Rows.Add(dr);

            dr = dtGetTable.NewRow();
            dr[0] = nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth);
            dr[1] = "三輥張力輥參數查詢表";
            dtGetTable.Rows.Add(dr);

            dr = dtGetTable.NewRow();
            dr[0] = nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener);
            dr[1] = "五辊整平机参数查询表";
            dtGetTable.Rows.Add(dr);

           

            #endregion

            Cob_Table.DisplayMember = "TableName";
            Cob_Table.ValueMember = "Table";
            Cob_Table.DataSource = dtGetTable;
        }


        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (bolEditStatus) 
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑后再查询", "查询LookupTable维护清单", 0);
                return; 
            }
            if (Cob_Table.SelectedIndex == -1)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选择维护项目", "查询LookupTable维护清单", 0);
                return;
            }

            strTableName = Cob_Table.SelectedValue.ToString();   
            if (strTableName.IsEmpty())
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选择维护项目", "查询LookupTable维护清单", 0);
                return;
            }
            dtPresetTable = Dt_GetDataTable(strTableName);
            Fun_TableDisplay(dtPresetTable, strTableName, Dgv_Table);
            Grb_NowGridData.Text = Cob_Table.Text;
        }

        private void Dgv_Table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Dgv_Table.CurrentRow == null) { return; }
            if (bolEditStatus) { return; }

            DataRow dr = Fun_GetDataRowFromCurrentRow(Dgv_Table, dtPresetTable);

            DataTable dt = dtPresetTable.Clone();
            try
            {
                dt.LoadDataRow(dr.ItemArray, false);
            }
            catch { return; }

            dtSelectOne = dt.Copy();

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);
        }

        /// <summary>
        /// 將選中的 DataGridViewRow 轉成 DataRow.
        /// </summary>
        /// <param name="dgv">來源DataGridView</param>
        /// <param name="dt">來源DataTable</param>
        /// <returns></returns>
        public DataRow Fun_GetDataRowFromCurrentRow(DataGridView dgv, DataTable dt)
        {
            if (dgv.CurrentRow == null) { return null; }
            if (dgv.SelectedRows.Count <= 0) { return null; }
            DataRowView drv = dgv.SelectedRows[0].DataBoundItem as DataRowView;
            int index = dt.Rows.IndexOf(drv.Row);
            DataRow dr = dt.Rows[index];

            return dr;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_New_Click(object sender, EventArgs e)
        {
            //if(已在编辑中)
            if (bolEditStatus) return;

            if (dtPresetTable == null)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请先查询要新增的参数表！", "新增参数", 0);
                return;
            }
            
            //if (Dgv_Table.CurrentRow == null)
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("请先查询要新增的参数表！", "新增代码维护", 0);
            //    return;
            //}
            if (dtSelectOne == null)
            {
                dtSelectOne = dtPresetTable.Clone();              
            }

            Pnl_CurrentRow.Visible = true;
            Dgv_Table.Enabled = false;
            bolEditStatus = true;
            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);
            //刪除
            Fun_SetBottonEnabled(Btn_Delete, false);
            //儲存
            Btn_Save.Visible = true;
            //取消
            Btn_Cancel.Visible = true;

            Dgv_CurrentRow.DataSource = null;
            DataTable dtNew = dtSelectOne.Clone();
            DataRow dr = dtNew.NewRow();
            dtNew.Rows.Add(dr);
            Fun_TableDisplay(dtNew, strTableName, Dgv_CurrentRow);

            //Fun_DataGridViewColumnsSetting();

            //Dgv_CurrentRow.Rows.Add(1);

        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            if (bolEditStatus) return;
           
            if (Dgv_Table.CurrentIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选取要修改的参数资料行","修改参数",0);

                return;
            }
            bolEditStatus = true;

            Pnl_CurrentRow.Visible = true;

            Dgv_Table.Enabled = false;

            //新增
            Fun_SetBottonEnabled(Btn_New,false);

            //刪除
            Fun_SetBottonEnabled(Btn_Delete,false);

            //儲存
            Btn_Save.Visible = true;

            //取消
            Btn_Cancel.Visible = true;

            //Fun_DataGridViewColumnsSetting();
            Dgv_CurrentRow.DataSource = null;
                                 
            Fun_TableDisplay(dtSelectOne, strTableName, Dgv_CurrentRow);
            dtBeforEdit = dtSelectOne.Copy();
            //DataGridViewRow Row = Dgv_Table.CurrentRow;

            //Dgv_CurrentRow.Rows.Add(Fun_CurrentRow(Row));
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            if (Dgv_Table.CurrentIsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选取要删除的参数资料行", "修改参数", 0);
                return;
            }
            DialogResult actionResult = DialogHandler.Instance.Fun_DialogShowOkCancel("确定要删除？", "删除", Properties.Resources.dialogQuestion, 1);

            if (actionResult == DialogResult.Cancel) return;

            Dgv_Table.Enabled = false;

            //新增
            Fun_SetBottonEnabled(Btn_New, false);

            //修改
            Fun_SetBottonEnabled(Btn_Edit, false);

            string strSql = Fun_TableDataInsertIntoDataBase(Action.Delete);

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, $"{Cob_Table.Text.Trim()}参数删除"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Cob_Table.Text.Trim()}参数删除失败", $"{Cob_Table.Text.Trim()}参数删除", 3);

                return;
            }

            //新增
            Fun_SetBottonEnabled(Btn_New, true);

            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);

            Dgv_Table.Enabled = true;
            dtPresetTable = Dt_GetDataTable(strTableName);
            Fun_TableDisplay(dtPresetTable, strTableName, Dgv_Table);
        }


        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (!bolEditStatus) return;
            bool bolSaveOK = false;
            string[] strArr ;
            if (strTableName == nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1))
            {
                //PRIMARY KEY 
                strArr = new string[] { "YS_Min", "YS_Max", "Coil_Thickness_Min", "Coil_Thickness_Max" };

                if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells["YS_Min"].Value.ToString()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"屈服强度下限 请勿空白", $"提示资讯", 3);
                    return;
                }
                if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells["YS_Max"].Value.ToString()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"屈服强度上限 请勿空白", $"提示资讯", 3);
                    return;
                }
            }else if (strTableName == nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength))
            {
                //PRIMARY KEY 
                strArr = new string[] { "Steel_Grade" };
                if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells["Steel_Grade"].Value.ToString()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"钢种 请勿空白", $"提示资讯", 3);
                    return;
                }
            }
            else
            {
                //PRIMARY KEY 
                strArr = new string[] {  "Material_Grade" , "Coil_Thickness_Min" , "Coil_Thickness_Max" };

                if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells["Material_Grade"].Value.ToString()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"钢种大类 请勿空白", $"提示资讯", 3);
                    return;
                }
            }

            if (strTableName != nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength))
            {
                if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells["Coil_Thickness_Min"].Value.ToString()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"厚度下限 请勿空白", $"提示资讯", 3);
                    return;
                }

                if (string.IsNullOrEmpty(Dgv_CurrentRow.Rows[0].Cells["Coil_Thickness_Min"].Value.ToString()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"厚度上限 请勿空白", $"提示资讯", 3);
                    return;
                }
            }
              


            string strSql = string.Empty, str = string.Empty;

            if (Btn_New.Enabled)
            {
                str = "新增";
              
                strSql = Fun_TableDataInsertIntoDataBase(Action.Insert);
            }
            else if (Btn_Edit.Enabled)
            {
                str = "修改";

                strSql = Fun_TableDataInsertIntoDataBase(Action.Update);
            }

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, $"{Cob_Table.Text.Trim()}参数维护{str}"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{Cob_Table.Text.Trim()}参数维护{str}失败", $"{Cob_Table.Text.Trim()}参数维护{str}", 3);

                return;
            }

            bolEditStatus = false;
            //新增
            Fun_SetBottonEnabled(Btn_New, true);

            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);

            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);

            //儲存
            Btn_Save.Visible = false;

            //取消
            Btn_Cancel.Visible = false;

            Pnl_CurrentRow.Visible = false;

            Dgv_Table.Enabled = true;
            dtPresetTable = Dt_GetDataTable(strTableName);
            Fun_TableDisplay(dtPresetTable, strTableName, Dgv_Table);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (!bolEditStatus) return;
            bolEditStatus = false;
            //新增
            Fun_SetBottonEnabled(Btn_New, true);

            //修改
            Fun_SetBottonEnabled(Btn_Edit, true);

            //刪除
            Fun_SetBottonEnabled(Btn_Delete, true);

            //儲存
            Btn_Save.Visible = false;

            //取消
            Btn_Cancel.Visible = false;

            Pnl_CurrentRow.Visible = false;

            Dgv_Table.Enabled = true;

        }

        /// <summary>
        /// 根據ComboBox維護項目查詢資料表
        /// </summary>
        private DataTable Dt_GetDataTable(string Table)
        {
            string strSql = "";// Frm_5_2_SqlFactory.SQL_Select_Table(Table);
           
            switch (Table)
            {
                case nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1):
                    {
                        //圓盤剪參數查詢表
                        strSql = Frm_4_3_SqlFactory_Trimmer.SQL_Select_SideTrimmer1(true);
                        break;
                    }

                case nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength):
                    {
                        //屈服强度查询表
                        strSql = Frm_4_3_SqlFactory_YieldStrength.SQL_Select_YieldStrength(true);
                        break;
                    }
                case nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension):
                    {
                        //單位張力參數查詢表
                        strSql = Frm_4_3_SqlFactory_Tension.SQL_Select_Tension();
                        break;
                    }
                case nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth):
                    {
                        //三輥張力輥參數查詢表
                        strSql = Frm_4_3_SqlFactory_TensionUnitDepth.SQL_Select_TensionUnitDepth();
                        break;
                    }
                case nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener):
                    {
                        //五輥整平機參數查詢表
                        strSql = Frm_4_3_SqlFactory_Flattener.SQL_Select_Flattener(true);
                        break;
                    }
                default:
                    break;
            }

           DataTable dtGetTable = DataAccess.Fun_SelectDate(strSql,$"{Table}栏位");
           
            return dtGetTable;
        }

        /// <summary>
        /// 顯示資料表內容
        /// </summary>
        private void Fun_TableDisplay(DataTable dt,string Table,DataGridView Dgv)
        {
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv, dt);

            switch (Table)
            {             
                //case nameof(DelayLocationModel.TBL_DelayLocation_Definition):
                //    Frm_5_2_ColumnsHandler.Instance.Frm_5_2_DelayLocation(Dgv);
                //    Index_No = int.Parse(dt.Rows[dt.Rows.Count - 1][nameof(DelayLocationModel.TBL_DelayLocation_Definition.Index_No)].ToString()) + 1;
                //    break;

                case nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1):
                    {
                        //圓盤剪參數查詢表
                        Frm_4_3_ColumnsHandler.Instance.Frm_4_3_Trimmer(Dgv);
                        break;
                    }
                case nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength):
                    {
                        //屈服强度查询表
                        Frm_4_3_ColumnsHandler.Instance.Frm_4_3_YieldStrength(Dgv);
                        break;
                    }
                case nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension):
                    {
                        //單位張力參數查詢表
                        Frm_4_3_ColumnsHandler.Instance.Frm_4_3_Tension(Dgv);
                        break;
                    }
                case nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth):
                    {
                        //三輥張力輥參數查詢表
                        Frm_4_3_ColumnsHandler.Instance.Frm_4_3_TensionUnitDepth(Dgv);
                        break;
                    }
                case nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener):
                    {
                        //五輥整平機參數查詢表
                        Frm_4_3_ColumnsHandler.Instance.Frm_4_3_Flattener(Dgv);
                        break;
                    }

                default:
                    break;
            }

            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv);

            Dgv.ClearSelection();
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

        /// <summary>
        /// Setting Dgv_CurrentRow Columns Title
        /// </summary>
        private void Fun_DataGridViewColumnsSetting()
        {
           
            //foreach (DataGridViewColumn Column in Dgv_Table.Columns)
            //{
            //    Dgv_CurrentRow.Columns.Add(Column.Name, Column.HeaderText);
            //}
            //Dgv_CurrentRow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// 設定按鈕啟用狀態並改顏色
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="bolE"></param>
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            //Color colorBack;
            btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

        /// <summary>
        /// 新增、修改語法
        /// </summary>
        /// <returns></returns>
        public string Fun_TableDataInsertIntoDataBase(Action action)
        {
            string strSql = string.Empty;
            DataRow dr;
            if (action.Equals(Action.Insert) )
            {
                dr = dtSelectOne.Rows[0];
            }
            else if (action.Equals(Action.Update))
            {
                dr = dtBeforEdit.Rows[0];
            }
            else
            {
                //DataTable dd = new DataTable();
                //dd = dtPresetTable.Clone();//赋值结构
                //foreach (DataGridViewRow dgvr in Dgv_CurrentRow.Rows)
                //{
                //    DataRow drr = dd.NewRow();
                //    drr = (dgvr.DataBoundItem as DataRowView).Row;//微软提供的唯一的转换DataRow
                //    dd.Rows.Add(drr.ItemArray);//此处不可是直接dr否则会报错
                //}
                //DataTable dtAfter = dd.Copy();

                dr =  dtSelectOne.Rows[0];
            }

            //////if (action.Equals(Action.Insert) || action.Equals(Action.Update))
            //////{
            //////    DataTable dd = new DataTable();
            //////    dd = dtPresetTable.Clone();//赋值结构
            //////    foreach (DataGridViewRow dgvr in Dgv_CurrentRow.Rows)
            //////    {
            //////        DataRow drr = dd.NewRow();
            //////        drr = (dgvr.DataBoundItem as DataRowView).Row;//微软提供的唯一的转换DataRow
            //////        dd.Rows.Add(drr.ItemArray);//此处不可是直接dr否则会报错
            //////    }
            //////    DataTable dtAfter = dd.Copy();



            //////    dr = dtAfter.Rows[0];// dtSelectOne.Rows[0];
            //////}else
            //////{
            //////    dr = dtSelectOne.Rows[0];
            //////}


            switch (strTableName)
            {
                case nameof(LkUpTableSideTrimmer1Entity.TBL_LookupTable_SideTrimmer1):                  
                    strSql = action.Equals(Action.Insert) ? Frm_4_3_SqlFactory_Trimmer.SQL_Insert_SideTrimmer() : 
                             action.Equals(Action.Update) ? Frm_4_3_SqlFactory_Trimmer.SQL_Update_SideTrimmer(dr) : Frm_4_3_SqlFactory_Trimmer.SQL_Delete_SideTrimmer(dr);
                    break;

                case nameof(LkUpTableYieldStrengthEntity.TBL_LookupTable_YieldStrength):
                    strSql = action.Equals(Action.Insert) ? Frm_4_3_SqlFactory_YieldStrength.SQL_Insert_YieldStrength() :
                             action.Equals(Action.Update) ? Frm_4_3_SqlFactory_YieldStrength.SQL_Update_YieldStrength(dr) : Frm_4_3_SqlFactory_YieldStrength.SQL_Delete_YieldStrength(dr);
                    break;

                case nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension):
                    strSql = action.Equals(Action.Insert) ? Frm_4_3_SqlFactory_Tension.SQL_Insert_Tension() : 
                             action.Equals(Action.Update) ? Frm_4_3_SqlFactory_Tension.SQL_Update_Tension(dr) : Frm_4_3_SqlFactory_Tension.SQL_Delete_Tension(dr);
                    break;

                case nameof(LkUpTableTensionUnitDepthEntity.TBL_LookupTable_TensionUnitDepth):
                    strSql = action.Equals(Action.Insert) ? Frm_4_3_SqlFactory_TensionUnitDepth.SQL_Insert_TensionUnitDepth() :
                             action.Equals(Action.Update) ? Frm_4_3_SqlFactory_TensionUnitDepth.SQL_Update_TensionUnitDepth(dr) : Frm_4_3_SqlFactory_TensionUnitDepth.SQL_Delete_TensionUnitDepth(dr);
                    break;
                case nameof(LkUpTableFlattenerEntity.TBL_LookupTable_Flattener):
                    strSql = action.Equals(Action.Insert) ? Frm_4_3_SqlFactory_Flattener.SQL_Insert_Flattener() :
                             action.Equals(Action.Update) ? Frm_4_3_SqlFactory_Flattener.SQL_Update_Flattener(dr) : Frm_4_3_SqlFactory_Flattener.SQL_Delete_Flattener(dr);
                    break;

              
               
                default:
                    break;
            }

            return strSql;
        }

        private void Dgv_CurrentRow_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            DataGridView dgv = (DataGridView)sender;
            string strHeaderText = dgv.Columns[e.ColumnIndex].HeaderText;
            DialogHandler.Instance.Fun_DialogShowOk($"[{strHeaderText}] 栏位输入格式错误，请重新输入！", $"警告资讯", 3);
        }

        private void Cob_Table_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Cob_Table.Text == "屈服强度查询表")
            {
                Chk_SteelGrade.Enabled = false;
                Cob_SteelGrade.Enabled = false;
                Chk_Thickness.Enabled = false;
                Txt_Thickness.ReadOnly = true;
            }
            else
            {
                Chk_SteelGrade.Enabled = true;
                Cob_SteelGrade.Enabled = true;
                Chk_Thickness.Enabled = true;
                Txt_Thickness.ReadOnly = false;
            }
        }
    }
}
