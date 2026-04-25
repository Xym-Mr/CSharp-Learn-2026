namespace SDLC_MinSingleDeviceDataMonitorSystem._1UI
{
    partial class MainFm
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
            gbConfigSetting = new GroupBox();
            btnMonitor = new Button();
            btnOpen = new Button();
            btnSave = new Button();
            tbQuantity = new TextBox();
            label8 = new Label();
            tbStartAddress = new TextBox();
            label7 = new Label();
            tbSlaveID = new TextBox();
            label6 = new Label();
            cbStopBits = new ComboBox();
            label5 = new Label();
            cbDataBits = new ComboBox();
            label4 = new Label();
            cbParity = new ComboBox();
            label3 = new Label();
            cbBaudRate = new ComboBox();
            label2 = new Label();
            cbPortName = new ComboBox();
            label1 = new Label();
            pnlData = new Panel();
            dgvDataMonitor = new DataGridView();
            gbConfigSetting.SuspendLayout();
            pnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDataMonitor).BeginInit();
            SuspendLayout();
            // 
            // gbConfigSetting
            // 
            gbConfigSetting.Controls.Add(btnMonitor);
            gbConfigSetting.Controls.Add(btnOpen);
            gbConfigSetting.Controls.Add(btnSave);
            gbConfigSetting.Controls.Add(tbQuantity);
            gbConfigSetting.Controls.Add(label8);
            gbConfigSetting.Controls.Add(tbStartAddress);
            gbConfigSetting.Controls.Add(label7);
            gbConfigSetting.Controls.Add(tbSlaveID);
            gbConfigSetting.Controls.Add(label6);
            gbConfigSetting.Controls.Add(cbStopBits);
            gbConfigSetting.Controls.Add(label5);
            gbConfigSetting.Controls.Add(cbDataBits);
            gbConfigSetting.Controls.Add(label4);
            gbConfigSetting.Controls.Add(cbParity);
            gbConfigSetting.Controls.Add(label3);
            gbConfigSetting.Controls.Add(cbBaudRate);
            gbConfigSetting.Controls.Add(label2);
            gbConfigSetting.Controls.Add(cbPortName);
            gbConfigSetting.Controls.Add(label1);
            gbConfigSetting.Dock = DockStyle.Top;
            gbConfigSetting.Location = new Point(0, 0);
            gbConfigSetting.Name = "gbConfigSetting";
            gbConfigSetting.Size = new Size(774, 94);
            gbConfigSetting.TabIndex = 0;
            gbConfigSetting.TabStop = false;
            gbConfigSetting.Text = "ConfigSetting";
            // 
            // btnMonitor
            // 
            btnMonitor.Location = new Point(689, 56);
            btnMonitor.Name = "btnMonitor";
            btnMonitor.Size = new Size(78, 31);
            btnMonitor.TabIndex = 18;
            btnMonitor.Text = "Monitor";
            btnMonitor.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(605, 56);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(78, 31);
            btnOpen.TabIndex = 17;
            btnOpen.Text = "Open";
            btnOpen.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(521, 56);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(78, 31);
            btnSave.TabIndex = 16;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // tbQuantity
            // 
            tbQuantity.Location = new Point(407, 60);
            tbQuantity.Name = "tbQuantity";
            tbQuantity.Size = new Size(78, 23);
            tbQuantity.TabIndex = 15;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(342, 63);
            label8.Name = "label8";
            label8.Size = new Size(59, 17);
            label8.TabIndex = 14;
            label8.Text = "Quantity:";
            // 
            // tbStartAddress
            // 
            tbStartAddress.Location = new Point(258, 60);
            tbStartAddress.Name = "tbStartAddress";
            tbStartAddress.Size = new Size(78, 23);
            tbStartAddress.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(166, 63);
            label7.Name = "label7";
            label7.Size = new Size(86, 17);
            label7.TabIndex = 12;
            label7.Text = "StartAddress:";
            // 
            // tbSlaveID
            // 
            tbSlaveID.Location = new Point(82, 60);
            tbSlaveID.Name = "tbSlaveID";
            tbSlaveID.Size = new Size(78, 23);
            tbSlaveID.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(22, 63);
            label6.Name = "label6";
            label6.Size = new Size(54, 17);
            label6.TabIndex = 10;
            label6.Text = "SlaveID:";
            // 
            // cbStopBits
            // 
            cbStopBits.FormattingEnabled = true;
            cbStopBits.Location = new Point(670, 21);
            cbStopBits.Name = "cbStopBits";
            cbStopBits.Size = new Size(78, 25);
            cbStopBits.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(605, 24);
            label5.Name = "label5";
            label5.Size = new Size(59, 17);
            label5.TabIndex = 8;
            label5.Text = "StopBits:";
            // 
            // cbDataBits
            // 
            cbDataBits.FormattingEnabled = true;
            cbDataBits.Location = new Point(521, 21);
            cbDataBits.Name = "cbDataBits";
            cbDataBits.Size = new Size(78, 25);
            cbDataBits.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(456, 24);
            label4.Name = "label4";
            label4.Size = new Size(59, 17);
            label4.TabIndex = 6;
            label4.Text = "DataBits:";
            // 
            // cbParity
            // 
            cbParity.FormattingEnabled = true;
            cbParity.Location = new Point(372, 21);
            cbParity.Name = "cbParity";
            cbParity.Size = new Size(78, 25);
            cbParity.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(323, 24);
            label3.Name = "label3";
            label3.Size = new Size(43, 17);
            label3.TabIndex = 4;
            label3.Text = "Parity:";
            // 
            // cbBaudRate
            // 
            cbBaudRate.FormattingEnabled = true;
            cbBaudRate.Location = new Point(239, 21);
            cbBaudRate.Name = "cbBaudRate";
            cbBaudRate.Size = new Size(78, 25);
            cbBaudRate.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(166, 24);
            label2.Name = "label2";
            label2.Size = new Size(67, 17);
            label2.TabIndex = 2;
            label2.Text = "BaudRate:";
            // 
            // cbPortName
            // 
            cbPortName.FormattingEnabled = true;
            cbPortName.Location = new Point(82, 21);
            cbPortName.Name = "cbPortName";
            cbPortName.Size = new Size(78, 25);
            cbPortName.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 24);
            label1.Name = "label1";
            label1.Size = new Size(70, 17);
            label1.TabIndex = 0;
            label1.Text = "PortName:";
            // 
            // pnlData
            // 
            pnlData.Controls.Add(dgvDataMonitor);
            pnlData.Dock = DockStyle.Fill;
            pnlData.Location = new Point(0, 94);
            pnlData.Name = "pnlData";
            pnlData.Size = new Size(774, 367);
            pnlData.TabIndex = 1;
            // 
            // dgvDataMonitor
            // 
            dgvDataMonitor.AllowUserToAddRows = false;
            dgvDataMonitor.AllowUserToDeleteRows = false;
            dgvDataMonitor.BackgroundColor = Color.White;
            dgvDataMonitor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDataMonitor.Dock = DockStyle.Fill;
            dgvDataMonitor.Location = new Point(0, 0);
            dgvDataMonitor.Name = "dgvDataMonitor";
            dgvDataMonitor.ReadOnly = true;
            dgvDataMonitor.Size = new Size(774, 367);
            dgvDataMonitor.TabIndex = 0;
            // 
            // MainFm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(774, 461);
            Controls.Add(pnlData);
            Controls.Add(gbConfigSetting);
            MaximumSize = new Size(790, 500);
            MinimumSize = new Size(790, 500);
            Name = "MainFm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SingleDeviceDataMonitorSystem";
            FormClosing += MainFm_FormClosing;
            gbConfigSetting.ResumeLayout(false);
            gbConfigSetting.PerformLayout();
            pnlData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDataMonitor).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbConfigSetting;
        private Panel pnlData;
        private Label label1;
        private ComboBox cbDataBits;
        private Label label4;
        private ComboBox cbParity;
        private Label label3;
        private ComboBox cbBaudRate;
        private Label label2;
        private ComboBox cbPortName;
        private ComboBox cbStopBits;
        private Label label5;
        private TextBox tbQuantity;
        private Label label8;
        private TextBox tbStartAddress;
        private Label label7;
        private TextBox tbSlaveID;
        private Label label6;
        private Button btnOpen;
        private Button btnSave;
        private DataGridView dgvDataMonitor;
        private Button btnMonitor;
    }
}