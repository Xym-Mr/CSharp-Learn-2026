using MinSqlServer.BLL;
using MinSqlServer.Model.DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinSqlServer.UI
{
    public partial class MainFm : Form
    {
        public MainFm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += MainFm_Load;
            this.btnLogin.Click += BtnLogin_Click;
        }

        private UserBLL userBLL = new UserBLL();


        #region 输入数据验证
        ///UI验证：保证用户交互体验，非空、长度、规则.....

        ///① 业务验证、安全验证（BLL 做）判断业务规则是否合法：
        //用户名不能为空（业务）
        //密码不能为空（业务）
        //年龄必须大于 0（业务）
        //列表必须至少有 1 条（业务）

        ///② 防崩验证（DAL 必须做），不做业务验证，判断会不会导致程序崩溃：
        //参数是否为 null
        //集合是否为 null
        //model 是否为 null
        //数组是否越界
        //这些 DAL 必须做！因为 DAL 要保证自己不崩溃、不报错、不依赖上层
        #endregion

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            try
            {
                string name = this.tbUserName.Text.Trim();
                string pwd = this.tbUserPwd.Text.Trim();
                string res = "";

                //UI验证：保证用户交互体验
                if (string.IsNullOrEmpty(name))
                {
                    this.tbUserName.Focus();
                    MessageBox.Show("用户名不能为空");
                    return;
                }
                if (string.IsNullOrEmpty(pwd))
                {
                    this.tbUserPwd.Focus();
                    MessageBox.Show("密码不能为空");
                    return;
                }

                UserInfo userInfo = new UserInfo()
                {
                    UserName = name,
                    UserPwd = pwd
                };

                userInfo = this.userBLL.UserLogin(userInfo);
                if (userInfo == null)
                {
                    this.BackColor = Color.Red;
                    res = "登录失败";
                }
                else
                {
                    this.BackColor = Color.Green;
                    res = $"登录成功:用户名{userInfo.UserName},密码{userInfo.UserPwd},Id={userInfo.UserId},删除标记={userInfo.IsDeleted}";
                }
                this.lbLoginResult.Text = res;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }
        }

        private void MainFm_Load(object? sender, EventArgs e)
        {

        }
    }
}
