using System;
using System.Data;
using System.Windows.Forms;
using Akka.Actor;
using System.Drawing;
using DataModel.HMIServerCom.Msg;
using DBService.Repository;
using DBService.Repository.LineStatus;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using DBService.Repository.SystemSetting;
using DBService.Repository.Leader;
using static DBService.Repository.ConnectionStatus.ConnectionStatusEntity;
using System.Text;

namespace CPL1HMI
{
    public partial class Frm_2_1_Tracking : Form
    {
        #region 變數
        DataTable  dtOff, dtOn;
        DataTable dtGetConnectionStatus = new DataTable();
        ToolTip Tip = new ToolTip();
        string strTip = string.Empty;
        string MouseDown_CoilID = string.Empty;

        //語系
        private LanguageHandler LanguageHand;

        #endregion

        public Frm_2_1_Tracking()
        {
            InitializeComponent();
        }

        #region frmLoad
        private void Frm_2_1_Tracking_Load(object sender, EventArgs e)
        {
            // LanguageHandler.Instance.Fun_CurrentLanguage(this.Name);
            if (PublicForms.Tracking == null) PublicForms.Tracking = this;
            Control[] Frm_2_1_Control = new Control[] {
            Btn_AutoCoilFeed,//進料模式
            Btn_Strip_X,//導帶資料
            Btn_ETOP_ManualFeed,//入料
            Btn_ESK01_Entry,//ESK01 入料
            Btn_ESK02_Entry,//ESK02 入料
            Btn_ESK01Reject,//ESK01 退料
            Btn_ESK02_Reject,//ESK02 退料
            Btn_ETOP_Reject,//ETOP 退料
            Btn_ESK01_PrintLabel,//ESK01 列印標籤
            Btn_ESK02_PrintLabel,//ESK02 列印標籤
            Btn_ETOP_PrintLabel,//ETOP 列印標籤
            Btn_ESK01_Del,//ESK01 刪除
            Btn_ESK02_Del,//ESK02 刪除
            Btn_ETOP_Del,//ETOP 刪除
            Btn_POR_Reject,//開捲機 退料
            Btn_PDO_X,//PDO確認
            Btn_DSK02_Weight_X,//DSK02 秤重
            Btn_DSK01_CoilOut,//DSK01 出料
            Btn_DSK02_CoilOut,//DSK02 出料
            Btn_DSK01_PrintLabel,//DSK01 列印標籤
            Btn_DSK02_PrintLabel,//DSK02 列印標籤
            Btn_DTOP_PrintLabel,//DTOP 列印標籤
            Btn_DSK01_Del,//DSK01 刪除
            Btn_DSK02_Del,//DSK02 刪除
            Btn_DTOP_Del//DTOP 刪除
             };

            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_2_1_Control, UserSetupHandler.Instance.Frm_2_1, PublicForms.Tracking);

            //Pic_Track_Picture.Image = GlobalVariableHandler.proLine.Equals("CPL1") ? Properties.Resources.CPL1_Tracking : Pic_Track_Picture.Image = Properties.Resources.CPL2_Tracking ;
            if (GlobalVariableHandler.proLine.Equals("CPL1"))
            {
                Pic_Track_Picture.Image = Properties.Resources.CPL1_Tracking;
                Pic_Track_Picture.Location = new Point(449, 34);//449, 94
                Pic_Track_Picture.Size = new Size(1042, 273);

                Pic_Track_Picture_W_L.Image = Properties.Resources.CPL1_Tracking_W_L;
                Pic_Track_Picture_W_L.Location = new Point(541, 119);//541, 179
                Pic_Track_Picture_W_L.Size = new Size(109, 147);

                Pic_Track_Picture_W_R.Image = Properties.Resources.CPL1_Tracking_W_R;
                Pic_Track_Picture_W_R.Location = new Point(1386, 168);//1386, 228
                Pic_Track_Picture_W_R.Size = new Size(106, 114);
            }
            else
            {
                Pic_Track_Picture.Image = Properties.Resources.CPL2_Tracking;
                Pic_Track_Picture.Location = new Point(449, 34);//449, 94
                Pic_Track_Picture.Size = new Size(1042, 273);

                Pic_Track_Picture_W_L.Image = Properties.Resources.CPL2_Tracking_W_L;
                Pic_Track_Picture_W_L.Location = new Point(542,112);//542, 172
                Pic_Track_Picture_W_L.Size = new Size(120, 156);

                Pic_Track_Picture_W_R.Image = Properties.Resources.CPL2_Tracking_W_R;
                Pic_Track_Picture_W_R.Location = new Point(1375,160);//1375, 220
                Pic_Track_Picture_W_R.Size = new Size(115, 126);
            }
            //進料模式
            Lbl_EntryMode.Text = Fun_SelectSystemSetting().Rows[0][nameof(SystemSettingEntity.TBL_SystemSetting.Value)].ToString().Trim().Equals("1") ? "Auto" : "Manual";

            //料
            Btn_ETOP_ManualFeed.Visible = !Fun_SelectSystemSetting().Rows[0][nameof(SystemSettingEntity.TBL_SystemSetting.Value)].ToString().Trim().Equals("1");

            Fun_FormShown();

            //Timer Start
            Fun_TimerStart();

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }

