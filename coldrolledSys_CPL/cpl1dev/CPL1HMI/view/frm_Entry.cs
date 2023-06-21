using Akka.Actor;
using DataModel.HMIServerCom.Msg;
using System;
using System.Data;
using System.Windows.Forms;
using static CPL1HMI.DataBaseTableFactory;

namespace CPL1HMI
{
    public enum EntrySkid
    {
        ESK01 = 2,
        ESK02 = 3,
        ETOP = 4
    }
    public partial class frm_Entry : Form
    {
        #region 變數
        public string Skid = string.Empty;
        #endregion

        public frm_Entry()
        {
            InitializeComponent();
        }
        private void Frm_Entry_Load(object sender, EventArgs e)
        {
            PublicForms.Entry = this;
            Fun_SelectScheduleList();
        }
        public void Fun_SelectScheduleList()
        {
            lbSkid.Text = $"鞍座 : {Skid}";

            string strSql = SqlFactory.frm_Entry_SelectedSchedule_DB_Schedule_PDI();

            DataTable dtGetSchedule = DataAccess.Fun_SelectDate(strSql, GlobalVariableHandler.Instance.strConn_CPL, "入料钢卷清单", "2-1");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(dgv_off, dtGetSchedule);
            DGVColumnsHandler.Instance.Frm_Entry(dgv_off);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(dgv_off);
        }
       
        /// <summary>
        /// 確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_True_Click(object sender, EventArgs e)
        {
            DialogResult dialogR = MessageBox.Show("確認儲存?", "提示", MessageBoxButtons.OKCancel);
            if (dialogR == DialogResult.OK)
            {
                string strSql = $@"Update [{nameof(TBL_CoilMap)}] set 
                              [{Skid}] = '{dgv_off.CurrentRow.Cells[0].Value}'";

                Fun_DataBaseAction(strSql);
               
                SCCommMsg.CS12_Coil_SkidFeed _SkidFeed = new SCCommMsg.CS12_Coil_SkidFeed
                {
                    Source = "CPL1_Server",
                    ID = "SkidFeed",
                    Coil_ID = dgv_off.CurrentRow.Cells[0].Value.ToString().Trim()
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

                //刪除排程
                //strSql = SqlFactory.Frm_1_1_DeleteSchedule_DB_Schedule(dgv_off.CurrentRow.Cells[0].Value.ToString());
                //DataAccess.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_CPL, "删除排程");

                PublicComm.client.Tell(_SkidFeed);
                EventLogHandler.Instance.EventPush_Message($"通知Server钢卷编号:{dgv_off.CurrentRow.Cells[0].Value.ToString().Trim()} 鞍座:{Skid} 确认入料 ");
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}入料作业", $"通知Server钢卷编号:{dgv_off.CurrentRow.Cells[0].Value.ToString().Trim()} 鞍座:{Skid} 确认入料 ");
                PublicComm.ClientLog.Info($"通知Server钢卷编号:{dgv_off.CurrentRow.Cells[0].Value.ToString().Trim()} 鞍座:{Skid} 确认入料 ");
                PublicComm.akkaLog.Info($"通知Server钢卷编号:{dgv_off.CurrentRow.Cells[0].Value.ToString().Trim()} 鞍座:{Skid} 确认入料 ");
                Close();
            }
            else
            {
                EventLogHandler.Instance.EventPush_Message($"取消入料");
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}入料作业", $"入料作业取消");
            }
        }
        private void Fun_DataBaseAction(string strSql)
        {
            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, GlobalVariableHandler.Instance.strConn_CPL, "入料作业", "2-1"))
            {
                EventLogHandler.Instance.EventPush_Message($"入料失败");
                return;
            }
        }
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
