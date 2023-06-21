using CPL1HMI.Helper;
using DBService.Repository.Leader;
using DBService.Repository.PDI;
using System;
using System.Data;
using System.Windows.Forms;


namespace CPL1HMI
{
    public partial class Frm_LeaderStripSelectioncs : Form
    {
        public string Coil_ID = string.Empty;
        public string Skid = string.Empty;
        private bool isEditStatuts = true;
        private string Out_Coil_ID = string.Empty;

        public Frm_LeaderStripSelectioncs()
        {
            InitializeComponent();
        }

        private void Frm_LeaderStripSelectioncs_Load(object sender, EventArgs e)
        {
            //Fun_ClearText();
            PublicForms.frm_Leader = this;

            txtCoil_ID.Text = Coil_ID.Trim();

            txtSkid.Text = Skid;

            Fun_SteelGradeComboBoxItems();
 
            var coilLeader = Fun_SelectCoilLeader(Coil_ID);
            
            if (coilLeader.IsNull()) {
                DialogHandler.Instance.Fun_DialogShowOk("無导带資料", "导带资料", 0);
                    Close();
                return;
            }

            if (coilLeader.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Coil_ID)].ToString().Equals(string.Empty))
                isEditStatuts = false;

            DisplayLeaderData(coilLeader);          
        }


        /// <summary>
        /// 查詢鋼捲的導帶資料
        /// </summary>
        private DataTable Fun_SelectCoilLeader(string entryCoilID)
        {

            string strSql = Frm_2_1_SqlFactory.SQL_Select_LeaderData(entryCoilID);
            DataTable dtGetStripData = DataAccess.Fun_SelectDate(strSql, "导带资料");
            return dtGetStripData;

        }

        /// <summary>
        /// 清空文字
        /// </summary>
        private void Fun_ClearText()
        {
            foreach (Control ctrl in pnl_Leader.Controls)
            {
                if (ctrl is TextBox || ctrl is ComboBox) ctrl.Text = string.Empty;
            }
            
        }

        /// <summary>
        /// 鋼種選單
        /// </summary>
        private void Fun_SteelGradeComboBoxItems()
        {
            Fun_SelectSteelGrade(HST_NO);
            Fun_SelectSteelGrade(TST_NO);
        }

        /// <summary>
        /// 鋼種清單
        /// </summary>
        private void Fun_SelectSteelGrade(ComboBox cbo)
        {
            string strSql = Frm_3_1_SqlFactory.SQL_Select_ComboBoxItems(nameof(CoilPDIEntity.TBL_PDI.St_No), true);
            DataTable dtSt_No = DataAccess.Fun_SelectDate(strSql, "钢种清单");

            if (dtSt_No.IsNull())
            {
                cbo.DataSource = null;
                return;
            }

           cbo.DisplayMember = nameof(CoilPDIEntity.TBL_PDI.St_No);
           cbo.ValueMember = nameof(CoilPDIEntity.TBL_PDI.St_No);
           cbo.DataSource = dtSt_No;
        }


        private void DisplayLeaderData(DataTable dtGetStripData)
        {
            Out_Coil_ID = dtGetStripData.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_ID)].ToString();


            //頭段導帶剛種
            HST_NO.Text = dtGetStripData.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_St_No)].ToString();
            //頭段導帶長度
            HeadStrip_Length.Text = dtGetStripData.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Length)].ToString();
            //頭段導帶寬度
            HeadStrip_Width.Text = dtGetStripData.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Width)].ToString();
            //頭段導帶厚度
            HeadStrip_Thickness.Text = dtGetStripData.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Thickness)].ToString();


            //尾段導帶剛種
            TST_NO.Text = dtGetStripData.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_St_No)].ToString();
            //尾段導帶長度
            TailStrip_Length.Text = dtGetStripData.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Length)].ToString();
            //尾段導帶寬度
            TailStrip_Width.Text = dtGetStripData.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Width)].ToString();
            //尾段導帶厚度
            TailStrip_Thickness.Text = dtGetStripData.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Thickness)].ToString();
        }
        private void ValidtionText()
        {

            //頭段導帶剛種
            HST_NO.Text.AssertTextEmpty("頭段導帶剛種未填");
            //頭段導帶長度
            HeadStrip_Length.Text.AssertTextEmpty("頭段導帶長度未填");
            //頭段導帶寬度
            HeadStrip_Width.Text.AssertTextEmpty("頭段導帶寬度未填");
            //頭段導帶厚度
            HeadStrip_Thickness.Text.AssertTextEmpty("頭段導帶厚度未填");


            //尾段導帶剛種
            TST_NO.Text.AssertTextEmpty("尾段導帶剛種未填");
            //尾段導帶長度
            TailStrip_Length.Text.AssertTextEmpty("尾段導帶長度未填");
            //尾段導帶寬度
            TailStrip_Width.Text.AssertTextEmpty("尾段導帶寬度未填");
            //尾段導帶厚度
            TailStrip_Thickness.Text.AssertTextEmpty("尾段導帶厚度未填");

        }



        /// <summary>
        /// 確定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Sure_Click(object sender, EventArgs e)
        {
            // Invaild 
            var invaildOK = InvaildViewTextHelp.Invaild(() => ValidtionText());
            if (!invaildOK)
                return;

            string strSql = isEditStatuts ? Frm_2_1_SqlFactory.SQL_Update_LeaderData(Coil_ID, Out_Coil_ID) : Frm_2_1_SqlFactory.SQL_Insert_LeaderData(Coil_ID,Out_Coil_ID);

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "导带资料输入"))
            {
                DialogHandler.Instance.Fun_DialogShowOk("导带资料储存失败", "导带资料输入", 3);
                return;
            }

            DialogHandler.Instance.Fun_DialogShowOk("导带资料储存成功", "导带资料输入", 4);

            Close();
            
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HST_NO_Click(object sender, EventArgs e)
        {
            Fun_SelectSteelGrade(HST_NO);
        }

        private void TST_NO_Click(object sender, EventArgs e)
        {
            Fun_SelectSteelGrade(TST_NO);
        }

      
      
    }
}
