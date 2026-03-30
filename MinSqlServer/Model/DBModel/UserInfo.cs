using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSqlServer.Model.DBModel
{
    /// <summary>
    /// 原始数据模型--对标数据表userinfo
    /// </summary>
    internal class UserInfo
    {
        /// <summary>
        /// PK id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户名 非空
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码 非空
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 逻辑删除 0 启用 1 逻辑删除
        /// </summary>
        public int IsDeleted { get; set; }
    }
}
