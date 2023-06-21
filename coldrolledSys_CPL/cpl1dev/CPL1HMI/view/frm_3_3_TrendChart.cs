using CPL1HMI.comm;
using DBService.Repository.LineStatus;
using DBService.Repository.PDI;
using DBService.Repository.PDO;
using DBService.Repository.ProcessDataCT;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ZedGraph;
using static DBService.Repository.LineStatus.ProcessDataEntity;
using static DBService.Repository.PDO.PDOEntity;

namespace CPL1HMI
{
    public partial class frm_3_3_TrendChart : Form
    {
        #region -- 變數 --
        //mousemove
        private bool IscanMove;
        private bool IscanMove_Weld;
        //用於取X軸時間與設定時間線
        System.Windows.Forms.DataVisualization.Charting.DataPoint ChartPoint;
        int PointIndex;

        //語系
        private LanguageHandler LanguageHand;


        //common names
        private const string X_passpos = "passpos";
        private const string X_datetime = "date_time";
        private const string Y_value = "value";
        private const string DataCode = "DataCode";
        private const string DataDesc = "DataDesc";
        #region TBL_ProcessDataCT [DataCode] //2022.02.07 New Chart Data
        
        private string Code_LINE_Speed_Actual;//实际产线速度
        private string Code_POR_Tension_Actual;//开卷机实际张力
        private string Code_POR_Current_Actual;//开卷机实际电流
        private string Code_POR_Tension_Set;//开卷机张力设定
        private const string Code_LINE_Speed_Actual_1 = "CPUNC01C0001";//实际产线速度(CPL1)
        private const string Code_POR_Tension_Actual_1 = "CPUNC01C0002";//开卷机实际张力(CPL1)
        private const string Code_POR_Current_Actual_1 = "CPUNC01C0003";//开卷机实际电流(CPL1)
        private const string Code_POR_Tension_Set_1 = "CPUNC01C0004";//开卷机张力设定(CPL1)
        private const string Code_LINE_Speed_Actual_2 = "CPUNC02C0001";//实际产线速度(CPL2)
        private const string Code_POR_Tension_Actual_2 = "CPUNC02C0002";//开卷机实际张力(CPL2)
        private const string Code_POR_Current_Actual_2 = "CPUNC02C0003";//开卷机实际电流(CPL2)
        private const string Code_POR_Tension_Set_2 = "CPUNC02C0004";//开卷机张力设定(CPL2)

        private string Code_TR_Tension_Actual;//收卷机实际张力
        private string Code_TR_Current_Actual;//收卷机实际电流
        private string Code_TR_Tension_Set;//收卷机张力设定
        private const string Code_TR_Tension_Actual_1 = "CPREC01C0001";//收卷机实际张力(CPL1)
        private const string Code_TR_Current_Actual_1 = "CPREC01C0002";//收卷机实际电流(CPL1)
        private const string Code_TR_Tension_Set_1 = "CPREC01C0003";//收卷机张力设定(CPL1)
        private const string Code_TR_Tension_Actual_2 = "CPREC02C0001";//收卷机实际张力(CPL2)
        private const string Code_TR_Current_Actual_2 = "CPREC02C0002";//收卷机实际电流(CPL2)
        private const string Code_TR_Tension_Set_2 = "CPREC02C0003";//收卷机张力设定(CPL2)

        private string Code_WELD_Current_Actual_Front;//焊接实际电流(前端)
        private string Code_WELD_Current_Actual_Rear;//焊接实际电流(后端)
        private string Code_WELD_Speed_Actual;//实际焊接速度
        private string Code_WELD_PlanishWheelForce_Actual;//实际焊接平板輪力
        private string Code_WELD_Temperture;//焊接溫度
        private const string Code_WELD_Current_Actual_Front_1 = "CPWEL01C0001";//焊接实际电流(前端)(CPL1)
        private const string Code_WELD_Current_Actual_Rear_1 = "CPWEL01C0002";//焊接实际电流(后端)(CPL1)
        private const string Code_WELD_Speed_Actual_1 = "CPWEL01C0003";//实际焊接速度(CPL1)
        private const string Code_WELD_PlanishWheelForce_Actual_1 = "CPWEL01C0004";//实际焊接平板輪力(CPL1)
        private const string Code_WELD_Temperture_1 = "CPWEL01C0005";//焊接溫度(CPL1)
        private const string Code_WELD_Current_Actual_Front_2 = "CPWEL02C0001";//焊接实际电流(前端)(CPL2)
        private const string Code_WELD_Current_Actual_Rear_2 = "CPWEL02C0002";//焊接实际电流(后端)(CPL2)
        private const string Code_WELD_Speed_Actual_2 = "CPWEL02C0003";//实际焊接速度(CPL2)
        private const string Code_WELD_PlanishWheelForce_Actual_2 = "CPWEL02C0004";//实际焊接平板輪力(CPL2)
        private const string Code_WELD_Temperture_2 = "CPWEL02C0005";//焊接溫度(CPL2)

        #endregion
        #region TBL_ProcessDataCT [DataDesc] //2022.02.07 New Chart Data
        private const string Desc_current = "current";
        private const string Desc_tension = "tension";
        private const string Desc_speed = "speed";
        private const string Desc_weld = "weld";
        #endregion
        #region item_En
        //=================================================
        private const string LINE_Speed_Actual_en = "LINE_Speed_Actual";//实际产线速度

        private const string POR_Tension_Set_en = "POR_Tension_Set";//开卷机张力设定
        private const string POR_Tension_Actual_en = "POR_Tension_Actual";//开卷机实际张力
        private const string POR_Current_Actual_en = "POR_Current_Actual";//开卷机实际电流

        private const string TR_Tension_Set_en = "TR_Tension_Set";//收卷机张力设定
        private const string TR_Tension_Actual_en = "TR_Tension_Actual";//收卷机实际张力
        private const string TR_Current_Actual_en = "TR_Current_Actual";//收卷机实际电流

        private const string WELD_Speed_Actual_en = "WELD_Speed_Actual";//实际焊接速度
        private const string WELD_Current_Actual_Front_en = "WELD_Current_Actual_Front";//焊接实际电流(前端)
        private const string WELD_Current_Actual_Rear_en = "WELD_Current_Actual_Rear";//焊接实际电流(后端)
        private const string WELD_PlanishWheelForce_Actual_en = "WELD_PlanishWheelForce_Actual";//实际焊接平板輪力
        private const string WELD_Temperture_en = "WELD_Temperture";//焊接溫度
        #endregion
        #region item_Cn
        //=================================================
        private const string LINE_Speed_Actual_cn = "实际产线速度";//LINE_Speed_Actual

        private const string POR_Tension_Set_cn = "开卷机张力设定";//POR_Tension_Set
        private const string POR_Tension_Actual_cn = "开卷机实际张力";//POR_Tension_Actual
        private const string POR_Current_Actual_cn = "开卷机实际电流";//POR_Current_Actual

        private const string TR_Tension_Set_cn = "收卷机张力设定";//TR_Tension_Set
        private const string TR_Tension_Actual_cn = "收卷机实际张力";//TR_Tension_Actual
        private const string TR_Current_Actual_cn = "收卷机实际电流";//TR_Current_Actual

        private const string WELD_Speed_Actual_cn = "实际焊接速度";//WELD_Speed_Actual
        private const string WELD_Current_Actual_Front_cn = "焊接实际电流(前端)";//WELD_Current_Actual_Front
        private const string WELD_Current_Actual_Rear_cn = "焊接实际电流(后端)";//WELD_Current_Actual_Rear
        private const string WELD_PlanishWheelForce_Actual_cn = "实际焊接平板輪力";//WELD_PlanishWheelForce_Actual
        private const string WELD_Temperture_cn = "焊接溫度";//WELD_Temperture
        #endregion

        #endregion

        public frm_3_3_TrendChart()
        {
            InitializeComponent();
        }

        private void Frm_3_3_TrendChart_Load(object sender, EventArgs e)
        {
           // LanguageHandler.Instance.Fun_CurrentLanguage(this.Name);
            IscanMove = false;
            IscanMove_Weld = false;

            if (PublicForms.TrendChart == null) PublicForms.TrendChart = this;

            Code_LINE_Speed_Actual = GlobalVariableHandler.proLine.Equals("CPL1") ? Code_LINE_Speed_Actual_1 : Code_LINE_Speed_Actual_2;
            Code_POR_Tension_Actual = GlobalVariableHandler.proLine.Equals("CPL1") ? Code_POR_Tension_Actual_1 : Code_POR_Tension_Actual_2;
            Code_POR_Current_Actual = GlobalVariableHandler.proLine.Equals("CPL1") ? Code_POR_Current_Actual_1 : Code_POR_Current_Actual_2;
            Code_POR_Tension_Set = GlobalVariableHandler.proLine.Equals("CPL1") ? Code_POR_Tension_Set_1 : Code_POR_Tension_Set_2;
            Code_TR_Tension_Actual = GlobalVariableHandler.proLine.Equals("CPL1") ? Code_TR_Tension_Actual_1 : Code_TR_Tension_Actual_2;
            Code_TR_Current_Actual = GlobalVariableHandler.proLine.Equals("CPL1") ? Code_TR_Current_Actual_1 : Code_TR_Current_Actual_2;
            Code_TR_Tension_Set = GlobalVariableHandler.proLine.Equals("CPL1") ? Code_TR_Tension_Set_1 : Code_TR_Tension_Set_2;
            Code_WELD_Current_Actual_Front = GlobalVariableHandler.proLine.Equals("CPL1") ? Code_WELD_Current_Actual_Front_1 : Code_WELD_Current_Actual_Front_2;
            Code_WELD_Current_Actual_Rear = GlobalVariableHandler.proLine.Equals("CPL1") ? Code_WELD_Current_Actual_Rear_1 : Code_WELD_Current_Actual_Rear_2;
            Code_WELD_Speed_Actual = GlobalVariableHandler.proLine.Equals("CPL1") ? Code_WELD_Speed_Actual_1 : Code_WELD_Speed_Actual_2;
            Code_WELD_PlanishWheelForce_Actual = GlobalVariableHandler.proLine.Equals("CPL1") ? Code_WELD_PlanishWheelForce_Actual_1 : Code_WELD_PlanishWheelForce_Actual_2;
            Code_WELD_Temperture = GlobalVariableHandler.proLine.Equals("CPL1") ? Code_WELD_Temperture_1 : Code_WELD_Temperture_2;

            //放最後...畫面Load後再取得現在語系做顯示
            LanguageHand = new LanguageHandler();
            LanguageHand.Fun_GetNowLangShow(this.Name);
        }
        private void Frm_3_3_TrendChart_Shown(object sender, EventArgs e)
        {
            Fun_SelectCoilList();
        }
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Fun_SelectCoilProcess();
        }

        private void Fun_SelectCoilProcess()
        {
            string strSql = Frm_3_3_SqlFactory.SQL_Select_ProcessData(Cob_CoilID.Text);

            DataTable dtProcessData = DataAccess.Fun_SelectDate(strSql, "Process Data");

            if (dtProcessData.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("查无资料","趋势图查询",0);

                return;
            } 

            Fun_DisplayProcessData(dtProcessData);

            IscanMove = true;
        }

        /// <summary>
        /// 鋼卷號清單
        /// </summary>
        private void Fun_SelectCoilList()
        {
            string strSql = Frm_3_1_SqlFactory.SQL_Select_PDOList();
            DataTable dtCoilID = DataAccess.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_CPL);

            if (dtCoilID.IsNull())
            {
                DialogHandler.Instance.Fun_DialogShowOk("查无钢卷号清单","查询钢卷号清单",0);

                return;
            } 

            Cob_CoilID.DisplayMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_CoilID.ValueMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_CoilID.DataSource = dtCoilID;

