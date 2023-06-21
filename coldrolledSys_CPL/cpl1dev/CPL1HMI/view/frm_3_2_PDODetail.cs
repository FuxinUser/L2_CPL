using Akka.Actor;
using Common.StTool;
using Controller.Coil;
using DataModel.HMIServerCom.Msg;
using DBService.Repository.DefectData;
using DBService.Repository.LookupTblPaper;
using DBService.Repository.LookupTbSleeve;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CPL1HMI.DataBaseTableFactory;

namespace CPL1HMI
{
    public partial class frm_3_2_PDODetail : Form
    {
        #region 變數
        public List<string> Insert_TBL_CutRecord_Head;
        public List<string> Insert_TBL_CutRecord_Tail;
        public string strSql_Insert_TBL_CutRecord = string.Empty;
        private bool bolDefectIsNull;
        private bool bolPdoIsNull;
        public static Frm_Defect frmDefect = null;

        string str_Now_FinishTime = "";
        //權限設定 功能Btn
        Control[] CtlAuthority;
        //權限設定 欄位不開放
        Control[] CtlNotOpen;
        //權限設定 欄位開放
        Control[] CtlOpen;
        //所有欄位頁籤
        Control[] CtrControlArray;       
        //主鍵
        Control[] CtlKeyArray;

        DataTable dtSelectOne;
        DataTable dtSelectOne_Def;
        DataTable dtBeforeEdit;
        DataTable dtBeforeEdit_Def;
        string strEditStatus = "Read";
        bool bolEditStatus = false;
        //語系
        private LanguageHandler LanguageHand;
        DataTable dtLangSwitch_Column;


        #endregion


        public frm_3_2_PDODetail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_3_2_ProdDetail_Load(object sender, EventArgs e)
        {
            //LanguageHandler.Instance.Fun_CurrentLanguage(this.Name);
           
            if (PublicForms.PDODetail == null) PublicForms.PDODetail = this;
            Control[] Frm_3_2_Control = new Control[] {
            Btn_New, //新增
             
            Btn_Update, //修改
            Btn_MMS //上傳MMS
            };//Btn_New,新增
            UserSetupHandler.Instance.Fun_SetControlAuthority(Frm_3_2_Control, UserSetupHandler.Instance.Frm_3_2);
           
            //權限控制項設定
            Fun_InitialAuthority();


            //CPL1才有圓盤剪資訊 ; CPL2沒有,所以不顯示
            if ( !GlobalVariableHandler.proLine.Equals("CPL1"))
            {
                //圆盘剪Gap平均值
                Lbl_Avg_Side_Trimmer_Gap_Title.Visible = false;
                Txt_Avg_Side_Trimmer_Gap.Visible = false;
                Lbl_Avg_Side_Trimmer_Gap_Unit.Visible = false;

                //圆盘剪Lap平均值
                Lbl_Avg_Side_Trimmer_Lap_Title.Visible = false;
                Txt_Avg_Side_Trimmer_Lap.Visible = false;
                Lbl_Avg_Side_Trimmer_Lap_Unit.Visible = false;

                //圆盘剪宽度平均值
                Lbl_Avg_Side_Trimmer_Width_Title.Visible = false;
                Txt_Avg_Side_Trimmer_Width.Visible = false;
                Lbl_Avg_Side_Trimmer_Width_Unit.Visible = false;

                //裁边量平均值_操作侧
                Lbl_Avg_Trimming_OperateSide_Title.Visible = false;
                Txt_Avg_Trimming_OperateSide.Visible = false;
                Lbl_Avg_Trimming_OperateSide_Unit.Visible = false;

                //裁边量平均值_Drive侧
                Lbl_Avg_Trimming_DriveSide_Title.Visible = false;
                Txt_Avg_Trimming_DriveSide.Visible = false;
                Lbl_Avg_Trimming_DriveSide_Unit.Visible = false;
            }

            ReadOnlyControl(Tab_PDODetailPage, true);
            ReadOnlyControl(Tab_PDODefectPage, true);
            //Btn淨重計算 不啟用
            Btn_GetNewWt.Enabled = false;

            ////出口鋼卷號ComboBox
            //Fun_SelectOut_mat_NoList();

            //combobox欄位
            Fun_ComboBoxItems();

            ////Read 不顯示 ; 編輯模式才顯示(功能暫時取消)
            //Btn_Head_Calc.Visible = false;
            //Btn_Tail_Calc.Visible = false;

            //取得栏位
            string strSql = Frm_3_2_SqlFactory.SQL_Select_PDODetail("");
            dtSelectOne = DataAccess.Fun_SelectDate(strSql, "PDO");
            string strSql_Def = Frm_3_2_SqlFactory.SQL_Select_DefectData("");
            dtSelectOne_Def = DataAccess.Fun_SelectDate(strSql_Def, "PDO缺陷资料");

            if(dtSelectOne == null || dtSelectOne.Rows.Count <= 0)
            {
                //新增
                Fun_SetBottonEnabled(Btn_New, true);
                //修改
                Fun_SetBottonEnabled(Btn_Update, false);
                //上传MMS
                Fun_SetBottonEnabled(Btn_MMS, false);
                //列印標籤
                Fun_SetBottonEnabled(Btn_PrintTag, true);
            }

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);

            Fun_GetColumnShowText();
        }

        private void Fun_InitialAuthority()
        {
            //權限設定 功能Btn
             CtlAuthority = new Control[] {Btn_New,Btn_Update,Btn_Save,Btn_Cancel, Btn_PrintTag, Btn_MMS };
            //權限設定 欄位不開放
            CtlNotOpen = new Control[] {
                        /*合同号*/          Txt_Order_No,
                        /*计划号*/          Txt_Plan_No,
                        /*出口钢卷号*/      Txt_Out_Coil_ID,
                        /*入口钢卷号*/      Txt_In_Coil_ID,
                        /*生产开始时间*/    Dtp_StartTime,
                        /*生产结束时间*/    Dtp_FinishTime,
                        /*班次*/            Cob_Shift,
                        /*班别*/            Cob_Team,
                        /*出口卷外径*/      Txt_Out_Coil_Outer_Diameter,
                        /*出口卷内径*/      Txt_Out_Coil_Inner,
                        /*出口厚度*/        Txt_Out_Coil_Thick,
                        /*出口宽度*/        Txt_Out_Coil_Width,
                        /*出口卷长度*/      Txt_Out_Coil_Length,
                        /*出口套筒内径*/    Txt_Sleeve_Inner_Exit_Diamter,
                        /*出口套筒类型*/    Cob_Sleeve_Type_Exit_Code,
                        /*头部打孔位置*/    Txt_Head_Hole_Position,
                        /*尾部打孔位置*/    Txt_Tail_PunchHole_Position,
                        /*头部切废长度*/    Txt_Scraped_Length_Entry,
                        /*尾部切废长度*/    Txt_Scraped_Length_Exit,
                        /*卷曲方向*/        Cob_Winding_Direction,
                        /*好面朝向*/        Cob_Base_Surface,
                        /*表面精度代码*/    Cob_Surface_Accuracy_Code,
                        /*头部未轧制区域*/  Txt_Head_Off_Gauge,
                        /*尾部未轧制区域*/  Txt_Tail_Off_Gauge,
                        /*内表面精度代码*/  Cob_Surface_Accu_Code_In,
                        /*外表面精度代码*/  Cob_Surface_Accu_Code_Out,
                        /*翻面标记*/        Cob_Flip_Tag,
                        /*工序工艺码*/      Cob_ProcessCode  };

            //權限設定 欄位開放
            CtlOpen = new Control[] { };
            //所有欄位頁籤
            CtrControlArray = new Control[] { Tab_PDODetailPage, Tab_PDODefectPage };
            //主鍵 Key
            CtlKeyArray = new Control[] { 
                /*计划号*/ Txt_Plan_No, 
                /*出口钢卷号*/ Txt_Out_Coil_ID, 
                /*入口钢卷号*/ Txt_In_Coil_ID, 
                /*生产结束时间*/ Dtp_FinishTime };
        }

        /// <summary>
        /// 取得画面栏位中英显示文字
        /// </summary>
        private void Fun_GetColumnShowText()
        {             
            string strSql = Frm_3_2_SqlFactory.SQL_Select_LangSwitch_Ctr_List("frm_3_2_PDODetail");
            dtLangSwitch_Column = DataAccess.Fun_SelectDate(strSql, "PDO画面栏位清单");
        }

        /// <summary>
        /// 出口捲號清單
        /// </summary>
        private void Fun_SelectOut_mat_NoList()
        {
            string strSql = Frm_3_2_SqlFactory.SQL_Select_Out_Coil_ID_List();
            DataTable dtGetCoilList = DataAccess.Fun_SelectDate(strSql, "出口卷号清单");

            if (dtGetCoilList.IsNull())
            {
                Cob_Search_Out_Coil_ID.DataSource = null;

                return;
            }

            Cob_Search_Out_Coil_ID.DisplayMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_Search_Out_Coil_ID.ValueMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_Search_Out_Coil_ID.DataSource = dtGetCoilList;

        }

