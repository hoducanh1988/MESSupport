namespace TestSuyHaoXML
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnHienthi = new System.Windows.Forms.Button();
            this.cbDay = new System.Windows.Forms.ComboBox();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.cbIP = new System.Windows.Forms.ComboBox();
            this.txtPow = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.aaaa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHienthi
            // 
            this.btnHienthi.BackColor = System.Drawing.Color.White;
            this.btnHienthi.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHienthi.Location = new System.Drawing.Point(16, 60);
            this.btnHienthi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHienthi.Name = "btnHienthi";
            this.btnHienthi.Size = new System.Drawing.Size(131, 43);
            this.btnHienthi.TabIndex = 0;
            this.btnHienthi.Text = "Start";
            this.btnHienthi.UseVisualStyleBackColor = false;
            this.btnHienthi.Click += new System.EventHandler(this.btnHienthi_Click);
            // 
            // cbDay
            // 
            this.cbDay.FormattingEnabled = true;
            this.cbDay.Location = new System.Drawing.Point(88, 14);
            this.cbDay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbDay.Name = "cbDay";
            this.cbDay.Size = new System.Drawing.Size(160, 24);
            this.cbDay.TabIndex = 2;
            // 
            // rtbOutput
            // 
            this.rtbOutput.BackColor = System.Drawing.Color.Blue;
            this.rtbOutput.Location = new System.Drawing.Point(16, 138);
            this.rtbOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(384, 365);
            this.rtbOutput.TabIndex = 3;
            this.rtbOutput.Text = "";
            // 
            // cbIP
            // 
            this.cbIP.FormattingEnabled = true;
            this.cbIP.Location = new System.Drawing.Point(337, 14);
            this.cbIP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbIP.Name = "cbIP";
            this.cbIP.Size = new System.Drawing.Size(241, 24);
            this.cbIP.TabIndex = 4;
            // 
            // txtPow
            // 
            this.txtPow.Location = new System.Drawing.Point(696, 14);
            this.txtPow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPow.Name = "txtPow";
            this.txtPow.Size = new System.Drawing.Size(132, 22);
            this.txtPow.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cord";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(268, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 26);
            this.label2.TabIndex = 7;
            this.label2.Text = "IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(604, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 26);
            this.label3.TabIndex = 8;
            this.label3.Text = "Power";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(177, 60);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 43);
            this.button1.TabIndex = 9;
            this.button1.Text = "Display";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(352, 60);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 43);
            this.button2.TabIndex = 10;
            this.button2.Text = "Open File";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(491, 71);
            this.txtLink.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLink.Multiline = true;
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(405, 26);
            this.txtLink.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(580, 102);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 35);
            this.label4.TabIndex = 12;
            this.label4.Text = "NONE";
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aaaa,
            this.Column2,
            this.Column1});
            this.dgv.Location = new System.Drawing.Point(439, 138);
            this.dgv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 51;
            this.dgv.Size = new System.Drawing.Size(459, 366);
            this.dgv.TabIndex = 14;
            // 
            // aaaa
            // 
            this.aaaa.HeaderText = "Frequency";
            this.aaaa.MinimumWidth = 6;
            this.aaaa.Name = "aaaa";
            this.aaaa.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Attenuation";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Delta";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 518);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPow);
            this.Controls.Add(this.cbIP);
            this.Controls.Add(this.rtbOutput);
            this.Controls.Add(this.cbDay);
            this.Controls.Add(this.btnHienthi);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHienthi;
        private System.Windows.Forms.ComboBox cbDay;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.ComboBox cbIP;
        private System.Windows.Forms.TextBox txtPow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn aaaa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}