            Cob_CoilID_Weld.DisplayMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_CoilID_Weld.ValueMember = nameof(PDOEntity.TBL_PDO.Out_Coil_ID);
            Cob_CoilID_Weld.DataSource = dtCoilID;
        }
       
        /// <summary>
        /// 設定MS Chart
        /// </summary>
        private void Fun_DisplayProcessData(DataTable dtProcessData)
        {
            Chart_PresetData.Series.Clear();

            //使用內建 DataSource
            Chart_PresetData.DataSource = dtProcessData;
            SetChartSeriesData(Chk_LINE_Speed_Actual, nameof(ProcessDataEntity.TBL_ProcessData.LINE_Speed_Actual), nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime), nameof(ProcessDataEntity.TBL_ProcessData.LINE_Speed_Actual));
            SetChartSeriesData(Chk_POR_Tension_Set, nameof(ProcessDataEntity.TBL_ProcessData.POR_Tension_Set), nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime), nameof(ProcessDataEntity.TBL_ProcessData.POR_Tension_Set));
            SetChartSeriesData(Chk_POR_Tension_Actual, nameof(ProcessDataEntity.TBL_ProcessData.POR_Tension_Actual), nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime), nameof(ProcessDataEntity.TBL_ProcessData.POR_Tension_Actual));
            SetChartSeriesData(Chk_POR_Current_Actual, nameof(ProcessDataEntity.TBL_ProcessData.POR_Current_Actual), nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime), nameof(ProcessDataEntity.TBL_ProcessData.POR_Current_Actual));
            SetChartSeriesData(Chk_TR_Tension_Set, nameof(ProcessDataEntity.TBL_ProcessData.TR_Tension_Set), nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime), nameof(ProcessDataEntity.TBL_ProcessData.TR_Tension_Set));
            SetChartSeriesData(Chk_TR_Tension_Actual, nameof(ProcessDataEntity.TBL_ProcessData.TR_Tension_Actual), nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime), nameof(ProcessDataEntity.TBL_ProcessData.TR_Tension_Actual));
            SetChartSeriesData(Chk_TR_Current_Actual, nameof(ProcessDataEntity.TBL_ProcessData.TR_Current_Actual), nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime), nameof(ProcessDataEntity.TBL_ProcessData.TR_Current_Actual));
            SetChartSeriesData(Chk_WELD_Current_Actual_Front, nameof(ProcessDataEntity.TBL_ProcessData.WELD_Current_Actual_Front), nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime), nameof(ProcessDataEntity.TBL_ProcessData.WELD_Current_Actual_Front));
            SetChartSeriesData(Chk_WELD_Speed_Actual, nameof(ProcessDataEntity.TBL_ProcessData.WELD_Speed_Actual), nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime), nameof(ProcessDataEntity.TBL_ProcessData.WELD_Speed_Actual));
            SetChartSeriesData(Chk_WELD_Current_Actual_Rear, nameof(ProcessDataEntity.TBL_ProcessData.WELD_Current_Actual_Rear), nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime), nameof(ProcessDataEntity.TBL_ProcessData.WELD_Current_Actual_Rear));
            SetChartSeriesData(Chk_WELD_PlanishWheelForce_Actual, nameof(ProcessDataEntity.TBL_ProcessData.WELD_PlanishWheelForce_Actual), nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime), nameof(ProcessDataEntity.TBL_ProcessData.WELD_PlanishWheelForce_Actual));
            SetChartSeriesData(Chk_WELD_Temperture, nameof(ProcessDataEntity.TBL_ProcessData.WELD_Temperture), nameof(ProcessDataEntity.TBL_ProcessData.ReceiveTime), nameof(ProcessDataEntity.TBL_ProcessData.WELD_Temperture));

            Chart_PresetData.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss";
            Chart_PresetData.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            Chart_PresetData.ChartAreas[0].CursorX.IsUserEnabled = true;
            Chart_PresetData.ChartAreas[0].CursorX.LineColor = Color.Red;
            Chart_PresetData.ChartAreas[0].CursorX.LineWidth = 3;
            Chart_PresetData.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Solid;
            Chart_PresetData.ChartAreas[0].CursorX.Interval = 0;
            Chart_PresetData.ChartAreas[0].RecalculateAxesScale();
            Chart_PresetData.DataBind();
        }
        /// <summary>
        /// 設定MS Chart
        /// </summary>
       
        private void SetChartSeriesData(CheckBox checkBox, string seriesName, string xValue, string yValue)
        {
            if (checkBox.Checked)
            {
                Chart_PresetData.Series.Add(seriesName);
                Chart_PresetData.Series[seriesName].XValueMember = xValue;
                Chart_PresetData.Series[seriesName].YValueMembers = yValue;
                Chart_PresetData.Series[seriesName].ChartType = SeriesChartType.Line;
                Chart_PresetData.Series[seriesName].MarkerSize = 4;
                Chart_PresetData.Series[seriesName].MarkerStyle = MarkerStyle.Circle;
                Chart_PresetData.Series[seriesName].BorderWidth = 3;
            }
        }

        private void Chart_PresetData_MouseMove(object sender, MouseEventArgs e)
        {

            if (IscanMove)
            {
                //用座標取得對應X軸的值
                double searchValue = Chart_PresetData.ChartAreas[0].AxisX.PixelPositionToValue(e.X);

                //用X值去查找對應的Y值   Points是集合物件支援LINQ!  (判別哪條線有勾選，計算結果pointindex在帶入其他條線)
                List<CheckBox> checkBoxList = new List<CheckBox>
                {
                    #region -- CheckBox列表 --
                     //實際產線速度
                    Chk_LINE_Speed_Actual,
                    //開卷機張力設定
                    Chk_POR_Tension_Set,
                    //開卷機實際張力
                    Chk_POR_Tension_Actual,
                    //開卷機實際電流
                    Chk_POR_Current_Actual,
                    //收卷機張力設定
                    Chk_TR_Tension_Set,
                    //收卷機實際張力
                    Chk_TR_Tension_Actual,
                    //收卷機實際電流
                    Chk_TR_Current_Actual,
                    //實際焊接電流
                    Chk_WELD_Current_Actual_Front,
                    //實際焊接速度
                    Chk_WELD_Speed_Actual,
                    //实际焊接轮力
                    Chk_WELD_Current_Actual_Rear,
                    //实际焊接平板輪力
                    Chk_WELD_PlanishWheelForce_Actual,
                    //焊接溫度
                    Chk_WELD_Temperture
	                #endregion
                };

                CheckBox IsChecked = checkBoxList.Where(x => x.Checked == true).FirstOrDefault();

                if (IsChecked != null)
                {
                    ////找出第一個目標point，取得pointindex，讓其他series取用
                    //var labelName = "Lbl_" + IsChecked.Name.Substring(4);//lb

                    //System.Windows.Forms.Label label = (System.Windows.Forms.Label)Pnl_Chart_PorTr.Controls.Find(labelName, true).FirstOrDefault();

                    ////目標跑函數  其他使用datapoint  index
                    //label.Text = GetYTextByXValue(labelName.Substring(4), searchValue);  

                    //foreach (var item in checkBoxList)
                    //{
                    //    //其他series使用datapoint index 來取pointY值
                    //    if (!item.Name.Equals(IsChecked.Name))
                    //    {
                    //        string anotherLabelName = "Lbl_" + item.Name.Substring(4);//lb

                    //        System.Windows.Forms.Label anotherLabel = (System.Windows.Forms.Label)Pnl_Chart_PorTr.Controls.Find(anotherLabelName, true).FirstOrDefault();

                    //        anotherLabel.Text = item.Checked.Equals(true) ? Chart_PresetData.Series[anotherLabelName.Substring(4)].Points[PointIndex].YValues[0].ToString() : string.Empty;
                    //    }
                    //}

                    //設定時間值 與 光標位置(圖表的X值) <--用目標point來取
                    var point1 = ChartPoint;

                    //Lbl_ReceiveTime.Text = DateTime.FromOADate(point1.XValue).ToString("yyyy/MM/dd HH:mm:ss");

                    Chart_PresetData.ChartAreas[0].CursorX.SetCursorPosition(point1.XValue);
                }
            }
        }

        //取得X值 對應的Y值Text
        private string GetYTextByXValue(string seriesName, double searchValue)
        {
            System.Windows.Forms.DataVisualization.Charting.DataPoint closestValuePoint = null;

            //取得最接近的point
            foreach (var item in Chart_PresetData.Series[seriesName].Points)
            {
                if (closestValuePoint == null)
                {
                    closestValuePoint = item;
                }

                closestValuePoint = Math.Abs(item.XValue - searchValue) < Math.Abs(closestValuePoint.XValue - searchValue) ? item : closestValuePoint;
            }

            ChartPoint = closestValuePoint;

            PointIndex = Chart_PresetData.Series[seriesName].Points.IndexOf(closestValuePoint);

            return closestValuePoint.YValues[0].ToString();
        }

        /// <summary>
        /// CheckBox選取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrendChart_CheckedChanged(object sender, EventArgs e)
        {
            //Fun_SelectCoilProcess();
        }

        private void Cob_CoilID_Click(object sender, EventArgs e)
        {
            Fun_SelectCoilList();
        }

        private void Btn_Search_New_Click(object sender, EventArgs e)
        {
            DataTable dtGetPdo = Fun_GetPdoData(Cob_CoilID.Text);
            Fun_Create_CoilChart(Zgc_CoilChart, Cob_CoilID.Text, dtGetPdo);

            //if (dtProcessData.IsNull())
            //{
            //    DialogHandler.Instance.Fun_DialogShowOk("查无资料", "趋势图查询", 0);
            //    return;
            //}          
            IscanMove = true;

            #region Old...Old...
            ////GraphPane myPane = new GraphPane();
            //////一键复原缩放
            ////this.ZedGraphControl1.ZoomOutAll(myPane);

            //Zgc_CoilChart.GraphPane.CurveList.Clear();
            //Zgc_CoilChart.GraphPane.GraphObjList.Clear();
            //Zgc_CoilChart.GraphPane.Y2AxisList.Clear();
            //Zgc_CoilChart.GraphPane.YAxisList.Clear();

            //Zgc_CoilChart.Refresh();

            //MasterPane masterPane = Zgc_CoilChart.MasterPane;
            //Zgc_CoilChart.GraphPane.YAxisList.Add("New_YAxis");
            //Zgc_CoilChart.GraphPane.Y2AxisList.Add("New_Y2Axis");

            //int intCountCheck = 0;

            //if (Chk_LINE_Speed_Actual.Checked)
            //{
            //    intCountCheck += 1;
            //    DataTable dtTrend = Fun_GetTrendData("APLT_TrendWidth", Cob_CoilID.Text);
            //    ZedGraphSetData zgSetData = new ZedGraphSetData()
            //    {
            //        dtTrend = dtTrend,
            //        StrXpoint = "passpos",
            //        StrYpoint = "value",
            //        StrTitle = "",// Cob_Coil_No.Text,
            //        StrXTitle = "米數",
            //        StrYTitle = "寬度",
            //        StrLineItem_Title = "寬度",
            //        LineItem_Color = Color.Blue,
            //        LineItem_Type = SymbolType.Diamond//菱形
            //    };

            //    GraphPane graphs = Fun_ZG(zgSetData, 1000, 500, intCountCheck);

            //    //// Make the Y axis scale red
            //    //graphs.YAxis.Scale.FontSpec.FontColor = zgSetData.LineItem_Color;
            //    //graphs.YAxis.Title.FontSpec.FontColor = zgSetData.LineItem_Color;
            //    //// turn off the opposite tics so the Y tics don't show up on the Y2 axis
            //    //graphs.YAxis.MajorTic.IsOpposite = false;
            //    //graphs.YAxis.MinorTic.IsOpposite = false;
            //    //// Don't display the Y zero line
            //    //graphs.YAxis.MajorGrid.IsZeroLine = false;
            //    //// Align the Y axis labels so they are flush to the axis
            //    //graphs.YAxis.Scale.Align = AlignP.Inside;
            //    //graphs.YAxis.Scale.Max = 1000;

            //    masterPane.Add(graphs);
            //}
            //else { }

            //if (Chk_POR_Tension_Set.Checked)
            //{
            //    intCountCheck += 1;
            //    DataTable dtTrend = Fun_GetTrendData("APLT_TrendThick", Cob_CoilID.Text);
            //    ZedGraphSetData zgSetData = new ZedGraphSetData()
            //    {
            //        dtTrend = dtTrend,
            //        StrXpoint = "passpos",
            //        StrYpoint = "avg_value",
            //        StrTitle = "",// Cob_Coil_No.Text,
            //        StrXTitle = "米數",
            //        StrYTitle = "厚度",
            //        StrLineItem_Title = "厚度",
            //        LineItem_Color = Color.Pink,
            //        LineItem_Type = SymbolType.Triangle//等邊三角形
            //    };
            //    GraphPane graphs = Fun_ZG(zgSetData, 3, 0, intCountCheck);

            //    //// Make the Y2 axis scale blue
            //    //graphs.Y2Axis.Scale.FontSpec.FontColor = zgSetData.LineItem_Color;
            //    //graphs.Y2Axis.Title.FontSpec.FontColor = zgSetData.LineItem_Color;
            //    //// turn off the opposite tics so the Y2 tics don't show up on the Y axis
            //    //graphs.Y2Axis.MajorTic.IsOpposite = false;
            //    //graphs.Y2Axis.MinorTic.IsOpposite = false;
            //    //// Display the Y2 axis grid lines
            //    //graphs.Y2Axis.MajorGrid.IsVisible = true;
            //    //// Align the Y2 axis labels so they are flush to the axis
            //    //graphs.Y2Axis.Scale.Align = AlignP.Inside;
            //    //graphs.Y2Axis.Scale.Min = 0.5;
            //    //graphs.Y2Axis.Scale.Max = 3;

            //    masterPane.Add(graphs);

            //}
            //else { }

            //if (Chk_POR_Tension_Actual.Checked)
            //{
            //    intCountCheck += 1;
            //    DataTable dtTrend = Fun_GetTrendData("APLT_TrendStrTempActHeat1", Cob_CoilID.Text);
            //    ZedGraphSetData zgSetData = new ZedGraphSetData()
            //    {
            //        dtTrend = dtTrend,
            //        StrXpoint = "passpos",
            //        StrYpoint = "value",
            //        StrTitle = "",// Cob_Coil_No.Text,
            //        StrXTitle = "米數",
            //        StrYTitle = "7區材溫",
            //        StrLineItem_Title = "7區材溫",
            //        LineItem_Color = Color.Green,
            //        LineItem_Type = SymbolType.Circle//圓形
            //    };
            //    GraphPane graphs = Fun_ZG(zgSetData, 1000, 500, intCountCheck);

            //    masterPane.Add(graphs);

            //}
            //else { }


            //if (Chk_POR_Current_Actual.Checked)
            //{
            //    intCountCheck += 1;
            //    DataTable dtTrend = Fun_GetTrendData("APLT_TrendStrTempActHeat2", Cob_CoilID.Text);
            //    ZedGraphSetData zgSetData = new ZedGraphSetData()
            //    {
            //        dtTrend = dtTrend,
            //        StrXpoint = "passpos",
            //        StrYpoint = "value",
            //        StrTitle = "",// Cob_Coil_No.Text,
            //        StrXTitle = "米數",
            //        StrYTitle = "9區材溫",
            //        StrLineItem_Title = "9區材溫",
            //        LineItem_Color = Color.Orange,
            //        LineItem_Type = SymbolType.Star//星號
            //    };
            //    GraphPane graphs = Fun_ZG(zgSetData, 1000, 500, intCountCheck);

            //    masterPane.Add(graphs);

            //}
            //else { }

            //int intPaneCount = masterPane.PaneList[0].CurveList.Count();

            ////if (intPaneCount == 1)
            ////{
            ////    masterPane.PaneList[0].YAxis.Title.Text = masterPane.PaneList[0].YAxis.Title.ToString();
            ////    //myPaneT.YAxis.Title.FontSpec.Size = 5;
            ////    //myPaneT.YAxis.Scale.Max = 3500;
            ////    //myPaneT.YAxis.Scale.Min = -3500;
            ////    //myPaneT.YAxis.Scale.MajorStep = 1000;
            ////    //myPaneT.YAxis.MajorTic.IsOpposite = false;
            ////    //myPaneT.YAxis.MinorTic.IsOpposite = false; //小刻度
            ////    masterPane.PaneList[0].YAxis.Scale.Align = AlignP.Inside;
            ////}
            ////else if(intPaneCount == 2)
            ////{
            ////    masterPane.PaneList[0].YAxis.Title.Text = masterPane.PaneList[0].YAxis.Title.ToString();
            ////    masterPane.PaneList[0].YAxis.Scale.Align = AlignP.Inside;
            ////}
            ////else if (intPaneCount > 2)
            ////{
            //int intCount = intPaneCount;
            //bool bolHaveYA = false;
            //bool bolHaveY2A = false;


            //for (int i = 0; i < intPaneCount; i++)
            //{
            //    if (intCount % 2 == 0)
            //    {
            //        if (!bolHaveYA)
            //        {
            //            masterPane.PaneList[0].YAxis.Title.Text = masterPane.PaneList[0].CurveList[i].Label.Text;
            //            masterPane.PaneList[0].YAxis.Scale.Align = AlignP.Inside;
            //            intCount--;
            //            bolHaveYA = true;
            //        }
            //        else
            //        {
            //            if (!bolHaveY2A)
            //            {
            //                //masterPane.PaneList[0].CurveList[i].IsYAxis = true;
            //                masterPane.PaneList[0].YAxis.IsVisible = true;
            //                masterPane.PaneList[0].YAxis.MajorGrid.IsVisible = true;
            //                masterPane.PaneList[0].YAxis.Title.Text = masterPane.PaneList[0].CurveList[i].Label.Text;
            //                masterPane.PaneList[0].YAxis.Scale.Align = AlignP.Inside;
            //                intCount--;
            //                bolHaveY2A = true;
            //            }
            //            else
            //            {
            //                YAxis yAxis3 = new YAxis(masterPane.PaneList[0].CurveList[i].Label.Text);
            //                masterPane.PaneList[0].YAxisList.Add(yAxis3);
            //                intCount--;
            //                bolHaveYA = true;
            //            }
            //        }


            //    }
            //    else
            //    {
            //        if (!bolHaveYA)
            //        {
            //            masterPane.PaneList[0].YAxis.Title.Text = masterPane.PaneList[0].CurveList[i].Label.Text;
            //            masterPane.PaneList[0].YAxis.Scale.Align = AlignP.Inside;
            //            intCount--;
            //            bolHaveYA = true;
            //        }
            //        else
            //        {


            //            if (!bolHaveY2A)
            //            {
            //                //masterPane.PaneList[0].CurveList[i].IsY2Axis = true;
            //                masterPane.PaneList[0].YAxis.IsVisible = true;
            //                masterPane.PaneList[0].YAxis.MajorGrid.IsVisible = true;
            //                masterPane.PaneList[0].YAxis.Title.Text = masterPane.PaneList[0].CurveList[i].Label.Text;
            //                masterPane.PaneList[0].YAxis.Scale.Align = AlignP.Inside;
            //                intCount--;
            //                bolHaveY2A = true;
            //            }
            //            else
            //            {
            //                if (intCount % 2 == 0)
            //                {
            //                    //Y2Axis yAxis4 = new Y2Axis(masterPane.PaneList[0].CurveList[i].Label.Text);
            //                    //yAxis4.IsVisible = true;
            //                    //masterPane.PaneList[0].Y2AxisList.Add(yAxis4);
            //                    intCount--;
            //                    bolHaveY2A = true;
            //                }
            //                else
            //                {
            //                    //YAxis yAxis3 = new YAxis(masterPane.PaneList[0].CurveList[i].Label.Text);
            //                    //masterPane.PaneList[0].YAxisList.Add(yAxis3);
            //                    intCount--;
            //                    bolHaveYA = true;
            //                }


            //            }
            //        }

            //    }
            //}
            ////}
            ////else 
            ////{

            ////}

            ////for (int i = 0; i < yList.Count; i++)
            ////{
            ////    LineItem myCurve = myPane.AddCurve(yList[i].Title, DataChartHelper.SetCurveText(interval, xAttribute.TitleKey, yList[i].TitleKey), System.Drawing.ColorTranslator.FromHtml(yList[i].Color), SymbolType.None);
            ////    //根据配置文件设置曲线类型
            ////    myCurve = setCurveType(myCurve, yList[i].Type, yList[i].Color);
            ////    myCurve.YAxisIndex = i;
            ////}




            //Zgc_CoilChart.AxisChange();
            //Zgc_CoilChart.Invalidate();
            ////ZedGraphControl1.Size = new Size(1712, 611);
            //Zgc_CoilChart.IsShowPointValues = true;// '顯示節點資料
            #endregion
        }
        public void Fun_Create_CoilChart(ZedGraphControl zgc, string strCoilNo, DataTable dtGetPdo)
        {
            //初始化ZedGraph
            Fun_IniZedGraph(zgc);
            MasterPane masterPane = zgc.MasterPane;
            GraphPane myPane = zgc.GraphPane;
            LineItem myCurve;

            //设置标题和轴标签
            myPane.Title.Text = $"{strCoilNo}";

            StringBuilder sbTableNoData = new StringBuilder();

            //==============================================================================

            SymbolType sItem_Type = SymbolType.None;//菱形

            #region Chk_LINE_Speed_Actual
            if (Chk_LINE_Speed_Actual.Checked)
            {
                DataTable dtPointData = Fun_GetTrendData(Desc_speed, Code_LINE_Speed_Actual,  strCoilNo, dtGetPdo);
                if (dtPointData.IsNull())
                {
                    if(LanguageHandler.Instance.DefaultLanguage)
                        sbTableNoData.Append($"{LINE_Speed_Actual_cn},");
                    else
                        sbTableNoData.Append($"{LINE_Speed_Actual_en},");
                }
                else
                {
                    ZedGraphSetData zgSetData = new ZedGraphSetData()
                    {
                        DtTrend = dtPointData,
                        StrYpoint = Y_value,
                        StrTitle = strCoilNo,
                        StrXTitle = "米數(m)",
                        LineItem_Color = Color.MediumVioletRed,
                        StrTag = LINE_Speed_Actual_en,
                        LineItem_Type = sItem_Type
                    };
                    if (LanguageHandler.Instance.DefaultLanguage)
                    {
                        zgSetData.StrYTitle = LINE_Speed_Actual_cn;
                        zgSetData.StrLineItem_Title = LINE_Speed_Actual_cn;
                    }
                    else
                    {
                        zgSetData.StrYTitle = LINE_Speed_Actual_en;
                        zgSetData.StrLineItem_Title = LINE_Speed_Actual_en;
                    }

                    GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
                    masterPane.Add(graphs);
                }
            }
            else { }
            #endregion Chk_LINE_Speed_Actual...End

            #region Chk_POR_Tension_Actual
            if (Chk_POR_Tension_Actual.Checked)
            {
                DataTable dtPointData = Fun_GetTrendData(Desc_tension, Code_POR_Tension_Actual, strCoilNo, dtGetPdo);
                if (dtPointData.IsNull())
                {
                    if (LanguageHandler.Instance.DefaultLanguage)
                        sbTableNoData.Append($"{POR_Tension_Actual_cn},");
                    else
                        sbTableNoData.Append($"{POR_Tension_Actual_en},");
                }
                else
                {
                    ZedGraphSetData zgSetData = new ZedGraphSetData()
                    {
                        DtTrend = dtPointData,
                        StrYpoint = Y_value,
                        StrTitle = strCoilNo,
                        StrXTitle = "米數(m)",
                        LineItem_Color = Color.DeepPink,
                        StrTag = POR_Tension_Actual_en,
                        LineItem_Type = sItem_Type
                    };
                    if (LanguageHandler.Instance.DefaultLanguage)
                    {
                        zgSetData.StrYTitle = POR_Tension_Actual_cn;
                        zgSetData.StrLineItem_Title = POR_Tension_Actual_cn;
                    }
                    else
                    {
                        zgSetData.StrYTitle = POR_Tension_Actual_en;
                        zgSetData.StrLineItem_Title = POR_Tension_Actual_en;
                    }

                    GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
                    masterPane.Add(graphs);
                }
            }
            else { }
            #endregion Chk_POR_Tension_Actual...End

            #region Chk_POR_Current_Actual
            if (Chk_POR_Current_Actual.Checked)
            {
                DataTable dtPointData = Fun_GetTrendData(Desc_current, Code_POR_Current_Actual, strCoilNo, dtGetPdo);
                if (dtPointData.IsNull())
                {
                    if (LanguageHandler.Instance.DefaultLanguage)
                        sbTableNoData.Append($"{POR_Current_Actual_cn},");
                    else
                        sbTableNoData.Append($"{POR_Current_Actual_en},");
                }
                else
                {
                    ZedGraphSetData zgSetData = new ZedGraphSetData()
                    {
                        DtTrend = dtPointData,
                        StrYpoint = Y_value,
                        StrTitle = strCoilNo,
                        StrXTitle = "米數(m)",
                        LineItem_Color = Color.OrangeRed,
                        StrTag = POR_Current_Actual_en,
                        LineItem_Type = sItem_Type
                    };
                    if (LanguageHandler.Instance.DefaultLanguage)
                    {
                        zgSetData.StrYTitle = POR_Current_Actual_cn;
                        zgSetData.StrLineItem_Title = POR_Current_Actual_cn;
                    }
                    else
                    {
                        zgSetData.StrYTitle = POR_Current_Actual_en;
                        zgSetData.StrLineItem_Title = POR_Current_Actual_en;
                    }

                    GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
                    masterPane.Add(graphs);
                }
            }
            else { }
            #endregion Chk_POR_Current_Actual...End

            #region Chk_POR_Tension_Set
            if (Chk_POR_Tension_Set.Checked)
            {
                DataTable dtPointData = Fun_GetTrendData(Desc_tension, Code_POR_Tension_Set, strCoilNo, dtGetPdo);
                if (dtPointData.IsNull())
                {
                    if (LanguageHandler.Instance.DefaultLanguage)
                        sbTableNoData.Append($"{POR_Tension_Set_cn},");
                    else
                        sbTableNoData.Append($"{POR_Tension_Set_en},");
                }
                else
                {
                    ZedGraphSetData zgSetData = new ZedGraphSetData()
                    {
                        DtTrend = dtPointData,
                        StrYpoint = Y_value,
                        StrTitle = strCoilNo,
                        StrXTitle = "米數(m)",
                        LineItem_Color = Color.DarkOrange,
                        StrTag = POR_Tension_Set_en,
                        LineItem_Type = sItem_Type
                    };
                    if (LanguageHandler.Instance.DefaultLanguage)
                    {
                        zgSetData.StrYTitle = POR_Tension_Set_cn;
                        zgSetData.StrLineItem_Title = POR_Tension_Set_cn;
                    }
                    else
                    {
                        zgSetData.StrYTitle = POR_Tension_Set_en;
                        zgSetData.StrLineItem_Title = POR_Tension_Set_en;
                    }

                    GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
                    masterPane.Add(graphs);
                }
            }
            else { }
            #endregion Chk_POR_Tension_Set...End

            #region Chk_TR_Tension_Actual
            if (Chk_TR_Tension_Actual.Checked)
            {
                DataTable dtPointData = Fun_GetTrendData(Desc_tension, Code_TR_Tension_Set, strCoilNo, dtGetPdo);
                if (dtPointData.IsNull())
                {
                    if (LanguageHandler.Instance.DefaultLanguage)
                        sbTableNoData.Append($"{TR_Tension_Actual_cn},");
                    else
                        sbTableNoData.Append($"{TR_Tension_Actual_en},");
                }
                else
                {
                    ZedGraphSetData zgSetData = new ZedGraphSetData()
                    {
                        DtTrend = dtPointData,
                        StrYpoint = Y_value,
                        StrTitle = strCoilNo,
                        StrXTitle = "米數(m)",
                        LineItem_Color = Color.Goldenrod,
                        StrTag = TR_Tension_Actual_en,
                        LineItem_Type = sItem_Type
                    };
                    if (LanguageHandler.Instance.DefaultLanguage)
                    {
                        zgSetData.StrYTitle = TR_Tension_Actual_cn;
                        zgSetData.StrLineItem_Title = TR_Tension_Actual_cn;
                    }
                    else
                    {
                        zgSetData.StrYTitle = TR_Tension_Actual_en;
                        zgSetData.StrLineItem_Title = TR_Tension_Actual_en;
                    }

                    GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
                    masterPane.Add(graphs);
                }
            }
            else { }
            #endregion Chk_TR_Tension_Actual...End

            #region Chk_TR_Current_Actual
            if (Chk_TR_Current_Actual.Checked)
            {
                DataTable dtPointData = Fun_GetTrendData(Desc_current, Code_TR_Current_Actual, strCoilNo, dtGetPdo);
                if (dtPointData.IsNull())
                {
                    if (LanguageHandler.Instance.DefaultLanguage)
                        sbTableNoData.Append($"{TR_Current_Actual_cn},");
                    else
                        sbTableNoData.Append($"{TR_Current_Actual_en},");
                }
                else
                {
                    ZedGraphSetData zgSetData = new ZedGraphSetData()
                    {
                        DtTrend = dtPointData,
                        StrYpoint = Y_value,
                        StrTitle = strCoilNo,
                        StrXTitle = "米數(m)",
                        LineItem_Color = Color.Green,
                        StrTag = TR_Current_Actual_en,
                        LineItem_Type = sItem_Type
                    };
                    if (LanguageHandler.Instance.DefaultLanguage)
                    {
                        zgSetData.StrYTitle = TR_Current_Actual_cn;
                        zgSetData.StrLineItem_Title = TR_Current_Actual_cn;
                    }
                    else
                    {
                        zgSetData.StrYTitle = TR_Current_Actual_en;
                        zgSetData.StrLineItem_Title = TR_Current_Actual_en;
                    }

                    GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
                    masterPane.Add(graphs);
                }
            }
            else { }
            #endregion Chk_TR_Current_Actual...End

            #region Chk_TR_Tension_Set
            if (Chk_TR_Tension_Set.Checked)
            {
                DataTable dtPointData = Fun_GetTrendData(Desc_tension, Code_TR_Tension_Set, strCoilNo, dtGetPdo);
                if (dtPointData.IsNull())
                {
                    if (LanguageHandler.Instance.DefaultLanguage)
                        sbTableNoData.Append($"{TR_Tension_Set_cn},");
                    else
                        sbTableNoData.Append($"{TR_Tension_Set_en},");
                }
                else
                {
                    ZedGraphSetData zgSetData = new ZedGraphSetData()
                    {
                        DtTrend = dtPointData,
                        StrYpoint = Y_value,
                        StrTitle = strCoilNo,
                        StrXTitle = "米數(m)",
                        LineItem_Color = Color.GreenYellow,
                        StrTag = TR_Tension_Set_en,
                        LineItem_Type = sItem_Type
                    };
                    if (LanguageHandler.Instance.DefaultLanguage)
                    {
                        zgSetData.StrYTitle = TR_Tension_Set_cn;
                        zgSetData.StrLineItem_Title = TR_Tension_Set_cn;
                    }
                    else
                    {
                        zgSetData.StrYTitle = TR_Tension_Set_en;
                        zgSetData.StrLineItem_Title = TR_Tension_Set_en;
                    }

                    GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
                    masterPane.Add(graphs);
                }
            }
            else { }
            #endregion Chk_TR_Tension_Set...End

            #region Chk_WELD_Speed_Actual
            //if (Chk_WELD_Speed_Actual.Checked)
            //{
            //    DataTable dtPointData = Fun_GetTrendData(Desc_weld, Code_WELD_Speed_Actual, strCoilNo, dtGetPdo);
            //    if (dtPointData.IsNull())
            //    {
            //        if (LanguageHandler.Instance.DefaultLanguage)
            //            sbTableNoData.Append($"{WELD_Speed_Actual_cn},");
            //        else
            //            sbTableNoData.Append($"{WELD_Speed_Actual_en},");
            //    }
            //    else
            //    {
            //        ZedGraphSetData zgSetData = new ZedGraphSetData()
            //        {
            //            DtTrend = dtPointData,
            //            StrYpoint = value,
            //            StrTitle = strCoilNo,
            //            StrXTitle = "米數(m)",
            //            LineItem_Color = Color.Navy,
            //            StrTag = WELD_Speed_Actual_en,
            //            LineItem_Type = sItem_Type
            //        };
            //        if (LanguageHandler.Instance.DefaultLanguage)
            //        {
            //            zgSetData.StrYTitle = WELD_Speed_Actual_cn;
            //            zgSetData.StrLineItem_Title = WELD_Speed_Actual_cn;
            //        }
            //        else
            //        {
            //            zgSetData.StrYTitle = WELD_Speed_Actual_en;
            //            zgSetData.StrLineItem_Title = WELD_Speed_Actual_en;
            //        }

            //        GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
            //        masterPane.Add(graphs);
            //    }
            //}
            //else { }
            #endregion Chk_WELD_Speed_Actual...End

            #region Chk_WELD_Current_Actual_Front
            //if (Chk_WELD_Current_Actual_Front.Checked)
            //{
            //    DataTable dtPointData = Fun_GetTrendData(Desc_weld, Code_WELD_Current_Actual_Front, strCoilNo, dtGetPdo);
            //    if (dtPointData.IsNull())
            //    {
            //        if (LanguageHandler.Instance.DefaultLanguage)
            //            sbTableNoData.Append($"{WELD_Current_Actual_Front_cn},");
            //        else
            //            sbTableNoData.Append($"{WELD_Current_Actual_Front_en},");
            //    }
            //    else
            //    {
            //        ZedGraphSetData zgSetData = new ZedGraphSetData()
            //        {
            //            DtTrend = dtPointData,
            //            StrYpoint = value,
            //            StrTitle = strCoilNo,
            //            StrXTitle = "米數(m)",
            //            LineItem_Color = Color.Blue,
            //            StrTag = WELD_Current_Actual_Front_en,
            //            LineItem_Type = sItem_Type
            //        };
            //        if (LanguageHandler.Instance.DefaultLanguage)
            //        {
            //            zgSetData.StrYTitle = WELD_Current_Actual_Front_cn;
            //            zgSetData.StrLineItem_Title = WELD_Current_Actual_Front_cn;
            //        }
            //        else
            //        {
            //            zgSetData.StrYTitle = WELD_Current_Actual_Front_en;
            //            zgSetData.StrLineItem_Title = WELD_Current_Actual_Front_en;
            //        }

            //        GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
            //        masterPane.Add(graphs);
            //    }
            //}
            //else { }
            #endregion Chk_WELD_Current_Actual_Front...End

            #region Chk_WELD_Current_Actual_Rear
            //if (Chk_WELD_Current_Actual_Rear.Checked)
            //{
            //    DataTable dtPointData = Fun_GetTrendData(Desc_weld, Code_WELD_Current_Actual_Rear, strCoilNo, dtGetPdo);
            //    if (dtPointData.IsNull())
            //    {
            //        if (LanguageHandler.Instance.DefaultLanguage)
            //            sbTableNoData.Append($"{WELD_Current_Actual_Rear_cn},");
            //        else
            //            sbTableNoData.Append($"{WELD_Current_Actual_Rear_en},");
            //    }
            //    else
            //    {
            //        ZedGraphSetData zgSetData = new ZedGraphSetData()
            //        {
            //            DtTrend = dtPointData,
            //            StrYpoint = value,
            //            StrTitle = strCoilNo,
            //            StrXTitle = "米數(m)",
            //            LineItem_Color = Color.CornflowerBlue,
            //            StrTag = WELD_Current_Actual_Rear_en,
            //            LineItem_Type = sItem_Type
            //        };
            //        if (LanguageHandler.Instance.DefaultLanguage)
            //        {
            //            zgSetData.StrYTitle = WELD_Current_Actual_Rear_cn;
            //            zgSetData.StrLineItem_Title = WELD_Current_Actual_Rear_cn;
            //        }
            //        else
            //        {
            //            zgSetData.StrYTitle = WELD_Current_Actual_Rear_en;
            //            zgSetData.StrLineItem_Title = WELD_Current_Actual_Rear_en;
            //        }

            //        GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
            //        masterPane.Add(graphs);
            //    }
            //}
            //else { }
            #endregion Chk_WELD_Current_Actual_Rear...End

            #region Chk_WELD_PlanishWheelForce_Actual
            //if (Chk_WELD_PlanishWheelForce_Actual.Checked)
            //{
            //    DataTable dtPointData = Fun_GetTrendData(Desc_weld, Code_WELD_PlanishWheelForce_Actual, strCoilNo, dtGetPdo);
            //    if (dtPointData.IsNull())
            //    {
            //        if (LanguageHandler.Instance.DefaultLanguage)
            //            sbTableNoData.Append($"{WELD_PlanishWheelForce_Actual_cn},");
            //        else
            //            sbTableNoData.Append($"{WELD_PlanishWheelForce_Actual_en},");
            //    }
            //    else
            //    {
            //        ZedGraphSetData zgSetData = new ZedGraphSetData()
            //        {
            //            DtTrend = dtPointData,
            //            StrYpoint = value,
            //            StrTitle = strCoilNo,
            //            StrXTitle = "米數(m)",
            //            LineItem_Color = Color.Purple,
            //            StrTag = WELD_PlanishWheelForce_Actual_en,
            //            LineItem_Type = sItem_Type
            //        };
            //        if (LanguageHandler.Instance.DefaultLanguage)
            //        {
            //            zgSetData.StrYTitle = WELD_PlanishWheelForce_Actual_cn;
            //            zgSetData.StrLineItem_Title = WELD_PlanishWheelForce_Actual_cn;
            //        }
            //        else
            //        {
            //            zgSetData.StrYTitle = WELD_PlanishWheelForce_Actual_en;
            //            zgSetData.StrLineItem_Title = WELD_PlanishWheelForce_Actual_en;
            //        }

            //        GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
            //        masterPane.Add(graphs);
            //    }
            //}
            //else { }
            #endregion Chk_WELD_PlanishWheelForce_Actual...End

            #region Chk_WELD_Temperture
            //if (Chk_WELD_Temperture.Checked)
            //{
            //    DataTable dtPointData = Fun_GetTrendData(Desc_weld, Code_WELD_Temperture, strCoilNo, dtGetPdo);
            //    if (dtPointData.IsNull())
            //    {
            //        if (LanguageHandler.Instance.DefaultLanguage)
            //            sbTableNoData.Append($"{WELD_Temperture_cn},");
            //        else
            //            sbTableNoData.Append($"{WELD_Temperture_en},");
            //    }
            //    else
            //    {
            //        ZedGraphSetData zgSetData = new ZedGraphSetData()
            //        {
            //            DtTrend = dtPointData,
            //            StrYpoint = value,
            //            StrTitle = strCoilNo,
            //            StrXTitle = "米數(m)",
            //            LineItem_Color = Color.DarkOrchid,
            //            StrTag = WELD_Temperture_en,
            //            LineItem_Type = sItem_Type
            //        };
            //        if (LanguageHandler.Instance.DefaultLanguage)
            //        {
            //            zgSetData.StrYTitle = WELD_Temperture_cn;
            //            zgSetData.StrLineItem_Title = WELD_Temperture_cn;
            //        }
            //        else
            //        {
            //            zgSetData.StrYTitle = WELD_Temperture_en;
            //            zgSetData.StrLineItem_Title = WELD_Temperture_en;
            //        }

            //        GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc);
            //        masterPane.Add(graphs);
            //    }
            //}
            //else { }
            #endregion Chk_WELD_Temperture...End

            //==============================================================================
            if (myPane.XAxis != null)
            {
                //显示 x 轴 Title
                myPane.XAxis.Title.Text = "米数(m)";
                //显示 x 轴 网格
                myPane.XAxis.MajorGrid.IsVisible = true;
            }

            if (myPane.YAxis != null)
            {
                //显示 y 轴 网格
                myPane.YAxis.MajorGrid.IsVisible = true;
            }
            //==============================================================================
            //用渐变填充轴背
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);

            //曲線標籤 外框 (false = 不顯示)
            Border border = new Border(false, Color.Black, 10);
            myPane.Legend.Border = border;
            //曲線標籤 位置
           // myPane.Legend.Position = LegendPos.Left;


            zgc.AxisChange();
            zgc.Invalidate();
            zgc.IsShowPointValues = true;// '顯示節點資料
            //恢复默认大小
            zgc.RestoreScale(myPane);
        }

        private void Btn_Search_Weld_Click(object sender, EventArgs e)
        {
            DataTable dtGetPdo = Fun_GetPdoData(Cob_CoilID_Weld.Text);
            Fun_Create_CoilChart_Weld(Zgc_CoilChart_Weld, Cob_CoilID_Weld.Text, dtGetPdo);
            IscanMove_Weld = true;
        }
        public void Fun_Create_CoilChart_Weld(ZedGraphControl zgc, string strCoilNo, DataTable dtGetPdo)
        {
            //初始化ZedGraph
            Fun_IniZedGraph(zgc);
            MasterPane masterPane = zgc.MasterPane;
            GraphPane myPane = zgc.GraphPane;
            LineItem myCurve;

            //设置标题和轴标签
            myPane.Title.Text = $"{strCoilNo}";

            StringBuilder sbTableNoData = new StringBuilder();

            //==============================================================================

            //測試資料PDO:CM201230010000  PDI:HE201230010000

            SymbolType sItem_Type = SymbolType.None;//菱形
                      
            #region Chk_WELD_Speed_Actual
            if (Chk_WELD_Speed_Actual.Checked)
            {
                DataTable dtPointData = Fun_GetTrendData(Desc_weld, Code_WELD_Speed_Actual, strCoilNo, dtGetPdo);
                if (dtPointData.IsNull())
                {
                    if (LanguageHandler.Instance.DefaultLanguage)
                        sbTableNoData.Append($"{WELD_Speed_Actual_cn},");
                    else
                        sbTableNoData.Append($"{WELD_Speed_Actual_en},");
                }
                else
                {
                    ZedGraphSetData zgSetData = new ZedGraphSetData()
                    {
                        DtTrend = dtPointData,
                        StrXpoint = X_datetime,
                        StrYpoint = Y_value,
                        StrTitle = strCoilNo,
                        StrXTitle = "时间",
                        LineItem_Color = Color.DeepPink,
                        StrTag = WELD_Speed_Actual_en,
                        LineItem_Type = sItem_Type
                    };
                    if (LanguageHandler.Instance.DefaultLanguage)
                    {
                        zgSetData.StrYTitle = WELD_Speed_Actual_cn;
                        zgSetData.StrLineItem_Title = WELD_Speed_Actual_cn;
                    }
                    else
                    {
                        zgSetData.StrYTitle = WELD_Speed_Actual_en;
                        zgSetData.StrLineItem_Title = WELD_Speed_Actual_en;
                    }

                    GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc, dtPointData);
                    masterPane.Add(graphs);
                }
            }
            else { }
            #endregion Chk_WELD_Speed_Actual...End

            #region Chk_WELD_Current_Actual_Front
            if (Chk_WELD_Current_Actual_Front.Checked)
            {
                DataTable dtPointData = Fun_GetTrendData(Desc_weld, Code_WELD_Current_Actual_Front, strCoilNo, dtGetPdo);
                if (dtPointData.IsNull())
                {
                    if (LanguageHandler.Instance.DefaultLanguage)
                        sbTableNoData.Append($"{WELD_Current_Actual_Front_cn},");
                    else
                        sbTableNoData.Append($"{WELD_Current_Actual_Front_en},");
                }
                else
                {
                    ZedGraphSetData zgSetData = new ZedGraphSetData()
                    {
                        DtTrend = dtPointData,
                        StrXpoint = X_datetime,
                        StrYpoint = Y_value,
                        StrTitle = strCoilNo,
                        StrXTitle = "时间",
                        LineItem_Color = Color.Blue,
                        StrTag = WELD_Current_Actual_Front_en,
                        LineItem_Type = sItem_Type
                    };
                    if (LanguageHandler.Instance.DefaultLanguage)
                    {
                        zgSetData.StrYTitle = WELD_Current_Actual_Front_cn;
                        zgSetData.StrLineItem_Title = WELD_Current_Actual_Front_cn;
                    }
                    else
                    {
                        zgSetData.StrYTitle = WELD_Current_Actual_Front_en;
                        zgSetData.StrLineItem_Title = WELD_Current_Actual_Front_en;
                    }

                    GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc, dtPointData);
                    masterPane.Add(graphs);
                }
            }
            else { }
            #endregion Chk_WELD_Current_Actual_Front...End

            #region Chk_WELD_Current_Actual_Rear
            if (Chk_WELD_Current_Actual_Rear.Checked)
            {
                DataTable dtPointData = Fun_GetTrendData(Desc_weld, Code_WELD_Current_Actual_Rear, strCoilNo, dtGetPdo);
                if (dtPointData.IsNull())
                {
                    if (LanguageHandler.Instance.DefaultLanguage)
                        sbTableNoData.Append($"{WELD_Current_Actual_Rear_cn},");
                    else
                        sbTableNoData.Append($"{WELD_Current_Actual_Rear_en},");
                }
                else
                {
                    ZedGraphSetData zgSetData = new ZedGraphSetData()
                    {
                        DtTrend = dtPointData,
                        StrXpoint = X_datetime,
                        StrYpoint = Y_value,
                        StrTitle = strCoilNo,
                        StrXTitle = "时间",
                        LineItem_Color = Color.Green,
                        StrTag = WELD_Current_Actual_Rear_en,
                        LineItem_Type = sItem_Type
                    };
                    if (LanguageHandler.Instance.DefaultLanguage)
                    {
                        zgSetData.StrYTitle = WELD_Current_Actual_Rear_cn;
                        zgSetData.StrLineItem_Title = WELD_Current_Actual_Rear_cn;
                    }
                    else
                    {
                        zgSetData.StrYTitle = WELD_Current_Actual_Rear_en;
                        zgSetData.StrLineItem_Title = WELD_Current_Actual_Rear_en;
                    }

                    GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc, dtPointData);
                    masterPane.Add(graphs);
                }
            }
            else { }
            #endregion Chk_WELD_Current_Actual_Rear...End

            #region Chk_WELD_PlanishWheelForce_Actual
            if (Chk_WELD_PlanishWheelForce_Actual.Checked)
            {
                DataTable dtPointData = Fun_GetTrendData(Desc_weld, Code_WELD_PlanishWheelForce_Actual, strCoilNo, dtGetPdo);
                if (dtPointData.IsNull())
                {
                    if (LanguageHandler.Instance.DefaultLanguage)
                        sbTableNoData.Append($"{WELD_PlanishWheelForce_Actual_cn},");
                    else
                        sbTableNoData.Append($"{WELD_PlanishWheelForce_Actual_en},");
                }
                else
                {
                    ZedGraphSetData zgSetData = new ZedGraphSetData()
                    {
                        DtTrend = dtPointData,
                        StrXpoint = X_datetime,
                        StrYpoint = Y_value,
                        StrTitle = strCoilNo,
                        StrXTitle = "时间",
                        LineItem_Color = Color.Purple,
                        StrTag = WELD_PlanishWheelForce_Actual_en,
                        LineItem_Type = sItem_Type
                    };
                    if (LanguageHandler.Instance.DefaultLanguage)
                    {
                        zgSetData.StrYTitle = WELD_PlanishWheelForce_Actual_cn;
                        zgSetData.StrLineItem_Title = WELD_PlanishWheelForce_Actual_cn;
                    }
                    else
                    {
                        zgSetData.StrYTitle = WELD_PlanishWheelForce_Actual_en;
                        zgSetData.StrLineItem_Title = WELD_PlanishWheelForce_Actual_en;
                    }

                    GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc, dtPointData);
                    masterPane.Add(graphs);
                }
            }
            else { }
            #endregion Chk_WELD_PlanishWheelForce_Actual...End

            #region Chk_WELD_Temperture
            if (Chk_WELD_Temperture.Checked)
            {
                DataTable dtPointData = Fun_GetTrendData(Desc_weld, Code_WELD_Temperture, strCoilNo, dtGetPdo);
                if (dtPointData.IsNull())
                {
                    if (LanguageHandler.Instance.DefaultLanguage)
                        sbTableNoData.Append($"{WELD_Temperture_cn},");
                    else
                        sbTableNoData.Append($"{WELD_Temperture_en},");
                }
                else
                {
                    ZedGraphSetData zgSetData = new ZedGraphSetData()
                    {
                        DtTrend = dtPointData,
                        StrXpoint = X_datetime,                      
                        StrYpoint = Y_value,
                        StrTitle = strCoilNo,
                        StrXTitle = "时间",
                        LineItem_Color = Color.DarkOrange,
                        StrTag = WELD_Temperture_en,
                        LineItem_Type = sItem_Type
                    };
                    if (LanguageHandler.Instance.DefaultLanguage)
                    {
                        zgSetData.StrYTitle = WELD_Temperture_cn;
                        zgSetData.StrLineItem_Title = WELD_Temperture_cn;
                    }
                    else
                    {
                        zgSetData.StrYTitle = WELD_Temperture_en;
                        zgSetData.StrLineItem_Title = WELD_Temperture_en;
                    }

                    GraphPane graphs = Fun_GetGraphPane(zgSetData, zgc, dtPointData);
                    masterPane.Add(graphs);
                }
            }
            else { }
            #endregion Chk_WELD_Temperture...End

            //==============================================================================
            if (myPane.XAxis != null)
            {
                //显示 x 轴 Title
                myPane.XAxis.Title.Text = "时间";
                //显示 x 轴 网格
                myPane.XAxis.MajorGrid.IsVisible = true;
            }

            if (myPane.YAxis != null)
            {
                //显示 y 轴 网格
                myPane.YAxis.MajorGrid.IsVisible = true;
            }
            //==============================================================================
            //用渐变填充轴背
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);

            zgc.AxisChange();
            zgc.Invalidate();
            zgc.IsShowPointValues = true;// '顯示節點資料
            //恢复默认大小
            zgc.RestoreScale(myPane);
        }

        private DataTable Fun_GetTrendData(string strDataDesc, string strDataCode, string strCoil, DataTable dtSelectPdo)
        {
            DateTime dtTime_S;
            DateTime dtTime_F;
            DateTime.TryParse(dtSelectPdo.Rows[0][nameof(PDOEntity.TBL_PDO.StartTime)].ToString(), out dtTime_S);

            DateTime.TryParse(dtSelectPdo.Rows[0][nameof(PDOEntity.TBL_PDO.FinishTime)].ToString(), out dtTime_F);    

            string strStartTime = dtTime_S.ToString("yyyyMMddHHmmss");
            string strFinishTime = dtTime_F.ToString("yyyyMMddHHmmss");
            string strBeginDate = strStartTime.Remove(8);
            string strBeginTime = strStartTime.Remove(0, 8);
            string strStopDate = strFinishTime.Remove(8);
            string strStopTime = strFinishTime.Remove(0, 8);


            DataTable dt = new DataTable();
            DataTable dtdata = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" SELECT * FROM {nameof(ProcessDataCTEntity.TBL_ProcessDataCT)}");//strTableName
          
            sb.AppendLine($" WHERE {nameof(ProcessDataCTEntity.TBL_ProcessDataCT.Out_Coil_No)} = N'" + strCoil + "'");

            sb.AppendLine($" AND {nameof(ProcessDataCTEntity.TBL_ProcessDataCT.BeginDate)} = N'{ strBeginDate}'");
            sb.AppendLine($" AND {nameof(ProcessDataCTEntity.TBL_ProcessDataCT.BeginTime)} = N'{ strBeginTime}'");
            sb.AppendLine($" AND {nameof(ProcessDataCTEntity.TBL_ProcessDataCT.StopDate)}  = N'{ strStopDate}'");
            sb.AppendLine($" AND {nameof(ProcessDataCTEntity.TBL_ProcessDataCT.StopTime)}  = N'{ strStopTime}'");

            sb.AppendLine($" AND {nameof(ProcessDataCTEntity.TBL_ProcessDataCT.DataDesc)} = N'" + strDataDesc + "'");
            sb.AppendLine($" AND {nameof(ProcessDataCTEntity.TBL_ProcessDataCT.DataCode)} = N'" + strDataCode + "'");
            sb.AppendLine($"");

            string strSql = sb.ToString();
            dtdata = DataAccess.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_CPL);
            if (dtdata != null && dtdata.Rows.Count > 0)
                dt = Fun_TransDataToDt(dtdata, Y_value, strDataDesc);           

            return dt;
        }

        /// <summary>
        /// 设定要绘制的图表
        /// </summary>
        /// <param name="ZGsetData">ZedGraph相关设定资料</param>
        /// <param name="dobMax">Y轴刻度最大值</param>
        /// <param name="dobMin">Y轴刻度最小值</param>
        /// <param name="zgCtrl">ZedGraph元件名称</param>
        /// <returns>GraphPane</returns>
        private GraphPane Fun_GetGraphPane(ZedGraphSetData ZGsetData, ZedGraphControl zgCtrl,DataTable dtX = null, double dobMax = -1, double dobMin = -1)
        {
            GraphPane gpZGC;
            gpZGC = zgCtrl.GraphPane;

            if (string.IsNullOrEmpty(ZGsetData.StrXpoint))
                ZGsetData.StrXpoint = X_passpos;

            gpZGC.Title.Text = ZGsetData.StrTitle;
            gpZGC.Tag = ZGsetData.StrTag;

            PointPairList pointList = new PointPairList();
            string[] strXArr = new string[] { };
            double[] douYArr = new double[] { };
            LineItem myCurve;
           
            if (ZGsetData.StrXpoint == X_datetime)
            {
                //X_datetime 用不到 pointList
                pointList = Fun_GetPointPairList(ZGsetData.DtTrend, ZGsetData.StrYpoint, ZGsetData.StrXpoint);

                List<string> Xlist = new List<string>(strXArr.ToList());
                List<double> Ylist = new List<double>(douYArr.ToList());
                foreach (DataRow dr in dtX.Rows)
                {
                    //"yyyyMMddHHmmss" => "yyyy-MM-dd HH:mm:ss"
                    string strX = dr[X_datetime].ToString().Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":");
                    double.TryParse(dr[ZGsetData.StrYpoint].ToString(), out double douY);//double douY =

                    Xlist.Add(strX);
                    Ylist.Add(douY);
                }
                strXArr = Xlist.ToArray();
                douYArr = Ylist.ToArray();

                //gpZGC.XAxis.Type = ZedGraph.AxisType.Date;
                //gpZGC.XAxis.Scale.FontSpec.Angle = 65;
                //gpZGC.XAxis.Scale.MajorStep = 1;
                //gpZGC.XAxis.Scale.MajorUnit = DateUnit.Day;
                //gpZGC.XAxis.Scale.MinorUnit = DateUnit.Day;
                //gpZGC.XAxis.Scale.Format = "yyyy-MM-dd HH:mm:ss";
                
                gpZGC.XAxis.Type = ZedGraph.AxisType.Text;// Date;
                gpZGC.XAxis.Scale.TextLabels = strXArr;//.IsDate
                gpZGC.XAxis.Scale.FontSpec.Size = 10;
                gpZGC.XAxis.Scale.FontSpec.Angle = 65;

                myCurve = gpZGC.AddCurve(ZGsetData.StrLineItem_Title, null, douYArr, ZGsetData.LineItem_Color, ZGsetData.LineItem_Type);
            }
            else
            {
                pointList = Fun_GetPointPairList(ZGsetData.DtTrend, ZGsetData.StrYpoint);
                myCurve = gpZGC.AddCurve(ZGsetData.StrLineItem_Title, pointList, ZGsetData.LineItem_Color, ZGsetData.LineItem_Type);
            }

            //myCurve.Line.IsSmooth = false;//Smooth 平滑曲線
            myCurve.Line.Width = 3;
            //myCurve.Symbol.IsVisible = false;
            //
            bool bolIsY1 = true;
            YAxis y2 = new YAxis();
            if(gpZGC.YAxisList.Count >0)
                bolIsY1 = false;
            y2.IsVisible = bolIsY1;
            y2.Title.Text = ZGsetData.StrYTitle;// ZGsetData.StrLineItem_Title;
            gpZGC.YAxisList.Add(y2);

            //设定Y轴 刻度颜色,字体大小
            y2.Scale.FontSpec.FontColor = ZGsetData.LineItem_Color;
            y2.Scale.FontSpec.Size = 12;
            //设定Y轴 文字颜色,字体大小
            y2.Title.FontSpec.FontColor = ZGsetData.LineItem_Color;
            y2.Title.FontSpec.Size = 12;

            //设定Y轴 刻度 不顯示
            y2.Scale.IsVisible = bolIsY1;
            //设定Y轴 文字 不顯示
            y2.Title.IsVisible = false;
            
            //將X、Y軸的對面座標軸隱藏 
            y2.MajorTic.IsInside = false;//頭尾端(大刻度)是否朝內 ; false = 不顯示
            y2.MinorTic.IsInside = false;//中間(小刻度)是否朝內 ; false = 不顯示
            y2.MajorTic.IsOpposite = false;//對面座標軸(大刻度)顯示 ; false = 不顯示
            y2.MinorTic.IsOpposite = false;//對面座標軸(小刻度)顯示 ; false = 不顯示
            // Align the Y2 axis labels so they are flush to the axis
            y2.Scale.Align = AlignP.Inside;
            //y2.Type = ZedGraph.AxisType.Log;

            if (dobMax == -1)
                dobMin = pointList.Min(point => point.Y) * 1.1;
            if (dobMax == -1)
                dobMax = pointList.Max(point => point.Y) * 0.9;

            y2.Scale.Max = dobMax;
            y2.Scale.Min = dobMin;
            //gpZGC.XAxis.Title.Text = "My X Axis"; //ZGsetData.StrXTitle;           
            //gpZGC.YAxis.Title.Text =  "My Y Axis";//ZGsetData.StrYTitle;
            return gpZGC;
        }
        /// <summary>
        /// 从DataTable取得PointPairList
        /// </summary>
        /// <param name="dtPointData">资料来源</param>
        /// <param name="strValue">Y轴栏位</param>
        /// <returns>PointPairList</returns>
        private PointPairList Fun_GetPointPairList(DataTable dtPointData, string strValue)
        {
            PointPairList pointList = new PointPairList();
            foreach (DataRow dr in dtPointData.Rows)
            {
                pointList.Add(double.Parse(dr[X_passpos].ToString()), double.Parse(dr[strValue].ToString()));
            }
            return pointList;
        }

        /// <summary>
        /// 从DataTable取得PointPairList
        /// </summary>
        /// <param name="dtPointData">资料来源</param>
        /// <param name="strValue">Y轴栏位</param>
        ///  <param name="strX">X轴栏位(yyyyMMddHHmmss)</param>
        /// <returns>PointPairList</returns>
        private PointPairList Fun_GetPointPairList(DataTable dtPointData, string strValue,string strX)
        {
            PointPairList pointList = new PointPairList();

            List<double> x = new List<double>();
            List<double> y = new List<double>();

            foreach (DataRow dr in dtPointData.Rows)
            {
                // pointList.Add(double.Parse(dr[strX].ToString()), double.Parse(dr[strValue].ToString()));
                string strD = dr[strX].ToString().Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":");
                //x.Add(new XDate(Convert.ToDateTime(dr[strX].ToString().Trim())));
                //y.Add(double.Parse(dr[strValue].ToString()));
                pointList.Add(new XDate(Convert.ToDateTime(strD)), double.Parse(dr[strValue].ToString()));//dr[strX].ToString().Trim()
            }
            return pointList;
        }

        private GraphPane Fun_ZG(ZedGraphSetData ZGsetData, int intMax, int intMin, int intCountCheck = 1)
        {

            GraphPane gpZGC = Zgc_CoilChart.GraphPane;

            gpZGC.Title.Text = ZGsetData.StrTitle;// "My Test Graph";
            gpZGC.XAxis.Title.Text = ZGsetData.StrXTitle;// "My X Axis";

            //if (intCountCheck % 2 == 0)
            //    gpZGC.Y2Axis.Title.Text = ZGsetData.StrYTitle;// "My Y Axis";
            //else
            gpZGC.YAxis.Title.Text = ZGsetData.StrYTitle;// "My Y Axis";

            double x, y;
            PointPairList list1 = new PointPairList();
            foreach (DataRow dr in ZGsetData.DtTrend.Rows)
            {
                x = Convert.ToDouble(dr[ZGsetData.StrXpoint].ToString());
                y = Convert.ToDouble(dr[ZGsetData.StrYpoint].ToString());
                list1.Add(x, y);
            }


            LineItem myCurve = gpZGC.AddCurve(ZGsetData.StrLineItem_Title, list1, ZGsetData.LineItem_Color, ZGsetData.LineItem_Type);

            if (intCountCheck % 2 == 0)
            {
                myCurve.IsY2Axis = true;
                if (intCountCheck > 2)
                {
                    YAxis y2 = new YAxis();
                    y2.IsVisible = true;
                    y2.Title.Text = ZGsetData.StrLineItem_Title;
                    gpZGC.YAxisList.Add(y2);
                    // turn off the opposite tics so the Y2 tics don't show up on the Y axis
                    y2.MajorTic.IsInside = false;
                    y2.MinorTic.IsInside = false;
                    y2.MajorTic.IsOpposite = false;
                    y2.MinorTic.IsOpposite = false;
                    // Align the Y2 axis labels so they are flush to the axis
                    y2.Scale.Align = AlignP.Inside;
                    y2.Type = ZedGraph.AxisType.Log;
                    y2.Scale.Max = intMax;
                    y2.Scale.Min = intMin;
                }
                else { }
            }
            else
            {
                if (intCountCheck > 2)
                {
                    YAxis y2 = new YAxis();
                    y2.IsVisible = true;
                    y2.Title.Text = ZGsetData.StrLineItem_Title;
                    gpZGC.YAxisList.Add(y2);
                    // turn off the opposite tics so the Y2 tics don't show up on the Y axis
                    y2.MajorTic.IsInside = false;
                    y2.MinorTic.IsInside = false;
                    y2.MajorTic.IsOpposite = false;
                    y2.MinorTic.IsOpposite = false;
                    // Align the Y2 axis labels so they are flush to the axis
                    y2.Scale.Align = AlignP.Inside;
                    y2.Type = ZedGraph.AxisType.Log;
                    y2.Scale.Max = intMax;
                    y2.Scale.Min = intMin;
                }
                else { }
            }




            return gpZGC;
        }

        private void Zgc_CoilChart_MouseMove(object sender, MouseEventArgs e)
        {
            ZedGraphControl zedGraph = (ZedGraphControl)sender;
            // Save the mouse location
            PointF mousePt = new PointF(e.X, e.Y);

            Graphics gc = zedGraph.CreateGraphics();
            Pen pen = new Pen(Color.Orange);
            //設定畫筆寬度
            pen.Width = 3;
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            RectangleF rect = zedGraph.GraphPane.Chart.Rect;

            //確保在畫圖區域
            if (rect.Contains(e.Location))
            {
                zedGraph.AxisChange();
                zedGraph.IsShowPointValues = true;//顯示節點資料
                zedGraph.Refresh();

                //畫豎線
                gc.DrawLine(pen, e.X, rect.Top, e.X, rect.Bottom);
                //畫橫線
                gc.DrawLine(pen, rect.Left, e.Y, rect.Right, e.Y);

                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                               
            }
            else { }

        }

        private void Zgc_CoilChart_MouseLeave(object sender, EventArgs e)
        {
            ZedGraphControl zedGraph = (ZedGraphControl)sender;
            Graphics gc = zedGraph.CreateGraphics();
            Pen pen = new Pen(Color.Orange);
            //設定畫筆寬度
            pen.Width = 1;
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            RectangleF rect = zedGraph.GraphPane.Chart.Rect;

            //zedGraphControl1.AxisChange();
            //zedGraphControl1.IsShowPointValues = true;//顯示節點資料
            zedGraph.Refresh();

            //畫豎線
            gc.DrawLine(pen, 0, 0, 0, 0);
            //畫橫線
            gc.DrawLine(pen, 0, 0, 0, 0);
        }


        //取得X值 對應的Y值Text
        private string GetYTextByXValue(ZedGraphControl zgCtrl,string seriesName, double searchValue)
        {
            System.Windows.Forms.DataVisualization.Charting.DataPoint closestValuePoint = null;

            //取得最接近的point
            foreach (var item in Chart_PresetData.Series[seriesName].Points)
            {
                if (closestValuePoint == null)
                {
                    closestValuePoint = item;
                }

                closestValuePoint = Math.Abs(item.XValue - searchValue) < Math.Abs(closestValuePoint.XValue - searchValue) ? item : closestValuePoint;
            }

            ChartPoint = closestValuePoint;

            PointIndex = Chart_PresetData.Series[seriesName].Points.IndexOf(closestValuePoint);

            return closestValuePoint.YValues[0].ToString();
        }

        /// <summary>
        /// 初始化_ZedGraphControl
        /// </summary>
        /// <param name="zgc"></param>
        private void Fun_IniZedGraph(ZedGraphControl zgc)
        {
            zgc.GraphPane.CurveList.Clear();
            zgc.GraphPane.GraphObjList.Clear();
            zgc.GraphPane.Y2AxisList.Clear();
            zgc.GraphPane.YAxisList.Clear();
            zgc.Refresh();

            //zgc.GraphPane.YAxisList.Add("New_YAxis");
            //zgc.GraphPane.Y2AxisList.Add("New_Y2Axis");
        }

        /// <summary>
        /// PDI有重複鋼捲號時,顯示選取視窗,以取得計劃號
        /// </summary>
        /// <param name="strCoilNo">鋼捲號</param>
        /// <returns></returns>
        private DataTable Fun_GetPdoData(string strCoilNo)
        {
            DataTable dtPdo = new DataTable();
            string strCoil_No = strCoilNo;
            //string strPlanNo = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"  SELECT  *");
                //sb.AppendLine($" {nameof(PDOEntity.TBL_PDO.Plan_No)}, ");
                //sb.AppendLine($" {nameof(PDOEntity.TBL_PDO.Mat_Seq_No)}, ");
                //sb.AppendLine($" {nameof(PDOEntity.TBL_PDO.Plan_Sort)}, ");
                //sb.AppendLine($" {nameof(PDOEntity.TBL_PDO.Entry_Coil_ID)}, ");
                //sb.AppendLine($" {nameof(PDOEntity.TBL_PDO.CreateTime)} ");
                sb.AppendLine($" FROM {nameof(PDOEntity.TBL_PDO)} ");
                sb.AppendLine($" WHERE {nameof(PDOEntity.TBL_PDO.Out_Coil_ID)} = '{ strCoil_No }' ");
                string strSql = sb.ToString();
                    //$" SELECT  Plan_No, Mat_Seq_No, Plan_Sort, Entry_Coil_ID, CreateTime  FROM TBL_PDI WHERE Entry_Coil_ID = '{ strCoil_No }' ";

                dtPdo = DataAccess.GetInstance().Select(strSql, GlobalVariableHandler.Instance.strConn_CPL); 
                //PublicComm.portal.DbHand.Fun_DataTable(strSql, PublicSystem.ConnectionString);

                //DataTable dtSelectBack = new DataTable();

                if (dtPdo.Rows.Count > 1)
                {
                    Frm_SelectDataOpen frm_SelectOpen = new Frm_SelectDataOpen
                    {
                        dtSelectData = dtPdo.Copy(),
                        strDataType = "PDO"
                    };
                    frm_SelectOpen.ShowDialog();
                    frm_SelectOpen.Dispose();

                    if (frm_SelectOpen.DialogResult == DialogResult.OK)
                    {
                        //string strSql_Select = Frm_3_2_SqlFactory.SQL_Select_PDODetail(frm_SelectOpen.strOut_Coil_ID, frm_SelectOpen.strFinishTime, frm_SelectOpen.strIn_Coil_ID, frm_SelectOpen.strPlan_No);
                        //dtSelectBack = DataAccess.Fun_SelectDate(strSql_Select, "PDODet");
                        dtPdo = frm_SelectOpen.dtSelectData.Copy();
                       // strPlanNo = dtSelectBack.Rows[0][nameof(PDOEntity.TBL_PDO.Plan_No)].ToString();
                    }
                    else
                    {
                      //  strPlanNo = dtPdo.Rows[0][nameof(PDOEntity.TBL_PDO.Plan_No)].ToString();
                    }
                }
                else if (dtPdo.Rows.Count == 1)
                {
                   // strPlanNo = dtPdo.Rows[0][nameof(PDOEntity.TBL_PDO.Plan_No)].ToString();
                }
                else
                {
                  //  strPlanNo = "";
                }


            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"{ex.Message}", "查询PDI钢卷的计划号", 0);
                //PublicComm.portal.DialogHand.Fun_DialogShowOk($"{ex.Message}", PublicConst.DialogInformation, Properties.Resources.dialogInformation, 0);
            }
            return dtPdo;
        }

        /// <summary>
        /// 指定資料分割字串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public DataTable Fun_TransDataToDt(DataTable data, string colName ,string strDataDesc)
        {
            var dt = new DataTable();
            if(strDataDesc == "weld")
                dt.Columns.Add("date_time");
            else
                dt.Columns.Add("passpos");
            dt.Columns.Add(colName);

            var items = data.Rows[0][nameof(ProcessDataCTEntity.TBL_ProcessDataCT.DataString)].ToString().Split(',');

            foreach (var item in items)
            {
                var vals = item.Split(':');

                dt.Rows.Add(new object[] { vals[0], vals[1] });
            }

            return dt;
        }

        private void Fun_LanguageIsEn_Font12_10_Chk(object sender, EventArgs e)
        {
            FontStyle fs = ((System.Windows.Forms.CheckBox)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((System.Windows.Forms.CheckBox)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((System.Windows.Forms.CheckBox)sender).Font = new Font(ffm, (float)12.25, fs);
            else
                ((System.Windows.Forms.CheckBox)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font12_9_Chk(object sender, EventArgs e)
        {

        }
        private void Fun_LanguageIsEn_Font12_10(object sender, EventArgs e)
        {
            FontStyle fs = ((System.Windows.Forms.Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((System.Windows.Forms.Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)12.25, fs);
            else
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)10, fs);
        }
        private void Fun_LanguageIsEn_Font12_9(object sender, EventArgs e)
        {
            FontStyle fs = ((System.Windows.Forms.Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((System.Windows.Forms.Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)12.25, fs);
            else
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)9, fs);
        }
        private void Fun_LanguageIsEn_Font12_8(object sender, EventArgs e)
        {
            FontStyle fs = ((System.Windows.Forms.Label)sender).Font.Style;
            System.Drawing.FontFamily ffm = ((System.Windows.Forms.Label)sender).Font.FontFamily;
            if (LanguageHandler.Instance.DefaultLanguage)
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)12.25, fs);
            else
                ((System.Windows.Forms.Label)sender).Font = new Font(ffm, (float)8, fs);
        }

        private string Zgc_CoilChart_PointValueEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            PointPair pt = curve[iPt]; 
            return curve.Label.Text + " \n X:" + pt.X.ToString() + " \n Y:" + pt.Y.ToString();
        }

        #region -- 匯出 --

        /// <summary>
        ///     匯出(開收捲機)
        /// </summary>
        private void Btn_Export_PorTr_Click(object sender, EventArgs e)
        {
            if (!IsGetSearchPdo(out var dtPdo, out var folderPath))
                return;

            var coilNo = dtPdo.Rows[0][$"{nameof(TBL_PDO.Out_Coil_ID)}"].ToString();
            var ds = new DataSet();

            if (Chk_LINE_Speed_Actual.Checked) GetTrendData(ref ds, Desc_speed, Code_LINE_Speed_Actual, coilNo, dtPdo, nameof(TBL_ProcessData.LINE_Speed_Actual));
            if (Chk_POR_Tension_Actual.Checked) GetTrendData(ref ds, Desc_tension, Code_POR_Tension_Actual, coilNo, dtPdo, nameof(TBL_ProcessData.POR_Tension_Actual));
            if (Chk_POR_Current_Actual.Checked) GetTrendData(ref ds, Desc_current, Code_POR_Current_Actual, coilNo, dtPdo, nameof(TBL_ProcessData.POR_Current_Actual));
            if (Chk_POR_Tension_Set.Checked) GetTrendData(ref ds, Desc_tension, Code_POR_Tension_Set, coilNo, dtPdo, nameof(TBL_ProcessData.POR_Tension_Set));
            if (Chk_TR_Tension_Actual.Checked) GetTrendData(ref ds, Desc_tension, Code_TR_Tension_Set, coilNo, dtPdo, nameof(TBL_ProcessData.TR_Tension_Actual));
            if (Chk_TR_Current_Actual.Checked) GetTrendData(ref ds, Desc_current, Code_TR_Current_Actual, coilNo, dtPdo, nameof(TBL_ProcessData.TR_Current_Actual));
            if (Chk_TR_Tension_Set.Checked) GetTrendData(ref ds, Desc_tension, Code_TR_Tension_Set, coilNo, dtPdo, nameof(TBL_ProcessData.TR_Tension_Set));

            Export(ds, folderPath, "开收卷机");
        }

        /// <summary>
        ///     匯出(焊機)
        /// </summary>
        private void Btn_Export_Weld_Click(object sender, EventArgs e)
        {
            if (!IsGetSearchPdo(out var dtPdo, out var folderPath))
                return;

            var coilNo = dtPdo.Rows[0][$"{nameof(TBL_PDO.Out_Coil_ID)}"].ToString();
            var ds = new DataSet();

            if (Chk_WELD_Speed_Actual.Checked) GetTrendData(ref ds, Desc_weld, Code_WELD_Speed_Actual, coilNo, dtPdo, nameof(TBL_ProcessData.WELD_Speed_Actual));
            if (Chk_WELD_Current_Actual_Front.Checked) GetTrendData(ref ds, Desc_weld, Code_WELD_Current_Actual_Front, coilNo, dtPdo, nameof(TBL_ProcessData.WELD_Current_Actual_Front));
            if (Chk_WELD_Current_Actual_Rear.Checked) GetTrendData(ref ds, Desc_weld, Code_WELD_Current_Actual_Rear, coilNo, dtPdo, nameof(TBL_ProcessData.WELD_Current_Actual_Rear));
            if (Chk_WELD_PlanishWheelForce_Actual.Checked) GetTrendData(ref ds, Desc_weld, Code_WELD_PlanishWheelForce_Actual, coilNo, dtPdo, nameof(TBL_ProcessData.WELD_PlanishWheelForce_Actual));
            if (Chk_WELD_Temperture.Checked) GetTrendData(ref ds, Desc_weld, Code_WELD_Temperture, coilNo, dtPdo, nameof(TBL_ProcessData.WELD_Temperture));

            Export(ds, folderPath, "焊机");
        }

        /// <summary>
        ///     選擇匯出路徑及檢查是否有 PDO
        /// </summary>
        /// <param name="dtPdo"> 輸出 PDO 資料 </param>
        /// <param name="folderPath"> 匯出的路徑 </param>
        /// <returns> true：成功，false：失敗 </returns>
        private bool IsGetSearchPdo(out DataTable dtPdo, out string folderPath)
        {
            dtPdo = null;

            if (!ExportFile.IsGetFolderPath(out folderPath))
                return false;

            dtPdo = Fun_GetPdoData(Cob_CoilID.Text);
            if (dtPdo != null)
                if (dtPdo.Rows.Count > 0)
                    return true;

            //PublicComm.portal.DialogHand.Fun_DialogShowOk("查询不到 {coilNo} 的数据! ", PublicConst.DialogCancel, Properties.Resources.dialogCancel, 3);
            return false;
        }

        /// <summary>
        ///     取得 trend 資料
        /// </summary>
        private void GetTrendData(ref DataSet ds, string desc, string code, string coilNo, DataTable dtPdo, string itemName)
        {
            var dt = Fun_GetTrendData(desc, code, coilNo, dtPdo);
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    dt.TableName = itemName;
                    ds.Tables.Add(dt);
                }
        }

        /// <summary>
        ///     執行匯出
        /// </summary>
        /// <param name="ds"> 連續值集合 </param>
        /// <param name="folderPath"> 資料夾路徑 </param>
        /// <param name="mode"> 匯出模式 </param>
        private void Export(DataSet ds, string folderPath, string mode)
        {
            var samplePath = $@"{Application.StartupPath}\Excel\趋势图范本.xls";
            var fileName = $"趋势图_{mode}";

            try
            {
                ExportFile.WriteToExcel_xls(SetExportFile, ds, samplePath, folderPath, fileName);

                DialogHandler.Instance.Fun_DialogShowOk("汇出Execl：完成! ", "趋势图", 0);
                PublicComm.ClientLog.Error($"汇出趋势图：完成。");
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk($"汇出Execl：失敗! ex.Message={ex.Message}", "趋势图", 3);
                PublicComm.ClientLog.Error($"汇出趋势图：失敗。");
                PublicComm.ClientLog.Error($"ex.Message={ex.Message}");
                PublicComm.ClientLog.Error($"ex.StackTrace={ex.StackTrace}");
            }
        }

        /// <summary>
        ///     取得描述
        /// </summary>
        /// <returns></returns>
        private string GetExportDescription(string name)
        {
            switch (name)
            {
                case nameof(TBL_ProcessData.LINE_Speed_Actual): return "实际产线速度";
                case nameof(TBL_ProcessData.POR_Tension_Actual): return "开卷机实际张力";
                case nameof(TBL_ProcessData.POR_Current_Actual): return "开卷机实际电流";
                case nameof(TBL_ProcessData.POR_Tension_Set): return "开卷机张力设定";
                case nameof(TBL_ProcessData.TR_Tension_Actual): return "收卷机实际张力";
                case nameof(TBL_ProcessData.TR_Current_Actual): return "收卷机实际电流";
                case nameof(TBL_ProcessData.TR_Tension_Set): return "收卷机张力设定";
                case nameof(TBL_ProcessData.WELD_Speed_Actual): return "实际焊接速度";
                case nameof(TBL_ProcessData.WELD_Current_Actual_Front): return "焊接实际电流(前端)";
                case nameof(TBL_ProcessData.WELD_Current_Actual_Rear): return "焊接实际电流(后端)";
                case nameof(TBL_ProcessData.WELD_PlanishWheelForce_Actual): return "实际焊接平板輪力";
                case nameof(TBL_ProcessData.WELD_Temperture): return "焊接溫度";
                default:
                    throw new Exception($"未定義的 name({name})..");
            }
        }

        /// <summary>
        ///     設置匯出資料
        /// </summary>
        /// <returns></returns>
        private IWorkbook SetExportFile(IWorkbook wb, object obj)
        {
            var ds = obj as DataSet;
            var now = DateTime.Now;
            var timeRange = $"{now:yyyyMMdd}-{now:HHmmss}";
            var wsSample = wb.GetSheet("Sample");
            var wsSampleIdx = wb.GetSheetIndex(wsSample.SheetName);

            foreach (DataTable dt in ds.Tables)
            {
                var name = dt.TableName;
                var ws = wb.CloneSheet(wsSampleIdx);
                var coilNo = Cob_CoilID.Text;
                var desc = GetExportDescription(name);
                var dataVals = DatastringToList(dt);
                var avgVal = dataVals.Average(x => x.Value);
                var maxVal = dataVals.Max(x => x.Value);
                var minVal = dataVals.Min(x => x.Value);

                ws.GetRow(0).Cells[0].SetCellValue($"钢卷编号：{coilNo}-{desc}");
                ws.GetRow(1).Cells[0].SetCellValue($"列印时间：{timeRange}");
                ws.GetRow(2).Cells[1].SetCellValue($"{avgVal}");
                ws.GetRow(2).Cells[3].SetCellValue($"");
                ws.GetRow(3).Cells[1].SetCellValue($"{maxVal}");
                ws.GetRow(3).Cells[3].SetCellValue($"{minVal}");

                var i = 6;
                foreach (var dataVal in dataVals)
                {
                    ws.CreateRow(i);
                    ws.GetRow(i).CreateCell(0).SetCellValue(dataVal.Member);
                    ws.GetRow(i).CreateCell(1).SetCellValue(dataVal.Value);
                    i++;
                }

                wb.SetSheetName(wb.GetSheetIndex(ws.SheetName), name);
            }

            wb.RemoveSheetAt(wb.GetSheetIndex(wsSample.SheetName));

            return wb;
        }

        /// <summary>
        ///     將 DataTable 數據轉為 List
        /// </summary>
        private IList<DataVal> DatastringToList(DataTable dt)
        {
            var dataVals = new List<DataVal>();

            foreach (DataRow dr in dt.Rows)
            {
                dataVals.Add(new DataVal(dr[0].ToString(), float.Parse(dr[1].ToString())));
            }

            return dataVals;
        }

        /// <summary>
        ///     資料類別定義
        /// </summary>
        private class DataVal
        {
            public string Member { get; private set; }
            public float Value { get; private set; }

            public DataVal(string member, float value) => (Member, Value) = (member, value);
        }

        #endregion

        
    }
}
