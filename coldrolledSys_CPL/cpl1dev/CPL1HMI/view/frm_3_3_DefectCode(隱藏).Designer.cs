namespace CPL1HMI
{
    partial class frm_3_3_DefectCode
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
            this.dgv_List = new System.Windows.Forms.DataGridView();
            this.Label2 = new System.Windows.Forms.Label();
            this.pnlShow = new System.Windows.Forms.Panel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Edit = new System.Windows.Forms.Button();
            this.Label42 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtMatcoilid = new System.Windows.Forms.TextBox();
            this.btnQueryData = new System.Windows.Forms.Button();
            this.btnUpdatePDIData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_List)).BeginInit();
            this.pnlShow.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_List
            // 
            this.dgv_List.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_List.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_List.Location = new System.Drawing.Point(15, 153);
            this.dgv_List.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dgv_List.Name = "dgv_List";
            this.dgv_List.ReadOnly = true;
            this.dgv_List.RowHeadersVisible = false;
            this.dgv_List.RowTemplate.Height = 24;
            this.dgv_List.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_List.Size = new System.Drawing.Size(1880, 499);
            this.dgv_List.TabIndex = 0;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.Cyan;
            this.Label2.Location = new System.Drawing.Point(877, 9);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(185, 25);
            this.Label2.TabIndex = 1100;
            this.Label2.Text = "3-3 鋼卷缺陷資料";
            // 
            // pnlShow
            // 
            this.pnlShow.BackColor = System.Drawing.Color.GreenYellow;
            this.pnlShow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlShow.Controls.Add(this.btn_Save);
            this.pnlShow.Controls.Add(this.btn_Edit);
            this.pnlShow.Controls.Add(this.Label42);
            this.pnlShow.Controls.Add(this.Label1);
            this.pnlShow.Controls.Add(this.txtMatcoilid);
            this.pnlShow.Controls.Add(this.btnQueryData);
            this.pnlShow.Controls.Add(this.btnUpdatePDIData);
            this.pnlShow.Location = new System.Drawing.Point(15, 57);
            this.pnlShow.Name = "pnlShow";
            this.pnlShow.Size = new System.Drawing.Size(1880, 88);
            this.pnlShow.TabIndex = 1101;
            // 
            // btn_Save
            // 
            this.btn_Save.BackgroundImage = global::CPL1HMI.Properties.Resources.按鈕;
            this.btn_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Save.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Save.Location = new System.Drawing.Point(1296, 5);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(118, 76);
            this.btn_Save.TabIndex = 1078;
            this.btn_Save.Text = "儲存";
            this.btn_Save.UseVisualStyleBackColor = true;
            // 
            // btn_Edit
            // 
            this.btn_Edit.BackgroundImage = global::CPL1HMI.Properties.Resources.按鈕;
            this.btn_Edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Edit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Edit.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Edit.Location = new System.Drawing.Point(1162, 5);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(118, 76);
            this.btn_Edit.TabIndex = 1077;
            this.btn_Edit.Text = "編輯";
            this.btn_Edit.UseVisualStyleBackColor = true;
            // 
            // Label42
            // 
            this.Label42.AutoSize = true;
            this.Label42.BackColor = System.Drawing.Color.Transparent;
            this.Label42.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label42.ForeColor = System.Drawing.Color.Red;
            this.Label42.Location = new System.Drawing.Point(388, 115);
            this.Label42.Name = "Label42";
            this.Label42.Size = new System.Drawing.Size(0, 16);
            this.Label42.TabIndex = 1076;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label1.Location = new System.Drawing.Point(31, 30);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(110, 24);
            this.Label1.TabIndex = 293;
            this.Label1.Text = "鋼捲號碼";
            // 
            // txtMatcoilid
            // 
            this.txtMatcoilid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtMatcoilid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMatcoilid.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtMatcoilid.Location = new System.Drawing.Point(180, 24);
            this.txtMatcoilid.MaxLength = 20;
            this.txtMatcoilid.Name = "txtMatcoilid";
            this.txtMatcoilid.Size = new System.Drawing.Size(321, 35);
            this.txtMatcoilid.TabIndex = 290;
            // 
            // btnQueryData
            // 
            this.btnQueryData.BackgroundImage = global::CPL1HMI.Properties.Resources.按鈕;
            this.btnQueryData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQueryData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQueryData.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnQueryData.Location = new System.Drawing.Point(513, 5);
            this.btnQueryData.Name = "btnQueryData";
            this.btnQueryData.Size = new System.Drawing.Size(118, 76);
            this.btnQueryData.TabIndex = 291;
            this.btnQueryData.Text = "查詢";
            this.btnQueryData.UseVisualStyleBackColor = true;
            // 
            // btnUpdatePDIData
            // 
            this.btnUpdatePDIData.BackgroundImage = global::CPL1HMI.Properties.Resources.按鈕;
            this.btnUpdatePDIData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdatePDIData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdatePDIData.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Bold);
            this.btnUpdatePDIData.Location = new System.Drawing.Point(1431, 5);
            this.btnUpdatePDIData.Name = "btnUpdatePDIData";
            this.btnUpdatePDIData.Size = new System.Drawing.Size(118, 76);
            this.btnUpdatePDIData.TabIndex = 13;
            this.btnUpdatePDIData.Text = "更新";
            this.btnUpdatePDIData.UseVisualStyleBackColor = true;
            // 
            // frm_3_3_DefectCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1910, 981);
            this.Controls.Add(this.pnlShow);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.dgv_List);
            this.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "frm_3_3_DefectCode";
            this.Text = "frm_DefectCode_Test";
            this.Load += new System.EventHandler(this.frm_DefectCode_Test_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_List)).EndInit();
            this.pnlShow.ResumeLayout(false);
            this.pnlShow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_List;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Panel pnlShow;
        internal System.Windows.Forms.Button btn_Save;
        internal System.Windows.Forms.Button btn_Edit;
        internal System.Windows.Forms.Label Label42;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtMatcoilid;
        internal System.Windows.Forms.Button btnQueryData;
        internal System.Windows.Forms.Button btnUpdatePDIData;
    }
}