        private void Fun_FormShown()
        {
            Fun_PORCoilFlag();

            //進料狀態
            Fun_SelectSystemSetting();

            //Tracking Map
            try
            {
                Fun_SelectTrackingMap();
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk("钢卷追踪查询资料库失败", "钢卷追踪", 3);

                EventLogHandler.Instance.LogDebug("2-1", $"钢卷追踪", $"钢卷追踪查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"鋼卷追蹤查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"鋼卷追蹤查詢資料庫失敗:{ex}");
            }

            //開/收卷、產線速度
            try
            {
                POR_LineSpeed_TR();
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk("开收卷机资讯查询失败", "开收卷机资讯", 3);

                EventLogHandler.Instance.LogDebug("2-1", $"开收卷机资讯", $"开收卷机资讯查询失败:{ex}");
                PublicComm.ClientLog.Debug($"開收卷機資訊查詢失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"開收卷機資訊查詢失敗:{ex}");
            }

            
            //未上線鋼卷詳細資料
            try
            {
                Fun_DgoffDataTableDisplay();
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk("未上线钢卷资料查询资料库失败", "未上线钢卷资料", 3);

                EventLogHandler.Instance.LogDebug("2-1", $"未上线钢卷资料", $"未上线钢卷资料查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"未上線鋼卷查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"未上線鋼卷查詢資料庫失敗:{ex}");
            }

            //線上鋼卷詳細資料
            try
            {
                Fun_DgoOnDataTableDisplay();
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk("线上钢卷资料查询资料库失败", "线上钢卷资料", 3);

                EventLogHandler.Instance.LogDebug("2-1", $"线上钢卷资料", $"线上钢卷资料查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"線上鋼卷查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"線上鋼卷查詢資料庫失敗:{ex}");
            }

            //取得連線狀態
            try
            {
                Fun_SelectNetWorkStatus();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug("2-1", $"2-1连线状态", $"2-1连线状态查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"2-1连线状态查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"2-1连线状态查詢資料庫失敗:{ex}");
            }
        }

        private void Fun_TimerStart()
        {
            //Tracking Map Timer
            Timer_TrackingMap.Start();

            //開收卷機資訊Timer
            Timer_ProcessData.Start();
        }

        /// <summary>
        /// TrackingMap刷新 5秒一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_TrackingMap_Tick(object sender, EventArgs e)
        {
            //Tracking Map
            try
            {
                Fun_SelectTrackingMap();
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk("钢卷追踪查询资料库失败", "钢卷追踪", 3);

                EventLogHandler.Instance.LogDebug("2-1", $"钢卷追踪", $"钢卷追踪查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"鋼卷追蹤查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"鋼卷追蹤查詢資料庫失敗:{ex}");
            }
           
            if (Chk_DGV_Reflash.Checked)
            {
                //未上線鋼卷詳細資料
                try
                {
                    Fun_DgoffDataTableDisplay();
                }
                catch (Exception ex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk("未上线钢卷资料查询资料库失败", "未上线钢卷资料", 3);

                    EventLogHandler.Instance.LogDebug("2-1", $"未上线钢卷资料", $"未上线钢卷资料查询资料库失败:{ex}");
                    PublicComm.ClientLog.Debug($"未上線鋼卷查詢資料庫失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"未上線鋼卷查詢資料庫失敗:{ex}");
                }

                //線上鋼卷詳細資料
                try
                {
                    Fun_DgoOnDataTableDisplay();
                }
                catch (Exception ex)
                {
                    DialogHandler.Instance.Fun_DialogShowOk("线上钢卷资料查询资料库失败", "线上钢卷资料", 3);

                    EventLogHandler.Instance.LogDebug("2-1", $"线上钢卷资料", $"线上钢卷资料查询资料库失败:{ex}");
                    PublicComm.ClientLog.Debug($"線上鋼卷查詢資料庫失敗:{ex}");
                    PublicComm.ExceptionLog.Debug($"線上鋼卷查詢資料庫失敗:{ex}");
                }
            }

            //取得連線狀態
            try
            {
                Fun_SelectNetWorkStatus();
            }
            catch (Exception ex)
            {
                EventLogHandler.Instance.LogDebug("2-1", $"2-1连线状态", $"2-1连线状态查询资料库失败:{ex}");
                PublicComm.ClientLog.Debug($"2-1连线状态查詢資料庫失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"2-1连线状态查詢資料庫失敗:{ex}");
            }
        }

        /// <summary>
        /// 開/收卷張力、電流，產線速度 1秒一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_ProcessData_Tick(object sender, EventArgs e)
        {
            //開/收卷、產線速度
            try
            {
                POR_LineSpeed_TR();
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk("开收卷机资讯查询失败", "开收卷机资讯", 3);

                EventLogHandler.Instance.LogDebug("2-1", $"开收卷机资讯", $"开收卷机资讯查询失败:{ex}");
                PublicComm.ClientLog.Debug($"開收卷機資訊查詢失敗:{ex}");
                PublicComm.ExceptionLog.Debug($"開收卷機資訊查詢失敗:{ex}");
            }
        }

        /// <summary>
        /// 開/收卷張力、電流及產線速度
        /// </summary>
        private void POR_LineSpeed_TR()
        {
            string strSql = Frm_2_1_SqlFactory.SQL_Select_ProcessData();
            DataTable dtGetProcessData = DataAccess.Fun_SelectDate(strSql, "開/收卷張力、電流及產線速度");

            if (dtGetProcessData.IsNull()) return;

            Lbl_LineSpeed.Text = dtGetProcessData.Rows[0][nameof(ProcessDataEntity.TBL_ProcessData.LINE_Speed_Actual)].ToString();
            Lbl_POR_Tension.Text = $@"{dtGetProcessData.Rows[0][nameof(ProcessDataEntity.TBL_ProcessData.POR_Tension_Actual)]}/{dtGetProcessData.Rows[0][nameof(ProcessDataEntity.TBL_ProcessData.POR_Tension_Set)]}";
            Lbl_POR_Current.Text = dtGetProcessData.Rows[0][nameof(ProcessDataEntity.TBL_ProcessData.POR_Current_Actual)].ToString();
            Lbl_TR_Tension.Text = $@"{dtGetProcessData.Rows[0][nameof(ProcessDataEntity.TBL_ProcessData.TR_Tension_Actual)]}/{dtGetProcessData.Rows[0][nameof(ProcessDataEntity.TBL_ProcessData.TR_Tension_Set)]}";
            Lbl_TR_Current.Text = dtGetProcessData.Rows[0][nameof(ProcessDataEntity.TBL_ProcessData.TR_Current_Actual)].ToString();
        }

        #region Tracking map

        /// <summary>
        /// 搜尋Tracking Map Table
        /// </summary>
        public void Fun_SelectTrackingMap()
        {
            string strSql = Frm_2_1_SqlFactory.SQL_Select_TrackingMap();
            DataTable dt = DataAccess.Fun_SelectDate(strSql, "钢卷追踪");

            if (dt.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("钢卷追踪查无资料", "钢卷追踪", 0);

                return;
            } 


            #region Tracking Map Insert to Lable

            //ECAR
            Lbl_ECar_CoilNo.Text = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.Entry_Car)].ToString().Trim();
            //ESK01
            Lbl_ESK01_CoilNo.Text = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.Entry_SK01)].ToString().Trim();
            //ESK02
            Lbl_ESK02_CoilNo.Text = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.Entry_SK02)].ToString().Trim();
            //ETOP
            Lbl_ETOP_CoilNo.Text = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.Entry_TOP)].ToString().Trim();

            //POR
            Lbl_POR_CoilNo.Text= dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.POR)].ToString().Trim();
            Pic_Track_Picture_W_L.Visible = string.IsNullOrEmpty(Lbl_POR_CoilNo.Text);
            //TR
            Lbl_TR_CoilNo.Text = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.TR)].ToString().Trim();
            Pic_Track_Picture_W_R.Visible = string.IsNullOrEmpty(Lbl_TR_CoilNo.Text);

            //DSK01
            Lbl_DSK01_CoilNo.Text = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK01)].ToString().Trim();
            //DSK02
            Lbl_DSK02_CoilNo.Text = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK02)].ToString().Trim();
            //DTOP
            Lbl_DTOP_CoilNo.Text = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.Delivery_TOP)].ToString().Trim();
            //DCAR
            Lbl_DCar_CoilNo.Text = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.Delivery_Car)].ToString().Trim();

            #endregion
            //ESK01 //Entry_SK01
            if (!string.IsNullOrEmpty( dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.Entry_SK01)].ToString().Trim()))
            {
                string strCoil = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.Entry_SK01)].ToString().Trim();
                string strSql_Skid = Frm_2_1_SqlFactory.SQL_Select_PDItoSkid(strCoil);

                 DataTable dtSkid_Pdi = DataAccess.Fun_SelectDate(strSql_Skid, "Skid_PDI");
                if (!dtSkid_Pdi.IsNull())
                {
                    Lbl_Por_ESK01_St_No.Text = dtSkid_Pdi.Rows[0][nameof(CoilPDIEntity.TBL_PDI.St_No)].ToString();
                    Lbl_Por_ESK01_Leader.Text = Fun_ChangeShow(dtSkid_Pdi.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)].ToString());
                    Lbl_Por_ESK01_Trim.Text = Fun_ChangeShow(dtSkid_Pdi.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)].ToString());
                    Lbl_Por_ESK01_Dividing.Text = Fun_ChangeShow(dtSkid_Pdi.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)].ToString());
                    Lbl_Por_ESK01_Paper.Text = dtSkid_Pdi.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Paper_Code)].ToString();//衬纸
                }               
            }
            else
            {
                Lbl_Por_ESK01_St_No.Text = "";
                Lbl_Por_ESK01_Leader.Text = "";
                Lbl_Por_ESK01_Trim.Text = "";
                Lbl_Por_ESK01_Dividing.Text = "";
                Lbl_Por_ESK01_Paper.Text = "";
            }
            //ESK02 //Entry_SK02
            if (!string.IsNullOrEmpty(dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.Entry_SK02)].ToString().Trim()))
            {
                string strCoil = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.Entry_SK02)].ToString().Trim();
                string strSql_Skid = Frm_2_1_SqlFactory.SQL_Select_PDItoSkid(strCoil);

                DataTable dtSkid_Pdi = DataAccess.Fun_SelectDate(strSql_Skid, "Skid_PDI");
                if (!dtSkid_Pdi.IsNull())
                {
                    Lbl_Por_ESK02_St_No.Text = dtSkid_Pdi.Rows[0][nameof(CoilPDIEntity.TBL_PDI.St_No)].ToString();
                    Lbl_Por_ESK02_Leader.Text = Fun_ChangeShow(dtSkid_Pdi.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)].ToString());
                    Lbl_Por_ESK02_Trim.Text = Fun_ChangeShow(dtSkid_Pdi.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)].ToString());
                    Lbl_Por_ESK02_Dividing.Text = Fun_ChangeShow(dtSkid_Pdi.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)].ToString());
                    Lbl_Por_ESK02_Paper.Text = dtSkid_Pdi.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Paper_Code)].ToString();
                }
            }
            else
            {
                Lbl_Por_ESK02_St_No.Text = "";
                Lbl_Por_ESK02_Leader.Text = "";
                Lbl_Por_ESK02_Trim.Text = "";
                Lbl_Por_ESK02_Dividing.Text = "";
                Lbl_Por_ESK02_Paper.Text = "";
            }
            #region 取得出口卷毛重 
            Txt_DSK01_Weight.Text = Fun_GetOut_Coil_Gross_WT(Lbl_DSK01_CoilNo.Text);

            Txt_DSK02_Weight.Text = Fun_GetOut_Coil_Gross_WT(Lbl_DSK02_CoilNo.Text);

            Txt_DTOP_Weight.Text = Fun_GetOut_Coil_Gross_WT(Lbl_DTOP_CoilNo.Text);
            #endregion


            #region --- Line Status ---

            //入口端狀態
            Lbl_EntryStatus.BackColor = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.LineStatus_Entry)].ToString().Equals("0") ? Color.Red : Color.Green; 

            //產線狀態
            Lbl_CPLStatus.BackColor = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.LineStatus_CPL)].ToString().Equals("0") ? Color.Red : Color.Green; 

            //出口端狀態
            Lbl_ExitStatus.BackColor = dt.Rows[0][nameof(CoilMapEntity.TBL_CoilMap.LineStatus_Exit)].ToString().Equals("0") ? Color.Red : Color.Green;

            #endregion

            ColorChange(Lbl_ESK01_CoilNo);
            ColorChange(Lbl_ESK02_CoilNo);
            ColorChange(Lbl_ETOP_CoilNo);
            ColorChange(Lbl_ECar_CoilNo);
            ColorChange(Lbl_DSK01_CoilNo);
            ColorChange(Lbl_DSK02_CoilNo);
            ColorChange(Lbl_DTOP_CoilNo);
            ColorChange(Lbl_DCar_CoilNo);
        }

        private string Fun_GetOut_Coil_Gross_WT(string strPdoCoilNo)
        {
            string strSql = Frm_2_1_SqlFactory.SQL_Select_Out_Coil_Gross_WT(strPdoCoilNo);
            DataTable dt = DataAccess.Fun_SelectDate(strSql, "出口Skid_出口卷毛重");

            if (dt.IsNull())
            {               
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        #endregion

        #region 變色判斷

        #region 入口段變色判斷

        /// <summary>
        /// 入口變色事件
        /// 鞍座上鋼捲號變更即判斷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fun_EntryColorChange(object sender, EventArgs e)
        {
            Label lb = sender as Label;

            ColorChange(lb);

            //if (!lb.Text.Trim().IsEmpty())
            //{
            //    Fun_EntryCoil_PDISelect(lb.Text, lb);
            //    EventLogHandler.Instance.LogDebug("2-1", $"入口段变色 钢卷编号: [{lb.Text}]", strTip);
            //    PublicComm.ClientLog.Info($"訊息名稱:2-1入口端變色 訊息: 钢卷编号 [{lb.Text}] {strTip}");
            //}
            //else
            //{
            //    LableColorReset(lb);
            //}
        }

        private void ColorChange(Label lb)
        {
            if (!lb.Text.Trim().IsEmpty())
            {
                Fun_EntryCoil_PDISelect(lb.Text, lb);
                //EventLogHandler.Instance.LogDebug("2-1", $"入口段变色 钢卷编号: [{lb.Text}]", strTip);
                //PublicComm.ClientLog.Info($"訊息名稱:2-1入口端變色 訊息: 钢卷编号 [{lb.Text}] {strTip}");
            }
            else
            {
                LableColorReset(lb);
            }
        }


        /// <summary>
        /// 入口鞍座上鋼卷資料
        /// </summary>
        private void Fun_EntryCoil_PDISelect(string Coil_ID, Label lb)
        {
            string strSql = Frm_2_1_SqlFactory.SQL_Select_EntrySkidCoilFlag(Coil_ID);
            DataTable dt_EntryCheckSaddle = DataAccess.Fun_SelectDate(strSql, "入口段鞍座钢卷资料");

            //Tip清空
            strTip = string.Empty;

            //确认PDI及掃描扫描状态
            Scan_PDI_Info_Check(dt_EntryCheckSaddle);

            //确认导带资料
            Strip_Info_Check(dt_EntryCheckSaddle);

            //ToolTip setup
            Label_ToolTip(lb);
        }
        #endregion


        #region 出口段變色判斷

        /// <summary>
        /// 出口變色事件
        /// 鞍座上鋼捲號變更即判斷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fun_ExitColorChange(object sender, EventArgs e)
        {
            Label lb = sender as Label;

            if (!lb.Text.Trim().IsEmpty())
            {
                Fun_DeliveryCoil_PDOSelect(lb.Text, lb);
                //EventLogHandler.Instance.LogDebug("2-1", $"出口段变色 钢卷编号: [{lb.Text}]", strTip);
                //PublicComm.ClientLog.Info($"訊息名稱:2-1出口端變色 訊息: 钢卷编号 [{lb.Text}] {strTip}");
            }
            else
            {
                LableColorReset(lb);
            }
        }


        /// <summary>
        /// 出口鞍座上鋼卷資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        /// <param name="lb"></param>
        private void Fun_DeliveryCoil_PDOSelect(string Coil_ID, Label lb)
        {
            string strSql = Frm_2_1_SqlFactory.SQL_Select_DeliverySkidCoilFlag(Coil_ID);
            DataTable dt_ExitCheckSaddle = DataAccess.Fun_SelectDate(strSql, "出口段鞍座钢卷资料");

            //Tip清空
            strTip = string.Empty;

            //确认PDO扫描状态
            Scan_PDO_Info_Check(dt_ExitCheckSaddle);

            //确认PDO是否有上传
            PDO_Uploaded_Flag(dt_ExitCheckSaddle);

            //ToolTip setup
            Label_ToolTip(lb);
        }

        #endregion


        /// <summary>
        /// 判斷導帶資訊是否未輸入
        /// </summary>
        /// <param name="Coil_ID"></param>
        private void Strip_Info_Check(DataTable dt)
        {
            if (!dt.IsNull())
            {
                if (!dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)].ToString().Trim().Equals("0")) 
                    return;

                if (dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)].ToString().Trim().Equals("1"))
                {
                    if (dt.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_St_No)].ToString().IsEmpty())
                    {
                        strTip += "导带资料未输入" + Environment.NewLine;
                    }
                }
            }
        }

        /// <summary>
        /// 判斷PDI鋼卷掃描結果
        /// </summary>
        /// <param name="Coil_ID"></param>
        private void Scan_PDI_Info_Check(DataTable dt)
        {

            if (dt.IsNull())
            {
                strTip += "钢卷尚未收到PDI" + Environment.NewLine;
                return;
            }

            if (!dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)].ToString().
               Equals(dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Scaned_Coil_ID)].ToString()))
            {
                if (!dt.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID_Checked)].ToString().Trim().Equals("1"))
                {
                    strTip += "钢卷编号扫描确认未完成" + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 判斷PDO鋼卷掃描結果
        /// </summary>
        private void Scan_PDO_Info_Check(DataTable dt)
        {

            if (!dt.IsNull())
            {
                if (!dt.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString().Trim().Equals(dt.Rows[0][nameof(PDOEntity.TBL_PDO.Exit_Scaned_CoilID)].ToString().Trim()))
                {
                    if (!dt.Rows[0][nameof(PDOEntity.TBL_PDO.Exit_CoilID_Checked)].ToString().Trim().Equals("1"))
                    {
                        strTip += "钢卷编号扫描确认未完成" + Environment.NewLine;
                    }
                }
            }
            else if (dt.IsNull())
            {
                strTip += "钢卷编号未扫描" + Environment.NewLine;
            }
        }
       
        /// <summary>
        /// PDO上传确认
        /// </summary>
        /// <param name="lb"></param>
        private void PDO_Uploaded_Flag(DataTable dt)
        {
            if (!dt.IsNull())
            {
                if (!dt.Rows[0][nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)].ToString().Trim().Equals("1") || dt.Rows[0][nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)] == null)
                {
                    strTip += "PDO未上传" + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// ToolTip
        /// </summary>
        private void Label_ToolTip(Label lb)
        {
            lb.BackColor = strTip.IsEmpty() ? Color.Transparent : Color.Red;
            lb.ForeColor = strTip.IsEmpty() ? Color.Black : Color.White;
            //提示視窗類型
            Tip.ToolTipIcon = ToolTipIcon.Info;
            //標題
            Tip.ToolTipTitle = "提示";

            Tip.SetToolTip(lb, strTip);
        }


        private void LableColorReset(Label lb)
        {
            if (lb.Text.IsEmpty())
            {
                lb.BackColor = Color.Transparent;
                lb.ForeColor = Color.Black;
            }
        }

        #endregion

        #region dgv

        private void Fun_DgoffDataTableDisplay()
        {
            //帶前十筆未上線鋼卷排程
            string strSql;//= Frm_2_1_SqlFactory.SQL_Select_Top10_Schedule();
            strSql = Frm_1_1_SqlFactory.SQL_Select_Production_Schedule(10);
            dtOff = DataAccess.Fun_SelectDate(strSql, "未上线钢卷资料");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_OffLine, dtOff);
            Frm_2_1_ColumnsHandler.Instance.Frm_2_1_Schedule(Dgv_OffLine);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_OffLine);

        }

        /// <summary>
        /// 未上线钢卷未收到PDI变色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_off_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (Dgv_OffLine.Rows.Count.Equals(0)) return;

            for (int RowIndex = 0; RowIndex < Dgv_OffLine.Rows.Count; RowIndex++)
            {
                if (RowIndex <= 2)
                {
                    Dgv_OffLine.Rows[RowIndex].DefaultCellStyle.BackColor = Color.DimGray;
                }
                Dgv_OffLine.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void Fun_DgoOnDataTableDisplay()
        {
            //帶線上鋼卷
            string strSql = Frm_2_1_SqlFactory.SQL_Select_TrackingMapCoilInfo();
            dtOn = DataAccess.Fun_SelectDate(strSql, "线上钢卷资料");

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_OnLine, dtOn);
            Frm_2_1_ColumnsHandler.Instance.Frm_2_1_TrackingMap(Dgv_OnLine);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_OnLine);
        }

        #endregion

        #endregion


        #region 入料

        /// <summary>
        /// ESK01
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ESK01Entry_Click(object sender, EventArgs e)
        {
            if (Lbl_ESK01_CoilNo.Text.IsEmpty()) 
            { 
                EntryCoil("Entry_SK01"); 
            }
            else
            {
                DialogHandler.Instance.Fun_DialogShowOk("ESkid1鞍座尚有鋼卷", "入料", 0);

                PublicComm.ClientLog.Info($"ESkid1鞍座尚有鋼卷不可進料");
            } 
        }

        /// <summary>
        /// ESK02
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ESK02Entry_Click(object sender, EventArgs e)
        {
            if (Lbl_ESK02_CoilNo.Text.IsEmpty()) 
            { 
                EntryCoil("Entry_SK02"); 
            }
            else
            {
                DialogHandler.Instance.Fun_DialogShowOk("ESkid2鞍座尚有鋼卷", "入料", 0);

                PublicComm.ClientLog.Info($"ESkid2鞍座尚有鋼卷不可進料");
            }
        }

        /// <summary>
        /// 開啟入料畫面
        /// </summary>
        /// <param name="Skid"></param>
        private void EntryCoil(string Skid)
        {
            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text }开启入料画面", $"开启{Skid}入料画面");

            Frm_TrackCoil _TrackCoil = new Frm_TrackCoil
            {
                StrSkidNumber = Skid
            };

            _TrackCoil.ShowDialog();
            _TrackCoil.Dispose();
        }
       
        #region 自動入料

        private void Btn_AutoCoilFeed_Click(object sender, EventArgs e)
        {
            string strMessage = $"确定变更入料模式?";
            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "入料模式", Properties.Resources.dialogQuestion, 1);
            if (dialogR == DialogResult.Cancel) return;

            string strSql = string.Empty;

            DataTable dtStatus = Fun_SelectSystemSetting();

            if (dtStatus.IsNull()) return;

            if (dtStatus.Rows[0][nameof(SystemSettingEntity.TBL_SystemSetting.Value)].ToString().Trim().Equals("0"))
            {

                //開始自動入料
                Lbl_EntryMode.Text = "Auto";

                strSql = Frm_2_1_SqlFactory.SQL_Update_AutoFeedStatus(AutoFeedStatus.Auto);

                Btn_ETOP_ManualFeed.Visible = false;

            }
            else if (dtStatus.Rows[0][nameof(SystemSettingEntity.TBL_SystemSetting.Value)].ToString().Trim().Equals("1"))
            {

                //取消自動入料
                Lbl_EntryMode.Text = "Manual";

                strSql = Frm_2_1_SqlFactory.SQL_Update_AutoFeedStatus(AutoFeedStatus.Manual);

                //手动入料按钮
                Btn_ETOP_ManualFeed.Visible = true;

            }

            
            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "自动入料状态"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"自动入料状态变更失败", "自动入料", 3);

                return;
            }
               

            EventLogHandler.Instance.LogDebug("2-1", "修改资料库", $"修改自动入料状态 Table[TBL_SystemSetting]，成功 状态更变:{Lbl_EntryMode.Text}");

            PublicComm.ClientLog.Info($"目前入料状态为:{Lbl_EntryMode.Text}");

            SCCommMsg.CS10_Coil_AutoFeedModeChange _AutoFeedMode = new SCCommMsg.CS10_Coil_AutoFeedModeChange
            {
                Source = "CPL1_HMI",
                ID = "AutoFeedModeChange"
            };

            PublicComm.Client.Tell(_AutoFeedMode);

            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}变更自动入料状态", $"通知Server自动入料状态变更为 :{Lbl_EntryMode.Text}");

            DialogHandler.Instance.Fun_DialogShowOk($"通知Server自动进料状态变更为:{Lbl_EntryMode.Text}", "自动入料", 4);

            PublicComm.ClientLog.Info($"通知Server入料狀態變更為:{Lbl_EntryMode.Text}");
            PublicComm.AkkaLog.Info($"通知Server入料狀態變更為:{Lbl_EntryMode.Text}");
        }


        private DataTable Fun_SelectSystemSetting()
        {
            string strSql = Frm_2_1_SqlFactory.SQL_Select_AutoFeedStatus();

            DataTable dtStatus = DataAccess.Fun_SelectDate(strSql, "系统设定值");

            return dtStatus;
        }

        #endregion

        #region 手動入料

        private void Btn_ManualFeed_Click(object sender, EventArgs e)
        {
            if (Lbl_ETOP_CoilNo.Text.IsEmpty() && Lbl_ESK02_CoilNo.Text.IsEmpty())
            {
               

                //手動進料
                SCCommMsg.CS11_Coil_ManualFeed _ManualFeed = new SCCommMsg.CS11_Coil_ManualFeed
                {
                    Source = "CPL1_HMI",
                    CoilID = dtOff.Rows[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].ToString().Trim()

                };

                PublicComm.Client.Tell(_ManualFeed);

                DialogHandler.Instance.Fun_DialogShowOk($"已通知Server告知WMS進料，鋼卷編號【{dtOff.Rows[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)]}】", "手动入料",4);

                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{ PublicForms.Main.Lbl_LoginUser.Text} 手动入料", $"已通知Server告知WMS進料，入料钢卷编号:{ dtOff.Rows[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)]}");
                PublicComm.ClientLog.Info($"已通知Server告知WMS進料，鋼卷號:[{dtOff.Rows[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)]}]");
                PublicComm.AkkaLog.Info($"已通知Server告知WMS進料，鋼卷號:[{dtOff.Rows[0][nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)]}]");
            }
            else
            {
                DialogHandler.Instance.Fun_DialogShowOk($"目前鞍座无空间可供进料", "手动入料", 0);

                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{ PublicForms.Main.Lbl_LoginUser.Text} 手动入料", $"目前鞍座无空间可供进料");
                PublicComm.ClientLog.Info($"目前鞍座無空間可供進料]");
            }
        }

        #endregion

        #endregion


        #region - 退料 -

        /// <summary>
        /// ETOP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ETOPReject_Click(object sender, EventArgs e)
        {
            DelReturn_TrackingMapColumns("ETOP", Lbl_ETOP_CoilNo.Text);
        }

        /// <summary>
        /// ESK02
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ESK02Reject_Click(object sender, EventArgs e)
        {
            DelReturn_TrackingMapColumns("ESK02", Lbl_ESK02_CoilNo.Text);
        }

        /// <summary>
        /// ESK01
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ESK01Reject_Click(object sender, EventArgs e)
        {
            DelReturn_TrackingMapColumns("ESK01", Lbl_ESK01_CoilNo.Text);
        }
       
        /// <summary>
        /// 退料
        /// </summary>
        /// <param name="action"></param>
        /// <param name="Coil_ID"></param>
        /// <param name="Saddle"></param>
        private void DelReturn_TrackingMapColumns(string Saddle, string Coil_ID)
        {

            if (Coil_ID.Trim().Equals(string.Empty))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"[{Saddle}]鞍座无钢卷可退料", "退料作业", 0);

                PublicComm.ClientLog.Info($"[{Saddle}]鞍座无钢卷可退料");
                return;
            }

            Frm_DialogReject _DialogReject = new Frm_DialogReject()
            { 
                Coil = Coil_ID.Trim(),
                ParentCoil = Coil_ID.Trim(),
                SkidNumber = Saddle
            };

            _DialogReject.Fun_CoilReject();
            _DialogReject.ShowDialog();
            _DialogReject.Dispose();
        }

        #endregion


        /// <summary>
        /// POR斷帶處理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_StripBreak_Click(object sender, EventArgs e)
        {

            StripBreakHandler.Instance.Fun_StripBreak(Lbl_POR_CoilNo.Text.Trim());

        }


        #region 標籤

        //ESK01
        private void BtnESK01_PrintLabel_Click(object sender, EventArgs e)
        {
            PrintTag(Lbl_ESK01_CoilNo.Text);
        }

        //ESK02
        private void BtnESK02_PrintLabel_Click(object sender, EventArgs e)
        {
            PrintTag(Lbl_ESK02_CoilNo.Text);
        }

        //ETOP
        private void BtnETOP_PrintLabel_Click(object sender, EventArgs e)
        {
            PrintTag(Lbl_ETOP_CoilNo.Text);
        }

        //DSK01
        private void BtnDT21PrintLabel_Click(object sender, EventArgs e)
        {
            PrintTag(Lbl_DSK01_CoilNo.Text);
        }

        //DSK02
        private void BtnDT22PrintLabel_Click(object sender, EventArgs e)
        {
            PrintTag(Lbl_DSK02_CoilNo.Text);
        }

        //DTOP
        private void BtnDT23PrintLabel_Click(object sender, EventArgs e)
        {
            PrintTag(Lbl_DTOP_CoilNo.Text);
        }

        /// <summary>
        /// 列印標籤
        /// </summary>
        /// <param name="Coil_ID"></param>
        private void PrintTag(string Coil_ID)
        {
            if (!Coil_ID.Trim().Equals(string.Empty))
            {
                Frm_PrintLabels frm_Print = new Frm_PrintLabels
                {
                    Str_Coil_No = Coil_ID.Trim()
                };
                frm_Print.ShowDialog();
                frm_Print.Dispose();

                if (frm_Print.DialogResult == DialogResult.OK)
                {
                    string strShowText = $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()} 打印钢卷号:[{frm_Print.Str_Coil_No}]标签";
                    EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()} 打印标签作业", $"打印钢卷号:[{frm_Print.Str_Coil_No}]标签");
                    EventLogHandler.Instance.EventPush_Message(strShowText);
                    PublicComm.ClientLog.Info(strShowText);

                }

                //SCCommMsg.CS07_PrintLabel _PrintLabel = new SCCommMsg.CS07_PrintLabel
                //{
                //    Source = "CPL1_HMI",
                //    ID = "PrintLabel",
                //    CoilID = Coil_ID.Trim()
                //};
                //PublicComm.Client.Tell(_PrintLabel);

                //DialogHandler.Instance.Fun_DialogShowOk($"已通知Server列印[{Coil_ID.Trim()}]标签", "列印标签", 4);

                //EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text}打印标签作业", $"打印{Coil_ID.Trim()}标签");

                //PublicComm.ClientLog.Info($"通知Server列印鋼卷號:[{Coil_ID.Trim()}]標籤");
            }
            else
            {
                DialogHandler.Instance.Fun_DialogShowOk($"鞍座无钢卷可列印", "列印标签", 0);

                PublicComm.ClientLog.Info($"清單尚無鋼卷可供列印");
            }
            
        }

        #endregion


        /// <summary>
        /// 確認開卷機上的鋼捲註記
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lbl_POR_Coil_ID_TextChanged(object sender, EventArgs e)
        {
            Fun_PORCoilFlag();
        }


        private void Fun_PORCoilFlag()
        {
            Frn_GroupBoxClear();

            if (Lbl_POR_CoilNo.Text.IsEmpty()) return;

            string strSql = Frm_2_1_SqlFactory.SQL_Select_CoilFlagChecked(Lbl_POR_CoilNo.Text);
            DataTable dtFlg = DataAccess.Fun_SelectDate(strSql, "钢卷注记");

            if (dtFlg.IsNull()) return;

            if (GlobalVariableHandler.proLine.Equals("CPL1"))
            {
                //切邊(裁邊)要求
                Fun_TrimFlag(dtFlg);
            }

            Fun_DividingFlag(dtFlg);

            //有分切就不會有導帶
            if (dtFlg.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)].ToString().Equals("0"))
            {
                Fun_StripFlg(dtFlg);
            }

            Lbl_ProcessCode_X.Text = dtFlg.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Process_Code)].ToString();
        }

        /// <summary>
        /// 清空裁邊機資料、導帶資訊、分卷資訊
        /// </summary>
        private void Frn_GroupBoxClear()
        {
            string strClear = "-";// string.Empty;
            #region 裁边清空

            Grb_Trim.Visible = true;
            //gb_Trim.Enabled = false;
            //目標寬度
            Lbl_OutWidth.Text = strClear;

            //最大值
            Lbl_WidthMax.Text = strClear;

            //最小值
            Lbl_WidthMin.Text = strClear;

            #endregion

            #region 导带清空

            Grb_Leader_Strip.Visible = true;
            //groupStrip.Enabled = false;
            //頭段
            Lbl_Head_St_no.Text = strClear;
            Lbl_Head_Length.Text = strClear;
            Lbl_Head_Width.Text = strClear;
            Lbl_Head_Thickness.Text = strClear;

            //尾段
            Lbl_Tail_St_no.Text = strClear;
            Lbl_Tail_Length.Text = strClear;
            Lbl_Tail_Width.Text = strClear;
            Lbl_Tail_Thickness.Text = strClear;
            #endregion

            #region 分卷清空
            Grb_Dividing.Visible = true;
            //gb_Dividing.Enabled = false;
            //分卷數量
            Lbl_Dividing_num1.Text = strClear;

            //目標重量1~6
            Lbl_Order_Weight_1.Text = strClear;
            Lbl_Order_Weight_2.Text = strClear;
            Lbl_Order_Weight_3.Text = strClear;
            Lbl_Order_Weight_4.Text = strClear;
            Lbl_Order_Weight_5.Text = strClear;
            Lbl_Order_Weight_6.Text = strClear;
            #endregion

        }

        /// <summary>Z
        /// 裁邊
        /// </summary>
        /// <param name="Trim"></param>
        private void Fun_TrimFlag(DataTable Trim)
        {
            #region 清空
            Grb_Trim.Visible = true;

            //目標寬度
            Lbl_OutWidth.Text = string.Empty;

            //最大值
            Lbl_WidthMax.Text = string.Empty;

            //最小值
            Lbl_WidthMin.Text = string.Empty;

            #endregion

            if (Trim.IsNull()) return;

            if (Trim.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)].ToString().Equals("1"))
            {
                //切邊要求 : [1]須切邊
                Grb_Trim.Visible = true;
                //gb_Trim.Enabled = true;
                //目標寬度
                Lbl_OutWidth.Text = Trim.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width)].ToString();

                //最大值
                Lbl_WidthMax.Text = Trim.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Max)].ToString();

                //最小值
                Lbl_WidthMin.Text = Trim.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Out_Coil_Width_Min)].ToString();
            }
            else if (Trim.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Trim_Flag)].ToString().Equals("0"))
            {
                //切邊要求 : [0]不須切邊
                Grb_Trim.Visible = true;
                //gb_Trim.Enabled = false;
            }
        }

        /// <summary>
        /// 分卷
        /// </summary>
        /// <param name="Dividing"></param>
        private void Fun_DividingFlag(DataTable Dividing)
        {
            #region 清空
            //groupDivision.BackColor = Color.Silver;
            Grb_Dividing.Visible = true;
            //gb_Dividing.Enabled = false;
            //分卷數量
            Lbl_Dividing_num1.Text = string.Empty;
            //目標重量1~6
            Lbl_Order_Weight_1.Text = string.Empty;
            Lbl_Order_Weight_2.Text = string.Empty;
            Lbl_Order_Weight_3.Text = string.Empty;
            Lbl_Order_Weight_4.Text = string.Empty;
            Lbl_Order_Weight_5.Text = string.Empty;
            Lbl_Order_Weight_6.Text = string.Empty;
            #endregion

            if (Dividing.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)].ToString().Equals("1"))
            {
                //分捲標記 : [1]分捲
                Grb_Dividing.Visible = true;
                //gb_Dividing.Enabled = true;
                //分卷數量
                Lbl_Dividing_num1.Text = Dividing.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Dividing_Num)].ToString();

                //目標重量1~6
                Lbl_Order_Weight_1.Text = Dividing.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Orderwt_1)].ToString();
                Lbl_Order_Weight_2.Text = Dividing.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Orderwt_2)].ToString();
                Lbl_Order_Weight_3.Text = Dividing.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Orderwt_3)].ToString();
                Lbl_Order_Weight_4.Text = Dividing.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Orderwt_4)].ToString();
                Lbl_Order_Weight_5.Text = Dividing.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Orderwt_5)].ToString();
                Lbl_Order_Weight_6.Text = Dividing.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Orderwt_6)].ToString();
            }
            else if(Dividing.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Dividing_Flag)].ToString().Equals("0"))
            {
                //分捲標記 : [0]不分捲
                Grb_Dividing.Visible = true;
                //gb_Dividing.Enabled = false;
            }
        }

        /// <summary>
        /// 导带
        /// </summary>
        /// <param name="Strip"></param>
        private void Fun_StripFlg(DataTable Strip)
        {
            #region 清空
            //groupStrip.BackColor = Color.Silver;
            Grb_Leader_Strip.Visible = true;//
            //groupStrip.Enabled = false;
            //頭段
            Lbl_Head_St_no.Text = string.Empty;
            Lbl_Head_Length.Text = string.Empty;
            Lbl_Head_Width.Text = string.Empty;
            Lbl_Head_Thickness.Text = string.Empty;
            //尾段
            Lbl_Tail_St_no.Text = string.Empty;
            Lbl_Tail_Length.Text = string.Empty;
            Lbl_Tail_Width.Text = string.Empty;
            Lbl_Tail_Thickness.Text = string.Empty;
            #endregion

            if (Strip.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)].ToString().Equals("1"))
            {
                //導帶使用 : [1]使用
                Grb_Leader_Strip.Visible = true;
                //groupStrip.Enabled = true;
                //頭段
                Lbl_Head_St_no.Text = Strip.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_St_No)].ToString();
                Lbl_Head_Length.Text = Strip.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Length)].ToString();
                Lbl_Head_Width.Text = Strip.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Width)].ToString();
                Lbl_Head_Thickness.Text = Strip.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Thickness)].ToString();

                //尾段
                Lbl_Tail_St_no.Text = Strip.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_St_No)].ToString();
                Lbl_Tail_Length.Text = Strip.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Length)].ToString();
                Lbl_Tail_Width.Text = Strip.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Width)].ToString();
                Lbl_Tail_Thickness.Text = Strip.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Thickness)].ToString();
            }
            else if (Strip.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Leader_Flag)].ToString().Equals("0"))
            {
                //導帶使用 : [0]不使用
                Grb_Leader_Strip.Visible = true;
                //groupStrip.Enabled = false;
            }
        }

        /// <summary>
        /// 判斷PDO Table有沒有這筆鋼卷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbDTOPCoilID_TextChanged(object sender, EventArgs e)
        {
            if (!Lbl_DTOP_CoilNo.Text.IsEmpty())
            {
                string strSql = Frm_2_1_SqlFactory.SQL_Select_PDOChecked(Lbl_DTOP_CoilNo.Text);
                DataTable dtPDO = DataAccess.Fun_SelectDate(strSql,$"钢卷号[{Lbl_DTOP_CoilNo.Text.Trim()}]PDO确认");
              
                if (dtPDO.IsNull())
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"尚无钢卷号[{Lbl_DTOP_CoilNo.Text.Trim()}]PDO", "出口端DTOP提示",0);

                    EventLogHandler.Instance.LogDebug("2-1", $"钢卷号[{Lbl_DTOP_CoilNo.Text.Trim()}]PDO确认", $"尚无钢卷号[{Lbl_DTOP_CoilNo.Text.Trim()}]PDO");
                    PublicComm.ClientLog.Debug($"尚無鋼卷號[{Lbl_DTOP_CoilNo.Text.Trim()}]PDO");
                }
            }
        }
       
        /// <summary>
        /// 导带资料 (保留)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Strip_Click(object sender, EventArgs e)
        {
            Frm_LeaderStripSelectioncs frm_LeaderStrip = new Frm_LeaderStripSelectioncs();
            frm_LeaderStrip.ShowDialog();
            frm_LeaderStrip.Dispose();
            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text} 开启导带资料画面", "开启导带资料输入画面");
        }

        /// <summary>
        /// ESK01导带资料输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ESK01_Leader_Click(object sender, EventArgs e)
        {
            Fun_LeaderDataInsert(Lbl_ESK01_CoilNo.Text, "ESK01");
        }

        /// <summary>
        /// ESK02导带资料输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ESK02_Leader_Click(object sender, EventArgs e)
        {
            Fun_LeaderDataInsert(Lbl_ESK02_CoilNo.Text, "ESK02");
        }

        /// <summary>
        /// ETOP导带资料输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ETOP_Leader_Click(object sender, EventArgs e)
        {
            Fun_LeaderDataInsert(Lbl_ETOP_CoilNo.Text, "ETOP");
        }

        /// <summary>
        /// 开启导带资料输入视窗
        /// </summary>
        private void Fun_LeaderDataInsert(string Coil_ID,string Skid)
        {
            Frm_LeaderStripSelectioncs frm_LeaderStrip = new Frm_LeaderStripSelectioncs
            {
                Coil_ID = Coil_ID,
                Skid = Skid
            };

            frm_LeaderStrip.ShowDialog();
            frm_LeaderStrip.Dispose();

            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text} 开启导带资料画面", "开启导带资料输入画面");
        }



        #region 天車入料掃描

        /// <summary>
        /// 天車入料Server通知，
        /// </summary>
        /// <param name="Message"></param>
        public void Handle_SC06_CraneEntryCoil(SCCommMsg.SC06_CraneEntryCoil Message)
        {
            Pnl_Scan.Visible = true;
            Pnl_Scan.BringToFront();
            Pnl_Scan.Size = new Size(350, 250);
            Pnl_Scan.Location = new Point(470, 274);

            Lbl_Scan_Desc.Text = "是否以扫描钢卷号码 :";
            Lbl_Scan_CoilNo_Title.Text = Message.CoilID.Trim();
            Lbl_Scan_SkidNo.Text = Message.CoilPosition.ToString();
        }

        private void Btn_ScanYes_Click(object sender, EventArgs e)
        {

            SCCommMsg.CS18_CarneEntryCoilSelect CarneEntry = new SCCommMsg.CS18_CarneEntryCoilSelect
            {
                coilID = Lbl_Scan_CoilNo_Title.Text,
                SKPos = Fun_EnumCoilSkPosition(Lbl_Scan_SkidNo.Text)
            };

            PublicComm.Client.Tell(CarneEntry);

            DialogHandler.Instance.Fun_DialogShowOk($"已通知Server天车入料钢卷号选择結果[{Lbl_Scan_CoilNo_Title.Text}]", "天车入料", 4);

            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}天车入料钢卷号不一致", $"已通知Server天车入料钢卷号选择結果[{Lbl_Scan_CoilNo_Title.Text}]");
            PublicComm.ClientLog.Info($"已通知Server天车入料钢卷号选择結果[{Lbl_Scan_CoilNo_Title.Text}]");
            PublicComm.AkkaLog.Info($"已通知Server天车入料钢卷号选择結果[{Lbl_Scan_CoilNo_Title.Text}]");

            Pnl_Scan.Visible = false;
        }

        private SCCommMsg.CoilSkPosition Fun_EnumCoilSkPosition(string Skid)
        {
            if (Skid == nameof(SCCommMsg.CoilSkPosition.ETOP))
                return SCCommMsg.CoilSkPosition.ETOP;
            else if (Skid == nameof(SCCommMsg.CoilSkPosition.ESK02))
                return SCCommMsg.CoilSkPosition.ESK02;
            else
                return SCCommMsg.CoilSkPosition.ETOP;
        }

        private void Btn_ScanNo_Click(object sender, EventArgs e)
        {
            Pnl_Scan.Visible = false;
            Pnl_Scan.SendToBack();
        }

        #endregion

        #region 右鍵要求PDI

        /// <summary>
        /// 選單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AckPDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strSql = Frm_2_1_SqlFactory.SQL_Select_PDIChecked(MouseDown_CoilID);
            DataTable dtGetPDI = DataAccess.Fun_SelectDate(strSql, "钢卷是否有PDI");


            if (dtGetPDI.IsNull())
            {
                SCCommMsg.CS02_AckPDI Msg = new SCCommMsg.CS02_AckPDI
                {
                    Source = "GPL_HMI",
                    ID = "AckPDI",
                    Coil_ID = MouseDown_CoilID.Trim()
                };
                PublicComm.Client.Tell(Msg);
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}通知Server Tracking请求PDI 钢卷编号:{MouseDown_CoilID.Trim()}", $"通知Server Tracking请求PDI 钢卷编号:{MouseDown_CoilID.Trim()}");
                EventLogHandler.Instance.EventPush_Message($"已通知Server要求钢卷编号:[{MouseDown_CoilID.Trim() }]PDI!");
                PublicComm.ClientLog.Info($"已通知Server要求[{MouseDown_CoilID.Trim()}] PDI");
            }
            else
            {
                EventLogHandler.Instance.EventPush_Message($"钢卷编号:[{MouseDown_CoilID }]已有PDI!");
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}Tracking请求PDI 钢卷编号:{MouseDown_CoilID.Trim()}", $"Tracking请求PDI 钢卷编号:{MouseDown_CoilID.Trim()}该钢卷已有PDI");
                PublicComm.ClientLog.Info($"鋼卷號:[{MouseDown_CoilID.Trim()}]已有PDI");
            }
        }

        /// <summary>
        /// dgoff mouse down
        /// 未上线钢卷DGV请求PDIcbo_ReturnCode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgoff_MouseDown(object sender, MouseEventArgs e)
        {
            Dgv_MouseDown(e);
        }

        /// <summary>
        /// ESK01 Mouse Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbESK01_MouseDown(object sender, MouseEventArgs e)
        {
            SK_MouseDown(Lbl_ESK01_CoilNo, e);
        }

        /// <summary>
        /// ESK02 Mouse Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbESK02_MouseDown(object sender, MouseEventArgs e)
        {
            SK_MouseDown(Lbl_ESK02_CoilNo, e);
        }

        /// <summary>
        /// ETOP Mouse Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbETOP_MouseDown(object sender, MouseEventArgs e)
        {
            SK_MouseDown(Lbl_ETOP_CoilNo, e);
        }

        /// <summary>
        /// 左右鍵判斷
        /// 滑鼠左右鍵都會觸發，點擊瞬間就會觸發
        /// 點擊右鍵，預存鞍座上鋼卷編號
        /// </summary>
        /// <param name="e"></param>
        private void SK_MouseDown(Label lb, MouseEventArgs e)
        {
            if (e.Button.Equals( MouseButtons.Right))
            {
                if (lb.Text.IsEmpty()) return;

                MouseDown_CoilID = lb.Text;
                EventLogHandler.Instance.EventPush_Message($"Tracking鞍座上请求钢卷编号:[{MouseDown_CoilID.Trim() }]PDI!");
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}要求PDI", $"Tracking鞍座上请求钢卷编号:[{MouseDown_CoilID.Trim() }]PDI!");
                PublicComm.ClientLog.Info($"鞍座要求鋼卷號:[{MouseDown_CoilID.Trim()}]PDI");
                CS02_AckPDI_Tell();
            }
        }

        private void Dgv_MouseDown(MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                if (Dgv_OffLine.CurrentIsNull()) return;
                if (Dgv_OffLine.DgvIsNull()) return;

                MouseDown_CoilID = Dgv_OffLine.CurrentRow.Cells[nameof(CoilScheduleEntity.TBL_Production_Schedule.Coil_ID)].Value.ToString();
                EventLogHandler.Instance.EventPush_Message($"上线钢卷资料列请求钢卷编号:[{MouseDown_CoilID.Trim()}]PDI!");
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}要求PDI", $"上线钢卷资料列请求钢卷编号:[{MouseDown_CoilID.Trim() }]PDI!");
                PublicComm.ClientLog.Info($"未上線鋼卷清單要求鋼卷號:[{MouseDown_CoilID.Trim()}]PDI");

                CS02_AckPDI_Tell();
            }

        }

        private void CS02_AckPDI_Tell()
        {
            SCCommMsg.CS02_AckPDI Msg = new SCCommMsg.CS02_AckPDI
            {
                Source = "CPL1_HMI",
                ID = "AckPDI",
                Coil_ID = MouseDown_CoilID
            };
            PublicComm.Client.Tell(Msg);
            EventLogHandler.Instance.EventPush_Message($"已通知Server要求钢卷号[{MouseDown_CoilID.Trim()}]PDI");
            EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}要求PDI", $"已通知Server要求钢卷号[{MouseDown_CoilID.Trim()}]PDI");
            PublicComm.ClientLog.Info($"已通知Server要求鋼卷號:[{MouseDown_CoilID.Trim()}]PDI");
            PublicComm.AkkaLog.Info($"已通知Server要求鋼卷號:[{MouseDown_CoilID.Trim()}]PDI");
        }


        #endregion

        #region "刪除鞍座鋼卷號"

        #region "入口端"
        //ESK01
        private void Btn_ESK01_Del_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_ESK01_CoilNo.Text, nameof(CoilMapEntity.TBL_CoilMap.Entry_SK01));
        }

        //ESK02
        private void Btn_ESK02_Del_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_ESK02_CoilNo.Text, nameof(CoilMapEntity.TBL_CoilMap.Entry_SK02));
        }

        //ETOP
        private void Btn_ETOP_Del_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_ETOP_CoilNo.Text, nameof(CoilMapEntity.TBL_CoilMap.Entry_TOP));
        }

        /// <summary>
        /// POR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PORReject_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_POR_CoilNo.Text, nameof(CoilMapEntity.TBL_CoilMap.POR));
        }

        #endregion

        #region "出口端"

        //DSK01
        private void Btn_DSK01_Del_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_DSK01_CoilNo.Text, nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK01));
        }

        //DSK02
        private void Btn_DSK02_Del_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_DSK02_CoilNo.Text, nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK02));
        }

        //DTOP
        private void Btn_DTOP_Del_Click(object sender, EventArgs e)
        {
            Fun_DeleteSkidCoilId(Lbl_DTOP_CoilNo.Text, nameof(CoilMapEntity.TBL_CoilMap.Delivery_TOP));
        }
              
        #endregion

        /// <summary>
        /// 刪除鞍座鋼卷號
        /// </summary>
        /// <param name="Coil_ID"></param>
        private void Fun_DeleteSkidCoilId(string DelCoil_ID, string Skid)
        {
            

            if (!DelCoil_ID.IsEmpty())
            {

                #region [Postion]
                int pos = 0;
                switch (Skid)
                {
                    case nameof(CoilMapEntity.TBL_CoilMap.Entry_Car):
                        pos = -1;
                        break;
                    case nameof(CoilMapEntity.TBL_CoilMap.Entry_SK01):
                        pos = 2;
                        break;
                    case nameof(CoilMapEntity.TBL_CoilMap.Entry_SK02):
                        pos = 3;
                        break;
                    case nameof(CoilMapEntity.TBL_CoilMap.Entry_TOP):
                        pos = 4;
                        break;
                    case nameof(CoilMapEntity.TBL_CoilMap.POR):
                        pos = 1;
                        break;
                    case nameof(CoilMapEntity.TBL_CoilMap.TR):
                        pos = 5;
                        break;
                    case nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK01):
                        pos = 6;
                        break;
                    case nameof(CoilMapEntity.TBL_CoilMap.Delivery_SK02):
                        pos = 7;
                        break;
                    case nameof(CoilMapEntity.TBL_CoilMap.Delivery_TOP):
                        pos = 8;
                        break;
                    case nameof(CoilMapEntity.TBL_CoilMap.Delivery_Car):
                        pos = 9;
                        break;
                    default:
                        break;
                }
                #endregion

                string strMessage = $"是否删除鞍座[{Skid}]钢卷?";

                DialogResult dialog = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "删", Properties.Resources.dialogQuestion, 1);

                if (dialog.Equals(DialogResult.OK))
                {
                    string strSql = Frm_2_1_SqlFactory.SQL_Update_ClearSkid(DelCoil_ID, Skid);

                    //if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "删除鞍座钢卷号"))
                    //{
                    //    DialogHandler.Instance.Fun_DialogShowOk("删除鞍座钢卷号错误", "删除鞍座钢卷号", 3);

                    //    return;
                    //}


                    SCCommMsg.CS13_DeleteSidCoil DeleteSkidCoil = new SCCommMsg.CS13_DeleteSidCoil
                    {
                        DelPos = short.Parse(pos.ToString()),
                        Coil_ID = DelCoil_ID
                    };

                    PublicComm.Client.Tell(DeleteSkidCoil);

                    string strMessageShow = $"通知Server 鞍座:[{Skid}] 删除 钢卷编号:[{DelCoil_ID.Trim()}] 。 ";

                    //DialogHandler.Instance.Fun_DialogShowOk(strMessageShow, $"删除鞍座[{Skid}]钢卷", 4);//$"已通知Server删除鞍座[{Skid}] 钢卷编号:[{DelCoil_ID.Trim()}]"
                    EventLogHandler.Instance.EventPush_Message(strMessageShow);
                    EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}删除鞍座:[{Skid}] 钢卷编号:{DelCoil_ID.Trim()}", strMessageShow);//$"删除鞍座:[{Skid}] 钢卷编号:[{DelCoil_ID.Trim()}]"
                    PublicComm.ClientLog.Info(strMessageShow);//$"通知Server刪除鞍座:[{Skid}]上鋼卷[{DelCoil_ID.Trim()}]"
                    PublicComm.AkkaLog.Info(strMessageShow);
                }
            }
            else
            {
                DialogHandler.Instance.Fun_DialogShowOk($"目前该鞍座尚无钢卷", $"删除鞍座[{Skid}]钢卷", 0);

                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}删除鞍座钢卷号", $"目前该鞍座尚无钢卷");
                PublicComm.ClientLog.Info($"鞍座上無鋼卷可刪除");
            }
        }

        #endregion

        #region 出料
        private void Btn_DSK01_CoilOut_Click(object sender, EventArgs e)
        {
            DeliveryCoilOut(Lbl_DSK01_CoilNo.Text, SCCommMsg.CoilSkPosition.DSK01);
        }
       
        private void Btn_DSK02_CoilOut_Click(object sender, EventArgs e)
        {
            DeliveryCoilOut(Lbl_DSK02_CoilNo.Text, SCCommMsg.CoilSkPosition.DSK02);
        }

        private void Btn_DTOP_CoilOut_Click(object sender, EventArgs e)
        {
            DeliveryCoilOut(Lbl_DTOP_CoilNo.Text, SCCommMsg.CoilSkPosition.DTOP);
        }

        private void DeliveryCoilOut(string DeliveryCoil, SCCommMsg.CoilSkPosition SkidPosition)
        {
            if (!DeliveryCoil.IsEmpty())
            {
                SCCommMsg.CS14_DeliveryCoilOut DeliveryCoilOut = new SCCommMsg.CS14_DeliveryCoilOut
                {
                    CoilID = DeliveryCoil,
                    CoilPosition = SkidPosition
                };
                PublicComm.Client.Tell(DeliveryCoilOut);
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}出口端出料", $"出口端[{SkidPosition}]{ DeliveryCoil.Trim()}出料");
                DialogHandler.Instance.Fun_DialogShowOk($"通知Server出口端[{SkidPosition}]{ DeliveryCoil.Trim()}出料完成!","出口端出料",0);
                PublicComm.ClientLog.Info($"通知Server出口端[{SkidPosition}]{ DeliveryCoil.Trim()}出料完成");
            }
            else
            {
                DialogHandler.Instance.Fun_DialogShowOk($"目前该鞍座尚无钢卷!", "出口端出料", 0);
                PublicComm.ClientLog.Info($"目前出口端[{nameof(SkidPosition)}]無鋼卷");
            }
        }
        #endregion

        #region 手動輸入重量視窗 (保留)

        /// <summary>
        /// 手動輸入重量視窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DT21Weight_Click(object sender, EventArgs e)
        {
            if (!Lbl_DSK02_CoilNo.Text.IsEmpty())
            {
                Lbl_Weight_Coil_Title_X.Text = "钢卷编号 : ";
                Pnl_Weight_X.Visible = true;
                Lbl_Weight_Coil_Title_X.Text += Lbl_DSK02_CoilNo.Text;
                EventLogHandler.Instance.LogInfo("2-1", $"使用者:{ PublicForms.Main.Lbl_LoginUser.Text}开启输入毛重画面", $"输入钢卷编号:{Lbl_DSK02_CoilNo.Text } 毛重");
                EventLogHandler.Instance.EventPush_Message($"输入钢卷编号:{Lbl_DSK02_CoilNo.Text.Trim() } 毛重");
                PublicComm.ClientLog.Info($"输入钢卷编号:{Lbl_DSK02_CoilNo.Text.Trim()} 毛重");
            }
            else
            {
                EventLogHandler.Instance.EventPush_Message($"鞍座上无钢卷");
                PublicComm.ClientLog.Info($"鞍座上无钢卷");
            }
        }

        /// <summary>
        /// 毛重輸入確定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Wt_Save_Click(object sender, EventArgs e)
        {
            #region 修改PDO毛重欄位
            string strSql = Frm_2_1_SqlFactory.SQL_Update_CoilWeight();

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "毛重输入"))
            {
                EventLogHandler.Instance.EventPush_Message($"毛重输入错误");
                return;
            }

            #endregion

            SCCommMsg.CS08_WeightInput _WeightInput = new SCCommMsg.CS08_WeightInput
            {
                Source = "CPL1_HMI",
                ID = "WeightInput",
                OutCoilID = Lbl_DSK02_CoilNo.Text,
                WeightInput = Txt_Weight_X.Text
            };

            Pnl_Weight_X.Visible = false;

            EventLogHandler.Instance.EventPush_Message($"手动输入钢卷号[{Lbl_DSK02_CoilNo.Text.Trim()}]毛重[{Txt_Weight_X.Text.Trim()}]");
            EventLogHandler.Instance.LogDebug("2-1", $"手动输入毛重", $"手动输入钢卷号[{Lbl_DSK02_CoilNo.Text.Trim()}]毛重[{Txt_Weight_X.Text.Trim()}]");
            PublicComm.ClientLog.Debug($"手動輸入鋼卷號[{Lbl_DSK02_CoilNo.Text.Trim()}]毛重[{Txt_Weight_X.Text.Trim()}]");
            PublicComm.Client.Tell(_WeightInput);
            PublicComm.ClientLog.Debug($"已通知Server鋼卷號[{Lbl_DSK02_CoilNo.Text.Trim()}]毛重已被更新");
            PublicComm.AkkaLog.Debug($"已通知Server鋼卷號[{Lbl_DSK02_CoilNo.Text.Trim()}]毛重已被更新");
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_WtCancel_Click(object sender, EventArgs e)
        {
            Pnl_Weight_X.Visible = false;
        }

        #endregion

        #region PDO確認畫面(保留)
        private void Btn_PDO_Click(object sender, EventArgs e)
        {
            //frm_PDOConfirm frm_PDO = new frm_PDOConfirm();
            //EventLogHandler.Instance.LogInfo("2-1", "使用者:" + PublicForms.Main.lblLoginUser.Text + "开启PDO确认", "开启PDO确认画面");
            //frm_PDO.Show();
        }

        #endregion

        private void Btn_Reflash_Click(object sender, EventArgs e)
        {
            Fun_FormShown();
        }

        // 限制輸入只能輸入數字
        private void TxtWt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = IsNumeric(sender, e);
        }

        private bool IsNumeric(object sender, KeyPressEventArgs e)
        {
            // Only Numeric
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !e.KeyChar.Equals('.'))
                return true;

            // One Decimal Point
            if ((e.KeyChar.Equals('.')) && ((sender as TextBox).Text.IndexOf('.') > -1))
                return true;

            return false;
        }

        private void Btn_StripBreakModify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Lbl_POR_CoilNo.Text.Trim())) { return; }

            string strMessage = $"确定断带修改POR卷号?";
            DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "断带修改POR卷号", Properties.Resources.dialogQuestion, 1);
            if (dialogR == DialogResult.Cancel) return;

            //在POR端因為鋼捲斷帶，要指定子捲號，HMI通知Server修改子捲號，Server需下拋205NewPORId通知L1修改POR捲號。
            SCCommMsg.CS21_POR_StripBreakModify _StripBreak = new SCCommMsg.CS21_POR_StripBreakModify
            {
                Coil_ID = Lbl_POR_CoilNo.Text.Trim()
            };

            PublicComm.Client.Tell(_StripBreak);

            DialogHandler.Instance.Fun_DialogShowOk($"已通知Server发送断带子卷号给L1", "断带修改POR卷号", 4);

            PublicComm.ClientLog.Info($"已通知Server发送断带子卷钢卷号:[{Lbl_POR_CoilNo.Text.Trim()}]给L1");

            EventLogHandler.Instance.LogInfo("2-1", "断带修改POR卷号", $"通知Server发送断带子卷钢卷号:[{Lbl_POR_CoilNo.Text.Trim()}]给L1");
        }

        private void Btn_PORPresetL1_Click(object sender, EventArgs e)
        {
            string strCoil = "";
            string strPlan_No = "";

            if (!string.IsNullOrEmpty(Lbl_POR_CoilNo.Text.Trim()))
            {
                strCoil = Lbl_POR_CoilNo.Text;
            }
            else
            {
                DialogHandler.Instance.Fun_DialogShowOk($"POR上无钢卷可下抛生产参数", "下抛钢卷生产参数", 0);
                return;
            }
            string strSql = $@"Select pdi.*
                        From [{nameof(CoilPDIEntity.TBL_PDI)}] pdi                        
                        Where pdi.[{nameof(CoilPDIEntity.TBL_PDI.Entry_Coil_ID)}] = '{strCoil}'
                        ORDER BY pdi.[{ nameof(CoilPDIEntity.TBL_PDI.CreateTime)}] DESC";
            DataTable dtData = DataAccess.Fun_SelectDate(strSql, "PDI_Data");
            if(dtData != null && dtData.Rows.Count > 0)
            {
                strPlan_No = dtData.Rows[0][nameof(CoilPDIEntity.TBL_PDI.Plan_No)].ToString();
            }

            SCCommMsg.CS22_POR_PresetL1 _POR_PresetL1 = new SCCommMsg.CS22_POR_PresetL1
            {
                Coil_ID = strCoil,
                Plan_No = strPlan_No
            };
            PublicComm.Client.Tell(_POR_PresetL1);

            DialogHandler.Instance.Fun_DialogShowOk($"已通知Server下抛POR钢卷生产参数给L1", "下抛POR钢卷生产参数", 4);

            PublicComm.ClientLog.Info($"已通知Server下抛POR钢卷 钢卷号:[{Lbl_POR_CoilNo.Text.Trim()}]生产参数给L1");

            EventLogHandler.Instance.LogInfo("2-1", "下抛POR钢卷生产参数", $"通知Server下抛POR钢卷 钢卷号:[{Lbl_POR_CoilNo.Text.Trim()}]生产参数给L1");
        }

        private void Btn_Por_Paper_Click(object sender, EventArgs e)
        {
            if (!Pnl_Spare.Visible)
            {
                Fun_ComboBoxDescription(((Button)(sender)));
                Pnl_Spare.Visible = true;
            }
            else
            {
                Pnl_Spare.Visible = false;
            }
        }

        private void Fun_ComboBoxDescription(Button btnName)
        {
            string strLblText = "";
            Cbo_Type cbo_Type = 0;
            Point point = new Point();
            Txt_Spare.ScrollBars = ScrollBars.None;
            switch (btnName.Name)
            {               
                //case nameof(Btn_Por_Paper):
                //    strLblText = "入口垫纸方式";
                //    cbo_Type = Cbo_Type.PAPER_REQ_CODE;
                //    point = new Point(341, 395);
                //    break;

                case nameof(Btn_Por_Paper):
                    strLblText = "垫纸类型";
                    cbo_Type = Cbo_Type.Paper_Type;
                    point = new Point(823, 393);
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    break;

                default:
                    return;

            }
            Lbl_ComboName_Spare.Text = strLblText;
            Pnl_Spare.Visible = true;
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
            Pnl_Spare.Location = point;
            Pnl_Spare.BringToFront();
            //ComboBox 的說明Panel 預設大小
            Pnl_Spare.Size = new Size(650, 230);
        }
        private void Btn_Close_Spare_Click(object sender, EventArgs e)
        {
            Pnl_Spare.Visible = false;
        }
        private void Tab_GridDataControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_GridDataControl, e);
        }

        private string Fun_ChangeShow( string strOldShow)
        {                       
            string strNewShow ;
            switch (strOldShow)
            {
                case "0":                   
                    strNewShow = "否";
                    break;
                case "1":                   
                    strNewShow = "是";
                    break;

                default:
                    strNewShow = "";
                    break;
            }           
            return strNewShow;
        }
        /// <summary>
        /// 連線狀態_取得資料(L1,WMS,MMS)
        /// </summary>
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

        /// <summary>
        /// 連線狀態_變色(L1,WMS,MMS)
        /// </summary>
        private void Fun_NetWorkStatusDisplay(DataTable dt)
        {
            for (int Index = 0; Index < dt.Rows.Count; Index++)
            {
                //Send MMS
                if (dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("MMS"))
                {
                    Lbl_SendWMS_Color.BackColor = dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Trim().Equals("1") ? Color.Lime : Color.Red;
                }
                //Rev MMS
                else if (dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_From)].ToString().Trim().Equals("MMS"))
                {
                    Lbl_RevMMS_Color.BackColor = dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ? Color.Lime : Color.Red;
                }
                ////Send L2.5
                //else if (dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_To)].ToString().Trim().Equals("LEVEL25"))
                //{
                //    Lbl_SendL25_Color.BackColor = dt.Rows[Index][nameof(TBL_ConnectionStatus.Connection_Status)].ToString().Equals("1") ? Color.Lime : Color.Red;
                //}
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
