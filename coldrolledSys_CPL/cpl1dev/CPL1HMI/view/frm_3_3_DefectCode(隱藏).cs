using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class frm_3_3_DefectCode : Form
    {
        public frm_3_3_DefectCode()
        {
            InitializeComponent();
        }

        private void frm_DefectCode_Test_Load(object sender, EventArgs e)
        {
            dgv_List.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataTable dtOn = new DataTable();
            dtOn = CommonDef.getDemoData(@"demodata\2-2缺陷資料.csv");
            dgv_List.DataSource = dtOn;
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            dgv_List.ReadOnly = false;
            dgv_List.Columns[0].ReadOnly = true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

            dgv_List.ReadOnly = true;
        }
    }
}