        private void Fun_ComboBoxItems()
        {

            //班次
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Shift, Cob_Shift);
            //班別
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Team, Cob_Team);
            //出口墊紙種類
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Paper(Cob_Paper_Code);
            //出口墊紙方式
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.PAPER_REQ_CODE, Cob_Paper_Req_Code,true);
            //出口套筒種類
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Sleeve(Cob_Sleeve_Type_Exit_Code);
            //好面朝向
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Base_Surface, Cob_Base_Surface);
            //封鎖標記
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Hold, Cob_Hold_Flag);
            //取樣標記
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Samp, Cob_Sample_Flag);
            //取樣位置 
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.SAMPLE_FRQN_CODE, Cob_Sample_Frqn_Code);
            //裁邊標記
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Trim, Cob_Trim_Flag);
            //分卷標記
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Trim, Cob_Fixed_WT_Flag);
            //最終卷標記
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.End, Cob_End_Flag);
            //廢品標記
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Scrap, Cob_Scrap_Flag);
            //表面精度代碼
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Surface_Accuracy, Cob_Surface_Accuracy_Code);
            ////內表面精度代碼
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, cbo_SURFACE_ACCU_CODE_IN);
            ////外表面精度代碼
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.Surface_Accuracy, cbo_SURFACE_ACCU_CODE_OUT);
            //卷曲方向
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Winding_Direction, Cob_Winding_Direction);
            //翻面标记
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.FLIP, Cob_Flip_Tag);
            //实际开卷方向
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.Decoiler, Cob_Decoiler_Direction);
            //工序代码
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems(Cbo_Type.ProcessCode, Cob_ProcessCode);
            #region -缺陷資料ComboBox设定-

            #region - Level -
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D01);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D02);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D03);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D04);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D05);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D06);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D07);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D08);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D09);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectLevel, cbo_Level_D10);
            #endregion

            #region - Sid -
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D01);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D02);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D03);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D04);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D05);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D06);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D07);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D08);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D09);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectSid, cbo_Sid_D10);
            #endregion

            #region - PosW -
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D01);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D02);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D03);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D04);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D05);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D06);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D07);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D08);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D09);
            //ComboBoxIndexHandler.Instance.SelectComboBoxItems(Cbo_Type.DefectPosW, cbo_PosW_D10);
            #endregion

            #endregion

        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (strEditStatus != "Read")
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再查询", "查询", 0);
                return;
            }
            if (bolEditStatus == true)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再查询", "查询", 0);
                return;
            }
            //strOut_Coil_ID, strFinishTime, strIn_Coil_ID, strPlan_No
            //if (!Cob_Search_Out_Coil_ID.Text.IsEmpty())
            //    Fun_SelectCoilPDO(Cob_Search_Out_Coil_ID.Text);
            if (!Txt_Search_Out_Coil_ID.Text.IsEmpty())
                Fun_SelectCoilPDO(Txt_Search_Out_Coil_ID.Text);

            ////檢查是否已上傳過
            ////string strUpFlag = Fun_CheckUploadFlag(Cob_Search_Out_Coil_ID.Text);
            //string strUpFlag = Fun_CheckUploadFlag(Txt_Search_Out_Coil_ID.Text);
            //if (strUpFlag.Equals("1"))
            //{
            //    //已上传MMS
            //    Fun_SetBottonEnabled(Btn_MMS, false);
            //}
            //else
            //{
            //    //未上传MMS
            //    Fun_SetBottonEnabled(Btn_MMS, true);
            //}

        }

        /// <summary>
        /// 查询母卷缺陷资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_SearchDefect_Click(object sender, EventArgs e)
        {
            if (Txt_In_Coil_ID.Text.Trim().Equals(""))
            {
                DialogHandler.Instance.Fun_DialogShowOk("请先查询钢卷PDO!", "母卷缺陷", 0);

                return;
            }


            //foreach (Form f in this.Controls.Cast<Control>().Where(x => x is Form))
            //{
            //    //如果子視窗已經存在
            //    if (f.Name == nameof(Frm_Defect))
            //    {
            //        //將該子視窗設為焦點
            //        f.Focus();
            //        DialogHandler.Instance.Fun_DialogShowOk("母卷缺陷视窗已开启!", "母卷缺陷", 0);
            //        return;
            //    }               
            //}


            // 紀錄是否已經有 PdiDetails
            bool bolHaveDefect = false;

            if(frmDefect == null || frmDefect.IsDisposed)
            {
                bolHaveDefect = false;
            }
            else
            {
                bolHaveDefect = true;
            }
           

            if (!bolHaveDefect)
            {
                frmDefect = new Frm_Defect
                {
                    Defect_Coil_ID = Txt_In_Coil_ID.Text.Trim()
                };
                frmDefect.Location = new Point(600, 145);
                frmDefect.Show();
            }
            else
            {
                DialogHandler.Instance.Fun_DialogShowOk("母卷缺陷视窗已开启!", "母卷缺陷", 0);
            }
           
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_New_Click(object sender, EventArgs e)
        {
            //已是编辑状态 return
            if (bolEditStatus) { return; }
            //紀錄是否編輯,狀態
            strEditStatus = "New";
            bolEditStatus = true;

            //先備份之前顯示的資料
            dtBeforeEdit = dtSelectOne.Copy();
            dtBeforeEdit_Def = dtSelectOne_Def.Copy();
            ////修改前先備份資料
            //if (dtSelectOne != null && dtSelectOne.Rows.Count >0)
            //{
            //    //先備份之前顯示的資料
            //    dtBeforeEdit = dtSelectOne.Copy();
            //}
            //else
            //{
            //    //如果之前沒有顯示資料,複製欄位
            //    dtSelectOne = dtPdo.Clone();
            //    dtBeforeEdit = dtSelectOne.Copy();
            //}
            ////修改前先備份資料_Def
            //if (dtSelectOne != null && dtSelectOne.Rows.Count > 0)
            //{
            //    //先備份之前顯示的資料
            //    dtBeforeEdit_Def = dtSelectOne_Def.Copy();
            //}
            //else
            //{
            //    //如果之前沒有顯示資料,複製欄位
            //    dtSelectOne_Def = dtPdo.Clone();
            //    dtBeforeEdit_Def = dtSelectOne_Def.Copy();
            //}                   
            
            //修改
            Fun_SetBottonEnabled(Btn_Update, false);
            //上传MMS
            Fun_SetBottonEnabled(Btn_MMS, false);
            //列印標籤
            Fun_SetBottonEnabled(Btn_PrintTag, true);
            //儲存
            Btn_Save.Visible = true; 
            //取消
            Btn_Cancel.Visible = true;
            ////Read 不顯示 ; 編輯模式才顯示(功能暫時取消)
            //Btn_Head_Calc.Visible = true;
            //Btn_Tail_Calc.Visible = true;

            ReadOnlyHandler.Instance.ReadOnly(Tab_PDODetailPage, false);
            ReadOnlyControl(Tab_PDODefectPage, false);
            ////唯獨欄位 僅顯示 不可編輯 (20211004 暫時先開放修改 圓盤剪&裁邊量，等業主決定是否鎖起來)
            //ReadOnlyHandler.Instance.Fun_FindControlReadOnly(Pnl_ReadOnly, true);//20211208_經理開放
            //计划号
            Txt_Plan_No.Text = string.Empty;
            //出口鋼卷號
            Txt_Out_Coil_ID.Text = string.Empty;
            Lbl_OutCoil_Defect.Text = string.Empty;
            //入口鋼卷號
            Txt_In_Coil_ID.Text = string.Empty;
            //生產開始時間
            //txtStarttime.Text = string.Empty;
            Dtp_StartTime.Value = DateTime.Now;// DateTimePicker.MaximumDateTime;
            //生產結束時間
            //txtFinishtime.Text = string.Empty;
            Dtp_FinishTime.Value = DateTime.Now;//DateTimePicker.MaximumDateTime;
            
            //唯獨欄位 清空,避免誤存值
            Fun_ClearPanelText(Pnl_ReadOnly);

            //PDI 出纲纪号 仅显示用
            Fun_ClearPanelText(Pnl_ReadOnly_St_No);
            ReadOnlyHandler.Instance.Fun_FindControlReadOnly(Pnl_ReadOnly_St_No, true);
            //Btn淨重計算 啟用
            Btn_GetNewWt.Enabled = true;

            //EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.lblLoginUser.Text } 新增", "新增");

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Update_Click(object sender, EventArgs e)
        {
            //已是编辑状态 return
            if (bolEditStatus) { return; }

            //修改前先備份資料
            dtBeforeEdit = dtSelectOne.Copy();
            dtBeforeEdit_Def = dtSelectOne_Def.Copy();

            ReadOnlyControl(Tab_PDODetailPage, false);
            ReadOnlyControl(Tab_PDODefectPage, false);
            ////唯獨欄位 僅顯示 不可編輯 (20211004 暫時先開放修改 圓盤剪&裁邊量，等業主決定是否鎖起來)
            //ReadOnlyHandler.Instance.Fun_FindControlReadOnly(Pnl_ReadOnly, true);//20211208_經理開放
            
            ////修改状态 key不可改
            ////计划号
            //Txt_Plan_No.ReadOnly = true;           
            ////出口鋼卷號
            //Txt_Out_Coil_ID.ReadOnly = true;
            ////入口鋼卷號
            //Txt_In_Coil_ID.ReadOnly = true;
            ////生產結束時間
            //Dtp_FinishTime.Enabled = false;
            ReadOnlyHandler.Instance.ReadOnly(CtlKeyArray, true); 

            // UserSetupHandler.Instance.Authority_Class : 1 = Administator ; 2 = Manager ; 3 : Operator 
            if (UserSetupHandler.Instance.Authority_Class != "1")
            {
                ReadOnlyHandler.Instance.ReadOnly(CtlNotOpen, true);
            }

            //PDI 出纲纪号 仅显示用
            Fun_ClearPanelText(Pnl_ReadOnly_St_No);
            ReadOnlyHandler.Instance.Fun_FindControlReadOnly(Pnl_ReadOnly_St_No, true);
            //Btn淨重計算 啟用
            Btn_GetNewWt.Enabled = true;

            //新增
            Fun_SetBottonEnabled(Btn_New, false);

            //上传MMS
            Fun_SetBottonEnabled(Btn_MMS, false);

            Btn_Cancel.Visible = true; //取消
            Btn_Save.Visible = true; //儲存

            ////Read 不顯示 ; 編輯模式才顯示(功能暫時取消)
            //Btn_Head_Calc.Visible = true;
            //Btn_Tail_Calc.Visible = true;

            //紀錄是否編輯,狀態
            strEditStatus = "Edit";
            bolEditStatus = true;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCoil_ID"></param>
        /// <returns>UploadFlag</returns>
        private string Fun_CheckUploadFlag(string strCoil_ID)
        {
            string strOut_Coil_ID = Txt_Out_Coil_ID.Text.Trim();//1                
            string strFinishTime = Dtp_FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss");//2
            string strPlan_No = Txt_Plan_No.Text.Trim();//3
            string strIn_Coil_ID = Txt_In_Coil_ID.Text.Trim();//4
            

            //檢查是否上傳過MMS
            string strSql = Frm_3_2_SqlFactory.SQL_Select_UploadedChecked(strCoil_ID, strFinishTime, strIn_Coil_ID, strPlan_No);
            DataTable dtUploadFlag = DataAccess.Fun_SelectDate(strSql, "PDO上传记录");
            string strUploadFlag;
            if (dtUploadFlag!=null&& dtUploadFlag.Rows.Count > 0)
            {
                strUploadFlag = dtUploadFlag.Rows[0][nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)].ToString().Trim(); ;
            }
            else
            {
                strUploadFlag = "";
            }

            return strUploadFlag;


            //
            //if (dtUploadFlag.IsNull())
            //{
            //    ReadOnlyControl(Tab_PDODetailPage, false);
            //    ReadOnlyControl(Tab_PDODefectPage, false);
            //    Txt_Out_Coil_ID.ReadOnly = true;
            //    //新增
            //    Fun_SetBottonEnabled(Btn_New, false);

            //    //上传MMS
            //    Fun_SetBottonEnabled(Btn_MMS, false);

            //    Btn_Cancel.Visible = true; //取消
            //    Btn_Save.Visible = true; //儲存

            //    ////Read 不顯示 ; 編輯模式才顯示(功能暫時取消)
            //    //Btn_Head_Calc.Visible = true;
            //    //Btn_Tail_Calc.Visible = true;

            //    //紀錄是否編輯,狀態
            //    strEditStatus = "Edit";
            //    bolEditStatus = true;
            //}
            //else
            //if (!strUploadFlag.Equals("1"))
            //{
            //    ReadOnlyControl(Tab_PDODetailPage, false);
            //    ReadOnlyControl(Tab_PDODefectPage, false);
            //    //新增
            //    Fun_SetBottonEnabled(Btn_New, false);

            //    //上传MMS
            //    Fun_SetBottonEnabled(Btn_MMS, false);

            //    Btn_Cancel.Visible = true; //取消
            //    Btn_Save.Visible = true; //儲存

            //    ////Read 不顯示 ; 編輯模式才顯示(功能暫時取消)
            //    //Btn_Head_Calc.Visible = true;
            //    //Btn_Tail_Calc.Visible = true;
            //    //紀錄是否編輯,狀態
            //    strEditStatus = "Edit";
            //    bolEditStatus = true;
            //}
            //else
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk($"钢卷号[{Txt_Out_Coil_ID.Text.Trim()}]已上传，不允许修改", "PDO修改", 0);
            //    //紀錄是否編輯,狀態
            //    strEditStatus = "Read";
            //    bolEditStatus = false;
            //}
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            //不是编辑状态 return
            if (!bolEditStatus) { return; }

           
            if (dtBeforeEdit != null && dtBeforeEdit.Rows.Count >0)
            {
                string strOut_Coil_ID = dtBeforeEdit.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString();// Txt_Out_Coil_ID.Text.Trim();//1
                DateTime dTime = DateTime.Parse(dtBeforeEdit.Rows[0][nameof(PDOEntity.TBL_PDO.FinishTime)].ToString());
                string strFinishTime = dTime.ToString("yyyy-MM-dd HH:mm:ss");//2  
                //string strFinishTime = dtBeforeEdit.Rows[0][nameof(PDOEntity.TBL_PDO.FinishTime)].ToString();
                // Dtp_FinishTime.ToString();//2
                // Dtp_FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                string strIn_Coil_ID = dtBeforeEdit.Rows[0][nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString();// Txt_In_Coil_ID.Text.Trim();//3
                string strPlan_No = dtBeforeEdit.Rows[0][nameof(PDOEntity.TBL_PDO.Plan_No)].ToString();// Txt_Plan_No.Text.Trim();//4
                Fun_SelectCoilPDO(strOut_Coil_ID, strFinishTime, strIn_Coil_ID, strPlan_No);
            }
            else
            {
                Fun_SelectCoilPDO("","", "", "");
            }


            ReadOnlyControl(Tab_PDODetailPage, true);
            ReadOnlyControl(Tab_PDODefectPage, true);
            //Btn淨重計算 不啟用
            Btn_GetNewWt.Enabled = false;

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            if(string.IsNullOrEmpty(Txt_Out_Coil_ID.Text.Trim()))
            {
                //修改
                Fun_SetBottonEnabled(Btn_Update, false);
                //上传MMS
                Fun_SetBottonEnabled(Btn_MMS, false);
            }
            else
            {
                //修改
                Fun_SetBottonEnabled(Btn_Update, true);
                //上传MMS
                Fun_SetBottonEnabled(Btn_MMS, true);
            }
            
          

            Btn_Cancel.Visible = false; //取消
            Btn_Save.Visible = false; //儲存

            ////Read 不顯示 ; 編輯模式才顯示(功能暫時取消)
            //Btn_Head_Calc.Visible = false;
            //Btn_Tail_Calc.Visible = false;

            //取消编辑后,恢复唯读状态
            strEditStatus = "Read";
            bolEditStatus = false;

            if (Pnl_Spare.Visible == true)
                Pnl_Spare.Visible = false;
        }

        /// <summary>
        /// 储存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            //檢查必填欄位            
            if (!Fun_IsColumnsEmpty()) { return; }

            string strOut_Coil_ID = Txt_Out_Coil_ID.Text.Trim();//1                
            string strFinishTime = Dtp_FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss");//2
            //strFinishTime += $".{Dtp_FinishTime.Value.Millisecond}" ;

            //FinishTime_Str
            strFinishTime = str_Now_FinishTime;//取得之前紀錄 的FinishTime_fff
            
            string strIn_Coil_ID = Txt_In_Coil_ID.Text.Trim();//3
            string strPlan_No = Txt_Plan_No.Text.Trim();//4

            string strCheckSql_PDO = Frm_3_2_SqlFactory.SQL_Select_PDODetail(strOut_Coil_ID, strFinishTime, strIn_Coil_ID,strPlan_No);
            DataTable dt_Check_PDO = DataAccess.Fun_SelectDate(strCheckSql_PDO, "PDO资料");
            bolPdoIsNull = dt_Check_PDO.IsNull();

            //新增
            if (Btn_New.Enabled)
            {
                if (bolPdoIsNull)
                {
                    //無此PDO 可新增
                }
                else
                {
                    //已有PDO 不可新增
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"新增PDO资料");
                    sb.AppendLine($"    计划号:{strPlan_No}");
                    sb.AppendLine($"    出口钢卷号:{strOut_Coil_ID}");
                    sb.AppendLine($"    入口钢卷号:{strIn_Coil_ID}");                   
                    sb.AppendLine($"    生产结束时间:{strFinishTime}");
                    sb.AppendLine($"资料已重复，请重新确认！");                    
                   
                    DialogHandler.Instance.Fun_DialogShowOk(sb.ToString(), "新增PDO", 3);
                    return;
                }
            }
            else if (Btn_Update.Enabled)
            {
                if (bolPdoIsNull)
                {
                    //無此PDO 無法更新資料
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"修改PDO资料");
                    sb.AppendLine($"    计划号:{strPlan_No}");
                    sb.AppendLine($"    出口钢卷号:{strOut_Coil_ID}");
                    sb.AppendLine($"    入口钢卷号:{strIn_Coil_ID}");
                    sb.AppendLine($"    生产结束时间:{strFinishTime}");
                    sb.AppendLine($"查无资料，请重新确认！");

                    DialogHandler.Instance.Fun_DialogShowOk(sb.ToString(), "修改PDO", 3);
                    return;
                }
                else
                {
                    //有此PDO 可修改
                }
            }



            string strCheckSql = Frm_3_2_SqlFactory.SQL_Select_DefectData(strOut_Coil_ID, strPlan_No);
            DataTable dt_Check_Defect = DataAccess.Fun_SelectDate(strCheckSql, "PDO缺陷资料");
                       
            bolDefectIsNull = dt_Check_Defect.IsNull() ;

           
            //头部切废长度 Txt_Scraped_Length_Entry 
            Int32.TryParse(Txt_Header_Cut_Length_Entry.Text.Trim(), out int intHeadLen_En);
            Int32.TryParse(Txt_Header_Cut_Length_Exit.Text.Trim(), out int intHeadLen_Ex);
            int intHeadSum = intHeadLen_En + intHeadLen_Ex;
            Txt_Scraped_Length_Entry.Text = intHeadSum.ToString();

            //尾部切废长度 Txt_Scraped_Length_Exit
            Int32.TryParse(Txt_Tail_Cut_Length_Entry.Text.Trim(), out int intTailLen_En);
            Int32.TryParse(Txt_Tail_Cut_Length_Exit.Text.Trim(), out int intTailLen_Ex);
            int intTailSum = intTailLen_En + intTailLen_Ex;
            Txt_Scraped_Length_Exit.Text = intTailSum.ToString();


            string strSql = string.Empty;

            string strSql_Defect = string.Empty;
            DialogResult dialogResult_Save;
            string strFinDeff = "";
            //新增
            if (Btn_New.Enabled)
            {
                dialogResult_Save = DialogResult.OK;
                //PDO
                strSql = Frm_3_2_SqlFactory.SQL_Insert_PDO();
                str_Now_FinishTime = Dtp_FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + ".000";
                //缺陷
                strSql_Defect = bolDefectIsNull ? Frm_3_2_SqlFactory.SQL_Insert_DefectData() : Frm_3_2_SqlFactory.SQL_Update_DefectData(strOut_Coil_ID, strPlan_No);
            }
            //修改
            else if (Btn_Update.Enabled)
            {
                Fun_GetUpdateDataToTable(dtSelectOne);
                Fun_GetUpdateDataToTable_Def(dtSelectOne_Def);
                StringBuilder sbFinDeff = new StringBuilder();
                sbFinDeff.AppendLine($"钢卷号码:{strOut_Coil_ID}修改");//strFinishTime, strIn_Coil_ID,strPlan_No
                foreach (DataRow dr in dtBeforeEdit.Rows)
                {
                    foreach (DataRow drS in dtSelectOne.Rows)
                    {
                        foreach (DataColumn dc in dtBeforeEdit.Columns)
                        {
                            foreach (DataColumn dcS in dtSelectOne.Columns)
                            {
                                if (dc.ToString() == "CreateTime") continue;
                                if (dc.ToString() == "PDO_Uploaded_Flag") continue;
                                if (dc.ToString() == "PDO_Uploaded_Time") continue;
                                if (dc.ToString() == "PDO_Uploaded_UserID") continue;
                                if (dc.ToString() == "LabelPrint_Time") continue;
                                if (dc.ToString() == "CoilWeight_Time") continue;
                                if (dc.ToString() == "Exit_ExportTime") continue;

                                if (dc.ToString() == dcS.ToString())
                                {
                                    if (dr[dc].ToString() != drS[dcS].ToString())
                                    {
                                        string strTitle = Fun_GetTitle(dc.ColumnName);
                                        sbFinDeff.AppendLine($"{strTitle} : {dr[dc].ToString()} => {drS[dcS].ToString()}");
                                    }
                                }
                            }
                        }
                    }

                }

                foreach (DataRow dr in dtBeforeEdit_Def.Rows)
                {
                    foreach (DataRow drS in dtSelectOne_Def.Rows)
                    {
                        foreach (DataColumn dc in dtBeforeEdit_Def.Columns)
                        {
                            foreach (DataColumn dcS in dtSelectOne_Def.Columns)
                            {
                                //if (dc.ToString() == "CreateTime") continue;
                                //if (dc.ToString() == "PDO_Uploaded_Flag") continue;
                                //if (dc.ToString() == "PDO_Uploaded_Time") continue;
                                //if (dc.ToString() == "PDO_Uploaded_UserID") continue;
                                //if (dc.ToString() == "LabelPrint_Time") continue;
                                //if (dc.ToString() == "CoilWeight_Time") continue;
                                //if (dc.ToString() == "Exit_ExportTime") continue;

                                if (dc.ToString() == dcS.ToString())
                                {
                                    if (dr[dc].ToString() != drS[dcS].ToString())
                                    {
                                        string strTitle = Fun_GetTitle_Def(dc.ColumnName);
                                        sbFinDeff.AppendLine($"{strTitle} : {dr[dc].ToString()} => {drS[dcS].ToString()}");
                                    }
                                }
                            }
                        }
                    }

                }


                strFinDeff = sbFinDeff.ToString();

                dialogResult_Save = DialogHandler.Instance.Fun_DialogShowOkCancel(strFinDeff, "修改PDO",null, 1);

                if (dialogResult_Save == DialogResult.OK)
                {
                    //PDO
                    strSql = Frm_3_2_SqlFactory.SQL_Update_PDO(strOut_Coil_ID, strFinishTime, strIn_Coil_ID, strPlan_No);
                    str_Now_FinishTime = strFinishTime;
                    //缺陷
                    strSql_Defect = bolDefectIsNull ? Frm_3_2_SqlFactory.SQL_Insert_DefectData() : Frm_3_2_SqlFactory.SQL_Update_DefectData(Txt_Out_Coil_ID.Text, strPlan_No);
                }
            }

            if (string.IsNullOrEmpty(strSql)) return;

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, "新增/修改PDO"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"新增/修改PDO失败", "新增/修改PDO", 3);
                return;
            }

            if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql_Defect, "新增/修改PDO缺陷"))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"新增/修改PDO缺陷失败", "新增/修改PDO缺陷", 3);
                return;
            }

            ReadOnlyControl(Tab_PDODetailPage, true);
            ReadOnlyControl(Tab_PDODefectPage, true);
            //Btn淨重計算 不啟用
            Btn_GetNewWt.Enabled = false;

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //修改
            Fun_SetBottonEnabled(Btn_Update, true);
            //上传MMS
            Fun_SetBottonEnabled(Btn_MMS, true);

            Btn_Cancel.Visible = false; //取消
            Btn_Save.Visible = false; //儲存

            ////Read 不顯示 ; 編輯模式才顯示(功能暫時取消)
            //Btn_Head_Calc.Visible = false;
            //Btn_Tail_Calc.Visible = false;

            strEditStatus = "Read";
            bolEditStatus = false;

            if (Pnl_Spare.Visible == true)
                Pnl_Spare.Visible = false;

            ////通知Server PDO已修改過資料
            //SCCommMsg.CS08_WeightInput _WeightInput = new SCCommMsg.CS08_WeightInput
            //{
            //    Source = "CPL1_HMI",
            //    ID = "WeightInput",
            //    OutCoilID = strOut_Coil_ID,                
            //    //FinishTime = strFinishTime,
            //    //In_Coil_ID = strIn_Coil_ID,
            //    //Plan_No = strPlan_No,
            //    WeightInput = Txt_Out_Coil_Gross_WT.Text.Trim()
            //};

            //PublicComm.Client.Tell(_WeightInput);

            //PublicComm.ClientLog.Info($"已通知Server <{strOut_Coil_ID}>PDO修改過");
            //PublicComm.AkkaLog.Info($"已通知Server <{strOut_Coil_ID}>PDO修改過");

            //DialogHandler.Instance.Fun_DialogShowOk($"已通知Server PDO异动", "新增/修改PDO", 4);
            //EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.lblLoginUser.Text} 出口钢卷编号:{strOut_Coil_ID} PDO異動", $" 出口钢卷编号:{strOut_Coil_ID}PDO異動");
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.Append($"使用者:{PublicForms.Main.Lbl_LoginUser.Text} 出口钢卷编号:{strOut_Coil_ID} PDO异动记录。 ");
            sbMessage.AppendLine($"操作者权限：{UserSetupHandler.Instance.Authority_Class_Show} ");
            sbMessage.Append($"客户端：{GlobalVariableHandler.Instance.getIpAdderss} ");
            sbMessage.AppendLine($"修改时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} ");
            string strMessage = sbMessage.ToString();
                //$"使用者:{PublicForms.Main.lblLoginUser.Text} 出口钢卷编号:{strOut_Coil_ID} PDO异动记录。 " +
                //                $"操作者权限：{UserSetupHandler.Instance.Authority_Class_Show} " +
                //                $"客户端：{GlobalVariableHandler.Instance.getIpAdderss} " +
                //                $"修改时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} ";

            //TBL_EventLog_Client
            EventLogHandler.Instance.LogInfo("3-2", $"{strMessage}", $" 出口钢卷编号:{strOut_Coil_ID}PDO异动记录。");
            EventLogHandler.Instance.LogInfo("3-2", $"{strFinDeff}", $" 出口钢卷编号:{strOut_Coil_ID}PDO异动记录。");
            //下方事件訊息
            EventLogHandler.Instance.EventPush_Message(strMessage);
            //Log檔
            PublicComm.ClientLog.Info(strMessage);
            PublicComm.ClientLog.Info(strFinDeff);
            Fun_SelectCoilPDO(strOut_Coil_ID, str_Now_FinishTime, strIn_Coil_ID, strPlan_No);
        }

        private string Fun_GetTitle(string strColumnName)
        {           
            string strLang = LanguageHandler.Instance.DefaultLanguage ? nameof(TBL_LangSwitch_Ctr.ZH) : nameof(TBL_LangSwitch_Ctr.EN);
            string strReturn = strColumnName;
            if ( dtLangSwitch_Column != null && dtLangSwitch_Column.Rows.Count > 0)
            {
                DataRow[] drText = dtLangSwitch_Column.Select($"ColumnName = '{strColumnName}'");
                if(drText != null && drText.Length >0)
                    strReturn = drText[0][strLang].ToString();
            }
            return strReturn;
        }

        private string Fun_GetTitle_Def(string strColumnName)
        {
            string strNo = strColumnName.Remove(3).Remove(0, 1);//01
            string strColumn = strColumnName.Remove(0, 4);//Code

            string strLang = LanguageHandler.Instance.DefaultLanguage ? nameof(TBL_LangSwitch_Ctr.ZH) : nameof(TBL_LangSwitch_Ctr.EN);
            string strReturn = strColumnName;
            if (dtLangSwitch_Column != null && dtLangSwitch_Column.Rows.Count > 0)
            {
                DataRow[] drText = dtLangSwitch_Column.Select($"ColumnName = '{strColumn}'");

                strReturn = drText[0][strLang].ToString()+ strNo;
            }
            return strReturn;
        }

        private double Fun_TryParseDouble(string strData)
        {          
            double.TryParse(strData, out double dou_Data);
            return dou_Data;
        }

        private void Fun_GetUpdateDataToTable(DataTable dtSaveData)
        {
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.OrderNo)] = PublicForms.PDODetail.Txt_Order_No.Text;
            //--[nameof(PDOEntity.TBL_PDO.Plan_No)] = PublicForms.PDODetail.Txt_Plan_No.Text;
            //--[nameof(PDOEntity.TBL_PDO.Out_Coil_ID)] = PublicForms.PDODetail.Txt_Out_Coil_ID.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.In_Coil_ID)] = PublicForms.PDODetail.Txt_In_Coil_ID.Text;
            //dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.StartTime)] = strStartTime;
            //--[nameof(PDOEntity.TBL_PDO.FinishTime)] = strFinishTime;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Shift)] = PublicForms.PDODetail.Cob_Shift.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Team)] = PublicForms.PDODetail.Cob_Team.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Outer_Diameter)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Out_Coil_Outer_Diameter.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Inner)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Out_Coil_Inner.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Wt)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Out_Coil_Wt.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Out_Coil_Gross_WT.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Out_Coil_Thick.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Width)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Out_Coil_Width.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Length)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Out_Coil_Length.Text);           
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Entry)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Header_Cut_Length_Entry.Text); 
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Entry)] = Fun_TryParseDouble(Txt_Tail_Cut_Length_Entry.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Exit)] = Fun_TryParseDouble(Txt_Header_Cut_Length_Exit.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Exit)] = Fun_TryParseDouble(Txt_Tail_Cut_Length_Exit.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Paper_Code)] = PublicForms.PDODetail.Cob_Paper_Code.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Paper_Req_Code)] = PublicForms.PDODetail.Cob_Paper_Req_Code.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Head_Paper_Length)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Out_Head_Paper_Length.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Head_Paper_Width)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Out_Head_Paper_Width.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Tail_Paper_Length)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Out_Tail_Paper_Length.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Tail_Paper_Width)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Out_Tail_Paper_Width.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Sleeve_Inner_Exit_Diamter)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Sleeve_Inner_Exit_Diamter.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Sleeve_Type_Exit_Code)] = PublicForms.PDODetail.Cob_Sleeve_Type_Exit_Code.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Hole_Position)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Head_Hole_Position.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_Length)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Head_Leader_Length.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_Width)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Head_Leader_Width.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_Thickness)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Head_Leader_Thickness.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_PunchHole_Position)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Tail_PunchHole_Position.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_Length)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Tail_Leader_Length.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_Width)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Tail_Leader_Width.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_Thickness)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Tail_Leader_Thickness.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Scraped_Length_Entry)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Scraped_Length_Entry.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Scraped_Length_Exit)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Scraped_Length_Exit.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_St_No)] = PublicForms.PDODetail.Txt_Head_Leader_St_No.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_St_No)] = PublicForms.PDODetail.Txt_Tail_Leader_St_No.Text;

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Gap)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Avg_Side_Trimmer_Gap.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Lap)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Avg_Side_Trimmer_Lap.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Width)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Avg_Side_Trimmer_Width.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Avg_Trimming_OperateSide)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Avg_Trimming_OperateSide.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Avg_Trimming_DriveSide)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Avg_Trimming_DriveSide.Text);

            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Winding_Direction)] = PublicForms.PDODetail.Cob_Winding_Direction.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Base_Surface)] = PublicForms.PDODetail.Cob_Base_Surface.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Inspector)] = PublicForms.PDODetail.Txt_Inspector.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Hold_Flag)] = PublicForms.PDODetail.Cob_Hold_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Hold_Cause_Code)] = PublicForms.PDODetail.Txt_Hold_Cause_Code.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Sample_Flag)] = PublicForms.PDODetail.Cob_Sample_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Trim_Flag)] = PublicForms.PDODetail.Cob_Trim_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Fixed_WT_Flag)] = PublicForms.PDODetail.Cob_Fixed_WT_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.End_Flag)] = PublicForms.PDODetail.Cob_End_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Scrap_Flag)] = PublicForms.PDODetail.Cob_Scrap_Flag.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Sample_Frqn_Code)] = PublicForms.PDODetail.Cob_Sample_Frqn_Code.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.No_Leader_Code)] = PublicForms.PDODetail.Txt_No_Leader_Code.Text;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Surface_Accuracy_Code)] = PublicForms.PDODetail.Cob_Surface_Accuracy_Code.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Off_Gauge)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Head_Off_Gauge.Text);
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Off_Gauge)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Tail_Off_Gauge.Text);
            //dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_In)] = PublicForms.PDODetail.Cob_Surface_Accu_Code_In.SelectedValue;
            //dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_Out)] = PublicForms.PDODetail.Cob_Surface_Accu_Code_Out.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Flip_Tag)] = PublicForms.PDODetail.Cob_Flip_Tag.SelectedValue; 
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Process_Code)] = PublicForms.PDODetail.Cob_ProcessCode.SelectedValue;         
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Decoiler_Direction)] = PublicForms.PDODetail.Cob_Decoiler_Direction.SelectedValue;
            dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.Recoiler_Actten_Avg)] = Fun_TryParseDouble(PublicForms.PDODetail.Txt_Recoiler_Actten_Avg.Text);

           

            //dtSaveData.Rows[0][nameof(PDOEntity.TBL_PDO.CreateTime)] = GlobalVariableHandler.Instance.getTime;
        }

        private void Fun_GetUpdateDataToTable_Def(DataTable dtSaveData)
        {
            if (dtSaveData.IsNull())
            {
                DataRow dr = dtSaveData.NewRow();
                try
                {
                    dtSaveData.LoadDataRow(dr.ItemArray, false);
                }
                catch { }
            }
            else { }

            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Code)] = Txt_Code_D01.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Origin)] = Txt_Origin_D01.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Sid)] = Cob_Sid_D01.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_W)] = Cob_PosW_D01.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_Start)] = Txt_Pos_L_Start_D01.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_End)] = Txt_Pos_L_End_D01.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Level)] = Cob_Level_D01.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Percent)] = Txt_Percent_D01.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_QGRADE)] = Txt_QGRADE_D01.Text.Trim();

            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Code)] = Txt_Code_D02.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Origin)] = Txt_Origin_D02.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Sid)] = Cob_Sid_D02.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_W)] = Cob_PosW_D02.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_Start)] = Txt_Pos_L_Start_D02.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_End)] = Txt_Pos_L_End_D02.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Level)] = Cob_Level_D02.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Percent)] = Txt_Percent_D02.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_QGRADE)] = Txt_QGRADE_D02.Text.Trim();

            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Code)] = Txt_Code_D03.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Origin)] = Txt_Origin_D03.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Sid)] = Cob_Sid_D03.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_W)] = Cob_PosW_D03.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_Start)] = Txt_Pos_L_Start_D03.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_End)] = Txt_Pos_L_End_D03.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Level)] = Cob_Level_D03.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Percent)] = Txt_Percent_D03.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_QGRADE)] = Txt_QGRADE_D03.Text.Trim();

            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Code)] = Txt_Code_D04.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Origin)] = Txt_Origin_D04.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Sid)] = Cob_Sid_D04.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_W)] = Cob_PosW_D04.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_Start)] = Txt_Pos_L_Start_D04.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_End)] = Txt_Pos_L_End_D04.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Level)] = Cob_Level_D04.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Percent)] = Txt_Percent_D04.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_QGRADE)] = Txt_QGRADE_D04.Text.Trim();

            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Code)] = Txt_Code_D05.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Origin)] = Txt_Origin_D05.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Sid)] = Cob_Sid_D05.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_W)] = Cob_PosW_D05.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_Start)] = Txt_Pos_L_Start_D05.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_End)] = Txt_Pos_L_End_D05.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Level)] = Cob_Level_D05.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Percent)] = Txt_Percent_D05.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_QGRADE)] = Txt_QGRADE_D05.Text.Trim();

            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Code)] = Txt_Code_D06.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Origin)] = Txt_Origin_D06.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Sid)] = Cob_Sid_D06.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_W)] = Cob_PosW_D06.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_Start)] = Txt_Pos_L_Start_D06.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_End)] = Txt_Pos_L_End_D06.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Level)] = Cob_Level_D06.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Percent)] = Txt_Percent_D06.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_QGRADE)] = Txt_QGRADE_D06.Text.Trim();

            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Code)] = Txt_Code_D07.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Origin)] = Txt_Origin_D07.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Sid)] = Cob_Sid_D07.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_W)] = Cob_PosW_D07.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_Start)] = Txt_Pos_L_Start_D07.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_End)] = Txt_Pos_L_End_D07.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Level)] = Cob_Level_D07.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Percent)] = Txt_Percent_D07.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_QGRADE)] = Txt_QGRADE_D07.Text.Trim();

            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Code)] = Txt_Code_D08.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Origin)] = Txt_Origin_D08.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Sid)] = Cob_Sid_D08.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_W)] = Cob_PosW_D08.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_Start)] = Txt_Pos_L_Start_D08.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_End)] = Txt_Pos_L_End_D08.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Level)] = Cob_Level_D08.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Percent)] = Txt_Percent_D08.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_QGRADE)] = Txt_QGRADE_D08.Text.Trim();

            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Code)] = Txt_Code_D09.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Origin)] = Txt_Origin_D09.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Sid)] = Cob_Sid_D09.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_W)] = Cob_PosW_D09.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_Start)] = Txt_Pos_L_Start_D09.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_End)] = Txt_Pos_L_End_D09.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Level)] = Cob_Level_D09.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Percent)] = Txt_Percent_D09.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_QGRADE)] = Txt_QGRADE_D09.Text.Trim();

            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Code)] = Txt_Code_D10.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Origin)] = Txt_Origin_D10.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Sid)] = Cob_Sid_D10.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_W)] = Cob_PosW_D10.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_Start)] = Txt_Pos_L_Start_D10.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_End)] = Txt_Pos_L_End_D10.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Level)] = Cob_Level_D10.Text;
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Percent)] = Txt_Percent_D10.Text.Trim();
            dtSaveData.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_QGRADE)] = Txt_QGRADE_D10.Text.Trim();


        }
        
        /// <summary>
        /// 檢查必填欄位是否空白
        /// </summary>
        /// <param name="intOtherMust">0:一般儲存前檢查 ; 1:上傳MMS前檢查</param>
        /// <returns></returns>
        private bool Fun_IsColumnsEmpty(int intOtherMust = 0)
        {
            if (string.IsNullOrEmpty(Txt_Out_Coil_ID.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"出口钢卷号 请勿空白! ", "提示资讯", 0);               
                Txt_Out_Coil_ID.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(Txt_In_Coil_ID.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"入口钢卷号 请勿空白! ", "提示资讯", 0);               
                Txt_In_Coil_ID.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(Txt_Plan_No.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"计划号 请勿空白! ", "提示资讯", 0);
                Txt_Plan_No.Focus();
                return false;
            }

            //分卷标记
            if (string.IsNullOrEmpty(Cob_Fixed_WT_Flag.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"分卷标记 请勿空白! ", "提示资讯",  0);
                Cob_Fixed_WT_Flag.Focus();
                return false;
            }

            //最终卷标记
            if (string.IsNullOrEmpty(Cob_End_Flag.Text.Trim()))
            {
                DialogHandler.Instance.Fun_DialogShowOk($"最终卷标记 请勿空白! ", "提示资讯", 0);
                Cob_End_Flag.Focus();
                return false;
            }
            //最终卷标记与分卷标记预设为空白,并检查两个栏位不会同时为0
            if (Cob_Fixed_WT_Flag.SelectedIndex == 0 && Cob_End_Flag.SelectedIndex == 0)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"分卷标记与最终卷标记 不可同时为0! ", "提示资讯",  0);
                Cob_End_Flag.Focus();
                return false;
            }

            if (intOtherMust == 1)
            {
                //合同號   Txt_Order_No
                if (string.IsNullOrEmpty(Txt_Order_No.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"合同号 请勿空白! ", "提示资讯",  0);
                    Txt_Order_No.Focus();
                    return false;
                }
                //出口材料内徑    Txt_Out_Coil_Inner_Diameter
                if (string.IsNullOrEmpty(Txt_Out_Coil_Inner.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷内径 请勿空白! ", "提示资讯",  0);
                    Txt_Out_Coil_Inner.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Inner.Text.Trim(), out double douMore);
                    if(douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷内径 请勿小于0! ", "提示资讯", 0);
                        Txt_Out_Coil_Inner.Focus();
                        return false;
                    }
                }
                //出口材料外徑    Txt_Out_Coil_Outer_Diameter
                if (string.IsNullOrEmpty(Txt_Out_Coil_Outer_Diameter.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷外径 请勿空白! ", "提示资讯",  0);
                    Txt_Out_Coil_Outer_Diameter.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Outer_Diameter.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷外径 请勿小于0! ", "提示资讯", 0);
                        Txt_Out_Coil_Outer_Diameter.Focus();
                        return false;
                    }
                }

                //出口材料净重    Txt_Out_Coil_Act_WT
                ////pdo净重小於等於pdi入料重的時候，才可以上抛，否則提示不讓上抛之原因。

                if (string.IsNullOrEmpty(Txt_Out_Coil_Wt.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷净重 请勿空白! ", "提示资讯",  0);
                    Txt_Out_Coil_Wt.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Wt.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷净重 请勿小于0! ", "提示资讯", 0);
                        Txt_Out_Coil_Wt.Focus();
                        return false;
                    }
                }

                //出口材料毛重    Txt_Out_Coil_Gross_WT
                if (string.IsNullOrEmpty(Txt_Out_Coil_Gross_WT.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷毛重 请勿空白! ", "提示资讯",  0);
                    Txt_Out_Coil_Gross_WT.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Gross_WT.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷毛重 请勿小于0! ", "提示资讯", 0);
                        Txt_Out_Coil_Gross_WT.Focus();
                        return false;
                    }
                }

                //净重不得大于毛重     (净重:douAct_WT ; 毛重:douGross_WT)
                if (double.TryParse(Txt_Out_Coil_Wt.Text.Trim(), out double douAct_WT))
                {
                    if (double.TryParse(Txt_Out_Coil_Gross_WT.Text.Trim(), out double douGross_WT))
                    {
                        if (douAct_WT > douGross_WT)
                        {
                            DialogHandler.Instance.Fun_DialogShowOk($"出口卷净重 大于 出口卷毛重 不可上传! ", "提示资讯",  0);
                            //Txt_Out_Coil_Gross_WT.Focus();
                            return false;
                        }

                    }
                }


                //出口材料厚度    Txt_Out_Coil_Thick
                if (string.IsNullOrEmpty(Txt_Out_Coil_Thick.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷厚度 请勿空白! ", "提示资讯",  0);
                    Txt_Out_Coil_Thick.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Thick.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷厚度 请勿小于0! ", "提示资讯", 0);
                        Txt_Out_Coil_Thick.Focus();
                        return false;
                    }
                }
                //出口材料寬度    Txt_Out_Coil_Width
                if (string.IsNullOrEmpty(Txt_Out_Coil_Width.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷宽度 请勿空白! ", "提示资讯",  0);
                    Txt_Out_Coil_Width.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Width.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷宽度 请勿小于0! ", "提示资讯", 0);
                        Txt_Out_Coil_Width.Focus();
                        return false;
                    }
                }
                //出口材料長度    Txt_Out_Coil_Length
                if (string.IsNullOrEmpty(Txt_Out_Coil_Length.Text.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"出口卷长度 请勿空白! ", "提示资讯",  0);
                    Txt_Out_Coil_Length.Focus();
                    return false;
                }
                else
                {
                    double.TryParse(Txt_Out_Coil_Length.Text.Trim(), out double douMore);
                    if (douMore <= 0)
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"出口卷长度 请勿小于0! ", "提示资讯", 0);
                        Txt_Out_Coil_Length.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        //設定按鈕啟用狀態並改顏色
        public void Fun_SetBottonEnabled(Button btn, bool bolE)
        {
            btn.Enabled = bolE;

            if (btn.Name.Equals(Btn_MMS.Name) || btn.Name.Equals(Btn_PrintTag.Name))
                btn.BackColor = bolE ? Color.Gold : Color.LightGray;
            else
                //Color colorBack;
                btn.BackColor = bolE ? Color.CornflowerBlue : Color.LightGray;
        }

        public async void Handle_SC08_PdoUploadedReply(string msg)
        {
            //  非同步處理
            await Task.Run(() => {
                //  建立調跳視窗
                var frm_D = new Frm_DialogOK
                {
                    MaximizeBox = false,                            //  鎖定放大
                    MinimizeBox = false                             //  鎖定縮小
                };
                //  註冊 Shown 事件，處理自動關閉視窗
                frm_D.Shown += async (s, e) => {
                    //  置頂
                    frm_D.TopMost = true;
                    //  等待延遲
                    await Task.Delay(60000);
                    //  關閉本視窗
                    frm_D.Close();
                };
                //  註冊 MouseDown 事件，處理拖曳視窗起始
                frm_D.MouseDown += (s, e) => {
                    if (e.Button == MouseButtons.Left)
                    {
                        frm_D.Dragging = true;
                        frm_D.DragOffset = e.Location;
                    }
                };
                //  註冊 MouseMove 事件，處理拖曳視窗期間
                frm_D.MouseMove += (s, e) => {
                    if (frm_D.Dragging)
                    {
                        var newLocation = frm_D.Location;
                        newLocation.X += e.Location.X - frm_D.DragOffset.X;
                        newLocation.Y += e.Location.Y - frm_D.DragOffset.Y;
                        frm_D.Location = newLocation;
                    }
                };
                //  註冊 MouseUp 事件，處理拖曳視窗結束
                frm_D.MouseUp += (s, e) => {
                    if (e.Button == MouseButtons.Left)
                    {
                        frm_D.Dragging = false;
                        frm_D.DragOffset = Point.Empty;
                    }
                };
                //  傳入要顯示的訊息
                frm_D.DialogShow($"{msg}", "提示资讯", null, 0);
                //  顯示視窗
                frm_D.ShowDialog();
                //  釋放資源
                frm_D.Dispose();
            });
        }

        #region 欄位ReadOnly & BackgroundColor

        private void ReadOnlyControl(TabPage page, bool bolReadOnly)
        {
            ReadOnlyHandler.Instance.ReadOnly(page, bolReadOnly);
        }

        #endregion

        #region SelectData

        public void Fun_SelectCoilPDO(string Coil_ID, string strFinishTime = null, string strIn_Coil_ID = null, string strPlan_No = null)
        {
            Lbl_Defect_IsNull.Visible = false;

           

            string strSql = Frm_3_2_SqlFactory.SQL_Select_PDODetail(Coil_ID, strFinishTime, strIn_Coil_ID, strPlan_No);
            DataTable dtGetCoilPDO = DataAccess.Fun_SelectDate(strSql, "PDO");

            if (dtGetCoilPDO.IsNull())
            {
                if (!string.IsNullOrEmpty(Coil_ID.Trim()))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"查无钢卷号 [{Coil_ID.Trim()}] PDO", "查詢PDO", 0);
                }
               

                //詳細資料頁籤清空
                Fun_ClearDisplay(Tab_PDODetailPage);
                Lbl_OutCoil_Defect.Text = "";
                Fun_ClearPanelText(Pnl_ReadOnly);
                //缺陷資料頁籤清空
                Fun_ClearDisplay(Tab_PDODefectPage);

                Fun_ClearGroupBoxText(Grb_HeadLeader);

                Fun_ClearGroupBoxText(Grb_TailLeader);
                //如果之前沒有顯示資料,複製欄位
                dtSelectOne = dtGetCoilPDO.Clone();
                dtBeforeEdit = dtSelectOne.Copy();
                return;
            }
            else if(dtGetCoilPDO.Rows.Count == 1)
            {
                Coil_ID = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString();
                strIn_Coil_ID = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString();
                strPlan_No = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Plan_No)].ToString();
            }

            DataTable dtSelectBack = new DataTable();

            if (dtGetCoilPDO.Rows.Count > 1)
            {
                Frm_SelectDataOpen frm_SelectOpen = new Frm_SelectDataOpen
                {
                    dtSelectData = dtGetCoilPDO.Copy()
                    ,strDataType="PDO"
                };
                frm_SelectOpen.ShowDialog();
                frm_SelectOpen.Dispose();

                if(frm_SelectOpen.DialogResult == DialogResult.OK)
                {
                    //string strSql_Select = Frm_3_2_SqlFactory.SQL_Select_PDODetail(frm_SelectOpen.strOut_Coil_ID, frm_SelectOpen.strFinishTime, frm_SelectOpen.strIn_Coil_ID, frm_SelectOpen.strPlan_No);
                    //dtSelectBack = DataAccess.Fun_SelectDate(strSql_Select, "PDODet");
                    dtSelectBack = frm_SelectOpen.dtSelectData.Copy();
                }
            }
            
            if(dtSelectBack != null && dtSelectBack.Rows.Count >0)
            {               
                Coil_ID = dtSelectBack.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString();
                //DateTime dTime = DateTime.Parse(dtSelectBack.Rows[0]["FinishTime"].ToString());
                //strFinishTime = dTime.ToString("yyyy-MM-dd HH:mm:ss");//2

                //FinishTime_Str
                strFinishTime = dtSelectBack.Rows[0]["FinishTime_Str"].ToString();
                str_Now_FinishTime = strFinishTime;//紀錄現在查詢的FinishTime_fff

                //strFinishTime = dtSelectBack.Rows[0][ nameof(PDOEntity.TBL_PDO.FinishTime)].ToString();
                strIn_Coil_ID = dtSelectBack.Rows[0][nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString();
                strPlan_No = dtSelectBack.Rows[0][nameof(PDOEntity.TBL_PDO.Plan_No)].ToString();

                string strSql_Select = Frm_3_2_SqlFactory.SQL_Select_PDODetail(Coil_ID, strFinishTime, strIn_Coil_ID, strPlan_No);
                dtGetCoilPDO = DataAccess.Fun_SelectDate(strSql_Select, "PDO");
            }

            Fun_ComboBoxItems();

            Fun_DisplayPDO(dtGetCoilPDO);
            dtSelectOne = dtGetCoilPDO.Copy();

            //新增
            Fun_SetBottonEnabled(Btn_New, true);
            //修改
            Fun_SetBottonEnabled(Btn_Update, true);


            //上传MMS
            Fun_SetBottonEnabled(Btn_MMS, true);


            strSql = Frm_3_2_SqlFactory.SQL_Select_DefectData(Coil_ID, strPlan_No);
            DataTable dt_Defect = DataAccess.Fun_SelectDate(strSql, "PDO缺陷资料");

            if (dt_Defect.IsNull())
            {
                string strMsg_DefectIsNull = $"无缺陷资料!{Environment.NewLine}若要查询母卷缺陷，请至[缺陷资料]页签使用右下角[母卷缺陷]按钮。";

                Lbl_Defect_IsNull.Text = strMsg_DefectIsNull;

                Lbl_Defect_IsNull.Visible = true;
                //DialogHandler.Instance.Fun_DialogShowOk($"此钢卷号 [{Coil_ID.Trim()}]无缺陷资料!{Environment.NewLine}若要查询母卷缺陷，请至[缺陷资料]页签使用右下角[母卷缺陷]按钮", "查詢缺陷资料", 0);

                bolDefectIsNull = true;

                Fun_ClearDisplay(Tab_PDODefectPage);

                //如果之前沒有顯示資料,複製欄位
                dtSelectOne_Def = dt_Defect.Clone();
                dtBeforeEdit_Def = dtSelectOne_Def.Copy();
                //return;
            }
            else
            {
                bolDefectIsNull = false;

                Fun_SelectDefect(dt_Defect);
                dtSelectOne_Def = dt_Defect.Copy();
            }

            

            //取得PDI 出纲纪号
            strSql = Frm_1_2_SqlFactory.SQL_Select_PDIDetail(Coil_ID, strPlan_No);
            DataTable dt_Pdi = DataAccess.Fun_SelectDate(strSql, "PDI资料");
            if (!dt_Pdi.IsNull())
                Txt_St_No.Text = dt_Pdi.Rows[0][nameof(CoilPDIEntity.TBL_PDI.St_No)].ToString();
            else
                Txt_St_No.Text = "";
        }

        /// <summary>
        /// 缺陷資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        private void Fun_SelectDefect(DataTable dt_Defect)
        {
            
            Fun_ClearDisplay(Tab_PDODefectPage);

            #region 填入资料

          

            #region  - Code -
            Txt_Code_D01.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Code)].ToString();
            Txt_Code_D02.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Code)].ToString();
            Txt_Code_D03.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Code)].ToString();
            Txt_Code_D04.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Code)].ToString();
            Txt_Code_D05.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Code)].ToString();
            Txt_Code_D06.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Code)].ToString();
            Txt_Code_D07.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Code)].ToString();
            Txt_Code_D08.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Code)].ToString();
            Txt_Code_D09.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Code)].ToString();
            Txt_Code_D10.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Code)].ToString();
            #endregion

            #region - Origin -
            Txt_Origin_D01.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Origin)].ToString();
            Txt_Origin_D02.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Origin)].ToString();
            Txt_Origin_D03.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Origin)].ToString();
            Txt_Origin_D04.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Origin)].ToString();
            Txt_Origin_D05.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Origin)].ToString();
            Txt_Origin_D06.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Origin)].ToString();
            Txt_Origin_D07.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Origin)].ToString();
            Txt_Origin_D08.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Origin)].ToString();
            Txt_Origin_D09.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Origin)].ToString();
            Txt_Origin_D10.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Origin)].ToString();
            #endregion

            #region - Sid (ComboBox)-
            Cob_Sid_D01.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Sid)].ToString();
            Cob_Sid_D02.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Sid)].ToString();
            Cob_Sid_D03.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Sid)].ToString();
            Cob_Sid_D04.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Sid)].ToString();
            Cob_Sid_D05.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Sid)].ToString();
            Cob_Sid_D06.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Sid)].ToString();
            Cob_Sid_D07.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Sid)].ToString();
            Cob_Sid_D08.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Sid)].ToString();
            Cob_Sid_D09.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Sid)].ToString();
            Cob_Sid_D10.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Sid)].ToString();
            #endregion

            #region - Pos_W (ComboBox)-
            Cob_PosW_D01.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_W)].ToString();
            Cob_PosW_D02.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_W)].ToString();
            Cob_PosW_D03.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_W)].ToString();
            Cob_PosW_D04.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_W)].ToString();
            Cob_PosW_D05.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_W)].ToString();
            Cob_PosW_D06.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_W)].ToString();
            Cob_PosW_D07.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_W)].ToString();
            Cob_PosW_D08.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_W)].ToString();
            Cob_PosW_D09.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_W)].ToString();
            Cob_PosW_D10.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_W)].ToString();
            #endregion

            #region - Pos_L_Start - 
            Txt_Pos_L_Start_D01.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D02.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D03.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D04.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D05.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D06.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D07.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D08.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D09.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_Start)].ToString();
            Txt_Pos_L_Start_D10.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_Start)].ToString();
            #endregion

            #region - Pos_L_End -
            Txt_Pos_L_End_D01.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Pos_L_End)].ToString();
            Txt_Pos_L_End_D02.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Pos_L_End)].ToString();
            Txt_Pos_L_End_D03.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Pos_L_End)].ToString();
            Txt_Pos_L_End_D04.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Pos_L_End)].ToString();
            Txt_Pos_L_End_D05.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Pos_L_End)].ToString();
            Txt_Pos_L_End_D06.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Pos_L_End)].ToString();
            Txt_Pos_L_End_D07.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Pos_L_End)].ToString();
            Txt_Pos_L_End_D08.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Pos_L_End)].ToString();
            Txt_Pos_L_End_D09.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Pos_L_End)].ToString();
            Txt_Pos_L_End_D10.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Pos_L_End)].ToString();
            #endregion

            #region - Level (ComboBox)-
            Cob_Level_D01.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Level)].ToString();
            Cob_Level_D02.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Level)].ToString();
            Cob_Level_D03.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Level)].ToString();
            Cob_Level_D04.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Level)].ToString();
            Cob_Level_D05.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Level)].ToString();
            Cob_Level_D06.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Level)].ToString();
            Cob_Level_D07.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Level)].ToString();
            Cob_Level_D08.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Level)].ToString();
            Cob_Level_D09.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Level)].ToString();
            Cob_Level_D10.SelectedItem = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Level)].ToString();
            #endregion

            #region - Percent -
            Txt_Percent_D01.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_Percent)].ToString();
            Txt_Percent_D02.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_Percent)].ToString();
            Txt_Percent_D03.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_Percent)].ToString();
            Txt_Percent_D04.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_Percent)].ToString();
            Txt_Percent_D05.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_Percent)].ToString();
            Txt_Percent_D06.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_Percent)].ToString();
            Txt_Percent_D07.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_Percent)].ToString();
            Txt_Percent_D08.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_Percent)].ToString();
            Txt_Percent_D09.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_Percent)].ToString();
            Txt_Percent_D10.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_Percent)].ToString();
            #endregion

            #region - QGrade - 
            Txt_QGRADE_D01.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D01_QGRADE)].ToString();
            Txt_QGRADE_D02.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D02_QGRADE)].ToString();
            Txt_QGRADE_D03.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D03_QGRADE)].ToString();
            Txt_QGRADE_D04.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D04_QGRADE)].ToString();
            Txt_QGRADE_D05.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D05_QGRADE)].ToString();
            Txt_QGRADE_D06.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D06_QGRADE)].ToString();
            Txt_QGRADE_D07.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D07_QGRADE)].ToString();
            Txt_QGRADE_D08.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D08_QGRADE)].ToString();
            Txt_QGRADE_D09.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D09_QGRADE)].ToString();
            Txt_QGRADE_D10.Text = dt_Defect.Rows[0][nameof(CoilDefectDataEntity.TBL_Coil_Defect.D10_QGRADE)].ToString();
            #endregion

            #endregion

        }

        /// <summary>
        /// 欄位資料填入
        /// </summary>
        public void Fun_DisplayPDO(DataTable dtGetCoilPDO)
        {
            Fun_ClearDisplay(Tab_PDODetailPage);

            Fun_ClearGroupBoxText(Grb_HeadLeader);

            Fun_ClearGroupBoxText(Grb_TailLeader);

            Fun_ClearGroupBoxText(Grb_Cut_Length_Entry);

            Fun_ClearGroupBoxText(Grb_Cut_Length_Exit);

            Fun_ClearPanelText(Pnl_ReadOnly);
            //合同號
            Txt_Order_No.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.OrderNo)].ToString();
            //計畫號
            Txt_Plan_No.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Plan_No)].ToString();
            //出口鋼卷號
            Txt_Out_Coil_ID.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString();
            Lbl_OutCoil_Defect.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_ID)].ToString();
            //入口鋼卷號
            Txt_In_Coil_ID.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.In_Coil_ID)].ToString();
            //開始時間
            //txtStarttime.Text = dtGetCoilPDO.Rows[0][nameof(PDOModel.TBL_PDO.StartTime)].ToString();
            //Dtp_StartTime.Value = Fun_ConverDateTime(dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.StartTime)].ToString(), "yyyy-MM-dd HH:mm:ss");//"yyyyMMdd"
            Dtp_StartTime.Value = Convert.ToDateTime(dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.StartTime)]);
            //結束時間
            //txtFinishtime.Text = dtGetCoilPDO.Rows[0][nameof(PDOModel.TBL_PDO.FinishTime)].ToString();
            //Dtp_FinishTime.Value = Fun_ConverDateTime(dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.FinishTime)].ToString() , "yyyy-MM-dd HH:mm:ss");//"yyyyMMdd"
            Dtp_FinishTime.Value = Convert.ToDateTime(dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.FinishTime)]);
            str_Now_FinishTime = dtGetCoilPDO.Rows[0]["FinishTime_Str"].ToString();

            //班次
            Cob_Shift.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Shift)].ToString();
            //班次
            Cob_Team.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Team)].ToString();
            //出口外徑
            Txt_Out_Coil_Outer_Diameter.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Outer_Diameter)].ToString();
            //出口內徑
            Txt_Out_Coil_Inner.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Inner)].ToString();
            //出口淨重
            Txt_Out_Coil_Wt.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Wt)].ToString();
            //出口毛重
            Txt_Out_Coil_Gross_WT.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Gross_WT)].ToString();
            //出口厚度
            Txt_Out_Coil_Thick.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Thick)].ToString();
            //出口寬度
            Txt_Out_Coil_Width.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Width)].ToString();
            //出口長度
            Txt_Out_Coil_Length.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Coil_Length)].ToString();

            //入口剪裁切鋼捲頭部的長度
            Txt_Header_Cut_Length_Entry.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Entry)].ToString();
            //入口剪裁切鋼捲尾部的長度
            Txt_Tail_Cut_Length_Entry.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Entry)].ToString();
            //出口剪裁切鋼捲頭部的長度
            Txt_Header_Cut_Length_Exit.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Exit)].ToString();
            //出口剪裁切鋼捲尾部的長度
            Txt_Tail_Cut_Length_Exit.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Exit)].ToString();

            //出口墊紙種類
            Cob_Paper_Code.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Paper_Code)].ToString();
            //出口墊紙方式
            Cob_Paper_Req_Code.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Paper_Req_Code)].ToString();
            //头部墊紙長度
            Txt_Out_Head_Paper_Length.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Head_Paper_Length)].ToString();
            //头部墊紙寬度
            Txt_Out_Head_Paper_Width.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Head_Paper_Width)].ToString();
            //尾部垫纸长度
            Txt_Out_Tail_Paper_Length.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Tail_Paper_Length)].ToString();
            //尾部垫纸宽度
            Txt_Out_Tail_Paper_Width.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Out_Tail_Paper_Width)].ToString();
            //套筒内径
            Txt_Sleeve_Inner_Exit_Diamter.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Sleeve_Inner_Exit_Diamter)].ToString();
            //套筒类型
            Cob_Sleeve_Type_Exit_Code.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Sleeve_Type_Exit_Code)].ToString();
            //头部打孔位置
            Txt_Head_Hole_Position.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Hole_Position)].ToString();
            //头部导带长度
            Txt_Head_Leader_Length.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_Length)].ToString();
            //头部导带宽度
            Txt_Head_Leader_Width.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_Width)].ToString();
            //头部导带厚度
            Txt_Head_Leader_Thickness.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_Thickness)].ToString();
            //尾部打孔位置
            Txt_Tail_PunchHole_Position.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_PunchHole_Position)].ToString();
            //尾部导带长度
            Txt_Tail_Leader_Length.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_Length)].ToString();
            //尾部导带宽度
            Txt_Tail_Leader_Width.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_Width)].ToString();
            //尾部导带厚度
            Txt_Tail_Leader_Thickness.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_Thickness)].ToString();
            //头段切废
            Txt_Scraped_Length_Entry.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Scraped_Length_Entry)].ToString();
            //尾段切废
            Txt_Scraped_Length_Exit.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Scraped_Length_Exit)].ToString();
            //头部导带钢种
            Txt_Head_Leader_St_No.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Leader_St_No)].ToString();
            //尾部导带钢种
            Txt_Tail_Leader_St_No.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Leader_St_No)].ToString();
            //卷曲方向
            Cob_Winding_Direction.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Winding_Direction)].ToString() ?? string.Empty;
            //好面朝向
            Cob_Base_Surface.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Base_Surface)].ToString();
            //封锁责任者
            Txt_Inspector.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Inspector)].ToString();
            //封鎖標記
            Cob_Hold_Flag.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Hold_Flag)].ToString();
            //封锁原因代码
            Txt_Hold_Cause_Code.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Hold_Cause_Code)].ToString();
            //取樣標記
            Cob_Sample_Flag.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Sample_Flag)].ToString();
            //切邊標記
            Cob_Trim_Flag.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Trim_Flag)].ToString();
            //分卷標記
            Cob_Fixed_WT_Flag.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Fixed_WT_Flag)].ToString();
            //最終卷標記
            Cob_End_Flag.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.End_Flag)].ToString();
            //廢品標記
            Cob_Scrap_Flag.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Scrap_Flag)].ToString();
            //取樣位置
            Cob_Sample_Frqn_Code.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Sample_Frqn_Code)].ToString();
            //导带使用
            Txt_No_Leader_Code.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.No_Leader_Code)].ToString();
            //表面精度代碼
            Cob_Surface_Accuracy_Code.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Surface_Accuracy_Code)].ToString();
            //头段未研磨
            Txt_Head_Off_Gauge.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Head_Off_Gauge)].ToString();
            //尾段未研磨
            Txt_Tail_Off_Gauge.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Off_Gauge)].ToString();
            //內表面精度代碼
            Cob_Surface_Accu_Code_In.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_In)].ToString();
            //外表面精度代碼
            Cob_Surface_Accu_Code_Out.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Surface_Accu_Code_Out)].ToString();
            //工序代码
            Cob_ProcessCode.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Process_Code)].ToString();
            //翻面標記
            Cob_Flip_Tag.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Flip_Tag)].ToString();
            //收卷方向
            Cob_Decoiler_Direction.SelectedValue = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Decoiler_Direction)].ToString();
            //卷取实际张力平均值
            Txt_Recoiler_Actten_Avg.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Recoiler_Actten_Avg)].ToString();

            //20211004 暫時先開放修改，等業主決定是否鎖起來
            //圆盘剪Gap平均值
            Txt_Avg_Side_Trimmer_Gap.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Gap)].ToString();
            //圆盘剪Lap平均值
            Txt_Avg_Side_Trimmer_Lap.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Lap)].ToString();
            //圆盘剪宽度平均值
            Txt_Avg_Side_Trimmer_Width.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Avg_Side_Trimmer_Width)].ToString();
            //裁边量平均值_操作侧
            Txt_Avg_Trimming_OperateSide.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Avg_Trimming_OperateSide)].ToString();
            //裁边量平均值_Drive侧
            Txt_Avg_Trimming_DriveSide.Text = dtGetCoilPDO.Rows[0][nameof(PDOEntity.TBL_PDO.Avg_Trimming_DriveSide)].ToString();
        }

        private DateTime Fun_ConverDateTime(string strColValue, string strType)
        {
            DateTime dtTime;
            if (!string.IsNullOrEmpty(strColValue.Trim()))
            {
                //dtTime = DateTime.ParseExact(strCol, strType, null);
                if (DateTime.TryParseExact(strColValue, strType, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dtTime))
                {
                    //TryParseExact轉換成功
                }
                else
                {
                    dtTime = DateTimePicker.MaximumDateTime;//DateTime.MaxValue.AddYears(-9);
                }
            }
            else
            {
                dtTime = DateTimePicker.MaximumDateTime;// DateTime.MinValue.AddYears(+1755);
            }
            return dtTime;
        }
        private void Fun_ClearDisplay(TabPage Page)
        {

            foreach (Control control in Page.Controls.OfType<Control>())
            {
                if (control is TextBox)
                {
                    control.Text = string.Empty;
                }

                if (Page.Name.Equals(nameof(Tab_PDODefectPage)))
                {
                    if (control is ComboBox)
                    {
                        control.Text = string.Empty;
                    }
                }

                if (control is CtrTextBox)
                {
                    control.Text = string.Empty;
                }

                if (control is CtrNumTextBox)
                {
                    control.Text = string.Empty;
                }
            }
        }

        private void Fun_ClearGroupBoxText(GroupBox group)
        {
            foreach (Control control in group.Controls)
            {
                if (control is TextBox)
                {
                    control.Text = string.Empty;
                }
                if (control is CtrTextBox)
                {
                    control.Text = string.Empty;
                }

                if (control is CtrNumTextBox)
                {
                    control.Text = string.Empty;
                }
            }
        }

        private void Fun_ClearPanelText(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is TextBox)
                {
                    control.Text = string.Empty;
                }
                if (control is CtrTextBox)
                {
                    control.Text = string.Empty;
                }

                if (control is CtrNumTextBox)
                {
                    control.Text = string.Empty;
                }
            }
        }
        #endregion

        private void Tab_PDOControll_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawItemHandler.Instance.DrawItem(Tab_PDOControl, e);
        }

        /// <summary>
        /// 返回PDO列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_TrunBack_Click(object sender, EventArgs e)
        {
            frm_0_0_Main FatherForm = Parent.Parent as frm_0_0_Main;

            FatherForm.tsMenuItem_3_1.PerformClick();

            //Form FrmChild = null;
            //FrmChild = new frm_3_1_PDOCoilList();
            //PublicForms.PDOCoilList = (frm_3_1_PDOCoilList)FrmChild;

            //foreach (Form form in FatherForm.pnl_Main.Controls.Cast<Control>().Where(x => x is Form))
            //{
            //    if (form.Name.Equals(nameof(frm_3_1_PDOCoilList)))
            //    {
            //        form.Focus();
            //        form.Visible = true;
            //    }
            //    if (form.Name.Equals(nameof(frm_3_2_PDODetail)))
            //    {
            //        form.Visible = false;
            //    }
            //}
        }

        #region ComboBox說明

        /// <summary>
        /// 出口墊紙方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Paper_Req_Code_Click(object sender, EventArgs e)
        {
            Lbl_Spare_ComboName.Text = Lbl_Paper_Req_Code_Title.Text;// "出口垫纸方式";
            //Pnl_Spare.Visible = true;
            //Pnl_Spare.Location = new Point(1143, 118);
            //Pnl_Spare.Size = new Size(432, 226);
            //Pnl_Spare.BringToFront();
            //ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(Cbo_Type.PAPER_REQ_CODE, Txt_Spare);
            Fun_SpareDisplay(Cbo_Type.PAPER_REQ_CODE,1);
        }

        /// <summary>
        /// 出口墊紙类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Paper_Code_Click(object sender, EventArgs e)
        {
            Lbl_Spare_ComboName.Text = Lbl_Paper_Code_Title.Text;//"出口垫纸类型";
            //Pnl_Spare.Visible = true;
            //Pnl_Spare.Location = new Point(1143, 167);
            //Pnl_Spare.Size = new Size(432, 226);
            //Pnl_Spare.BringToFront();
            //ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(Cbo_Type.Paper_Type, Txt_Spare);
            Fun_SpareDisplay(Cbo_Type.Paper_Type,2);
        }

        /// <summary>
        /// 出口套筒類型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Sleeve_Type_Exit_Click(object sender, EventArgs e)
        {
            Lbl_Spare_ComboName.Text = Lbl_Sleeve_Type_Exit_Code_Title.Text;// "出口套筒类型";
            //Pnl_Spare.Visible = true;
            //Pnl_Spare.Location = new Point(1143, 69);
            //Pnl_Spare.Size = new Size(432, 226);
            //Pnl_Spare.BringToFront();
            //ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(Cbo_Type.Sleeve_Type, Txt_Spare);
            Fun_SpareDisplay(Cbo_Type.Sleeve_Type,3);
        }

        /// <summary>
        /// 表面精度代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Surface_Accuracy_Code_Click(object sender, EventArgs e)
        {
            Lbl_Spare_ComboName.Text = Lbl_Surface_Accuracy_Code_Title.Text;// "表面精度代码";
            //Pnl_Spare.Visible = true;
            //Pnl_Spare.Location = new Point(780, 461);
            //Pnl_Spare.Size = new Size(432, 226);
            //Pnl_Spare.BringToFront();
            //ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(Cbo_Type.Surface_Accuracy, Txt_Spare);
            Fun_SpareDisplay(Cbo_Type.Surface_Accuracy,4);
        }

        private void Btn_ProcessCode_Dec_Click(object sender, EventArgs e)
        {
            Lbl_Spare_ComboName.Text = Lbl_Process_Code_Title.Text;// "工序代码";
        
            Fun_SpareDisplay(Cbo_Type.ProcessCode,5);
        }
        private void Fun_SpareDisplay(Cbo_Type cbo_Type,int intNo)
        {
            Pnl_Spare.Visible = true;
            Pnl_Spare.BringToFront();
            Txt_Spare.ScrollBars = ScrollBars.None;
            // switch (Lbl_Spare_ComboName.Text)
            switch (intNo)
            {
                case 1:// "出口垫纸方式":
                    ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
                    Pnl_Spare.Location = new Point(1165, 105);
                    Pnl_Spare.Size = new Size(432, 226);
                    break;

                case 2:// "出口垫纸类型":
                    ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
                    Pnl_Spare.Location = new Point(1165, 145);
                    Pnl_Spare.Size = new Size(432, 226);
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    break;

                case 3:// "出口套筒类型":
                    ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
                    Pnl_Spare.Location = new Point(1165, 63);
                    Pnl_Spare.Size = new Size(432, 226);
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    break;
                case 4:// "表面精度代码":
                    ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
                    Pnl_Spare.Location = new Point(780, 461);
                    Pnl_Spare.Size = new Size(432, 226);
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    break;
                case 5:// "工序代码":
                    ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItemsSpare(cbo_Type, Txt_Spare);
                    Pnl_Spare.Location = new Point(1165, 461);
                    Pnl_Spare.Size = new Size(432, 226);
                    Txt_Spare.ScrollBars = ScrollBars.Both;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 關閉說明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Spare_Close_Click(object sender, EventArgs e)
        {
            Pnl_Spare.Visible = false;
        }

        #endregion

        private void Btn_Description_Click(object sender, EventArgs e)
        {
            Fun_DefectSpare();
            Pnl_Description.Size = new Size(1010, 571);
            Pnl_Description.Location = new Point(403, 117);
            Pnl_Description.Visible = true;
        }

        private void Btn_Close_Desc_Click(object sender, EventArgs e)
        {
            Pnl_Description.Visible = false;
            Pnl_Description.Location = new Point(1530, 647);
            Pnl_Description.Size = new Size(117, 46);
        }

        private void Btn_PrintTag_Click(object sender, EventArgs e)
        {
            //if (strEditStatus != "Read")
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再打印标签", "打印标签", 0);
            //    return;
            //}
            //if (bolEditStatus == true)
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再打印标签", "打印标签", 0);
            //    return;
            //}

            //if (Txt_Out_Coil_ID.Text.Trim().IsEmpty())
            //{
            //    EventLogHandler.Instance.EventPush_Message($"请查询欲打印标签之钢卷");
            //    return;
            //}

            Frm_PrintLabels frm_Print = new Frm_PrintLabels
            {
                Str_Coil_No = Txt_Out_Coil_ID.Text.Trim()
            };
            frm_Print.ShowDialog();
            frm_Print.Dispose();

            if (frm_Print.DialogResult == DialogResult.OK)
            {
                string strShowText = $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()} 打印钢卷号:[{frm_Print.Str_Coil_No}]标签";
                EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()} 打印标签作业", $"打印钢卷号:[{frm_Print.Str_Coil_No}]标签");
                EventLogHandler.Instance.EventPush_Message(strShowText);
                PublicComm.ClientLog.Info(strShowText);

            }

            ////改由HMI直接列印
            //SCCommMsg.CS07_PrintLabel _PrintLabel = new SCCommMsg.CS07_PrintLabel
            //{
            //    Source = "GPL_HMI",
            //    ID = "PrintLabel",
            //    CoilID = Txt_Out_Coil_ID.Text.Trim()
            //};

            //PublicComm.Client.Tell(_PrintLabel);

            //EventLogHandler.Instance.LogInfo("2-1", $"使用者:{PublicForms.Main.lblLoginUser.Text.Trim()}打印标签作业", $"打印{Txt_Out_Coil_ID.Text.Trim()}标签");
            //EventLogHandler.Instance.EventPush_Message($"列印[{Txt_Out_Coil_ID.Text.Trim()}]标签");
            //PublicComm.ClientLog.Info($"通知Server列印鋼卷號:[{Txt_Out_Coil_ID.Text.Trim()}]標籤");
        }

        //列印试片标签
        private void Btn_PrintTagSample_Click(object sender, EventArgs e)
        {
            string strSql = $" SELECT  Sample_Lot_No,Sample_Frqn_Code  FROM  TBL_PDI  WHERE Entry_Coil_ID = '{Txt_In_Coil_ID.Text}'";
            DataTable dtPdiSample = DataAccess.Fun_SelectDate(strSql, "PDI资料");

            string strSampleNo = "";
            string strSampleCode = "";
            if (!dtPdiSample.IsNull())
            {
                strSampleNo = dtPdiSample.Rows[0]["Sample_Lot_No"].ToString();
                strSampleCode = dtPdiSample.Rows[0]["Sample_Frqn_Code"].ToString(); //Sample_Frqn_Code = Sample_Position ?

                ////轉換頭100 = H ; 中010 = M ;尾001 = T
                //if (dtPdiSample.Rows[0]["Sample_Position"].ToString() == "100")
                //    strSampleCode = "H";
                //else if (dtPdiSample.Rows[0]["Sample_Position"].ToString() == "010")
                //    strSampleCode = "M";
                //else if (dtPdiSample.Rows[0]["Sample_Position"].ToString() == "001")
                //    strSampleCode = "T";
                //else
                //    strSampleCode = "";
            }

            Frm_PrintLabelsSample frm_Print = new Frm_PrintLabelsSample
            {
                //Str_Coil_No = Txt_Entry_Coil_No.Text,//入口钢卷号
                Str_Coil_No = Txt_Out_Coil_ID.Text,//出口钢卷号
                Str_Steel_Grade_Sign = Txt_St_No.Text,//钢种(牌号)
                Str_Coil_Thick = Txt_Out_Coil_Thick.Text,//钢卷厚度
                Str_Sample_Lot_No = strSampleNo,//试批号
                Str_Sample_Position = strSampleCode,//取样位置
                Str_Unit_code = "CPL",//产线 CAPL
            };
            frm_Print.ShowDialog();
            frm_Print.Dispose();

            if (frm_Print.DialogResult == DialogResult.OK)
            {
                string strShowText = $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()} 打印钢卷号:[{frm_Print.Str_Coil_No}]标签";
                EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text.Trim()} 打印标签作业", $"打印钢卷号:[{frm_Print.Str_Coil_No}]标签");
                EventLogHandler.Instance.EventPush_Message(strShowText);
                PublicComm.ClientLog.Info(strShowText);

            }
        }

        private void Cob_Search_Out_Coil_ID_Click(object sender, EventArgs e)
        {
            //Fun_SelectOut_mat_NoList();
        }

        private void Cob_Sleeve_Type_Exit_Click(object sender, EventArgs e)
        {
            //出口套筒種類
            ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Sleeve(Cob_Sleeve_Type_Exit_Code);
        }

        private void Cob_Paper_Code_Click(object sender, EventArgs e)
        {
            //出口墊紙種類
            //ComboBoxIndexHandler.Instance.Fun_SelectComboBoxItems_Paper(Cob_Paper_Code);
        }

        private void Fun_DefectSpare()
        {
            #region 缺陷代码
            Txt_Code_Desc.Text = "第一位：缺陷种类（1st Byte，Defect Sort）" + Environment.NewLine
                         + "   S_表面缺陷（Surface）" + Environment.NewLine
                         + "   E_边缘缺陷 (Edge)" + Environment.NewLine
                         + "   F_板形缺陷 (Shape)" + Environment.NewLine
                         + "   W_盘卷缺陷 (Coil)" + Environment.NewLine
                         + "   D_尺寸/重量缺陷 (Size/Weight)" + Environment.NewLine
                         + "   M_物性缺陷 (Physics)" + Environment.NewLine
                         + "   C_化性/耐蚀性缺陷 (Chemistry/ Corrosion Resistance)" + Environment.NewLine
                         + "   其它缺陷(Others)" + Environment.NewLine
                         + "第二位与第三位：缺陷种类序号 01,02，……" + Environment.NewLine
                         + "(2nd  Byte And 3rd Byte,Sequence No,01,02, …)" + Environment.NewLine;
            #endregion
            #region 缺陷来源
            Txt_Origin_Desc.Text = "Sm1:1#炼钢厂(Steel Plant)" + Environment.NewLine
                           + "Rf1:1#Rf" + Environment.NewLine
                           + "Hm1:1#Hsm" + Environment.NewLine
                           + "Ba1:1#Baf" + Environment.NewLine
                           + "Hp1:1#Hapl" + Environment.NewLine
                           + "Rc1:1#Rcl" + Environment.NewLine
                           + "Pa1:1#Pak" + Environment.NewLine
                           + "Rs1:1#Roll_Shop" + Environment.NewLine
                           + "Wh1:1#Warehouse" + Environment.NewLine;
            #endregion
            #region 表面区分
            Txt_Sid_Desc.Text = "T:上(Top)" + Environment.NewLine
                        + "B:下(Bottom)" + Environment.NewLine
                        + "A:上下都有(Both)" + Environment.NewLine;
            #endregion
            #region 宽向位置
            Txt_Pos_Desc.Text = "W:操作側1/4板宽 " + Environment.NewLine
                        + "  (1/4 Strip Width Of Operate Side)" + Environment.NewLine
                        + "D:驅動側1/4板宽 " + Environment.NewLine
                        + "  (1/4 Strip Width Of Driver Side)" + Environment.NewLine
                        + "C:中央1/2位置(Center)" + Environment.NewLine
                        + "B:兩側皆有 (Both Side)" + Environment.NewLine
                        + "A:宽度方向全面皆有(All Side)" + Environment.NewLine;
            #endregion
            #region 长向位置
            Txt_Pos_L_Desc.Text = "比如(Example):" + Environment.NewLine
                          + "缺陷于距带钢头部800m，则为0800" + Environment.NewLine
                          + "Distance Of Strip Head 800m,so the Item is 0800." + Environment.NewLine;
            #endregion

            #region 缺陷程度
            Txt_Level_Desc.Text = "L：轻微(Light)" + Environment.NewLine
                          + "M：中等(Middle)" + Environment.NewLine
                          + "H：严重(Heavy)" + Environment.NewLine
                          + "S：极严重(Serious)" + Environment.NewLine;
            #endregion

            #region 缺陷比例
            Txt_Percent_Desc.Text = "比如(Example):" + Environment.NewLine
                            + "13.14% Record To 131;" + Environment.NewLine
                            + "3.16% Record To 032;" + Environment.NewLine
                            + "100% Record To 000" + Environment.NewLine;
            #endregion
        }


        private void Btn_MMS_Click(object sender, EventArgs e)
        {
            if (strEditStatus != "Read") 
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再上传", "上传PDO", 0);
                return; 
            }
            if (bolEditStatus == true)
            {
                DialogHandler.Instance.Fun_DialogShowOk("请结束编辑状态后再上传", "上传PDO", 0); 
                return; 
            }

            if (!Fun_IsColumnsEmpty(1)) { return; }
            ////檢查是否已上傳過
            //string strUpFlag = Fun_CheckUploadFlag(Txt_Out_Coil_ID.Text);
            //if (strUpFlag.Equals("1"))
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk($"钢卷号:{Txt_Out_Coil_ID.Text.Trim()}已上传过MMS", "上传PDO", 0);
            //    return;
            //}

            string strOut_Coil_ID = Txt_Out_Coil_ID.Text.Trim();//1                
            string strFinishTime = str_Now_FinishTime ;//Dtp_FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss");//2
            string strFinishTime_fff = "";
              //strFinishTime += $".{Dtp_FinishTime.Value.Millisecond}";
              string strIn_Coil_ID = Txt_In_Coil_ID.Text.Trim();//3
            string strPlan_No = Txt_Plan_No.Text.Trim();//4
            string strFixed_WT_Flag = Cob_Fixed_WT_Flag.SelectedIndex.ToString();

            string strMessage = "";


            string strSql = Frm_3_2_SqlFactory.SQL_Select_UploadedChecked(strOut_Coil_ID, strFinishTime, strIn_Coil_ID, strPlan_No);
            //取得上傳前資料
            DataTable dtUploadFlag = DataAccess.Fun_SelectDate(strSql, "PDO上传记录");
            if (dtUploadFlag != null && dtUploadFlag.Rows.Count > 0)
                strFinishTime_fff = dtUploadFlag.Rows[0]["FinishTime_Str"].ToString();

            DataTable dtLength = dtUploadFlag.Copy();

            // MWW 2023/3/8 PDO上傳前檢查是否已上傳最終卷
            string strSql_IsUploadEndFlag = Frm_3_2_SqlFactory.SQL_Select_UploadEndFlag(strIn_Coil_ID, strPlan_No);
            DataTable dtGetCoilPDO = DataAccess.Fun_SelectDate(strSql_IsUploadEndFlag, "PDO");
            //已上抛過最終卷
            if (dtGetCoilPDO != null && dtGetCoilPDO.Rows.Count > 0)
            {
                strMessage = "此入料母卷" + strIn_Coil_ID + "已有最终卷上传记录，请确认是否需要于产销系统界面修改最终卷标记！" + "\r\n"
                    + "PS：确认于产销系统修改最终卷标记后才可以点击【继续上传】按钮，二级亦需同步修改该栏位资料！";
                DialogResult dialog_Result = DialogHandler.Instance.Fun_DialogEndFlagCheck(strMessage, "上传PDO", Properties.Resources.dialogQuestion, 1);
                Fun_Dialog(dialog_Result);
            }
            //未上抛最終卷
            else
            {
                //需上抛的鋼捲，是最終卷
                if (Cob_End_Flag.SelectedIndex == 1)
                {
                    //分卷
                    if (Cob_Fixed_WT_Flag.SelectedIndex == 1)
                    {
                        string strNo = "";
                        strSql_IsUploadEndFlag = Frm_3_2_SqlFactory.SQL_Select_SubvolumeCoil(strIn_Coil_ID, strPlan_No);
                        dtGetCoilPDO = DataAccess.Fun_SelectDate(strSql_IsUploadEndFlag, "PDO");
                        //有上抛過子卷
                        if (dtGetCoilPDO != null && dtGetCoilPDO.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtGetCoilPDO.Rows.Count; i++)
                            {
                                strNo += dtGetCoilPDO.Rows[i]["Out_Coil_ID"].ToString() + "、";
                            }
                            strNo = strNo.Substring(0, strNo.Length - 1);
                            strMessage = "该卷最终卷标记为1，之前上抛的子卷清单有" + strNo + "，请确认是否全部子卷已上抛！";
                            DialogResult dialog_Result = DialogHandler.Instance.Fun_DialogEndFlagCheck(strMessage, "上传PDO", Properties.Resources.dialogQuestion, 1);
                            Fun_Dialog(dialog_Result);
                        }
                        //未上抛過子卷
                        else
                        {
                            strMessage = "该卷最终卷标记为1，请确认是否全部子卷已上抛！";
                            DialogResult dialog_Result = DialogHandler.Instance.Fun_DialogEndFlagCheck(strMessage, "上传PDO", Properties.Resources.dialogQuestion, 1);
                            Fun_Dialog(dialog_Result);
                        }


                    }
                    else//不分卷
                    {
                        strMessage = "请确定是否要上传PDO资料?";//PDO只能上传一次，
                        DialogResult dialog_Result = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "上传PDO", Properties.Resources.dialogQuestion, 1);
                        Fun_Dialog(dialog_Result);
                    }
                }
                else////需上抛的鋼捲，不是最終卷
                {
                    strMessage = "请确定是否要上传PDO资料?";//PDO只能上传一次，
                    DialogResult dialog_Result = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "上传PDO", Properties.Resources.dialogQuestion, 1);
                    Fun_Dialog(dialog_Result);
                }
                #region 原代碼，無用
                ////沒上傳過
                ////if (dtUploadFlag != null && !dtUploadFlag.Rows[0][nameof(PDOEntity.TBL_PDO.PDO_Uploaded_Flag)].ToString().Equals("1"))
                ////{
                //strMessage = "请确定是否要上传PDO资料?";//PDO只能上传一次，
                //DialogResult dialogR = DialogHandler.Instance.Fun_DialogShowOkCancel(strMessage, "上传PDO", Properties.Resources.dialogQuestion, 1);

                //if (dialogR.Equals(DialogResult.OK))
                //{
                //    //string strScrLen_En = dtUploadFlag.Rows[0][nameof(PDOEntity.TBL_PDO.Scraped_Length_Entry)].ToString();
                //    //string strScrLen_Ex = dtUploadFlag.Rows[0][nameof(PDOEntity.TBL_PDO.Scraped_Length_Exit)].ToString();

                //    //Int32.TryParse(dtUploadFlag.Rows[0][nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Entry)].ToString(), out int intHeadLen_En);
                //    //Int32.TryParse(dtUploadFlag.Rows[0][nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Exit)].ToString(), out int intHeadLen_Ex);
                //    //int intHeadSum = intHeadLen_En + intHeadLen_Ex;

                //    dtLength.Rows[0][nameof(PDOEntity.TBL_PDO.Scraped_Length_Entry)] = Fun_SumCutLength(dtUploadFlag, "H");// intHeadSum;

                //    string strSql_ScrLen_En = Frm_3_2_SqlFactory.SQL_Update_Pdo_Scraped_Length(dtLength, nameof(PDOEntity.TBL_PDO.Scraped_Length_Entry), strFinishTime);
                //    if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql_ScrLen_En, "更新頭部切廢長度资料"))
                //    {
                //        DialogHandler.Instance.Fun_DialogShowOk($"更新頭部切廢長度资料失败", "更新頭部切廢長度资料资料", 3);
                //        return;
                //    }

                //    //Int32.TryParse(dtUploadFlag.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Entry)].ToString(), out int intTailLen_En);
                //    //Int32.TryParse(dtUploadFlag.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Exit)].ToString(), out int intTailLen_Ex);
                //    //int intTailSum = intTailLen_En + intTailLen_Ex;

                //    dtLength.Rows[0][nameof(PDOEntity.TBL_PDO.Scraped_Length_Exit)] = Fun_SumCutLength(dtUploadFlag, "T");//intTailSum;

                //    string strSql_ScrLen_Ex = Frm_3_2_SqlFactory.SQL_Update_Pdo_Scraped_Length(dtLength, nameof(PDOEntity.TBL_PDO.Scraped_Length_Exit), strFinishTime);
                //    if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql_ScrLen_Ex, "更新尾部切廢長度资料"))
                //    {
                //        DialogHandler.Instance.Fun_DialogShowOk($"更新尾部切廢長度资料失败", "更新尾部切廢長度资料资料", 3);
                //        return;
                //    }


                //    Fun_InsertTBL_CutRecord();

                //    SCCommMsg.CS06_SendMMSPDO _SendMMSPDO = new SCCommMsg.CS06_SendMMSPDO
                //    {
                //        Source = "CPL1_HMI",
                //        ID = "SendMMSPDO",
                //        Coil_ID = Txt_Out_Coil_ID.Text,
                //        In_Coil_ID = Txt_In_Coil_ID.Text,
                //        OperatorID = PublicForms.Main.Lbl_LoginUser.Text.Trim(),
                //        Plan_No = Txt_Plan_No.Text,
                //        FinishTime = strFinishTime_fff
                //    };

                //    PublicComm.Client.Tell(_SendMMSPDO);

                //    strMessage = $"钢卷号[{Txt_Out_Coil_ID.Text.Trim()}]已通知Server上传MMS";

                //    EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}PDO资料确认上传 钢卷编号[{Txt_Out_Coil_ID.Text.Trim()}]", $"PDO资料确认上传 钢卷编号[{ Txt_Out_Coil_ID.Text.Trim()}]");

                //    DialogHandler.Instance.Fun_DialogShowOk(strMessage, "上传PDO", 4);

                //    PublicComm.ClientLog.Info(strMessage);
                //    PublicComm.AkkaLog.Info(strMessage);

                //    //Fun_SelectCoilPDO(Txt_Out_Coil_ID.Text.Trim());
                //}
                ////}
                ////else
                ////{
                ////    EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.lblLoginUser.Text}PDO资料确认上传 钢卷编号[{Txt_Out_Coil_ID.Text.Trim()}]", $"PDO已上传过 钢卷编号[{ Txt_Out_Coil_ID.Text.Trim()}]");

                ////    DialogHandler.Instance.Fun_DialogShowOk($"钢卷号:{Txt_Out_Coil_ID.Text.Trim()}已上传过MMS", "上传PDO", 0);

                ////    PublicComm.ClientLog.Info($"鋼卷號[{Txt_Out_Coil_ID.Text.Trim()}]已上傳過MMS");
                ////    PublicComm.AkkaLog.Info($"鋼卷號[{Txt_Out_Coil_ID.Text.Trim()}]已上傳過MMS");
                ////}
                #endregion
            }
            void Fun_Dialog(DialogResult dialog)
            {
                if (dialog.Equals(DialogResult.OK))
                {
                    dtLength.Rows[0][nameof(PDOEntity.TBL_PDO.Scraped_Length_Entry)] = Fun_SumCutLength(dtUploadFlag, "H");// intHeadSum;

                    string strSql_ScrLen_En = Frm_3_2_SqlFactory.SQL_Update_Pdo_Scraped_Length(dtLength, nameof(PDOEntity.TBL_PDO.Scraped_Length_Entry), strFinishTime);
                    if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql_ScrLen_En, "更新頭部切廢長度资料"))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"更新頭部切廢長度资料失败", "更新頭部切廢長度资料资料", 3);
                        return;
                    }

                    dtLength.Rows[0][nameof(PDOEntity.TBL_PDO.Scraped_Length_Exit)] = Fun_SumCutLength(dtUploadFlag, "T");//intTailSum;

                    string strSql_ScrLen_Ex = Frm_3_2_SqlFactory.SQL_Update_Pdo_Scraped_Length(dtLength, nameof(PDOEntity.TBL_PDO.Scraped_Length_Exit), strFinishTime);
                    if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql_ScrLen_Ex, "更新尾部切廢長度资料"))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"更新尾部切廢長度资料失败", "更新尾部切廢長度资料资料", 3);
                        return;
                    }


                    Fun_InsertTBL_CutRecord();

                    SCCommMsg.CS06_SendMMSPDO _SendMMSPDO = new SCCommMsg.CS06_SendMMSPDO
                    {
                        Source = "CPL1_HMI",
                        ID = "SendMMSPDO",
                        Coil_ID = Txt_Out_Coil_ID.Text,
                        In_Coil_ID = Txt_In_Coil_ID.Text,
                        OperatorID = PublicForms.Main.Lbl_LoginUser.Text.Trim(),
                        Plan_No = Txt_Plan_No.Text,
                        FinishTime = strFinishTime_fff
                    };

                    PublicComm.Client.Tell(_SendMMSPDO);

                    strMessage = $"钢卷号[{Txt_Out_Coil_ID.Text.Trim()}]已通知Server上传MMS";

                    EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}PDO资料确认上传 钢卷编号[{Txt_Out_Coil_ID.Text.Trim()}]", $"PDO资料确认上传 钢卷编号[{ Txt_Out_Coil_ID.Text.Trim()}]");

                    DialogHandler.Instance.Fun_DialogShowOk(strMessage, "上传PDO", 4);

                    PublicComm.ClientLog.Info(strMessage);
                    PublicComm.AkkaLog.Info(strMessage);

                    //Fun_SelectCoilPDO(Txt_Out_Coil_ID.Text.Trim());
                }
            }
        }

        private int Fun_SumCutLength(DataTable dtData,string strFlag)
        {          
            int intLen_En ;
            int intLen_Ex ;
            
            if (strFlag == "H")
            {
                Int32.TryParse(dtData.Rows[0][nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Entry)].ToString(), out intLen_En);
                Int32.TryParse(dtData.Rows[0][nameof(PDOEntity.TBL_PDO.Header_Cut_Length_Exit)].ToString(), out intLen_Ex);
            }
            else if (strFlag == "T")
            {
                Int32.TryParse(dtData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Entry)].ToString(), out intLen_En);
                Int32.TryParse(dtData.Rows[0][nameof(PDOEntity.TBL_PDO.Tail_Cut_Length_Exit)].ToString(), out intLen_Ex);
            }
            else
            {
                 intLen_En = 0;
                 intLen_Ex = 0;
            }                      
            int intSum = intLen_En + intLen_Ex;
            return intSum;           
        }

        /// <summary>
        /// 新增TBL_CutRecord資料
        /// </summary>
        private void Fun_InsertTBL_CutRecord()
        {
            string strSql_Tail = strSql_Insert_TBL_CutRecord;

            //頭段切廢長度紀錄寫入
            if (Insert_TBL_CutRecord_Head != null)
            {
                if (!Txt_Scraped_Length_Entry.Text.Trim().Equals(""))
                {

                    //頭段
                    for (int index = 0; index < Insert_TBL_CutRecord_Head.Count; index++)
                    {
                        if (index == Insert_TBL_CutRecord_Head.Count - 1)
                        {
                            strSql_Insert_TBL_CutRecord += Insert_TBL_CutRecord_Head[index];
                        }
                        else
                        {
                            strSql_Insert_TBL_CutRecord += $"{Insert_TBL_CutRecord_Head[index]},";
                        }
                    }

                    if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql_Insert_TBL_CutRecord, "新增头段TBL_CutRecord资料"))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"新增头段TBL_CutRecord资料失败", "新增头段TBL_CutRecord资料", 3);

                        return;
                    }
                }
            }

            //尾段切廢長度紀錄寫入
            if (Insert_TBL_CutRecord_Tail != null)
            {
                if (!Txt_Scraped_Length_Exit.Text.Trim().Equals(""))
                {

                    //尾段
                    for (int index = 0; index < Insert_TBL_CutRecord_Tail.Count; index++)
                    {
                        if (index == Insert_TBL_CutRecord_Tail.Count - 1)
                        {
                            strSql_Tail += Insert_TBL_CutRecord_Tail[index];
                        }
                        else
                        {
                            strSql_Tail += $"{Insert_TBL_CutRecord_Tail[index]},";
                        }
                    }

                    if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql_Tail, "新增尾段TBL_CutRecord资料"))
                    {
                        DialogHandler.Instance.Fun_DialogShowOk($"新增尾段TBL_CutRecord资料失败", "新增尾段TBL_CutRecord资料", 3);

                        return;
                    }
                }
            }

            Fun_RemoveTBL_CutRecord_Temp(Txt_Out_Coil_ID.Text.Trim());
        }


        /// <summary>
        /// 刪除TBL_CutRecord_Temp資料
        /// </summary>
        /// <param name="Coil_ID"></param>
        private void Fun_RemoveTBL_CutRecord_Temp(string Coil_ID)
        {            
            string strCheckSql = Frm_3_2_SqlFactory.SQL_Select_CutRecordTemp(Coil_ID, "");            
            DataTable dtGetCutRecordTemp = DataAccess.Fun_SelectDate(strCheckSql, "查询TBL_CutRecord_Temp");

            if (dtGetCutRecordTemp != null && dtGetCutRecordTemp.Rows.Count > 0)
            {
                string strSql = Frm_3_2_SqlFactory.SQL_Delete_CoilCutRecordTemp(Coil_ID);

                if (!DataAccess.GetInstance().Fun_ExecuteQuery(strSql, $"删除钢卷号[{Coil_ID}]TBL_CutRecord_Temp资料"))
                {
                    DialogHandler.Instance.Fun_DialogShowOk($"删除钢卷号[{Coil_ID}]TBL_CutRecord_Temp资料失败", "删除钢卷号[{Coil_ID}]TBL_CutRecord_Temp资料", 3);

                    return;
                }
            }
            //EventLogHandler.Instance.EventPush_Message($"查无钢卷号[{Coil_ID}]的切废资讯");
            ////return;
           
        }


        /// <summary>
        /// 計算頭段切廢長度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Head_Calc_Click(object sender, EventArgs e)
        {
            Fun_OpenDialogCutLength("头段", true);
        }


        /// <summary>
        /// 計算尾段切廢長度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Tail_Calc_Click(object sender, EventArgs e)
        {
            Fun_OpenDialogCutLength("尾段", false);
        }

        private void Fun_OpenDialogCutLength(string str, bool IsHead)
        {
            Frm_DialogCutLength _DialogCutLength = new Frm_DialogCutLength
            {
                Coil_ID = Txt_Out_Coil_ID.Text.Trim(),
                IsHead = IsHead
            };
            _DialogCutLength.ShowDialog();
            _DialogCutLength.Dispose();

            EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text} 开启计算{str}切废长度", $"开启计算{str}切废长度");
        }
       
        /// <summary>
        /// 刪除-已隱藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            EventLogHandler.Instance.LogInfo("3-2", $"使用者:{PublicForms.Main.Lbl_LoginUser.Text}删除出口钢卷编号:{Txt_Out_Coil_ID.Text}PDO", $" 删除出口钢卷编号:{Txt_Out_Coil_ID.Text} PDO");
            //上传MMS
            Fun_SetBottonEnabled(Btn_MMS, false);
        }

        private void Fun_OnlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //&& (e.KeyChar != (char)Keys.Space) 
            //          數字                                            //backspace                    //Enter             
            if (!( (e.KeyChar >= '0' && e.KeyChar <= '9')  ||  (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Enter) ))
            {                                        
                e.Handled = true;
            }
        }

        private void Cob_Sample_Flag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (strEditStatus == "Read") { return; }
            if (bolEditStatus == false) { return; }
            if (Cob_Sample_Flag.SelectedIndex == 0)
            {
                Cob_Sample_Frqn_Code.SelectedIndex = -1;
            }
        }

        private void Btn_GetNewWt_Click(object sender, EventArgs e)
        {
            //不是编辑状态 return
            if (!bolEditStatus) { return; }

            //净重//Txt_Out_Coil_Wt 
            //毛重//Txt_Out_Coil_Gross_WT
            //出口套筒类型//Cob_Sleeve_Type_Exit_Code.Text;
            //出口垫纸方式//Cob_Paper_Req_Code.Text;
            //出口垫纸类型//Cob_Paper_Code.Text;


            /*
            3-2 PDO畫面在輸入導帶資料後，需要重新計算淨重，

            要新增計算淨重按鈕，按鈕放在淨重旁
             淨重 = pdo毛重(Out_Coil_Groo_WT)-(墊紙重量 + 套桶重量 + 頭段導帶重量 + 尾段導帶重量)
                                               23.2208  + 直接用(不用算)
          */


            //pdo毛重(Out_Coil_Groo_WT)     毛重:Txt_Out_Coil_Gross_WT
            float flo_Out_Coil_Groo_WT;
            bool bol_Groo_WT = float.TryParse(Txt_Out_Coil_Gross_WT.Text, out flo_Out_Coil_Groo_WT);

            // =================================
            /*
                襯紙重 計算Ex:
                襯紙方式:[1]整卷墊 , 
                襯紙類型:[02]基重(32 g/m2)
                出口長(575m) 
                襯紙寬(1262mm) 

                襯紙重 = 出口長(m) *寬(m)   *基重(g/m2)
                            575    * 1.262 *32 = 23200.8
                23200.8 / 1000 = 23.2208 (kg)
            */
            //襯紙重 (kg)
            float flo_PaperWt = 0;
            //有使用墊紙 才依墊紙方式 & 墊紙類型  取得 墊紙重量
            if (Cob_Paper_Req_Code.SelectedIndex > 0)
            {
                //出口卷长度:Txt_Out_Coil_Length
                float flo_Out_Coil_Length;
                float.TryParse(Txt_Out_Coil_Length.Text, out flo_Out_Coil_Length);

                //出口卷宽度:Txt_Out_Coil_Width(mm)
                float flo_Out_Coil_Width;
                float.TryParse(Txt_Out_Coil_Width.Text, out flo_Out_Coil_Width);

                //出口垫纸方式:Cob_Paper_Req_Code
                string strPaper_Req_Code = Cob_Paper_Req_Code.Text;

                // 依垫纸方式 取  墊紙長度(m)
                float flo_PaperLength = Fun_GetPaperLength(strPaper_Req_Code, flo_Out_Coil_Length);

                //出口垫纸类型:Cob_Paper_Code
                string strPaper_Code = Cob_Paper_Code.Text;
                //依墊紙類型 取得 基重
                float flo_Base_WT = Fun_GetPaperBaseWt(strPaper_Code);

                //襯紙重 (kg)
                flo_PaperWt = ((flo_PaperLength) * (flo_Out_Coil_Width / 1000) * flo_Base_WT) / 1000;
            }


            //================================================ 
            //套筒重量 
            float flo_Sleeve_WT = 0;
            //出口套筒使用否: Cob_Out_Coil_Use_Sleeve_Flag
            //有使用 才依套筒類型 取得 套筒重量
            //if (Cob_Out_Coil_Use_Sleeve_Flag.SelectedIndex == 0)
            //{
                //出口套筒类型 : Cob_Sleeve_Type_Exit_Code
                string str_Sleeve_Type_Exit = Cob_Sleeve_Type_Exit_Code.Text;

                //依套筒類型 取得 套筒重量           
                flo_Sleeve_WT = Fun_GetSleeveWt(str_Sleeve_Type_Exit);
            //}

            //   =================================
            /*
                           導帶重量 計算Ex:
                           導帶長度(m)
                           導帶寬度(mm) /1000 => (m)
                           導帶厚度(mm) /1000 => (m)

                           抓PDI 密度(Density)(kg/m³)

                           導帶重量 = 導帶長度(m)*導帶寬度(m)*導帶厚度(m)*密度(kg/m³)
             */
            //導帶長度(m)
            float flo_Head_Leader_Length, flo_Tail_Leader_Length;
            //導帶寬度(mm)
            float flo_Head_Leader_Width, flo_Tail_Leader_Width;
            //導帶厚度(mm)
            float flo_Head_Leader_Thickness, flo_Tail_Leader_Thickness;
            //密度(Density)(g/m³)
            float flo_Head_Leader_Density, flo_Tail_Leader_Density;

            float.TryParse(Txt_Head_Leader_Length.Text, out flo_Head_Leader_Length);
            float.TryParse(Txt_Head_Leader_Width.Text, out flo_Head_Leader_Width);
            float.TryParse(Txt_Head_Leader_Thickness.Text, out flo_Head_Leader_Thickness);
            flo_Head_Leader_Density = Fun_GetLeader_Density(Txt_Head_Leader_St_No.Text, true);

            float.TryParse(Txt_Tail_Leader_Length.Text, out flo_Tail_Leader_Length);
            float.TryParse(Txt_Tail_Leader_Width.Text, out flo_Tail_Leader_Width);
            float.TryParse(Txt_Tail_Leader_Thickness.Text, out flo_Tail_Leader_Thickness);
            flo_Tail_Leader_Density = Fun_GetLeader_Density(Txt_Tail_Leader_St_No.Text, false);

            float flo_Head_Leader_Wt = flo_Head_Leader_Length * (flo_Head_Leader_Width / 1000) * (flo_Head_Leader_Thickness / 1000) * (flo_Head_Leader_Density);
            float flo_Tail_Leader_Wt = flo_Tail_Leader_Length * (flo_Tail_Leader_Width / 1000) * (flo_Tail_Leader_Thickness / 1000) * (flo_Tail_Leader_Density);

            //淨重 = pdo毛重(Out_Coil_Groo_WT) - (墊紙重量 + 套桶重量 + 頭段導帶重量 + 尾段導帶重量)
            float flo_Act_WT = flo_Out_Coil_Groo_WT - (flo_PaperWt + flo_Sleeve_WT + flo_Head_Leader_Wt + flo_Tail_Leader_Wt);

            double douWT = 0;
            double.TryParse(flo_Act_WT.ToString(), out douWT);

            //淨重:Txt_Out_Coil_Wt    四捨五入:Round    無條件捨去:Floor
            Txt_Out_Coil_Wt.Text = Math.Round(douWT).ToString();
        }

        /// <summary>
        /// 依墊紙方式取得墊紙長度
        /// </summary>
        /// <param name="strPaper_Req_Code">墊紙方式代碼</param>
        /// <param name="flo_Out_Coil_Length">出口卷长度</param>
        /// <returns></returns>
        private float Fun_GetPaperLength(string strPaper_Req_Code, float flo_Out_Coil_Length)
        {
            float flo_PaperLength = 0;
            switch (strPaper_Req_Code)
            {
                //不覆膜/不墊紙
                case "0":
                    flo_PaperLength = 0;
                    break;
                //整卷墊
                case "1":
                    flo_PaperLength = flo_Out_Coil_Length;
                    break;
                //頭尾端各50米
                case "2":
                    flo_PaperLength = 100;
                    break;
                //頭尾端各30米
                case "3":
                    flo_PaperLength = 60;
                    break;
                //尾端80米
                case "4":
                    flo_PaperLength = 80;
                    break;
                //尾端200米
                case "5":
                    flo_PaperLength = 200;
                    break;
                default:
                    flo_PaperLength = 0;
                    break;
            }
            return flo_PaperLength;
        }

        /// <summary>
        /// 依墊紙類型取得墊紙基重
        /// </summary>
        /// <param name="strPaper_Code">墊紙類型代碼</param>       
        /// <returns></returns>
        private float Fun_GetPaperBaseWt(string strPaper_Code)
        {
            float flo_PaperBaseWt = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"SELECT * ");
            sb.AppendLine($"FROM  {nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper)}");
            sb.AppendLine($"WHERE {nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Code)} = '{strPaper_Code}'");
            sb.AppendLine($"");

            string strSql = sb.ToString();
            DataTable dtData = DataAccess.Fun_SelectDate(strSql, "LkUpTablePaperData");
            //SqlFactory.Frm_3_2_SelectData_DB_PDO(Coil_ID, strPlan_No, strIn_Coil_ID);          

            if (!dtData.IsNull())
            {
                string strPaper_Base_Weight = dtData.Rows[0][
                    nameof(LkUpTablePaperEntity.TBL_LookupTable_Paper.Paper_Base_Weight)].ToString();
                float.TryParse(strPaper_Base_Weight, out flo_PaperBaseWt);
            }
            return flo_PaperBaseWt;
        }

        /// <summary>
        /// 依套筒代碼取得套筒重量
        /// </summary>
        /// <param name="strSleeve_Code">套筒代碼</param>
        /// <returns></returns>
        private float Fun_GetSleeveWt(string strSleeve_Code)
        {
            float flo_SleeveWt = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"SELECT * ");
            sb.AppendLine($"FROM  {nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve)}");
            sb.AppendLine($"WHERE {nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Code)} = '{strSleeve_Code}'");
            sb.AppendLine($"");

            string strSql = sb.ToString();
            DataTable dtData = DataAccess.Fun_SelectDate(strSql, "LkUpTableSleeveData");
            //SqlFactory.Frm_3_2_SelectData_DB_PDO(Coil_ID, strPlan_No, strIn_Coil_ID);          

            if (!dtData.IsNull())
            {
                string strSleeve_Weight = dtData.Rows[0][
                    nameof(LkUpTableSleeveEntity.TBL_LookupTable_Sleeve.Sleeve_Weight)].ToString();
                float.TryParse(strSleeve_Weight, out flo_SleeveWt);
            }
            return flo_SleeveWt;
        }

        /// <summary>
        /// 依導帶鋼種取得導帶密度
        /// </summary>
        /// <param name="strLeader_St_No">導帶鋼種</param>
        /// <param name="bolIsHead">是否為頭段導帶</param>
        /// <returns></returns>
        private float Fun_GetLeader_Density(string strLeader_St_No, bool bolIsHead)
        {
            float flo_Leader_Density;
            string strLeaderType = bolIsHead ? Grb_HeadLeader.Text : Grb_TailLeader.Text;
            switch (strLeader_St_No)
            {
                case "301":
                case "304":
                    flo_Leader_Density = bolIsHead ? 7930 : 7930; break;
                case "443":
                    flo_Leader_Density = bolIsHead ? 7740 : 7740; break;
                case "430":
                    flo_Leader_Density = bolIsHead ? 7700 : 7700; break;

                default:
                    PublicComm.ClientLog.Debug($"{strLeaderType} 鋼種:{strLeader_St_No}，查無導帶密度");
                    flo_Leader_Density = bolIsHead ? 0 : 0;
                    break;
            }
            return flo_Leader_Density;
        }

        //出口墊紙方式
        private void Cob_Paper_Req_Code_SelectedValueChanged(object sender, EventArgs e)
        {
            if (strEditStatus == "Read")
                return;

            if (bolEditStatus == false)
                return;
            if (Cob_Paper_Req_Code.SelectedIndex != -1)
            {
                if (Cob_Paper_Req_Code.SelectedValue.ToString() == "0")
                {
                    Txt_Out_Head_Paper_Length.Text = "0";
                    Txt_Out_Tail_Paper_Length.Text = "0";
                }
                else if (Cob_Paper_Req_Code.SelectedValue.ToString() == "1")
                {
                    //Txt_Out_Head_Paper_Length.Text = "0";
                    //Txt_Out_Tail_Paper_Length.Text = "0";
                    double dou_Coil_Len = 0, dou_Coil_Len_H, dou_Coil_Len_T;

                    if (!string.IsNullOrEmpty(Txt_Out_Coil_Length.Text))
                    {
                        double.TryParse(Txt_Out_Coil_Length.Text, out dou_Coil_Len);
                    }
                    dou_Coil_Len_T = Math.Floor(dou_Coil_Len / 2);
                    dou_Coil_Len_H = dou_Coil_Len - dou_Coil_Len_T;

                    Txt_Out_Head_Paper_Length.Text = dou_Coil_Len_H.ToString();
                    Txt_Out_Tail_Paper_Length.Text = dou_Coil_Len_T.ToString();
                }
                else if (Cob_Paper_Req_Code.SelectedValue.ToString() == "2")
                {
                    Txt_Out_Head_Paper_Length.Text = "50";
                    Txt_Out_Tail_Paper_Length.Text = "50";
                }
                else if (Cob_Paper_Req_Code.SelectedValue.ToString() == "3")
                {
                    Txt_Out_Head_Paper_Length.Text = "30";
                    Txt_Out_Tail_Paper_Length.Text = "30";
                }
                else if (Cob_Paper_Req_Code.SelectedValue.ToString() == "4")
                {
                    Txt_Out_Head_Paper_Length.Text = "0";
                    Txt_Out_Tail_Paper_Length.Text = "80";
                }
                else
                {
                    Txt_Out_Head_Paper_Length.Text = "0";
                    Txt_Out_Tail_Paper_Length.Text = "0";
                }
            }
            else
            {

            }
        }

        //出口墊紙類型
        private void Cob_Paper_Code_SelectedValueChanged(object sender, EventArgs e)
        {
            if (strEditStatus == "Read")
                return;

            if (bolEditStatus == false)
                return;

            if (Cob_Paper_Code.SelectedIndex != -1)
            {
                if (Cob_Paper_Code.SelectedValue.ToString() == "01")
                {
                    Txt_Out_Head_Paper_Width.Text = "1020";
                    Txt_Out_Tail_Paper_Width.Text = "1020";
                }
                else if (Cob_Paper_Code.SelectedValue.ToString() == "02")
                {
                    Txt_Out_Head_Paper_Width.Text = "1220";
                    Txt_Out_Tail_Paper_Width.Text = "1220";
                }
                else if (Cob_Paper_Code.SelectedValue.ToString() == "03")
                {
                    Txt_Out_Head_Paper_Width.Text = "1020";
                    Txt_Out_Tail_Paper_Width.Text = "1020";
                }
                else if (Cob_Paper_Code.SelectedValue.ToString() == "04")
                {
                    Txt_Out_Head_Paper_Width.Text = "1220";
                    Txt_Out_Tail_Paper_Width.Text = "1220";
                }
                else if (Cob_Paper_Code.SelectedValue.ToString() == "05")
                {
                    Txt_Out_Head_Paper_Width.Text = "1020";
                    Txt_Out_Tail_Paper_Width.Text = "1020";
                }
                else if (Cob_Paper_Code.SelectedValue.ToString() == "06")
                {
                    Txt_Out_Head_Paper_Width.Text = "1220";
                    Txt_Out_Tail_Paper_Width.Text = "1220";
                }
                else
                {
                    Txt_Out_Head_Paper_Width.Text = "0";
                    Txt_Out_Tail_Paper_Width.Text = "0";
                }
            }
            else
            {

            }
        }

        #region 配合語系切換調整字體大小
        private void Fun_LanguageIsEn_Font14_10(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font14_12(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)14, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
        }
        private void Fun_LanguageIsEn_Font12_10(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font12_9(object sender, EventArgs e)
        {
            FontStyle fs = ((Label)sender).Font.Style;
            FontFamily ffm = ((Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((Label)sender).Font = new Font(ffm, (float)12, fs);
            else
                ((Label)sender).Font = new Font(ffm, (float)9, fs);
        }
        #endregion

        
    }
}
