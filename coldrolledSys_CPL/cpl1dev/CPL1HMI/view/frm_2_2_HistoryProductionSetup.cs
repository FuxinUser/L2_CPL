using DBService.Repository.LookupTbl;
using DBService.Repository.LookupTblSideTrimmer;
using DBService.Repository.Leader;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using System;
using System.Data;
using System.Windows.Forms;
using DBService.Repository.PresetRecord;

namespace CPL1HMI
{
    public partial class Frm_2_2_HistoryProductionSetup : Form
    {   
        //語系
        private LanguageHandler LanguageHand;
        public Frm_2_2_HistoryProductionSetup()
        {
            InitializeComponent();
            
        }

        private void Frm_2_2_HistoryProductionSetup_Load(object sender, EventArgs e)
        {
           // LanguageHandler.Instance.Fun_CurrentLanguage(this.Name);

            if (PublicForms.HistoryProductionSetup == null) PublicForms.HistoryProductionSetup = this;

            Grb_Trimmer.Visible = GlobalVariableHandler.proLine.Equals("CPL1");

            Fun_SelectComboBoxCoilList();

            Fun_SqlCondition();
            //Fun_SelectCoilList();

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }
       
        private void Fun_SelectComboBoxCoilList()
        {
            string strSql = Frm_2_2_SqlFactory.Frm_2_2_ComboBoxCoilList_DB_PDI();
            DataTable dtCoilList = DataAccess.Fun_SelectDate(strSql, "生产设定");

            Cob_Coil_ID.DisplayMember = "Coil_ID";
            Cob_Coil_ID.ValueMember = "Coil_ID";

            Cob_Coil_ID.DataSource = dtCoilList.IsNull() ? null : dtCoilList;

        }

        /// <summary>
        /// 查詢按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Fun_SqlCondition();
        }

