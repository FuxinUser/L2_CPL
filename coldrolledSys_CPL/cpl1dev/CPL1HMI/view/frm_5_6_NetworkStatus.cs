using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static CPL1HMI.DataBaseTableFactory;

namespace CPL1HMI
{
    public partial class frm_5_6_NetworkStatus : Form
    {
        //語系
        private LanguageHandler LanguageHand;
        public frm_5_6_NetworkStatus()
        {
            InitializeComponent();
        }


        private void Frm_5_6_NetworkStatus_Load(object sender, EventArgs e)
        {
            if (PublicForms.NetworkStatus == null) PublicForms.NetworkStatus = this;
             
            Fun_SelectNetWorkStatus();

            TimerForRefresh.Start();

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }


        private void Timer_ForRefresh_Tick(object sender, EventArgs e)
        {
            Fun_SelectNetWorkStatus();
        }


        private void Fun_SelectNetWorkStatus()
        {
            string strSql = Frm_5_6_SqlFactory.SQL_Select_SystemStatus();

            DataTable dtGetStatus = DataAccess.Fun_SelectDate(strSql, "连线状态");

            if (dtGetStatus.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("查无连线状态", "连线状态", 0);

                return;
            } 

            Fun_NetWorkStatusDisplay(dtGetStatus);
        }


        private void Fun_NetWorkStatusDisplay(DataTable dt)
        {
            for (int Index = 0; Index < dt.Rows.Count; Index++)
            {
                
                //Send MMS
                if (dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("MMS"))
                {
                    Lbl_SendMMS_Color.BackColor = dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Trim().Equals("1") ? Color.Lime : Color.Red;
                }
                //Rev MMS
                else if (dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_From)].ToString().Trim().Equals("MMS"))
                {
                    Lbl_RevMMS_Color.BackColor = dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ? Color.Lime : Color.Red;
                }
                //Send L2.5
                else if (dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("LEVEL25"))
                {
                    Lbl_SendL25_Color.BackColor = dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ? Color.Lime : Color.Red;
                }
                //Send WMS
                else if (dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("WMS"))
                {
                    Lbl_SendWMS_Color.BackColor = dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ? Color.Lime : Color.Red;
                }
                //Rev WMS
                else if (dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_From)].ToString().Trim().Equals("WMS"))
                {
                    Lbl_RevWMS_Color.BackColor = dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ? Color.Lime : Color.Red;
                }
                //PLC
                else if (dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("LEVEL1"))
                {
                    Lbl_PLC_Color.BackColor = dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ? Color.Lime : Color.Red;
                }
            }
        }
    }
}
