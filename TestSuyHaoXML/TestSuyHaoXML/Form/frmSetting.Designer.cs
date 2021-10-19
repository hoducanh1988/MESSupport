namespace TestSuyHaoXML
{
    partial class frmSetting
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxAttenuatorTimes = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtConnectorAttenuator = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtVisaAddress = new System.Windows.Forms.TextBox();
            this.cbxReceivePort = new System.Windows.Forms.ComboBox();
            this.cbxTransmissionPort = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTransmissionPower = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxInstrumentStyle = new System.Windows.Forms.ComboBox();
            this.lblSaveStatus = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbNote = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxAttenuatorTimes);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtConnectorAttenuator);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtVisaAddress);
            this.groupBox1.Controls.Add(this.cbxReceivePort);
            this.groupBox1.Controls.Add(this.cbxTransmissionPort);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTransmissionPower);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxInstrumentStyle);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(447, 352);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Máy Đo";
            // 
            // cbxAttenuatorTimes
            // 
            this.cbxAttenuatorTimes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAttenuatorTimes.FormattingEnabled = true;
            this.cbxAttenuatorTimes.Location = new System.Drawing.Point(231, 310);
            this.cbxAttenuatorTimes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxAttenuatorTimes.Name = "cbxAttenuatorTimes";
            this.cbxAttenuatorTimes.Size = new System.Drawing.Size(206, 28);
            this.cbxAttenuatorTimes.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 317);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(208, 21);
            this.label7.TabIndex = 17;
            this.label7.Text = "Số Lần Tính Suy Hao TB:";
            // 
            // txtConnectorAttenuator
            // 
            this.txtConnectorAttenuator.Location = new System.Drawing.Point(231, 270);
            this.txtConnectorAttenuator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtConnectorAttenuator.Name = "txtConnectorAttenuator";
            this.txtConnectorAttenuator.Size = new System.Drawing.Size(206, 28);
            this.txtConnectorAttenuator.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 277);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(216, 21);
            this.label6.TabIndex = 15;
            this.label6.Text = "Suy Hao Connector (dBm):";
            // 
            // txtVisaAddress
            // 
            this.txtVisaAddress.Location = new System.Drawing.Point(144, 89);
            this.txtVisaAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtVisaAddress.Name = "txtVisaAddress";
            this.txtVisaAddress.Size = new System.Drawing.Size(293, 28);
            this.txtVisaAddress.TabIndex = 14;
            // 
            // cbxReceivePort
            // 
            this.cbxReceivePort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxReceivePort.FormattingEnabled = true;
            this.cbxReceivePort.Location = new System.Drawing.Point(144, 229);
            this.cbxReceivePort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxReceivePort.Name = "cbxReceivePort";
            this.cbxReceivePort.Size = new System.Drawing.Size(293, 28);
            this.cbxReceivePort.TabIndex = 12;
            // 
            // cbxTransmissionPort
            // 
            this.cbxTransmissionPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTransmissionPort.FormattingEnabled = true;
            this.cbxTransmissionPort.Location = new System.Drawing.Point(144, 182);
            this.cbxTransmissionPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxTransmissionPort.Name = "cbxTransmissionPort";
            this.cbxTransmissionPort.Size = new System.Drawing.Size(293, 28);
            this.cbxTransmissionPort.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "Port Nhận:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Port Phát:";
            // 
            // txtTransmissionPower
            // 
            this.txtTransmissionPower.Location = new System.Drawing.Point(144, 130);
            this.txtTransmissionPower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTransmissionPower.Name = "txtTransmissionPower";
            this.txtTransmissionPower.Size = new System.Drawing.Size(293, 28);
            this.txtTransmissionPower.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 42);
            this.label3.TabIndex = 4;
            this.label3.Text = "Công Suất Phát:\r\n       (dBm)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Địa Chỉ VISA:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Loại Máy Đo:";
            // 
            // cbxInstrumentStyle
            // 
            this.cbxInstrumentStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxInstrumentStyle.FormattingEnabled = true;
            this.cbxInstrumentStyle.Location = new System.Drawing.Point(144, 39);
            this.cbxInstrumentStyle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxInstrumentStyle.Name = "cbxInstrumentStyle";
            this.cbxInstrumentStyle.Size = new System.Drawing.Size(293, 28);
            this.cbxInstrumentStyle.TabIndex = 0;
            this.cbxInstrumentStyle.SelectedIndexChanged += new System.EventHandler(this.CbxInstrumentStyle_SelectedIndexChanged);
            // 
            // lblSaveStatus
            // 
            this.lblSaveStatus.AutoSize = true;
            this.lblSaveStatus.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaveStatus.ForeColor = System.Drawing.Color.Red;
            this.lblSaveStatus.Location = new System.Drawing.Point(10, 27);
            this.lblSaveStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSaveStatus.Name = "lblSaveStatus";
            this.lblSaveStatus.Size = new System.Drawing.Size(18, 19);
            this.lblSaveStatus.TabIndex = 13;
            this.lblSaveStatus.Text = "*";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(540, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(205, 42);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Lưu Thông Số";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(756, 416);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbNote);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(456, 2);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(297, 352);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ghi Chú";
            this.groupBox2.UseCompatibleTextRendering = true;
            // 
            // rtbNote
            // 
            this.rtbNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbNote.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.rtbNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbNote.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rtbNote.Location = new System.Drawing.Point(7, 26);
            this.rtbNote.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtbNote.Name = "rtbNote";
            this.rtbNote.ReadOnly = true;
            this.rtbNote.Size = new System.Drawing.Size(285, 320);
            this.rtbNote.TabIndex = 0;
            this.rtbNote.Text = "- Địa chỉ Visa có dạng:\nTCPIP0::192.168.88.2::inst0::INSTR";
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.lblSaveStatus);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 358);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 56);
            this.panel1.TabIndex = 2;
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(756, 416);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(774, 463);
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu Hình Các Thông Số Của Phần Mềm";
            this.Load += new System.EventHandler(this.FrmSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxInstrumentStyle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTransmissionPower;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbxReceivePort;
        private System.Windows.Forms.ComboBox cbxTransmissionPort;
        private System.Windows.Forms.Label lblSaveStatus;
        private System.Windows.Forms.RichTextBox rtbNote;
        private System.Windows.Forms.TextBox txtVisaAddress;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtConnectorAttenuator;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxAttenuatorTimes;
        private System.Windows.Forms.Label label7;
    }
}