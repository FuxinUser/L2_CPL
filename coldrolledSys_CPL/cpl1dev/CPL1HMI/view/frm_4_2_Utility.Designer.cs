namespace CPL1HMI
{
    partial class frm_4_2_Utility
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
            this.Btn_Save = new System.Windows.Forms.Button();
            this.Lbl_UtilityDate_TiTle = new System.Windows.Forms.Label();
            this.Dtp_UtilityDate = new System.Windows.Forms.DateTimePicker();
            this.Cob_Team = new System.Windows.Forms.ComboBox();
            this.Cob_Shift = new System.Windows.Forms.ComboBox();
            this.Lbl_Shift_TiTle = new System.Windows.Forms.Label();
            this.Lbl_Team_TiTle = new System.Windows.Forms.Label();
            this.Grb_Total = new System.Windows.Forms.GroupBox();
            this.Lbl_CoolingWater_Tol_Unit = new System.Windows.Forms.Label();
            this.Lbl_CompressedAir_Tol_Title = new System.Windows.Forms.Label();
            this.Lbl_CoolingWater_Tol_Title = new System.Windows.Forms.Label();
            this.Lbl_CompressedAir_Tol_Unit = new System.Windows.Forms.Label();
            this.Btn_Delete = new System.Windows.Forms.Button();
            this.Btn_New = new System.Windows.Forms.Button();
            this.Btn_ReFresh = new System.Windows.Forms.Button();
            this.Btn_SendToMMS = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Edit = new System.Windows.Forms.Button();
            this.Grb_DataCol = new System.Windows.Forms.GroupBox();
            this.Lbl_CoolingWater_TiTle = new System.Windows.Forms.Label();
            this.Txt_CoolingWater = new System.Windows.Forms.TextBox();
            this.Lbl_ComeAir_TiTle = new System.Windows.Forms.Label();
            this.Txt_ComeAir = new System.Windows.Forms.TextBox();
            this.Lbl_ComeAir_Unit = new System.Windows.Forms.Label();
            this.Dtp_DateShitf = new System.Windows.Forms.DateTimePicker();
            this.Dgv_Utility = new System.Windows.Forms.DataGridView();
            this.Lbl_MainTitle = new System.Windows.Forms.Label();
            this.Pnl_Show = new System.Windows.Forms.Panel();
            this.Dtp_DateStart = new System.Windows.Forms.DateTimePicker();
            this.Dtp_DateEnd = new System.Windows.Forms.DateTimePicker();
            this.Lbl_DateRange = new System.Windows.Forms.Label();
            this.Cob_Shift_S = new System.Windows.Forms.ComboBox();
            this.Rdb_Shitf = new System.Windows.Forms.RadioButton();
            this.Btn_Query = new System.Windows.Forms.Button();
            this.Rdb_DateTime = new System.Windows.Forms.RadioButton();
            this.Pnl_Data = new System.Windows.Forms.Panel();
            this.Txt_CoolingWater_Tol = new Common.StTool.CtrNumTextBox();
            this.Txt_CompressedAir_Tol = new Common.StTool.CtrNumTextBox();
            this.Grb_Total.SuspendLayout();
            this.Grb_DataCol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Utility)).BeginInit();
            this.Pnl_Show.SuspendLayout();
            this.Pnl_Data.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Save
            // 
            this.Btn_Save.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Save.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Save.Location = new System.Drawing.Point(1181, 78);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(150, 60);
            this.Btn_Save.TabIndex = 1182;
            this.Btn_Save.Text = "储存";
            this.Btn_Save.UseVisualStyleBackColor = false;
            this.Btn_Save.Visible = false;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // Lbl_UtilityDate_TiTle
            // 
            this.Lbl_UtilityDate_TiTle.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_UtilityDate_TiTle.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_UtilityDate_TiTle.ForeColor = System.Drawing.Color.Black;
            this.Lbl_UtilityDate_TiTle.Location = new System.Drawing.Point(20, 35);
            this.Lbl_UtilityDate_TiTle.Name = "Lbl_UtilityDate_TiTle";
            this.Lbl_UtilityDate_TiTle.Size = new System.Drawing.Size(100, 35);
            this.Lbl_UtilityDate_TiTle.TabIndex = 1458;
            this.Lbl_UtilityDate_TiTle.Text = "能耗时间";
            this.Lbl_UtilityDate_TiTle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Dtp_UtilityDate
            // 
            this.Dtp_UtilityDate.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.Dtp_UtilityDate.Enabled = false;
            this.Dtp_UtilityDate.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_UtilityDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_UtilityDate.Location = new System.Drawing.Point(120, 35);
            this.Dtp_UtilityDate.Name = "Dtp_UtilityDate";
            this.Dtp_UtilityDate.Size = new System.Drawing.Size(224, 33);
            this.Dtp_UtilityDate.TabIndex = 1456;
            // 
            // Cob_Team
            // 
            this.Cob_Team.DropDownWidth = 50;
            this.Cob_Team.Enabled = false;
            this.Cob_Team.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_Team.FormattingEnabled = true;
            this.Cob_Team.Items.AddRange(new object[] {
            "A - 甲班",
            "B - 乙班",
            "C - 丙班",
            "D - 丁班"});
            this.Cob_Team.Location = new System.Drawing.Point(120, 121);
            this.Cob_Team.Name = "Cob_Team";
            this.Cob_Team.Size = new System.Drawing.Size(120, 32);
            this.Cob_Team.TabIndex = 1455;
            // 
            // Cob_Shift
            // 
            this.Cob_Shift.DropDownWidth = 50;
            this.Cob_Shift.Enabled = false;
            this.Cob_Shift.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_Shift.FormattingEnabled = true;
            this.Cob_Shift.Items.AddRange(new object[] {
            "1 - 夜",
            "2 - 早",
            "3 - 中"});
            this.Cob_Shift.Location = new System.Drawing.Point(120, 78);
            this.Cob_Shift.Name = "Cob_Shift";
            this.Cob_Shift.Size = new System.Drawing.Size(120, 32);
            this.Cob_Shift.TabIndex = 1454;
            // 
            // Lbl_Shift_TiTle
            // 
            this.Lbl_Shift_TiTle.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_Shift_TiTle.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Shift_TiTle.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Shift_TiTle.Location = new System.Drawing.Point(20, 78);
            this.Lbl_Shift_TiTle.Name = "Lbl_Shift_TiTle";
            this.Lbl_Shift_TiTle.Size = new System.Drawing.Size(100, 35);
            this.Lbl_Shift_TiTle.TabIndex = 1093;
            this.Lbl_Shift_TiTle.Text = "班次";
            this.Lbl_Shift_TiTle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Team_TiTle
            // 
            this.Lbl_Team_TiTle.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_Team_TiTle.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Team_TiTle.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Team_TiTle.Location = new System.Drawing.Point(20, 121);
            this.Lbl_Team_TiTle.Name = "Lbl_Team_TiTle";
            this.Lbl_Team_TiTle.Size = new System.Drawing.Size(100, 35);
            this.Lbl_Team_TiTle.TabIndex = 1092;
            this.Lbl_Team_TiTle.Text = "班别";
            this.Lbl_Team_TiTle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Grb_Total
            // 
            this.Grb_Total.BackColor = System.Drawing.Color.LightGray;
            this.Grb_Total.Controls.Add(this.Lbl_CoolingWater_Tol_Unit);
            this.Grb_Total.Controls.Add(this.Txt_CoolingWater_Tol);
            this.Grb_Total.Controls.Add(this.Txt_CompressedAir_Tol);
            this.Grb_Total.Controls.Add(this.Lbl_CompressedAir_Tol_Title);
            this.Grb_Total.Controls.Add(this.Lbl_CoolingWater_Tol_Title);
            this.Grb_Total.Controls.Add(this.Lbl_CompressedAir_Tol_Unit);
            this.Grb_Total.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Grb_Total.Location = new System.Drawing.Point(5, 767);
            this.Grb_Total.Name = "Grb_Total";
            this.Grb_Total.Size = new System.Drawing.Size(1910, 121);
            this.Grb_Total.TabIndex = 1780;
            this.Grb_Total.TabStop = false;
            this.Grb_Total.Text = "能源消耗统计";
            // 
            // Lbl_CoolingWater_Tol_Unit
            // 
            this.Lbl_CoolingWater_Tol_Unit.AutoSize = true;
            this.Lbl_CoolingWater_Tol_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_CoolingWater_Tol_Unit.ForeColor = System.Drawing.Color.Black;
            this.Lbl_CoolingWater_Tol_Unit.Location = new System.Drawing.Point(676, 54);
            this.Lbl_CoolingWater_Tol_Unit.Name = "Lbl_CoolingWater_Tol_Unit";
            this.Lbl_CoolingWater_Tol_Unit.Size = new System.Drawing.Size(33, 24);
            this.Lbl_CoolingWater_Tol_Unit.TabIndex = 1467;
            this.Lbl_CoolingWater_Tol_Unit.Text = "kg";
            // 
            // Lbl_CompressedAir_Tol_Title
            // 
            this.Lbl_CompressedAir_Tol_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_CompressedAir_Tol_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_CompressedAir_Tol_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_CompressedAir_Tol_Title.Location = new System.Drawing.Point(20, 50);
            this.Lbl_CompressedAir_Tol_Title.Name = "Lbl_CompressedAir_Tol_Title";
            this.Lbl_CompressedAir_Tol_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_CompressedAir_Tol_Title.TabIndex = 1091;
            this.Lbl_CompressedAir_Tol_Title.Text = "压缩空气";
            this.Lbl_CompressedAir_Tol_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Lbl_CompressedAir_Tol_Title.TextChanged += new System.EventHandler(this.Fun_LanguageIsEn_Font14_12);
            // 
            // Lbl_CoolingWater_Tol_Title
            // 
            this.Lbl_CoolingWater_Tol_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_CoolingWater_Tol_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_CoolingWater_Tol_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_CoolingWater_Tol_Title.Location = new System.Drawing.Point(376, 50);
            this.Lbl_CoolingWater_Tol_Title.Name = "Lbl_CoolingWater_Tol_Title";
            this.Lbl_CoolingWater_Tol_Title.Size = new System.Drawing.Size(150, 33);
            this.Lbl_CoolingWater_Tol_Title.TabIndex = 1463;
            this.Lbl_CoolingWater_Tol_Title.Text = "间接冷却水";
            this.Lbl_CoolingWater_Tol_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Lbl_CoolingWater_Tol_Title.TextChanged += new System.EventHandler(this.Fun_LanguageIsEn_Font14_12);
            // 
            // Lbl_CompressedAir_Tol_Unit
            // 
            this.Lbl_CompressedAir_Tol_Unit.AutoSize = true;
            this.Lbl_CompressedAir_Tol_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_CompressedAir_Tol_Unit.ForeColor = System.Drawing.Color.Black;
            this.Lbl_CompressedAir_Tol_Unit.Location = new System.Drawing.Point(320, 54);
            this.Lbl_CompressedAir_Tol_Unit.Name = "Lbl_CompressedAir_Tol_Unit";
            this.Lbl_CompressedAir_Tol_Unit.Size = new System.Drawing.Size(36, 24);
            this.Lbl_CompressedAir_Tol_Unit.TabIndex = 1094;
            this.Lbl_CompressedAir_Tol_Unit.Text = "m³";
            // 
            // Btn_Delete
            // 
            this.Btn_Delete.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Delete.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Delete.Location = new System.Drawing.Point(1011, 78);
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Size = new System.Drawing.Size(150, 60);
            this.Btn_Delete.TabIndex = 1755;
            this.Btn_Delete.Text = "删除";
            this.Btn_Delete.UseVisualStyleBackColor = false;
            this.Btn_Delete.Visible = false;
            this.Btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // Btn_New
            // 
            this.Btn_New.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_New.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_New.Location = new System.Drawing.Point(671, 78);
            this.Btn_New.Name = "Btn_New";
            this.Btn_New.Size = new System.Drawing.Size(150, 60);
            this.Btn_New.TabIndex = 1754;
            this.Btn_New.Text = "新增";
            this.Btn_New.UseVisualStyleBackColor = false;
            this.Btn_New.Visible = false;
            this.Btn_New.Click += new System.EventHandler(this.Btn_New_Click);
            // 
            // Btn_ReFresh
            // 
            this.Btn_ReFresh.BackColor = System.Drawing.Color.Gold;
            this.Btn_ReFresh.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_ReFresh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_ReFresh.Location = new System.Drawing.Point(1570, 11);
            this.Btn_ReFresh.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_ReFresh.Name = "Btn_ReFresh";
            this.Btn_ReFresh.Size = new System.Drawing.Size(150, 60);
            this.Btn_ReFresh.TabIndex = 1753;
            this.Btn_ReFresh.Text = "重新载入";
            this.Btn_ReFresh.UseVisualStyleBackColor = false;
            this.Btn_ReFresh.Visible = false;
            // 
            // Btn_SendToMMS
            // 
            this.Btn_SendToMMS.BackColor = System.Drawing.Color.Gold;
            this.Btn_SendToMMS.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_SendToMMS.Location = new System.Drawing.Point(1740, 11);
            this.Btn_SendToMMS.Name = "Btn_SendToMMS";
            this.Btn_SendToMMS.Size = new System.Drawing.Size(150, 60);
            this.Btn_SendToMMS.TabIndex = 1184;
            this.Btn_SendToMMS.Text = "上传MMS";
            this.Btn_SendToMMS.UseVisualStyleBackColor = false;
            this.Btn_SendToMMS.Click += new System.EventHandler(this.Btn_SendToMMS_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Cancel.Location = new System.Drawing.Point(1351, 78);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(150, 60);
            this.Btn_Cancel.TabIndex = 1183;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Cancel.Visible = false;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_Edit
            // 
            this.Btn_Edit.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Edit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Edit.Location = new System.Drawing.Point(841, 78);
            this.Btn_Edit.Name = "Btn_Edit";
            this.Btn_Edit.Size = new System.Drawing.Size(150, 60);
            this.Btn_Edit.TabIndex = 1179;
            this.Btn_Edit.Text = "修改";
            this.Btn_Edit.UseVisualStyleBackColor = false;
            this.Btn_Edit.Visible = false;
            this.Btn_Edit.Click += new System.EventHandler(this.Btn_Edit_Click);
            // 
            // Grb_DataCol
            // 
            this.Grb_DataCol.BackColor = System.Drawing.Color.LightGray;
            this.Grb_DataCol.Controls.Add(this.Btn_Delete);
            this.Grb_DataCol.Controls.Add(this.Lbl_CoolingWater_TiTle);
            this.Grb_DataCol.Controls.Add(this.Btn_New);
            this.Grb_DataCol.Controls.Add(this.Txt_CoolingWater);
            this.Grb_DataCol.Controls.Add(this.Lbl_UtilityDate_TiTle);
            this.Grb_DataCol.Controls.Add(this.Btn_Cancel);
            this.Grb_DataCol.Controls.Add(this.Dtp_UtilityDate);
            this.Grb_DataCol.Controls.Add(this.Btn_Save);
            this.Grb_DataCol.Controls.Add(this.Cob_Team);
            this.Grb_DataCol.Controls.Add(this.Btn_Edit);
            this.Grb_DataCol.Controls.Add(this.Cob_Shift);
            this.Grb_DataCol.Controls.Add(this.Lbl_Shift_TiTle);
            this.Grb_DataCol.Controls.Add(this.Lbl_Team_TiTle);
            this.Grb_DataCol.Controls.Add(this.Lbl_ComeAir_TiTle);
            this.Grb_DataCol.Controls.Add(this.Txt_ComeAir);
            this.Grb_DataCol.Controls.Add(this.Lbl_ComeAir_Unit);
            this.Grb_DataCol.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Grb_DataCol.Location = new System.Drawing.Point(12, 557);
            this.Grb_DataCol.Name = "Grb_DataCol";
            this.Grb_DataCol.Size = new System.Drawing.Size(1858, 171);
            this.Grb_DataCol.TabIndex = 1779;
            this.Grb_DataCol.TabStop = false;
            this.Grb_DataCol.Text = "能源消耗信息";
            this.Grb_DataCol.Visible = false;
            // 
            // Lbl_CoolingWater_TiTle
            // 
            this.Lbl_CoolingWater_TiTle.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_CoolingWater_TiTle.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_CoolingWater_TiTle.ForeColor = System.Drawing.Color.Black;
            this.Lbl_CoolingWater_TiTle.Location = new System.Drawing.Point(326, 121);
            this.Lbl_CoolingWater_TiTle.Name = "Lbl_CoolingWater_TiTle";
            this.Lbl_CoolingWater_TiTle.Size = new System.Drawing.Size(127, 35);
            this.Lbl_CoolingWater_TiTle.TabIndex = 1459;
            this.Lbl_CoolingWater_TiTle.Text = "间接冷却水";
            this.Lbl_CoolingWater_TiTle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Txt_CoolingWater
            // 
            this.Txt_CoolingWater.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_CoolingWater.Enabled = false;
            this.Txt_CoolingWater.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_CoolingWater.Location = new System.Drawing.Point(453, 121);
            this.Txt_CoolingWater.Name = "Txt_CoolingWater";
            this.Txt_CoolingWater.Size = new System.Drawing.Size(114, 33);
            this.Txt_CoolingWater.TabIndex = 1460;
            // 
            // Lbl_ComeAir_TiTle
            // 
            this.Lbl_ComeAir_TiTle.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_ComeAir_TiTle.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_ComeAir_TiTle.ForeColor = System.Drawing.Color.Black;
            this.Lbl_ComeAir_TiTle.Location = new System.Drawing.Point(326, 78);
            this.Lbl_ComeAir_TiTle.Name = "Lbl_ComeAir_TiTle";
            this.Lbl_ComeAir_TiTle.Size = new System.Drawing.Size(127, 35);
            this.Lbl_ComeAir_TiTle.TabIndex = 1;
            this.Lbl_ComeAir_TiTle.Text = "压缩空气";
            this.Lbl_ComeAir_TiTle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Txt_ComeAir
            // 
            this.Txt_ComeAir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_ComeAir.Enabled = false;
            this.Txt_ComeAir.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_ComeAir.Location = new System.Drawing.Point(453, 78);
            this.Txt_ComeAir.Name = "Txt_ComeAir";
            this.Txt_ComeAir.Size = new System.Drawing.Size(114, 33);
            this.Txt_ComeAir.TabIndex = 344;
            // 
            // Lbl_ComeAir_Unit
            // 
            this.Lbl_ComeAir_Unit.AutoSize = true;
            this.Lbl_ComeAir_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_ComeAir_Unit.ForeColor = System.Drawing.Color.Black;
            this.Lbl_ComeAir_Unit.Location = new System.Drawing.Point(573, 83);
            this.Lbl_ComeAir_Unit.Name = "Lbl_ComeAir_Unit";
            this.Lbl_ComeAir_Unit.Size = new System.Drawing.Size(83, 24);
            this.Lbl_ComeAir_Unit.TabIndex = 1080;
            this.Lbl_ComeAir_Unit.Text = "(Nm^3)";
            // 
            // Dtp_DateShitf
            // 
            this.Dtp_DateShitf.CalendarFont = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_DateShitf.CustomFormat = "yyyy/MM/dd";
            this.Dtp_DateShitf.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_DateShitf.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_DateShitf.Location = new System.Drawing.Point(604, 25);
            this.Dtp_DateShitf.Name = "Dtp_DateShitf";
            this.Dtp_DateShitf.Size = new System.Drawing.Size(150, 33);
            this.Dtp_DateShitf.TabIndex = 1777;
            // 
            // Dgv_Utility
            // 
            this.Dgv_Utility.AllowUserToAddRows = false;
            this.Dgv_Utility.AllowUserToDeleteRows = false;
            this.Dgv_Utility.AllowUserToResizeColumns = false;
            this.Dgv_Utility.AllowUserToResizeRows = false;
            this.Dgv_Utility.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Utility.Location = new System.Drawing.Point(5, 139);
            this.Dgv_Utility.Name = "Dgv_Utility";
            this.Dgv_Utility.ReadOnly = true;
            this.Dgv_Utility.RowHeadersVisible = false;
            this.Dgv_Utility.RowTemplate.Height = 24;
            this.Dgv_Utility.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Utility.Size = new System.Drawing.Size(1910, 623);
            this.Dgv_Utility.TabIndex = 1777;
            this.Dgv_Utility.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvUtility_CellClick);
            // 
            // Lbl_MainTitle
            // 
            this.Lbl_MainTitle.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            this.Lbl_MainTitle.ForeColor = System.Drawing.Color.Cyan;
            this.Lbl_MainTitle.Location = new System.Drawing.Point(0, 10);
            this.Lbl_MainTitle.Name = "Lbl_MainTitle";
            this.Lbl_MainTitle.Size = new System.Drawing.Size(1920, 35);
            this.Lbl_MainTitle.TabIndex = 1775;
            this.Lbl_MainTitle.Text = "4-2 能源耗用";
            this.Lbl_MainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pnl_Show
            // 
            this.Pnl_Show.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Show.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Show.Controls.Add(this.Dtp_DateStart);
            this.Pnl_Show.Controls.Add(this.Dtp_DateEnd);
            this.Pnl_Show.Controls.Add(this.Lbl_DateRange);
            this.Pnl_Show.Controls.Add(this.Cob_Shift_S);
            this.Pnl_Show.Controls.Add(this.Dtp_DateShitf);
            this.Pnl_Show.Controls.Add(this.Rdb_Shitf);
            this.Pnl_Show.Controls.Add(this.Btn_Query);
            this.Pnl_Show.Controls.Add(this.Rdb_DateTime);
            this.Pnl_Show.Location = new System.Drawing.Point(5, 50);
            this.Pnl_Show.Name = "Pnl_Show";
            this.Pnl_Show.Size = new System.Drawing.Size(1910, 84);
            this.Pnl_Show.TabIndex = 1776;
            // 
            // Dtp_DateStart
            // 
            this.Dtp_DateStart.CustomFormat = "yyyy/MM/dd";
            this.Dtp_DateStart.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_DateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_DateStart.Location = new System.Drawing.Point(140, 25);
            this.Dtp_DateStart.Name = "Dtp_DateStart";
            this.Dtp_DateStart.Size = new System.Drawing.Size(150, 33);
            this.Dtp_DateStart.TabIndex = 1779;
            // 
            // Dtp_DateEnd
            // 
            this.Dtp_DateEnd.CustomFormat = "yyyy/MM/dd";
            this.Dtp_DateEnd.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_DateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_DateEnd.Location = new System.Drawing.Point(314, 25);
            this.Dtp_DateEnd.Name = "Dtp_DateEnd";
            this.Dtp_DateEnd.Size = new System.Drawing.Size(150, 33);
            this.Dtp_DateEnd.TabIndex = 1781;
            // 
            // Lbl_DateRange
            // 
            this.Lbl_DateRange.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DateRange.ForeColor = System.Drawing.Color.Black;
            this.Lbl_DateRange.Location = new System.Drawing.Point(290, 29);
            this.Lbl_DateRange.Name = "Lbl_DateRange";
            this.Lbl_DateRange.Size = new System.Drawing.Size(24, 24);
            this.Lbl_DateRange.TabIndex = 1780;
            this.Lbl_DateRange.Text = "~";
            this.Lbl_DateRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cob_Shift_S
            // 
            this.Cob_Shift_S.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_Shift_S.FormattingEnabled = true;
            this.Cob_Shift_S.Items.AddRange(new object[] {
            "1 - 夜",
            "2 - 早",
            "3 - 中"});
            this.Cob_Shift_S.Location = new System.Drawing.Point(762, 25);
            this.Cob_Shift_S.Name = "Cob_Shift_S";
            this.Cob_Shift_S.Size = new System.Drawing.Size(129, 32);
            this.Cob_Shift_S.TabIndex = 1776;
            // 
            // Rdb_Shitf
            // 
            this.Rdb_Shitf.BackColor = System.Drawing.Color.SkyBlue;
            this.Rdb_Shitf.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Rdb_Shitf.ForeColor = System.Drawing.Color.Black;
            this.Rdb_Shitf.Location = new System.Drawing.Point(484, 25);
            this.Rdb_Shitf.Name = "Rdb_Shitf";
            this.Rdb_Shitf.Size = new System.Drawing.Size(120, 33);
            this.Rdb_Shitf.TabIndex = 1775;
            this.Rdb_Shitf.Text = "班次";
            this.Rdb_Shitf.UseVisualStyleBackColor = false;
            // 
            // Btn_Query
            // 
            this.Btn_Query.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Query.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Query.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Query.Location = new System.Drawing.Point(911, 11);
            this.Btn_Query.Name = "Btn_Query";
            this.Btn_Query.Size = new System.Drawing.Size(150, 60);
            this.Btn_Query.TabIndex = 1774;
            this.Btn_Query.Text = "查询";
            this.Btn_Query.UseVisualStyleBackColor = false;
            this.Btn_Query.Click += new System.EventHandler(this.Btn_Query_Click);
            // 
            // Rdb_DateTime
            // 
            this.Rdb_DateTime.BackColor = System.Drawing.Color.SkyBlue;
            this.Rdb_DateTime.Checked = true;
            this.Rdb_DateTime.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Rdb_DateTime.ForeColor = System.Drawing.Color.Black;
            this.Rdb_DateTime.Location = new System.Drawing.Point(20, 25);
            this.Rdb_DateTime.Name = "Rdb_DateTime";
            this.Rdb_DateTime.Size = new System.Drawing.Size(120, 33);
            this.Rdb_DateTime.TabIndex = 1773;
            this.Rdb_DateTime.TabStop = true;
            this.Rdb_DateTime.Text = "起始日期";
            this.Rdb_DateTime.UseVisualStyleBackColor = false;
            // 
            // Pnl_Data
            // 
            this.Pnl_Data.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Data.Controls.Add(this.Btn_ReFresh);
            this.Pnl_Data.Controls.Add(this.Btn_SendToMMS);
            this.Pnl_Data.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Pnl_Data.Location = new System.Drawing.Point(5, 893);
            this.Pnl_Data.Name = "Pnl_Data";
            this.Pnl_Data.Size = new System.Drawing.Size(1910, 84);
            this.Pnl_Data.TabIndex = 1778;
            // 
            // Txt_CoolingWater_Tol
            // 
            this.Txt_CoolingWater_Tol.BindColumnName = null;
            this.Txt_CoolingWater_Tol.BindCurrentValue = null;
            this.Txt_CoolingWater_Tol.DecimalDigit = 8;
            this.Txt_CoolingWater_Tol.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_CoolingWater_Tol.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Txt_CoolingWater_Tol.IntegerDigit = 20;
            this.Txt_CoolingWater_Tol.Location = new System.Drawing.Point(526, 50);
            this.Txt_CoolingWater_Tol.MaxLength = 21;
            this.Txt_CoolingWater_Tol.Name = "Txt_CoolingWater_Tol";
            this.Txt_CoolingWater_Tol.Size = new System.Drawing.Size(150, 33);
            this.Txt_CoolingWater_Tol.TabIndex = 1466;
            // 
            // Txt_CompressedAir_Tol
            // 
            this.Txt_CompressedAir_Tol.BindColumnName = null;
            this.Txt_CompressedAir_Tol.BindCurrentValue = null;
            this.Txt_CompressedAir_Tol.DecimalDigit = 8;
            this.Txt_CompressedAir_Tol.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_CompressedAir_Tol.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Txt_CompressedAir_Tol.IntegerDigit = 20;
            this.Txt_CompressedAir_Tol.Location = new System.Drawing.Point(170, 50);
            this.Txt_CompressedAir_Tol.MaxLength = 21;
            this.Txt_CompressedAir_Tol.Name = "Txt_CompressedAir_Tol";
            this.Txt_CompressedAir_Tol.Size = new System.Drawing.Size(150, 33);
            this.Txt_CompressedAir_Tol.TabIndex = 1465;
            // 
            // frm_4_2_Utility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1920, 982);
            this.Controls.Add(this.Grb_Total);
            this.Controls.Add(this.Dgv_Utility);
            this.Controls.Add(this.Grb_DataCol);
            this.Controls.Add(this.Lbl_MainTitle);
            this.Controls.Add(this.Pnl_Show);
            this.Controls.Add(this.Pnl_Data);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frm_4_2_Utility";
            this.Text = "frm_4_2_Utility";
            this.Load += new System.EventHandler(this.Frm_4_2_Utility_Load);
            this.Grb_Total.ResumeLayout(false);
            this.Grb_Total.PerformLayout();
            this.Grb_DataCol.ResumeLayout(false);
            this.Grb_DataCol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Utility)).EndInit();
            this.Pnl_Show.ResumeLayout(false);
            this.Pnl_Data.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.Label Lbl_UtilityDate_TiTle;
        internal System.Windows.Forms.DateTimePicker Dtp_UtilityDate;
        internal System.Windows.Forms.ComboBox Cob_Team;
        internal System.Windows.Forms.ComboBox Cob_Shift;
        private System.Windows.Forms.Label Lbl_Shift_TiTle;
        private System.Windows.Forms.Label Lbl_Team_TiTle;
        private System.Windows.Forms.GroupBox Grb_Total;
        private System.Windows.Forms.Button Btn_Delete;
        private System.Windows.Forms.Button Btn_New;
        private System.Windows.Forms.Button Btn_ReFresh;
        private System.Windows.Forms.Button Btn_SendToMMS;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_Edit;
        private System.Windows.Forms.GroupBox Grb_DataCol;
        internal System.Windows.Forms.DateTimePicker Dtp_DateShitf;
        internal System.Windows.Forms.DataGridView Dgv_Utility;
        internal System.Windows.Forms.Label Lbl_MainTitle;
        internal System.Windows.Forms.Panel Pnl_Show;
        internal System.Windows.Forms.DateTimePicker Dtp_DateStart;
        internal System.Windows.Forms.DateTimePicker Dtp_DateEnd;
        internal System.Windows.Forms.Label Lbl_DateRange;
        internal System.Windows.Forms.ComboBox Cob_Shift_S;
        internal System.Windows.Forms.RadioButton Rdb_Shitf;
        internal System.Windows.Forms.Button Btn_Query;
        internal System.Windows.Forms.RadioButton Rdb_DateTime;
        private System.Windows.Forms.Panel Pnl_Data;
        private System.Windows.Forms.Label Lbl_CompressedAir_Tol_Title;
        private System.Windows.Forms.Label Lbl_CoolingWater_Tol_Title;
        internal System.Windows.Forms.Label Lbl_CompressedAir_Tol_Unit;
        private System.Windows.Forms.Label Lbl_CoolingWater_TiTle;
        internal System.Windows.Forms.TextBox Txt_CoolingWater;
        private System.Windows.Forms.Label Lbl_ComeAir_TiTle;
        internal System.Windows.Forms.TextBox Txt_ComeAir;
        internal System.Windows.Forms.Label Lbl_ComeAir_Unit;
        internal Common.StTool.CtrNumTextBox Txt_CoolingWater_Tol;
        internal System.Windows.Forms.Label Lbl_CoolingWater_Tol_Unit;
        internal Common.StTool.CtrNumTextBox Txt_CompressedAir_Tol;
    }
}