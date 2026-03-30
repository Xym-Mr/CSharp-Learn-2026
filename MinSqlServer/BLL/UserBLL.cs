using MinSqlServer.DAL;
using MinSqlServer.Model.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSqlServer.BLL
{
    /// <summary>
    /// 用户业务逻辑层--》流程控制
    /// </summary>
    internal class UserBLL
    {
        UserDAL userDAL = new UserDAL();

        ///① 业务验证、安全验证（BLL 做）判断业务规则是否合法：
        //用户名不能为空（业务）
        //密码不能为空（业务）
        //年龄必须大于 0（业务）
        //列表必须至少有 1 条（业务）

        public UserInfo UserLogin(UserInfo userInfo)
        {
            //1、所有的业务验证、权限验证、安全验证
            if (userInfo == null) throw new ArgumentNullException(nameof(userInfo), "用户数据对象为空异常");
            if (string.IsNullOrWhiteSpace(userInfo.UserName)) throw new ArgumentNullException(nameof(userInfo.UserName), "用户名不能为空");
            if (string.IsNullOrWhiteSpace(userInfo.UserPwd)) throw new ArgumentNullException(nameof(userInfo.UserPwd), "密码不能为空");

            try
            {
                //业务执行
                //业务结果解析
                return this.userDAL.Select(userInfo)?[0];
            }
            catch (Exception ex)
            {
                throw new Exception("BLL登录异常：" + ex.Message);
            }
        }
    }
}
