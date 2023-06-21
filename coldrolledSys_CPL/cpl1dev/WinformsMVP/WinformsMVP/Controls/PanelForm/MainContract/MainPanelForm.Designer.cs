namespace WinformsMVP
{
    public partial class MainPanelForm 
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
            this.Button_Cal = new System.Windows.Forms.Button();
            this.Label_Add = new System.Windows.Forms.Label();
            this.Label_Added = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Label_Answer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Button_Cal
            // 
            this.Button_Cal.Location = new System.Drawing.Point(376, 240);
            this.Button_Cal.Name = "Button_Cal";
            this.Button_Cal.Size = new System.Drawing.Size(75, 23);
            this.Button_Cal.TabIndex = 0;
            this.Button_Cal.Text = "計算";
            this.Button_Cal.UseVisualStyleBackColor = true;
            this.Button_Cal.Click += new System.EventHandler(this.Button_Cal_Click);
            // 
            // Label_Add
            // 
            this.Label_Add.AutoSize = true;
            this.Label_Add.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label_Add.Location = new System.Drawing.Point(313, 148);
            this.Label_Add.Name = "Label_Add";
            this.Label_Add.Size = new System.Drawing.Size(30, 32);
            this.Label_Add.TabIndex = 2;
            this.Label_Add.Text = "2";
            // 
            // Label_Added
            // 
            this.Label_Added.AutoSize = true;
            this.Label_Added.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label_Added.Location = new System.Drawing.Point(143, 148);
            this.Label_Added.Name = "Label_Added";
            this.Label_Added.Size = new System.Drawing.Size(30, 32);
            this.Label_Added.TabIndex = 3;
            this.Label_Added.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(228, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "+";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(383, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 32);
            this.label4.TabIndex = 5;
            this.label4.Text = "=";
            // 
            // Label_Answer
            // 
            this.Label_Answer.AutoSize = true;
            this.Label_Answer.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label_Answer.Location = new System.Drawing.Point(451, 148);
            this.Label_Answer.Name = "Label_Answer";
            this.Label_Answer.Size = new System.Drawing.Size(0, 32);
            this.Label_Answer.TabIndex = 6;
            // 
            // MainPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Label_Answer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Label_Added);
            this.Controls.Add(this.Label_Add);
            this.Controls.Add(this.Button_Cal);
            this.Name = "MainPanelForm";
            this.Text = "MainPanelForm";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_Cal;
        private System.Windows.Forms.Label Label_Add;
        private System.Windows.Forms.Label Label_Added;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Label_Answer;
    }
}