using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using System;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class frm_Scan : Form
    {
        string strSql = "";
        public frm_Scan()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PublicForms.frm_Scan = this;
        }
        private void chk_PDI_CoilID_CheckedChanged(object sender, EventArgs e)
        {
            chk_Scan_CoilID.Checked = chk_PDI_CoilID.Checked == true ? chk_Scan_CoilID.Checked = false: chk_Scan_CoilID.Checked = true;
            
        }
        private void chk_Scan_CoilID_CheckedChanged(object sender, EventArgs e)
        {
            chk_Scan_CoilID.Checked = chk_PDI_CoilID.Checked == true ? chk_Scan_CoilID.Checked = false : chk_Scan_CoilID.Checked = true;
        }

        private void btn_Scan_ok_Click(object sender, EventArgs e)
        {
            string Coil_ID = "";
            Coil_ID = chk_Scan_CoilID.Checked == true ? Coil_ID = chk_Scan_CoilID.Text : Coil_ID = chk_PDI_CoilID.Text;
            UpdateDB(Coil_ID);
        }
        private void UpdateDB(string Coil_ID)
        {
            strSql = SqlFactory.frm_Scan_UpdateScanCheck_DB_PDI(Coil_ID);
            try
            {
                DataAccess.GetInstance().ExecuteNonQuery(strSql, GlobalVariableHandler.Instance.strConn_CPL);
                EventLogHandler.Instance.EventPush_Message("", "扫描结果提示! 选择之钢卷编号:「"+ Coil_ID + "」!");
                EventLogHandler.Instance.Log(System_ID.Client, "2-1",Event_Type.Info, "使用者:" + PublicForms.Main.lblLoginUser.Text + "确认扫描结果", "扫描结果确认 钢卷编号:"+Coil_ID);
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.EventPush_Message("", $"警告! 語法:{strSql}有錯誤:{ex.ToString()}");
            }
            SCCommMsg.CS04_RenameCoil _RenameCoil = new SCCommMsg.CS04_RenameCoil();
            _RenameCoil.Source = "CPL1_HMI";
            _RenameCoil.ID = "RenameCoil";
            _RenameCoil.Coil_ID = Coil_ID;
            PublicComm.client.Tell(_RenameCoil);
            EventLogHandler.Instance.Log(System_ID.Client, "2-1",Event_Type.Info, "使用者:" + PublicForms.Main.lblLoginUser.Text + "通知Server确认扫描结果", "通知Server扫描结果确认 钢卷编号:" + Coil_ID);
        }
    }
}
