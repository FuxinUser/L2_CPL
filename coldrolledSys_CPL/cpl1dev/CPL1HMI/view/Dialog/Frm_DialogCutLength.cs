using DBService.Repository.CoilCutRecord;
using DBService.Repository.CutReocrd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class Frm_DialogCutLength : Form
    {
        private DataTable dtGetCutRecordTemp;
        public bool IsHead;
        public string Coil_ID;


        public Frm_DialogCutLength()
        {
            InitializeComponent();
        }

        private void Frm_DialogCutLength_Shown(object sender, EventArgs e)
        {
            Fun_SelectCutRecordTemp();
        }


        /// <summary>
        /// 查询切割资料 IsHead = true (头段) ; false(尾段) ;
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <param name="IsHead"></param>
        public void Fun_SelectCutRecordTemp()
        {
            //標題文字
            string strPosition = IsHead ? "头段" : "尾段";
            string strTitle = $"计算{strPosition}导带切废长度";
            Lbl_Title.Text = strTitle;

            //分切模式
            string CutMode = IsHead ? "a" : "b";

            string strSql = Frm_3_2_SqlFactory.SQL_Select_CutRecordTemp(Coil_ID, CutMode);

            dtGetCutRecordTemp = DataAccess.Fun_SelectDate(strSql, "查询TBL_CutRecord_Temp");

            if (dtGetCutRecordTemp.IsNull())
            {
                EventLogHandler.Instance.EventPush_Message($"查无钢卷号[{Coil_ID}]的切废资讯");
                //return;
            }

            Fun_SettingDataGridView();
        }


        private void Btn_Sure_Click(object sender, EventArgs e)
        {
            Fun_SearchDataRow_CheckBox_True();

            Close();
        }



        private void Fun_SearchDataRow_CheckBox_True()
        {

            PublicForms.PDODetail.strSql_Insert_TBL_CutRecord = $@"Insert into [{nameof(CoilCutRecordEntity.TBL_Coil_CutRecord)}] 
                                       ([{nameof(CoilCutRecordEntity.TBL_Coil_CutRecord.CutTime)}],
                                        [{nameof(CoilCutRecordEntity.TBL_Coil_CutRecord.Coil_ID)}],
                                        [{nameof(CoilCutRecordEntity.TBL_Coil_CutRecord.In_Coil_ID)}],
                                        [{nameof(CoilCutRecordEntity.TBL_Coil_CutRecord.Cut_Type)}],
                                        [{nameof(CoilCutRecordEntity.TBL_Coil_CutRecord.CutLength)}]) Values";

            if (IsHead)
            {
                PublicForms.PDODetail.Insert_TBL_CutRecord_Head = new List<string>();

                Fun_CombinationSqlCommand(PublicForms.PDODetail.Insert_TBL_CutRecord_Head, "H");

            }
            else
            {
                PublicForms.PDODetail.Insert_TBL_CutRecord_Tail = new List<string>();

                Fun_CombinationSqlCommand(PublicForms.PDODetail.Insert_TBL_CutRecord_Tail, "T");

            }

        }

        private void Fun_CombinationSqlCommand(List<string> list, string Cut_Type)
        {
            int Length = 0;

            DataGridViewCheckBoxCell check;

            for (int RowIndex = 0; RowIndex < dtGetCutRecordTemp.Rows.Count; RowIndex++)
            {
                check = (DataGridViewCheckBoxCell)Dgv_CutRecord_Temp.Rows[RowIndex].Cells[0];

                if ((bool)check.FormattedValue)
                {
                    list.Add($@"('{dtGetCutRecordTemp.Rows[RowIndex][nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutTime)]}',
                                 '{dtGetCutRecordTemp.Rows[RowIndex][nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.Coil_ID)]}',
                                 '{dtGetCutRecordTemp.Rows[RowIndex][nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.In_Coil_ID)]}',
                                 '{Cut_Type}',
                                 '{dtGetCutRecordTemp.Rows[RowIndex][nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutLength)]}')");

                    Length += Convert.ToInt32(dtGetCutRecordTemp.Rows[RowIndex][nameof(CoilCutRecordTempEntity.TBL_Coil_CutRecord_Temp.CutLength)].ToString());
                }
            }

            if (IsHead)
                PublicForms.PDODetail.Txt_Scraped_Length_Entry.Text = Length.ToString();
            else
                PublicForms.PDODetail.Txt_Scraped_Length_Exit.Text = Length.ToString();

        }


        /// <summary>
        /// 设定DataGridView
        /// </summary>
        /// <param name="dt"></param>
        private void Fun_SettingDataGridView()
        {

            Dgv_CutRecord_Temp.DataSource = dtGetCutRecordTemp;

            //DGV設定
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_CutRecord_Temp, dtGetCutRecordTemp);
            Frm_DialogCutLength_ColumnsHandler.Instance.Frm_DialogCutLength_CutRecord_Temp(Dgv_CutRecord_Temp);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_CutRecord_Temp);
        
            //Fun_DataGridView_AddCheckBox();
        }


        /// <summary>
        /// 新增CheckBox在DataGridView
        /// </summary>
        private void Fun_DataGridView_AddCheckBox()
        {
            DataGridViewColumn CheckBoxColumn = new DataGridViewCheckBoxColumn
            {
                Width = 70,
                HeaderText = "选取"
            };

            this.Dgv_CutRecord_Temp.Columns.Insert(0, CheckBoxColumn);
        }
      
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
