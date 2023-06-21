namespace CPL1HMI
{
    partial class frm_5_1_EventLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Chk_Keyword = new System.Windows.Forms.CheckBox();
            this.Txt_Keyword = new System.Windows.Forms.TextBox();
            this.Chk_EventType = new System.Windows.Forms.CheckBox();
            this.Lbl_DateTime_Ranage = new System.Windows.Forms.Label();
            this.Pnl_Show = new System.Windows.Forms.Panel();
            this.Lbl_SelectTop_Title = new System.Windows.Forms.Label();
            this.Lbl_System_ID_Title = new System.Windows.Forms.Label();
            this.Lbl_DateTime_Title = new System.Windows.Forms.Label();
            this.Cob_System_ID = new System.Windows.Forms.ComboBox();
            this.Cob_EventType = new System.Windows.Forms.ComboBox();
            this.Dtp_Finish_Time = new System.Windows.Forms.DateTimePicker();
            this.Cob_ComputerName = new System.Windows.Forms.ComboBox();
            this.Dtp_Start_Time = new System.Windows.Forms.DateTimePicker();
            this.Chk_ComputerName = new System.Windows.Forms.CheckBox();
            this.Btn_QueryOK = new System.Windows.Forms.Button();
            this.Dgv_EventLog = new System.Windows.Forms.DataGridView();
            this.Lbl_MainTitle = new System.Windows.Forms.Label();
            this.Pnl_DataCol = new System.Windows.Forms.Panel();
            this.Txt_Command = new System.Windows.Forms.TextBox();
            this.Txt_Event_Description = new System.Windows.Forms.TextBox();
            this.Lbl_Command_Title = new System.Windows.Forms.Label();
            this.Lbl_Event_Description_Title = new System.Windows.Forms.Label();
            this.Txt_SelectTop = new Common.StTool.CtrNumTextBox();
            this.Pnl_Show.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EventLog)).BeginInit();
            this.Pnl_DataCol.SuspendLayout();
            this.SuspendLayout();
            // 
            // Chk_Keyword
            // 
            this.Chk_Keyword.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_Keyword.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_Keyword.ForeColor = System.Drawing.Color.Black;
            this.Chk_Keyword.Location = new System.Drawing.Point(20, 53);
            this.Chk_Keyword.Name = "Chk_Keyword";
            this.Chk_Keyword.Size = new System.Drawing.Size(162, 33);
            this.Chk_Keyword.TabIndex = 9;
            this.Chk_Keyword.Text = "内容关键字";
            this.Chk_Keyword.UseVisualStyleBackColor = false;
            // 
            // Txt_Keyword
            // 
            this.Txt_Keyword.BackColor = System.Drawing.SystemColors.Window;
            this.Txt_Keyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Keyword.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_Keyword.Location = new System.Drawing.Point(182, 53);
            this.Txt_Keyword.Name = "Txt_Keyword";
            this.Txt_Keyword.Size = new System.Drawing.Size(405, 33);
            this.Txt_Keyword.TabIndex = 10;
            // 
            // Chk_EventType
            // 
            this.Chk_EventType.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_EventType.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_EventType.ForeColor = System.Drawing.Color.Black;
            this.Chk_EventType.Location = new System.Drawing.Point(605, 10);
            this.Chk_EventType.Name = "Chk_EventType";
            this.Chk_EventType.Size = new System.Drawing.Size(162, 33);
            this.Chk_EventType.TabIndex = 14;
            this.Chk_EventType.Text = "类别";
            this.Chk_EventType.UseVisualStyleBackColor = false;
            // 
            // Lbl_DateTime_Ranage
            // 
            this.Lbl_DateTime_Ranage.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_DateTime_Ranage.ForeColor = System.Drawing.Color.Black;
            this.Lbl_DateTime_Ranage.Location = new System.Drawing.Point(373, 11);
            this.Lbl_DateTime_Ranage.Name = "Lbl_DateTime_Ranage";
            this.Lbl_DateTime_Ranage.Size = new System.Drawing.Size(22, 29);
            this.Lbl_DateTime_Ranage.TabIndex = 16;
            this.Lbl_DateTime_Ranage.Text = "~";
            // 
            // Pnl_Show
            // 
            this.Pnl_Show.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Show.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Show.Controls.Add(this.Txt_SelectTop);
            this.Pnl_Show.Controls.Add(this.Lbl_SelectTop_Title);
            this.Pnl_Show.Controls.Add(this.Lbl_System_ID_Title);
            this.Pnl_Show.Controls.Add(this.Lbl_DateTime_Title);
            this.Pnl_Show.Controls.Add(this.Cob_System_ID);
            this.Pnl_Show.Controls.Add(this.Chk_Keyword);
            this.Pnl_Show.Controls.Add(this.Cob_EventType);
            this.Pnl_Show.Controls.Add(this.Dtp_Finish_Time);
            this.Pnl_Show.Controls.Add(this.Cob_ComputerName);
            this.Pnl_Show.Controls.Add(this.Txt_Keyword);
            this.Pnl_Show.Controls.Add(this.Dtp_Start_Time);
            this.Pnl_Show.Controls.Add(this.Chk_ComputerName);
            this.Pnl_Show.Controls.Add(this.Btn_QueryOK);
            this.Pnl_Show.Controls.Add(this.Chk_EventType);
            this.Pnl_Show.Controls.Add(this.Lbl_DateTime_Ranage);
            this.Pnl_Show.Location = new System.Drawing.Point(5, 50);
            this.Pnl_Show.Name = "Pnl_Show";
            this.Pnl_Show.Size = new System.Drawing.Size(1910, 100);
            this.Pnl_Show.TabIndex = 301;
            // 
            // Lbl_SelectTop_Title
            // 
            this.Lbl_SelectTop_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_SelectTop_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_SelectTop_Title.Location = new System.Drawing.Point(1036, 53);
            this.Lbl_SelectTop_Title.Name = "Lbl_SelectTop_Title";
            this.Lbl_SelectTop_Title.Size = new System.Drawing.Size(162, 33);
            this.Lbl_SelectTop_Title.TabIndex = 1123;
            this.Lbl_SelectTop_Title.Text = "查询资料笔数";
            this.Lbl_SelectTop_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_System_ID_Title
            // 
            this.Lbl_System_ID_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_System_ID_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_System_ID_Title.Location = new System.Drawing.Point(1036, 10);
            this.Lbl_System_ID_Title.Name = "Lbl_System_ID_Title";
            this.Lbl_System_ID_Title.Size = new System.Drawing.Size(162, 33);
            this.Lbl_System_ID_Title.TabIndex = 1122;
            this.Lbl_System_ID_Title.Text = "系统";
            this.Lbl_System_ID_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_DateTime_Title
            // 
            this.Lbl_DateTime_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_DateTime_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_DateTime_Title.Location = new System.Drawing.Point(20, 10);
            this.Lbl_DateTime_Title.Name = "Lbl_DateTime_Title";
            this.Lbl_DateTime_Title.Size = new System.Drawing.Size(162, 33);
            this.Lbl_DateTime_Title.TabIndex = 1121;
            this.Lbl_DateTime_Title.Text = "事件日期时间";
            this.Lbl_DateTime_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Cob_System_ID
            // 
            this.Cob_System_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cob_System_ID.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_System_ID.ForeColor = System.Drawing.Color.Black;
            this.Cob_System_ID.Location = new System.Drawing.Point(1198, 10);
            this.Cob_System_ID.Name = "Cob_System_ID";
            this.Cob_System_ID.Size = new System.Drawing.Size(194, 32);
            this.Cob_System_ID.TabIndex = 1120;
            // 
            // Cob_EventType
            // 
            this.Cob_EventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cob_EventType.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_EventType.ForeColor = System.Drawing.Color.Black;
            this.Cob_EventType.Items.AddRange(new object[] {
            "警告",
            "事件",
            "錯誤",
            "未列管",
            "全部"});
            this.Cob_EventType.Location = new System.Drawing.Point(767, 10);
            this.Cob_EventType.Name = "Cob_EventType";
            this.Cob_EventType.Size = new System.Drawing.Size(249, 32);
            this.Cob_EventType.TabIndex = 1116;
            // 
            // Dtp_Finish_Time
            // 
            this.Dtp_Finish_Time.CustomFormat = "yyyy/MM/dd HH时";
            this.Dtp_Finish_Time.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_Finish_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_Finish_Time.Location = new System.Drawing.Point(397, 10);
            this.Dtp_Finish_Time.Margin = new System.Windows.Forms.Padding(4);
            this.Dtp_Finish_Time.Name = "Dtp_Finish_Time";
            this.Dtp_Finish_Time.Size = new System.Drawing.Size(190, 33);
            this.Dtp_Finish_Time.TabIndex = 1114;
            // 
            // Cob_ComputerName
            // 
            this.Cob_ComputerName.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_ComputerName.ForeColor = System.Drawing.Color.Black;
            this.Cob_ComputerName.Location = new System.Drawing.Point(767, 53);
            this.Cob_ComputerName.Name = "Cob_ComputerName";
            this.Cob_ComputerName.Size = new System.Drawing.Size(249, 32);
            this.Cob_ComputerName.TabIndex = 1118;
            // 
            // Dtp_Start_Time
            // 
            this.Dtp_Start_Time.CustomFormat = "yyyy/MM/dd HH时";
            this.Dtp_Start_Time.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_Start_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_Start_Time.Location = new System.Drawing.Point(182, 10);
            this.Dtp_Start_Time.Margin = new System.Windows.Forms.Padding(4);
            this.Dtp_Start_Time.Name = "Dtp_Start_Time";
            this.Dtp_Start_Time.Size = new System.Drawing.Size(190, 33);
            this.Dtp_Start_Time.TabIndex = 1112;
            // 
            // Chk_ComputerName
            // 
            this.Chk_ComputerName.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_ComputerName.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_ComputerName.ForeColor = System.Drawing.Color.Black;
            this.Chk_ComputerName.Location = new System.Drawing.Point(605, 53);
            this.Chk_ComputerName.Name = "Chk_ComputerName";
            this.Chk_ComputerName.Size = new System.Drawing.Size(162, 33);
            this.Chk_ComputerName.TabIndex = 1117;
            this.Chk_ComputerName.Text = "电脑名称";
            this.Chk_ComputerName.UseVisualStyleBackColor = false;
            this.Chk_ComputerName.TextChanged += new System.EventHandler(this.Fun_LanguageIsEn_Font14_12_Chk);
            // 
            // Btn_QueryOK
            // 
            this.Btn_QueryOK.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_QueryOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_QueryOK.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_QueryOK.ForeColor = System.Drawing.Color.Black;
            this.Btn_QueryOK.Location = new System.Drawing.Point(1412, 19);
            this.Btn_QueryOK.Name = "Btn_QueryOK";
            this.Btn_QueryOK.Size = new System.Drawing.Size(150, 60);
            this.Btn_QueryOK.TabIndex = 320;
            this.Btn_QueryOK.Text = "查询";
            this.Btn_QueryOK.UseVisualStyleBackColor = false;
            this.Btn_QueryOK.Click += new System.EventHandler(this.Btn_QueryOK_Click);
            // 
            // Dgv_EventLog
            // 
            this.Dgv_EventLog.AllowUserToAddRows = false;
            this.Dgv_EventLog.AllowUserToDeleteRows = false;
            this.Dgv_EventLog.AllowUserToResizeColumns = false;
            this.Dgv_EventLog.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_EventLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_EventLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_EventLog.Location = new System.Drawing.Point(5, 155);
            this.Dgv_EventLog.Name = "Dgv_EventLog";
            this.Dgv_EventLog.ReadOnly = true;
            this.Dgv_EventLog.RowHeadersVisible = false;
            this.Dgv_EventLog.RowHeadersWidth = 51;
            this.Dgv_EventLog.RowTemplate.Height = 24;
            this.Dgv_EventLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_EventLog.Size = new System.Drawing.Size(1910, 822);
            this.Dgv_EventLog.TabIndex = 300;
            this.Dgv_EventLog.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Dgv_EventLog_RowPostPaint);
            // 
            // Lbl_MainTitle
            // 
            this.Lbl_MainTitle.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            this.Lbl_MainTitle.ForeColor = System.Drawing.Color.Cyan;
            this.Lbl_MainTitle.Location = new System.Drawing.Point(0, 10);
            this.Lbl_MainTitle.Name = "Lbl_MainTitle";
            this.Lbl_MainTitle.Size = new System.Drawing.Size(1920, 35);
            this.Lbl_MainTitle.TabIndex = 299;
            this.Lbl_MainTitle.Text = "5-1 事件记录";
            this.Lbl_MainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pnl_DataCol
            // 
            this.Pnl_DataCol.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_DataCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pnl_DataCol.Controls.Add(this.Txt_Command);
            this.Pnl_DataCol.Controls.Add(this.Txt_Event_Description);
            this.Pnl_DataCol.Controls.Add(this.Lbl_Command_Title);
            this.Pnl_DataCol.Controls.Add(this.Lbl_Event_Description_Title);
            this.Pnl_DataCol.Location = new System.Drawing.Point(5, 836);
            this.Pnl_DataCol.Margin = new System.Windows.Forms.Padding(4);
            this.Pnl_DataCol.Name = "Pnl_DataCol";
            this.Pnl_DataCol.Size = new System.Drawing.Size(1910, 141);
            this.Pnl_DataCol.TabIndex = 1760;
            this.Pnl_DataCol.Visible = false;
            // 
            // Txt_Command
            // 
            this.Txt_Command.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Command.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Command.Location = new System.Drawing.Point(121, 53);
            this.Txt_Command.Multiline = true;
            this.Txt_Command.Name = "Txt_Command";
            this.Txt_Command.ReadOnly = true;
            this.Txt_Command.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Txt_Command.Size = new System.Drawing.Size(1761, 79);
            this.Txt_Command.TabIndex = 3;
            // 
            // Txt_Event_Description
            // 
            this.Txt_Event_Description.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Event_Description.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Event_Description.Location = new System.Drawing.Point(121, 10);
            this.Txt_Event_Description.Name = "Txt_Event_Description";
            this.Txt_Event_Description.ReadOnly = true;
            this.Txt_Event_Description.Size = new System.Drawing.Size(1761, 33);
            this.Txt_Event_Description.TabIndex = 2;
            // 
            // Lbl_Command_Title
            // 
            this.Lbl_Command_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_Command_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Command_Title.Location = new System.Drawing.Point(20, 53);
            this.Lbl_Command_Title.Name = "Lbl_Command_Title";
            this.Lbl_Command_Title.Size = new System.Drawing.Size(101, 79);
            this.Lbl_Command_Title.TabIndex = 1;
            this.Lbl_Command_Title.Text = "内容 ";
            this.Lbl_Command_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Event_Description_Title
            // 
            this.Lbl_Event_Description_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_Event_Description_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Event_Description_Title.Location = new System.Drawing.Point(20, 10);
            this.Lbl_Event_Description_Title.Name = "Lbl_Event_Description_Title";
            this.Lbl_Event_Description_Title.Size = new System.Drawing.Size(101, 33);
            this.Lbl_Event_Description_Title.TabIndex = 0;
            this.Lbl_Event_Description_Title.Text = "事件名称 ";
            this.Lbl_Event_Description_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Txt_SelectTop
            // 
            this.Txt_SelectTop.BindColumnName = null;
            this.Txt_SelectTop.BindCurrentValue = null;
            this.Txt_SelectTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_SelectTop.DecimalDigit = 0;
            this.Txt_SelectTop.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_SelectTop.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Txt_SelectTop.IntegerDigit = 15;
            this.Txt_SelectTop.Location = new System.Drawing.Point(1198, 53);
            this.Txt_SelectTop.MaxLength = 15;
            this.Txt_SelectTop.Name = "Txt_SelectTop";
            this.Txt_SelectTop.Size = new System.Drawing.Size(194, 33);
            this.Txt_SelectTop.TabIndex = 1124;
            this.Txt_SelectTop.Text = "1000";
            // 
            // frm_5_1_EventLog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1920, 982);
            this.Controls.Add(this.Pnl_Show);
            this.Controls.Add(this.Dgv_EventLog);
            this.Controls.Add(this.Lbl_MainTitle);
            this.Controls.Add(this.Pnl_DataCol);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_5_1_EventLog";
            this.Text = "frm_5_1_EventLog";
            this.Load += new System.EventHandler(this.Frm_5_1_EventLog_Load);
            this.Shown += new System.EventHandler(this.Frm_5_1_EventLog_Shown);
            this.Pnl_Show.ResumeLayout(false);
            this.Pnl_Show.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_EventLog)).EndInit();
            this.Pnl_DataCol.ResumeLayout(false);
            this.Pnl_DataCol.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.CheckBox Chk_Keyword;
        internal System.Windows.Forms.TextBox Txt_Keyword;
        internal System.Windows.Forms.CheckBox Chk_EventType;
        internal System.Windows.Forms.Label Lbl_DateTime_Ranage;
        internal System.Windows.Forms.Panel Pnl_Show;
        internal System.Windows.Forms.Button Btn_QueryOK;
        internal System.Windows.Forms.DataGridView Dgv_EventLog;
        internal System.Windows.Forms.Label Lbl_MainTitle;
        private System.Windows.Forms.DateTimePicker Dtp_Start_Time;
        private System.Windows.Forms.DateTimePicker Dtp_Finish_Time;
        internal System.Windows.Forms.ComboBox Cob_EventType;
        internal System.Windows.Forms.ComboBox Cob_System_ID;
        internal System.Windows.Forms.ComboBox Cob_ComputerName;
        internal System.Windows.Forms.CheckBox Chk_ComputerName;
        internal System.Windows.Forms.Panel Pnl_DataCol;
        private System.Windows.Forms.Label Lbl_Command_Title;
        private System.Windows.Forms.Label Lbl_Event_Description_Title;
        private System.Windows.Forms.TextBox Txt_Command;
        private System.Windows.Forms.TextBox Txt_Event_Description;
        private System.Windows.Forms.Label Lbl_DateTime_Title;
        private System.Windows.Forms.Label Lbl_System_ID_Title;
        private Common.StTool.CtrNumTextBox Txt_SelectTop;
        private System.Windows.Forms.Label Lbl_SelectTop_Title;
    }
}