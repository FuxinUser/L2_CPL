using DBService.Repository.DelayLocation;
using DBService.Repository.DelayReasonCode;
using DBService.Repository.EventLog;
using DBService.Repository.LineFaultRecords;
using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblFlattener;
using DBService.Repository.LookupTblSideTrimmer;
using DBService.Repository.MaterialGrade;
using DBService.Repository.ScheduleDelete_CoilReject_Code;
using DBService.Repository.Utility;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DBService.Repository.DefectData;
using static CPL1HMI.DataBaseTableFactory;

namespace CPL1HMI
{
    public class DGVColumnsHandler
    {
        private static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly DGVColumnsHandler INSTANCE = new DGVColumnsHandler();
        }

        public static DGVColumnsHandler Instance { get { return SingletonHolder.INSTANCE; } }


        /// <summary>
        /// 將DataTable繫結進DataGridView
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="dt"></param>
        public void Fun_DataGridViewDataDisplay(DataGridView dgv, DataTable dt)
        {
            ColumnsClear(dgv);

            if (dt.IsNull())
            {
                dgv.DataSource = null;
                return;
            }

            //將Columns Header先隱藏
            dgv.ColumnHeadersVisible = false;
            dgv.AutoGenerateColumns = false;
            dgv.DataSource = dt;

            ColumnsClear(dgv);
        }


        public void ColumnHeadVisableControl(DataGridView dgv)
        {
            dgv.ColumnHeadersVisible = true;
            dgv.DefaultCellStyle.Font = new System.Drawing.Font("微软正黑体", 12, System.Drawing.FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("微软正黑体", 12, System.Drawing.FontStyle.Bold);
        }

        /// <summary>
        /// 清空ColumnsHeader，否则Header会叠加
        /// </summary>
        private void ColumnsClear(DataGridView dgv)
        {
            RowHeadersVisible(dgv);


            if (dgv.Columns.Count > 0)
            {
                dgv.Columns.Clear();
            }
        }


        /// <summary>
        /// 將DataGridView Row Selection隱藏
        /// </summary>
        /// <param name="dgv"></param>
        private void RowHeadersVisible(DataGridView dgv)
        {
            dgv.RowHeadersVisible = false;
        }

        /// <summary>
        /// 设定DataGridView 标题中文(预设宽度依显示字元数目计算)
        /// </summary>
        /// <param name="strShow"></param>
        /// <param name="strColumnName"></param>
        /// <returns></returns>
        /// 
        public DataGridViewTextBoxColumn Fun_SetGridViewColumn(string strShow, string strColumnName)
        {
            // 显示一个栏位
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                Name = strColumnName,//strShow,
                DataPropertyName = strColumnName,
                HeaderText = strShow
            };
            int intWidth = Encoding.Default.GetBytes(strShow).Length;
            idColumn.Width = 15 * intWidth;
            return idColumn;
        }

        /// <summary>
        /// 设定DataGridView 标题中文(指定宽度)
        /// </summary>
        /// <param name="strShow">要显示的中文</param>
        /// <param name="strColumnName">栏位原始名称</param>
        /// <param name="intWidth">指定栏宽</param>
        /// <returns></returns>
        public DataGridViewTextBoxColumn Fun_SetGridViewColumn(string strShow, string strColumnName, int intWidth)
        {
            // 显示一个栏位
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                Name = strColumnName,//strShow,
                DataPropertyName = strColumnName,
                HeaderText = strShow,
                Width = intWidth
               // , SortMode = DataGridViewColumnSortMode.NotSortable
            };
            return idColumn;
        }

        /// <summary>
        /// 設定DataGridView 標題中文(指定寬度)
        /// </summary>
        /// <param name="strShow">要顯示的中文</param>
        /// <param name="strColumnName">欄位原始名稱</param>
        /// <param name="intWidth">指定欄寬</param>
        /// <param name="intMaxInputLength">输入字元最大值</param>
        /// <returns></returns>
        public DataGridViewTextBoxColumn Fun_SetGridViewColumn(string strShow, string strColumnName, int intWidth, int intMaxInputLength,bool bolNum = false)
        {
            // 顯示一個欄位
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                Name = strColumnName,//strShow,
                DataPropertyName = strColumnName,
                HeaderText = strShow,
                Width = intWidth,
                MaxInputLength = intMaxInputLength                
            };

            if (bolNum)
                idColumn.ValueType = typeof(decimal);

            return idColumn;
        }
    }
}
