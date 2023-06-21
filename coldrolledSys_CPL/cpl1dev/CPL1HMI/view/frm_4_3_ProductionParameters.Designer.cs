namespace CPL1HMI
{
    partial class frm_4_3_ProductionParameters
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
            this.Lbl_MainTitle = new System.Windows.Forms.Label();
            this.Txt_Thickness = new System.Windows.Forms.TextBox();
            this.Cob_SteelGrade = new System.Windows.Forms.ComboBox();
            this.Btn_Query = new System.Windows.Forms.Button();
            this.Btn_Reload = new System.Windows.Forms.Button();
            this.Chk_Thickness = new System.Windows.Forms.CheckBox();
            this.Chk_SteelGrade = new System.Windows.Forms.CheckBox();
            this.Btn_Tension_Delete = new System.Windows.Forms.Button();
            this.Btn_Tension_Insert = new System.Windows.Forms.Button();
            this.Btn_Tension_Update = new System.Windows.Forms.Button();
            this.Tab_MainControl = new System.Windows.Forms.TabControl();
            this.Tab_Main_TensionPage = new System.Windows.Forms.TabPage();
            this.Pnl_Tension = new System.Windows.Forms.Panel();
            this.Dgv_Tension_Edit = new System.Windows.Forms.DataGridView();
            this.Pnl_Tension_Bottom = new System.Windows.Forms.Panel();
            this.Btn_Tension_Cancel = new System.Windows.Forms.Button();
            this.Btn_Tension_Save = new System.Windows.Forms.Button();
            this.Dgv_Tension = new System.Windows.Forms.DataGridView();
            this.Tab_Main_FlattenerPage = new System.Windows.Forms.TabPage();
            this.Pnl_Flattener = new System.Windows.Forms.Panel();
            this.Dgv_Flattener_Edit = new System.Windows.Forms.DataGridView();
            this.Pnl_Flattener_Bottom = new System.Windows.Forms.Panel();
            this.Btn_Flattener_Insert = new System.Windows.Forms.Button();
            this.Btn_Flattener_Cancel = new System.Windows.Forms.Button();
            this.Btn_Flattener_Update = new System.Windows.Forms.Button();
            this.Btn_Flattener_Save = new System.Windows.Forms.Button();
            this.Btn_Flattener_Delete = new System.Windows.Forms.Button();
            this.Dgv_Flattener = new System.Windows.Forms.DataGridView();
            this.Tab_Main_SideTrimmerPage = new System.Windows.Forms.TabPage();
            this.Pnl_Trimmer = new System.Windows.Forms.Panel();
            this.Dgv_SideTrimmer_Edit = new System.Windows.Forms.DataGridView();
            this.Pnl_Trimmer_Bottom = new System.Windows.Forms.Panel();
            this.Btn_Trimmer_Cancel = new System.Windows.Forms.Button();
            this.Btn_Trimmer_Insert = new System.Windows.Forms.Button();
            this.Btn_Trimmer_Save = new System.Windows.Forms.Button();
            this.Btn_Trimmer_Delete = new System.Windows.Forms.Button();
            this.Btn_Trimmer_Update = new System.Windows.Forms.Button();
            this.Dgv_SideTrimmer = new System.Windows.Forms.DataGridView();
            this.Pnl_Top = new System.Windows.Forms.Panel();
            this.Tab_MainControl.SuspendLayout();
            this.Tab_Main_TensionPage.SuspendLayout();
            this.Pnl_Tension.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Tension_Edit)).BeginInit();
            this.Pnl_Tension_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Tension)).BeginInit();
            this.Tab_Main_FlattenerPage.SuspendLayout();
            this.Pnl_Flattener.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Flattener_Edit)).BeginInit();
            this.Pnl_Flattener_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Flattener)).BeginInit();
            this.Tab_Main_SideTrimmerPage.SuspendLayout();
            this.Pnl_Trimmer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_SideTrimmer_Edit)).BeginInit();
            this.Pnl_Trimmer_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_SideTrimmer)).BeginInit();
            this.Pnl_Top.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lbl_MainTitle
            // 
            this.Lbl_MainTitle.Font = new System.Drawing.Font("微軟正黑體", 20F, System.Drawing.FontStyle.Bold);
            this.Lbl_MainTitle.ForeColor = System.Drawing.Color.Cyan;
            this.Lbl_MainTitle.Location = new System.Drawing.Point(0, 10);
            this.Lbl_MainTitle.Name = "Lbl_MainTitle";
            this.Lbl_MainTitle.Size = new System.Drawing.Size(1920, 35);
            this.Lbl_MainTitle.TabIndex = 1309;
            this.Lbl_MainTitle.Text = "4-3 设备参数";
            this.Lbl_MainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Txt_Thickness
            // 
            this.Txt_Thickness.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Txt_Thickness.Location = new System.Drawing.Point(410, 25);
            this.Txt_Thickness.MaxLength = 12;
            this.Txt_Thickness.Name = "Txt_Thickness";
            this.Txt_Thickness.Size = new System.Drawing.Size(184, 33);
            this.Txt_Thickness.TabIndex = 357;
            // 
            // Cob_SteelGrade
            // 
            this.Cob_SteelGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cob_SteelGrade.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cob_SteelGrade.FormattingEnabled = true;
            this.Cob_SteelGrade.Items.AddRange(new object[] {
            "N",
            "M",
            "A"});
            this.Cob_SteelGrade.Location = new System.Drawing.Point(98, 25);
            this.Cob_SteelGrade.Name = "Cob_SteelGrade";
            this.Cob_SteelGrade.Size = new System.Drawing.Size(214, 32);
            this.Cob_SteelGrade.TabIndex = 353;
            this.Cob_SteelGrade.Click += new System.EventHandler(this.Cbo_SteelGrade_Click);
            // 
            // Btn_Query
            // 
            this.Btn_Query.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Btn_Query.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Query.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Query.ForeColor = System.Drawing.Color.Black;
            this.Btn_Query.Location = new System.Drawing.Point(614, 11);
            this.Btn_Query.Name = "Btn_Query";
            this.Btn_Query.Size = new System.Drawing.Size(150, 60);
            this.Btn_Query.TabIndex = 315;
            this.Btn_Query.Text = "查询";
            this.Btn_Query.UseVisualStyleBackColor = false;
            this.Btn_Query.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // Btn_Reload
            // 
            this.Btn_Reload.BackColor = System.Drawing.Color.Gold;
            this.Btn_Reload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Reload.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Reload.ForeColor = System.Drawing.Color.Black;
            this.Btn_Reload.Location = new System.Drawing.Point(1740, 10);
            this.Btn_Reload.Name = "Btn_Reload";
            this.Btn_Reload.Size = new System.Drawing.Size(150, 60);
            this.Btn_Reload.TabIndex = 362;
            this.Btn_Reload.Text = "重新整理";
            this.Btn_Reload.UseVisualStyleBackColor = false;
            this.Btn_Reload.Click += new System.EventHandler(this.Btn_Reload_Click);
            // 
            // Chk_Thickness
            // 
            this.Chk_Thickness.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_Thickness.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_Thickness.Location = new System.Drawing.Point(332, 25);
            this.Chk_Thickness.Name = "Chk_Thickness";
            this.Chk_Thickness.Size = new System.Drawing.Size(78, 33);
            this.Chk_Thickness.TabIndex = 360;
            this.Chk_Thickness.Text = "厚度";
            this.Chk_Thickness.UseVisualStyleBackColor = false;
            // 
            // Chk_SteelGrade
            // 
            this.Chk_SteelGrade.BackColor = System.Drawing.Color.SkyBlue;
            this.Chk_SteelGrade.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Chk_SteelGrade.Location = new System.Drawing.Point(20, 25);
            this.Chk_SteelGrade.Name = "Chk_SteelGrade";
            this.Chk_SteelGrade.Size = new System.Drawing.Size(78, 32);
            this.Chk_SteelGrade.TabIndex = 359;
            this.Chk_SteelGrade.Text = "钢种";
            this.Chk_SteelGrade.UseVisualStyleBackColor = false;
            // 
            // Btn_Tension_Delete
            // 
            this.Btn_Tension_Delete.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Tension_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Tension_Delete.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Tension_Delete.ForeColor = System.Drawing.Color.Black;
            this.Btn_Tension_Delete.Location = new System.Drawing.Point(360, 11);
            this.Btn_Tension_Delete.Name = "Btn_Tension_Delete";
            this.Btn_Tension_Delete.Size = new System.Drawing.Size(150, 60);
            this.Btn_Tension_Delete.TabIndex = 364;
            this.Btn_Tension_Delete.Text = "删除";
            this.Btn_Tension_Delete.UseVisualStyleBackColor = false;
            this.Btn_Tension_Delete.Click += new System.EventHandler(this.Btn_Tension_Delete_Click);
            // 
            // Btn_Tension_Insert
            // 
            this.Btn_Tension_Insert.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Tension_Insert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Tension_Insert.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Tension_Insert.ForeColor = System.Drawing.Color.Black;
            this.Btn_Tension_Insert.Location = new System.Drawing.Point(20, 11);
            this.Btn_Tension_Insert.Name = "Btn_Tension_Insert";
            this.Btn_Tension_Insert.Size = new System.Drawing.Size(150, 60);
            this.Btn_Tension_Insert.TabIndex = 363;
            this.Btn_Tension_Insert.Text = "新增";
            this.Btn_Tension_Insert.UseVisualStyleBackColor = false;
            this.Btn_Tension_Insert.Click += new System.EventHandler(this.Btn_Tension_Insert_Click);
            // 
            // Btn_Tension_Update
            // 
            this.Btn_Tension_Update.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Tension_Update.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Tension_Update.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Tension_Update.ForeColor = System.Drawing.Color.Black;
            this.Btn_Tension_Update.Location = new System.Drawing.Point(190, 11);
            this.Btn_Tension_Update.Name = "Btn_Tension_Update";
            this.Btn_Tension_Update.Size = new System.Drawing.Size(150, 60);
            this.Btn_Tension_Update.TabIndex = 362;
            this.Btn_Tension_Update.Text = "修改";
            this.Btn_Tension_Update.UseVisualStyleBackColor = false;
            this.Btn_Tension_Update.Click += new System.EventHandler(this.Btn_Tension_Update_Click);
            // 
            // Tab_MainControl
            // 
            this.Tab_MainControl.Controls.Add(this.Tab_Main_TensionPage);
            this.Tab_MainControl.Controls.Add(this.Tab_Main_FlattenerPage);
            this.Tab_MainControl.Controls.Add(this.Tab_Main_SideTrimmerPage);
            this.Tab_MainControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.Tab_MainControl.Location = new System.Drawing.Point(5, 139);
            this.Tab_MainControl.Name = "Tab_MainControl";
            this.Tab_MainControl.SelectedIndex = 0;
            this.Tab_MainControl.Size = new System.Drawing.Size(1910, 838);
            this.Tab_MainControl.TabIndex = 1314;
            this.Tab_MainControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabControl1_DrawItem);
            // 
            // Tab_Main_TensionPage
            // 
            this.Tab_Main_TensionPage.Controls.Add(this.Pnl_Tension);
            this.Tab_Main_TensionPage.Controls.Add(this.Pnl_Tension_Bottom);
            this.Tab_Main_TensionPage.Controls.Add(this.Dgv_Tension);
            this.Tab_Main_TensionPage.Location = new System.Drawing.Point(4, 35);
            this.Tab_Main_TensionPage.Name = "Tab_Main_TensionPage";
            this.Tab_Main_TensionPage.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Main_TensionPage.Size = new System.Drawing.Size(1902, 799);
            this.Tab_Main_TensionPage.TabIndex = 0;
            this.Tab_Main_TensionPage.Text = "张力机参数";
            this.Tab_Main_TensionPage.UseVisualStyleBackColor = true;
            // 
            // Pnl_Tension
            // 
            this.Pnl_Tension.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Tension.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Tension.Controls.Add(this.Dgv_Tension_Edit);
            this.Pnl_Tension.Location = new System.Drawing.Point(0, 519);
            this.Pnl_Tension.Name = "Pnl_Tension";
            this.Pnl_Tension.Size = new System.Drawing.Size(1689, 187);
            this.Pnl_Tension.TabIndex = 1779;
            this.Pnl_Tension.Visible = false;
            // 
            // Dgv_Tension_Edit
            // 
            this.Dgv_Tension_Edit.AllowUserToAddRows = false;
            this.Dgv_Tension_Edit.AllowUserToDeleteRows = false;
            this.Dgv_Tension_Edit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dgv_Tension_Edit.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Dgv_Tension_Edit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Tension_Edit.Location = new System.Drawing.Point(13, 17);
            this.Dgv_Tension_Edit.Name = "Dgv_Tension_Edit";
            this.Dgv_Tension_Edit.RowHeadersVisible = false;
            this.Dgv_Tension_Edit.RowTemplate.Height = 24;
            this.Dgv_Tension_Edit.Size = new System.Drawing.Size(1657, 153);
            this.Dgv_Tension_Edit.TabIndex = 0;
            // 
            // Pnl_Tension_Bottom
            // 
            this.Pnl_Tension_Bottom.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Tension_Bottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Tension_Bottom.Controls.Add(this.Btn_Tension_Insert);
            this.Pnl_Tension_Bottom.Controls.Add(this.Btn_Tension_Cancel);
            this.Pnl_Tension_Bottom.Controls.Add(this.Btn_Tension_Delete);
            this.Pnl_Tension_Bottom.Controls.Add(this.Btn_Tension_Save);
            this.Pnl_Tension_Bottom.Controls.Add(this.Btn_Tension_Update);
            this.Pnl_Tension_Bottom.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Pnl_Tension_Bottom.Location = new System.Drawing.Point(0, 713);
            this.Pnl_Tension_Bottom.Name = "Pnl_Tension_Bottom";
            this.Pnl_Tension_Bottom.Size = new System.Drawing.Size(1902, 84);
            this.Pnl_Tension_Bottom.TabIndex = 1778;
            // 
            // Btn_Tension_Cancel
            // 
            this.Btn_Tension_Cancel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Tension_Cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Tension_Cancel.Location = new System.Drawing.Point(700, 11);
            this.Btn_Tension_Cancel.Name = "Btn_Tension_Cancel";
            this.Btn_Tension_Cancel.Size = new System.Drawing.Size(150, 60);
            this.Btn_Tension_Cancel.TabIndex = 1277;
            this.Btn_Tension_Cancel.Text = "取消";
            this.Btn_Tension_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Tension_Cancel.Visible = false;
            this.Btn_Tension_Cancel.Click += new System.EventHandler(this.Btn_Tension_Cancel_Click);
            // 
            // Btn_Tension_Save
            // 
            this.Btn_Tension_Save.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Tension_Save.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Tension_Save.Location = new System.Drawing.Point(530, 11);
            this.Btn_Tension_Save.Name = "Btn_Tension_Save";
            this.Btn_Tension_Save.Size = new System.Drawing.Size(150, 60);
            this.Btn_Tension_Save.TabIndex = 1276;
            this.Btn_Tension_Save.Text = "确认";
            this.Btn_Tension_Save.UseVisualStyleBackColor = false;
            this.Btn_Tension_Save.Visible = false;
            this.Btn_Tension_Save.Click += new System.EventHandler(this.Btn_Tension_Save_Click);
            // 
            // Dgv_Tension
            // 
            this.Dgv_Tension.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Dgv_Tension.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Tension.Location = new System.Drawing.Point(0, 0);
            this.Dgv_Tension.MultiSelect = false;
            this.Dgv_Tension.Name = "Dgv_Tension";
            this.Dgv_Tension.ReadOnly = true;
            this.Dgv_Tension.RowHeadersVisible = false;
            this.Dgv_Tension.RowTemplate.Height = 24;
            this.Dgv_Tension.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Tension.Size = new System.Drawing.Size(1902, 708);
            this.Dgv_Tension.TabIndex = 365;
            // 
            // Tab_Main_FlattenerPage
            // 
            this.Tab_Main_FlattenerPage.Controls.Add(this.Pnl_Flattener);
            this.Tab_Main_FlattenerPage.Controls.Add(this.Pnl_Flattener_Bottom);
            this.Tab_Main_FlattenerPage.Controls.Add(this.Dgv_Flattener);
            this.Tab_Main_FlattenerPage.Location = new System.Drawing.Point(4, 35);
            this.Tab_Main_FlattenerPage.Name = "Tab_Main_FlattenerPage";
            this.Tab_Main_FlattenerPage.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Main_FlattenerPage.Size = new System.Drawing.Size(1902, 799);
            this.Tab_Main_FlattenerPage.TabIndex = 1;
            this.Tab_Main_FlattenerPage.Text = "整平机参数";
            this.Tab_Main_FlattenerPage.UseVisualStyleBackColor = true;
            // 
            // Pnl_Flattener
            // 
            this.Pnl_Flattener.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Flattener.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Flattener.Controls.Add(this.Dgv_Flattener_Edit);
            this.Pnl_Flattener.Location = new System.Drawing.Point(0, 519);
            this.Pnl_Flattener.Name = "Pnl_Flattener";
            this.Pnl_Flattener.Size = new System.Drawing.Size(1689, 187);
            this.Pnl_Flattener.TabIndex = 1780;
            this.Pnl_Flattener.Visible = false;
            // 
            // Dgv_Flattener_Edit
            // 
            this.Dgv_Flattener_Edit.AllowUserToAddRows = false;
            this.Dgv_Flattener_Edit.AllowUserToDeleteRows = false;
            this.Dgv_Flattener_Edit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dgv_Flattener_Edit.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Dgv_Flattener_Edit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Flattener_Edit.Location = new System.Drawing.Point(13, 17);
            this.Dgv_Flattener_Edit.Name = "Dgv_Flattener_Edit";
            this.Dgv_Flattener_Edit.RowHeadersVisible = false;
            this.Dgv_Flattener_Edit.RowTemplate.Height = 24;
            this.Dgv_Flattener_Edit.Size = new System.Drawing.Size(1657, 153);
            this.Dgv_Flattener_Edit.TabIndex = 0;
            // 
            // Pnl_Flattener_Bottom
            // 
            this.Pnl_Flattener_Bottom.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Flattener_Bottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Flattener_Bottom.Controls.Add(this.Btn_Flattener_Insert);
            this.Pnl_Flattener_Bottom.Controls.Add(this.Btn_Flattener_Cancel);
            this.Pnl_Flattener_Bottom.Controls.Add(this.Btn_Flattener_Update);
            this.Pnl_Flattener_Bottom.Controls.Add(this.Btn_Flattener_Save);
            this.Pnl_Flattener_Bottom.Controls.Add(this.Btn_Flattener_Delete);
            this.Pnl_Flattener_Bottom.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Pnl_Flattener_Bottom.Location = new System.Drawing.Point(0, 713);
            this.Pnl_Flattener_Bottom.Name = "Pnl_Flattener_Bottom";
            this.Pnl_Flattener_Bottom.Size = new System.Drawing.Size(1902, 84);
            this.Pnl_Flattener_Bottom.TabIndex = 1779;
            // 
            // Btn_Flattener_Insert
            // 
            this.Btn_Flattener_Insert.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Flattener_Insert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Flattener_Insert.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Flattener_Insert.ForeColor = System.Drawing.Color.Black;
            this.Btn_Flattener_Insert.Location = new System.Drawing.Point(20, 11);
            this.Btn_Flattener_Insert.Name = "Btn_Flattener_Insert";
            this.Btn_Flattener_Insert.Size = new System.Drawing.Size(150, 60);
            this.Btn_Flattener_Insert.TabIndex = 365;
            this.Btn_Flattener_Insert.Text = "新增";
            this.Btn_Flattener_Insert.UseVisualStyleBackColor = false;
            this.Btn_Flattener_Insert.Click += new System.EventHandler(this.Btn_Flattener_Insert_Click);
            // 
            // Btn_Flattener_Cancel
            // 
            this.Btn_Flattener_Cancel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Flattener_Cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Flattener_Cancel.Location = new System.Drawing.Point(700, 11);
            this.Btn_Flattener_Cancel.Name = "Btn_Flattener_Cancel";
            this.Btn_Flattener_Cancel.Size = new System.Drawing.Size(150, 60);
            this.Btn_Flattener_Cancel.TabIndex = 1279;
            this.Btn_Flattener_Cancel.Text = "取消";
            this.Btn_Flattener_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Flattener_Cancel.Visible = false;
            this.Btn_Flattener_Cancel.Click += new System.EventHandler(this.Btn_Flattener_Cancel_Click);
            // 
            // Btn_Flattener_Update
            // 
            this.Btn_Flattener_Update.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Flattener_Update.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Flattener_Update.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Flattener_Update.ForeColor = System.Drawing.Color.Black;
            this.Btn_Flattener_Update.Location = new System.Drawing.Point(190, 11);
            this.Btn_Flattener_Update.Name = "Btn_Flattener_Update";
            this.Btn_Flattener_Update.Size = new System.Drawing.Size(150, 60);
            this.Btn_Flattener_Update.TabIndex = 364;
            this.Btn_Flattener_Update.Text = "修改";
            this.Btn_Flattener_Update.UseVisualStyleBackColor = false;
            this.Btn_Flattener_Update.Click += new System.EventHandler(this.Btn_Flattener_Update_Click);
            // 
            // Btn_Flattener_Save
            // 
            this.Btn_Flattener_Save.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Flattener_Save.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Flattener_Save.Location = new System.Drawing.Point(530, 11);
            this.Btn_Flattener_Save.Name = "Btn_Flattener_Save";
            this.Btn_Flattener_Save.Size = new System.Drawing.Size(150, 60);
            this.Btn_Flattener_Save.TabIndex = 1278;
            this.Btn_Flattener_Save.Text = "确认";
            this.Btn_Flattener_Save.UseVisualStyleBackColor = false;
            this.Btn_Flattener_Save.Visible = false;
            this.Btn_Flattener_Save.Click += new System.EventHandler(this.Btn_Flattener_Save_Click);
            // 
            // Btn_Flattener_Delete
            // 
            this.Btn_Flattener_Delete.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Flattener_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Flattener_Delete.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Flattener_Delete.ForeColor = System.Drawing.Color.Black;
            this.Btn_Flattener_Delete.Location = new System.Drawing.Point(360, 11);
            this.Btn_Flattener_Delete.Name = "Btn_Flattener_Delete";
            this.Btn_Flattener_Delete.Size = new System.Drawing.Size(150, 60);
            this.Btn_Flattener_Delete.TabIndex = 365;
            this.Btn_Flattener_Delete.Text = "删除";
            this.Btn_Flattener_Delete.UseVisualStyleBackColor = false;
            this.Btn_Flattener_Delete.Click += new System.EventHandler(this.Btn_Flattener_Delete_Click);
            // 
            // Dgv_Flattener
            // 
            this.Dgv_Flattener.AllowUserToAddRows = false;
            this.Dgv_Flattener.AllowUserToDeleteRows = false;
            this.Dgv_Flattener.AllowUserToResizeColumns = false;
            this.Dgv_Flattener.AllowUserToResizeRows = false;
            this.Dgv_Flattener.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Dgv_Flattener.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_Flattener.Location = new System.Drawing.Point(0, 0);
            this.Dgv_Flattener.MultiSelect = false;
            this.Dgv_Flattener.Name = "Dgv_Flattener";
            this.Dgv_Flattener.ReadOnly = true;
            this.Dgv_Flattener.RowHeadersVisible = false;
            this.Dgv_Flattener.RowTemplate.Height = 24;
            this.Dgv_Flattener.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_Flattener.Size = new System.Drawing.Size(1902, 708);
            this.Dgv_Flattener.TabIndex = 1308;
            // 
            // Tab_Main_SideTrimmerPage
            // 
            this.Tab_Main_SideTrimmerPage.Controls.Add(this.Pnl_Trimmer);
            this.Tab_Main_SideTrimmerPage.Controls.Add(this.Pnl_Trimmer_Bottom);
            this.Tab_Main_SideTrimmerPage.Controls.Add(this.Dgv_SideTrimmer);
            this.Tab_Main_SideTrimmerPage.Location = new System.Drawing.Point(4, 35);
            this.Tab_Main_SideTrimmerPage.Name = "Tab_Main_SideTrimmerPage";
            this.Tab_Main_SideTrimmerPage.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Main_SideTrimmerPage.Size = new System.Drawing.Size(1902, 799);
            this.Tab_Main_SideTrimmerPage.TabIndex = 2;
            this.Tab_Main_SideTrimmerPage.Text = "裁边机参数";
            this.Tab_Main_SideTrimmerPage.UseVisualStyleBackColor = true;
            // 
            // Pnl_Trimmer
            // 
            this.Pnl_Trimmer.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Trimmer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Trimmer.Controls.Add(this.Dgv_SideTrimmer_Edit);
            this.Pnl_Trimmer.Location = new System.Drawing.Point(0, 519);
            this.Pnl_Trimmer.Name = "Pnl_Trimmer";
            this.Pnl_Trimmer.Size = new System.Drawing.Size(1689, 187);
            this.Pnl_Trimmer.TabIndex = 1781;
            this.Pnl_Trimmer.Visible = false;
            // 
            // Dgv_SideTrimmer_Edit
            // 
            this.Dgv_SideTrimmer_Edit.AllowUserToAddRows = false;
            this.Dgv_SideTrimmer_Edit.AllowUserToDeleteRows = false;
            this.Dgv_SideTrimmer_Edit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dgv_SideTrimmer_Edit.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Dgv_SideTrimmer_Edit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_SideTrimmer_Edit.Location = new System.Drawing.Point(13, 17);
            this.Dgv_SideTrimmer_Edit.Name = "Dgv_SideTrimmer_Edit";
            this.Dgv_SideTrimmer_Edit.RowHeadersVisible = false;
            this.Dgv_SideTrimmer_Edit.RowTemplate.Height = 24;
            this.Dgv_SideTrimmer_Edit.Size = new System.Drawing.Size(1657, 153);
            this.Dgv_SideTrimmer_Edit.TabIndex = 0;
            // 
            // Pnl_Trimmer_Bottom
            // 
            this.Pnl_Trimmer_Bottom.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Trimmer_Bottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Trimmer_Bottom.Controls.Add(this.Btn_Trimmer_Cancel);
            this.Pnl_Trimmer_Bottom.Controls.Add(this.Btn_Trimmer_Insert);
            this.Pnl_Trimmer_Bottom.Controls.Add(this.Btn_Trimmer_Save);
            this.Pnl_Trimmer_Bottom.Controls.Add(this.Btn_Trimmer_Delete);
            this.Pnl_Trimmer_Bottom.Controls.Add(this.Btn_Trimmer_Update);
            this.Pnl_Trimmer_Bottom.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Pnl_Trimmer_Bottom.Location = new System.Drawing.Point(0, 713);
            this.Pnl_Trimmer_Bottom.Name = "Pnl_Trimmer_Bottom";
            this.Pnl_Trimmer_Bottom.Size = new System.Drawing.Size(1902, 84);
            this.Pnl_Trimmer_Bottom.TabIndex = 1780;
            // 
            // Btn_Trimmer_Cancel
            // 
            this.Btn_Trimmer_Cancel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Trimmer_Cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Trimmer_Cancel.Location = new System.Drawing.Point(700, 11);
            this.Btn_Trimmer_Cancel.Name = "Btn_Trimmer_Cancel";
            this.Btn_Trimmer_Cancel.Size = new System.Drawing.Size(150, 60);
            this.Btn_Trimmer_Cancel.TabIndex = 1279;
            this.Btn_Trimmer_Cancel.Text = "取消";
            this.Btn_Trimmer_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Trimmer_Cancel.Visible = false;
            this.Btn_Trimmer_Cancel.Click += new System.EventHandler(this.Btn_Trimmer_Cancel_Click);
            // 
            // Btn_Trimmer_Insert
            // 
            this.Btn_Trimmer_Insert.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Trimmer_Insert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Trimmer_Insert.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Trimmer_Insert.ForeColor = System.Drawing.Color.Black;
            this.Btn_Trimmer_Insert.Location = new System.Drawing.Point(20, 11);
            this.Btn_Trimmer_Insert.Name = "Btn_Trimmer_Insert";
            this.Btn_Trimmer_Insert.Size = new System.Drawing.Size(150, 60);
            this.Btn_Trimmer_Insert.TabIndex = 1315;
            this.Btn_Trimmer_Insert.Text = "新增";
            this.Btn_Trimmer_Insert.UseVisualStyleBackColor = false;
            this.Btn_Trimmer_Insert.Click += new System.EventHandler(this.Btn_Trimmer_Insert_Click);
            // 
            // Btn_Trimmer_Save
            // 
            this.Btn_Trimmer_Save.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Trimmer_Save.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Trimmer_Save.Location = new System.Drawing.Point(530, 11);
            this.Btn_Trimmer_Save.Name = "Btn_Trimmer_Save";
            this.Btn_Trimmer_Save.Size = new System.Drawing.Size(150, 60);
            this.Btn_Trimmer_Save.TabIndex = 1278;
            this.Btn_Trimmer_Save.Text = "确认";
            this.Btn_Trimmer_Save.UseVisualStyleBackColor = false;
            this.Btn_Trimmer_Save.Visible = false;
            this.Btn_Trimmer_Save.Click += new System.EventHandler(this.Btn_Trimmer_Save_Click);
            // 
            // Btn_Trimmer_Delete
            // 
            this.Btn_Trimmer_Delete.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Trimmer_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Trimmer_Delete.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Trimmer_Delete.ForeColor = System.Drawing.Color.Black;
            this.Btn_Trimmer_Delete.Location = new System.Drawing.Point(360, 11);
            this.Btn_Trimmer_Delete.Name = "Btn_Trimmer_Delete";
            this.Btn_Trimmer_Delete.Size = new System.Drawing.Size(150, 60);
            this.Btn_Trimmer_Delete.TabIndex = 1314;
            this.Btn_Trimmer_Delete.Text = "删除";
            this.Btn_Trimmer_Delete.UseVisualStyleBackColor = false;
            this.Btn_Trimmer_Delete.Click += new System.EventHandler(this.Btn_Trimmer_Delete_Click);
            // 
            // Btn_Trimmer_Update
            // 
            this.Btn_Trimmer_Update.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Btn_Trimmer_Update.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Trimmer_Update.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Trimmer_Update.ForeColor = System.Drawing.Color.Black;
            this.Btn_Trimmer_Update.Location = new System.Drawing.Point(190, 11);
            this.Btn_Trimmer_Update.Name = "Btn_Trimmer_Update";
            this.Btn_Trimmer_Update.Size = new System.Drawing.Size(150, 60);
            this.Btn_Trimmer_Update.TabIndex = 1314;
            this.Btn_Trimmer_Update.Text = "修改";
            this.Btn_Trimmer_Update.UseVisualStyleBackColor = false;
            this.Btn_Trimmer_Update.Click += new System.EventHandler(this.Btn_Trimmer_Update_Click);
            // 
            // Dgv_SideTrimmer
            // 
            this.Dgv_SideTrimmer.AllowUserToAddRows = false;
            this.Dgv_SideTrimmer.AllowUserToDeleteRows = false;
            this.Dgv_SideTrimmer.AllowUserToResizeColumns = false;
            this.Dgv_SideTrimmer.AllowUserToResizeRows = false;
            this.Dgv_SideTrimmer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Dgv_SideTrimmer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_SideTrimmer.Location = new System.Drawing.Point(0, 0);
            this.Dgv_SideTrimmer.MultiSelect = false;
            this.Dgv_SideTrimmer.Name = "Dgv_SideTrimmer";
            this.Dgv_SideTrimmer.ReadOnly = true;
            this.Dgv_SideTrimmer.RowHeadersVisible = false;
            this.Dgv_SideTrimmer.RowTemplate.Height = 24;
            this.Dgv_SideTrimmer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgv_SideTrimmer.Size = new System.Drawing.Size(1902, 708);
            this.Dgv_SideTrimmer.TabIndex = 361;
            // 
            // Pnl_Top
            // 
            this.Pnl_Top.BackColor = System.Drawing.Color.GreenYellow;
            this.Pnl_Top.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Top.Controls.Add(this.Btn_Reload);
            this.Pnl_Top.Controls.Add(this.Btn_Query);
            this.Pnl_Top.Controls.Add(this.Chk_SteelGrade);
            this.Pnl_Top.Controls.Add(this.Cob_SteelGrade);
            this.Pnl_Top.Controls.Add(this.Chk_Thickness);
            this.Pnl_Top.Controls.Add(this.Txt_Thickness);
            this.Pnl_Top.Location = new System.Drawing.Point(5, 50);
            this.Pnl_Top.Name = "Pnl_Top";
            this.Pnl_Top.Size = new System.Drawing.Size(1910, 84);
            this.Pnl_Top.TabIndex = 0;
            // 
            // frm_4_3_ProductionParameters
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1920, 982);
            this.Controls.Add(this.Pnl_Top);
            this.Controls.Add(this.Tab_MainControl);
            this.Controls.Add(this.Lbl_MainTitle);
            this.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_4_3_ProductionParameters";
            this.Text = "frm_4_1_ProductionParameters";
            this.Load += new System.EventHandler(this.frm_4_3_ProductionParameters_Load);
            this.Tab_MainControl.ResumeLayout(false);
            this.Tab_Main_TensionPage.ResumeLayout(false);
            this.Pnl_Tension.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Tension_Edit)).EndInit();
            this.Pnl_Tension_Bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Tension)).EndInit();
            this.Tab_Main_FlattenerPage.ResumeLayout(false);
            this.Pnl_Flattener.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Flattener_Edit)).EndInit();
            this.Pnl_Flattener_Bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_Flattener)).EndInit();
            this.Tab_Main_SideTrimmerPage.ResumeLayout(false);
            this.Pnl_Trimmer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_SideTrimmer_Edit)).EndInit();
            this.Pnl_Trimmer_Bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_SideTrimmer)).EndInit();
            this.Pnl_Top.ResumeLayout(false);
            this.Pnl_Top.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label Lbl_MainTitle;
        internal System.Windows.Forms.TextBox Txt_Thickness;
        internal System.Windows.Forms.ComboBox Cob_SteelGrade;
        internal System.Windows.Forms.Button Btn_Query;
        internal System.Windows.Forms.Button Btn_Tension_Insert;
        internal System.Windows.Forms.Button Btn_Tension_Update;
        internal System.Windows.Forms.Button Btn_Tension_Delete;
        internal System.Windows.Forms.CheckBox Chk_Thickness;
        internal System.Windows.Forms.CheckBox Chk_SteelGrade;
        internal System.Windows.Forms.Button Btn_Reload;
        private System.Windows.Forms.TabControl Tab_MainControl;
        private System.Windows.Forms.TabPage Tab_Main_TensionPage;
        private System.Windows.Forms.Button Btn_Tension_Cancel;
        private System.Windows.Forms.Button Btn_Tension_Save;
        private System.Windows.Forms.DataGridView Dgv_Tension;
        private System.Windows.Forms.TabPage Tab_Main_FlattenerPage;
        private System.Windows.Forms.TabPage Tab_Main_SideTrimmerPage;
        private System.Windows.Forms.Button Btn_Flattener_Cancel;
        private System.Windows.Forms.Button Btn_Flattener_Save;
        internal System.Windows.Forms.Button Btn_Flattener_Delete;
        private System.Windows.Forms.DataGridView Dgv_Flattener;
        internal System.Windows.Forms.Button Btn_Flattener_Insert;
        internal System.Windows.Forms.Button Btn_Flattener_Update;
        private System.Windows.Forms.Button Btn_Trimmer_Cancel;
        private System.Windows.Forms.Button Btn_Trimmer_Save;
        internal System.Windows.Forms.Button Btn_Trimmer_Delete;
        private System.Windows.Forms.DataGridView Dgv_SideTrimmer;
        internal System.Windows.Forms.Button Btn_Trimmer_Insert;
        internal System.Windows.Forms.Button Btn_Trimmer_Update;
        private System.Windows.Forms.Panel Pnl_Top;
        private System.Windows.Forms.Panel Pnl_Tension_Bottom;
        private System.Windows.Forms.Panel Pnl_Flattener_Bottom;
        private System.Windows.Forms.Panel Pnl_Trimmer_Bottom;
        private System.Windows.Forms.Panel Pnl_Tension;
        internal System.Windows.Forms.DataGridView Dgv_Tension_Edit;
        private System.Windows.Forms.Panel Pnl_Flattener;
        internal System.Windows.Forms.DataGridView Dgv_Flattener_Edit;
        private System.Windows.Forms.Panel Pnl_Trimmer;
        internal System.Windows.Forms.DataGridView Dgv_SideTrimmer_Edit;
    }
}