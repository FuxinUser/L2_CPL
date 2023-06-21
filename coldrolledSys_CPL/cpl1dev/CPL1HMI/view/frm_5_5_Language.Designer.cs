namespace CPL1HMI
{
    partial class frm_5_5_Language
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
            this.Dgv_Language = new System.Windows.Forms.DataGridView();
            this.Pnl_Top = new System.Windows.Forms.Panel();
            this.Rdo_KeywordEn = new System.Windows.Forms.RadioButton();
            this.Rdo_KeywordCn = new System.Windows.Forms.RadioButton();
            this.txtKeywordEnglish = new System.Windows.Forms.TextBox();
            this.txtKeywordChinese = new System.Windows.Forms.TextBox();
            this.Btn_Search = new System.Windows.Forms.Button();
            this.Btn_Edit = new System.Windows.Forms.Button();
            this.lblMainTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Language)).BeginInit();
            this.Pnl_Top.SuspendLayout();
            this.SuspendLayout();
            // 
            // Dgv_Language
            // 
            this.Dgv_Language.AllowUserToAddRows = false;
            this.Dgv_Language.AllowUserToDeleteRows = false;
            this.Dgv_Language.AllowUserToResizeColumns = false;
            this.Dgv_Language.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_Language.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_Language.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Language.Location = new System.Drawing.Point(5, 139);
            this.Dgv_Language.Name = "Dgv_Language";
            this.Dgv_Language.RowHeadersVisible = false;
            this.Dgv_Language.RowTemplate.Height = 24;
            this.Dgv_Language.Size = new System.Drawing.Size(1910, 838);
            this.Dgv_Language.TabIndex = 1083;
            this.Dgv_Language.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Language_CellClick);
            // 
            // Pnl_Top
            // 
            this.Pnl_Top.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Top.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pnl_Top.Controls.Add(this.Rdo_KeywordEn);
            this.Pnl_Top.Controls.Add(this.Rdo_KeywordCn);
            this.Pnl_Top.Controls.Add(this.txtKeywordEnglish);
            this.Pnl_Top.Controls.Add(this.txtKeywordChinese);
            this.Pnl_Top.Controls.Add(this.Btn_Search);
            this.Pnl_Top.Controls.Add(this.Btn_Edit);
            this.Pnl_Top.Location = new System.Drawing.Point(5, 50);
            this.Pnl_Top.Name = "Pnl_Top";
            this.Pnl_Top.Size = new System.Drawing.Size(1910, 84);
            this.Pnl_Top.TabIndex = 1082;
            // 
            // Rdo_KeywordEn
            // 
            this.Rdo_KeywordEn.BackColor = System.Drawing.Color.SkyBlue;
            this.Rdo_KeywordEn.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Rdo_KeywordEn.ForeColor = System.Drawing.Color.Black;
            this.Rdo_KeywordEn.Location = new System.Drawing.Point(495, 25);
            this.Rdo_KeywordEn.Name = "Rdo_KeywordEn";
            this.Rdo_KeywordEn.Size = new System.Drawing.Size(155, 33);
            this.Rdo_KeywordEn.TabIndex = 1088;
            this.Rdo_KeywordEn.Text = "英文关键字　";
            this.Rdo_KeywordEn.UseVisualStyleBackColor = false;
            // 
            // Rdo_KeywordCn
            // 
            this.Rdo_KeywordCn.BackColor = System.Drawing.Color.SkyBlue;
            this.Rdo_KeywordCn.Checked = true;
            this.Rdo_KeywordCn.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Rdo_KeywordCn.ForeColor = System.Drawing.Color.Black;
            this.Rdo_KeywordCn.Location = new System.Drawing.Point(20, 25);
            this.Rdo_KeywordCn.Name = "Rdo_KeywordCn";
            this.Rdo_KeywordCn.Size = new System.Drawing.Size(155, 33);
            this.Rdo_KeywordCn.TabIndex = 1087;
            this.Rdo_KeywordCn.TabStop = true;
            this.Rdo_KeywordCn.Text = "中文关键字";
            this.Rdo_KeywordCn.UseVisualStyleBackColor = false;
            // 
            // txtKeywordEnglish
            // 
            this.txtKeywordEnglish.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeywordEnglish.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtKeywordEnglish.Location = new System.Drawing.Point(650, 25);
            this.txtKeywordEnglish.Name = "txtKeywordEnglish";
            this.txtKeywordEnglish.Size = new System.Drawing.Size(300, 33);
            this.txtKeywordEnglish.TabIndex = 1086;
            // 
            // txtKeywordChinese
            // 
            this.txtKeywordChinese.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeywordChinese.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtKeywordChinese.Location = new System.Drawing.Point(175, 25);
            this.txtKeywordChinese.Name = "txtKeywordChinese";
            this.txtKeywordChinese.Size = new System.Drawing.Size(300, 33);
            this.txtKeywordChinese.TabIndex = 1085;
            // 
            // Btn_Search
            // 
            this.Btn_Search.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Search.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Search.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Search.Location = new System.Drawing.Point(970, 11);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(150, 60);
            this.Btn_Search.TabIndex = 1084;
            this.Btn_Search.Text = "查询";
            this.Btn_Search.UseVisualStyleBackColor = false;
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // Btn_Edit
            // 
            this.Btn_Edit.BackColor = System.Drawing.Color.Gold;
            this.Btn_Edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Edit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Edit.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Edit.Location = new System.Drawing.Point(1140, 11);
            this.Btn_Edit.Name = "Btn_Edit";
            this.Btn_Edit.Size = new System.Drawing.Size(150, 60);
            this.Btn_Edit.TabIndex = 1083;
            this.Btn_Edit.Text = "修改";
            this.Btn_Edit.UseVisualStyleBackColor = false;
            this.Btn_Edit.Click += new System.EventHandler(this.Btn_Edit_Click);
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            this.lblMainTitle.ForeColor = System.Drawing.Color.Cyan;
            this.lblMainTitle.Location = new System.Drawing.Point(0, 10);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(1920, 35);
            this.lblMainTitle.TabIndex = 1081;
            this.lblMainTitle.Text = "5-5 中英文对照";
            this.lblMainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm_5_5_Language
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1920, 982);
            this.Controls.Add(this.Dgv_Language);
            this.Controls.Add(this.Pnl_Top);
            this.Controls.Add(this.lblMainTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_5_5_Language";
            this.Text = "frm_5_5_Language";
            this.Load += new System.EventHandler(this.Frm_5_5_Language_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Language)).EndInit();
            this.Pnl_Top.ResumeLayout(false);
            this.Pnl_Top.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView Dgv_Language;
        internal System.Windows.Forms.Panel Pnl_Top;
        internal System.Windows.Forms.Label lblMainTitle;
        internal System.Windows.Forms.RadioButton Rdo_KeywordEn;
        internal System.Windows.Forms.RadioButton Rdo_KeywordCn;
        internal System.Windows.Forms.TextBox txtKeywordEnglish;
        internal System.Windows.Forms.TextBox txtKeywordChinese;
        internal System.Windows.Forms.Button Btn_Search;
        internal System.Windows.Forms.Button Btn_Edit;
    }
}