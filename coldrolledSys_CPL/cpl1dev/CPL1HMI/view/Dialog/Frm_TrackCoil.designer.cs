namespace CPL1HMI
{
    partial class Frm_TrackCoil
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
            this.Cob_Coil_No = new System.Windows.Forms.ComboBox();
            this.Lbl_Select = new System.Windows.Forms.Label();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Lbl_Title = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Cob_Coil_No
            // 
            this.Cob_Coil_No.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cob_Coil_No.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_Coil_No.FormattingEnabled = true;
            this.Cob_Coil_No.IntegralHeight = false;
            this.Cob_Coil_No.ItemHeight = 30;
            this.Cob_Coil_No.Location = new System.Drawing.Point(113, 52);
            this.Cob_Coil_No.Margin = new System.Windows.Forms.Padding(4);
            this.Cob_Coil_No.MaxDropDownItems = 10;
            this.Cob_Coil_No.Name = "Cob_Coil_No";
            this.Cob_Coil_No.Size = new System.Drawing.Size(272, 38);
            this.Cob_Coil_No.TabIndex = 36;
            // 
            // Lbl_Select
            // 
            this.Lbl_Select.BackColor = System.Drawing.Color.SkyBlue;
            this.Lbl_Select.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_Select.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Select.Location = new System.Drawing.Point(20, 52);
            this.Lbl_Select.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Lbl_Select.Name = "Lbl_Select";
            this.Lbl_Select.Size = new System.Drawing.Size(93, 32);
            this.Lbl_Select.TabIndex = 35;
            this.Lbl_Select.Text = "选取钢卷";
            this.Lbl_Select.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.BackColor = System.Drawing.Color.LightPink;
            this.Btn_Cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Cancel.Location = new System.Drawing.Point(305, 94);
            this.Btn_Cancel.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(80, 40);
            this.Btn_Cancel.TabIndex = 34;
            this.Btn_Cancel.Text = "取消";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_OK
            // 
            this.Btn_OK.BackColor = System.Drawing.Color.LightGreen;
            this.Btn_OK.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_OK.Location = new System.Drawing.Point(205, 94);
            this.Btn_OK.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(80, 40);
            this.Btn_OK.TabIndex = 33;
            this.Btn_OK.Text = "确定";
            this.Btn_OK.UseVisualStyleBackColor = false;
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Lbl_Title
            // 
            this.Lbl_Title.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Lbl_Title.Location = new System.Drawing.Point(20, 10);
            this.Lbl_Title.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Lbl_Title.Name = "Lbl_Title";
            this.Lbl_Title.Size = new System.Drawing.Size(118, 32);
            this.Lbl_Title.TabIndex = 37;
            this.Lbl_Title.Text = "入-插入作业";
            this.Lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkOrange;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(5, 144);
            this.panel5.TabIndex = 46;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkOrange;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(405, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(5, 144);
            this.panel4.TabIndex = 47;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkOrange;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 149);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(410, 5);
            this.panel3.TabIndex = 48;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkOrange;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(410, 5);
            this.panel2.TabIndex = 45;
            // 
            // Frm_TrackCoil
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(410, 154);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Lbl_Title);
            this.Controls.Add(this.Cob_Coil_No);
            this.Controls.Add(this.Lbl_Select);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_TrackCoil";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Frm_TrackCoil";
            this.Load += new System.EventHandler(this.Frm_TrackCoil_Load);
            this.Shown += new System.EventHandler(this.Frm_TrackCoil_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox Cob_Coil_No;
        private System.Windows.Forms.Label Lbl_Select;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_OK;
        private System.Windows.Forms.Label Lbl_Title;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
    }
}