        /// <summary>
        /// FormLoad資料搜尋語法
        /// </summary>
        private void Fun_SelectCoilList()
        {
            string strSql = Frm_2_2_SqlFactory.SQL_Select_ProductionSetup();
            DataTable dt = DataAccess.Fun_SelectDate(strSql, "生产设定钢卷清单");
           
            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_CoilList, dt);
            Frm_2_2_ColumnsHandler.Instance.Frm_2_2_HistoryProduction_Setup(Dgv_CoilList);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_CoilList);
        }

        /// <summary>
        /// 搜尋條件
        /// </summary>
        private void Fun_SqlCondition()
        {
            string strSql = Frm_2_2_SqlFactory.SQL_Select_ProductionSetup();

            #region 條件如有 Checked

            //鋼捲號碼 Checked
            if (Rdb_Coil_ID.Checked)
            {
                strSql += $" AND [Coil_ID] ='{Cob_Coil_ID.Text}' ";
            }
            else if (Rdb_PresetTime.Checked)
            {

                if (Dtp_PresetTime_Start.DateTimeRangeIsFail(Dtp_PresetTime_Finish.Value))
                {
                    DialogHandler.Instance.Fun_DialogShowOk("生产时间区间起讫时间不正确，请重新确认", "查询", 0);
                    Dtp_PresetTime_Start.Focus();
                    return;
                }

                strSql += $" AND [PresetTime] Between '{Dtp_PresetTime_Start.Value:yyyy-MM-dd HH}:00:00' and '{Dtp_PresetTime_Finish.Value:yyyy-MM-dd HH}:59:59' ";              
            }


            if (Chk_Width.Checked)
            {
                if (!string.IsNullOrEmpty(Txt_Width_Min.Text))
                {
                    if (!string.IsNullOrEmpty(Txt_Width_Max.Text))
                    {
                        strSql += $" AND [Width] >= { Txt_Width_Min.Text } AND [Width] <= { Txt_Width_Max.Text } ";
                    }
                    else
                    {
                        strSql += $" AND [Width] >= { Txt_Width_Min.Text } AND [Width] <= { Txt_Width_Min.Text } " ;
                    }

                }
                else
                {
                    //警告 必须填入最小值
                    DialogHandler.Instance.Fun_DialogShowOk("请填入宽度最小值！", "查询",  0);
                }
            }

            if (Chk_Thickness.Checked)
            {
                if (!string.IsNullOrEmpty(Txt_Thickness_Min.Text))
                {
                    if (!string.IsNullOrEmpty(Txt_Thickness_Max.Text))
                    {
                        strSql += $" AND [Thickness] >= { Txt_Thickness_Min.Text } AND [Thickness] <=  { Txt_Thickness_Max.Text } ";
                    }
                    else
                    {
                        strSql += $" AND [Thickness] >= { Txt_Thickness_Min.Text } AND [Thickness] <= { Txt_Thickness_Min.Text } ";
                    }

                }
                else
                {
                    //警告 必须填入最小值
                    DialogHandler.Instance.Fun_DialogShowOk("请填入厚度最小值！", "查询", 0);
                }
            }

            if (Chk_Steelgcode.Checked)
            {                
                strSql += $" AND SteelGrade = N'{ Txt_Steelgcode.Text }' ";               
            }

            #region Old
            //if (Chk_Steelgcode.Checked && Chk_Width.Checked)
            //{
            //    strSql += $@" and a.[{nameof(CoilPDIModel.TBL_PDI.St_No)}] ='{Txt_Steelgcode.Text}'
            //                      and a.[{nameof(CoilPDIModel.TBL_PDI.Entry_Coil_Width)}] ='{Txt_Width_Min.Text}'";
            //}
            //else if (Chk_Steelgcode.Checked && Chk_Width.Checked.Equals(false))
            //{
            //    strSql += $" and a.[{nameof(CoilPDIModel.TBL_PDI.St_No)}] ='{Txt_Steelgcode.Text}'";
            //}
            //else if (Chk_Steelgcode.Checked.Equals(false) && Chk_Width.Checked)
            //{
            //    strSql += $" and a.[{nameof(CoilPDIModel.TBL_PDI.Entry_Coil_Width)}] ='{Txt_Width_Min.Text}'";
            //}
            #endregion

            #endregion

            DataTable dt = DataAccess.Fun_SelectDate(strSql, "生产设定钢卷清单条件");

            //if (dt.IsNull())
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("查询生产设定钢卷清单无资料", "查询", 0);
            //}

            DGVColumnsHandler.Instance.Fun_DataGridViewDataDisplay(Dgv_CoilList, dt);
            Frm_2_2_ColumnsHandler.Instance.Frm_2_2_HistoryProduction_Setup(Dgv_CoilList);
            DGVColumnsHandler.Instance.ColumnHeadVisableControl(Dgv_CoilList);

        }
      
        /// <summary>
        /// 選鋼卷編號為搜尋條件，鋼種、寬度就取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rdb_CoilNo_CheckedChanged(object sender, EventArgs e)
        {
            Chk_Steelgcode.Checked = false;

            Chk_Width.Checked = false;
        }
             
        private void Fun_SelectCoilProcessData(string Coil_ID)
        {
            string strSql = Frm_2_2_SqlFactory.SQL_Select_ProcessData(Coil_ID);
            
            DataTable dt_Process = DataAccess.Fun_SelectDate(strSql, "钢卷生产设定");

            if (dt_Process.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("查询钢卷生产设定无资料", "查询",0);

                return;
            } 

            Fun_FillData(dt_Process);
        }

        private void Fun_FillData(DataTable dt_Process)
        {
            if (!dt_Process.IsNull())
            {
                #region ProcessData填資料

                //頭段導帶鋼種
                Lbl_Head_ST_NO.Text = dt_Process.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_St_No)].ToString() ?? string.Empty;
                //頭段導帶長度
                Lbl_Headead_Len.Text = dt_Process.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Length)].ToString() ?? string.Empty;
                //頭段導帶寬度
                Lbl_HeadWidth.Text = dt_Process.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Width)].ToString() ?? string.Empty;
                //頭段導帶厚度
                Lbl_HeadThick.Text = dt_Process.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Head_Leader_Thickness)].ToString() ?? string.Empty;
                //尾段導帶鋼種
                Lbl_Tail_ST_NO.Text = dt_Process.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_St_No)].ToString() ?? string.Empty;
                //尾段導帶長度
                Lbl_TailLen.Text = dt_Process.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Length)].ToString() ?? string.Empty;
                //尾段導帶寬度
                Lbl_TailWidth.Text = dt_Process.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Width)].ToString() ?? string.Empty;
                //尾段導帶厚度
                Lbl_TailThick.Text = dt_Process.Rows[0][nameof(LeaderTempEntity.TBL_Leader_Temp.Tail_Leader_Thickness)].ToString() ?? string.Empty;

                //張力機鋼種大類
                Lbl_TensionMaterialGrade.Text = dt_Process.Rows[0][nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Material_Grade)].ToString() ?? string.Empty;
                //張力機卷寬
                Lbl_TensionWidth.Text = dt_Process.Rows[0][nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Width)].ToString() ?? string.Empty;
                //張力機卷厚最大值
                Lbl_TensionThick_Max.Text = dt_Process.Rows[0][nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Max)].ToString() ?? string.Empty;
                //張力機卷厚最小值
                Lbl_TensionThick_Min.Text = dt_Process.Rows[0][nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.Coil_Thickness_Min)].ToString() ?? string.Empty;
                //開卷機張力
                Lbl_POR_Tension.Text = dt_Process.Rows[0][nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.PORTension)].ToString() ?? string.Empty;
                //收卷機張力
                Lbl_TR_Tension.Text = dt_Process.Rows[0][nameof(LkUpTableLineTensionEntity.TBL_LookupTable_LineTension.TRTension)].ToString() ?? string.Empty;            
                //裁邊機鋼種大類
                Lbl_TrimmerMaterialGrade.Text = dt_Process.Rows[0][nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Material_Grade)].ToString() ?? string.Empty;
                //裁邊機卷厚最大值
                Lbl_TrimmerThick_Max.Text = dt_Process.Rows[0][nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Max)].ToString() ?? string.Empty;
                //裁邊機卷厚最小值
                Lbl_TrimmerThick_Min.Text = dt_Process.Rows[0][nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.Coil_Thickness_Min)].ToString() ?? string.Empty;
                //裁邊機刀具間隙
                Lbl_TrimGap.Text = dt_Process.Rows[0][nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.KnifeGap)].ToString() ?? string.Empty;
                //裁邊機刀具重疊量
                Lbl_TrimLap.Text = dt_Process.Rows[0][nameof(LkUpTableSideTrimmerEntity.TBL_LookupTable_SideTrimmer.KnifeLap)].ToString() ?? string.Empty;
                #endregion
            }
        }

        /// <summary>
        /// 選取Row代出生產相關資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_coilList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void Frm_2_2_HistoryProductionSetup_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                Fun_SelectComboBoxCoilList();
            }
        }
    }
}
