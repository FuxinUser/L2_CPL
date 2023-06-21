using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using DBService.Repository;
using System;
using System.Data;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class Frm_TrackCoil : Form
    {
        public string StrSkidNumber;
        public enum EntrySkid
        { 
            ESK01 = 2,
            ESK02 = 3,
            ETOP = 4
        }


        public Frm_TrackCoil()
        {
            InitializeComponent();
        }

        private void Frm_TrackCoil_Load(object sender, EventArgs e)
        {
            Fun_InitialComboBox();
            Lbl_Title.Text = $"入-插入作业[{StrSkidNumber}]";
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            if(Cob_Coil_No.SelectedValue == null)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请选取钢卷号码", $"警告",3);
                Cob_Coil_No.Focus();
                return;
            }
            string strMessage = $"确认[{StrSkidNumber}]插入钢卷[{Cob_Coil_No.SelectedValue}]?";

            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "入-插入作业", Properties.Resources.dialogQuestion, 1);

            if (dialogR == DialogResult.Cancel) return;

            //if (!Fun_UpdateTrackingMap(Cob_Coil_No.SelectedValue.ToString(), StrSkidNumber))
            //{
            //    EventLogHandler.Instance.EventPush_Message($"入-插入作业失败");

            //    return;
            //}

            Fun_AkkaTellServer(Cob_Coil_No.SelectedValue.ToString(), StrSkidNumber);

            this.Close();
        }


        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // ComboBox 
        private void Fun_InitialComboBox()
        {
            string strSql = Frm_2_1_SqlFactory.SQL_Select_Schedule();

            DataTable dtGetSchedule = DataAccess.Fun_SelectDate(strSql, "入料钢卷清单");

            Cob_Coil_No.DisplayMember = nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID);
            Cob_Coil_No.ValueMember = nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID);
            Cob_Coil_No.DataSource = dtGetSchedule;

        }


        private void Frm_TrackCoil_Shown(object sender, EventArgs e)
        {
           // Fun_InitialComboBox();
          
        }


        private bool Fun_UpdateTrackingMap(string Coil, string Skid)
        {
            bool bol = true;

            string strSql = Frm_2_1_SqlFactory.SQL_Update_Tracking(Skid, Coil);

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "插入作业"))
            {
                EventLogHandler.Instance.EventPush_Message($"入-插入作业失败");
                bol = false;
            }

            return bol;
        }


        private void Fun_AkkaTellServer(string Coil,string Skid)
        {
            SCCommMsg.CS12_Coil_SkidFeed _SkidFeed = new SCCommMsg.CS12_Coil_SkidFeed
            {
                Source = "CPL1_Server",
                ID = "SkidFeed",
                Coil_ID = Coil.Trim()
            };

            switch (Skid)
            {
                case "Entry_SK01":
                    _SkidFeed.Skid = (int)EntrySkid.ESK01;
                    break;
                case "Entry_SK02":
                    _SkidFeed.Skid = (int)EntrySkid.ESK02;
                    break;
                default:
                    break;
            }


            PublicComm.Client.Tell(_SkidFeed);

            string strMessage = $"通知Server 钢卷编号:[{Coil}] 插入 鞍座:[{Skid}] 。 ";

            //DialogHandler.Instance.Fun_DialogShowOk(strMessage, $"插入鞍座[{Skid}]钢卷", 4);
            EventLogHandler.Instance.EventPush_Message(strMessage);
            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()} 插入作业", strMessage);
            PublicComm.ClientLog.Info(strMessage);//通知Server钢卷编号:{Coil} 鞍座:{Skid} 确认入料 
            PublicComm.AkkaLog.Info(strMessage);//$"通知Server钢卷编号:{Coil} 鞍座:{Skid} 确认入料 "
        }
    }
}
