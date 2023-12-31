﻿using System;
using System.Data;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class Frm_Defect : Form
    {

        public string Defect_Coil_ID;
        public string Defect_Plan_No;
        public Frm_Defect()
        {
            InitializeComponent();
            
        }

        private void Frm_Defect_Load(object sender, EventArgs e)
        {
           
            string strSql = Frm_3_2_SqlFactory.SQL_Select_DefectData(Defect_Coil_ID, Defect_Plan_No);

            DataTable dtGetDefect = DataAccess.Fun_SelectDate(strSql, "母卷缺陷");
            this.Text = $"母卷号:{Defect_Coil_ID} 缺陷资讯";
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_Defect, dtGetDefect);
            Frm_Defect_ColumnsHandler.Instance.Frm_Defect_Columns(Dgv_Defect);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_Defect);

        }

        private void Frm_Defect_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            // Application.Exit();//通知所有訊息終止，并在終止后關閉所有表單，并釋放資源.
        }
    }
}
