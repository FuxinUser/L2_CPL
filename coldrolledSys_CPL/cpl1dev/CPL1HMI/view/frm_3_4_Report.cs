using OfficeOpenXml;
using System;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace CPL1HMI
{
    public partial class Frm_3_4_Report : Form
    {
        private System.Data.DataTable dtGetList;
        //語系
        private LanguageHandler LanguageHand;
        public Frm_3_4_Report()
        {
            InitializeComponent();
        }

        private void Frm_3_4_Report_Load(object sender, EventArgs e)
        {
           // LanguageHandler.Instance.Fun_CurrentLanguage(this.Name);
            if (PublicForms.Report == null) PublicForms.Report = this;

            Control[] Frm_3_4_Control = new Control[] {
               Btn_Search,
               Btn_ExportExcel,
               Btn_ExportPDF,
               Btn_ExportReport
            };

            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_3_4_Control, UserSetupHandler.Instance.Frm_3_4);

            //班次
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Shift, Cob_shift_no);

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {

            string strSql = Frm_3_4_SqlFactory.SQL_Select_CoilList();

            dtGetList = DataAccess.Fun_SelectDate(strSql, "查询钢卷清单");

            if (dtGetList.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("查无钢卷清单", "查询钢卷清单", 0);

                return;
            }

            Dgv_CoilList.DataSource = dtGetList;
        }

        /// <summary>
        /// 匯出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ExportExcel_Click(object sender, EventArgs e)
        {

            if (Dgv_CoilList.Rows.Count.Equals(0))
            {
                DialogHandler.Instance.Fun_DialogShowOk("无任何清单，请先查询再进行汇出作业!", "汇出Excel", 0);

                return;
            }

            ExcelPackage CoilPackage = new ExcelPackage();

            FileInfo fileInfo = new FileInfo($"{DateTime.Now:yyyyMMdd}報表.xlsx");

            ExcelWorksheet excelWorksheet = CoilPackage.Workbook.Worksheets.Add("Test_WorkSheet");

            Fun_ExporExcel_Setting(excelWorksheet);

            CoilPackage.SaveAs(fileInfo);
           
        }

        /// <summary>
        /// 匯出PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ExportPDF_Click(object sender, EventArgs e)
        {

            if (Dgv_CoilList.Rows.Count.Equals(0))
            {
                DialogHandler.Instance.Fun_DialogShowOk("无任何清单，请先查询再进行汇出作业!", "汇出PDF", 0);

                return;
            }

            // Excel 檔案位置
            string sourcexlsx = $@"C:\Users\asd09\Documents\fuxin\cold_rolled_cpl\cpl1dev\CPL1HMI\bin\Debug\{DateTime.Now:yyyyMMdd}報表.xlsx";

            // PDF 儲存位置
            string targetpdf = $@"C:\Users\asd09\Documents\fuxin\cold_rolled_cpl\cpl1dev\CPL1HMI\bin\Debug\{DateTime.Now:yyyyMMdd}報表.pdf";

            //建立 Excel application instance
            Microsoft.Office.Interop.Excel.Application appExcel = new Microsoft.Office.Interop.Excel.Application();

            //開啟 Excel 檔案
            var xlsxDocument = appExcel.Workbooks.Open(sourcexlsx);

            //匯出為 pdf
            xlsxDocument.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, targetpdf);

            //關閉 Excel 檔
            xlsxDocument.Close();

            //結束 Excel
            appExcel.Quit();
        }


        /// <summary>
        /// Excel 設定
        /// </summary>
        /// <param name="CoilPackage"></param>
        /// <param name="fileInfo"></param>
        /// <param name="excelWorksheet"></param>
        private void Fun_ExporExcel_Setting(ExcelWorksheet excelWorksheet)
        {

            excelWorksheet.Cells["A1"].LoadFromDataTable(dtGetList,true);

            //自動欄寬
            excelWorksheet.Cells[excelWorksheet.Dimension.Address].AutoFitColumns();

            //置中
            excelWorksheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            //轉橫向
            //excelWorksheet.PivotTables[excelWorksheet.Name].DataOnRows = false;

        }
       
    }
}
