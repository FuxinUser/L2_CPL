namespace CPL1HMI
{
    partial class Frm_DialogCutLength
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Btn_Sure = new System.Windows.Forms.Button();
            this.Dgv_CutRecord_Temp = new System.Windows.Forms.DataGridView();
            this.Lbl_Title = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CutRecord_Temp)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.NavajoWhite;
            this.panel1.Controls.Add(this.Lbl_Title);
            this.panel1.Controls.Add(this.Btn_Close);
            this.panel1.Controls.Add(this.Btn_Sure);
            this.panel1.Controls.Add(this.Dgv_CutRecord_Temp);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1040, 460);
            this.panel1.TabIndex = 0;
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Close.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Close.Location = new System.Drawing.Point(941, 419);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(90, 33);
            this.Btn_Close.TabIndex = 1780;
            this.Btn_Close.Text = "取消";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_Sure
            // 
            this.Btn_Sure.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Sure.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Sure.Location = new System.Drawing.Point(831, 419);
            this.Btn_Sure.Name = "Btn_Sure";
            this.Btn_Sure.Size = new System.Drawing.Size(90, 33);
            this.Btn_Sure.TabIndex = 1779;
            this.Btn_Sure.Text = "确定";
            this.Btn_Sure.UseVisualStyleBackColor = false;
            this.Btn_Sure.Click += new System.EventHandler(this.Btn_Sure_Click);
            // 
            // Dgv_CutRecord_Temp
            // 
            this.Dgv_CutRecord_Temp.AllowUserToAddRows = false;
            this.Dgv_CutRecord_Temp.AllowUserToDeleteRows = false;
            this.Dgv_CutRecord_Temp.AllowUserToResizeColumns = false;
            this.Dgv_CutRecord_Temp.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_CutRecord_Temp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_CutRecord_Temp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_CutRecord_Temp.Location = new System.Drawing.Point(11, 52);
            this.Dgv_CutRecord_Temp.Name = "Dgv_CutRecord_Temp";
            this.Dgv_CutRecord_Temp.RowHeadersVisible = false;
            this.Dgv_CutRecord_Temp.RowHeadersWidth = 51;
            this.Dgv_CutRecord_Temp.RowTemplate.Height = 24;
            this.Dgv_CutRecord_Temp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_CutRecord_Temp.Size = new System.Drawing.Size(1020, 350);
            this.Dgv_CutRecord_Temp.TabIndex = 1778;
            // 
            // Lbl_Title
            // 
            this.Lbl_Title.AutoSize = true;
            this.Lbl_Title.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Title.Location = new System.Drawing.Point(16, 16);
            this.Lbl_Title.Name = "Lbl_Title";
            this.Lbl_Title.Size = new System.Drawing.Size(262, 26);
            this.Lbl_Title.TabIndex = 1781;
            this.Lbl_Title.Text = "计算{strPosition}切废长度";
            // 
            // Frm_DialogCutLength
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.ClientSize = new System.Drawing.Size(1050, 470);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Frm_DialogCutLength";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_DialogCutLength";
            this.Shown += new System.EventHandler(this.Frm_DialogCutLength_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_CutRecord_Temp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.DataGridView Dgv_CutRecord_Temp;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.Button Btn_Sure;
        private System.Windows.Forms.Label Lbl_Title;
    }
}