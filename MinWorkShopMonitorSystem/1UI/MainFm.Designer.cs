namespace MinWorkShopMonitorSystem.UI
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
            pnlCommunication = new Panel();
            gbCommunication = new GroupBox();
            btnSetting = new Button();
            btnMonitorEnable = new Button();
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
            pnlMain = new Panel();
            gbDatas = new GroupBox();
            dgvDatas = new DataGridView();
            gbLogInfos = new GroupBox();
            lbLoginfos = new ListBox();
            pnlCommunication.SuspendLayout();
            gbCommunication.SuspendLayout();
            pnlMain.SuspendLayout();
            gbDatas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDatas).BeginInit();
            gbLogInfos.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCommunication
            // 
            pnlCommunication.Controls.Add(gbCommunication);
            pnlCommunication.Dock = DockStyle.Top;
            pnlCommunication.Location = new Point(0, 0);
            pnlCommunication.Name = "pnlCommunication";
            pnlCommunication.Size = new Size(865, 73);
            pnlCommunication.TabIndex = 0;
            // 
            // gbCommunication
            // 
            gbCommunication.Controls.Add(btnSetting);
            gbCommunication.Controls.Add(btnMonitorEnable);
            gbCommunication.Controls.Add(cbStopBits);
            gbCommunication.Controls.Add(label5);
            gbCommunication.Controls.Add(cbDataBits);
            gbCommunication.Controls.Add(label4);
            gbCommunication.Controls.Add(cbParity);
            gbCommunication.Controls.Add(label3);
            gbCommunication.Controls.Add(cbBaudRate);
            gbCommunication.Controls.Add(label2);
            gbCommunication.Controls.Add(cbPortName);
            gbCommunication.Controls.Add(label1);
            gbCommunication.Dock = DockStyle.Fill;
            gbCommunication.Location = new Point(0, 0);
            gbCommunication.Name = "gbCommunication";
            gbCommunication.Size = new Size(865, 73);
            gbCommunication.TabIndex = 0;
            gbCommunication.TabStop = false;
            gbCommunication.Text = "ModbusRtu";
            // 
            // btnSetting
            // 
            btnSetting.Location = new Point(639, 10);
            btnSetting.Name = "btnSetting";
            btnSetting.Size = new Size(118, 30);
            btnSetting.TabIndex = 9;
            btnSetting.Text = "Setting";
            btnSetting.UseVisualStyleBackColor = true;
            btnSetting.Click += btnSetting_Click;
            // 
            // btnMonitorEnable
            // 
            btnMonitorEnable.Location = new Point(639, 40);
            btnMonitorEnable.Name = "btnMonitorEnable";
            btnMonitorEnable.Size = new Size(118, 30);
            btnMonitorEnable.TabIndex = 5;
            btnMonitorEnable.Text = "MonitoerEnable";
            btnMonitorEnable.UseVisualStyleBackColor = true;
            btnMonitorEnable.Click += btnMonitorEnable_Click;
            // 
            // cbStopBits
            // 
            cbStopBits.FormattingEnabled = true;
            cbStopBits.Location = new Point(512, 39);
            cbStopBits.Name = "cbStopBits";
            cbStopBits.Size = new Size(121, 25);
            cbStopBits.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(511, 19);
            label5.Name = "label5";
            label5.Size = new Size(52, 17);
            label5.TabIndex = 8;
            label5.Text = "StopBis";
            // 
            // cbDataBits
            // 
            cbDataBits.FormattingEnabled = true;
            cbDataBits.Location = new Point(387, 39);
            cbDataBits.Name = "cbDataBits";
            cbDataBits.Size = new Size(121, 25);
            cbDataBits.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(387, 19);
            label4.Name = "label4";
            label4.Size = new Size(56, 17);
            label4.TabIndex = 6;
            label4.Text = "DataBits";
            // 
            // cbParity
            // 
            cbParity.FormattingEnabled = true;
            cbParity.Location = new Point(262, 39);
            cbParity.Name = "cbParity";
            cbParity.Size = new Size(121, 25);
            cbParity.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(279, 19);
            label3.Name = "label3";
            label3.Size = new Size(40, 17);
            label3.TabIndex = 4;
            label3.Text = "Parity";
            // 
            // cbBaudRate
            // 
            cbBaudRate.FormattingEnabled = true;
            cbBaudRate.Location = new Point(137, 39);
            cbBaudRate.Name = "cbBaudRate";
            cbBaudRate.Size = new Size(121, 25);
            cbBaudRate.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(147, 19);
            label2.Name = "label2";
            label2.Size = new Size(64, 17);
            label2.TabIndex = 2;
            label2.Text = "BaudRate";
            // 
            // cbPortName
            // 
            cbPortName.FormattingEnabled = true;
            cbPortName.Location = new Point(12, 39);
            cbPortName.Name = "cbPortName";
            cbPortName.Size = new Size(121, 25);
            cbPortName.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(67, 17);
            label1.TabIndex = 0;
            label1.Text = "PortName";
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(gbDatas);
            pnlMain.Controls.Add(gbLogInfos);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 73);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(865, 369);
            pnlMain.TabIndex = 1;
            // 
            // gbDatas
            // 
            gbDatas.Controls.Add(dgvDatas);
            gbDatas.Dock = DockStyle.Fill;
            gbDatas.Location = new Point(200, 0);
            gbDatas.Name = "gbDatas";
            gbDatas.Size = new Size(665, 369);
            gbDatas.TabIndex = 1;
            gbDatas.TabStop = false;
            gbDatas.Text = "Datas";
            // 
            // dgvDatas
            // 
            dgvDatas.AllowUserToAddRows = false;
            dgvDatas.AllowUserToDeleteRows = false;
            dgvDatas.BackgroundColor = Color.White;
            dgvDatas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDatas.Dock = DockStyle.Fill;
            dgvDatas.Location = new Point(3, 19);
            dgvDatas.Name = "dgvDatas";
            dgvDatas.ReadOnly = true;
            dgvDatas.Size = new Size(659, 347);
            dgvDatas.TabIndex = 0;
            // 
            // gbLogInfos
            // 
            gbLogInfos.Controls.Add(lbLoginfos);
            gbLogInfos.Dock = DockStyle.Left;
            gbLogInfos.Location = new Point(0, 0);
            gbLogInfos.Name = "gbLogInfos";
            gbLogInfos.Size = new Size(200, 369);
            gbLogInfos.TabIndex = 0;
            gbLogInfos.TabStop = false;
            gbLogInfos.Text = "RunLogInfos";
            // 
            // lbLoginfos
            // 
            lbLoginfos.Dock = DockStyle.Fill;
            lbLoginfos.FormattingEnabled = true;
            lbLoginfos.ItemHeight = 17;
            lbLoginfos.Location = new Point(3, 19);
            lbLoginfos.Name = "lbLoginfos";
            lbLoginfos.Size = new Size(194, 347);
            lbLoginfos.TabIndex = 10;
            // 
            // MainFm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(865, 442);
            Controls.Add(pnlMain);
            Controls.Add(pnlCommunication);
            Name = "MainFm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "车间数据采集系统";
            pnlCommunication.ResumeLayout(false);
            gbCommunication.ResumeLayout(false);
            gbCommunication.PerformLayout();
            pnlMain.ResumeLayout(false);
            gbDatas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDatas).EndInit();
            gbLogInfos.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCommunication;
        private GroupBox gbCommunication;
        private Label label1;
        private ComboBox cbPortName;
        private ComboBox cbStopBits;
        private Label label5;
        private ComboBox cbDataBits;
        private Label label4;
        private ComboBox cbParity;
        private Label label3;
        private ComboBox cbBaudRate;
        private Label label2;
        private Button btnMonitorEnable;
        private Panel pnlMain;
        private GroupBox gbLogInfos;
        private GroupBox gbDatas;
        private DataGridView dgvDatas;
        private Button btnSetting;
        private ListBox lbLoginfos;
    }
}