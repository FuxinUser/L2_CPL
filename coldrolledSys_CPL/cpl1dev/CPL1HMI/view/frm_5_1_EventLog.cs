using DBService.Repository.EventLog;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CPL1HMI
{
    public partial class frm_5_1_EventLog : Form
    {
        //語系
        private LanguageHandler LanguageHand;

        public frm_5_1_EventLog()
        {
            InitializeComponent();
        }
        private void Frm_5_1_EventLog_Load(object sender, EventArgs e)
        {
            if (PublicForms.EventLog == null) PublicForms.EventLog = this;

            
            //起時-1小時
            Dtp_Start_Time.Value = DateTime.Now.AddHours(-1);

            //訖時+1小時
            Dtp_Finish_Time.Value = DateTime.Now.AddHours(1);

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);

        }

        private void Frm_5_1_EventLog_Shown(object sender, EventArgs e)
        {

            //類別
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.EventLogLevel, Cob_EventType);
                       
            //系統
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.System, Cob_System_ID);
            Cob_System_ID.SelectedIndex = 1;
            //電腦名稱
            Fun_ComboBoxComputerNameList();


            //Fun_SearchByKeyword_Server();

            //Fun_DataGridViewDisplay();

            //Fun_DataTableEvent_TypeToString();

        }


        /// <summary>
        /// 電腦名稱清單
        /// </summary>
        private void Fun_ComboBoxComputerNameList()
        {
            string strSql = "";
            strSql = Frm_5_1_SqlFactory.SQL_Select_FrameGroupComboBoxItems(Cob_System_ID.SelectedIndex);


            DataTable dtComputerName = DataAccess.Fun_SelectDate(strSql, "系統電腦名稱清單");

            if (dtComputerName.IsNull())
            {
                //DialogHandler.Instance.Fun_DialogShowOk("查无系统清单","查询系统電腦名稱清单",0);
                return;
            } 
           
            Cob_ComputerName.ValueMember = nameof(EventLogEntity.TBL_EventLog.FrameGroup_No);
            Cob_ComputerName.DisplayMember = nameof(EventLogEntity.TBL_EventLog.FrameGroup_No);
            Cob_ComputerName.DataSource = dtComputerName;
           
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_QueryOK_Click(object sender, EventArgs e)
        {
            //判断资料笔数
            if (Txt_SelectTop.Text.Trim().Equals(""))
            {
                DialogHandler.Instance.Fun_DialogShowOk("请输入欲查询资料笔数","查询事件记录",0);
                return;
            }

            int DataCount = Convert.ToInt32(Txt_SelectTop.Text.Trim());

            // 查詢判斷: 1.Server 2.Client
            // Server to TBL_EventLog
            // Client to TBL_EventLog_Client
            // if select [1]Server
            // else if select [2]Client

            DataTable dtGetEventLog = Cob_System_ID.SelectedValue.Equals("1") ? Fun_SelectEventLog_Server(DataCount) : Fun_SelectEventLog_Client(DataCount);

            Fun_DataGridViewDisplay(dtGetEventLog);

            Fun_DataTableEvent_TypeToString();
        }

        /// <summary>
        /// DataGridView Display
        /// </summary>
        private void Fun_DataGridViewDisplay(DataTable dt)
        {

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_EventLog, dt);
            Frm_5_1_ColumnsHandler.Instance.Frm_5_1_EventLog(Dgv_EventLog);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_EventLog);

            //DataGridView 自動排序 - 關閉
            foreach (DataGridViewColumn col in Dgv_EventLog.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        /// <summary>
        /// 事件類別文字替換
        /// </summary>
        private void Fun_DataTableEvent_TypeToString()
        {
            if (Dgv_EventLog.Rows.Count.Equals(0))
                return;

            for (int i = 0; i < Dgv_EventLog.Rows.Count; i++)
            {
                switch (Dgv_EventLog.Rows[i].Cells[2].Value.ToString())
                {

                    case "1":
                        Dgv_EventLog.Rows[i].Cells[2].Value = "Error";
                        break;

                    case "2":
                        Dgv_EventLog.Rows[i].Cells[2].Value = "Alarm";
                        break;

                    case "3":
                        Dgv_EventLog.Rows[i].Cells[2].Value = "Info";
                        break;

                    case "4":
                        Dgv_EventLog.Rows[i].Cells[2].Value = "Debug";
                        break;

                    default:
                        break;

                }
            }
        }

        #region SQL

        /// <summary>
        /// 搜尋Server EventLog Table
        /// </summary>
        /// <returns></returns>
        private DataTable Fun_SelectEventLog_Server(int DataCount)
        {
            DataTable dt = null;

            if (Dtp_Start_Time.DateTimeRangeIsFail(Dtp_Finish_Time.Value))
            {
                DialogHandler.Instance.Fun_DialogShowOk("日期区间起讫时间不正确，请重新确认", "查询条件", 0);
            }
            else
            {
                string strSql = Frm_5_1_SqlFactory.SQL_Select_ServerEventLog(Dtp_Start_Time.Value.ToString("yyyy-MM-dd HH"), Dtp_Finish_Time.Value.ToString("yyyy-MM-dd HH"), DataCount);

                dt = DataAccess.Fun_SelectDate(strSql, "事件記錄");
            }

            return dt;

        }

        /// <summary>
        /// 搜尋Client EventLog Table
        /// </summary>
        /// <returns></returns>
        private DataTable Fun_SelectEventLog_Client(int DataCount)
        {
            DataTable dt = null;

            if (Dtp_Start_Time.DateTimeRangeIsFail(Dtp_Finish_Time.Value))
            {
                DialogHandler.Instance.Fun_DialogShowOk("日期区间起讫时间不正确，请重新确认", "查询条件", 0);
            }
            else
            {
                string strSql = Frm_5_1_SqlFactory.SQL_Select_ClientEventLog(Dtp_Start_Time.Value.ToString("yyyy-MM-dd HH"), Dtp_Finish_Time.Value.ToString("yyyy-MM-dd HH"), DataCount);

                dt = DataAccess.Fun_SelectDate(strSql, "事件記錄");
            }

            return dt;

        }

        #endregion

        /// <summary>
        /// DataGridView Cells Click 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgEventLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Dgv_EventLog.Rows.Count.Equals(0))
            {
                return;
            }

            Txt_Event_Description.Text = Dgv_EventLog.CurrentRow.Cells[3].Value.ToString().Trim();

            Txt_Command.Text = Dgv_EventLog.CurrentRow.Cells[4].Value.ToString().Trim();

        }

        private void Dgv_EventLog_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //if (dtGetEventLog.IsNull())
            //    return;
            if (Dgv_EventLog.DgvIsNull())
                return;

            if (Dgv_EventLog.Rows.Count != 0)
            {
                //foreach (DataGridViewRow row in Dgv_RollHistory.Rows)
                //{

                //string rollStatus = dtHistory.Rows[e.RowIndex]["RollUse_Status"].ToString();
                string strType = Dgv_EventLog.Rows[e.RowIndex].Cells[nameof(EventLogEntity.TBL_EventLog.Event_Type)].Value.ToString().Trim();

                if (strType == "Error")
                    Dgv_EventLog.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightPink;
                else if(strType == "Alarm")
                    Dgv_EventLog.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 255, 128);
                else if (strType == "Info")
                    Dgv_EventLog.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                else if (strType == "Debug")
                    Dgv_EventLog.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;


            }
        }

        private void Fun_LanguageIsEn_Font14_12_Chk(object sender, EventArgs e)
        {
            FontStyle fs = ((System.Windows.Forms.CheckBox)sender).Font.Style;
            FontFamily ffm = ((System.Windows.Forms.CheckBox)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((CheckBox)sender).Font = new Font(ffm, (float)14.25, fs);
            else
                ((CheckBox)sender).Font = new Font(ffm, (float)12, fs);
        }

    }
}
