namespace DelegateCommunication.UI
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
            btnOpenSerialPort = new Button();
            gbModbusPoll = new GroupBox();
            lab40005 = new Label();
            lab40004 = new Label();
            lab40003 = new Label();
            lab40001 = new Label();
            lab40002 = new Label();
            label9 = new Label();
            label2 = new Label();
            label10 = new Label();
            label11 = new Label();
            label13 = new Label();
            label12 = new Label();
            gbModbusSlave = new GroupBox();
            tb40005 = new TextBox();
            label8 = new Label();
            tb40004 = new TextBox();
            label7 = new Label();
            tb40003 = new TextBox();
            label6 = new Label();
            tb40002 = new TextBox();
            label5 = new Label();
            tb40001 = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            labSatus = new Label();
            gbModbusPoll.SuspendLayout();
            gbModbusSlave.SuspendLayout();
            SuspendLayout();
            // 
            // btnOpenSerialPort
            // 
            btnOpenSerialPort.Location = new Point(55, 231);
            btnOpenSerialPort.Name = "btnOpenSerialPort";
            btnOpenSerialPort.Size = new Size(107, 29);
            btnOpenSerialPort.TabIndex = 0;
            btnOpenSerialPort.Text = "打开串口";
            btnOpenSerialPort.UseVisualStyleBackColor = true;
            btnOpenSerialPort.Click += btnOpenSerialPort_Click;
            // 
            // gbModbusPoll
            // 
            gbModbusPoll.Controls.Add(labSatus);
            gbModbusPoll.Controls.Add(lab40005);
            gbModbusPoll.Controls.Add(lab40004);
            gbModbusPoll.Controls.Add(lab40003);
            gbModbusPoll.Controls.Add(lab40001);
            gbModbusPoll.Controls.Add(lab40002);
            gbModbusPoll.Controls.Add(label9);
            gbModbusPoll.Controls.Add(label2);
            gbModbusPoll.Controls.Add(label10);
            gbModbusPoll.Controls.Add(btnOpenSerialPort);
            gbModbusPoll.Controls.Add(label11);
            gbModbusPoll.Controls.Add(label13);
            gbModbusPoll.Controls.Add(label12);
            gbModbusPoll.Location = new Point(21, 12);
            gbModbusPoll.Name = "gbModbusPoll";
            gbModbusPoll.Size = new Size(300, 369);
            gbModbusPoll.TabIndex = 1;
            gbModbusPoll.TabStop = false;
            gbModbusPoll.Text = "Modbus主站";
            // 
            // lab40005
            // 
            lab40005.AutoSize = true;
            lab40005.Location = new Point(55, 179);
            lab40005.Name = "lab40005";
            lab40005.Size = new Size(15, 17);
            lab40005.TabIndex = 24;
            lab40005.Text = "0";
            // 
            // lab40004
            // 
            lab40004.AutoSize = true;
            lab40004.Location = new Point(55, 146);
            lab40004.Name = "lab40004";
            lab40004.Size = new Size(15, 17);
            lab40004.TabIndex = 23;
            lab40004.Text = "0";
            // 
            // lab40003
            // 
            lab40003.AutoSize = true;
            lab40003.Location = new Point(55, 113);
            lab40003.Name = "lab40003";
            lab40003.Size = new Size(15, 17);
            lab40003.TabIndex = 22;
            lab40003.Text = "0";
            // 
            // lab40001
            // 
            lab40001.AutoSize = true;
            lab40001.Location = new Point(55, 47);
            lab40001.Name = "lab40001";
            lab40001.Size = new Size(15, 17);
            lab40001.TabIndex = 20;
            lab40001.Text = "0";
            // 
            // lab40002
            // 
            lab40002.AutoSize = true;
            lab40002.Location = new Point(55, 80);
            lab40002.Name = "lab40002";
            lab40002.Size = new Size(15, 17);
            lab40002.TabIndex = 21;
            lab40002.Text = "0";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 179);
            label9.Name = "label9";
            label9.Size = new Size(43, 17);
            label9.TabIndex = 19;
            label9.Text = "40005";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(263, 17);
            label2.TabIndex = 4;
            label2.Text = "采集Modbus从站设备4区连续5个保存寄存器值";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 146);
            label10.Name = "label10";
            label10.Size = new Size(43, 17);
            label10.TabIndex = 18;
            label10.Text = "40004";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 113);
            label11.Name = "label11";
            label11.Size = new Size(43, 17);
            label11.TabIndex = 17;
            label11.Text = "40003";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 47);
            label13.Name = "label13";
            label13.Size = new Size(43, 17);
            label13.TabIndex = 15;
            label13.Text = "40001";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 80);
            label12.Name = "label12";
            label12.Size = new Size(43, 17);
            label12.TabIndex = 16;
            label12.Text = "40002";
            // 
            // gbModbusSlave
            // 
            gbModbusSlave.Controls.Add(tb40005);
            gbModbusSlave.Controls.Add(label8);
            gbModbusSlave.Controls.Add(tb40004);
            gbModbusSlave.Controls.Add(label7);
            gbModbusSlave.Controls.Add(tb40003);
            gbModbusSlave.Controls.Add(label6);
            gbModbusSlave.Controls.Add(tb40002);
            gbModbusSlave.Controls.Add(label5);
            gbModbusSlave.Controls.Add(tb40001);
            gbModbusSlave.Controls.Add(label4);
            gbModbusSlave.Controls.Add(label3);
            gbModbusSlave.Location = new Point(398, 12);
            gbModbusSlave.Name = "gbModbusSlave";
            gbModbusSlave.Size = new Size(300, 369);
            gbModbusSlave.TabIndex = 2;
            gbModbusSlave.TabStop = false;
            gbModbusSlave.Text = "Modbus从站";
            // 
            // tb40005
            // 
            tb40005.Location = new Point(55, 179);
            tb40005.Name = "tb40005";
            tb40005.Size = new Size(100, 23);
            tb40005.TabIndex = 14;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 182);
            label8.Name = "label8";
            label8.Size = new Size(43, 17);
            label8.TabIndex = 13;
            label8.Text = "40005";
            // 
            // tb40004
            // 
            tb40004.Location = new Point(55, 146);
            tb40004.Name = "tb40004";
            tb40004.Size = new Size(100, 23);
            tb40004.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 149);
            label7.Name = "label7";
            label7.Size = new Size(43, 17);
            label7.TabIndex = 11;
            label7.Text = "40004";
            // 
            // tb40003
            // 
            tb40003.Location = new Point(55, 113);
            tb40003.Name = "tb40003";
            tb40003.Size = new Size(100, 23);
            tb40003.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 116);
            label6.Name = "label6";
            label6.Size = new Size(43, 17);
            label6.TabIndex = 9;
            label6.Text = "40003";
            // 
            // tb40002
            // 
            tb40002.Location = new Point(55, 80);
            tb40002.Name = "tb40002";
            tb40002.Size = new Size(100, 23);
            tb40002.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 83);
            label5.Name = "label5";
            label5.Size = new Size(43, 17);
            label5.TabIndex = 7;
            label5.Text = "40002";
            // 
            // tb40001
            // 
            tb40001.Location = new Point(55, 47);
            tb40001.Name = "tb40001";
            tb40001.Size = new Size(100, 23);
            tb40001.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 50);
            label4.Name = "label4";
            label4.Size = new Size(43, 17);
            label4.TabIndex = 5;
            label4.Text = "40001";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 19);
            label3.Name = "label3";
            label3.Size = new Size(197, 17);
            label3.TabIndex = 4;
            label3.Text = "从站4区1-5连续5个保持寄存器的值";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(327, 195);
            label1.Name = "label1";
            label1.Size = new Size(64, 17);
            label1.TabIndex = 3;
            label1.Text = "<96N81>";
            // 
            // labSatus
            // 
            labSatus.AutoSize = true;
            labSatus.Location = new Point(6, 302);
            labSatus.Name = "labSatus";
            labSatus.Size = new Size(43, 17);
            labSatus.TabIndex = 25;
            labSatus.Text = "00000";
            // 
            // MainFm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(705, 393);
            Controls.Add(label1);
            Controls.Add(gbModbusSlave);
            Controls.Add(gbModbusPoll);
            Name = "MainFm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "委托和通讯";
            Load += MainFm_Load;
            gbModbusPoll.ResumeLayout(false);
            gbModbusPoll.PerformLayout();
            gbModbusSlave.ResumeLayout(false);
            gbModbusSlave.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOpenSerialPort;
        private GroupBox gbModbusPoll;
        private GroupBox gbModbusSlave;
        private Label label1;
        private Label label2;
        private TextBox tb40005;
        private Label label8;
        private TextBox tb40004;
        private Label label7;
        private TextBox tb40003;
        private Label label6;
        private TextBox tb40002;
        private Label label5;
        private TextBox tb40001;
        private Label label4;
        private Label label3;
        private Label lab40005;
        private Label lab40004;
        private Label lab40003;
        private Label lab40001;
        private Label lab40002;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label13;
        private Label label12;
        private Label labSatus;
    }
}