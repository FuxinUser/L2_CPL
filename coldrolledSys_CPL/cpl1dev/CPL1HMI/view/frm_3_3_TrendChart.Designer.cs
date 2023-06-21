namespace CPL1HMI
{
    partial class frm_3_3_TrendChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.Pnl_Chart_PorTr = new System.Windows.Forms.Panel();
            this.Zgc_CoilChart = new ZedGraph.ZedGraphControl();
            this.Chart_PresetData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Btn_Search = new System.Windows.Forms.Button();
            this.Lbl_CoilID_Title = new System.Windows.Forms.Label();
            this.Cob_CoilID = new System.Windows.Forms.ComboBox();
            this.Lbl_FrmTitle = new System.Windows.Forms.Label();
            this.Pnl_Top_PorTr = new System.Windows.Forms.Panel();
            this.Btn_Search_New = new System.Windows.Forms.Button();
            this.Grb_Check = new System.Windows.Forms.GroupBox();
            this.Chk_TR_Tension_Set = new System.Windows.Forms.CheckBox();
            this.Chk_TR_Current_Actual = new System.Windows.Forms.CheckBox();
            this.Chk_TR_Tension_Actual = new System.Windows.Forms.CheckBox();
            this.Chk_POR_Current_Actual = new System.Windows.Forms.CheckBox();
            this.Chk_POR_Tension_Actual = new System.Windows.Forms.CheckBox();
            this.Chk_POR_Tension_Set = new System.Windows.Forms.CheckBox();
            this.Chk_LINE_Speed_Actual = new System.Windows.Forms.CheckBox();
            this.Tab_ChartControl = new System.Windows.Forms.TabControl();
            this.Tab_Chart_PorTrPage = new System.Windows.Forms.TabPage();
            this.Tab_Chart_WeldPage = new System.Windows.Forms.TabPage();
            this.Pnl_Chart_Weld = new System.Windows.Forms.Panel();
            this.Zgc_CoilChart_Weld = new ZedGraph.ZedGraphControl();
            this.Pnl_Top_Weld = new System.Windows.Forms.Panel();
            this.Btn_Search_Weld = new System.Windows.Forms.Button();
            this.Grb_Check_Weld = new System.Windows.Forms.GroupBox();
            this.Chk_WELD_Speed_Actual = new System.Windows.Forms.CheckBox();
            this.Chk_WELD_PlanishWheelForce_Actual = new System.Windows.Forms.CheckBox();
            this.Chk_WELD_Current_Actual_Front = new System.Windows.Forms.CheckBox();
            this.Chk_WELD_Current_Actual_Rear = new System.Windows.Forms.CheckBox();
            this.Chk_WELD_Temperture = new System.Windows.Forms.CheckBox();
            this.Lbl_CoilID_Weld_Title = new System.Windows.Forms.Label();
            this.Cob_CoilID_Weld = new System.Windows.Forms.ComboBox();
            this.Btn_Export_PorTr = new System.Windows.Forms.Button();
            this.Btn_Export_Weld = new System.Windows.Forms.Button();
            this.Pnl_Chart_PorTr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_PresetData)).BeginInit();
            this.Pnl_Top_PorTr.SuspendLayout();
            this.Grb_Check.SuspendLayout();
            this.Tab_ChartControl.SuspendLayout();
            this.Tab_Chart_PorTrPage.SuspendLayout();
            this.Tab_Chart_WeldPage.SuspendLayout();
            this.Pnl_Chart_Weld.SuspendLayout();
            this.Pnl_Top_Weld.SuspendLayout();
            this.Grb_Check_Weld.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Chart_PorTr
            // 
            this.Pnl_Chart_PorTr.BackColor = System.Drawing.Color.White;
            this.Pnl_Chart_PorTr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Chart_PorTr.Controls.Add(this.Zgc_CoilChart);
            this.Pnl_Chart_PorTr.Controls.Add(this.Chart_PresetData);
            this.Pnl_Chart_PorTr.Location = new System.Drawing.Point(6, 122);
            this.Pnl_Chart_PorTr.Name = "Pnl_Chart_PorTr";
            this.Pnl_Chart_PorTr.Size = new System.Drawing.Size(1876, 757);
            this.Pnl_Chart_PorTr.TabIndex = 406;
            // 
            // Zgc_CoilChart
            // 
            this.Zgc_CoilChart.IsAutoScrollRange = true;
            this.Zgc_CoilChart.IsEnableSelection = true;
            this.Zgc_CoilChart.IsShowPointValues = true;
            this.Zgc_CoilChart.IsSynchronizeXAxes = true;
            this.Zgc_CoilChart.Location = new System.Drawing.Point(3, 3);
            this.Zgc_CoilChart.Name = "Zgc_CoilChart";
            this.Zgc_CoilChart.PanModifierKeys = System.Windows.Forms.Keys.None;
            this.Zgc_CoilChart.ScrollGrace = 0D;
            this.Zgc_CoilChart.ScrollMaxX = 0D;
            this.Zgc_CoilChart.ScrollMaxY = 0D;
            this.Zgc_CoilChart.ScrollMaxY2 = 0D;
            this.Zgc_CoilChart.ScrollMinX = 0D;
            this.Zgc_CoilChart.ScrollMinY = 0D;
            this.Zgc_CoilChart.ScrollMinY2 = 0D;
            this.Zgc_CoilChart.Size = new System.Drawing.Size(1868, 749);
            this.Zgc_CoilChart.TabIndex = 1780;
            this.Zgc_CoilChart.UseExtendedPrintDialog = true;
            this.Zgc_CoilChart.PointValueEvent += new ZedGraph.ZedGraphControl.PointValueHandler(this.Zgc_CoilChart_PointValueEvent);
            this.Zgc_CoilChart.MouseLeave += new System.EventHandler(this.Zgc_CoilChart_MouseLeave);
            this.Zgc_CoilChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Zgc_CoilChart_MouseMove);
            // 
            // Chart_PresetData
            // 
            this.Chart_PresetData.BorderlineColor = System.Drawing.Color.Black;
            this.Chart_PresetData.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.Chart_PresetData.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Chart_PresetData.Legends.Add(legend1);
            this.Chart_PresetData.Location = new System.Drawing.Point(45, 162);
            this.Chart_PresetData.Name = "Chart_PresetData";
            this.Chart_PresetData.Size = new System.Drawing.Size(182, 156);
            this.Chart_PresetData.TabIndex = 30;
            this.Chart_PresetData.Text = "chart1";
            this.Chart_PresetData.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Chart_PresetData_MouseMove);
            // 
            // Btn_Search
            // 
            this.Btn_Search.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Search.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Search.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Search.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btn_Search.Location = new System.Drawing.Point(365, 72);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(72, 27);
            this.Btn_Search.TabIndex = 1122;
            this.Btn_Search.Text = "查询";
            this.Btn_Search.UseVisualStyleBackColor = false;
            this.Btn_Search.Visible = false;
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // Lbl_CoilID_Title
            // 
            this.Lbl_CoilID_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_CoilID_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_CoilID_Title.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Lbl_CoilID_Title.Location = new System.Drawing.Point(20, 38);
            this.Lbl_CoilID_Title.Name = "Lbl_CoilID_Title";
            this.Lbl_CoilID_Title.Size = new System.Drawing.Size(135, 32);
            this.Lbl_CoilID_Title.TabIndex = 1114;
            this.Lbl_CoilID_Title.Text = "出口钢卷号";
            this.Lbl_CoilID_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Cob_CoilID
            // 
            this.Cob_CoilID.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_CoilID.FormattingEnabled = true;
            this.Cob_CoilID.Location = new System.Drawing.Point(155, 38);
            this.Cob_CoilID.Margin = new System.Windows.Forms.Padding(4);
            this.Cob_CoilID.Name = "Cob_CoilID";
            this.Cob_CoilID.Size = new System.Drawing.Size(282, 32);
            this.Cob_CoilID.TabIndex = 1112;
            this.Cob_CoilID.Text = "HE2104282900";
            // 
            // Lbl_FrmTitle
            // 
            this.Lbl_FrmTitle.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            this.Lbl_FrmTitle.ForeColor = System.Drawing.Color.Cyan;
            this.Lbl_FrmTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Lbl_FrmTitle.Location = new System.Drawing.Point(0, 10);
            this.Lbl_FrmTitle.Name = "Lbl_FrmTitle";
            this.Lbl_FrmTitle.Size = new System.Drawing.Size(1920, 35);
            this.Lbl_FrmTitle.TabIndex = 404;
            this.Lbl_FrmTitle.Text = "3-3 趋势图";
            this.Lbl_FrmTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pnl_Top_PorTr
            // 
            this.Pnl_Top_PorTr.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Top_PorTr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Top_PorTr.Controls.Add(this.Btn_Export_PorTr);
            this.Pnl_Top_PorTr.Controls.Add(this.Btn_Search_New);
            this.Pnl_Top_PorTr.Controls.Add(this.Grb_Check);
            this.Pnl_Top_PorTr.Controls.Add(this.Btn_Search);
            this.Pnl_Top_PorTr.Controls.Add(this.Lbl_CoilID_Title);
            this.Pnl_Top_PorTr.Controls.Add(this.Cob_CoilID);
            this.Pnl_Top_PorTr.Location = new System.Drawing.Point(6, 6);
            this.Pnl_Top_PorTr.Name = "Pnl_Top_PorTr";
            this.Pnl_Top_PorTr.Size = new System.Drawing.Size(1876, 110);
            this.Pnl_Top_PorTr.TabIndex = 405;
            // 
            // Btn_Search_New
            // 
            this.Btn_Search_New.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Search_New.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Search_New.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Search_New.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Search_New.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btn_Search_New.Location = new System.Drawing.Point(457, 24);
            this.Btn_Search_New.Name = "Btn_Search_New";
            this.Btn_Search_New.Size = new System.Drawing.Size(150, 60);
            this.Btn_Search_New.TabIndex = 1124;
            this.Btn_Search_New.Text = "查询";
            this.Btn_Search_New.UseVisualStyleBackColor = false;
            this.Btn_Search_New.Click += new System.EventHandler(this.Btn_Search_New_Click);
            // 
            // Grb_Check
            // 
            this.Grb_Check.Controls.Add(this.Chk_TR_Tension_Set);
            this.Grb_Check.Controls.Add(this.Chk_TR_Current_Actual);
            this.Grb_Check.Controls.Add(this.Chk_TR_Tension_Actual);
            this.Grb_Check.Controls.Add(this.Chk_POR_Current_Actual);
            this.Grb_Check.Controls.Add(this.Chk_POR_Tension_Actual);
            this.Grb_Check.Controls.Add(this.Chk_POR_Tension_Set);
            this.Grb_Check.Controls.Add(this.Chk_LINE_Speed_Actual);
            this.Grb_Check.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Grb_Check.Location = new System.Drawing.Point(627, 3);
            this.Grb_Check.Name = "Grb_Check";
            this.Grb_Check.Size = new System.Drawing.Size(1057, 96);
            this.Grb_Check.TabIndex = 1123;
            this.Grb_Check.TabStop = false;
            this.Grb_Check.Text = "选项";
            // 
            // Chk_TR_Tension_Set
            // 
            this.Chk_TR_Tension_Set.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_TR_Tension_Set.Checked = true;
            this.Chk_TR_Tension_Set.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_TR_Tension_Set.Font = new System.Drawing.Font("微軟正黑體", 12.25F, System.Drawing.FontStyle.Bold);
            this.Chk_TR_Tension_Set.Location = new System.Drawing.Point(795, 55);
            this.Chk_TR_Tension_Set.Name = "Chk_TR_Tension_Set";
            this.Chk_TR_Tension_Set.Size = new System.Drawing.Size(240, 26);
            this.Chk_TR_Tension_Set.TabIndex = 24;
            this.Chk_TR_Tension_Set.Text = "收卷机张力设定";
            this.Chk_TR_Tension_Set.UseVisualStyleBackColor = false;
            // 
            // Chk_TR_Current_Actual
            // 
            this.Chk_TR_Current_Actual.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_TR_Current_Actual.Checked = true;
            this.Chk_TR_Current_Actual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_TR_Current_Actual.Font = new System.Drawing.Font("微軟正黑體", 12.25F, System.Drawing.FontStyle.Bold);
            this.Chk_TR_Current_Actual.Location = new System.Drawing.Point(535, 55);
            this.Chk_TR_Current_Actual.Name = "Chk_TR_Current_Actual";
            this.Chk_TR_Current_Actual.Size = new System.Drawing.Size(240, 26);
            this.Chk_TR_Current_Actual.TabIndex = 6;
            this.Chk_TR_Current_Actual.Text = "收卷机实际电流";
            this.Chk_TR_Current_Actual.UseVisualStyleBackColor = false;
            this.Chk_TR_Current_Actual.CheckedChanged += new System.EventHandler(this.TrendChart_CheckedChanged);
            // 
            // Chk_TR_Tension_Actual
            // 
            this.Chk_TR_Tension_Actual.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_TR_Tension_Actual.Checked = true;
            this.Chk_TR_Tension_Actual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_TR_Tension_Actual.Font = new System.Drawing.Font("微軟正黑體", 12.25F, System.Drawing.FontStyle.Bold);
            this.Chk_TR_Tension_Actual.Location = new System.Drawing.Point(275, 55);
            this.Chk_TR_Tension_Actual.Name = "Chk_TR_Tension_Actual";
            this.Chk_TR_Tension_Actual.Size = new System.Drawing.Size(240, 26);
            this.Chk_TR_Tension_Actual.TabIndex = 5;
            this.Chk_TR_Tension_Actual.Text = "收卷机实际张力";
            this.Chk_TR_Tension_Actual.UseVisualStyleBackColor = false;
            this.Chk_TR_Tension_Actual.CheckedChanged += new System.EventHandler(this.TrendChart_CheckedChanged);
            // 
            // Chk_POR_Current_Actual
            // 
            this.Chk_POR_Current_Actual.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_POR_Current_Actual.Checked = true;
            this.Chk_POR_Current_Actual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_POR_Current_Actual.Font = new System.Drawing.Font("微軟正黑體", 12.25F, System.Drawing.FontStyle.Bold);
            this.Chk_POR_Current_Actual.Location = new System.Drawing.Point(535, 24);
            this.Chk_POR_Current_Actual.Name = "Chk_POR_Current_Actual";
            this.Chk_POR_Current_Actual.Size = new System.Drawing.Size(240, 26);
            this.Chk_POR_Current_Actual.TabIndex = 3;
            this.Chk_POR_Current_Actual.Text = "开卷机实际电流";
            this.Chk_POR_Current_Actual.UseVisualStyleBackColor = false;
            this.Chk_POR_Current_Actual.CheckedChanged += new System.EventHandler(this.TrendChart_CheckedChanged);
            // 
            // Chk_POR_Tension_Actual
            // 
            this.Chk_POR_Tension_Actual.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_POR_Tension_Actual.Checked = true;
            this.Chk_POR_Tension_Actual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_POR_Tension_Actual.Font = new System.Drawing.Font("微軟正黑體", 12.25F, System.Drawing.FontStyle.Bold);
            this.Chk_POR_Tension_Actual.Location = new System.Drawing.Point(275, 24);
            this.Chk_POR_Tension_Actual.Name = "Chk_POR_Tension_Actual";
            this.Chk_POR_Tension_Actual.Size = new System.Drawing.Size(240, 26);
            this.Chk_POR_Tension_Actual.TabIndex = 2;
            this.Chk_POR_Tension_Actual.Text = "开卷机实际张力";
            this.Chk_POR_Tension_Actual.UseVisualStyleBackColor = false;
            this.Chk_POR_Tension_Actual.CheckedChanged += new System.EventHandler(this.TrendChart_CheckedChanged);
            // 
            // Chk_POR_Tension_Set
            // 
            this.Chk_POR_Tension_Set.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_POR_Tension_Set.Checked = true;
            this.Chk_POR_Tension_Set.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_POR_Tension_Set.Font = new System.Drawing.Font("微軟正黑體", 12.25F, System.Drawing.FontStyle.Bold);
            this.Chk_POR_Tension_Set.Location = new System.Drawing.Point(795, 24);
            this.Chk_POR_Tension_Set.Name = "Chk_POR_Tension_Set";
            this.Chk_POR_Tension_Set.Size = new System.Drawing.Size(240, 26);
            this.Chk_POR_Tension_Set.TabIndex = 1;
            this.Chk_POR_Tension_Set.Text = "开卷机张力设定";
            this.Chk_POR_Tension_Set.UseVisualStyleBackColor = false;
            this.Chk_POR_Tension_Set.CheckedChanged += new System.EventHandler(this.TrendChart_CheckedChanged);
            // 
            // Chk_LINE_Speed_Actual
            // 
            this.Chk_LINE_Speed_Actual.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_LINE_Speed_Actual.Checked = true;
            this.Chk_LINE_Speed_Actual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_LINE_Speed_Actual.Font = new System.Drawing.Font("微軟正黑體", 12.25F, System.Drawing.FontStyle.Bold);
            this.Chk_LINE_Speed_Actual.Location = new System.Drawing.Point(15, 24);
            this.Chk_LINE_Speed_Actual.Name = "Chk_LINE_Speed_Actual";
            this.Chk_LINE_Speed_Actual.Size = new System.Drawing.Size(240, 26);
            this.Chk_LINE_Speed_Actual.TabIndex = 0;
            this.Chk_LINE_Speed_Actual.Text = "实际产线速度";
            this.Chk_LINE_Speed_Actual.UseVisualStyleBackColor = false;
            this.Chk_LINE_Speed_Actual.CheckedChanged += new System.EventHandler(this.TrendChart_CheckedChanged);
            // 
            // Tab_ChartControl
            // 
            this.Tab_ChartControl.Controls.Add(this.Tab_Chart_PorTrPage);
            this.Tab_ChartControl.Controls.Add(this.Tab_Chart_WeldPage);
            this.Tab_ChartControl.Location = new System.Drawing.Point(12, 48);
            this.Tab_ChartControl.Name = "Tab_ChartControl";
            this.Tab_ChartControl.SelectedIndex = 0;
            this.Tab_ChartControl.Size = new System.Drawing.Size(1896, 922);
            this.Tab_ChartControl.TabIndex = 407;
            // 
            // Tab_Chart_PorTrPage
            // 
            this.Tab_Chart_PorTrPage.Controls.Add(this.Pnl_Top_PorTr);
            this.Tab_Chart_PorTrPage.Controls.Add(this.Pnl_Chart_PorTr);
            this.Tab_Chart_PorTrPage.Location = new System.Drawing.Point(4, 33);
            this.Tab_Chart_PorTrPage.Name = "Tab_Chart_PorTrPage";
            this.Tab_Chart_PorTrPage.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Chart_PorTrPage.Size = new System.Drawing.Size(1888, 885);
            this.Tab_Chart_PorTrPage.TabIndex = 0;
            this.Tab_Chart_PorTrPage.Text = "开卷机、收卷机资讯";
            this.Tab_Chart_PorTrPage.UseVisualStyleBackColor = true;
            // 
            // Tab_Chart_WeldPage
            // 
            this.Tab_Chart_WeldPage.Controls.Add(this.Pnl_Chart_Weld);
            this.Tab_Chart_WeldPage.Controls.Add(this.Pnl_Top_Weld);
            this.Tab_Chart_WeldPage.Location = new System.Drawing.Point(4, 33);
            this.Tab_Chart_WeldPage.Name = "Tab_Chart_WeldPage";
            this.Tab_Chart_WeldPage.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Chart_WeldPage.Size = new System.Drawing.Size(1888, 885);
            this.Tab_Chart_WeldPage.TabIndex = 1;
            this.Tab_Chart_WeldPage.Text = "焊机资讯";
            this.Tab_Chart_WeldPage.UseVisualStyleBackColor = true;
            // 
            // Pnl_Chart_Weld
            // 
            this.Pnl_Chart_Weld.BackColor = System.Drawing.Color.White;
            this.Pnl_Chart_Weld.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Chart_Weld.Controls.Add(this.Zgc_CoilChart_Weld);
            this.Pnl_Chart_Weld.Location = new System.Drawing.Point(6, 122);
            this.Pnl_Chart_Weld.Name = "Pnl_Chart_Weld";
            this.Pnl_Chart_Weld.Size = new System.Drawing.Size(1876, 757);
            this.Pnl_Chart_Weld.TabIndex = 407;
            // 
            // Zgc_CoilChart_Weld
            // 
            this.Zgc_CoilChart_Weld.IsAutoScrollRange = true;
            this.Zgc_CoilChart_Weld.IsEnableSelection = true;
            this.Zgc_CoilChart_Weld.IsShowPointValues = true;
            this.Zgc_CoilChart_Weld.IsSynchronizeXAxes = true;
            this.Zgc_CoilChart_Weld.Location = new System.Drawing.Point(3, 3);
            this.Zgc_CoilChart_Weld.Name = "Zgc_CoilChart_Weld";
            this.Zgc_CoilChart_Weld.PanModifierKeys = System.Windows.Forms.Keys.None;
            this.Zgc_CoilChart_Weld.ScrollGrace = 0D;
            this.Zgc_CoilChart_Weld.ScrollMaxX = 0D;
            this.Zgc_CoilChart_Weld.ScrollMaxY = 0D;
            this.Zgc_CoilChart_Weld.ScrollMaxY2 = 0D;
            this.Zgc_CoilChart_Weld.ScrollMinX = 0D;
            this.Zgc_CoilChart_Weld.ScrollMinY = 0D;
            this.Zgc_CoilChart_Weld.ScrollMinY2 = 0D;
            this.Zgc_CoilChart_Weld.Size = new System.Drawing.Size(1868, 749);
            this.Zgc_CoilChart_Weld.TabIndex = 1780;
            this.Zgc_CoilChart_Weld.UseExtendedPrintDialog = true;
            // 
            // Pnl_Top_Weld
            // 
            this.Pnl_Top_Weld.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Top_Weld.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Top_Weld.Controls.Add(this.Btn_Export_Weld);
            this.Pnl_Top_Weld.Controls.Add(this.Btn_Search_Weld);
            this.Pnl_Top_Weld.Controls.Add(this.Grb_Check_Weld);
            this.Pnl_Top_Weld.Controls.Add(this.Lbl_CoilID_Weld_Title);
            this.Pnl_Top_Weld.Controls.Add(this.Cob_CoilID_Weld);
            this.Pnl_Top_Weld.Location = new System.Drawing.Point(6, 6);
            this.Pnl_Top_Weld.Name = "Pnl_Top_Weld";
            this.Pnl_Top_Weld.Size = new System.Drawing.Size(1876, 110);
            this.Pnl_Top_Weld.TabIndex = 406;
            // 
            // Btn_Search_Weld
            // 
            this.Btn_Search_Weld.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Search_Weld.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Search_Weld.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Search_Weld.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Search_Weld.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btn_Search_Weld.Location = new System.Drawing.Point(457, 24);
            this.Btn_Search_Weld.Name = "Btn_Search_Weld";
            this.Btn_Search_Weld.Size = new System.Drawing.Size(150, 60);
            this.Btn_Search_Weld.TabIndex = 1124;
            this.Btn_Search_Weld.Text = "查询";
            this.Btn_Search_Weld.UseVisualStyleBackColor = false;
            this.Btn_Search_Weld.Click += new System.EventHandler(this.Btn_Search_Weld_Click);
            // 
            // Grb_Check_Weld
            // 
            this.Grb_Check_Weld.Controls.Add(this.Chk_WELD_Speed_Actual);
            this.Grb_Check_Weld.Controls.Add(this.Chk_WELD_PlanishWheelForce_Actual);
            this.Grb_Check_Weld.Controls.Add(this.Chk_WELD_Current_Actual_Front);
            this.Grb_Check_Weld.Controls.Add(this.Chk_WELD_Current_Actual_Rear);
            this.Grb_Check_Weld.Controls.Add(this.Chk_WELD_Temperture);
            this.Grb_Check_Weld.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Grb_Check_Weld.Location = new System.Drawing.Point(627, 3);
            this.Grb_Check_Weld.Name = "Grb_Check_Weld";
            this.Grb_Check_Weld.Size = new System.Drawing.Size(1057, 96);
            this.Grb_Check_Weld.TabIndex = 1123;
            this.Grb_Check_Weld.TabStop = false;
            this.Grb_Check_Weld.Text = "选项";
            // 
            // Chk_WELD_Speed_Actual
            // 
            this.Chk_WELD_Speed_Actual.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_WELD_Speed_Actual.Checked = true;
            this.Chk_WELD_Speed_Actual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_WELD_Speed_Actual.Font = new System.Drawing.Font("微軟正黑體", 12.25F, System.Drawing.FontStyle.Bold);
            this.Chk_WELD_Speed_Actual.Location = new System.Drawing.Point(15, 24);
            this.Chk_WELD_Speed_Actual.Name = "Chk_WELD_Speed_Actual";
            this.Chk_WELD_Speed_Actual.Size = new System.Drawing.Size(240, 26);
            this.Chk_WELD_Speed_Actual.TabIndex = 14;
            this.Chk_WELD_Speed_Actual.Text = "实际焊接速度";
            this.Chk_WELD_Speed_Actual.UseVisualStyleBackColor = false;
            // 
            // Chk_WELD_PlanishWheelForce_Actual
            // 
            this.Chk_WELD_PlanishWheelForce_Actual.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_WELD_PlanishWheelForce_Actual.Checked = true;
            this.Chk_WELD_PlanishWheelForce_Actual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_WELD_PlanishWheelForce_Actual.Font = new System.Drawing.Font("微軟正黑體", 12.25F, System.Drawing.FontStyle.Bold);
            this.Chk_WELD_PlanishWheelForce_Actual.Location = new System.Drawing.Point(15, 55);
            this.Chk_WELD_PlanishWheelForce_Actual.Name = "Chk_WELD_PlanishWheelForce_Actual";
            this.Chk_WELD_PlanishWheelForce_Actual.Size = new System.Drawing.Size(240, 26);
            this.Chk_WELD_PlanishWheelForce_Actual.TabIndex = 15;
            this.Chk_WELD_PlanishWheelForce_Actual.Text = "实际焊接平板輪力";
            this.Chk_WELD_PlanishWheelForce_Actual.UseVisualStyleBackColor = false;
            // 
            // Chk_WELD_Current_Actual_Front
            // 
            this.Chk_WELD_Current_Actual_Front.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_WELD_Current_Actual_Front.Checked = true;
            this.Chk_WELD_Current_Actual_Front.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_WELD_Current_Actual_Front.Font = new System.Drawing.Font("微軟正黑體", 12.25F, System.Drawing.FontStyle.Bold);
            this.Chk_WELD_Current_Actual_Front.Location = new System.Drawing.Point(275, 24);
            this.Chk_WELD_Current_Actual_Front.Name = "Chk_WELD_Current_Actual_Front";
            this.Chk_WELD_Current_Actual_Front.Size = new System.Drawing.Size(240, 26);
            this.Chk_WELD_Current_Actual_Front.TabIndex = 13;
            this.Chk_WELD_Current_Actual_Front.Text = "焊接实际电流(前端)";
            this.Chk_WELD_Current_Actual_Front.UseVisualStyleBackColor = false;
            this.Chk_WELD_Current_Actual_Front.TextChanged += new System.EventHandler(this.Fun_LanguageIsEn_Font12_10_Chk);
            // 
            // Chk_WELD_Current_Actual_Rear
            // 
            this.Chk_WELD_Current_Actual_Rear.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_WELD_Current_Actual_Rear.Checked = true;
            this.Chk_WELD_Current_Actual_Rear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_WELD_Current_Actual_Rear.Font = new System.Drawing.Font("微軟正黑體", 12.25F, System.Drawing.FontStyle.Bold);
            this.Chk_WELD_Current_Actual_Rear.Location = new System.Drawing.Point(275, 55);
            this.Chk_WELD_Current_Actual_Rear.Name = "Chk_WELD_Current_Actual_Rear";
            this.Chk_WELD_Current_Actual_Rear.Size = new System.Drawing.Size(240, 26);
            this.Chk_WELD_Current_Actual_Rear.TabIndex = 17;
            this.Chk_WELD_Current_Actual_Rear.Text = "焊接实际电流(后端)";
            this.Chk_WELD_Current_Actual_Rear.UseVisualStyleBackColor = false;
            this.Chk_WELD_Current_Actual_Rear.TextChanged += new System.EventHandler(this.Fun_LanguageIsEn_Font12_10_Chk);
            // 
            // Chk_WELD_Temperture
            // 
            this.Chk_WELD_Temperture.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_WELD_Temperture.Checked = true;
            this.Chk_WELD_Temperture.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_WELD_Temperture.Font = new System.Drawing.Font("微軟正黑體", 12.25F, System.Drawing.FontStyle.Bold);
            this.Chk_WELD_Temperture.Location = new System.Drawing.Point(535, 24);
            this.Chk_WELD_Temperture.Name = "Chk_WELD_Temperture";
            this.Chk_WELD_Temperture.Size = new System.Drawing.Size(240, 26);
            this.Chk_WELD_Temperture.TabIndex = 16;
            this.Chk_WELD_Temperture.Text = "焊接溫度";
            this.Chk_WELD_Temperture.UseVisualStyleBackColor = false;
            // 
            // Lbl_CoilID_Weld_Title
            // 
            this.Lbl_CoilID_Weld_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_CoilID_Weld_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_CoilID_Weld_Title.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Lbl_CoilID_Weld_Title.Location = new System.Drawing.Point(20, 38);
            this.Lbl_CoilID_Weld_Title.Name = "Lbl_CoilID_Weld_Title";
            this.Lbl_CoilID_Weld_Title.Size = new System.Drawing.Size(135, 32);
            this.Lbl_CoilID_Weld_Title.TabIndex = 1114;
            this.Lbl_CoilID_Weld_Title.Text = "出口钢卷号";
            this.Lbl_CoilID_Weld_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Cob_CoilID_Weld
            // 
            this.Cob_CoilID_Weld.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_CoilID_Weld.FormattingEnabled = true;
            this.Cob_CoilID_Weld.Location = new System.Drawing.Point(155, 38);
            this.Cob_CoilID_Weld.Margin = new System.Windows.Forms.Padding(4);
            this.Cob_CoilID_Weld.Name = "Cob_CoilID_Weld";
            this.Cob_CoilID_Weld.Size = new System.Drawing.Size(282, 32);
            this.Cob_CoilID_Weld.TabIndex = 1112;
            this.Cob_CoilID_Weld.Text = "HE2104282900";
            // 
            // Btn_Export_PorTr
            // 
            this.Btn_Export_PorTr.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Export_PorTr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Export_PorTr.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Export_PorTr.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Export_PorTr.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btn_Export_PorTr.Location = new System.Drawing.Point(1700, 23);
            this.Btn_Export_PorTr.Name = "Btn_Export_PorTr";
            this.Btn_Export_PorTr.Size = new System.Drawing.Size(150, 60);
            this.Btn_Export_PorTr.TabIndex = 1125;
            this.Btn_Export_PorTr.Text = "汇出";
            this.Btn_Export_PorTr.UseVisualStyleBackColor = false;
            this.Btn_Export_PorTr.Click += new System.EventHandler(this.Btn_Export_PorTr_Click);
            // 
            // Btn_Export_Weld
            // 
            this.Btn_Export_Weld.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Export_Weld.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Export_Weld.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Export_Weld.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Export_Weld.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Btn_Export_Weld.Location = new System.Drawing.Point(1706, 23);
            this.Btn_Export_Weld.Name = "Btn_Export_Weld";
            this.Btn_Export_Weld.Size = new System.Drawing.Size(150, 60);
            this.Btn_Export_Weld.TabIndex = 1126;
            this.Btn_Export_Weld.Text = "汇出";
            this.Btn_Export_Weld.UseVisualStyleBackColor = false;
            this.Btn_Export_Weld.Click += new System.EventHandler(this.Btn_Export_Weld_Click);
            // 
            // frm_3_3_TrendChart
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1920, 982);
            this.Controls.Add(this.Tab_ChartControl);
            this.Controls.Add(this.Lbl_FrmTitle);
            this.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "frm_3_3_TrendChart";
            this.Text = "frm_3_3_TrendChart";
            this.Load += new System.EventHandler(this.Frm_3_3_TrendChart_Load);
            this.Shown += new System.EventHandler(this.Frm_3_3_TrendChart_Shown);
            this.Pnl_Chart_PorTr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Chart_PresetData)).EndInit();
            this.Pnl_Top_PorTr.ResumeLayout(false);
            this.Grb_Check.ResumeLayout(false);
            this.Tab_ChartControl.ResumeLayout(false);
            this.Tab_Chart_PorTrPage.ResumeLayout(false);
            this.Tab_Chart_WeldPage.ResumeLayout(false);
            this.Pnl_Chart_Weld.ResumeLayout(false);
            this.Pnl_Top_Weld.ResumeLayout(false);
            this.Grb_Check_Weld.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Panel Pnl_Chart_PorTr;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_PresetData;
        internal System.Windows.Forms.Button Btn_Search;
        private System.Windows.Forms.Label Lbl_CoilID_Title;
        private System.Windows.Forms.ComboBox Cob_CoilID;
        internal System.Windows.Forms.Label Lbl_FrmTitle;
        internal System.Windows.Forms.Panel Pnl_Top_PorTr;
        private System.Windows.Forms.GroupBox Grb_Check;
        private System.Windows.Forms.CheckBox Chk_TR_Current_Actual;
        private System.Windows.Forms.CheckBox Chk_TR_Tension_Actual;
        private System.Windows.Forms.CheckBox Chk_POR_Current_Actual;
        private System.Windows.Forms.CheckBox Chk_POR_Tension_Actual;
        private System.Windows.Forms.CheckBox Chk_POR_Tension_Set;
        private System.Windows.Forms.CheckBox Chk_LINE_Speed_Actual;
        internal System.Windows.Forms.Button Btn_Search_New;
        private ZedGraph.ZedGraphControl Zgc_CoilChart;
        private System.Windows.Forms.CheckBox Chk_TR_Tension_Set;
        private System.Windows.Forms.TabControl Tab_ChartControl;
        private System.Windows.Forms.TabPage Tab_Chart_PorTrPage;
        private System.Windows.Forms.TabPage Tab_Chart_WeldPage;
        internal System.Windows.Forms.Panel Pnl_Top_Weld;
        internal System.Windows.Forms.Button Btn_Search_Weld;
        private System.Windows.Forms.GroupBox Grb_Check_Weld;
        private System.Windows.Forms.CheckBox Chk_WELD_Speed_Actual;
        private System.Windows.Forms.CheckBox Chk_WELD_PlanishWheelForce_Actual;
        private System.Windows.Forms.CheckBox Chk_WELD_Current_Actual_Front;
        private System.Windows.Forms.CheckBox Chk_WELD_Current_Actual_Rear;
        private System.Windows.Forms.CheckBox Chk_WELD_Temperture;
        private System.Windows.Forms.Label Lbl_CoilID_Weld_Title;
        private System.Windows.Forms.ComboBox Cob_CoilID_Weld;
        internal System.Windows.Forms.Panel Pnl_Chart_Weld;
        private ZedGraph.ZedGraphControl Zgc_CoilChart_Weld;
        internal System.Windows.Forms.Button Btn_Export_PorTr;
        internal System.Windows.Forms.Button Btn_Export_Weld;
    }
}