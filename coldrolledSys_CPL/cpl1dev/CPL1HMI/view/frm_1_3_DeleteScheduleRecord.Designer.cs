namespace CPL1HMI
{
    partial class Frm_1_3_DeleteScheduleRecord
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
            this.Cob_DeleteCode = new System.Windows.Forms.ComboBox();
            this.Pnl_Show = new System.Windows.Forms.Panel();
            this.Btn_Search = new System.Windows.Forms.Button();
            this.Chk_Coil = new System.Windows.Forms.CheckBox();
            this.Chk_User = new System.Windows.Forms.CheckBox();
            this.Chk_DeleteCode = new System.Windows.Forms.CheckBox();
            this.Cob_User = new System.Windows.Forms.ComboBox();
            this.Dtp__Finish_Time = new System.Windows.Forms.DateTimePicker();
            this.Lbl_Time_Range = new System.Windows.Forms.Label();
            this.Dtp_Start_Time = new System.Windows.Forms.DateTimePicker();
            this.Cob_Entry_Coil_No = new System.Windows.Forms.ComboBox();
            this.Chk_Time = new System.Windows.Forms.CheckBox();
            this.Btn_Update = new System.Windows.Forms.Button();
            this.Lbl_DelSchdlTitle = new System.Windows.Forms.Label();
            this.Dgv_DeleteSchedule = new System.Windows.Forms.DataGridView();
            this.Txt_Spare = new System.Windows.Forms.TextBox();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.Txt_Coil = new System.Windows.Forms.TextBox();
            this.Lbl_CoilNo = new System.Windows.Forms.Label();
            this.Lbl_Spare = new System.Windows.Forms.Label();
            this.Pnl_Bottom = new System.Windows.Forms.Panel();
            this.Cob_Remarks_Type = new System.Windows.Forms.ComboBox();
            this.Chk_Remarks_Type = new System.Windows.Forms.CheckBox();
            this.Pnl_Show.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_DeleteSchedule)).BeginInit();
            this.Pnl_Bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cob_DeleteCode
            // 
            this.Cob_DeleteCode.DropDownWidth = 350;
            this.Cob_DeleteCode.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.Cob_DeleteCode.FormattingEnabled = true;
            this.Cob_DeleteCode.Location = new System.Drawing.Point(564, 10);
            this.Cob_DeleteCode.Name = "Cob_DeleteCode";
            this.Cob_DeleteCode.Size = new System.Drawing.Size(483, 32);
            this.Cob_DeleteCode.TabIndex = 355;
            // 
            // Pnl_Show
            // 
            this.Pnl_Show.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Show.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Show.Controls.Add(this.Cob_Remarks_Type);
            this.Pnl_Show.Controls.Add(this.Chk_Remarks_Type);
            this.Pnl_Show.Controls.Add(this.Btn_Search);
            this.Pnl_Show.Controls.Add(this.Chk_Coil);
            this.Pnl_Show.Controls.Add(this.Chk_User);
            this.Pnl_Show.Controls.Add(this.Chk_DeleteCode);
            this.Pnl_Show.Controls.Add(this.Cob_User);
            this.Pnl_Show.Controls.Add(this.Dtp__Finish_Time);
            this.Pnl_Show.Controls.Add(this.Lbl_Time_Range);
            this.Pnl_Show.Controls.Add(this.Dtp_Start_Time);
            this.Pnl_Show.Controls.Add(this.Cob_Entry_Coil_No);
            this.Pnl_Show.Controls.Add(this.Chk_Time);
            this.Pnl_Show.Controls.Add(this.Cob_DeleteCode);
            this.Pnl_Show.Location = new System.Drawing.Point(5, 50);
            this.Pnl_Show.Name = "Pnl_Show";
            this.Pnl_Show.Size = new System.Drawing.Size(1910, 100);
            this.Pnl_Show.TabIndex = 350;
            // 
            // Btn_Search
            // 
            this.Btn_Search.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Search.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Search.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Search.Location = new System.Drawing.Point(1067, 18);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(150, 60);
            this.Btn_Search.TabIndex = 291;
            this.Btn_Search.Text = "查询";
            this.Btn_Search.UseVisualStyleBackColor = false;
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // Chk_Coil
            // 
            this.Chk_Coil.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_Coil.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_Coil.Location = new System.Drawing.Point(20, 10);
            this.Chk_Coil.Name = "Chk_Coil";
            this.Chk_Coil.Size = new System.Drawing.Size(160, 33);
            this.Chk_Coil.TabIndex = 1778;
            this.Chk_Coil.Text = "入口钢卷号";
            this.Chk_Coil.UseVisualStyleBackColor = false;
            // 
            // Chk_User
            // 
            this.Chk_User.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_User.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_User.Location = new System.Drawing.Point(1493, 10);
            this.Chk_User.Name = "Chk_User";
            this.Chk_User.Size = new System.Drawing.Size(160, 33);
            this.Chk_User.TabIndex = 1777;
            this.Chk_User.Text = "操作人员";
            this.Chk_User.UseVisualStyleBackColor = false;
            this.Chk_User.Visible = false;
            // 
            // Chk_DeleteCode
            // 
            this.Chk_DeleteCode.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_DeleteCode.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_DeleteCode.Location = new System.Drawing.Point(404, 10);
            this.Chk_DeleteCode.Name = "Chk_DeleteCode";
            this.Chk_DeleteCode.Size = new System.Drawing.Size(160, 33);
            this.Chk_DeleteCode.TabIndex = 1776;
            this.Chk_DeleteCode.Text = "原因代码";
            this.Chk_DeleteCode.UseVisualStyleBackColor = false;
            // 
            // Cob_User
            // 
            this.Cob_User.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.Cob_User.FormattingEnabled = true;
            this.Cob_User.Location = new System.Drawing.Point(1653, 10);
            this.Cob_User.Name = "Cob_User";
            this.Cob_User.Size = new System.Drawing.Size(234, 32);
            this.Cob_User.TabIndex = 1774;
            this.Cob_User.Visible = false;
            // 
            // Dtp__Finish_Time
            // 
            this.Dtp__Finish_Time.CustomFormat = "yyyy/MM/dd/ HH时";
            this.Dtp__Finish_Time.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp__Finish_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp__Finish_Time.Location = new System.Drawing.Point(404, 53);
            this.Dtp__Finish_Time.Margin = new System.Windows.Forms.Padding(4);
            this.Dtp__Finish_Time.Name = "Dtp__Finish_Time";
            this.Dtp__Finish_Time.Size = new System.Drawing.Size(200, 33);
            this.Dtp__Finish_Time.TabIndex = 1765;
            // 
            // Lbl_Time_Range
            // 
            this.Lbl_Time_Range.AutoSize = true;
            this.Lbl_Time_Range.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Time_Range.Location = new System.Drawing.Point(380, 57);
            this.Lbl_Time_Range.Name = "Lbl_Time_Range";
            this.Lbl_Time_Range.Size = new System.Drawing.Size(24, 24);
            this.Lbl_Time_Range.TabIndex = 1769;
            this.Lbl_Time_Range.Text = "~";
            // 
            // Dtp_Start_Time
            // 
            this.Dtp_Start_Time.CustomFormat = "yyyy/MM/dd/ HH时";
            this.Dtp_Start_Time.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Dtp_Start_Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_Start_Time.Location = new System.Drawing.Point(180, 53);
            this.Dtp_Start_Time.Margin = new System.Windows.Forms.Padding(4);
            this.Dtp_Start_Time.Name = "Dtp_Start_Time";
            this.Dtp_Start_Time.Size = new System.Drawing.Size(200, 33);
            this.Dtp_Start_Time.TabIndex = 1764;
            // 
            // Cob_Entry_Coil_No
            // 
            this.Cob_Entry_Coil_No.Font = new System.Drawing.Font("微軟正黑體", 14.25F);
            this.Cob_Entry_Coil_No.FormattingEnabled = true;
            this.Cob_Entry_Coil_No.Location = new System.Drawing.Point(180, 10);
            this.Cob_Entry_Coil_No.Name = "Cob_Entry_Coil_No";
            this.Cob_Entry_Coil_No.Size = new System.Drawing.Size(200, 32);
            this.Cob_Entry_Coil_No.TabIndex = 1772;
            this.Cob_Entry_Coil_No.Click += new System.EventHandler(this.Cob_Entry_Coil_No_Click);
            // 
            // Chk_Time
            // 
            this.Chk_Time.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_Time.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_Time.Location = new System.Drawing.Point(20, 53);
            this.Chk_Time.Name = "Chk_Time";
            this.Chk_Time.Size = new System.Drawing.Size(160, 33);
            this.Chk_Time.TabIndex = 1766;
            this.Chk_Time.Text = "日期区间";
            this.Chk_Time.UseVisualStyleBackColor = false;
            // 
            // Btn_Update
            // 
            this.Btn_Update.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Update.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Update.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Update.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Update.Location = new System.Drawing.Point(1118, 13);
            this.Btn_Update.Name = "Btn_Update";
            this.Btn_Update.Size = new System.Drawing.Size(150, 60);
            this.Btn_Update.TabIndex = 294;
            this.Btn_Update.Text = "修改";
            this.Btn_Update.UseVisualStyleBackColor = false;
            this.Btn_Update.Click += new System.EventHandler(this.Btn_Update_Click);
            // 
            // Lbl_DelSchdlTitle
            // 
            this.Lbl_DelSchdlTitle.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            this.Lbl_DelSchdlTitle.ForeColor = System.Drawing.Color.Cyan;
            this.Lbl_DelSchdlTitle.Location = new System.Drawing.Point(0, 10);
            this.Lbl_DelSchdlTitle.Name = "Lbl_DelSchdlTitle";
            this.Lbl_DelSchdlTitle.Size = new System.Drawing.Size(1920, 35);
            this.Lbl_DelSchdlTitle.TabIndex = 349;
            this.Lbl_DelSchdlTitle.Text = "1-3 删除排程记录";
            this.Lbl_DelSchdlTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Dgv_DeleteSchedule
            // 
            this.Dgv_DeleteSchedule.AllowUserToAddRows = false;
            this.Dgv_DeleteSchedule.AllowUserToDeleteRows = false;
            this.Dgv_DeleteSchedule.AllowUserToResizeColumns = false;
            this.Dgv_DeleteSchedule.AllowUserToResizeRows = false;
            this.Dgv_DeleteSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_DeleteSchedule.Location = new System.Drawing.Point(5, 156);
            this.Dgv_DeleteSchedule.Name = "Dgv_DeleteSchedule";
            this.Dgv_DeleteSchedule.ReadOnly = true;
            this.Dgv_DeleteSchedule.RowHeadersVisible = false;
            this.Dgv_DeleteSchedule.RowTemplate.Height = 24;
            this.Dgv_DeleteSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_DeleteSchedule.Size = new System.Drawing.Size(1910, 821);
            this.Dgv_DeleteSchedule.TabIndex = 351;
            // 
            // Txt_Spare
            // 
            this.Txt_Spare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Spare.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_Spare.Location = new System.Drawing.Point(541, 29);
            this.Txt_Spare.MaxLength = 50;
            this.Txt_Spare.Multiline = true;
            this.Txt_Spare.Name = "Txt_Spare";
            this.Txt_Spare.Size = new System.Drawing.Size(557, 33);
            this.Txt_Spare.TabIndex = 1129;
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Cancel.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Cancel.Location = new System.Drawing.Point(1458, 13);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(150, 60);
            this.Btn_Cancel.TabIndex = 1128;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Cancel.Visible = false;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Save.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Save.Location = new System.Drawing.Point(1288, 13);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(150, 60);
            this.Btn_Save.TabIndex = 1127;
            this.Btn_Save.Text = "储存";
            this.Btn_Save.UseVisualStyleBackColor = false;
            this.Btn_Save.Visible = false;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // Txt_Coil
            // 
            this.Txt_Coil.Font = new System.Drawing.Font("微軟正黑體", 15.25F);
            this.Txt_Coil.Location = new System.Drawing.Point(160, 30);
            this.Txt_Coil.Name = "Txt_Coil";
            this.Txt_Coil.ReadOnly = true;
            this.Txt_Coil.Size = new System.Drawing.Size(221, 35);
            this.Txt_Coil.TabIndex = 1126;
            // 
            // Lbl_CoilNo
            // 
            this.Lbl_CoilNo.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_CoilNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_CoilNo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_CoilNo.Location = new System.Drawing.Point(20, 29);
            this.Lbl_CoilNo.Name = "Lbl_CoilNo";
            this.Lbl_CoilNo.Size = new System.Drawing.Size(140, 35);
            this.Lbl_CoilNo.TabIndex = 1125;
            this.Lbl_CoilNo.Text = "钢卷号码";
            this.Lbl_CoilNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_Spare
            // 
            this.Lbl_Spare.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_Spare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Spare.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Spare.Location = new System.Drawing.Point(401, 29);
            this.Lbl_Spare.Name = "Lbl_Spare";
            this.Lbl_Spare.Size = new System.Drawing.Size(140, 33);
            this.Lbl_Spare.TabIndex = 1123;
            this.Lbl_Spare.Text = "备注";
            this.Lbl_Spare.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Pnl_Bottom
            // 
            this.Pnl_Bottom.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Bottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Bottom.Controls.Add(this.Btn_Cancel);
            this.Pnl_Bottom.Controls.Add(this.Txt_Spare);
            this.Pnl_Bottom.Controls.Add(this.Btn_Save);
            this.Pnl_Bottom.Controls.Add(this.Lbl_CoilNo);
            this.Pnl_Bottom.Controls.Add(this.Txt_Coil);
            this.Pnl_Bottom.Controls.Add(this.Lbl_Spare);
            this.Pnl_Bottom.Controls.Add(this.Btn_Update);
            this.Pnl_Bottom.Location = new System.Drawing.Point(5, 893);
            this.Pnl_Bottom.Name = "Pnl_Bottom";
            this.Pnl_Bottom.Size = new System.Drawing.Size(1910, 84);
            this.Pnl_Bottom.TabIndex = 353;
            // 
            // Cob_Remarks_Type
            // 
            this.Cob_Remarks_Type.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Cob_Remarks_Type.FormattingEnabled = true;
            this.Cob_Remarks_Type.Location = new System.Drawing.Point(784, 53);
            this.Cob_Remarks_Type.Name = "Cob_Remarks_Type";
            this.Cob_Remarks_Type.Size = new System.Drawing.Size(263, 32);
            this.Cob_Remarks_Type.TabIndex = 1780;
            // 
            // Chk_Remarks_Type
            // 
            this.Chk_Remarks_Type.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_Remarks_Type.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.Chk_Remarks_Type.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Chk_Remarks_Type.Location = new System.Drawing.Point(624, 53);
            this.Chk_Remarks_Type.Name = "Chk_Remarks_Type";
            this.Chk_Remarks_Type.Size = new System.Drawing.Size(160, 33);
            this.Chk_Remarks_Type.TabIndex = 1779;
            this.Chk_Remarks_Type.Text = "删除记录类型";
            this.Chk_Remarks_Type.UseVisualStyleBackColor = false;
            // 
            // Frm_1_3_DeleteScheduleRecord
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1920, 982);
            this.Controls.Add(this.Dgv_DeleteSchedule);
            this.Controls.Add(this.Pnl_Bottom);
            this.Controls.Add(this.Pnl_Show);
            this.Controls.Add(this.Lbl_DelSchdlTitle);
            this.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_1_3_DeleteScheduleRecord";
            this.Text = "frm_1_3_DeleteCoilRecord";
            this.Load += new System.EventHandler(this.Frm_1_3_DeleteScheduleRecord_Load);
            this.Pnl_Show.ResumeLayout(false);
            this.Pnl_Show.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_DeleteSchedule)).EndInit();
            this.Pnl_Bottom.ResumeLayout(false);
            this.Pnl_Bottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.ComboBox Cob_DeleteCode;
        internal System.Windows.Forms.Button Btn_Update;
        internal System.Windows.Forms.Panel Pnl_Show;
        internal System.Windows.Forms.Button Btn_Search;
        internal System.Windows.Forms.Label Lbl_DelSchdlTitle;
        internal System.Windows.Forms.Label Lbl_Time_Range;
        internal System.Windows.Forms.DataGridView Dgv_DeleteSchedule;
        private System.Windows.Forms.Label Lbl_CoilNo;
        private System.Windows.Forms.Label Lbl_Spare;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_Save;
        internal System.Windows.Forms.CheckBox Chk_Time;
        internal System.Windows.Forms.CheckBox Chk_User;
        internal System.Windows.Forms.CheckBox Chk_DeleteCode;
        internal System.Windows.Forms.ComboBox Cob_User;
        internal System.Windows.Forms.CheckBox Chk_Coil;
        internal System.Windows.Forms.ComboBox Cob_Entry_Coil_No;
        internal System.Windows.Forms.TextBox Txt_Coil;
        internal System.Windows.Forms.TextBox Txt_Spare;
        internal System.Windows.Forms.DateTimePicker Dtp_Start_Time;
        internal System.Windows.Forms.DateTimePicker Dtp__Finish_Time;
        internal System.Windows.Forms.Panel Pnl_Bottom;
        internal System.Windows.Forms.ComboBox Cob_Remarks_Type;
        internal System.Windows.Forms.CheckBox Chk_Remarks_Type;
    }
}