namespace MinSqlServer.UI
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
            btnLogin = new Button();
            tbUserName = new TextBox();
            tbUserPwd = new TextBox();
            label1 = new Label();
            label2 = new Label();
            lbLoginResult = new Label();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(124, 117);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(154, 33);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "点击登录";
            btnLogin.UseVisualStyleBackColor = true;
            // 
            // tbUserName
            // 
            tbUserName.Location = new Point(124, 25);
            tbUserName.Name = "tbUserName";
            tbUserName.Size = new Size(159, 23);
            tbUserName.TabIndex = 1;
            // 
            // tbUserPwd
            // 
            tbUserPwd.Location = new Point(124, 67);
            tbUserPwd.Name = "tbUserPwd";
            tbUserPwd.Size = new Size(159, 23);
            tbUserPwd.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 28);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 3;
            label1.Text = "用户名：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 70);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 4;
            label2.Text = "密   码：";
            // 
            // lbLoginResult
            // 
            lbLoginResult.AutoSize = true;
            lbLoginResult.Location = new Point(50, 191);
            lbLoginResult.Name = "lbLoginResult";
            lbLoginResult.Size = new Size(68, 17);
            lbLoginResult.TabIndex = 5;
            lbLoginResult.Text = "登录结果：";
            // 
            // MainFm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 248);
            Controls.Add(lbLoginResult);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbUserPwd);
            Controls.Add(tbUserName);
            Controls.Add(btnLogin);
            Name = "MainFm";
            Text = "最小架构训练-SQLServer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogin;
        private TextBox tbUserName;
        private TextBox tbUserPwd;
        private Label label1;
        private Label label2;
        private Label lbLoginResult;
    }
}