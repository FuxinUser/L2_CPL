using System;
using System.Data;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class Frm_SleevePaper : Form
    {
        public Frm_SleevePaper()
        {
            InitializeComponent();
        }
                

        private void Frm_SleevePaper_Load(object sender, EventArgs e)
        {
        }

        private void Fun_Initial_DgvSleeve()
        {
            string strSql = Frm_1_1_SqlFactory.SQL_Select_SleevePaperCount("Out_Sleeve_Type_Code");
            strSql = $"Select * From ({strSql}) T Where Out_Sleeve_Type_Code != '00' ";
            DataTable dtGetSleeveCount = DataAccess.Fun_SelectDate(strSql, "套筒统计");

            //DGV設定
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(DgvSleeve, dtGetSleeveCount);
            Frm_1_1_ColumnsHandler.Instance.Frm_SleevePaer_Sleeve(DgvSleeve);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(DgvSleeve);
            DgvSleeve.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;//.DisplayedCells; 
        }

        private void Fun_Initial_DgvPaper()
        {
            string strSql = Frm_1_1_SqlFactory.SQL_Select_SleevePaperCount("Out_Paper_Code");
            strSql = $"Select * From ({strSql}) T Where Out_Paper_Code != '00' ";
            DataTable dtGetPaperCount = DataAccess.Fun_SelectDate(strSql, "垫纸统计");

            //DGV設定
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(DgvPaper, dtGetPaperCount);
            Frm_1_1_ColumnsHandler.Instance.Frm_SleevePaer_Paper(DgvPaper);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(DgvPaper);
            DgvPaper.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;//.DisplayedCells; 
        }

        // 套筒垫纸资讯 关闭
        private void Btn_MaterialInfoClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Close();
        }

        private void Frm_SleevePaper_Shown(object sender, EventArgs e)
        {
            Fun_Initial_DgvSleeve();
            Fun_Initial_DgvPaper();
        }
    }
}
