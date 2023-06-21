namespace CPL1HMI
{
    partial class Frm_3_4_Report
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
            this.panel = new System.Windows.Forms.Panel();
            this.Pnl_Show1 = new System.Windows.Forms.Panel();
            this.Btn_ExportExcel = new System.Windows.Forms.Button();
            this.Btn_ExportPDF = new System.Windows.Forms.Button();
            this.Btn_ExportReport = new System.Windows.Forms.Button();
            this.Chk_shift_no = new System.Windows.Forms.CheckBox();
            this.Cob_shift_no = new System.Windows.Forms.ComboBox();
            this.Lbl_Date = new System.Windows.Forms.Label();
            this.Lbl_Date_Range = new System.Windows.Forms.Label();
            this.Txt_Count = new System.Windows.Forms.TextBox();
            this.Lbl_CoilCount_Unit = new System.Windows.Forms.Label();
            this.Lbl_CoilCount_Title = new System.Windows.Forms.Label();
            this.Btn_Search = new System.Windows.Forms.Button();
            this.Dtp_Start_Time = new System.Windows.Forms.DateTimePicker();
            this.Dtp_Finish_Time = new System.Windows.Forms.DateTimePicker();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.Dgv_CoilList = new System.Windows.Forms.DataGridView();
            this.panel.SuspendLayout();
            this.Pnl_Show1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CoilList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.Controls.Add(this.Pnl_Show1);
            this.panel.Controls.Add(this.lblMainTitle);
            this.panel.Controls.Add(this.Dgv_CoilList);
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Margin = new System.Windows.Forms.Padding(4);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1920, 982);
            this.panel.TabIndex = 1760;
            // 
            // Pnl_Show1
            // 
            this.Pnl_Show1.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Show1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Show1.Controls.Add(this.Btn_ExportExcel);
            this.Pnl_Show1.Controls.Add(this.Btn_ExportPDF);
            this.Pnl_Show1.Controls.Add(this.Btn_ExportReport);
            this.Pnl_Show1.Controls.Add(this.Chk_shift_no);
            this.Pnl_Show1.Controls.Add(this.Cob_shift_no);
            this.Pnl_Show1.Controls.Add(this.Lbl_Date);
            this.Pnl_Show1.Controls.Add(this.Lbl_Date_Range);
            this.Pnl_Show1.Controls.Add(this.Txt_Count);
            this.Pnl_Show1.Controls.Add(this.Lbl_CoilCount_Unit);
            this.Pnl_Show1.Controls.Add(this.Lbl_CoilCount_Title);
            this.Pnl_Show1.Controls.Add(this.Btn_Search);
            this.Pnl_Show1.Controls.Add(this.Dtp_Start_Time);
            this.Pnl_Show1.Controls.Add(this.Dtp_Finish_Time);
            this.Pnl_Show1.Location = new System.Drawing.Point(5, 50);
            this.Pnl_Show1.Margin = new System.Windows.Forms.Padding(4);
            this.Pnl_Show1.Name = "Pnl_Show1";
            this.Pnl_Show1.Size = new System.Drawing.Size(1910, 84);
            this.Pnl_Show1.TabIndex = 1756;
            // 
            // Btn_ExportExcel
            // 
            this.Btn_ExportExcel.BackColor = System.Drawing.Color.Gold;
            this.Btn_ExportExcel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_ExportExcel.Location = new System.Drawing.Point(1075, 12);
            this.Btn_ExportExcel.Name = "Btn_ExportExcel";
            this.Btn_ExportExcel.Size = new System.Drawing.Size(150, 60);
            this.Btn_ExportExcel.TabIndex = 1779;
            this.Btn_ExportExcel.Text = "汇出Excel";
            this.Btn_ExportExcel.UseVisualStyleBackColor = false;
            this.Btn_ExportExcel.Click += new System.EventHandler(this.Btn_ExportExcel_Click);
            // 
            // Btn_ExportPDF
            // 
            this.Btn_ExportPDF.BackColor = System.Drawing.Color.Gold;
            this.Btn_ExportPDF.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_ExportPDF.Location = new System.Drawing.Point(1245, 12);
            this.Btn_ExportPDF.Name = "Btn_ExportPDF";
            this.Btn_ExportPDF.Size = new System.Drawing.Size(150, 60);
            this.Btn_ExportPDF.TabIndex = 1778;
            this.Btn_ExportPDF.Text = "汇出PDF";
            this.Btn_ExportPDF.UseVisualStyleBackColor = false;
            this.Btn_ExportPDF.Visible = false;
            this.Btn_ExportPDF.Click += new System.EventHandler(this.Btn_ExportPDF_Click);
            // 
            // Btn_ExportReport
            // 
            this.Btn_ExportReport.BackColor = System.Drawing.Color.Gold;
            this.Btn_ExportReport.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_ExportReport.Location = new System.Drawing.Point(1415, 12);
            this.Btn_ExportReport.Name = "Btn_ExportReport";
            this.Btn_ExportReport.Size = new System.Drawing.Size(150, 60);
            this.Btn_ExportReport.TabIndex = 1777;
            this.Btn_ExportReport.Text = "汇出报表";
            this.Btn_ExportReport.UseVisualStyleBackColor = false;
            this.Btn_ExportReport.Visible = false;
            // 
            // Chk_shift_no
            // 
            this.Chk_shift_no.AccessibleDescription = "chk";
            this.Chk_shift_no.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_shift_no.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_shift_no.Location = new System.Drawing.Point(626, 26);
            this.Chk_shift_no.Name = "Chk_shift_no";
            this.Chk_shift_no.Size = new System.Drawing.Size(90, 32);
            this.Chk_shift_no.TabIndex = 1776;
            this.Chk_shift_no.Text = "班次";
            this.Chk_shift_no.UseVisualStyleBackColor = false;
            // 
            // Cob_shift_no
            // 
            this.Cob_shift_no.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_shift_no.FormattingEnabled = true;
            this.Cob_shift_no.Location = new System.Drawing.Point(716, 26);
            this.Cob_shift_no.Margin = new System.Windows.Forms.Padding(4);
            this.Cob_shift_no.Name = "Cob_shift_no";
            this.Cob_shift_no.Size = new System.Drawing.Size(169, 32);
            this.Cob_shift_no.TabIndex = 1775;
            // 
            // Lbl_Date
            // 
            this.Lbl_Date.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_Date.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Date.Location = new System.Drawing.Point(20, 25);
            this.Lbl_Date.Name = "Lbl_Date";
            this.Lbl_Date.Size = new System.Drawing.Size(124, 33);
            this.Lbl_Date.TabIndex = 1115;
            this.Lbl_Date.Text = "日期区间";
            this.Lbl_Date.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Date_Range
            // 
            this.Lbl_Date_Range.AutoSize = true;
            this.Lbl_Date_Range.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Date_Range.Location = new System.Drawing.Point(365, 29);
            this.Lbl_Date_Range.Name = "Lbl_Date_Range";
            this.Lbl_Date_Range.Size = new System.Drawing.Size(24, 24);
            this.Lbl_Date_Range.TabIndex = 1113;
            this.Lbl_Date_Range.Text = "~";
            // 
            // Txt_Count
            // 
            this.Txt_Count.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_Count.Location = new System.Drawing.Point(1754, 25);
            this.Txt_Count.Name = "Txt_Count";
            this.Txt_Count.Size = new System.Drawing.Size(86, 33);
            this.Txt_Count.TabIndex = 1110;
            this.Txt_Count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Lbl_CoilCount_Unit
            // 
            this.Lbl_CoilCount_Unit.BackColor = System.Drawing.Color.DarkGray;
            this.Lbl_CoilCount_Unit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_CoilCount_Unit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_CoilCount_Unit.Location = new System.Drawing.Point(1840, 25);
            this.Lbl_CoilCount_Unit.Name = "Lbl_CoilCount_Unit";
            this.Lbl_CoilCount_Unit.Size = new System.Drawing.Size(52, 33);
            this.Lbl_CoilCount_Unit.TabIndex = 1109;
            this.Lbl_CoilCount_Unit.Text = "颗";
            this.Lbl_CoilCount_Unit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_CoilCount_Title
            // 
            this.Lbl_CoilCount_Title.BackColor = System.Drawing.Color.DarkGray;
            this.Lbl_CoilCount_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_CoilCount_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_CoilCount_Title.Location = new System.Drawing.Point(1644, 25);
            this.Lbl_CoilCount_Title.Name = "Lbl_CoilCount_Title";
            this.Lbl_CoilCount_Title.Size = new System.Drawing.Size(110, 33);
            this.Lbl_CoilCount_Title.TabIndex = 1108;
            this.Lbl_CoilCount_Title.Text = "钢卷总数:";
            this.Lbl_CoilCount_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_Search
            // 
            this.Btn_Search.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Search.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Search.Location = new System.Drawing.Point(905, 12);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(150, 60);
            this.Btn_Search.TabIndex = 1107;
            this.Btn_Search.Text = "查询";
            this.Btn_Search.UseVisualStyleBackColor = false;
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // Dtp_Start_Time
            // 
            this.Dtp_Start_Time.CustomFormat = "yyyy/MM/dd HH时";
            this.Dtp_Start_Time.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_Start_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_Start_Time.Location = new System.Drawing.Point(144, 25);
            this.Dtp_Start_Time.Margin = new System.Windows.Forms.Padding(4);
            this.Dtp_Start_Time.Name = "Dtp_Start_Time";
            this.Dtp_Start_Time.Size = new System.Drawing.Size(217, 33);
            this.Dtp_Start_Time.TabIndex = 1097;
            // 
            // Dtp_Finish_Time
            // 
            this.Dtp_Finish_Time.CustomFormat = "yyyy/MM/dd HH时";
            this.Dtp_Finish_Time.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_Finish_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_Finish_Time.Location = new System.Drawing.Point(389, 25);
            this.Dtp_Finish_Time.Margin = new System.Windows.Forms.Padding(4);
            this.Dtp_Finish_Time.Name = "Dtp_Finish_Time";
            this.Dtp_Finish_Time.Size = new System.Drawing.Size(217, 33);
            this.Dtp_Finish_Time.TabIndex = 1098;
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.BackColor = System.Drawing.Color.Gray;
            this.lblMainTitle.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            this.lblMainTitle.ForeColor = System.Drawing.Color.Cyan;
            this.lblMainTitle.Location = new System.Drawing.Point(0, 10);
            this.lblMainTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(1920, 35);
            this.lblMainTitle.TabIndex = 1755;
            this.lblMainTitle.Text = "3-4 报表";
            this.lblMainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Dgv_CoilList
            // 
            this.Dgv_CoilList.AllowUserToAddRows = false;
            this.Dgv_CoilList.AllowUserToDeleteRows = false;
            this.Dgv_CoilList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dgv_CoilList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Dgv_CoilList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_CoilList.Location = new System.Drawing.Point(5, 139);
            this.Dgv_CoilList.Margin = new System.Windows.Forms.Padding(4);
            this.Dgv_CoilList.Name = "Dgv_CoilList";
            this.Dgv_CoilList.ReadOnly = true;
            this.Dgv_CoilList.RowHeadersVisible = false;
            this.Dgv_CoilList.RowTemplate.Height = 24;
            this.Dgv_CoilList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_CoilList.Size = new System.Drawing.Size(1910, 838);
            this.Dgv_CoilList.TabIndex = 1757;
            // 
            // Frm_3_4_Report
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1920, 982);
            this.Controls.Add(this.panel);
            this.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_3_4_Report";
            this.Text = "3-4 Report";
            this.Load += new System.EventHandler(this.Frm_3_4_Report_Load);
            this.panel.ResumeLayout(false);
            this.Pnl_Show1.ResumeLayout(false);
            this.Pnl_Show1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CoilList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.DataGridView Dgv_CoilList;
        private System.Windows.Forms.Panel Pnl_Show1;
        private System.Windows.Forms.Button Btn_ExportExcel;
        private System.Windows.Forms.Button Btn_ExportPDF;
        private System.Windows.Forms.Button Btn_ExportReport;
        internal System.Windows.Forms.CheckBox Chk_shift_no;
        internal System.Windows.Forms.ComboBox Cob_shift_no;
        private System.Windows.Forms.Label Lbl_Date;
        internal System.Windows.Forms.Label Lbl_Date_Range;
        private System.Windows.Forms.TextBox Txt_Count;
        internal System.Windows.Forms.Label Lbl_CoilCount_Unit;
        internal System.Windows.Forms.Label Lbl_CoilCount_Title;
        private System.Windows.Forms.Button Btn_Search;
        internal System.Windows.Forms.DateTimePicker Dtp_Start_Time;
        internal System.Windows.Forms.DateTimePicker Dtp_Finish_Time;
        internal System.Windows.Forms.Label lblMainTitle;
    }
}