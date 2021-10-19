namespace TestSuyHaoXML
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbProcess = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOpenMasterFile = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gbxMasterAttenuator = new System.Windows.Forms.GroupBox();
            this.dvwMasterAttenuator = new System.Windows.Forms.DataGridView();
            this.cbxMasterWriteSelected = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbxStationAttenuator = new System.Windows.Forms.GroupBox();
            this.dvwAttenuator = new System.Windows.Forms.DataGridView();
            this.cbxWriteSelected = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMasterLink = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxAttenuatorStype = new System.Windows.Forms.ComboBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRFAttenuator = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblReceivePort = new System.Windows.Forms.Label();
            this.lblVisaAddress = new System.Windows.Forms.Label();
            this.lblTrasmissonPort = new System.Windows.Forms.Label();
            this.lblInstrument = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gbxMasterAttenuator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvwMasterAttenuator)).BeginInit();
            this.gbxStationAttenuator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvwAttenuator)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1315, 688);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tableLayoutPanel1.SetColumnSpan(this.menuStrip1, 2);
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSetting,
            this.BtnAbout,
            this.btnExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1315, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnSetting
            // 
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(70, 36);
            this.btnSetting.Text = "Setting";
            this.btnSetting.Click += new System.EventHandler(this.BtnSetting_Click);
            // 
            // BtnAbout
            // 
            this.BtnAbout.Name = "BtnAbout";
            this.BtnAbout.Size = new System.Drawing.Size(64, 36);
            this.BtnAbout.Text = "About";
            this.BtnAbout.Click += new System.EventHandler(this.BtnAbout_Click);
            // 
            // btnExit
            // 
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(47, 36);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbProcess);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 227);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(454, 459);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quá Trình Tính Suy Hao";
            // 
            // rtbProcess
            // 
            this.rtbProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbProcess.Location = new System.Drawing.Point(7, 26);
            this.rtbProcess.Margin = new System.Windows.Forms.Padding(4);
            this.rtbProcess.Name = "rtbProcess";
            this.rtbProcess.ReadOnly = true;
            this.rtbProcess.Size = new System.Drawing.Size(439, 420);
            this.rtbProcess.TabIndex = 0;
            this.rtbProcess.Text = "";
            this.rtbProcess.TextChanged += new System.EventHandler(this.RtbProcess_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnOpenMasterFile);
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Controls.Add(this.txtMasterLink);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbxAttenuatorStype);
            this.groupBox2.Controls.Add(this.lblResult);
            this.groupBox2.Controls.Add(this.txtLink);
            this.groupBox2.Controls.Add(this.btnOpenFile);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnRFAttenuator);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(463, 42);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.SetRowSpan(this.groupBox2, 2);
            this.groupBox2.Size = new System.Drawing.Size(849, 644);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thực Hiện Tính Suy Hao";
            // 
            // btnOpenMasterFile
            // 
            this.btnOpenMasterFile.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenMasterFile.Location = new System.Drawing.Point(25, 211);
            this.btnOpenMasterFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenMasterFile.Name = "btnOpenMasterFile";
            this.btnOpenMasterFile.Size = new System.Drawing.Size(248, 34);
            this.btnOpenMasterFile.TabIndex = 15;
            this.btnOpenMasterFile.Text = "Mở File Suy Hao Master";
            this.btnOpenMasterFile.UseVisualStyleBackColor = true;
            this.btnOpenMasterFile.Click += new System.EventHandler(this.BtnOpenMasterFile_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.gbxMasterAttenuator, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.gbxStationAttenuator, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(24, 250);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 384F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(813, 384);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // gbxMasterAttenuator
            // 
            this.gbxMasterAttenuator.Controls.Add(this.dvwMasterAttenuator);
            this.gbxMasterAttenuator.Controls.Add(this.cbxMasterWriteSelected);
            this.gbxMasterAttenuator.Controls.Add(this.label3);
            this.gbxMasterAttenuator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxMasterAttenuator.Location = new System.Drawing.Point(409, 3);
            this.gbxMasterAttenuator.Name = "gbxMasterAttenuator";
            this.gbxMasterAttenuator.Size = new System.Drawing.Size(401, 378);
            this.gbxMasterAttenuator.TabIndex = 2;
            this.gbxMasterAttenuator.TabStop = false;
            this.gbxMasterAttenuator.Text = "Suy Hao Master";
            // 
            // dvwMasterAttenuator
            // 
            this.dvwMasterAttenuator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dvwMasterAttenuator.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dvwMasterAttenuator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvwMasterAttenuator.Location = new System.Drawing.Point(8, 69);
            this.dvwMasterAttenuator.Margin = new System.Windows.Forms.Padding(5);
            this.dvwMasterAttenuator.Name = "dvwMasterAttenuator";
            this.dvwMasterAttenuator.ReadOnly = true;
            this.dvwMasterAttenuator.RowHeadersWidth = 4;
            this.dvwMasterAttenuator.RowTemplate.Height = 24;
            this.dvwMasterAttenuator.Size = new System.Drawing.Size(385, 301);
            this.dvwMasterAttenuator.TabIndex = 0;
            // 
            // cbxMasterWriteSelected
            // 
            this.cbxMasterWriteSelected.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMasterWriteSelected.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMasterWriteSelected.FormattingEnabled = true;
            this.cbxMasterWriteSelected.Location = new System.Drawing.Point(171, 29);
            this.cbxMasterWriteSelected.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxMasterWriteSelected.Name = "cbxMasterWriteSelected";
            this.cbxMasterWriteSelected.Size = new System.Drawing.Size(204, 27);
            this.cbxMasterWriteSelected.TabIndex = 4;
            this.cbxMasterWriteSelected.SelectedIndexChanged += new System.EventHandler(this.CbxMasterWriteSelected_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(6, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "Lựa Chọn Dây RF:";
            // 
            // gbxStationAttenuator
            // 
            this.gbxStationAttenuator.Controls.Add(this.dvwAttenuator);
            this.gbxStationAttenuator.Controls.Add(this.cbxWriteSelected);
            this.gbxStationAttenuator.Controls.Add(this.label1);
            this.gbxStationAttenuator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxStationAttenuator.Location = new System.Drawing.Point(3, 3);
            this.gbxStationAttenuator.Name = "gbxStationAttenuator";
            this.gbxStationAttenuator.Size = new System.Drawing.Size(400, 378);
            this.gbxStationAttenuator.TabIndex = 1;
            this.gbxStationAttenuator.TabStop = false;
            this.gbxStationAttenuator.Text = "Suy Hao Trạm";
            // 
            // dvwAttenuator
            // 
            this.dvwAttenuator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dvwAttenuator.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dvwAttenuator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvwAttenuator.Location = new System.Drawing.Point(8, 69);
            this.dvwAttenuator.Margin = new System.Windows.Forms.Padding(5);
            this.dvwAttenuator.Name = "dvwAttenuator";
            this.dvwAttenuator.ReadOnly = true;
            this.dvwAttenuator.RowHeadersWidth = 4;
            this.dvwAttenuator.RowTemplate.Height = 24;
            this.dvwAttenuator.Size = new System.Drawing.Size(384, 301);
            this.dvwAttenuator.TabIndex = 0;
            // 
            // cbxWriteSelected
            // 
            this.cbxWriteSelected.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxWriteSelected.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxWriteSelected.FormattingEnabled = true;
            this.cbxWriteSelected.Location = new System.Drawing.Point(171, 29);
            this.cbxWriteSelected.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxWriteSelected.Name = "cbxWriteSelected";
            this.cbxWriteSelected.Size = new System.Drawing.Size(204, 27);
            this.cbxWriteSelected.TabIndex = 4;
            this.cbxWriteSelected.SelectedIndexChanged += new System.EventHandler(this.CbxWriteSelected_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Lựa Chọn Dây RF:";
            // 
            // txtMasterLink
            // 
            this.txtMasterLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMasterLink.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMasterLink.Location = new System.Drawing.Point(279, 214);
            this.txtMasterLink.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMasterLink.Name = "txtMasterLink";
            this.txtMasterLink.ReadOnly = true;
            this.txtMasterLink.Size = new System.Drawing.Size(558, 26);
            this.txtMasterLink.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(32, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 26);
            this.label2.TabIndex = 10;
            this.label2.Text = "Kiểu Tính Suy Hao: ";
            // 
            // cbxAttenuatorStype
            // 
            this.cbxAttenuatorStype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAttenuatorStype.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAttenuatorStype.ForeColor = System.Drawing.Color.Blue;
            this.cbxAttenuatorStype.FormattingEnabled = true;
            this.cbxAttenuatorStype.Location = new System.Drawing.Point(279, 33);
            this.cbxAttenuatorStype.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxAttenuatorStype.Name = "cbxAttenuatorStype";
            this.cbxAttenuatorStype.Size = new System.Drawing.Size(231, 33);
            this.cbxAttenuatorStype.TabIndex = 9;
            this.cbxAttenuatorStype.SelectedIndexChanged += new System.EventHandler(this.CbxAttenuatorStype_SelectedIndexChanged);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Times New Roman", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.Color.Red;
            this.lblResult.Location = new System.Drawing.Point(552, 25);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(158, 55);
            this.lblResult.TabIndex = 8;
            this.lblResult.Text = "NONE";
            // 
            // txtLink
            // 
            this.txtLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLink.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLink.Location = new System.Drawing.Point(279, 177);
            this.txtLink.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLink.Name = "txtLink";
            this.txtLink.ReadOnly = true;
            this.txtLink.Size = new System.Drawing.Size(557, 26);
            this.txtLink.TabIndex = 7;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFile.Location = new System.Drawing.Point(25, 173);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(248, 34);
            this.btnOpenFile.TabIndex = 6;
            this.btnOpenFile.Text = "Mở File Suy Hao Trạm";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.BtnOpenFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(279, 84);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 76);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Lưu File";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnRFAttenuator
            // 
            this.btnRFAttenuator.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRFAttenuator.ForeColor = System.Drawing.Color.Red;
            this.btnRFAttenuator.Location = new System.Drawing.Point(25, 84);
            this.btnRFAttenuator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRFAttenuator.Name = "btnRFAttenuator";
            this.btnRFAttenuator.Size = new System.Drawing.Size(248, 76);
            this.btnRFAttenuator.TabIndex = 0;
            this.btnRFAttenuator.Text = "Tính Suy Hao ";
            this.btnRFAttenuator.UseVisualStyleBackColor = true;
            this.btnRFAttenuator.Click += new System.EventHandler(this.BtnRFAttenuator_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblReceivePort);
            this.groupBox4.Controls.Add(this.lblVisaAddress);
            this.groupBox4.Controls.Add(this.lblTrasmissonPort);
            this.groupBox4.Controls.Add(this.lblInstrument);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(3, 42);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(454, 181);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Máy Đo";
            // 
            // lblReceivePort
            // 
            this.lblReceivePort.AutoSize = true;
            this.lblReceivePort.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceivePort.ForeColor = System.Drawing.Color.Red;
            this.lblReceivePort.Location = new System.Drawing.Point(12, 140);
            this.lblReceivePort.Name = "lblReceivePort";
            this.lblReceivePort.Size = new System.Drawing.Size(209, 19);
            this.lblReceivePort.TabIndex = 4;
            this.lblReceivePort.Text = "Port nhận: ______________";
            // 
            // lblVisaAddress
            // 
            this.lblVisaAddress.AutoSize = true;
            this.lblVisaAddress.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVisaAddress.ForeColor = System.Drawing.Color.Red;
            this.lblVisaAddress.Location = new System.Drawing.Point(12, 69);
            this.lblVisaAddress.Name = "lblVisaAddress";
            this.lblVisaAddress.Size = new System.Drawing.Size(220, 19);
            this.lblVisaAddress.TabIndex = 3;
            this.lblVisaAddress.Text = "Địa Chỉ Visa: _____________";
            // 
            // lblTrasmissonPort
            // 
            this.lblTrasmissonPort.AutoSize = true;
            this.lblTrasmissonPort.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrasmissonPort.ForeColor = System.Drawing.Color.Red;
            this.lblTrasmissonPort.Location = new System.Drawing.Point(12, 101);
            this.lblTrasmissonPort.Name = "lblTrasmissonPort";
            this.lblTrasmissonPort.Size = new System.Drawing.Size(219, 19);
            this.lblTrasmissonPort.TabIndex = 2;
            this.lblTrasmissonPort.Text = "Port phát: _______________ ";
            // 
            // lblInstrument
            // 
            this.lblInstrument.AutoSize = true;
            this.lblInstrument.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrument.ForeColor = System.Drawing.Color.Red;
            this.lblInstrument.Location = new System.Drawing.Point(9, 34);
            this.lblInstrument.Name = "lblInstrument";
            this.lblInstrument.Size = new System.Drawing.Size(223, 19);
            this.lblInstrument.TabIndex = 1;
            this.lblInstrument.Text = "Máy đo: _________________";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1315, 688);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1330, 735);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.gbxMasterAttenuator.ResumeLayout(false);
            this.gbxMasterAttenuator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvwMasterAttenuator)).EndInit();
            this.gbxStationAttenuator.ResumeLayout(false);
            this.gbxStationAttenuator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvwAttenuator)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTrasmissonPort;
        private System.Windows.Forms.Label lblInstrument;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRFAttenuator;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxWriteSelected;
        private System.Windows.Forms.DataGridView dvwAttenuator;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnSetting;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.RichTextBox rtbProcess;
        private System.Windows.Forms.ToolStripMenuItem BtnAbout;
        private System.Windows.Forms.ToolStripMenuItem btnExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxAttenuatorStype;
        private System.Windows.Forms.Label lblReceivePort;
        private System.Windows.Forms.Label lblVisaAddress;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtMasterLink;
        private System.Windows.Forms.Button btnOpenMasterFile;
        private System.Windows.Forms.GroupBox gbxStationAttenuator;
        private System.Windows.Forms.GroupBox gbxMasterAttenuator;
        private System.Windows.Forms.DataGridView dvwMasterAttenuator;
        private System.Windows.Forms.ComboBox cbxMasterWriteSelected;
        private System.Windows.Forms.Label label3;
    }
}