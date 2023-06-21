namespace CPL1HMI
{
    partial class frm_Entry
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
            this.lbSkid = new System.Windows.Forms.Label();
            this.btn_True = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.dgv_off = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_off)).BeginInit();
            this.SuspendLayout();
            // 
            // lbSkid
            // 
            this.lbSkid.AutoSize = true;
            this.lbSkid.Location = new System.Drawing.Point(14, 9);
            this.lbSkid.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbSkid.Name = "lbSkid";
            this.lbSkid.Size = new System.Drawing.Size(62, 24);
            this.lbSkid.TabIndex = 1;
            this.lbSkid.Text = "鞍座 : ";
            // 
            // btn_True
            // 
            this.btn_True.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_True.Location = new System.Drawing.Point(574, 220);
            this.btn_True.Name = "btn_True";
            this.btn_True.Size = new System.Drawing.Size(75, 31);
            this.btn_True.TabIndex = 4;
            this.btn_True.Text = "确定";
            this.btn_True.UseVisualStyleBackColor = false;
            this.btn_True.Click += new System.EventHandler(this.Btn_True_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_Close.Location = new System.Drawing.Point(671, 220);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 31);
            this.btn_Close.TabIndex = 5;
            this.btn_Close.Text = "取消";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // dgv_off
            // 
            this.dgv_off.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_off.Location = new System.Drawing.Point(12, 50);
            this.dgv_off.Name = "dgv_off";
            this.dgv_off.RowHeadersVisible = false;
            this.dgv_off.RowTemplate.Height = 24;
            this.dgv_off.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_off.Size = new System.Drawing.Size(731, 164);
            this.dgv_off.TabIndex = 6;
            // 
            // frm_Entry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 263);
            this.Controls.Add(this.dgv_off);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_True);
            this.Controls.Add(this.lbSkid);
            this.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "frm_Entry";
            this.Text = "入料";
            this.Load += new System.EventHandler(this.Frm_Entry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_off)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSkid;
        private System.Windows.Forms.Button btn_True;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.DataGridView dgv_off;
    }
}