namespace CPL1HMI
{
    partial class Frm_2_1_Tracking
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
            if (disposing)
            {
                //DisposeShapeContainer(ShapeContainer1);

            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected void DisposeShapeContainer(Microsoft.VisualBasic.PowerPacks.ShapeContainer AShapeContainer)
        {
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Button Btn_ETOP_Leader;
            this.Menu_AckPDI = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AckPDIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Timer_TrackingMap = new System.Windows.Forms.Timer(this.components);
            this.Timer_ProcessData = new System.Windows.Forms.Timer(this.components);
            this.Pnl_NotUse_X = new System.Windows.Forms.Panel();
            this.Lbl_NotUse_X = new System.Windows.Forms.Label();
            this.Btn_DSK02_Weight_X = new System.Windows.Forms.Button();
            this.Lbl_ProcessCode_Title_X = new System.Windows.Forms.Label();
            this.Lbl_ProcessCode_X = new System.Windows.Forms.Label();
            this.Lbl_DTDisp_Title_X = new System.Windows.Forms.Label();
            this.Lbl_DT23Disp_X = new System.Windows.Forms.Label();
            this.Btn_Strip_X = new System.Windows.Forms.Button();
            this.Lbl_DT21Disp_X = new System.Windows.Forms.Label();
            this.Lbl_DT22Disp_X = new System.Windows.Forms.Label();
            this.Btn_PDO_X = new System.Windows.Forms.Button();
            this.Pnl_Weight_X = new System.Windows.Forms.Panel();
            this.Lbl_Weight_Title_X = new System.Windows.Forms.Label();
            this.Txt_Weight_X = new System.Windows.Forms.TextBox();
            this.Lbl_Weight_Coil_Title_X = new System.Windows.Forms.Label();
            this.Btn_Weight_Cancel_X = new System.Windows.Forms.Button();
            this.Btn_Weight_Save_X = new System.Windows.Forms.Button();
            this.Btn_Del_ETOP_X = new System.Windows.Forms.Button();
            this.Btn_ETOPEntry_X = new System.Windows.Forms.Button();
            this.Btn_DT23Abandon_X = new System.Windows.Forms.Button();
            this.Btn_DT23Reset_X = new System.Windows.Forms.Button();
            this.Tab_GridDataControl = new System.Windows.Forms.TabControl();
            this.Tab_SchedulePage = new System.Windows.Forms.TabPage();
            this.Dgv_OffLine = new System.Windows.Forms.DataGridView();
            this.Tab_OnlinePage = new System.Windows.Forms.TabPage();
            this.Dgv_OnLine = new System.Windows.Forms.DataGridView();
            this.Lbl_EntryMode_Title = new System.Windows.Forms.Label();
            this.Chk_DGV_Reflash = new System.Windows.Forms.CheckBox();
            this.Lbl_EntryMode = new System.Windows.Forms.Label();
            this.Btn_AutoCoilFeed = new System.Windows.Forms.Button();
            this.Lbl_TrackingTitle = new System.Windows.Forms.Label();
            this.Btn_Reflash = new System.Windows.Forms.Button();
            this.Pnl_ShowData_DTOP = new System.Windows.Forms.Panel();
            this.Btn_DTOP_CoilOut = new System.Windows.Forms.Button();
            this.Lbl_DTOP_Weight_Unit = new System.Windows.Forms.Label();
            this.Btn_DTOP_Del = new System.Windows.Forms.Button();
            this.Btn_DTOP_PrintLabel = new System.Windows.Forms.Button();
            this.Lbl_DTOP_CoilNo = new System.Windows.Forms.Label();
            this.Txt_DTOP_Weight = new System.Windows.Forms.TextBox();
            this.Lbl_DTOP_CoilNo_Title = new System.Windows.Forms.Label();
            this.Lbl_DTOP_Weight_Title = new System.Windows.Forms.Label();
            this.Lbl_DTOP_Skid_Title = new System.Windows.Forms.Label();
            this.Lbl_LineSpeed_Title = new System.Windows.Forms.Label();
            this.Lbl_LineSpeed = new System.Windows.Forms.Label();
            this.Lbl_LineSpeed_Unit = new System.Windows.Forms.Label();
            this.Lbl_EntryStatus_Title = new System.Windows.Forms.Label();
            this.Lbl_CPLStatus_Title = new System.Windows.Forms.Label();
            this.Lbl_ExitStatus_Title = new System.Windows.Forms.Label();
            this.Lbl_EntryStatus = new System.Windows.Forms.Label();
            this.Lbl_CPLStatus = new System.Windows.Forms.Label();
            this.Lbl_ExitStatus = new System.Windows.Forms.Label();
            this.Pnl_ShowData_POR = new System.Windows.Forms.Panel();
            this.Btn_PORPresetL1 = new System.Windows.Forms.Button();
            this.Lbl_POR_CoilNo = new System.Windows.Forms.Label();
            this.Btn_StripBreakModify = new System.Windows.Forms.Button();
            this.Lbl_POR_Skid_Title = new System.Windows.Forms.Label();
            this.Btn_StripBreak = new System.Windows.Forms.Button();
            this.Btn_POR_Reject = new System.Windows.Forms.Button();
            this.Lbl_POR_CoilNo_Title = new System.Windows.Forms.Label();
            this.Lbl_POR_Tension_Title = new System.Windows.Forms.Label();
            this.Lbl_POR_Tension = new System.Windows.Forms.Label();
            this.Lbl_POR_Current_Title = new System.Windows.Forms.Label();
            this.Lbl_POR_Current = new System.Windows.Forms.Label();
            this.Lbl_POR_Tension_Unit = new System.Windows.Forms.Label();
            this.Lbl_POR_Current_Unit = new System.Windows.Forms.Label();
            this.Pnl_ShowData_ESK01 = new System.Windows.Forms.Panel();
            this.Lbl_ESK01_CoilNo_Title = new System.Windows.Forms.Label();
            this.Lbl_ESK01_Skid_Title = new System.Windows.Forms.Label();
            this.Lbl_ESK01_CoilNo = new System.Windows.Forms.Label();
            this.Btn_ESK01_Leader = new System.Windows.Forms.Button();
            this.Btn_ESK01_Entry = new System.Windows.Forms.Button();
            this.Btn_ESK01_Del = new System.Windows.Forms.Button();
            this.Btn_ESK01Reject = new System.Windows.Forms.Button();
            this.Btn_ESK01_PrintLabel = new System.Windows.Forms.Button();
            this.Pnl_ShowData_ESK02 = new System.Windows.Forms.Panel();
            this.Lbl_ESK02_CoilNo_Title = new System.Windows.Forms.Label();
            this.Btn_ESK02_Leader = new System.Windows.Forms.Button();
            this.Lbl_ESK02_Skid_Title = new System.Windows.Forms.Label();
            this.Lbl_ESK02_CoilNo = new System.Windows.Forms.Label();
            this.Btn_ESK02_Entry = new System.Windows.Forms.Button();
            this.Btn_ESK02_PrintLabel = new System.Windows.Forms.Button();
            this.Btn_ESK02_Del = new System.Windows.Forms.Button();
            this.Btn_ESK02_Reject = new System.Windows.Forms.Button();
            this.Pnl_ShowData_ETOP = new System.Windows.Forms.Panel();
            this.Lbl_ETOP_CoilNo_Title = new System.Windows.Forms.Label();
            this.Btn_ETOP_PrintLabel = new System.Windows.Forms.Button();
            this.Lbl_ETOP_Skid_Title = new System.Windows.Forms.Label();
            this.Btn_ETOP_Del = new System.Windows.Forms.Button();
            this.Lbl_ETOP_CoilNo = new System.Windows.Forms.Label();
            this.Btn_ETOP_ManualFeed = new System.Windows.Forms.Button();
            this.Btn_ETOP_Reject = new System.Windows.Forms.Button();
            this.Pnl_ShowData_DSK01 = new System.Windows.Forms.Panel();
            this.Btn_DSK01_Del = new System.Windows.Forms.Button();
            this.Lbl_DSK01_Weight_Unit = new System.Windows.Forms.Label();
            this.Btn_DSK01_CoilOut = new System.Windows.Forms.Button();
            this.Lbl_DSK01_Weight_Title = new System.Windows.Forms.Label();
            this.Lbl_DSK01_CoilNo_Title = new System.Windows.Forms.Label();
            this.Lbl_DSK01_Skid_Title = new System.Windows.Forms.Label();
            this.Lbl_DSK01_CoilNo = new System.Windows.Forms.Label();
            this.Txt_DSK01_Weight = new System.Windows.Forms.TextBox();
            this.Btn_DSK01_PrintLabel = new System.Windows.Forms.Button();
            this.Pnl_ShowData_TR = new System.Windows.Forms.Panel();
            this.Lbl_TR_Skid_Title = new System.Windows.Forms.Label();
            this.Lbl_TR_CoilNo_Title = new System.Windows.Forms.Label();
            this.Lbl_TR_CoilNo = new System.Windows.Forms.Label();
            this.Lbl_TR_Tension = new System.Windows.Forms.Label();
            this.Lbl_TR_Current_Title = new System.Windows.Forms.Label();
            this.Lbl_TR_Tension_Title = new System.Windows.Forms.Label();
            this.Lbl_TR_Current = new System.Windows.Forms.Label();
            this.Lbl_TR_Current_Unit = new System.Windows.Forms.Label();
            this.Lbl_TR_Tension_Unit = new System.Windows.Forms.Label();
            this.Pnl_ShowData_DSK02 = new System.Windows.Forms.Panel();
            this.Btn_DSK02_Del = new System.Windows.Forms.Button();
            this.Lbl_DSK02_Weight_Unit = new System.Windows.Forms.Label();
            this.Btn_DSK02_CoilOut = new System.Windows.Forms.Button();
            this.Lbl_DSK02_Weight_Title = new System.Windows.Forms.Label();
            this.Lbl_DSK02_CoilNo_Title = new System.Windows.Forms.Label();
            this.Lbl_DSK02_Skid_Title = new System.Windows.Forms.Label();
            this.Lbl_DSK02_CoilNo = new System.Windows.Forms.Label();
            this.Btn_DSK02_PrintLabel = new System.Windows.Forms.Button();
            this.Txt_DSK02_Weight = new System.Windows.Forms.TextBox();
            this.Pnl_ShowData_ECar = new System.Windows.Forms.Panel();
            this.Lbl_ECar_CoilNo = new System.Windows.Forms.Label();
            this.Lbl_ECar_Skid_Title = new System.Windows.Forms.Label();
            this.Lbl_ECar_CoilNo_Title = new System.Windows.Forms.Label();
            this.Pnl_ShowData_DCar = new System.Windows.Forms.Panel();
            this.Lbl_DCar_CoilNo = new System.Windows.Forms.Label();
            this.Lbl_DCar_Skid_Title = new System.Windows.Forms.Label();
            this.Lbl_DCar_CoilNo_Title = new System.Windows.Forms.Label();
            this.Pic_Track_Picture = new System.Windows.Forms.PictureBox();
            this.Pnl_Scan = new System.Windows.Forms.Panel();
            this.Pnl_Scan_BackGround = new System.Windows.Forms.Panel();
            this.Btn_Scan_No = new System.Windows.Forms.Button();
            this.Btn_Scan_Yes = new System.Windows.Forms.Button();
            this.Lbl_Scan_SkidNo_Title = new System.Windows.Forms.Label();
            this.Lbl_Scan_Desc = new System.Windows.Forms.Label();
            this.Lbl_Scan_CoilNo_Title = new System.Windows.Forms.Label();
            this.Lbl_Scan_SkidNo = new System.Windows.Forms.Label();
            this.Pnl_NetworkStatus = new System.Windows.Forms.Panel();
            this.Lbl_PLC_Color = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Lbl_RevMMS_Color = new System.Windows.Forms.Label();
            this.Lbl_SendMMS_Color = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.Lbl_RevWMS_Color = new System.Windows.Forms.Label();
            this.Lbl_SendWMS_Color = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.Pic_Track_Picture_W_L = new System.Windows.Forms.PictureBox();
            this.Pnl_ShowAll = new System.Windows.Forms.Panel();
            this.Pnl_Spare = new System.Windows.Forms.Panel();
            this.Btn_Close_Spare = new System.Windows.Forms.Button();
            this.Txt_Spare = new System.Windows.Forms.TextBox();
            this.Lbl_ComboName_Spare = new System.Windows.Forms.Label();
            this.Pln_Por_SkidPdiData = new System.Windows.Forms.Panel();
            this.Lbl_Por_Title = new System.Windows.Forms.Label();
            this.Btn_Por_Paper = new System.Windows.Forms.Button();
            this.Lbl_Por_ESK02_Title = new System.Windows.Forms.Label();
            this.Lbl_Por_ESK01_Title = new System.Windows.Forms.Label();
            this.Lbl_Por_ESK02_Paper = new System.Windows.Forms.Label();
            this.Lbl_Por_Paper_Title = new System.Windows.Forms.Label();
            this.Lbl_Por_ESK01_Paper = new System.Windows.Forms.Label();
            this.Lbl_Por_ESK02_Dividing = new System.Windows.Forms.Label();
            this.Lbl_Por_Dividing_Title = new System.Windows.Forms.Label();
            this.Lbl_Por_ESK01_Dividing = new System.Windows.Forms.Label();
            this.Lbl_Por_ESK02_Trim = new System.Windows.Forms.Label();
            this.Lbl_Por_Trim_Title = new System.Windows.Forms.Label();
            this.Lbl_Por_ESK02_Leader = new System.Windows.Forms.Label();
            this.Lbl_Por_ESK01_Trim = new System.Windows.Forms.Label();
            this.Lbl_Por_ESK02_St_No = new System.Windows.Forms.Label();
            this.Lbl_Por_ESK01_Leader = new System.Windows.Forms.Label();
            this.Lbl_Por_ESK01_St_No = new System.Windows.Forms.Label();
            this.Lbl_Por_Leader_Title = new System.Windows.Forms.Label();
            this.Lbl_Por_St_Title = new System.Windows.Forms.Label();
            this.Grb_Dividing = new CPL1HMI.Tool.toolGroupBox(this.components);
            this.Lbl_Order_Weight_2_Unit = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_3_Unit = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_4_Unit = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_5_Unit = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_6_Unit = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_1_Unit = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_6 = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_5 = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_4 = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_6_Title = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_5_Title = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_4_Title = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_3 = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_2 = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_1 = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_3_Title = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_2_Title = new System.Windows.Forms.Label();
            this.Lbl_Order_Weight_1_Title = new System.Windows.Forms.Label();
            this.Lbl_Dividing_num1 = new System.Windows.Forms.Label();
            this.Lbl_Dividing_num_Title = new System.Windows.Forms.Label();
            this.Pic_Track_Picture_W_R = new System.Windows.Forms.PictureBox();
            this.Grb_Leader_Strip = new CPL1HMI.Tool.toolGroupBox(this.components);
            this.Lbl_Head_Thickness_Unit = new System.Windows.Forms.Label();
            this.Lbl_Head_Width_Unit = new System.Windows.Forms.Label();
            this.Lbl_Head_Length_Unit = new System.Windows.Forms.Label();
            this.Lbl_Leader_Strip_Tail_Title = new System.Windows.Forms.Label();
            this.Lbl_Leader_Strip_Head_Title = new System.Windows.Forms.Label();
            this.Lbl_Tail_Thickness = new System.Windows.Forms.Label();
            this.Lbl_Tail_Width = new System.Windows.Forms.Label();
            this.Lbl_Tail_Length = new System.Windows.Forms.Label();
            this.Lbl_Head_Thickness = new System.Windows.Forms.Label();
            this.Lbl_Head_Thickness_Title = new System.Windows.Forms.Label();
            this.Lbl_Head_Width = new System.Windows.Forms.Label();
            this.Lbl_Head_Width_Title = new System.Windows.Forms.Label();
            this.Lbl_Head_Length = new System.Windows.Forms.Label();
            this.Lbl_Head_Length_Title = new System.Windows.Forms.Label();
            this.Lbl_Tail_St_no = new System.Windows.Forms.Label();
            this.Lbl_Head_St_no = new System.Windows.Forms.Label();
            this.Lbl_Head_St_no_Title = new System.Windows.Forms.Label();
            this.Grb_Trim = new CPL1HMI.Tool.toolGroupBox(this.components);
            this.Lbl_WidthMax_Unit = new System.Windows.Forms.Label();
            this.Lbl_WidthMin_Unit = new System.Windows.Forms.Label();
            this.Lbl_OutWidth_Unit = new System.Windows.Forms.Label();
            this.Lbl_WidthMin = new System.Windows.Forms.Label();
            this.Lbl_OutWidth_Title = new System.Windows.Forms.Label();
            this.Lbl_WidthMin_Title = new System.Windows.Forms.Label();
            this.Lbl_OutWidth = new System.Windows.Forms.Label();
            this.Lbl_WidthMax = new System.Windows.Forms.Label();
            this.Lbl_WidthMax_Title = new System.Windows.Forms.Label();
            Btn_ETOP_Leader = new System.Windows.Forms.Button();
            this.Menu_AckPDI.SuspendLayout();
            this.Pnl_NotUse_X.SuspendLayout();
            this.Pnl_Weight_X.SuspendLayout();
            this.Tab_GridDataControl.SuspendLayout();
            this.Tab_SchedulePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_OffLine)).BeginInit();
            this.Tab_OnlinePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_OnLine)).BeginInit();
            this.Pnl_ShowData_DTOP.SuspendLayout();
            this.Pnl_ShowData_POR.SuspendLayout();
            this.Pnl_ShowData_ESK01.SuspendLayout();
            this.Pnl_ShowData_ESK02.SuspendLayout();
            this.Pnl_ShowData_ETOP.SuspendLayout();
            this.Pnl_ShowData_DSK01.SuspendLayout();
            this.Pnl_ShowData_TR.SuspendLayout();
            this.Pnl_ShowData_DSK02.SuspendLayout();
            this.Pnl_ShowData_ECar.SuspendLayout();
            this.Pnl_ShowData_DCar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Track_Picture)).BeginInit();
            this.Pnl_Scan.SuspendLayout();
            this.Pnl_Scan_BackGround.SuspendLayout();
            this.Pnl_NetworkStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Track_Picture_W_L)).BeginInit();
            this.Pnl_ShowAll.SuspendLayout();
            this.Pnl_Spare.SuspendLayout();
            this.Pln_Por_SkidPdiData.SuspendLayout();
            this.Grb_Dividing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Track_Picture_W_R)).BeginInit();
            this.Grb_Leader_Strip.SuspendLayout();
            this.Grb_Trim.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_ETOP_Leader
            // 
            Btn_ETOP_Leader.BackColor = System.Drawing.Color.Gold;
            Btn_ETOP_Leader.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            Btn_ETOP_Leader.Location = new System.Drawing.Point(139, 65);
            Btn_ETOP_Leader.Name = "Btn_ETOP_Leader";
            Btn_ETOP_Leader.Size = new System.Drawing.Size(34, 34);
            Btn_ETOP_Leader.TabIndex = 1480;
            Btn_ETOP_Leader.Text = "导";
            Btn_ETOP_Leader.UseVisualStyleBackColor = false;
            Btn_ETOP_Leader.Click += new System.EventHandler(this.Btn_ETOP_Leader_Click);
            // 
            // Menu_AckPDI
            // 
            this.Menu_AckPDI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AckPDIToolStripMenuItem});
            this.Menu_AckPDI.Name = "Menu";
            this.Menu_AckPDI.Size = new System.Drawing.Size(118, 26);
            // 
            // AckPDIToolStripMenuItem
            // 
            this.AckPDIToolStripMenuItem.Name = "AckPDIToolStripMenuItem";
            this.AckPDIToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.AckPDIToolStripMenuItem.Text = "请求PDI";
            this.AckPDIToolStripMenuItem.Click += new System.EventHandler(this.AckPDIToolStripMenuItem_Click);
            // 
            // Timer_TrackingMap
            // 
            this.Timer_TrackingMap.Interval = 5000;
            this.Timer_TrackingMap.Tick += new System.EventHandler(this.Timer_TrackingMap_Tick);
            // 
            // Timer_ProcessData
            // 
            this.Timer_ProcessData.Interval = 15000;
            this.Timer_ProcessData.Tick += new System.EventHandler(this.Timer_ProcessData_Tick);
            // 
            // Pnl_NotUse_X
            // 
            this.Pnl_NotUse_X.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Pnl_NotUse_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_NotUse_X.Controls.Add(this.Lbl_NotUse_X);
            this.Pnl_NotUse_X.Controls.Add(this.Btn_DSK02_Weight_X);
            this.Pnl_NotUse_X.Controls.Add(this.Lbl_ProcessCode_Title_X);
            this.Pnl_NotUse_X.Controls.Add(this.Lbl_ProcessCode_X);
            this.Pnl_NotUse_X.Controls.Add(this.Lbl_DTDisp_Title_X);
            this.Pnl_NotUse_X.Controls.Add(this.Lbl_DT23Disp_X);
            this.Pnl_NotUse_X.Controls.Add(this.Btn_Strip_X);
            this.Pnl_NotUse_X.Controls.Add(this.Lbl_DT21Disp_X);
            this.Pnl_NotUse_X.Controls.Add(this.Lbl_DT22Disp_X);
            this.Pnl_NotUse_X.Controls.Add(this.Btn_PDO_X);
            this.Pnl_NotUse_X.Controls.Add(this.Pnl_Weight_X);
            this.Pnl_NotUse_X.Controls.Add(this.Btn_Del_ETOP_X);
            this.Pnl_NotUse_X.Controls.Add(this.Btn_ETOPEntry_X);
            this.Pnl_NotUse_X.Controls.Add(this.Btn_DT23Abandon_X);
            this.Pnl_NotUse_X.Controls.Add(this.Btn_DT23Reset_X);
            this.Pnl_NotUse_X.Location = new System.Drawing.Point(1564, 3);
            this.Pnl_NotUse_X.Name = "Pnl_NotUse_X";
            this.Pnl_NotUse_X.Size = new System.Drawing.Size(103, 51);
            this.Pnl_NotUse_X.TabIndex = 1466;
            this.Pnl_NotUse_X.Visible = false;
            // 
            // Lbl_NotUse_X
            // 
            this.Lbl_NotUse_X.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Lbl_NotUse_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_NotUse_X.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_NotUse_X.ForeColor = System.Drawing.Color.Black;
            this.Lbl_NotUse_X.Location = new System.Drawing.Point(3, 3);
            this.Lbl_NotUse_X.Name = "Lbl_NotUse_X";
            this.Lbl_NotUse_X.Size = new System.Drawing.Size(90, 43);
            this.Lbl_NotUse_X.TabIndex = 1896;
            this.Lbl_NotUse_X.Text = "暂不使用\r\n的控制項";
            this.Lbl_NotUse_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_NotUse_X.Visible = false;
            // 
            // Btn_DSK02_Weight_X
            // 
            this.Btn_DSK02_Weight_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_DSK02_Weight_X.Location = new System.Drawing.Point(201, 65);
            this.Btn_DSK02_Weight_X.Name = "Btn_DSK02_Weight_X";
            this.Btn_DSK02_Weight_X.Size = new System.Drawing.Size(34, 33);
            this.Btn_DSK02_Weight_X.TabIndex = 1895;
            this.Btn_DSK02_Weight_X.Text = "称";
            this.Btn_DSK02_Weight_X.UseVisualStyleBackColor = true;
            this.Btn_DSK02_Weight_X.Visible = false;
            // 
            // Lbl_ProcessCode_Title_X
            // 
            this.Lbl_ProcessCode_Title_X.BackColor = System.Drawing.Color.DimGray;
            this.Lbl_ProcessCode_Title_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ProcessCode_Title_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_ProcessCode_Title_X.ForeColor = System.Drawing.Color.Yellow;
            this.Lbl_ProcessCode_Title_X.Location = new System.Drawing.Point(123, 27);
            this.Lbl_ProcessCode_Title_X.Name = "Lbl_ProcessCode_Title_X";
            this.Lbl_ProcessCode_Title_X.Size = new System.Drawing.Size(64, 33);
            this.Lbl_ProcessCode_Title_X.TabIndex = 1471;
            this.Lbl_ProcessCode_Title_X.Text = "工序";
            this.Lbl_ProcessCode_Title_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_ProcessCode_Title_X.Visible = false;
            // 
            // Lbl_ProcessCode_X
            // 
            this.Lbl_ProcessCode_X.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Lbl_ProcessCode_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ProcessCode_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_ProcessCode_X.ForeColor = System.Drawing.Color.Black;
            this.Lbl_ProcessCode_X.Location = new System.Drawing.Point(187, 27);
            this.Lbl_ProcessCode_X.Name = "Lbl_ProcessCode_X";
            this.Lbl_ProcessCode_X.Size = new System.Drawing.Size(230, 33);
            this.Lbl_ProcessCode_X.TabIndex = 1470;
            this.Lbl_ProcessCode_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_ProcessCode_X.Visible = false;
            // 
            // Lbl_DTDisp_Title_X
            // 
            this.Lbl_DTDisp_Title_X.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DTDisp_Title_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DTDisp_Title_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_DTDisp_Title_X.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_DTDisp_Title_X.Location = new System.Drawing.Point(3, 62);
            this.Lbl_DTDisp_Title_X.Name = "Lbl_DTDisp_Title_X";
            this.Lbl_DTDisp_Title_X.Size = new System.Drawing.Size(53, 29);
            this.Lbl_DTDisp_Title_X.TabIndex = 960;
            this.Lbl_DTDisp_Title_X.Text = "判定";
            this.Lbl_DTDisp_Title_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_DTDisp_Title_X.Visible = false;
            // 
            // Lbl_DT23Disp_X
            // 
            this.Lbl_DT23Disp_X.BackColor = System.Drawing.Color.White;
            this.Lbl_DT23Disp_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DT23Disp_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DT23Disp_X.Location = new System.Drawing.Point(3, 157);
            this.Lbl_DT23Disp_X.Name = "Lbl_DT23Disp_X";
            this.Lbl_DT23Disp_X.Size = new System.Drawing.Size(53, 33);
            this.Lbl_DT23Disp_X.TabIndex = 968;
            this.Lbl_DT23Disp_X.Text = " ";
            this.Lbl_DT23Disp_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_DT23Disp_X.Visible = false;
            // 
            // Btn_Strip_X
            // 
            this.Btn_Strip_X.BackColor = System.Drawing.Color.Gold;
            this.Btn_Strip_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_Strip_X.Location = new System.Drawing.Point(3, 211);
            this.Btn_Strip_X.Name = "Btn_Strip_X";
            this.Btn_Strip_X.Size = new System.Drawing.Size(112, 33);
            this.Btn_Strip_X.TabIndex = 1147;
            this.Btn_Strip_X.Text = "导带资料";
            this.Btn_Strip_X.UseVisualStyleBackColor = false;
            this.Btn_Strip_X.Visible = false;
            this.Btn_Strip_X.Click += new System.EventHandler(this.Btn_Strip_Click);
            // 
            // Lbl_DT21Disp_X
            // 
            this.Lbl_DT21Disp_X.BackColor = System.Drawing.Color.White;
            this.Lbl_DT21Disp_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DT21Disp_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DT21Disp_X.Location = new System.Drawing.Point(3, 91);
            this.Lbl_DT21Disp_X.Name = "Lbl_DT21Disp_X";
            this.Lbl_DT21Disp_X.Size = new System.Drawing.Size(53, 33);
            this.Lbl_DT21Disp_X.TabIndex = 964;
            this.Lbl_DT21Disp_X.Text = " ";
            this.Lbl_DT21Disp_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_DT21Disp_X.Visible = false;
            // 
            // Lbl_DT22Disp_X
            // 
            this.Lbl_DT22Disp_X.BackColor = System.Drawing.Color.White;
            this.Lbl_DT22Disp_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DT22Disp_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DT22Disp_X.Location = new System.Drawing.Point(3, 124);
            this.Lbl_DT22Disp_X.Name = "Lbl_DT22Disp_X";
            this.Lbl_DT22Disp_X.Size = new System.Drawing.Size(53, 33);
            this.Lbl_DT22Disp_X.TabIndex = 966;
            this.Lbl_DT22Disp_X.Text = " ";
            this.Lbl_DT22Disp_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_DT22Disp_X.Visible = false;
            // 
            // Btn_PDO_X
            // 
            this.Btn_PDO_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_PDO_X.Location = new System.Drawing.Point(67, 64);
            this.Btn_PDO_X.Name = "Btn_PDO_X";
            this.Btn_PDO_X.Size = new System.Drawing.Size(100, 35);
            this.Btn_PDO_X.TabIndex = 1342;
            this.Btn_PDO_X.Text = "PDO确认";
            this.Btn_PDO_X.UseVisualStyleBackColor = true;
            this.Btn_PDO_X.Visible = false;
            this.Btn_PDO_X.Click += new System.EventHandler(this.Btn_PDO_Click);
            // 
            // Pnl_Weight_X
            // 
            this.Pnl_Weight_X.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Pnl_Weight_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Weight_X.Controls.Add(this.Lbl_Weight_Title_X);
            this.Pnl_Weight_X.Controls.Add(this.Txt_Weight_X);
            this.Pnl_Weight_X.Controls.Add(this.Lbl_Weight_Coil_Title_X);
            this.Pnl_Weight_X.Controls.Add(this.Btn_Weight_Cancel_X);
            this.Pnl_Weight_X.Controls.Add(this.Btn_Weight_Save_X);
            this.Pnl_Weight_X.Location = new System.Drawing.Point(62, 116);
            this.Pnl_Weight_X.Name = "Pnl_Weight_X";
            this.Pnl_Weight_X.Size = new System.Drawing.Size(401, 89);
            this.Pnl_Weight_X.TabIndex = 1424;
            this.Pnl_Weight_X.Visible = false;
            // 
            // Lbl_Weight_Title_X
            // 
            this.Lbl_Weight_Title_X.AutoSize = true;
            this.Lbl_Weight_Title_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Weight_Title_X.Location = new System.Drawing.Point(3, 47);
            this.Lbl_Weight_Title_X.Name = "Lbl_Weight_Title_X";
            this.Lbl_Weight_Title_X.Size = new System.Drawing.Size(100, 24);
            this.Lbl_Weight_Title_X.TabIndex = 5;
            this.Lbl_Weight_Title_X.Text = "重量(kg) : ";
            // 
            // Txt_Weight_X
            // 
            this.Txt_Weight_X.Location = new System.Drawing.Point(103, 44);
            this.Txt_Weight_X.Name = "Txt_Weight_X";
            this.Txt_Weight_X.Size = new System.Drawing.Size(143, 30);
            this.Txt_Weight_X.TabIndex = 4;
            this.Txt_Weight_X.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtWt_KeyPress);
            // 
            // Lbl_Weight_Coil_Title_X
            // 
            this.Lbl_Weight_Coil_Title_X.AutoSize = true;
            this.Lbl_Weight_Coil_Title_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Weight_Coil_Title_X.Location = new System.Drawing.Point(3, 10);
            this.Lbl_Weight_Coil_Title_X.Name = "Lbl_Weight_Coil_Title_X";
            this.Lbl_Weight_Coil_Title_X.Size = new System.Drawing.Size(101, 24);
            this.Lbl_Weight_Coil_Title_X.TabIndex = 3;
            this.Lbl_Weight_Coil_Title_X.Text = "钢卷编号 : ";
            // 
            // Btn_Weight_Cancel_X
            // 
            this.Btn_Weight_Cancel_X.Location = new System.Drawing.Point(324, 46);
            this.Btn_Weight_Cancel_X.Name = "Btn_Weight_Cancel_X";
            this.Btn_Weight_Cancel_X.Size = new System.Drawing.Size(59, 28);
            this.Btn_Weight_Cancel_X.TabIndex = 2;
            this.Btn_Weight_Cancel_X.Text = "取消";
            this.Btn_Weight_Cancel_X.UseVisualStyleBackColor = true;
            this.Btn_Weight_Cancel_X.Click += new System.EventHandler(this.Btn_WtCancel_Click);
            // 
            // Btn_Weight_Save_X
            // 
            this.Btn_Weight_Save_X.Location = new System.Drawing.Point(259, 46);
            this.Btn_Weight_Save_X.Name = "Btn_Weight_Save_X";
            this.Btn_Weight_Save_X.Size = new System.Drawing.Size(59, 28);
            this.Btn_Weight_Save_X.TabIndex = 1;
            this.Btn_Weight_Save_X.Text = "确认";
            this.Btn_Weight_Save_X.UseVisualStyleBackColor = true;
            this.Btn_Weight_Save_X.Click += new System.EventHandler(this.Btn_Wt_Save_Click);
            // 
            // Btn_Del_ETOP_X
            // 
            this.Btn_Del_ETOP_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_Del_ETOP_X.Location = new System.Drawing.Point(167, 64);
            this.Btn_Del_ETOP_X.Name = "Btn_Del_ETOP_X";
            this.Btn_Del_ETOP_X.Size = new System.Drawing.Size(34, 34);
            this.Btn_Del_ETOP_X.TabIndex = 1149;
            this.Btn_Del_ETOP_X.Text = "刪";
            this.Btn_Del_ETOP_X.UseVisualStyleBackColor = true;
            this.Btn_Del_ETOP_X.Visible = false;
            // 
            // Btn_ETOPEntry_X
            // 
            this.Btn_ETOPEntry_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ETOPEntry_X.Location = new System.Drawing.Point(235, 64);
            this.Btn_ETOPEntry_X.Name = "Btn_ETOPEntry_X";
            this.Btn_ETOPEntry_X.Size = new System.Drawing.Size(34, 34);
            this.Btn_ETOPEntry_X.TabIndex = 1147;
            this.Btn_ETOPEntry_X.Text = "入";
            this.Btn_ETOPEntry_X.UseVisualStyleBackColor = true;
            this.Btn_ETOPEntry_X.Visible = false;
            // 
            // Btn_DT23Abandon_X
            // 
            this.Btn_DT23Abandon_X.BackColor = System.Drawing.Color.Transparent;
            this.Btn_DT23Abandon_X.Enabled = false;
            this.Btn_DT23Abandon_X.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_DT23Abandon_X.ForeColor = System.Drawing.Color.Black;
            this.Btn_DT23Abandon_X.Location = new System.Drawing.Point(269, 64);
            this.Btn_DT23Abandon_X.Name = "Btn_DT23Abandon_X";
            this.Btn_DT23Abandon_X.Size = new System.Drawing.Size(34, 34);
            this.Btn_DT23Abandon_X.TabIndex = 1340;
            this.Btn_DT23Abandon_X.Text = "廢";
            this.Btn_DT23Abandon_X.UseVisualStyleBackColor = false;
            this.Btn_DT23Abandon_X.Visible = false;
            // 
            // Btn_DT23Reset_X
            // 
            this.Btn_DT23Reset_X.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_DT23Reset_X.Location = new System.Drawing.Point(303, 64);
            this.Btn_DT23Reset_X.Name = "Btn_DT23Reset_X";
            this.Btn_DT23Reset_X.Size = new System.Drawing.Size(34, 34);
            this.Btn_DT23Reset_X.TabIndex = 1337;
            this.Btn_DT23Reset_X.Text = "刪";
            this.Btn_DT23Reset_X.UseVisualStyleBackColor = true;
            this.Btn_DT23Reset_X.Visible = false;
            // 
            // Tab_GridDataControl
            // 
            this.Tab_GridDataControl.Controls.Add(this.Tab_SchedulePage);
            this.Tab_GridDataControl.Controls.Add(this.Tab_OnlinePage);
            this.Tab_GridDataControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.Tab_GridDataControl.Location = new System.Drawing.Point(5, 711);
            this.Tab_GridDataControl.Name = "Tab_GridDataControl";
            this.Tab_GridDataControl.SelectedIndex = 0;
            this.Tab_GridDataControl.Size = new System.Drawing.Size(1910, 269);
            this.Tab_GridDataControl.TabIndex = 1426;
            this.Tab_GridDataControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Tab_GridDataControl_DrawItem);
            // 
            // Tab_SchedulePage
            // 
            this.Tab_SchedulePage.Controls.Add(this.Dgv_OffLine);
            this.Tab_SchedulePage.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Tab_SchedulePage.Location = new System.Drawing.Point(4, 29);
            this.Tab_SchedulePage.Name = "Tab_SchedulePage";
            this.Tab_SchedulePage.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_SchedulePage.Size = new System.Drawing.Size(1902, 236);
            this.Tab_SchedulePage.TabIndex = 0;
            this.Tab_SchedulePage.Text = "排程钢卷资讯";
            this.Tab_SchedulePage.UseVisualStyleBackColor = true;
            // 
            // Dgv_OffLine
            // 
            this.Dgv_OffLine.AllowUserToAddRows = false;
            this.Dgv_OffLine.AllowUserToDeleteRows = false;
            this.Dgv_OffLine.AllowUserToResizeColumns = false;
            this.Dgv_OffLine.AllowUserToResizeRows = false;
            this.Dgv_OffLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_OffLine.Location = new System.Drawing.Point(0, 0);
            this.Dgv_OffLine.Name = "Dgv_OffLine";
            this.Dgv_OffLine.ReadOnly = true;
            this.Dgv_OffLine.RowHeadersVisible = false;
            this.Dgv_OffLine.RowTemplate.Height = 24;
            this.Dgv_OffLine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_OffLine.Size = new System.Drawing.Size(1902, 240);
            this.Dgv_OffLine.TabIndex = 1354;
            this.Dgv_OffLine.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.Dgv_off_CellPainting);
            // 
            // Tab_OnlinePage
            // 
            this.Tab_OnlinePage.Controls.Add(this.Dgv_OnLine);
            this.Tab_OnlinePage.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Tab_OnlinePage.Location = new System.Drawing.Point(4, 22);
            this.Tab_OnlinePage.Name = "Tab_OnlinePage";
            this.Tab_OnlinePage.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_OnlinePage.Size = new System.Drawing.Size(1902, 243);
            this.Tab_OnlinePage.TabIndex = 1;
            this.Tab_OnlinePage.Text = "线上钢卷资讯";
            this.Tab_OnlinePage.UseVisualStyleBackColor = true;
            // 
            // Dgv_OnLine
            // 
            this.Dgv_OnLine.AllowUserToAddRows = false;
            this.Dgv_OnLine.AllowUserToDeleteRows = false;
            this.Dgv_OnLine.AllowUserToResizeColumns = false;
            this.Dgv_OnLine.AllowUserToResizeRows = false;
            this.Dgv_OnLine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_OnLine.Location = new System.Drawing.Point(0, 0);
            this.Dgv_OnLine.Name = "Dgv_OnLine";
            this.Dgv_OnLine.ReadOnly = true;
            this.Dgv_OnLine.RowHeadersVisible = false;
            this.Dgv_OnLine.RowTemplate.Height = 24;
            this.Dgv_OnLine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_OnLine.Size = new System.Drawing.Size(1902, 238);
            this.Dgv_OnLine.TabIndex = 1355;
            // 
            // Lbl_EntryMode_Title
            // 
            this.Lbl_EntryMode_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_EntryMode_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_EntryMode_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_EntryMode_Title.Location = new System.Drawing.Point(20, 146);
            this.Lbl_EntryMode_Title.Name = "Lbl_EntryMode_Title";
            this.Lbl_EntryMode_Title.Size = new System.Drawing.Size(140, 33);
            this.Lbl_EntryMode_Title.TabIndex = 1427;
            this.Lbl_EntryMode_Title.Text = "入料模式";
            this.Lbl_EntryMode_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Chk_DGV_Reflash
            // 
            this.Chk_DGV_Reflash.AutoSize = true;
            this.Chk_DGV_Reflash.BackColor = System.Drawing.Color.Yellow;
            this.Chk_DGV_Reflash.Location = new System.Drawing.Point(16, 686);
            this.Chk_DGV_Reflash.Name = "Chk_DGV_Reflash";
            this.Chk_DGV_Reflash.Size = new System.Drawing.Size(128, 23);
            this.Chk_DGV_Reflash.TabIndex = 1469;
            this.Chk_DGV_Reflash.Text = "資料表刷新";
            this.Chk_DGV_Reflash.UseVisualStyleBackColor = false;
            // 
            // Lbl_EntryMode
            // 
            this.Lbl_EntryMode.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_EntryMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_EntryMode.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_EntryMode.Location = new System.Drawing.Point(160, 146);
            this.Lbl_EntryMode.Name = "Lbl_EntryMode";
            this.Lbl_EntryMode.Size = new System.Drawing.Size(110, 33);
            this.Lbl_EntryMode.TabIndex = 1428;
            this.Lbl_EntryMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_AutoCoilFeed
            // 
            this.Btn_AutoCoilFeed.BackColor = System.Drawing.Color.Gold;
            this.Btn_AutoCoilFeed.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_AutoCoilFeed.Location = new System.Drawing.Point(278, 146);
            this.Btn_AutoCoilFeed.Name = "Btn_AutoCoilFeed";
            this.Btn_AutoCoilFeed.Size = new System.Drawing.Size(89, 33);
            this.Btn_AutoCoilFeed.TabIndex = 1429;
            this.Btn_AutoCoilFeed.Text = "变更";
            this.Btn_AutoCoilFeed.UseVisualStyleBackColor = false;
            this.Btn_AutoCoilFeed.Click += new System.EventHandler(this.Btn_AutoCoilFeed_Click);
            // 
            // Lbl_TrackingTitle
            // 
            this.Lbl_TrackingTitle.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_TrackingTitle.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_TrackingTitle.ForeColor = System.Drawing.Color.Cyan;
            this.Lbl_TrackingTitle.Location = new System.Drawing.Point(710, 10);
            this.Lbl_TrackingTitle.Name = "Lbl_TrackingTitle";
            this.Lbl_TrackingTitle.Size = new System.Drawing.Size(500, 35);
            this.Lbl_TrackingTitle.TabIndex = 1755;
            this.Lbl_TrackingTitle.Text = "2-1 钢卷追踪";
            this.Lbl_TrackingTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_Reflash
            // 
            this.Btn_Reflash.BackColor = System.Drawing.Color.Gold;
            this.Btn_Reflash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Reflash.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_Reflash.Location = new System.Drawing.Point(905, 294);
            this.Btn_Reflash.Name = "Btn_Reflash";
            this.Btn_Reflash.Size = new System.Drawing.Size(110, 32);
            this.Btn_Reflash.TabIndex = 1880;
            this.Btn_Reflash.Text = "刷新画面";
            this.Btn_Reflash.UseVisualStyleBackColor = false;
            this.Btn_Reflash.Click += new System.EventHandler(this.Btn_Reflash_Click);
            // 
            // Pnl_ShowData_DTOP
            // 
            this.Pnl_ShowData_DTOP.BackColor = System.Drawing.Color.Transparent;
            this.Pnl_ShowData_DTOP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_ShowData_DTOP.Controls.Add(this.Btn_DTOP_CoilOut);
            this.Pnl_ShowData_DTOP.Controls.Add(this.Lbl_DTOP_Weight_Unit);
            this.Pnl_ShowData_DTOP.Controls.Add(this.Btn_DTOP_Del);
            this.Pnl_ShowData_DTOP.Controls.Add(this.Btn_DTOP_PrintLabel);
            this.Pnl_ShowData_DTOP.Controls.Add(this.Lbl_DTOP_CoilNo);
            this.Pnl_ShowData_DTOP.Controls.Add(this.Txt_DTOP_Weight);
            this.Pnl_ShowData_DTOP.Controls.Add(this.Lbl_DTOP_CoilNo_Title);
            this.Pnl_ShowData_DTOP.Controls.Add(this.Lbl_DTOP_Weight_Title);
            this.Pnl_ShowData_DTOP.Controls.Add(this.Lbl_DTOP_Skid_Title);
            this.Pnl_ShowData_DTOP.Location = new System.Drawing.Point(1521, 562);
            this.Pnl_ShowData_DTOP.Name = "Pnl_ShowData_DTOP";
            this.Pnl_ShowData_DTOP.Size = new System.Drawing.Size(387, 101);
            this.Pnl_ShowData_DTOP.TabIndex = 1136;
            // 
            // Btn_DTOP_CoilOut
            // 
            this.Btn_DTOP_CoilOut.BackColor = System.Drawing.Color.Gold;
            this.Btn_DTOP_CoilOut.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_DTOP_CoilOut.Location = new System.Drawing.Point(350, 66);
            this.Btn_DTOP_CoilOut.Name = "Btn_DTOP_CoilOut";
            this.Btn_DTOP_CoilOut.Size = new System.Drawing.Size(34, 34);
            this.Btn_DTOP_CoilOut.TabIndex = 1882;
            this.Btn_DTOP_CoilOut.Text = "出";
            this.Btn_DTOP_CoilOut.UseVisualStyleBackColor = false;
            this.Btn_DTOP_CoilOut.Click += new System.EventHandler(this.Btn_DTOP_CoilOut_Click);
            // 
            // Lbl_DTOP_Weight_Unit
            // 
            this.Lbl_DTOP_Weight_Unit.AutoSize = true;
            this.Lbl_DTOP_Weight_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DTOP_Weight_Unit.Location = new System.Drawing.Point(226, 69);
            this.Lbl_DTOP_Weight_Unit.Name = "Lbl_DTOP_Weight_Unit";
            this.Lbl_DTOP_Weight_Unit.Size = new System.Drawing.Size(34, 24);
            this.Lbl_DTOP_Weight_Unit.TabIndex = 1881;
            this.Lbl_DTOP_Weight_Unit.Text = "Kg";
            // 
            // Btn_DTOP_Del
            // 
            this.Btn_DTOP_Del.BackColor = System.Drawing.Color.Gold;
            this.Btn_DTOP_Del.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_DTOP_Del.Location = new System.Drawing.Point(282, 66);
            this.Btn_DTOP_Del.Name = "Btn_DTOP_Del";
            this.Btn_DTOP_Del.Size = new System.Drawing.Size(34, 34);
            this.Btn_DTOP_Del.TabIndex = 1473;
            this.Btn_DTOP_Del.Text = "删";
            this.Btn_DTOP_Del.UseVisualStyleBackColor = false;
            this.Btn_DTOP_Del.Click += new System.EventHandler(this.Btn_DTOP_Del_Click);
            // 
            // Btn_DTOP_PrintLabel
            // 
            this.Btn_DTOP_PrintLabel.BackColor = System.Drawing.Color.Gold;
            this.Btn_DTOP_PrintLabel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_DTOP_PrintLabel.Location = new System.Drawing.Point(316, 66);
            this.Btn_DTOP_PrintLabel.Name = "Btn_DTOP_PrintLabel";
            this.Btn_DTOP_PrintLabel.Size = new System.Drawing.Size(34, 34);
            this.Btn_DTOP_PrintLabel.TabIndex = 1066;
            this.Btn_DTOP_PrintLabel.Text = "签";
            this.Btn_DTOP_PrintLabel.UseVisualStyleBackColor = false;
            this.Btn_DTOP_PrintLabel.Click += new System.EventHandler(this.BtnDT23PrintLabel_Click);
            // 
            // Lbl_DTOP_CoilNo
            // 
            this.Lbl_DTOP_CoilNo.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DTOP_CoilNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DTOP_CoilNo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DTOP_CoilNo.Location = new System.Drawing.Point(153, 32);
            this.Lbl_DTOP_CoilNo.Name = "Lbl_DTOP_CoilNo";
            this.Lbl_DTOP_CoilNo.Size = new System.Drawing.Size(230, 33);
            this.Lbl_DTOP_CoilNo.TabIndex = 683;
            this.Lbl_DTOP_CoilNo.Text = " ";
            this.Lbl_DTOP_CoilNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_DTOP_CoilNo.TextChanged += new System.EventHandler(this.Fun_ExitColorChange);
            // 
            // Txt_DTOP_Weight
            // 
            this.Txt_DTOP_Weight.BackColor = System.Drawing.Color.Silver;
            this.Txt_DTOP_Weight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_DTOP_Weight.Cursor = System.Windows.Forms.Cursors.Default;
            this.Txt_DTOP_Weight.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_DTOP_Weight.Location = new System.Drawing.Point(153, 65);
            this.Txt_DTOP_Weight.MaxLength = 5;
            this.Txt_DTOP_Weight.Name = "Txt_DTOP_Weight";
            this.Txt_DTOP_Weight.ReadOnly = true;
            this.Txt_DTOP_Weight.Size = new System.Drawing.Size(73, 33);
            this.Txt_DTOP_Weight.TabIndex = 1065;
            this.Txt_DTOP_Weight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Lbl_DTOP_CoilNo_Title
            // 
            this.Lbl_DTOP_CoilNo_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DTOP_CoilNo_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DTOP_CoilNo_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_DTOP_CoilNo_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_DTOP_CoilNo_Title.Location = new System.Drawing.Point(3, 32);
            this.Lbl_DTOP_CoilNo_Title.Name = "Lbl_DTOP_CoilNo_Title";
            this.Lbl_DTOP_CoilNo_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_DTOP_CoilNo_Title.TabIndex = 970;
            this.Lbl_DTOP_CoilNo_Title.Text = "钢卷编号";
            this.Lbl_DTOP_CoilNo_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_DTOP_Weight_Title
            // 
            this.Lbl_DTOP_Weight_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DTOP_Weight_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DTOP_Weight_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_DTOP_Weight_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_DTOP_Weight_Title.Location = new System.Drawing.Point(3, 65);
            this.Lbl_DTOP_Weight_Title.Name = "Lbl_DTOP_Weight_Title";
            this.Lbl_DTOP_Weight_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_DTOP_Weight_Title.TabIndex = 959;
            this.Lbl_DTOP_Weight_Title.Text = "重量";
            this.Lbl_DTOP_Weight_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_DTOP_Skid_Title
            // 
            this.Lbl_DTOP_Skid_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DTOP_Skid_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DTOP_Skid_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_DTOP_Skid_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_DTOP_Skid_Title.Location = new System.Drawing.Point(3, 3);
            this.Lbl_DTOP_Skid_Title.Name = "Lbl_DTOP_Skid_Title";
            this.Lbl_DTOP_Skid_Title.Size = new System.Drawing.Size(380, 29);
            this.Lbl_DTOP_Skid_Title.TabIndex = 682;
            this.Lbl_DTOP_Skid_Title.Text = "Skid3";
            this.Lbl_DTOP_Skid_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_LineSpeed_Title
            // 
            this.Lbl_LineSpeed_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_LineSpeed_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_LineSpeed_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_LineSpeed_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_LineSpeed_Title.Location = new System.Drawing.Point(797, 55);
            this.Lbl_LineSpeed_Title.Name = "Lbl_LineSpeed_Title";
            this.Lbl_LineSpeed_Title.Size = new System.Drawing.Size(128, 29);
            this.Lbl_LineSpeed_Title.TabIndex = 1386;
            this.Lbl_LineSpeed_Title.Text = "产线速度";
            this.Lbl_LineSpeed_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_LineSpeed
            // 
            this.Lbl_LineSpeed.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_LineSpeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_LineSpeed.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_LineSpeed.Location = new System.Drawing.Point(925, 55);
            this.Lbl_LineSpeed.Name = "Lbl_LineSpeed";
            this.Lbl_LineSpeed.Size = new System.Drawing.Size(117, 29);
            this.Lbl_LineSpeed.TabIndex = 1387;
            this.Lbl_LineSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_LineSpeed_Unit
            // 
            this.Lbl_LineSpeed_Unit.AutoSize = true;
            this.Lbl_LineSpeed_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_LineSpeed_Unit.Location = new System.Drawing.Point(1042, 57);
            this.Lbl_LineSpeed_Unit.Name = "Lbl_LineSpeed_Unit";
            this.Lbl_LineSpeed_Unit.Size = new System.Drawing.Size(71, 24);
            this.Lbl_LineSpeed_Unit.TabIndex = 1875;
            this.Lbl_LineSpeed_Unit.Text = "m/min";
            // 
            // Lbl_EntryStatus_Title
            // 
            this.Lbl_EntryStatus_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_EntryStatus_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_EntryStatus_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_EntryStatus_Title.Location = new System.Drawing.Point(20, 53);
            this.Lbl_EntryStatus_Title.Name = "Lbl_EntryStatus_Title";
            this.Lbl_EntryStatus_Title.Size = new System.Drawing.Size(140, 33);
            this.Lbl_EntryStatus_Title.TabIndex = 1881;
            this.Lbl_EntryStatus_Title.Text = "入口端状态";
            this.Lbl_EntryStatus_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_CPLStatus_Title
            // 
            this.Lbl_CPLStatus_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_CPLStatus_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_CPLStatus_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_CPLStatus_Title.Location = new System.Drawing.Point(20, 86);
            this.Lbl_CPLStatus_Title.Name = "Lbl_CPLStatus_Title";
            this.Lbl_CPLStatus_Title.Size = new System.Drawing.Size(140, 33);
            this.Lbl_CPLStatus_Title.TabIndex = 1882;
            this.Lbl_CPLStatus_Title.Text = "产线状态";
            this.Lbl_CPLStatus_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_ExitStatus_Title
            // 
            this.Lbl_ExitStatus_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ExitStatus_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ExitStatus_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_ExitStatus_Title.Location = new System.Drawing.Point(1712, 70);
            this.Lbl_ExitStatus_Title.Name = "Lbl_ExitStatus_Title";
            this.Lbl_ExitStatus_Title.Size = new System.Drawing.Size(140, 33);
            this.Lbl_ExitStatus_Title.TabIndex = 1883;
            this.Lbl_ExitStatus_Title.Text = "出口端状态";
            this.Lbl_ExitStatus_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_EntryStatus
            // 
            this.Lbl_EntryStatus.BackColor = System.Drawing.Color.Green;
            this.Lbl_EntryStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_EntryStatus.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_EntryStatus.Location = new System.Drawing.Point(160, 53);
            this.Lbl_EntryStatus.Name = "Lbl_EntryStatus";
            this.Lbl_EntryStatus.Size = new System.Drawing.Size(52, 33);
            this.Lbl_EntryStatus.TabIndex = 1884;
            this.Lbl_EntryStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_CPLStatus
            // 
            this.Lbl_CPLStatus.BackColor = System.Drawing.Color.Green;
            this.Lbl_CPLStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_CPLStatus.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_CPLStatus.Location = new System.Drawing.Point(160, 86);
            this.Lbl_CPLStatus.Name = "Lbl_CPLStatus";
            this.Lbl_CPLStatus.Size = new System.Drawing.Size(52, 33);
            this.Lbl_CPLStatus.TabIndex = 1885;
            this.Lbl_CPLStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_ExitStatus
            // 
            this.Lbl_ExitStatus.BackColor = System.Drawing.Color.Green;
            this.Lbl_ExitStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ExitStatus.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_ExitStatus.Location = new System.Drawing.Point(1852, 70);
            this.Lbl_ExitStatus.Name = "Lbl_ExitStatus";
            this.Lbl_ExitStatus.Size = new System.Drawing.Size(52, 33);
            this.Lbl_ExitStatus.TabIndex = 1886;
            this.Lbl_ExitStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pnl_ShowData_POR
            // 
            this.Pnl_ShowData_POR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_ShowData_POR.Controls.Add(this.Btn_PORPresetL1);
            this.Pnl_ShowData_POR.Controls.Add(this.Lbl_POR_CoilNo);
            this.Pnl_ShowData_POR.Controls.Add(this.Btn_StripBreakModify);
            this.Pnl_ShowData_POR.Controls.Add(this.Lbl_POR_Skid_Title);
            this.Pnl_ShowData_POR.Controls.Add(this.Btn_StripBreak);
            this.Pnl_ShowData_POR.Controls.Add(this.Btn_POR_Reject);
            this.Pnl_ShowData_POR.Controls.Add(this.Lbl_POR_CoilNo_Title);
            this.Pnl_ShowData_POR.Controls.Add(this.Lbl_POR_Tension_Title);
            this.Pnl_ShowData_POR.Controls.Add(this.Lbl_POR_Tension);
            this.Pnl_ShowData_POR.Controls.Add(this.Lbl_POR_Current_Title);
            this.Pnl_ShowData_POR.Controls.Add(this.Lbl_POR_Current);
            this.Pnl_ShowData_POR.Controls.Add(this.Lbl_POR_Tension_Unit);
            this.Pnl_ShowData_POR.Controls.Add(this.Lbl_POR_Current_Unit);
            this.Pnl_ShowData_POR.Location = new System.Drawing.Point(20, 192);
            this.Pnl_ShowData_POR.Name = "Pnl_ShowData_POR";
            this.Pnl_ShowData_POR.Size = new System.Drawing.Size(387, 168);
            this.Pnl_ShowData_POR.TabIndex = 1887;
            // 
            // Btn_PORPresetL1
            // 
            this.Btn_PORPresetL1.BackColor = System.Drawing.Color.Gold;
            this.Btn_PORPresetL1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_PORPresetL1.Location = new System.Drawing.Point(206, 132);
            this.Btn_PORPresetL1.Name = "Btn_PORPresetL1";
            this.Btn_PORPresetL1.Size = new System.Drawing.Size(176, 33);
            this.Btn_PORPresetL1.TabIndex = 1895;
            this.Btn_PORPresetL1.Text = "下抛钢卷生产参数";
            this.Btn_PORPresetL1.UseVisualStyleBackColor = false;
            this.Btn_PORPresetL1.Click += new System.EventHandler(this.Btn_PORPresetL1_Click);
            // 
            // Lbl_POR_CoilNo
            // 
            this.Lbl_POR_CoilNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Lbl_POR_CoilNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_POR_CoilNo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_POR_CoilNo.ForeColor = System.Drawing.Color.Black;
            this.Lbl_POR_CoilNo.Location = new System.Drawing.Point(153, 32);
            this.Lbl_POR_CoilNo.Name = "Lbl_POR_CoilNo";
            this.Lbl_POR_CoilNo.Size = new System.Drawing.Size(230, 33);
            this.Lbl_POR_CoilNo.TabIndex = 1344;
            this.Lbl_POR_CoilNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_POR_CoilNo.TextChanged += new System.EventHandler(this.Lbl_POR_Coil_ID_TextChanged);
            // 
            // Btn_StripBreakModify
            // 
            this.Btn_StripBreakModify.BackColor = System.Drawing.Color.Gold;
            this.Btn_StripBreakModify.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_StripBreakModify.Location = new System.Drawing.Point(3, 132);
            this.Btn_StripBreakModify.Name = "Btn_StripBreakModify";
            this.Btn_StripBreakModify.Size = new System.Drawing.Size(195, 33);
            this.Btn_StripBreakModify.TabIndex = 1878;
            this.Btn_StripBreakModify.Text = "断带修改POR卷号";
            this.Btn_StripBreakModify.UseVisualStyleBackColor = false;
            this.Btn_StripBreakModify.Click += new System.EventHandler(this.Btn_StripBreakModify_Click);
            // 
            // Lbl_POR_Skid_Title
            // 
            this.Lbl_POR_Skid_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_POR_Skid_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_POR_Skid_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_POR_Skid_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_POR_Skid_Title.Location = new System.Drawing.Point(3, 3);
            this.Lbl_POR_Skid_Title.Name = "Lbl_POR_Skid_Title";
            this.Lbl_POR_Skid_Title.Size = new System.Drawing.Size(380, 29);
            this.Lbl_POR_Skid_Title.TabIndex = 1343;
            this.Lbl_POR_Skid_Title.Text = "POR";
            this.Lbl_POR_Skid_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_StripBreak
            // 
            this.Btn_StripBreak.BackColor = System.Drawing.Color.Gold;
            this.Btn_StripBreak.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_StripBreak.Location = new System.Drawing.Point(308, 3);
            this.Btn_StripBreak.Name = "Btn_StripBreak";
            this.Btn_StripBreak.Size = new System.Drawing.Size(35, 33);
            this.Btn_StripBreak.TabIndex = 1481;
            this.Btn_StripBreak.Text = "断带";
            this.Btn_StripBreak.UseVisualStyleBackColor = false;
            this.Btn_StripBreak.Visible = false;
            this.Btn_StripBreak.Click += new System.EventHandler(this.Btn_StripBreak_Click);
            // 
            // Btn_POR_Reject
            // 
            this.Btn_POR_Reject.BackColor = System.Drawing.Color.Gold;
            this.Btn_POR_Reject.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_POR_Reject.Location = new System.Drawing.Point(349, 3);
            this.Btn_POR_Reject.Name = "Btn_POR_Reject";
            this.Btn_POR_Reject.Size = new System.Drawing.Size(34, 34);
            this.Btn_POR_Reject.TabIndex = 1148;
            this.Btn_POR_Reject.Text = "删";
            this.Btn_POR_Reject.UseVisualStyleBackColor = false;
            this.Btn_POR_Reject.Visible = false;
            this.Btn_POR_Reject.Click += new System.EventHandler(this.Btn_PORReject_Click);
            // 
            // Lbl_POR_CoilNo_Title
            // 
            this.Lbl_POR_CoilNo_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_POR_CoilNo_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_POR_CoilNo_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_POR_CoilNo_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_POR_CoilNo_Title.Location = new System.Drawing.Point(3, 32);
            this.Lbl_POR_CoilNo_Title.Name = "Lbl_POR_CoilNo_Title";
            this.Lbl_POR_CoilNo_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_POR_CoilNo_Title.TabIndex = 1346;
            this.Lbl_POR_CoilNo_Title.Text = "钢卷编号";
            this.Lbl_POR_CoilNo_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_POR_Tension_Title
            // 
            this.Lbl_POR_Tension_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_POR_Tension_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_POR_Tension_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_POR_Tension_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_POR_Tension_Title.Location = new System.Drawing.Point(3, 65);
            this.Lbl_POR_Tension_Title.Name = "Lbl_POR_Tension_Title";
            this.Lbl_POR_Tension_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_POR_Tension_Title.TabIndex = 1388;
            this.Lbl_POR_Tension_Title.Text = "张力(实际/设定)";
            this.Lbl_POR_Tension_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_POR_Tension
            // 
            this.Lbl_POR_Tension.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_POR_Tension.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_POR_Tension.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_POR_Tension.Location = new System.Drawing.Point(153, 65);
            this.Lbl_POR_Tension.Name = "Lbl_POR_Tension";
            this.Lbl_POR_Tension.Size = new System.Drawing.Size(190, 33);
            this.Lbl_POR_Tension.TabIndex = 1389;
            this.Lbl_POR_Tension.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_POR_Current_Title
            // 
            this.Lbl_POR_Current_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_POR_Current_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_POR_Current_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_POR_Current_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_POR_Current_Title.Location = new System.Drawing.Point(3, 98);
            this.Lbl_POR_Current_Title.Name = "Lbl_POR_Current_Title";
            this.Lbl_POR_Current_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_POR_Current_Title.TabIndex = 1390;
            this.Lbl_POR_Current_Title.Text = "电流";
            this.Lbl_POR_Current_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_POR_Current
            // 
            this.Lbl_POR_Current.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_POR_Current.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_POR_Current.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_POR_Current.Location = new System.Drawing.Point(153, 98);
            this.Lbl_POR_Current.Name = "Lbl_POR_Current";
            this.Lbl_POR_Current.Size = new System.Drawing.Size(190, 33);
            this.Lbl_POR_Current.TabIndex = 1391;
            this.Lbl_POR_Current.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_POR_Tension_Unit
            // 
            this.Lbl_POR_Tension_Unit.AutoSize = true;
            this.Lbl_POR_Tension_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_POR_Tension_Unit.Location = new System.Drawing.Point(343, 69);
            this.Lbl_POR_Tension_Unit.Name = "Lbl_POR_Tension_Unit";
            this.Lbl_POR_Tension_Unit.Size = new System.Drawing.Size(37, 24);
            this.Lbl_POR_Tension_Unit.TabIndex = 1876;
            this.Lbl_POR_Tension_Unit.Text = "kN";
            // 
            // Lbl_POR_Current_Unit
            // 
            this.Lbl_POR_Current_Unit.AutoSize = true;
            this.Lbl_POR_Current_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_POR_Current_Unit.Location = new System.Drawing.Point(343, 102);
            this.Lbl_POR_Current_Unit.Name = "Lbl_POR_Current_Unit";
            this.Lbl_POR_Current_Unit.Size = new System.Drawing.Size(24, 24);
            this.Lbl_POR_Current_Unit.TabIndex = 1877;
            this.Lbl_POR_Current_Unit.Text = "A";
            // 
            // Pnl_ShowData_ESK01
            // 
            this.Pnl_ShowData_ESK01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_ShowData_ESK01.Controls.Add(this.Lbl_ESK01_CoilNo_Title);
            this.Pnl_ShowData_ESK01.Controls.Add(this.Lbl_ESK01_Skid_Title);
            this.Pnl_ShowData_ESK01.Controls.Add(this.Lbl_ESK01_CoilNo);
            this.Pnl_ShowData_ESK01.Controls.Add(this.Btn_ESK01_Leader);
            this.Pnl_ShowData_ESK01.Controls.Add(this.Btn_ESK01_Entry);
            this.Pnl_ShowData_ESK01.Controls.Add(this.Btn_ESK01_Del);
            this.Pnl_ShowData_ESK01.Controls.Add(this.Btn_ESK01Reject);
            this.Pnl_ShowData_ESK01.Controls.Add(this.Btn_ESK01_PrintLabel);
            this.Pnl_ShowData_ESK01.Location = new System.Drawing.Point(20, 360);
            this.Pnl_ShowData_ESK01.Name = "Pnl_ShowData_ESK01";
            this.Pnl_ShowData_ESK01.Size = new System.Drawing.Size(387, 101);
            this.Pnl_ShowData_ESK01.TabIndex = 1888;
            // 
            // Lbl_ESK01_CoilNo_Title
            // 
            this.Lbl_ESK01_CoilNo_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ESK01_CoilNo_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ESK01_CoilNo_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_ESK01_CoilNo_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_ESK01_CoilNo_Title.Location = new System.Drawing.Point(3, 32);
            this.Lbl_ESK01_CoilNo_Title.Name = "Lbl_ESK01_CoilNo_Title";
            this.Lbl_ESK01_CoilNo_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_ESK01_CoilNo_Title.TabIndex = 1479;
            this.Lbl_ESK01_CoilNo_Title.Text = "钢卷编号";
            this.Lbl_ESK01_CoilNo_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_ESK01_Skid_Title
            // 
            this.Lbl_ESK01_Skid_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ESK01_Skid_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ESK01_Skid_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ESK01_Skid_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_ESK01_Skid_Title.Location = new System.Drawing.Point(3, 3);
            this.Lbl_ESK01_Skid_Title.Name = "Lbl_ESK01_Skid_Title";
            this.Lbl_ESK01_Skid_Title.Size = new System.Drawing.Size(380, 29);
            this.Lbl_ESK01_Skid_Title.TabIndex = 1080;
            this.Lbl_ESK01_Skid_Title.Text = "Skid1";
            this.Lbl_ESK01_Skid_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_ESK01_CoilNo
            // 
            this.Lbl_ESK01_CoilNo.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ESK01_CoilNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ESK01_CoilNo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_ESK01_CoilNo.Location = new System.Drawing.Point(153, 32);
            this.Lbl_ESK01_CoilNo.Name = "Lbl_ESK01_CoilNo";
            this.Lbl_ESK01_CoilNo.Size = new System.Drawing.Size(230, 33);
            this.Lbl_ESK01_CoilNo.TabIndex = 1081;
            this.Lbl_ESK01_CoilNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_ESK01_CoilNo.TextChanged += new System.EventHandler(this.Fun_EntryColorChange);
            // 
            // Btn_ESK01_Leader
            // 
            this.Btn_ESK01_Leader.BackColor = System.Drawing.Color.Gold;
            this.Btn_ESK01_Leader.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ESK01_Leader.Location = new System.Drawing.Point(139, 65);
            this.Btn_ESK01_Leader.Name = "Btn_ESK01_Leader";
            this.Btn_ESK01_Leader.Size = new System.Drawing.Size(34, 34);
            this.Btn_ESK01_Leader.TabIndex = 1478;
            this.Btn_ESK01_Leader.Text = "导";
            this.Btn_ESK01_Leader.UseVisualStyleBackColor = false;
            this.Btn_ESK01_Leader.Click += new System.EventHandler(this.Btn_ESK01_Leader_Click);
            // 
            // Btn_ESK01_Entry
            // 
            this.Btn_ESK01_Entry.BackColor = System.Drawing.Color.Gold;
            this.Btn_ESK01_Entry.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ESK01_Entry.Location = new System.Drawing.Point(3, 65);
            this.Btn_ESK01_Entry.Name = "Btn_ESK01_Entry";
            this.Btn_ESK01_Entry.Size = new System.Drawing.Size(34, 34);
            this.Btn_ESK01_Entry.TabIndex = 1145;
            this.Btn_ESK01_Entry.Text = "入";
            this.Btn_ESK01_Entry.UseVisualStyleBackColor = false;
            this.Btn_ESK01_Entry.Click += new System.EventHandler(this.Btn_ESK01Entry_Click);
            // 
            // Btn_ESK01_Del
            // 
            this.Btn_ESK01_Del.BackColor = System.Drawing.Color.Gold;
            this.Btn_ESK01_Del.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ESK01_Del.Location = new System.Drawing.Point(71, 65);
            this.Btn_ESK01_Del.Name = "Btn_ESK01_Del";
            this.Btn_ESK01_Del.Size = new System.Drawing.Size(34, 34);
            this.Btn_ESK01_Del.TabIndex = 1472;
            this.Btn_ESK01_Del.Text = "删";
            this.Btn_ESK01_Del.UseVisualStyleBackColor = false;
            this.Btn_ESK01_Del.Click += new System.EventHandler(this.Btn_ESK01_Del_Click);
            // 
            // Btn_ESK01Reject
            // 
            this.Btn_ESK01Reject.BackColor = System.Drawing.Color.Gold;
            this.Btn_ESK01Reject.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ESK01Reject.Location = new System.Drawing.Point(37, 65);
            this.Btn_ESK01Reject.Name = "Btn_ESK01Reject";
            this.Btn_ESK01Reject.Size = new System.Drawing.Size(34, 34);
            this.Btn_ESK01Reject.TabIndex = 1144;
            this.Btn_ESK01Reject.Text = "退";
            this.Btn_ESK01Reject.UseVisualStyleBackColor = false;
            this.Btn_ESK01Reject.Click += new System.EventHandler(this.Btn_ESK01Reject_Click);
            // 
            // Btn_ESK01_PrintLabel
            // 
            this.Btn_ESK01_PrintLabel.BackColor = System.Drawing.Color.Gold;
            this.Btn_ESK01_PrintLabel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ESK01_PrintLabel.Location = new System.Drawing.Point(105, 65);
            this.Btn_ESK01_PrintLabel.Name = "Btn_ESK01_PrintLabel";
            this.Btn_ESK01_PrintLabel.Size = new System.Drawing.Size(34, 34);
            this.Btn_ESK01_PrintLabel.TabIndex = 1474;
            this.Btn_ESK01_PrintLabel.Text = "签";
            this.Btn_ESK01_PrintLabel.UseVisualStyleBackColor = false;
            this.Btn_ESK01_PrintLabel.Click += new System.EventHandler(this.BtnESK01_PrintLabel_Click);
            // 
            // Pnl_ShowData_ESK02
            // 
            this.Pnl_ShowData_ESK02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_ShowData_ESK02.Controls.Add(this.Lbl_ESK02_CoilNo_Title);
            this.Pnl_ShowData_ESK02.Controls.Add(this.Btn_ESK02_Leader);
            this.Pnl_ShowData_ESK02.Controls.Add(this.Lbl_ESK02_Skid_Title);
            this.Pnl_ShowData_ESK02.Controls.Add(this.Lbl_ESK02_CoilNo);
            this.Pnl_ShowData_ESK02.Controls.Add(this.Btn_ESK02_Entry);
            this.Pnl_ShowData_ESK02.Controls.Add(this.Btn_ESK02_PrintLabel);
            this.Pnl_ShowData_ESK02.Controls.Add(this.Btn_ESK02_Del);
            this.Pnl_ShowData_ESK02.Controls.Add(this.Btn_ESK02_Reject);
            this.Pnl_ShowData_ESK02.Location = new System.Drawing.Point(20, 461);
            this.Pnl_ShowData_ESK02.Name = "Pnl_ShowData_ESK02";
            this.Pnl_ShowData_ESK02.Size = new System.Drawing.Size(387, 101);
            this.Pnl_ShowData_ESK02.TabIndex = 1889;
            // 
            // Lbl_ESK02_CoilNo_Title
            // 
            this.Lbl_ESK02_CoilNo_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ESK02_CoilNo_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ESK02_CoilNo_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_ESK02_CoilNo_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_ESK02_CoilNo_Title.Location = new System.Drawing.Point(3, 32);
            this.Lbl_ESK02_CoilNo_Title.Name = "Lbl_ESK02_CoilNo_Title";
            this.Lbl_ESK02_CoilNo_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_ESK02_CoilNo_Title.TabIndex = 1479;
            this.Lbl_ESK02_CoilNo_Title.Text = "钢卷编号";
            this.Lbl_ESK02_CoilNo_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Btn_ESK02_Leader
            // 
            this.Btn_ESK02_Leader.BackColor = System.Drawing.Color.Gold;
            this.Btn_ESK02_Leader.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ESK02_Leader.Location = new System.Drawing.Point(139, 65);
            this.Btn_ESK02_Leader.Name = "Btn_ESK02_Leader";
            this.Btn_ESK02_Leader.Size = new System.Drawing.Size(34, 34);
            this.Btn_ESK02_Leader.TabIndex = 1479;
            this.Btn_ESK02_Leader.Text = "导";
            this.Btn_ESK02_Leader.UseVisualStyleBackColor = false;
            this.Btn_ESK02_Leader.Click += new System.EventHandler(this.Btn_ESK02_Leader_Click);
            // 
            // Lbl_ESK02_Skid_Title
            // 
            this.Lbl_ESK02_Skid_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ESK02_Skid_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ESK02_Skid_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ESK02_Skid_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_ESK02_Skid_Title.Location = new System.Drawing.Point(3, 3);
            this.Lbl_ESK02_Skid_Title.Name = "Lbl_ESK02_Skid_Title";
            this.Lbl_ESK02_Skid_Title.Size = new System.Drawing.Size(380, 29);
            this.Lbl_ESK02_Skid_Title.TabIndex = 1082;
            this.Lbl_ESK02_Skid_Title.Text = "Skid2";
            this.Lbl_ESK02_Skid_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_ESK02_CoilNo
            // 
            this.Lbl_ESK02_CoilNo.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ESK02_CoilNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ESK02_CoilNo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_ESK02_CoilNo.Location = new System.Drawing.Point(153, 32);
            this.Lbl_ESK02_CoilNo.Name = "Lbl_ESK02_CoilNo";
            this.Lbl_ESK02_CoilNo.Size = new System.Drawing.Size(230, 33);
            this.Lbl_ESK02_CoilNo.TabIndex = 1083;
            this.Lbl_ESK02_CoilNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_ESK02_CoilNo.TextChanged += new System.EventHandler(this.Fun_EntryColorChange);
            // 
            // Btn_ESK02_Entry
            // 
            this.Btn_ESK02_Entry.BackColor = System.Drawing.Color.Gold;
            this.Btn_ESK02_Entry.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ESK02_Entry.Location = new System.Drawing.Point(3, 65);
            this.Btn_ESK02_Entry.Name = "Btn_ESK02_Entry";
            this.Btn_ESK02_Entry.Size = new System.Drawing.Size(34, 34);
            this.Btn_ESK02_Entry.TabIndex = 1146;
            this.Btn_ESK02_Entry.Text = "入";
            this.Btn_ESK02_Entry.UseVisualStyleBackColor = false;
            this.Btn_ESK02_Entry.Click += new System.EventHandler(this.Btn_ESK02Entry_Click);
            // 
            // Btn_ESK02_PrintLabel
            // 
            this.Btn_ESK02_PrintLabel.BackColor = System.Drawing.Color.Gold;
            this.Btn_ESK02_PrintLabel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ESK02_PrintLabel.Location = new System.Drawing.Point(105, 65);
            this.Btn_ESK02_PrintLabel.Name = "Btn_ESK02_PrintLabel";
            this.Btn_ESK02_PrintLabel.Size = new System.Drawing.Size(34, 34);
            this.Btn_ESK02_PrintLabel.TabIndex = 1475;
            this.Btn_ESK02_PrintLabel.Text = "签";
            this.Btn_ESK02_PrintLabel.UseVisualStyleBackColor = false;
            this.Btn_ESK02_PrintLabel.Click += new System.EventHandler(this.BtnESK02_PrintLabel_Click);
            // 
            // Btn_ESK02_Del
            // 
            this.Btn_ESK02_Del.BackColor = System.Drawing.Color.Gold;
            this.Btn_ESK02_Del.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ESK02_Del.Location = new System.Drawing.Point(71, 65);
            this.Btn_ESK02_Del.Name = "Btn_ESK02_Del";
            this.Btn_ESK02_Del.Size = new System.Drawing.Size(34, 34);
            this.Btn_ESK02_Del.TabIndex = 1473;
            this.Btn_ESK02_Del.Text = "删";
            this.Btn_ESK02_Del.UseVisualStyleBackColor = false;
            this.Btn_ESK02_Del.Click += new System.EventHandler(this.Btn_ESK02_Del_Click);
            // 
            // Btn_ESK02_Reject
            // 
            this.Btn_ESK02_Reject.BackColor = System.Drawing.Color.Gold;
            this.Btn_ESK02_Reject.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ESK02_Reject.Location = new System.Drawing.Point(37, 65);
            this.Btn_ESK02_Reject.Name = "Btn_ESK02_Reject";
            this.Btn_ESK02_Reject.Size = new System.Drawing.Size(34, 34);
            this.Btn_ESK02_Reject.TabIndex = 1143;
            this.Btn_ESK02_Reject.Text = "退";
            this.Btn_ESK02_Reject.UseVisualStyleBackColor = false;
            this.Btn_ESK02_Reject.Click += new System.EventHandler(this.Btn_ESK02Reject_Click);
            // 
            // Pnl_ShowData_ETOP
            // 
            this.Pnl_ShowData_ETOP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_ShowData_ETOP.Controls.Add(Btn_ETOP_Leader);
            this.Pnl_ShowData_ETOP.Controls.Add(this.Lbl_ETOP_CoilNo_Title);
            this.Pnl_ShowData_ETOP.Controls.Add(this.Btn_ETOP_PrintLabel);
            this.Pnl_ShowData_ETOP.Controls.Add(this.Lbl_ETOP_Skid_Title);
            this.Pnl_ShowData_ETOP.Controls.Add(this.Btn_ETOP_Del);
            this.Pnl_ShowData_ETOP.Controls.Add(this.Lbl_ETOP_CoilNo);
            this.Pnl_ShowData_ETOP.Controls.Add(this.Btn_ETOP_ManualFeed);
            this.Pnl_ShowData_ETOP.Controls.Add(this.Btn_ETOP_Reject);
            this.Pnl_ShowData_ETOP.Location = new System.Drawing.Point(20, 562);
            this.Pnl_ShowData_ETOP.Name = "Pnl_ShowData_ETOP";
            this.Pnl_ShowData_ETOP.Size = new System.Drawing.Size(387, 101);
            this.Pnl_ShowData_ETOP.TabIndex = 1890;
            // 
            // Lbl_ETOP_CoilNo_Title
            // 
            this.Lbl_ETOP_CoilNo_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ETOP_CoilNo_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ETOP_CoilNo_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_ETOP_CoilNo_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_ETOP_CoilNo_Title.Location = new System.Drawing.Point(3, 32);
            this.Lbl_ETOP_CoilNo_Title.Name = "Lbl_ETOP_CoilNo_Title";
            this.Lbl_ETOP_CoilNo_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_ETOP_CoilNo_Title.TabIndex = 1479;
            this.Lbl_ETOP_CoilNo_Title.Text = "钢卷编号";
            this.Lbl_ETOP_CoilNo_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Btn_ETOP_PrintLabel
            // 
            this.Btn_ETOP_PrintLabel.BackColor = System.Drawing.Color.Gold;
            this.Btn_ETOP_PrintLabel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ETOP_PrintLabel.Location = new System.Drawing.Point(105, 65);
            this.Btn_ETOP_PrintLabel.Name = "Btn_ETOP_PrintLabel";
            this.Btn_ETOP_PrintLabel.Size = new System.Drawing.Size(34, 34);
            this.Btn_ETOP_PrintLabel.TabIndex = 1477;
            this.Btn_ETOP_PrintLabel.Text = "签";
            this.Btn_ETOP_PrintLabel.UseVisualStyleBackColor = false;
            this.Btn_ETOP_PrintLabel.Click += new System.EventHandler(this.BtnETOP_PrintLabel_Click);
            // 
            // Lbl_ETOP_Skid_Title
            // 
            this.Lbl_ETOP_Skid_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ETOP_Skid_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ETOP_Skid_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ETOP_Skid_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_ETOP_Skid_Title.Location = new System.Drawing.Point(3, 3);
            this.Lbl_ETOP_Skid_Title.Name = "Lbl_ETOP_Skid_Title";
            this.Lbl_ETOP_Skid_Title.Size = new System.Drawing.Size(380, 29);
            this.Lbl_ETOP_Skid_Title.TabIndex = 1134;
            this.Lbl_ETOP_Skid_Title.Text = "Skid3";
            this.Lbl_ETOP_Skid_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_ETOP_Del
            // 
            this.Btn_ETOP_Del.BackColor = System.Drawing.Color.Gold;
            this.Btn_ETOP_Del.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ETOP_Del.Location = new System.Drawing.Point(71, 65);
            this.Btn_ETOP_Del.Name = "Btn_ETOP_Del";
            this.Btn_ETOP_Del.Size = new System.Drawing.Size(34, 34);
            this.Btn_ETOP_Del.TabIndex = 1476;
            this.Btn_ETOP_Del.Text = "删";
            this.Btn_ETOP_Del.UseVisualStyleBackColor = false;
            this.Btn_ETOP_Del.Click += new System.EventHandler(this.Btn_ETOP_Del_Click);
            // 
            // Lbl_ETOP_CoilNo
            // 
            this.Lbl_ETOP_CoilNo.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ETOP_CoilNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ETOP_CoilNo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_ETOP_CoilNo.Location = new System.Drawing.Point(153, 32);
            this.Lbl_ETOP_CoilNo.Name = "Lbl_ETOP_CoilNo";
            this.Lbl_ETOP_CoilNo.Size = new System.Drawing.Size(230, 33);
            this.Lbl_ETOP_CoilNo.TabIndex = 1135;
            this.Lbl_ETOP_CoilNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_ETOP_CoilNo.TextChanged += new System.EventHandler(this.Fun_EntryColorChange);
            // 
            // Btn_ETOP_ManualFeed
            // 
            this.Btn_ETOP_ManualFeed.BackColor = System.Drawing.Color.Gold;
            this.Btn_ETOP_ManualFeed.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ETOP_ManualFeed.Location = new System.Drawing.Point(3, 65);
            this.Btn_ETOP_ManualFeed.Name = "Btn_ETOP_ManualFeed";
            this.Btn_ETOP_ManualFeed.Size = new System.Drawing.Size(34, 34);
            this.Btn_ETOP_ManualFeed.TabIndex = 1141;
            this.Btn_ETOP_ManualFeed.Text = "料";
            this.Btn_ETOP_ManualFeed.UseVisualStyleBackColor = false;
            this.Btn_ETOP_ManualFeed.Visible = false;
            this.Btn_ETOP_ManualFeed.Click += new System.EventHandler(this.Btn_ManualFeed_Click);
            // 
            // Btn_ETOP_Reject
            // 
            this.Btn_ETOP_Reject.BackColor = System.Drawing.Color.Gold;
            this.Btn_ETOP_Reject.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_ETOP_Reject.Location = new System.Drawing.Point(37, 65);
            this.Btn_ETOP_Reject.Name = "Btn_ETOP_Reject";
            this.Btn_ETOP_Reject.Size = new System.Drawing.Size(34, 34);
            this.Btn_ETOP_Reject.TabIndex = 1137;
            this.Btn_ETOP_Reject.Text = "退";
            this.Btn_ETOP_Reject.UseVisualStyleBackColor = false;
            this.Btn_ETOP_Reject.Click += new System.EventHandler(this.Btn_ETOPReject_Click);
            // 
            // Pnl_ShowData_DSK01
            // 
            this.Pnl_ShowData_DSK01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_ShowData_DSK01.Controls.Add(this.Btn_DSK01_Del);
            this.Pnl_ShowData_DSK01.Controls.Add(this.Lbl_DSK01_Weight_Unit);
            this.Pnl_ShowData_DSK01.Controls.Add(this.Btn_DSK01_CoilOut);
            this.Pnl_ShowData_DSK01.Controls.Add(this.Lbl_DSK01_Weight_Title);
            this.Pnl_ShowData_DSK01.Controls.Add(this.Lbl_DSK01_CoilNo_Title);
            this.Pnl_ShowData_DSK01.Controls.Add(this.Lbl_DSK01_Skid_Title);
            this.Pnl_ShowData_DSK01.Controls.Add(this.Lbl_DSK01_CoilNo);
            this.Pnl_ShowData_DSK01.Controls.Add(this.Txt_DSK01_Weight);
            this.Pnl_ShowData_DSK01.Controls.Add(this.Btn_DSK01_PrintLabel);
            this.Pnl_ShowData_DSK01.Location = new System.Drawing.Point(1521, 360);
            this.Pnl_ShowData_DSK01.Name = "Pnl_ShowData_DSK01";
            this.Pnl_ShowData_DSK01.Size = new System.Drawing.Size(387, 101);
            this.Pnl_ShowData_DSK01.TabIndex = 1891;
            // 
            // Btn_DSK01_Del
            // 
            this.Btn_DSK01_Del.BackColor = System.Drawing.Color.Gold;
            this.Btn_DSK01_Del.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_DSK01_Del.Location = new System.Drawing.Point(282, 66);
            this.Btn_DSK01_Del.Name = "Btn_DSK01_Del";
            this.Btn_DSK01_Del.Size = new System.Drawing.Size(34, 34);
            this.Btn_DSK01_Del.TabIndex = 1474;
            this.Btn_DSK01_Del.Text = "删";
            this.Btn_DSK01_Del.UseVisualStyleBackColor = false;
            this.Btn_DSK01_Del.Click += new System.EventHandler(this.Btn_DSK01_Del_Click);
            // 
            // Lbl_DSK01_Weight_Unit
            // 
            this.Lbl_DSK01_Weight_Unit.AutoSize = true;
            this.Lbl_DSK01_Weight_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DSK01_Weight_Unit.Location = new System.Drawing.Point(226, 69);
            this.Lbl_DSK01_Weight_Unit.Name = "Lbl_DSK01_Weight_Unit";
            this.Lbl_DSK01_Weight_Unit.Size = new System.Drawing.Size(34, 24);
            this.Lbl_DSK01_Weight_Unit.TabIndex = 1879;
            this.Lbl_DSK01_Weight_Unit.Text = "Kg";
            // 
            // Btn_DSK01_CoilOut
            // 
            this.Btn_DSK01_CoilOut.BackColor = System.Drawing.Color.Gold;
            this.Btn_DSK01_CoilOut.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_DSK01_CoilOut.Location = new System.Drawing.Point(350, 66);
            this.Btn_DSK01_CoilOut.Name = "Btn_DSK01_CoilOut";
            this.Btn_DSK01_CoilOut.Size = new System.Drawing.Size(34, 34);
            this.Btn_DSK01_CoilOut.TabIndex = 1348;
            this.Btn_DSK01_CoilOut.Text = "天";
            this.Btn_DSK01_CoilOut.UseVisualStyleBackColor = false;
            this.Btn_DSK01_CoilOut.Visible = false;
            this.Btn_DSK01_CoilOut.Click += new System.EventHandler(this.Btn_DSK01_CoilOut_Click);
            // 
            // Lbl_DSK01_Weight_Title
            // 
            this.Lbl_DSK01_Weight_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DSK01_Weight_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DSK01_Weight_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_DSK01_Weight_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_DSK01_Weight_Title.Location = new System.Drawing.Point(3, 65);
            this.Lbl_DSK01_Weight_Title.Name = "Lbl_DSK01_Weight_Title";
            this.Lbl_DSK01_Weight_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_DSK01_Weight_Title.TabIndex = 1480;
            this.Lbl_DSK01_Weight_Title.Text = "重量";
            this.Lbl_DSK01_Weight_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_DSK01_CoilNo_Title
            // 
            this.Lbl_DSK01_CoilNo_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DSK01_CoilNo_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DSK01_CoilNo_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_DSK01_CoilNo_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_DSK01_CoilNo_Title.Location = new System.Drawing.Point(3, 32);
            this.Lbl_DSK01_CoilNo_Title.Name = "Lbl_DSK01_CoilNo_Title";
            this.Lbl_DSK01_CoilNo_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_DSK01_CoilNo_Title.TabIndex = 1479;
            this.Lbl_DSK01_CoilNo_Title.Text = "钢卷编号";
            this.Lbl_DSK01_CoilNo_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_DSK01_Skid_Title
            // 
            this.Lbl_DSK01_Skid_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DSK01_Skid_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DSK01_Skid_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_DSK01_Skid_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_DSK01_Skid_Title.Location = new System.Drawing.Point(3, 3);
            this.Lbl_DSK01_Skid_Title.Name = "Lbl_DSK01_Skid_Title";
            this.Lbl_DSK01_Skid_Title.Size = new System.Drawing.Size(380, 29);
            this.Lbl_DSK01_Skid_Title.TabIndex = 678;
            this.Lbl_DSK01_Skid_Title.Text = "Skid1";
            this.Lbl_DSK01_Skid_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_DSK01_CoilNo
            // 
            this.Lbl_DSK01_CoilNo.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DSK01_CoilNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DSK01_CoilNo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DSK01_CoilNo.Location = new System.Drawing.Point(153, 32);
            this.Lbl_DSK01_CoilNo.Name = "Lbl_DSK01_CoilNo";
            this.Lbl_DSK01_CoilNo.Size = new System.Drawing.Size(230, 33);
            this.Lbl_DSK01_CoilNo.TabIndex = 679;
            this.Lbl_DSK01_CoilNo.Text = " ";
            this.Lbl_DSK01_CoilNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_DSK01_CoilNo.TextChanged += new System.EventHandler(this.Fun_ExitColorChange);
            // 
            // Txt_DSK01_Weight
            // 
            this.Txt_DSK01_Weight.BackColor = System.Drawing.Color.Silver;
            this.Txt_DSK01_Weight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_DSK01_Weight.Cursor = System.Windows.Forms.Cursors.Default;
            this.Txt_DSK01_Weight.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_DSK01_Weight.Location = new System.Drawing.Point(153, 65);
            this.Txt_DSK01_Weight.MaxLength = 5;
            this.Txt_DSK01_Weight.Name = "Txt_DSK01_Weight";
            this.Txt_DSK01_Weight.ReadOnly = true;
            this.Txt_DSK01_Weight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Txt_DSK01_Weight.Size = new System.Drawing.Size(73, 33);
            this.Txt_DSK01_Weight.TabIndex = 1063;
            this.Txt_DSK01_Weight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Btn_DSK01_PrintLabel
            // 
            this.Btn_DSK01_PrintLabel.BackColor = System.Drawing.Color.Gold;
            this.Btn_DSK01_PrintLabel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_DSK01_PrintLabel.Location = new System.Drawing.Point(316, 66);
            this.Btn_DSK01_PrintLabel.Name = "Btn_DSK01_PrintLabel";
            this.Btn_DSK01_PrintLabel.Size = new System.Drawing.Size(34, 34);
            this.Btn_DSK01_PrintLabel.TabIndex = 1044;
            this.Btn_DSK01_PrintLabel.Text = "签";
            this.Btn_DSK01_PrintLabel.UseVisualStyleBackColor = false;
            this.Btn_DSK01_PrintLabel.Click += new System.EventHandler(this.BtnDT21PrintLabel_Click);
            // 
            // Pnl_ShowData_TR
            // 
            this.Pnl_ShowData_TR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_ShowData_TR.Controls.Add(this.Lbl_TR_Skid_Title);
            this.Pnl_ShowData_TR.Controls.Add(this.Lbl_TR_CoilNo_Title);
            this.Pnl_ShowData_TR.Controls.Add(this.Lbl_TR_CoilNo);
            this.Pnl_ShowData_TR.Controls.Add(this.Lbl_TR_Tension);
            this.Pnl_ShowData_TR.Controls.Add(this.Lbl_TR_Current_Title);
            this.Pnl_ShowData_TR.Controls.Add(this.Lbl_TR_Tension_Title);
            this.Pnl_ShowData_TR.Controls.Add(this.Lbl_TR_Current);
            this.Pnl_ShowData_TR.Controls.Add(this.Lbl_TR_Current_Unit);
            this.Pnl_ShowData_TR.Controls.Add(this.Lbl_TR_Tension_Unit);
            this.Pnl_ShowData_TR.Location = new System.Drawing.Point(1521, 201);
            this.Pnl_ShowData_TR.Name = "Pnl_ShowData_TR";
            this.Pnl_ShowData_TR.Size = new System.Drawing.Size(387, 159);
            this.Pnl_ShowData_TR.TabIndex = 1892;
            // 
            // Lbl_TR_Skid_Title
            // 
            this.Lbl_TR_Skid_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_TR_Skid_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_TR_Skid_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_TR_Skid_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_TR_Skid_Title.Location = new System.Drawing.Point(3, 3);
            this.Lbl_TR_Skid_Title.Name = "Lbl_TR_Skid_Title";
            this.Lbl_TR_Skid_Title.Size = new System.Drawing.Size(380, 29);
            this.Lbl_TR_Skid_Title.TabIndex = 1349;
            this.Lbl_TR_Skid_Title.Text = "TR";
            this.Lbl_TR_Skid_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_TR_CoilNo_Title
            // 
            this.Lbl_TR_CoilNo_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_TR_CoilNo_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_TR_CoilNo_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_TR_CoilNo_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_TR_CoilNo_Title.Location = new System.Drawing.Point(3, 32);
            this.Lbl_TR_CoilNo_Title.Name = "Lbl_TR_CoilNo_Title";
            this.Lbl_TR_CoilNo_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_TR_CoilNo_Title.TabIndex = 1352;
            this.Lbl_TR_CoilNo_Title.Text = "钢卷编号";
            this.Lbl_TR_CoilNo_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_TR_CoilNo
            // 
            this.Lbl_TR_CoilNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Lbl_TR_CoilNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_TR_CoilNo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_TR_CoilNo.ForeColor = System.Drawing.Color.Black;
            this.Lbl_TR_CoilNo.Location = new System.Drawing.Point(153, 32);
            this.Lbl_TR_CoilNo.Name = "Lbl_TR_CoilNo";
            this.Lbl_TR_CoilNo.Size = new System.Drawing.Size(230, 33);
            this.Lbl_TR_CoilNo.TabIndex = 1350;
            this.Lbl_TR_CoilNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_TR_Tension
            // 
            this.Lbl_TR_Tension.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_TR_Tension.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_TR_Tension.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_TR_Tension.Location = new System.Drawing.Point(153, 65);
            this.Lbl_TR_Tension.Name = "Lbl_TR_Tension";
            this.Lbl_TR_Tension.Size = new System.Drawing.Size(190, 33);
            this.Lbl_TR_Tension.TabIndex = 1393;
            this.Lbl_TR_Tension.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_TR_Current_Title
            // 
            this.Lbl_TR_Current_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_TR_Current_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_TR_Current_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_TR_Current_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_TR_Current_Title.Location = new System.Drawing.Point(3, 98);
            this.Lbl_TR_Current_Title.Name = "Lbl_TR_Current_Title";
            this.Lbl_TR_Current_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_TR_Current_Title.TabIndex = 1394;
            this.Lbl_TR_Current_Title.Text = "电流";
            this.Lbl_TR_Current_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_TR_Tension_Title
            // 
            this.Lbl_TR_Tension_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_TR_Tension_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_TR_Tension_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_TR_Tension_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_TR_Tension_Title.Location = new System.Drawing.Point(3, 65);
            this.Lbl_TR_Tension_Title.Name = "Lbl_TR_Tension_Title";
            this.Lbl_TR_Tension_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_TR_Tension_Title.TabIndex = 1392;
            this.Lbl_TR_Tension_Title.Text = "张力(实际/设定)";
            this.Lbl_TR_Tension_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_TR_Current
            // 
            this.Lbl_TR_Current.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_TR_Current.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_TR_Current.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_TR_Current.Location = new System.Drawing.Point(153, 98);
            this.Lbl_TR_Current.Name = "Lbl_TR_Current";
            this.Lbl_TR_Current.Size = new System.Drawing.Size(190, 33);
            this.Lbl_TR_Current.TabIndex = 1395;
            this.Lbl_TR_Current.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_TR_Current_Unit
            // 
            this.Lbl_TR_Current_Unit.AutoSize = true;
            this.Lbl_TR_Current_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_TR_Current_Unit.Location = new System.Drawing.Point(343, 102);
            this.Lbl_TR_Current_Unit.Name = "Lbl_TR_Current_Unit";
            this.Lbl_TR_Current_Unit.Size = new System.Drawing.Size(24, 24);
            this.Lbl_TR_Current_Unit.TabIndex = 1879;
            this.Lbl_TR_Current_Unit.Text = "A";
            // 
            // Lbl_TR_Tension_Unit
            // 
            this.Lbl_TR_Tension_Unit.AutoSize = true;
            this.Lbl_TR_Tension_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_TR_Tension_Unit.Location = new System.Drawing.Point(343, 69);
            this.Lbl_TR_Tension_Unit.Name = "Lbl_TR_Tension_Unit";
            this.Lbl_TR_Tension_Unit.Size = new System.Drawing.Size(37, 24);
            this.Lbl_TR_Tension_Unit.TabIndex = 1878;
            this.Lbl_TR_Tension_Unit.Text = "kN";
            // 
            // Pnl_ShowData_DSK02
            // 
            this.Pnl_ShowData_DSK02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_ShowData_DSK02.Controls.Add(this.Btn_DSK02_Del);
            this.Pnl_ShowData_DSK02.Controls.Add(this.Lbl_DSK02_Weight_Unit);
            this.Pnl_ShowData_DSK02.Controls.Add(this.Btn_DSK02_CoilOut);
            this.Pnl_ShowData_DSK02.Controls.Add(this.Lbl_DSK02_Weight_Title);
            this.Pnl_ShowData_DSK02.Controls.Add(this.Lbl_DSK02_CoilNo_Title);
            this.Pnl_ShowData_DSK02.Controls.Add(this.Lbl_DSK02_Skid_Title);
            this.Pnl_ShowData_DSK02.Controls.Add(this.Lbl_DSK02_CoilNo);
            this.Pnl_ShowData_DSK02.Controls.Add(this.Btn_DSK02_PrintLabel);
            this.Pnl_ShowData_DSK02.Controls.Add(this.Txt_DSK02_Weight);
            this.Pnl_ShowData_DSK02.Location = new System.Drawing.Point(1521, 461);
            this.Pnl_ShowData_DSK02.Name = "Pnl_ShowData_DSK02";
            this.Pnl_ShowData_DSK02.Size = new System.Drawing.Size(387, 101);
            this.Pnl_ShowData_DSK02.TabIndex = 1893;
            // 
            // Btn_DSK02_Del
            // 
            this.Btn_DSK02_Del.BackColor = System.Drawing.Color.Gold;
            this.Btn_DSK02_Del.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_DSK02_Del.Location = new System.Drawing.Point(282, 66);
            this.Btn_DSK02_Del.Name = "Btn_DSK02_Del";
            this.Btn_DSK02_Del.Size = new System.Drawing.Size(34, 34);
            this.Btn_DSK02_Del.TabIndex = 1472;
            this.Btn_DSK02_Del.Text = "删";
            this.Btn_DSK02_Del.UseVisualStyleBackColor = false;
            this.Btn_DSK02_Del.Click += new System.EventHandler(this.Btn_DSK02_Del_Click);
            // 
            // Lbl_DSK02_Weight_Unit
            // 
            this.Lbl_DSK02_Weight_Unit.AutoSize = true;
            this.Lbl_DSK02_Weight_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DSK02_Weight_Unit.Location = new System.Drawing.Point(226, 69);
            this.Lbl_DSK02_Weight_Unit.Name = "Lbl_DSK02_Weight_Unit";
            this.Lbl_DSK02_Weight_Unit.Size = new System.Drawing.Size(34, 24);
            this.Lbl_DSK02_Weight_Unit.TabIndex = 1880;
            this.Lbl_DSK02_Weight_Unit.Text = "Kg";
            // 
            // Btn_DSK02_CoilOut
            // 
            this.Btn_DSK02_CoilOut.BackColor = System.Drawing.Color.Gold;
            this.Btn_DSK02_CoilOut.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_DSK02_CoilOut.Location = new System.Drawing.Point(350, 66);
            this.Btn_DSK02_CoilOut.Name = "Btn_DSK02_CoilOut";
            this.Btn_DSK02_CoilOut.Size = new System.Drawing.Size(34, 34);
            this.Btn_DSK02_CoilOut.TabIndex = 1346;
            this.Btn_DSK02_CoilOut.Text = "出";
            this.Btn_DSK02_CoilOut.UseVisualStyleBackColor = false;
            this.Btn_DSK02_CoilOut.Click += new System.EventHandler(this.Btn_DSK02_CoilOut_Click);
            // 
            // Lbl_DSK02_Weight_Title
            // 
            this.Lbl_DSK02_Weight_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DSK02_Weight_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DSK02_Weight_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_DSK02_Weight_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_DSK02_Weight_Title.Location = new System.Drawing.Point(3, 65);
            this.Lbl_DSK02_Weight_Title.Name = "Lbl_DSK02_Weight_Title";
            this.Lbl_DSK02_Weight_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_DSK02_Weight_Title.TabIndex = 1480;
            this.Lbl_DSK02_Weight_Title.Text = "重量";
            this.Lbl_DSK02_Weight_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_DSK02_CoilNo_Title
            // 
            this.Lbl_DSK02_CoilNo_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DSK02_CoilNo_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DSK02_CoilNo_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_DSK02_CoilNo_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_DSK02_CoilNo_Title.Location = new System.Drawing.Point(3, 32);
            this.Lbl_DSK02_CoilNo_Title.Name = "Lbl_DSK02_CoilNo_Title";
            this.Lbl_DSK02_CoilNo_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_DSK02_CoilNo_Title.TabIndex = 1479;
            this.Lbl_DSK02_CoilNo_Title.Text = "钢卷编号";
            this.Lbl_DSK02_CoilNo_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_DSK02_Skid_Title
            // 
            this.Lbl_DSK02_Skid_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DSK02_Skid_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DSK02_Skid_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_DSK02_Skid_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_DSK02_Skid_Title.Location = new System.Drawing.Point(3, 3);
            this.Lbl_DSK02_Skid_Title.Name = "Lbl_DSK02_Skid_Title";
            this.Lbl_DSK02_Skid_Title.Size = new System.Drawing.Size(380, 29);
            this.Lbl_DSK02_Skid_Title.TabIndex = 680;
            this.Lbl_DSK02_Skid_Title.Text = "Skid2";
            this.Lbl_DSK02_Skid_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_DSK02_CoilNo
            // 
            this.Lbl_DSK02_CoilNo.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DSK02_CoilNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DSK02_CoilNo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DSK02_CoilNo.Location = new System.Drawing.Point(153, 32);
            this.Lbl_DSK02_CoilNo.Name = "Lbl_DSK02_CoilNo";
            this.Lbl_DSK02_CoilNo.Size = new System.Drawing.Size(230, 33);
            this.Lbl_DSK02_CoilNo.TabIndex = 681;
            this.Lbl_DSK02_CoilNo.Text = " ";
            this.Lbl_DSK02_CoilNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Lbl_DSK02_CoilNo.TextChanged += new System.EventHandler(this.Fun_ExitColorChange);
            // 
            // Btn_DSK02_PrintLabel
            // 
            this.Btn_DSK02_PrintLabel.BackColor = System.Drawing.Color.Gold;
            this.Btn_DSK02_PrintLabel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_DSK02_PrintLabel.Location = new System.Drawing.Point(316, 66);
            this.Btn_DSK02_PrintLabel.Name = "Btn_DSK02_PrintLabel";
            this.Btn_DSK02_PrintLabel.Size = new System.Drawing.Size(34, 34);
            this.Btn_DSK02_PrintLabel.TabIndex = 1065;
            this.Btn_DSK02_PrintLabel.Text = "签";
            this.Btn_DSK02_PrintLabel.UseVisualStyleBackColor = false;
            this.Btn_DSK02_PrintLabel.Click += new System.EventHandler(this.BtnDT22PrintLabel_Click);
            // 
            // Txt_DSK02_Weight
            // 
            this.Txt_DSK02_Weight.BackColor = System.Drawing.Color.Silver;
            this.Txt_DSK02_Weight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_DSK02_Weight.Cursor = System.Windows.Forms.Cursors.Default;
            this.Txt_DSK02_Weight.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_DSK02_Weight.Location = new System.Drawing.Point(153, 65);
            this.Txt_DSK02_Weight.MaxLength = 5;
            this.Txt_DSK02_Weight.Name = "Txt_DSK02_Weight";
            this.Txt_DSK02_Weight.ReadOnly = true;
            this.Txt_DSK02_Weight.Size = new System.Drawing.Size(73, 33);
            this.Txt_DSK02_Weight.TabIndex = 1064;
            this.Txt_DSK02_Weight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Pnl_ShowData_ECar
            // 
            this.Pnl_ShowData_ECar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_ShowData_ECar.Controls.Add(this.Lbl_ECar_CoilNo);
            this.Pnl_ShowData_ECar.Controls.Add(this.Lbl_ECar_Skid_Title);
            this.Pnl_ShowData_ECar.Controls.Add(this.Lbl_ECar_CoilNo_Title);
            this.Pnl_ShowData_ECar.Location = new System.Drawing.Point(411, 290);
            this.Pnl_ShowData_ECar.Name = "Pnl_ShowData_ECar";
            this.Pnl_ShowData_ECar.Size = new System.Drawing.Size(387, 70);
            this.Pnl_ShowData_ECar.TabIndex = 1892;
            // 
            // Lbl_ECar_CoilNo
            // 
            this.Lbl_ECar_CoilNo.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ECar_CoilNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ECar_CoilNo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_ECar_CoilNo.Location = new System.Drawing.Point(153, 32);
            this.Lbl_ECar_CoilNo.Name = "Lbl_ECar_CoilNo";
            this.Lbl_ECar_CoilNo.Size = new System.Drawing.Size(230, 33);
            this.Lbl_ECar_CoilNo.TabIndex = 1144;
            this.Lbl_ECar_CoilNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_ECar_Skid_Title
            // 
            this.Lbl_ECar_Skid_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ECar_Skid_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ECar_Skid_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_ECar_Skid_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_ECar_Skid_Title.Location = new System.Drawing.Point(3, 3);
            this.Lbl_ECar_Skid_Title.Name = "Lbl_ECar_Skid_Title";
            this.Lbl_ECar_Skid_Title.Size = new System.Drawing.Size(380, 29);
            this.Lbl_ECar_Skid_Title.TabIndex = 1145;
            this.Lbl_ECar_Skid_Title.Text = "ECar";
            this.Lbl_ECar_Skid_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_ECar_CoilNo_Title
            // 
            this.Lbl_ECar_CoilNo_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_ECar_CoilNo_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_ECar_CoilNo_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_ECar_CoilNo_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_ECar_CoilNo_Title.Location = new System.Drawing.Point(3, 32);
            this.Lbl_ECar_CoilNo_Title.Name = "Lbl_ECar_CoilNo_Title";
            this.Lbl_ECar_CoilNo_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_ECar_CoilNo_Title.TabIndex = 1479;
            this.Lbl_ECar_CoilNo_Title.Text = "钢卷编号";
            this.Lbl_ECar_CoilNo_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Pnl_ShowData_DCar
            // 
            this.Pnl_ShowData_DCar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_ShowData_DCar.Controls.Add(this.Lbl_DCar_CoilNo);
            this.Pnl_ShowData_DCar.Controls.Add(this.Lbl_DCar_Skid_Title);
            this.Pnl_ShowData_DCar.Controls.Add(this.Lbl_DCar_CoilNo_Title);
            this.Pnl_ShowData_DCar.Location = new System.Drawing.Point(1131, 290);
            this.Pnl_ShowData_DCar.Name = "Pnl_ShowData_DCar";
            this.Pnl_ShowData_DCar.Size = new System.Drawing.Size(387, 70);
            this.Pnl_ShowData_DCar.TabIndex = 1894;
            // 
            // Lbl_DCar_CoilNo
            // 
            this.Lbl_DCar_CoilNo.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DCar_CoilNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DCar_CoilNo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DCar_CoilNo.Location = new System.Drawing.Point(153, 32);
            this.Lbl_DCar_CoilNo.Name = "Lbl_DCar_CoilNo";
            this.Lbl_DCar_CoilNo.Size = new System.Drawing.Size(230, 33);
            this.Lbl_DCar_CoilNo.TabIndex = 1468;
            this.Lbl_DCar_CoilNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_DCar_Skid_Title
            // 
            this.Lbl_DCar_Skid_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DCar_Skid_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DCar_Skid_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_DCar_Skid_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_DCar_Skid_Title.Location = new System.Drawing.Point(3, 3);
            this.Lbl_DCar_Skid_Title.Name = "Lbl_DCar_Skid_Title";
            this.Lbl_DCar_Skid_Title.Size = new System.Drawing.Size(380, 29);
            this.Lbl_DCar_Skid_Title.TabIndex = 1469;
            this.Lbl_DCar_Skid_Title.Text = "DCar";
            this.Lbl_DCar_Skid_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_DCar_CoilNo_Title
            // 
            this.Lbl_DCar_CoilNo_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_DCar_CoilNo_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_DCar_CoilNo_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_DCar_CoilNo_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_DCar_CoilNo_Title.Location = new System.Drawing.Point(3, 32);
            this.Lbl_DCar_CoilNo_Title.Name = "Lbl_DCar_CoilNo_Title";
            this.Lbl_DCar_CoilNo_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_DCar_CoilNo_Title.TabIndex = 1479;
            this.Lbl_DCar_CoilNo_Title.Text = "钢卷编号";
            this.Lbl_DCar_CoilNo_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Pic_Track_Picture
            // 
            this.Pic_Track_Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Pic_Track_Picture.Image = global::CPL1HMI.Properties.Resources.CPL1_Tracking;
            this.Pic_Track_Picture.Location = new System.Drawing.Point(449, 34);
            this.Pic_Track_Picture.Name = "Pic_Track_Picture";
            this.Pic_Track_Picture.Size = new System.Drawing.Size(1042, 273);
            this.Pic_Track_Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_Track_Picture.TabIndex = 1430;
            this.Pic_Track_Picture.TabStop = false;
            // 
            // Pnl_Scan
            // 
            this.Pnl_Scan.BackColor = System.Drawing.Color.DarkOrange;
            this.Pnl_Scan.Controls.Add(this.Pnl_Scan_BackGround);
            this.Pnl_Scan.Location = new System.Drawing.Point(227, 30);
            this.Pnl_Scan.Name = "Pnl_Scan";
            this.Pnl_Scan.Size = new System.Drawing.Size(161, 54);
            this.Pnl_Scan.TabIndex = 1477;
            this.Pnl_Scan.Visible = false;
            // 
            // Pnl_Scan_BackGround
            // 
            this.Pnl_Scan_BackGround.BackColor = System.Drawing.Color.Bisque;
            this.Pnl_Scan_BackGround.Controls.Add(this.Btn_Scan_No);
            this.Pnl_Scan_BackGround.Controls.Add(this.Btn_Scan_Yes);
            this.Pnl_Scan_BackGround.Controls.Add(this.Lbl_Scan_SkidNo_Title);
            this.Pnl_Scan_BackGround.Controls.Add(this.Lbl_Scan_Desc);
            this.Pnl_Scan_BackGround.Controls.Add(this.Lbl_Scan_CoilNo_Title);
            this.Pnl_Scan_BackGround.Controls.Add(this.Lbl_Scan_SkidNo);
            this.Pnl_Scan_BackGround.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pnl_Scan_BackGround.Location = new System.Drawing.Point(5, 5);
            this.Pnl_Scan_BackGround.Name = "Pnl_Scan_BackGround";
            this.Pnl_Scan_BackGround.Size = new System.Drawing.Size(340, 240);
            this.Pnl_Scan_BackGround.TabIndex = 2;
            // 
            // Btn_Scan_No
            // 
            this.Btn_Scan_No.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.Btn_Scan_No.Location = new System.Drawing.Point(190, 183);
            this.Btn_Scan_No.Name = "Btn_Scan_No";
            this.Btn_Scan_No.Size = new System.Drawing.Size(101, 45);
            this.Btn_Scan_No.TabIndex = 5;
            this.Btn_Scan_No.Text = "否";
            this.Btn_Scan_No.UseVisualStyleBackColor = true;
            this.Btn_Scan_No.Click += new System.EventHandler(this.Btn_ScanNo_Click);
            // 
            // Btn_Scan_Yes
            // 
            this.Btn_Scan_Yes.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.Btn_Scan_Yes.Location = new System.Drawing.Point(49, 183);
            this.Btn_Scan_Yes.Name = "Btn_Scan_Yes";
            this.Btn_Scan_Yes.Size = new System.Drawing.Size(101, 45);
            this.Btn_Scan_Yes.TabIndex = 4;
            this.Btn_Scan_Yes.Text = "是";
            this.Btn_Scan_Yes.UseVisualStyleBackColor = true;
            this.Btn_Scan_Yes.Click += new System.EventHandler(this.Btn_ScanYes_Click);
            // 
            // Lbl_Scan_SkidNo_Title
            // 
            this.Lbl_Scan_SkidNo_Title.AutoSize = true;
            this.Lbl_Scan_SkidNo_Title.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.Lbl_Scan_SkidNo_Title.Location = new System.Drawing.Point(49, 110);
            this.Lbl_Scan_SkidNo_Title.Name = "Lbl_Scan_SkidNo_Title";
            this.Lbl_Scan_SkidNo_Title.Size = new System.Drawing.Size(146, 31);
            this.Lbl_Scan_SkidNo_Title.TabIndex = 3;
            this.Lbl_Scan_SkidNo_Title.Text = "建立于鞍座 :";
            // 
            // Lbl_Scan_Desc
            // 
            this.Lbl_Scan_Desc.AutoSize = true;
            this.Lbl_Scan_Desc.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Scan_Desc.Location = new System.Drawing.Point(49, 12);
            this.Lbl_Scan_Desc.Name = "Lbl_Scan_Desc";
            this.Lbl_Scan_Desc.Size = new System.Drawing.Size(242, 31);
            this.Lbl_Scan_Desc.TabIndex = 2;
            this.Lbl_Scan_Desc.Text = "是否以扫描钢卷号码 :";
            // 
            // Lbl_Scan_CoilNo_Title
            // 
            this.Lbl_Scan_CoilNo_Title.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.Lbl_Scan_CoilNo_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_Scan_CoilNo_Title.Location = new System.Drawing.Point(49, 61);
            this.Lbl_Scan_CoilNo_Title.Name = "Lbl_Scan_CoilNo_Title";
            this.Lbl_Scan_CoilNo_Title.Size = new System.Drawing.Size(242, 31);
            this.Lbl_Scan_CoilNo_Title.TabIndex = 0;
            this.Lbl_Scan_CoilNo_Title.Text = "钢卷号码";
            // 
            // Lbl_Scan_SkidNo
            // 
            this.Lbl_Scan_SkidNo.AutoSize = true;
            this.Lbl_Scan_SkidNo.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.Lbl_Scan_SkidNo.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_Scan_SkidNo.Location = new System.Drawing.Point(203, 110);
            this.Lbl_Scan_SkidNo.Name = "Lbl_Scan_SkidNo";
            this.Lbl_Scan_SkidNo.Size = new System.Drawing.Size(62, 31);
            this.Lbl_Scan_SkidNo.TabIndex = 1;
            this.Lbl_Scan_SkidNo.Text = "鞍座";
            // 
            // Pnl_NetworkStatus
            // 
            this.Pnl_NetworkStatus.Controls.Add(this.Lbl_PLC_Color);
            this.Pnl_NetworkStatus.Controls.Add(this.label11);
            this.Pnl_NetworkStatus.Controls.Add(this.Lbl_RevMMS_Color);
            this.Pnl_NetworkStatus.Controls.Add(this.Lbl_SendMMS_Color);
            this.Pnl_NetworkStatus.Controls.Add(this.label32);
            this.Pnl_NetworkStatus.Controls.Add(this.Lbl_RevWMS_Color);
            this.Pnl_NetworkStatus.Controls.Add(this.Lbl_SendWMS_Color);
            this.Pnl_NetworkStatus.Controls.Add(this.label35);
            this.Pnl_NetworkStatus.Location = new System.Drawing.Point(1734, 11);
            this.Pnl_NetworkStatus.Name = "Pnl_NetworkStatus";
            this.Pnl_NetworkStatus.Size = new System.Drawing.Size(169, 46);
            this.Pnl_NetworkStatus.TabIndex = 1912;
            // 
            // Lbl_PLC_Color
            // 
            this.Lbl_PLC_Color.BackColor = System.Drawing.Color.Lime;
            this.Lbl_PLC_Color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_PLC_Color.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_PLC_Color.ForeColor = System.Drawing.Color.Black;
            this.Lbl_PLC_Color.Location = new System.Drawing.Point(113, 21);
            this.Lbl_PLC_Color.Name = "Lbl_PLC_Color";
            this.Lbl_PLC_Color.Size = new System.Drawing.Size(50, 18);
            this.Lbl_PLC_Color.TabIndex = 25;
            this.Lbl_PLC_Color.Text = "S/R";
            this.Lbl_PLC_Color.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(113, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 18);
            this.label11.TabIndex = 24;
            this.label11.Text = "L1";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_RevMMS_Color
            // 
            this.Lbl_RevMMS_Color.BackColor = System.Drawing.Color.Lime;
            this.Lbl_RevMMS_Color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_RevMMS_Color.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_RevMMS_Color.ForeColor = System.Drawing.Color.Black;
            this.Lbl_RevMMS_Color.Location = new System.Drawing.Point(83, 21);
            this.Lbl_RevMMS_Color.Name = "Lbl_RevMMS_Color";
            this.Lbl_RevMMS_Color.Size = new System.Drawing.Size(25, 18);
            this.Lbl_RevMMS_Color.TabIndex = 5;
            this.Lbl_RevMMS_Color.Text = "R";
            this.Lbl_RevMMS_Color.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_SendMMS_Color
            // 
            this.Lbl_SendMMS_Color.BackColor = System.Drawing.Color.Lime;
            this.Lbl_SendMMS_Color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_SendMMS_Color.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_SendMMS_Color.ForeColor = System.Drawing.Color.Black;
            this.Lbl_SendMMS_Color.Location = new System.Drawing.Point(58, 21);
            this.Lbl_SendMMS_Color.Name = "Lbl_SendMMS_Color";
            this.Lbl_SendMMS_Color.Size = new System.Drawing.Size(25, 18);
            this.Lbl_SendMMS_Color.TabIndex = 4;
            this.Lbl_SendMMS_Color.Text = "S";
            this.Lbl_SendMMS_Color.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.Black;
            this.label32.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label32.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label32.ForeColor = System.Drawing.Color.White;
            this.label32.Location = new System.Drawing.Point(58, 3);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(50, 18);
            this.label32.TabIndex = 3;
            this.label32.Text = "MMS";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_RevWMS_Color
            // 
            this.Lbl_RevWMS_Color.BackColor = System.Drawing.Color.Lime;
            this.Lbl_RevWMS_Color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_RevWMS_Color.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_RevWMS_Color.ForeColor = System.Drawing.Color.Black;
            this.Lbl_RevWMS_Color.Location = new System.Drawing.Point(28, 21);
            this.Lbl_RevWMS_Color.Name = "Lbl_RevWMS_Color";
            this.Lbl_RevWMS_Color.Size = new System.Drawing.Size(25, 18);
            this.Lbl_RevWMS_Color.TabIndex = 2;
            this.Lbl_RevWMS_Color.Text = "R";
            this.Lbl_RevWMS_Color.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_SendWMS_Color
            // 
            this.Lbl_SendWMS_Color.BackColor = System.Drawing.Color.Lime;
            this.Lbl_SendWMS_Color.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Lbl_SendWMS_Color.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_SendWMS_Color.ForeColor = System.Drawing.Color.Black;
            this.Lbl_SendWMS_Color.Location = new System.Drawing.Point(3, 21);
            this.Lbl_SendWMS_Color.Name = "Lbl_SendWMS_Color";
            this.Lbl_SendWMS_Color.Size = new System.Drawing.Size(25, 18);
            this.Lbl_SendWMS_Color.TabIndex = 1;
            this.Lbl_SendWMS_Color.Text = "S";
            this.Lbl_SendWMS_Color.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.Black;
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label35.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(3, 3);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(50, 18);
            this.label35.TabIndex = 0;
            this.label35.Text = "WMS";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pic_Track_Picture_W_L
            // 
            this.Pic_Track_Picture_W_L.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Pic_Track_Picture_W_L.Image = global::CPL1HMI.Properties.Resources.CPL1_Tracking_W_L;
            this.Pic_Track_Picture_W_L.Location = new System.Drawing.Point(541, 119);
            this.Pic_Track_Picture_W_L.Name = "Pic_Track_Picture_W_L";
            this.Pic_Track_Picture_W_L.Size = new System.Drawing.Size(109, 147);
            this.Pic_Track_Picture_W_L.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_Track_Picture_W_L.TabIndex = 1913;
            this.Pic_Track_Picture_W_L.TabStop = false;
            this.Pic_Track_Picture_W_L.Visible = false;
            // 
            // Pnl_ShowAll
            // 
            this.Pnl_ShowAll.AutoScroll = true;
            this.Pnl_ShowAll.BackColor = System.Drawing.Color.Silver;
            this.Pnl_ShowAll.Controls.Add(this.Pnl_Spare);
            this.Pnl_ShowAll.Controls.Add(this.Pln_Por_SkidPdiData);
            this.Pnl_ShowAll.Controls.Add(this.Grb_Dividing);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_ShowData_DCar);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_ShowData_ECar);
            this.Pnl_ShowAll.Controls.Add(this.Btn_Reflash);
            this.Pnl_ShowAll.Controls.Add(this.Lbl_TrackingTitle);
            this.Pnl_ShowAll.Controls.Add(this.Lbl_LineSpeed_Unit);
            this.Pnl_ShowAll.Controls.Add(this.Lbl_LineSpeed);
            this.Pnl_ShowAll.Controls.Add(this.Lbl_LineSpeed_Title);
            this.Pnl_ShowAll.Controls.Add(this.Pic_Track_Picture_W_R);
            this.Pnl_ShowAll.Controls.Add(this.Pic_Track_Picture_W_L);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_NetworkStatus);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_Scan);
            this.Pnl_ShowAll.Controls.Add(this.Pic_Track_Picture);
            this.Pnl_ShowAll.Controls.Add(this.Grb_Leader_Strip);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_ShowData_DSK02);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_ShowData_TR);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_ShowData_DSK01);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_ShowData_ETOP);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_ShowData_ESK02);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_ShowData_ESK01);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_ShowData_POR);
            this.Pnl_ShowAll.Controls.Add(this.Lbl_ExitStatus);
            this.Pnl_ShowAll.Controls.Add(this.Lbl_CPLStatus);
            this.Pnl_ShowAll.Controls.Add(this.Lbl_EntryStatus);
            this.Pnl_ShowAll.Controls.Add(this.Lbl_ExitStatus_Title);
            this.Pnl_ShowAll.Controls.Add(this.Lbl_CPLStatus_Title);
            this.Pnl_ShowAll.Controls.Add(this.Lbl_EntryStatus_Title);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_ShowData_DTOP);
            this.Pnl_ShowAll.Controls.Add(this.Btn_AutoCoilFeed);
            this.Pnl_ShowAll.Controls.Add(this.Lbl_EntryMode);
            this.Pnl_ShowAll.Controls.Add(this.Chk_DGV_Reflash);
            this.Pnl_ShowAll.Controls.Add(this.Lbl_EntryMode_Title);
            this.Pnl_ShowAll.Controls.Add(this.Tab_GridDataControl);
            this.Pnl_ShowAll.Controls.Add(this.Grb_Trim);
            this.Pnl_ShowAll.Controls.Add(this.Pnl_NotUse_X);
            this.Pnl_ShowAll.Location = new System.Drawing.Point(0, 1);
            this.Pnl_ShowAll.Name = "Pnl_ShowAll";
            this.Pnl_ShowAll.Size = new System.Drawing.Size(1920, 982);
            this.Pnl_ShowAll.TabIndex = 1313;
            // 
            // Pnl_Spare
            // 
            this.Pnl_Spare.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Spare.Controls.Add(this.Btn_Close_Spare);
            this.Pnl_Spare.Controls.Add(this.Txt_Spare);
            this.Pnl_Spare.Controls.Add(this.Lbl_ComboName_Spare);
            this.Pnl_Spare.Location = new System.Drawing.Point(820, 321);
            this.Pnl_Spare.Name = "Pnl_Spare";
            this.Pnl_Spare.Size = new System.Drawing.Size(79, 35);
            this.Pnl_Spare.TabIndex = 1932;
            this.Pnl_Spare.Visible = false;
            // 
            // Btn_Close_Spare
            // 
            this.Btn_Close_Spare.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_Close_Spare.ForeColor = System.Drawing.Color.Red;
            this.Btn_Close_Spare.Location = new System.Drawing.Point(602, 3);
            this.Btn_Close_Spare.Name = "Btn_Close_Spare";
            this.Btn_Close_Spare.Size = new System.Drawing.Size(31, 32);
            this.Btn_Close_Spare.TabIndex = 1833;
            this.Btn_Close_Spare.Text = "X";
            this.Btn_Close_Spare.UseVisualStyleBackColor = true;
            this.Btn_Close_Spare.Click += new System.EventHandler(this.Btn_Close_Spare_Click);
            // 
            // Txt_Spare
            // 
            this.Txt_Spare.BackColor = System.Drawing.Color.White;
            this.Txt_Spare.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Txt_Spare.Font = new System.Drawing.Font("微軟正黑體", 13F, System.Drawing.FontStyle.Bold);
            this.Txt_Spare.ForeColor = System.Drawing.Color.Black;
            this.Txt_Spare.Location = new System.Drawing.Point(15, 38);
            this.Txt_Spare.Multiline = true;
            this.Txt_Spare.Name = "Txt_Spare";
            this.Txt_Spare.ReadOnly = true;
            this.Txt_Spare.Size = new System.Drawing.Size(618, 181);
            this.Txt_Spare.TabIndex = 3;
            // 
            // Lbl_ComboName_Spare
            // 
            this.Lbl_ComboName_Spare.AutoSize = true;
            this.Lbl_ComboName_Spare.Location = new System.Drawing.Point(5, 7);
            this.Lbl_ComboName_Spare.Name = "Lbl_ComboName_Spare";
            this.Lbl_ComboName_Spare.Size = new System.Drawing.Size(128, 19);
            this.Lbl_ComboName_Spare.TabIndex = 0;
            this.Lbl_ComboName_Spare.Text = "(ComboName)";
            // 
            // Pln_Por_SkidPdiData
            // 
            this.Pln_Por_SkidPdiData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_Title);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Btn_Por_Paper);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_ESK02_Title);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_ESK01_Title);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_ESK02_Paper);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_Paper_Title);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_ESK01_Paper);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_ESK02_Dividing);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_Dividing_Title);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_ESK01_Dividing);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_ESK02_Trim);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_Trim_Title);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_ESK02_Leader);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_ESK01_Trim);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_ESK02_St_No);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_ESK01_Leader);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_ESK01_St_No);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_Leader_Title);
            this.Pln_Por_SkidPdiData.Controls.Add(this.Lbl_Por_St_Title);
            this.Pln_Por_SkidPdiData.Location = new System.Drawing.Point(411, 479);
            this.Pln_Por_SkidPdiData.Name = "Pln_Por_SkidPdiData";
            this.Pln_Por_SkidPdiData.Size = new System.Drawing.Size(400, 184);
            this.Pln_Por_SkidPdiData.TabIndex = 1931;
            // 
            // Lbl_Por_Title
            // 
            this.Lbl_Por_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Por_Title.Location = new System.Drawing.Point(3, 4);
            this.Lbl_Por_Title.Name = "Lbl_Por_Title";
            this.Lbl_Por_Title.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_Title.TabIndex = 1935;
            this.Lbl_Por_Title.Text = "入口端";
            this.Lbl_Por_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Btn_Por_Paper
            // 
            this.Btn_Por_Paper.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Btn_Por_Paper.Image = global::CPL1HMI.Properties.Resources.YellowButton;
            this.Btn_Por_Paper.Location = new System.Drawing.Point(365, 146);
            this.Btn_Por_Paper.Name = "Btn_Por_Paper";
            this.Btn_Por_Paper.Size = new System.Drawing.Size(31, 32);
            this.Btn_Por_Paper.TabIndex = 1934;
            this.Btn_Por_Paper.Text = "?";
            this.Btn_Por_Paper.UseVisualStyleBackColor = true;
            this.Btn_Por_Paper.Click += new System.EventHandler(this.Btn_Por_Paper_Click);
            // 
            // Lbl_Por_ESK02_Title
            // 
            this.Lbl_Por_ESK02_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_ESK02_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_ESK02_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Por_ESK02_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_Por_ESK02_Title.Location = new System.Drawing.Point(243, 4);
            this.Lbl_Por_ESK02_Title.Name = "Lbl_Por_ESK02_Title";
            this.Lbl_Por_ESK02_Title.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_ESK02_Title.TabIndex = 1933;
            this.Lbl_Por_ESK02_Title.Text = "Skid2";
            this.Lbl_Por_ESK02_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Por_ESK01_Title
            // 
            this.Lbl_Por_ESK01_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_ESK01_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_ESK01_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Por_ESK01_Title.ForeColor = System.Drawing.Color.Blue;
            this.Lbl_Por_ESK01_Title.Location = new System.Drawing.Point(123, 4);
            this.Lbl_Por_ESK01_Title.Name = "Lbl_Por_ESK01_Title";
            this.Lbl_Por_ESK01_Title.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_ESK01_Title.TabIndex = 1933;
            this.Lbl_Por_ESK01_Title.Text = "Skid1";
            this.Lbl_Por_ESK01_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Por_ESK02_Paper
            // 
            this.Lbl_Por_ESK02_Paper.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_ESK02_Paper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_ESK02_Paper.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Por_ESK02_Paper.Location = new System.Drawing.Point(243, 149);
            this.Lbl_Por_ESK02_Paper.Name = "Lbl_Por_ESK02_Paper";
            this.Lbl_Por_ESK02_Paper.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_ESK02_Paper.TabIndex = 1929;
            this.Lbl_Por_ESK02_Paper.Text = "02";
            this.Lbl_Por_ESK02_Paper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Por_Paper_Title
            // 
            this.Lbl_Por_Paper_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_Paper_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_Paper_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Por_Paper_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Por_Paper_Title.Location = new System.Drawing.Point(3, 149);
            this.Lbl_Por_Paper_Title.Name = "Lbl_Por_Paper_Title";
            this.Lbl_Por_Paper_Title.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_Paper_Title.TabIndex = 1930;
            this.Lbl_Por_Paper_Title.Text = "垫纸类型";
            this.Lbl_Por_Paper_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Por_ESK01_Paper
            // 
            this.Lbl_Por_ESK01_Paper.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_ESK01_Paper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_ESK01_Paper.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Por_ESK01_Paper.Location = new System.Drawing.Point(123, 149);
            this.Lbl_Por_ESK01_Paper.Name = "Lbl_Por_ESK01_Paper";
            this.Lbl_Por_ESK01_Paper.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_ESK01_Paper.TabIndex = 1929;
            this.Lbl_Por_ESK01_Paper.Text = "01";
            this.Lbl_Por_ESK01_Paper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Por_ESK02_Dividing
            // 
            this.Lbl_Por_ESK02_Dividing.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_ESK02_Dividing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_ESK02_Dividing.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Por_ESK02_Dividing.Location = new System.Drawing.Point(243, 120);
            this.Lbl_Por_ESK02_Dividing.Name = "Lbl_Por_ESK02_Dividing";
            this.Lbl_Por_ESK02_Dividing.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_ESK02_Dividing.TabIndex = 1932;
            this.Lbl_Por_ESK02_Dividing.Text = "是/否";
            this.Lbl_Por_ESK02_Dividing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Por_Dividing_Title
            // 
            this.Lbl_Por_Dividing_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_Dividing_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_Dividing_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Por_Dividing_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Por_Dividing_Title.Location = new System.Drawing.Point(3, 120);
            this.Lbl_Por_Dividing_Title.Name = "Lbl_Por_Dividing_Title";
            this.Lbl_Por_Dividing_Title.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_Dividing_Title.TabIndex = 1931;
            this.Lbl_Por_Dividing_Title.Text = "分卷";
            this.Lbl_Por_Dividing_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Por_ESK01_Dividing
            // 
            this.Lbl_Por_ESK01_Dividing.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_ESK01_Dividing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_ESK01_Dividing.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Por_ESK01_Dividing.Location = new System.Drawing.Point(123, 120);
            this.Lbl_Por_ESK01_Dividing.Name = "Lbl_Por_ESK01_Dividing";
            this.Lbl_Por_ESK01_Dividing.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_ESK01_Dividing.TabIndex = 1932;
            this.Lbl_Por_ESK01_Dividing.Text = "是/否";
            this.Lbl_Por_ESK01_Dividing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Por_ESK02_Trim
            // 
            this.Lbl_Por_ESK02_Trim.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_ESK02_Trim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_ESK02_Trim.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Por_ESK02_Trim.Location = new System.Drawing.Point(243, 91);
            this.Lbl_Por_ESK02_Trim.Name = "Lbl_Por_ESK02_Trim";
            this.Lbl_Por_ESK02_Trim.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_ESK02_Trim.TabIndex = 1928;
            this.Lbl_Por_ESK02_Trim.Text = "是/否";
            this.Lbl_Por_ESK02_Trim.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Por_Trim_Title
            // 
            this.Lbl_Por_Trim_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_Trim_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_Trim_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Por_Trim_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Por_Trim_Title.Location = new System.Drawing.Point(3, 91);
            this.Lbl_Por_Trim_Title.Name = "Lbl_Por_Trim_Title";
            this.Lbl_Por_Trim_Title.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_Trim_Title.TabIndex = 1927;
            this.Lbl_Por_Trim_Title.Text = "修边";
            this.Lbl_Por_Trim_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Por_ESK02_Leader
            // 
            this.Lbl_Por_ESK02_Leader.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_ESK02_Leader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_ESK02_Leader.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Por_ESK02_Leader.Location = new System.Drawing.Point(243, 62);
            this.Lbl_Por_ESK02_Leader.Name = "Lbl_Por_ESK02_Leader";
            this.Lbl_Por_ESK02_Leader.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_ESK02_Leader.TabIndex = 1926;
            this.Lbl_Por_ESK02_Leader.Text = "是/否";
            this.Lbl_Por_ESK02_Leader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Por_ESK01_Trim
            // 
            this.Lbl_Por_ESK01_Trim.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_ESK01_Trim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_ESK01_Trim.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Por_ESK01_Trim.Location = new System.Drawing.Point(123, 91);
            this.Lbl_Por_ESK01_Trim.Name = "Lbl_Por_ESK01_Trim";
            this.Lbl_Por_ESK01_Trim.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_ESK01_Trim.TabIndex = 1928;
            this.Lbl_Por_ESK01_Trim.Text = "是/否";
            this.Lbl_Por_ESK01_Trim.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Por_ESK02_St_No
            // 
            this.Lbl_Por_ESK02_St_No.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_ESK02_St_No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_ESK02_St_No.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Por_ESK02_St_No.Location = new System.Drawing.Point(243, 33);
            this.Lbl_Por_ESK02_St_No.Name = "Lbl_Por_ESK02_St_No";
            this.Lbl_Por_ESK02_St_No.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_ESK02_St_No.TabIndex = 1924;
            this.Lbl_Por_ESK02_St_No.Text = "ST";
            this.Lbl_Por_ESK02_St_No.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Por_ESK01_Leader
            // 
            this.Lbl_Por_ESK01_Leader.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_ESK01_Leader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_ESK01_Leader.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Por_ESK01_Leader.Location = new System.Drawing.Point(123, 62);
            this.Lbl_Por_ESK01_Leader.Name = "Lbl_Por_ESK01_Leader";
            this.Lbl_Por_ESK01_Leader.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_ESK01_Leader.TabIndex = 1926;
            this.Lbl_Por_ESK01_Leader.Text = "是/否";
            this.Lbl_Por_ESK01_Leader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Por_ESK01_St_No
            // 
            this.Lbl_Por_ESK01_St_No.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_ESK01_St_No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_ESK01_St_No.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Por_ESK01_St_No.Location = new System.Drawing.Point(123, 33);
            this.Lbl_Por_ESK01_St_No.Name = "Lbl_Por_ESK01_St_No";
            this.Lbl_Por_ESK01_St_No.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_ESK01_St_No.TabIndex = 1924;
            this.Lbl_Por_ESK01_St_No.Text = "ST";
            this.Lbl_Por_ESK01_St_No.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Por_Leader_Title
            // 
            this.Lbl_Por_Leader_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_Leader_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_Leader_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Por_Leader_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Por_Leader_Title.Location = new System.Drawing.Point(3, 62);
            this.Lbl_Por_Leader_Title.Name = "Lbl_Por_Leader_Title";
            this.Lbl_Por_Leader_Title.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_Leader_Title.TabIndex = 1925;
            this.Lbl_Por_Leader_Title.Text = "焊接导带";
            this.Lbl_Por_Leader_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Por_St_Title
            // 
            this.Lbl_Por_St_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Por_St_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Por_St_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Por_St_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Por_St_Title.Location = new System.Drawing.Point(3, 33);
            this.Lbl_Por_St_Title.Name = "Lbl_Por_St_Title";
            this.Lbl_Por_St_Title.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Por_St_Title.TabIndex = 1923;
            this.Lbl_Por_St_Title.Text = "钢种";
            this.Lbl_Por_St_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Grb_Dividing
            // 
            this.Grb_Dividing.BorderColor = System.Drawing.Color.White;
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_2_Unit);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_3_Unit);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_4_Unit);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_5_Unit);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_6_Unit);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_1_Unit);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_6);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_5);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_4);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_6_Title);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_5_Title);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_4_Title);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_3);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_2);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_1);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_3_Title);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_2_Title);
            this.Grb_Dividing.Controls.Add(this.Lbl_Order_Weight_1_Title);
            this.Grb_Dividing.Controls.Add(this.Lbl_Dividing_num1);
            this.Grb_Dividing.Controls.Add(this.Lbl_Dividing_num_Title);
            this.Grb_Dividing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Grb_Dividing.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Grb_Dividing.Location = new System.Drawing.Point(1229, 399);
            this.Grb_Dividing.Name = "Grb_Dividing";
            this.Grb_Dividing.Size = new System.Drawing.Size(288, 264);
            this.Grb_Dividing.TabIndex = 1391;
            this.Grb_Dividing.TabStop = false;
            this.Grb_Dividing.Text = "分卷标记";
            // 
            // Lbl_Order_Weight_2_Unit
            // 
            this.Lbl_Order_Weight_2_Unit.AutoSize = true;
            this.Lbl_Order_Weight_2_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Order_Weight_2_Unit.Location = new System.Drawing.Point(241, 90);
            this.Lbl_Order_Weight_2_Unit.Name = "Lbl_Order_Weight_2_Unit";
            this.Lbl_Order_Weight_2_Unit.Size = new System.Drawing.Size(34, 24);
            this.Lbl_Order_Weight_2_Unit.TabIndex = 1874;
            this.Lbl_Order_Weight_2_Unit.Text = "Kg";
            // 
            // Lbl_Order_Weight_3_Unit
            // 
            this.Lbl_Order_Weight_3_Unit.AutoSize = true;
            this.Lbl_Order_Weight_3_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Order_Weight_3_Unit.Location = new System.Drawing.Point(241, 119);
            this.Lbl_Order_Weight_3_Unit.Name = "Lbl_Order_Weight_3_Unit";
            this.Lbl_Order_Weight_3_Unit.Size = new System.Drawing.Size(34, 24);
            this.Lbl_Order_Weight_3_Unit.TabIndex = 1873;
            this.Lbl_Order_Weight_3_Unit.Text = "Kg";
            // 
            // Lbl_Order_Weight_4_Unit
            // 
            this.Lbl_Order_Weight_4_Unit.AutoSize = true;
            this.Lbl_Order_Weight_4_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Order_Weight_4_Unit.Location = new System.Drawing.Point(241, 148);
            this.Lbl_Order_Weight_4_Unit.Name = "Lbl_Order_Weight_4_Unit";
            this.Lbl_Order_Weight_4_Unit.Size = new System.Drawing.Size(34, 24);
            this.Lbl_Order_Weight_4_Unit.TabIndex = 1872;
            this.Lbl_Order_Weight_4_Unit.Text = "Kg";
            // 
            // Lbl_Order_Weight_5_Unit
            // 
            this.Lbl_Order_Weight_5_Unit.AutoSize = true;
            this.Lbl_Order_Weight_5_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Order_Weight_5_Unit.Location = new System.Drawing.Point(241, 177);
            this.Lbl_Order_Weight_5_Unit.Name = "Lbl_Order_Weight_5_Unit";
            this.Lbl_Order_Weight_5_Unit.Size = new System.Drawing.Size(34, 24);
            this.Lbl_Order_Weight_5_Unit.TabIndex = 1871;
            this.Lbl_Order_Weight_5_Unit.Text = "Kg";
            // 
            // Lbl_Order_Weight_6_Unit
            // 
            this.Lbl_Order_Weight_6_Unit.AutoSize = true;
            this.Lbl_Order_Weight_6_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Order_Weight_6_Unit.Location = new System.Drawing.Point(241, 206);
            this.Lbl_Order_Weight_6_Unit.Name = "Lbl_Order_Weight_6_Unit";
            this.Lbl_Order_Weight_6_Unit.Size = new System.Drawing.Size(34, 24);
            this.Lbl_Order_Weight_6_Unit.TabIndex = 1870;
            this.Lbl_Order_Weight_6_Unit.Text = "Kg";
            // 
            // Lbl_Order_Weight_1_Unit
            // 
            this.Lbl_Order_Weight_1_Unit.AutoSize = true;
            this.Lbl_Order_Weight_1_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Order_Weight_1_Unit.Location = new System.Drawing.Point(241, 61);
            this.Lbl_Order_Weight_1_Unit.Name = "Lbl_Order_Weight_1_Unit";
            this.Lbl_Order_Weight_1_Unit.Size = new System.Drawing.Size(34, 24);
            this.Lbl_Order_Weight_1_Unit.TabIndex = 1869;
            this.Lbl_Order_Weight_1_Unit.Text = "Kg";
            // 
            // Lbl_Order_Weight_6
            // 
            this.Lbl_Order_Weight_6.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Order_Weight_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Order_Weight_6.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Order_Weight_6.Location = new System.Drawing.Point(151, 204);
            this.Lbl_Order_Weight_6.Name = "Lbl_Order_Weight_6";
            this.Lbl_Order_Weight_6.Size = new System.Drawing.Size(90, 29);
            this.Lbl_Order_Weight_6.TabIndex = 1411;
            this.Lbl_Order_Weight_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Order_Weight_5
            // 
            this.Lbl_Order_Weight_5.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Order_Weight_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Order_Weight_5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Order_Weight_5.Location = new System.Drawing.Point(151, 175);
            this.Lbl_Order_Weight_5.Name = "Lbl_Order_Weight_5";
            this.Lbl_Order_Weight_5.Size = new System.Drawing.Size(90, 29);
            this.Lbl_Order_Weight_5.TabIndex = 1410;
            this.Lbl_Order_Weight_5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Order_Weight_4
            // 
            this.Lbl_Order_Weight_4.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Order_Weight_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Order_Weight_4.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Order_Weight_4.Location = new System.Drawing.Point(151, 146);
            this.Lbl_Order_Weight_4.Name = "Lbl_Order_Weight_4";
            this.Lbl_Order_Weight_4.Size = new System.Drawing.Size(90, 29);
            this.Lbl_Order_Weight_4.TabIndex = 1409;
            this.Lbl_Order_Weight_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Order_Weight_6_Title
            // 
            this.Lbl_Order_Weight_6_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Order_Weight_6_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Order_Weight_6_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Order_Weight_6_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Order_Weight_6_Title.Location = new System.Drawing.Point(6, 204);
            this.Lbl_Order_Weight_6_Title.Name = "Lbl_Order_Weight_6_Title";
            this.Lbl_Order_Weight_6_Title.Size = new System.Drawing.Size(145, 29);
            this.Lbl_Order_Weight_6_Title.TabIndex = 1408;
            this.Lbl_Order_Weight_6_Title.Text = "目标重量 6";
            this.Lbl_Order_Weight_6_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Order_Weight_5_Title
            // 
            this.Lbl_Order_Weight_5_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Order_Weight_5_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Order_Weight_5_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Order_Weight_5_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Order_Weight_5_Title.Location = new System.Drawing.Point(6, 175);
            this.Lbl_Order_Weight_5_Title.Name = "Lbl_Order_Weight_5_Title";
            this.Lbl_Order_Weight_5_Title.Size = new System.Drawing.Size(145, 29);
            this.Lbl_Order_Weight_5_Title.TabIndex = 1407;
            this.Lbl_Order_Weight_5_Title.Text = "目标重量 5";
            this.Lbl_Order_Weight_5_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Order_Weight_4_Title
            // 
            this.Lbl_Order_Weight_4_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Order_Weight_4_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Order_Weight_4_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Order_Weight_4_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Order_Weight_4_Title.Location = new System.Drawing.Point(6, 146);
            this.Lbl_Order_Weight_4_Title.Name = "Lbl_Order_Weight_4_Title";
            this.Lbl_Order_Weight_4_Title.Size = new System.Drawing.Size(145, 29);
            this.Lbl_Order_Weight_4_Title.TabIndex = 1406;
            this.Lbl_Order_Weight_4_Title.Text = "目标重量 4";
            this.Lbl_Order_Weight_4_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Order_Weight_3
            // 
            this.Lbl_Order_Weight_3.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Order_Weight_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Order_Weight_3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Order_Weight_3.Location = new System.Drawing.Point(151, 117);
            this.Lbl_Order_Weight_3.Name = "Lbl_Order_Weight_3";
            this.Lbl_Order_Weight_3.Size = new System.Drawing.Size(90, 29);
            this.Lbl_Order_Weight_3.TabIndex = 1405;
            this.Lbl_Order_Weight_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Order_Weight_2
            // 
            this.Lbl_Order_Weight_2.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Order_Weight_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Order_Weight_2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Order_Weight_2.Location = new System.Drawing.Point(151, 88);
            this.Lbl_Order_Weight_2.Name = "Lbl_Order_Weight_2";
            this.Lbl_Order_Weight_2.Size = new System.Drawing.Size(90, 29);
            this.Lbl_Order_Weight_2.TabIndex = 1404;
            this.Lbl_Order_Weight_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Order_Weight_1
            // 
            this.Lbl_Order_Weight_1.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Order_Weight_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Order_Weight_1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Order_Weight_1.Location = new System.Drawing.Point(151, 59);
            this.Lbl_Order_Weight_1.Name = "Lbl_Order_Weight_1";
            this.Lbl_Order_Weight_1.Size = new System.Drawing.Size(90, 29);
            this.Lbl_Order_Weight_1.TabIndex = 1403;
            this.Lbl_Order_Weight_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Order_Weight_3_Title
            // 
            this.Lbl_Order_Weight_3_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Order_Weight_3_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Order_Weight_3_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Order_Weight_3_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Order_Weight_3_Title.Location = new System.Drawing.Point(6, 117);
            this.Lbl_Order_Weight_3_Title.Name = "Lbl_Order_Weight_3_Title";
            this.Lbl_Order_Weight_3_Title.Size = new System.Drawing.Size(145, 29);
            this.Lbl_Order_Weight_3_Title.TabIndex = 1402;
            this.Lbl_Order_Weight_3_Title.Text = "目标重量 3";
            this.Lbl_Order_Weight_3_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Order_Weight_2_Title
            // 
            this.Lbl_Order_Weight_2_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Order_Weight_2_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Order_Weight_2_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Order_Weight_2_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Order_Weight_2_Title.Location = new System.Drawing.Point(6, 88);
            this.Lbl_Order_Weight_2_Title.Name = "Lbl_Order_Weight_2_Title";
            this.Lbl_Order_Weight_2_Title.Size = new System.Drawing.Size(145, 29);
            this.Lbl_Order_Weight_2_Title.TabIndex = 1401;
            this.Lbl_Order_Weight_2_Title.Text = "目标重量 2";
            this.Lbl_Order_Weight_2_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Order_Weight_1_Title
            // 
            this.Lbl_Order_Weight_1_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Order_Weight_1_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Order_Weight_1_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Order_Weight_1_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Order_Weight_1_Title.Location = new System.Drawing.Point(6, 59);
            this.Lbl_Order_Weight_1_Title.Name = "Lbl_Order_Weight_1_Title";
            this.Lbl_Order_Weight_1_Title.Size = new System.Drawing.Size(145, 29);
            this.Lbl_Order_Weight_1_Title.TabIndex = 1400;
            this.Lbl_Order_Weight_1_Title.Text = "目标重量 1";
            this.Lbl_Order_Weight_1_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Dividing_num1
            // 
            this.Lbl_Dividing_num1.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Dividing_num1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Dividing_num1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Dividing_num1.Location = new System.Drawing.Point(151, 30);
            this.Lbl_Dividing_num1.Name = "Lbl_Dividing_num1";
            this.Lbl_Dividing_num1.Size = new System.Drawing.Size(90, 29);
            this.Lbl_Dividing_num1.TabIndex = 1399;
            this.Lbl_Dividing_num1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Dividing_num_Title
            // 
            this.Lbl_Dividing_num_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Dividing_num_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Dividing_num_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Dividing_num_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Dividing_num_Title.Location = new System.Drawing.Point(6, 30);
            this.Lbl_Dividing_num_Title.Name = "Lbl_Dividing_num_Title";
            this.Lbl_Dividing_num_Title.Size = new System.Drawing.Size(145, 29);
            this.Lbl_Dividing_num_Title.TabIndex = 1398;
            this.Lbl_Dividing_num_Title.Text = "分卷数量";
            this.Lbl_Dividing_num_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Pic_Track_Picture_W_R
            // 
            this.Pic_Track_Picture_W_R.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Pic_Track_Picture_W_R.Image = global::CPL1HMI.Properties.Resources.CPL1_Tracking_W_R;
            this.Pic_Track_Picture_W_R.Location = new System.Drawing.Point(1386, 168);
            this.Pic_Track_Picture_W_R.Name = "Pic_Track_Picture_W_R";
            this.Pic_Track_Picture_W_R.Size = new System.Drawing.Size(106, 114);
            this.Pic_Track_Picture_W_R.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_Track_Picture_W_R.TabIndex = 1914;
            this.Pic_Track_Picture_W_R.TabStop = false;
            this.Pic_Track_Picture_W_R.Visible = false;
            // 
            // Grb_Leader_Strip
            // 
            this.Grb_Leader_Strip.BorderColor = System.Drawing.Color.White;
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Head_Thickness_Unit);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Head_Width_Unit);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Head_Length_Unit);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Leader_Strip_Tail_Title);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Leader_Strip_Head_Title);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Tail_Thickness);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Tail_Width);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Tail_Length);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Head_Thickness);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Head_Thickness_Title);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Head_Width);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Head_Width_Title);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Head_Length);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Head_Length_Title);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Tail_St_no);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Head_St_no);
            this.Grb_Leader_Strip.Controls.Add(this.Lbl_Head_St_no_Title);
            this.Grb_Leader_Strip.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Grb_Leader_Strip.Location = new System.Drawing.Point(823, 473);
            this.Grb_Leader_Strip.Name = "Grb_Leader_Strip";
            this.Grb_Leader_Strip.Size = new System.Drawing.Size(400, 190);
            this.Grb_Leader_Strip.TabIndex = 1414;
            this.Grb_Leader_Strip.TabStop = false;
            this.Grb_Leader_Strip.Text = "导带";
            // 
            // Lbl_Head_Thickness_Unit
            // 
            this.Lbl_Head_Thickness_Unit.AutoSize = true;
            this.Lbl_Head_Thickness_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Head_Thickness_Unit.Location = new System.Drawing.Point(350, 142);
            this.Lbl_Head_Thickness_Unit.Name = "Lbl_Head_Thickness_Unit";
            this.Lbl_Head_Thickness_Unit.Size = new System.Drawing.Size(46, 24);
            this.Lbl_Head_Thickness_Unit.TabIndex = 1873;
            this.Lbl_Head_Thickness_Unit.Text = "mm";
            // 
            // Lbl_Head_Width_Unit
            // 
            this.Lbl_Head_Width_Unit.AutoSize = true;
            this.Lbl_Head_Width_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Head_Width_Unit.Location = new System.Drawing.Point(350, 113);
            this.Lbl_Head_Width_Unit.Name = "Lbl_Head_Width_Unit";
            this.Lbl_Head_Width_Unit.Size = new System.Drawing.Size(46, 24);
            this.Lbl_Head_Width_Unit.TabIndex = 1871;
            this.Lbl_Head_Width_Unit.Text = "mm";
            // 
            // Lbl_Head_Length_Unit
            // 
            this.Lbl_Head_Length_Unit.AutoSize = true;
            this.Lbl_Head_Length_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Head_Length_Unit.Location = new System.Drawing.Point(350, 84);
            this.Lbl_Head_Length_Unit.Name = "Lbl_Head_Length_Unit";
            this.Lbl_Head_Length_Unit.Size = new System.Drawing.Size(46, 24);
            this.Lbl_Head_Length_Unit.TabIndex = 1869;
            this.Lbl_Head_Length_Unit.Text = "mm";
            // 
            // Lbl_Leader_Strip_Tail_Title
            // 
            this.Lbl_Leader_Strip_Tail_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Leader_Strip_Tail_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Leader_Strip_Tail_Title.Location = new System.Drawing.Point(236, 21);
            this.Lbl_Leader_Strip_Tail_Title.Name = "Lbl_Leader_Strip_Tail_Title";
            this.Lbl_Leader_Strip_Tail_Title.Size = new System.Drawing.Size(110, 29);
            this.Lbl_Leader_Strip_Tail_Title.TabIndex = 1428;
            this.Lbl_Leader_Strip_Tail_Title.Text = "尾段";
            // 
            // Lbl_Leader_Strip_Head_Title
            // 
            this.Lbl_Leader_Strip_Head_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Leader_Strip_Head_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Leader_Strip_Head_Title.Location = new System.Drawing.Point(126, 21);
            this.Lbl_Leader_Strip_Head_Title.Name = "Lbl_Leader_Strip_Head_Title";
            this.Lbl_Leader_Strip_Head_Title.Size = new System.Drawing.Size(110, 29);
            this.Lbl_Leader_Strip_Head_Title.TabIndex = 1425;
            this.Lbl_Leader_Strip_Head_Title.Text = "头段";
            // 
            // Lbl_Tail_Thickness
            // 
            this.Lbl_Tail_Thickness.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Tail_Thickness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Tail_Thickness.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Tail_Thickness.Location = new System.Drawing.Point(236, 137);
            this.Lbl_Tail_Thickness.Name = "Lbl_Tail_Thickness";
            this.Lbl_Tail_Thickness.Size = new System.Drawing.Size(110, 29);
            this.Lbl_Tail_Thickness.TabIndex = 1427;
            this.Lbl_Tail_Thickness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Tail_Width
            // 
            this.Lbl_Tail_Width.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Tail_Width.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Tail_Width.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Tail_Width.Location = new System.Drawing.Point(236, 108);
            this.Lbl_Tail_Width.Name = "Lbl_Tail_Width";
            this.Lbl_Tail_Width.Size = new System.Drawing.Size(110, 29);
            this.Lbl_Tail_Width.TabIndex = 1425;
            this.Lbl_Tail_Width.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Tail_Length
            // 
            this.Lbl_Tail_Length.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Tail_Length.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Tail_Length.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Tail_Length.Location = new System.Drawing.Point(236, 79);
            this.Lbl_Tail_Length.Name = "Lbl_Tail_Length";
            this.Lbl_Tail_Length.Size = new System.Drawing.Size(110, 29);
            this.Lbl_Tail_Length.TabIndex = 1423;
            this.Lbl_Tail_Length.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Head_Thickness
            // 
            this.Lbl_Head_Thickness.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Head_Thickness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Head_Thickness.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Head_Thickness.Location = new System.Drawing.Point(126, 137);
            this.Lbl_Head_Thickness.Name = "Lbl_Head_Thickness";
            this.Lbl_Head_Thickness.Size = new System.Drawing.Size(110, 29);
            this.Lbl_Head_Thickness.TabIndex = 1421;
            this.Lbl_Head_Thickness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Head_Thickness_Title
            // 
            this.Lbl_Head_Thickness_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Head_Thickness_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Head_Thickness_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Head_Thickness_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Head_Thickness_Title.Location = new System.Drawing.Point(6, 137);
            this.Lbl_Head_Thickness_Title.Name = "Lbl_Head_Thickness_Title";
            this.Lbl_Head_Thickness_Title.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Head_Thickness_Title.TabIndex = 1420;
            this.Lbl_Head_Thickness_Title.Text = "厚度";
            this.Lbl_Head_Thickness_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Head_Width
            // 
            this.Lbl_Head_Width.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Head_Width.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Head_Width.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Head_Width.Location = new System.Drawing.Point(126, 108);
            this.Lbl_Head_Width.Name = "Lbl_Head_Width";
            this.Lbl_Head_Width.Size = new System.Drawing.Size(110, 29);
            this.Lbl_Head_Width.TabIndex = 1419;
            this.Lbl_Head_Width.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Head_Width_Title
            // 
            this.Lbl_Head_Width_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Head_Width_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Head_Width_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Head_Width_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Head_Width_Title.Location = new System.Drawing.Point(6, 108);
            this.Lbl_Head_Width_Title.Name = "Lbl_Head_Width_Title";
            this.Lbl_Head_Width_Title.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Head_Width_Title.TabIndex = 1418;
            this.Lbl_Head_Width_Title.Text = "宽度";
            this.Lbl_Head_Width_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Head_Length
            // 
            this.Lbl_Head_Length.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Head_Length.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Head_Length.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Head_Length.Location = new System.Drawing.Point(126, 79);
            this.Lbl_Head_Length.Name = "Lbl_Head_Length";
            this.Lbl_Head_Length.Size = new System.Drawing.Size(110, 29);
            this.Lbl_Head_Length.TabIndex = 1417;
            this.Lbl_Head_Length.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Head_Length_Title
            // 
            this.Lbl_Head_Length_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Head_Length_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Head_Length_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Head_Length_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Head_Length_Title.Location = new System.Drawing.Point(6, 79);
            this.Lbl_Head_Length_Title.Name = "Lbl_Head_Length_Title";
            this.Lbl_Head_Length_Title.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Head_Length_Title.TabIndex = 1416;
            this.Lbl_Head_Length_Title.Text = "长度";
            this.Lbl_Head_Length_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Tail_St_no
            // 
            this.Lbl_Tail_St_no.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Tail_St_no.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Tail_St_no.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Tail_St_no.Location = new System.Drawing.Point(236, 50);
            this.Lbl_Tail_St_no.Name = "Lbl_Tail_St_no";
            this.Lbl_Tail_St_no.Size = new System.Drawing.Size(110, 29);
            this.Lbl_Tail_St_no.TabIndex = 1415;
            this.Lbl_Tail_St_no.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Head_St_no
            // 
            this.Lbl_Head_St_no.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Head_St_no.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Head_St_no.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Head_St_no.Location = new System.Drawing.Point(126, 50);
            this.Lbl_Head_St_no.Name = "Lbl_Head_St_no";
            this.Lbl_Head_St_no.Size = new System.Drawing.Size(110, 29);
            this.Lbl_Head_St_no.TabIndex = 1413;
            this.Lbl_Head_St_no.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_Head_St_no_Title
            // 
            this.Lbl_Head_St_no_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Head_St_no_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Head_St_no_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_Head_St_no_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Head_St_no_Title.Location = new System.Drawing.Point(6, 50);
            this.Lbl_Head_St_no_Title.Name = "Lbl_Head_St_no_Title";
            this.Lbl_Head_St_no_Title.Size = new System.Drawing.Size(120, 29);
            this.Lbl_Head_St_no_Title.TabIndex = 1412;
            this.Lbl_Head_St_no_Title.Text = "钢种";
            this.Lbl_Head_St_no_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Grb_Trim
            // 
            this.Grb_Trim.BorderColor = System.Drawing.Color.White;
            this.Grb_Trim.Controls.Add(this.Lbl_WidthMax_Unit);
            this.Grb_Trim.Controls.Add(this.Lbl_WidthMin_Unit);
            this.Grb_Trim.Controls.Add(this.Lbl_OutWidth_Unit);
            this.Grb_Trim.Controls.Add(this.Lbl_WidthMin);
            this.Grb_Trim.Controls.Add(this.Lbl_OutWidth_Title);
            this.Grb_Trim.Controls.Add(this.Lbl_WidthMin_Title);
            this.Grb_Trim.Controls.Add(this.Lbl_OutWidth);
            this.Grb_Trim.Controls.Add(this.Lbl_WidthMax);
            this.Grb_Trim.Controls.Add(this.Lbl_WidthMax_Title);
            this.Grb_Trim.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Grb_Trim.Location = new System.Drawing.Point(413, 399);
            this.Grb_Trim.Name = "Grb_Trim";
            this.Grb_Trim.Size = new System.Drawing.Size(797, 68);
            this.Grb_Trim.TabIndex = 1423;
            this.Grb_Trim.TabStop = false;
            this.Grb_Trim.Text = "裁边(仅CPL#1)";
            // 
            // Lbl_WidthMax_Unit
            // 
            this.Lbl_WidthMax_Unit.AutoSize = true;
            this.Lbl_WidthMax_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_WidthMax_Unit.Location = new System.Drawing.Point(472, 32);
            this.Lbl_WidthMax_Unit.Name = "Lbl_WidthMax_Unit";
            this.Lbl_WidthMax_Unit.Size = new System.Drawing.Size(46, 24);
            this.Lbl_WidthMax_Unit.TabIndex = 1868;
            this.Lbl_WidthMax_Unit.Text = "mm";
            // 
            // Lbl_WidthMin_Unit
            // 
            this.Lbl_WidthMin_Unit.AutoSize = true;
            this.Lbl_WidthMin_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_WidthMin_Unit.Location = new System.Drawing.Point(738, 32);
            this.Lbl_WidthMin_Unit.Name = "Lbl_WidthMin_Unit";
            this.Lbl_WidthMin_Unit.Size = new System.Drawing.Size(46, 24);
            this.Lbl_WidthMin_Unit.TabIndex = 1867;
            this.Lbl_WidthMin_Unit.Text = "mm";
            // 
            // Lbl_OutWidth_Unit
            // 
            this.Lbl_OutWidth_Unit.AutoSize = true;
            this.Lbl_OutWidth_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_OutWidth_Unit.Location = new System.Drawing.Point(206, 32);
            this.Lbl_OutWidth_Unit.Name = "Lbl_OutWidth_Unit";
            this.Lbl_OutWidth_Unit.Size = new System.Drawing.Size(46, 24);
            this.Lbl_OutWidth_Unit.TabIndex = 1866;
            this.Lbl_OutWidth_Unit.Text = "mm";
            // 
            // Lbl_WidthMin
            // 
            this.Lbl_WidthMin.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_WidthMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_WidthMin.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_WidthMin.Location = new System.Drawing.Point(648, 30);
            this.Lbl_WidthMin.Name = "Lbl_WidthMin";
            this.Lbl_WidthMin.Size = new System.Drawing.Size(90, 29);
            this.Lbl_WidthMin.TabIndex = 1421;
            this.Lbl_WidthMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_OutWidth_Title
            // 
            this.Lbl_OutWidth_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_OutWidth_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_OutWidth_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_OutWidth_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_OutWidth_Title.Location = new System.Drawing.Point(6, 30);
            this.Lbl_OutWidth_Title.Name = "Lbl_OutWidth_Title";
            this.Lbl_OutWidth_Title.Size = new System.Drawing.Size(110, 29);
            this.Lbl_OutWidth_Title.TabIndex = 1416;
            this.Lbl_OutWidth_Title.Text = "目标宽度";
            this.Lbl_OutWidth_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_WidthMin_Title
            // 
            this.Lbl_WidthMin_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_WidthMin_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_WidthMin_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_WidthMin_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_WidthMin_Title.Location = new System.Drawing.Point(538, 30);
            this.Lbl_WidthMin_Title.Name = "Lbl_WidthMin_Title";
            this.Lbl_WidthMin_Title.Size = new System.Drawing.Size(110, 29);
            this.Lbl_WidthMin_Title.TabIndex = 1420;
            this.Lbl_WidthMin_Title.Text = "最小值";
            this.Lbl_WidthMin_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_OutWidth
            // 
            this.Lbl_OutWidth.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_OutWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_OutWidth.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_OutWidth.Location = new System.Drawing.Point(116, 30);
            this.Lbl_OutWidth.Name = "Lbl_OutWidth";
            this.Lbl_OutWidth.Size = new System.Drawing.Size(90, 29);
            this.Lbl_OutWidth.TabIndex = 1417;
            this.Lbl_OutWidth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_WidthMax
            // 
            this.Lbl_WidthMax.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_WidthMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_WidthMax.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_WidthMax.Location = new System.Drawing.Point(382, 30);
            this.Lbl_WidthMax.Name = "Lbl_WidthMax";
            this.Lbl_WidthMax.Size = new System.Drawing.Size(90, 29);
            this.Lbl_WidthMax.TabIndex = 1419;
            this.Lbl_WidthMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_WidthMax_Title
            // 
            this.Lbl_WidthMax_Title.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_WidthMax_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_WidthMax_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Lbl_WidthMax_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_WidthMax_Title.Location = new System.Drawing.Point(272, 30);
            this.Lbl_WidthMax_Title.Name = "Lbl_WidthMax_Title";
            this.Lbl_WidthMax_Title.Size = new System.Drawing.Size(110, 29);
            this.Lbl_WidthMax_Title.TabIndex = 1418;
            this.Lbl_WidthMax_Title.Text = "最大值";
            this.Lbl_WidthMax_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Frm_2_1_Tracking
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1920, 980);
            this.Controls.Add(this.Pnl_ShowAll);
            this.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_2_1_Tracking";
            this.Text = "frm_2_1_Tracking";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.Frm_2_1_Tracking_Load);
            this.Menu_AckPDI.ResumeLayout(false);
            this.Pnl_NotUse_X.ResumeLayout(false);
            this.Pnl_Weight_X.ResumeLayout(false);
            this.Pnl_Weight_X.PerformLayout();
            this.Tab_GridDataControl.ResumeLayout(false);
            this.Tab_SchedulePage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_OffLine)).EndInit();
            this.Tab_OnlinePage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_OnLine)).EndInit();
            this.Pnl_ShowData_DTOP.ResumeLayout(false);
            this.Pnl_ShowData_DTOP.PerformLayout();
            this.Pnl_ShowData_POR.ResumeLayout(false);
            this.Pnl_ShowData_POR.PerformLayout();
            this.Pnl_ShowData_ESK01.ResumeLayout(false);
            this.Pnl_ShowData_ESK02.ResumeLayout(false);
            this.Pnl_ShowData_ETOP.ResumeLayout(false);
            this.Pnl_ShowData_DSK01.ResumeLayout(false);
            this.Pnl_ShowData_DSK01.PerformLayout();
            this.Pnl_ShowData_TR.ResumeLayout(false);
            this.Pnl_ShowData_TR.PerformLayout();
            this.Pnl_ShowData_DSK02.ResumeLayout(false);
            this.Pnl_ShowData_DSK02.PerformLayout();
            this.Pnl_ShowData_ECar.ResumeLayout(false);
            this.Pnl_ShowData_DCar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Track_Picture)).EndInit();
            this.Pnl_Scan.ResumeLayout(false);
            this.Pnl_Scan_BackGround.ResumeLayout(false);
            this.Pnl_Scan_BackGround.PerformLayout();
            this.Pnl_NetworkStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Track_Picture_W_L)).EndInit();
            this.Pnl_ShowAll.ResumeLayout(false);
            this.Pnl_ShowAll.PerformLayout();
            this.Pnl_Spare.ResumeLayout(false);
            this.Pnl_Spare.PerformLayout();
            this.Pln_Por_SkidPdiData.ResumeLayout(false);
            this.Grb_Dividing.ResumeLayout(false);
            this.Grb_Dividing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Track_Picture_W_R)).EndInit();
            this.Grb_Leader_Strip.ResumeLayout(false);
            this.Grb_Leader_Strip.PerformLayout();
            this.Grb_Trim.ResumeLayout(false);
            this.Grb_Trim.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer Timer_TrackingMap;
        private System.Windows.Forms.Timer Timer_ProcessData;
        private System.Windows.Forms.ContextMenuStrip Menu_AckPDI;
        internal System.Windows.Forms.ToolStripMenuItem AckPDIToolStripMenuItem;
        private System.Windows.Forms.Panel Pnl_NotUse_X;
        internal System.Windows.Forms.Label Lbl_NotUse_X;
        internal System.Windows.Forms.Button Btn_DSK02_Weight_X;
        internal System.Windows.Forms.Label Lbl_ProcessCode_Title_X;
        internal System.Windows.Forms.Label Lbl_ProcessCode_X;
        internal System.Windows.Forms.Label Lbl_DTDisp_Title_X;
        internal System.Windows.Forms.Label Lbl_DT23Disp_X;
        internal System.Windows.Forms.Button Btn_Strip_X;
        internal System.Windows.Forms.Label Lbl_DT21Disp_X;
        internal System.Windows.Forms.Label Lbl_DT22Disp_X;
        internal System.Windows.Forms.Button Btn_PDO_X;
        private System.Windows.Forms.Panel Pnl_Weight_X;
        private System.Windows.Forms.Label Lbl_Weight_Title_X;
        internal System.Windows.Forms.TextBox Txt_Weight_X;
        private System.Windows.Forms.Label Lbl_Weight_Coil_Title_X;
        private System.Windows.Forms.Button Btn_Weight_Cancel_X;
        private System.Windows.Forms.Button Btn_Weight_Save_X;
        internal System.Windows.Forms.Button Btn_Del_ETOP_X;
        internal System.Windows.Forms.Button Btn_ETOPEntry_X;
        internal System.Windows.Forms.Button Btn_DT23Abandon_X;
        internal System.Windows.Forms.Button Btn_DT23Reset_X;
        private Tool.toolGroupBox Grb_Dividing;
        private System.Windows.Forms.Label Lbl_Order_Weight_2_Unit;
        private System.Windows.Forms.Label Lbl_Order_Weight_3_Unit;
        private System.Windows.Forms.Label Lbl_Order_Weight_4_Unit;
        private System.Windows.Forms.Label Lbl_Order_Weight_5_Unit;
        private System.Windows.Forms.Label Lbl_Order_Weight_6_Unit;
        private System.Windows.Forms.Label Lbl_Order_Weight_1_Unit;
        internal System.Windows.Forms.Label Lbl_Order_Weight_6;
        internal System.Windows.Forms.Label Lbl_Order_Weight_5;
        internal System.Windows.Forms.Label Lbl_Order_Weight_4;
        internal System.Windows.Forms.Label Lbl_Order_Weight_6_Title;
        internal System.Windows.Forms.Label Lbl_Order_Weight_5_Title;
        internal System.Windows.Forms.Label Lbl_Order_Weight_4_Title;
        internal System.Windows.Forms.Label Lbl_Order_Weight_3;
        internal System.Windows.Forms.Label Lbl_Order_Weight_2;
        internal System.Windows.Forms.Label Lbl_Order_Weight_1;
        internal System.Windows.Forms.Label Lbl_Order_Weight_3_Title;
        internal System.Windows.Forms.Label Lbl_Order_Weight_2_Title;
        internal System.Windows.Forms.Label Lbl_Order_Weight_1_Title;
        internal System.Windows.Forms.Label Lbl_Dividing_num1;
        internal System.Windows.Forms.Label Lbl_Dividing_num_Title;
        private Tool.toolGroupBox Grb_Trim;
        private System.Windows.Forms.Label Lbl_WidthMax_Unit;
        private System.Windows.Forms.Label Lbl_WidthMin_Unit;
        private System.Windows.Forms.Label Lbl_OutWidth_Unit;
        internal System.Windows.Forms.Label Lbl_WidthMin;
        internal System.Windows.Forms.Label Lbl_OutWidth_Title;
        internal System.Windows.Forms.Label Lbl_WidthMin_Title;
        internal System.Windows.Forms.Label Lbl_OutWidth;
        internal System.Windows.Forms.Label Lbl_WidthMax;
        internal System.Windows.Forms.Label Lbl_WidthMax_Title;
        private System.Windows.Forms.TabControl Tab_GridDataControl;
        private System.Windows.Forms.TabPage Tab_SchedulePage;
        internal System.Windows.Forms.DataGridView Dgv_OffLine;
        private System.Windows.Forms.TabPage Tab_OnlinePage;
        internal System.Windows.Forms.DataGridView Dgv_OnLine;
        internal System.Windows.Forms.Label Lbl_EntryMode_Title;
        private System.Windows.Forms.CheckBox Chk_DGV_Reflash;
        internal System.Windows.Forms.Label Lbl_EntryMode;
        internal System.Windows.Forms.Button Btn_AutoCoilFeed;
        internal System.Windows.Forms.Label Lbl_TrackingTitle;
        internal System.Windows.Forms.Button Btn_Reflash;
        internal System.Windows.Forms.Panel Pnl_ShowData_DTOP;
        internal System.Windows.Forms.Button Btn_DTOP_CoilOut;
        private System.Windows.Forms.Label Lbl_DTOP_Weight_Unit;
        internal System.Windows.Forms.Button Btn_DTOP_Del;
        internal System.Windows.Forms.Button Btn_DTOP_PrintLabel;
        internal System.Windows.Forms.Label Lbl_DTOP_CoilNo;
        internal System.Windows.Forms.TextBox Txt_DTOP_Weight;
        internal System.Windows.Forms.Label Lbl_DTOP_CoilNo_Title;
        internal System.Windows.Forms.Label Lbl_DTOP_Weight_Title;
        internal System.Windows.Forms.Label Lbl_DTOP_Skid_Title;
        internal System.Windows.Forms.Label Lbl_LineSpeed_Title;
        internal System.Windows.Forms.Label Lbl_LineSpeed;
        private System.Windows.Forms.Label Lbl_LineSpeed_Unit;
        internal System.Windows.Forms.Label Lbl_EntryStatus_Title;
        internal System.Windows.Forms.Label Lbl_CPLStatus_Title;
        internal System.Windows.Forms.Label Lbl_ExitStatus_Title;
        internal System.Windows.Forms.Label Lbl_EntryStatus;
        internal System.Windows.Forms.Label Lbl_CPLStatus;
        internal System.Windows.Forms.Label Lbl_ExitStatus;
        private System.Windows.Forms.Panel Pnl_ShowData_POR;
        internal System.Windows.Forms.Button Btn_PORPresetL1;
        internal System.Windows.Forms.Label Lbl_POR_CoilNo;
        internal System.Windows.Forms.Button Btn_StripBreakModify;
        internal System.Windows.Forms.Label Lbl_POR_Skid_Title;
        internal System.Windows.Forms.Button Btn_StripBreak;
        internal System.Windows.Forms.Button Btn_POR_Reject;
        internal System.Windows.Forms.Label Lbl_POR_CoilNo_Title;
        internal System.Windows.Forms.Label Lbl_POR_Tension_Title;
        internal System.Windows.Forms.Label Lbl_POR_Tension;
        internal System.Windows.Forms.Label Lbl_POR_Current_Title;
        internal System.Windows.Forms.Label Lbl_POR_Current;
        private System.Windows.Forms.Label Lbl_POR_Tension_Unit;
        private System.Windows.Forms.Label Lbl_POR_Current_Unit;
        private System.Windows.Forms.Panel Pnl_ShowData_ESK01;
        internal System.Windows.Forms.Label Lbl_ESK01_CoilNo_Title;
        internal System.Windows.Forms.Label Lbl_ESK01_Skid_Title;
        internal System.Windows.Forms.Label Lbl_ESK01_CoilNo;
        internal System.Windows.Forms.Button Btn_ESK01_Leader;
        internal System.Windows.Forms.Button Btn_ESK01_Entry;
        internal System.Windows.Forms.Button Btn_ESK01_Del;
        internal System.Windows.Forms.Button Btn_ESK01Reject;
        internal System.Windows.Forms.Button Btn_ESK01_PrintLabel;
        private System.Windows.Forms.Panel Pnl_ShowData_ESK02;
        internal System.Windows.Forms.Label Lbl_ESK02_CoilNo_Title;
        internal System.Windows.Forms.Button Btn_ESK02_Leader;
        internal System.Windows.Forms.Label Lbl_ESK02_Skid_Title;
        internal System.Windows.Forms.Label Lbl_ESK02_CoilNo;
        internal System.Windows.Forms.Button Btn_ESK02_Entry;
        internal System.Windows.Forms.Button Btn_ESK02_PrintLabel;
        internal System.Windows.Forms.Button Btn_ESK02_Del;
        internal System.Windows.Forms.Button Btn_ESK02_Reject;
        private System.Windows.Forms.Panel Pnl_ShowData_ETOP;
        internal System.Windows.Forms.Label Lbl_ETOP_CoilNo_Title;
        internal System.Windows.Forms.Button Btn_ETOP_PrintLabel;
        internal System.Windows.Forms.Label Lbl_ETOP_Skid_Title;
        internal System.Windows.Forms.Button Btn_ETOP_Del;
        internal System.Windows.Forms.Label Lbl_ETOP_CoilNo;
        internal System.Windows.Forms.Button Btn_ETOP_ManualFeed;
        internal System.Windows.Forms.Button Btn_ETOP_Reject;
        private System.Windows.Forms.Panel Pnl_ShowData_DSK01;
        internal System.Windows.Forms.Button Btn_DSK01_Del;
        private System.Windows.Forms.Label Lbl_DSK01_Weight_Unit;
        internal System.Windows.Forms.Button Btn_DSK01_CoilOut;
        internal System.Windows.Forms.Label Lbl_DSK01_Weight_Title;
        internal System.Windows.Forms.Label Lbl_DSK01_CoilNo_Title;
        internal System.Windows.Forms.Label Lbl_DSK01_Skid_Title;
        internal System.Windows.Forms.Label Lbl_DSK01_CoilNo;
        internal System.Windows.Forms.TextBox Txt_DSK01_Weight;
        internal System.Windows.Forms.Button Btn_DSK01_PrintLabel;
        private System.Windows.Forms.Panel Pnl_ShowData_TR;
        internal System.Windows.Forms.Label Lbl_TR_Skid_Title;
        internal System.Windows.Forms.Label Lbl_TR_CoilNo_Title;
        internal System.Windows.Forms.Label Lbl_TR_CoilNo;
        internal System.Windows.Forms.Label Lbl_TR_Tension;
        internal System.Windows.Forms.Label Lbl_TR_Current_Title;
        internal System.Windows.Forms.Label Lbl_TR_Tension_Title;
        internal System.Windows.Forms.Label Lbl_TR_Current;
        private System.Windows.Forms.Label Lbl_TR_Current_Unit;
        private System.Windows.Forms.Label Lbl_TR_Tension_Unit;
        private System.Windows.Forms.Panel Pnl_ShowData_DSK02;
        internal System.Windows.Forms.Button Btn_DSK02_Del;
        private System.Windows.Forms.Label Lbl_DSK02_Weight_Unit;
        internal System.Windows.Forms.Button Btn_DSK02_CoilOut;
        internal System.Windows.Forms.Label Lbl_DSK02_Weight_Title;
        internal System.Windows.Forms.Label Lbl_DSK02_CoilNo_Title;
        internal System.Windows.Forms.Label Lbl_DSK02_Skid_Title;
        internal System.Windows.Forms.Label Lbl_DSK02_CoilNo;
        internal System.Windows.Forms.Button Btn_DSK02_PrintLabel;
        internal System.Windows.Forms.TextBox Txt_DSK02_Weight;
        private System.Windows.Forms.Panel Pnl_ShowData_ECar;
        internal System.Windows.Forms.Label Lbl_ECar_CoilNo;
        internal System.Windows.Forms.Label Lbl_ECar_Skid_Title;
        internal System.Windows.Forms.Label Lbl_ECar_CoilNo_Title;
        private System.Windows.Forms.Panel Pnl_ShowData_DCar;
        internal System.Windows.Forms.Label Lbl_DCar_CoilNo;
        internal System.Windows.Forms.Label Lbl_DCar_Skid_Title;
        internal System.Windows.Forms.Label Lbl_DCar_CoilNo_Title;
        private Tool.toolGroupBox Grb_Leader_Strip;
        private System.Windows.Forms.Label Lbl_Head_Thickness_Unit;
        private System.Windows.Forms.Label Lbl_Head_Width_Unit;
        private System.Windows.Forms.Label Lbl_Head_Length_Unit;
        private System.Windows.Forms.Label Lbl_Leader_Strip_Tail_Title;
        private System.Windows.Forms.Label Lbl_Leader_Strip_Head_Title;
        internal System.Windows.Forms.Label Lbl_Tail_Thickness;
        internal System.Windows.Forms.Label Lbl_Tail_Width;
        internal System.Windows.Forms.Label Lbl_Tail_Length;
        internal System.Windows.Forms.Label Lbl_Head_Thickness;
        internal System.Windows.Forms.Label Lbl_Head_Thickness_Title;
        internal System.Windows.Forms.Label Lbl_Head_Width;
        internal System.Windows.Forms.Label Lbl_Head_Width_Title;
        internal System.Windows.Forms.Label Lbl_Head_Length;
        internal System.Windows.Forms.Label Lbl_Head_Length_Title;
        internal System.Windows.Forms.Label Lbl_Tail_St_no;
        internal System.Windows.Forms.Label Lbl_Head_St_no;
        internal System.Windows.Forms.Label Lbl_Head_St_no_Title;
        internal System.Windows.Forms.PictureBox Pic_Track_Picture;
        private System.Windows.Forms.Panel Pnl_Scan;
        private System.Windows.Forms.Panel Pnl_Scan_BackGround;
        private System.Windows.Forms.Button Btn_Scan_No;
        private System.Windows.Forms.Button Btn_Scan_Yes;
        private System.Windows.Forms.Label Lbl_Scan_SkidNo_Title;
        private System.Windows.Forms.Label Lbl_Scan_Desc;
        private System.Windows.Forms.Label Lbl_Scan_CoilNo_Title;
        private System.Windows.Forms.Label Lbl_Scan_SkidNo;
        private System.Windows.Forms.Panel Pnl_NetworkStatus;
        private System.Windows.Forms.Label Lbl_PLC_Color;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label Lbl_RevMMS_Color;
        private System.Windows.Forms.Label Lbl_SendMMS_Color;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label Lbl_RevWMS_Color;
        private System.Windows.Forms.Label Lbl_SendWMS_Color;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.PictureBox Pic_Track_Picture_W_L;
        internal System.Windows.Forms.Panel Pnl_ShowAll;
        private System.Windows.Forms.PictureBox Pic_Track_Picture_W_R;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Label Lbl_Por_ESK02_Paper;
        internal System.Windows.Forms.Label Lbl_Por_ESK02_Dividing;
        internal System.Windows.Forms.Label Lbl_Por_ESK02_Trim;
        internal System.Windows.Forms.Label Lbl_Por_ESK02_Leader;
        internal System.Windows.Forms.Label Lbl_Por_ESK02_St_No;
        private System.Windows.Forms.Panel Pln_Por_SkidPdiData;
        internal System.Windows.Forms.Label Lbl_Por_ESK01_Title;
        internal System.Windows.Forms.Label Lbl_Por_Paper_Title;
        internal System.Windows.Forms.Label Lbl_Por_ESK01_Paper;
        internal System.Windows.Forms.Label Lbl_Por_Dividing_Title;
        internal System.Windows.Forms.Label Lbl_Por_ESK01_Dividing;
        internal System.Windows.Forms.Label Lbl_Por_Trim_Title;
        internal System.Windows.Forms.Label Lbl_Por_ESK01_Trim;
        internal System.Windows.Forms.Label Lbl_Por_ESK01_Leader;
        internal System.Windows.Forms.Label Lbl_Por_ESK01_St_No;
        internal System.Windows.Forms.Label Lbl_Por_Leader_Title;
        internal System.Windows.Forms.Label Lbl_Por_St_Title;
        internal System.Windows.Forms.Label Lbl_Por_ESK02_Title;
        private System.Windows.Forms.Button Btn_Por_Paper;
        internal System.Windows.Forms.Label Lbl_Por_Title;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Label label15;
        internal System.Windows.Forms.Label label16;
        internal System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Label label18;
        internal System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel Pnl_Spare;
        private System.Windows.Forms.Button Btn_Close_Spare;
        private System.Windows.Forms.TextBox Txt_Spare;
        private System.Windows.Forms.Label Lbl_ComboName_Spare;
    }
}