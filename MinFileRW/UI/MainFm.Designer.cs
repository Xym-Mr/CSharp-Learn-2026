namespace MinFileRW.UI
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
            groupBox1 = new GroupBox();
            tbTxtReadMsg = new TextBox();
            btnTxtRead = new Button();
            btnTxtWrite = new Button();
            tbTxtWriteMsg = new TextBox();
            groupBox2 = new GroupBox();
            tbCSVReadMsg = new TextBox();
            btnCSVRead = new Button();
            btnCSVWrite = new Button();
            tbCSVWriteMsg = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tbTxtReadMsg);
            groupBox1.Controls.Add(btnTxtRead);
            groupBox1.Controls.Add(btnTxtWrite);
            groupBox1.Controls.Add(tbTxtWriteMsg);
            groupBox1.Location = new Point(17, 13);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(366, 197);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Text_FileRW";
            // 
            // tbTxtReadMsg
            // 
            tbTxtReadMsg.Location = new Point(87, 51);
            tbTxtReadMsg.Multiline = true;
            tbTxtReadMsg.Name = "tbTxtReadMsg";
            tbTxtReadMsg.Size = new Size(273, 140);
            tbTxtReadMsg.TabIndex = 3;
            // 
            // btnTxtRead
            // 
            btnTxtRead.Location = new Point(6, 51);
            btnTxtRead.Name = "btnTxtRead";
            btnTxtRead.Size = new Size(75, 23);
            btnTxtRead.TabIndex = 2;
            btnTxtRead.Text = "Read";
            btnTxtRead.UseVisualStyleBackColor = true;
            btnTxtRead.Click += btnTxtRead_Click;
            // 
            // btnTxtWrite
            // 
            btnTxtWrite.Location = new Point(6, 22);
            btnTxtWrite.Name = "btnTxtWrite";
            btnTxtWrite.Size = new Size(75, 23);
            btnTxtWrite.TabIndex = 1;
            btnTxtWrite.Text = "Write";
            btnTxtWrite.UseVisualStyleBackColor = true;
            btnTxtWrite.Click += btnTxtWrite_Click;
            // 
            // tbTxtWriteMsg
            // 
            tbTxtWriteMsg.Location = new Point(87, 22);
            tbTxtWriteMsg.Name = "tbTxtWriteMsg";
            tbTxtWriteMsg.Size = new Size(273, 23);
            tbTxtWriteMsg.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tbCSVReadMsg);
            groupBox2.Controls.Add(btnCSVRead);
            groupBox2.Controls.Add(btnCSVWrite);
            groupBox2.Controls.Add(tbCSVWriteMsg);
            groupBox2.Location = new Point(17, 216);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(366, 224);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "CSV_FileRW";
            // 
            // tbCSVReadMsg
            // 
            tbCSVReadMsg.Location = new Point(87, 51);
            tbCSVReadMsg.Multiline = true;
            tbCSVReadMsg.Name = "tbCSVReadMsg";
            tbCSVReadMsg.Size = new Size(273, 167);
            tbCSVReadMsg.TabIndex = 3;
            // 
            // btnCSVRead
            // 
            btnCSVRead.Location = new Point(6, 51);
            btnCSVRead.Name = "btnCSVRead";
            btnCSVRead.Size = new Size(75, 23);
            btnCSVRead.TabIndex = 2;
            btnCSVRead.Text = "Read";
            btnCSVRead.UseVisualStyleBackColor = true;
            btnCSVRead.Click += btnCSVRead_Click;
            // 
            // btnCSVWrite
            // 
            btnCSVWrite.Location = new Point(6, 22);
            btnCSVWrite.Name = "btnCSVWrite";
            btnCSVWrite.Size = new Size(75, 23);
            btnCSVWrite.TabIndex = 1;
            btnCSVWrite.Text = "Write";
            btnCSVWrite.UseVisualStyleBackColor = true;
            btnCSVWrite.Click += btnCSVWrite_Click;
            // 
            // tbCSVWriteMsg
            // 
            tbCSVWriteMsg.Location = new Point(87, 22);
            tbCSVWriteMsg.Name = "tbCSVWriteMsg";
            tbCSVWriteMsg.Size = new Size(273, 23);
            tbCSVWriteMsg.TabIndex = 0;
            // 
            // MainFm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(789, 452);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "MainFm";
            Text = "最小架构训练_FileReadAndWrite";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox tbTxtReadMsg;
        private Button btnTxtRead;
        private Button btnTxtWrite;
        private TextBox tbTxtWriteMsg;
        private GroupBox groupBox2;
        private TextBox tbCSVReadMsg;
        private Button btnCSVRead;
        private Button btnCSVWrite;
        private TextBox tbCSVWriteMsg;
    }
}