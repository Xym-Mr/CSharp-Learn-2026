using Microsoft.Data.SqlClient;
using MinSqlServer.Model.DBModel;
using MinSqlServer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSqlServer.DAL
{
    /// <summary>
    /// 数据访问层--》增删改查操作和数据读取、解析
    /// </summary>
    internal class UserDAL
    {
        BaseSQLServer baseSQLServer = new BaseSQLServer();


        #region 需求分析：实现数据读写操作
        /// 1、写sql语句(核心实现)
        /// 2、调用数据库操作
        /// 3、接收并解析数据(核心实现)
        #endregion

        ///② 防崩验证（DAL 必须做），不做业务验证，判断会不会导致程序崩溃：
        //参数是否为 null
        //集合是否为 null
        //model 是否为 null
        //数组是否越界
        //这些 DAL 必须做！因为 DAL 要保证自己不会因为上层传入的参数而崩溃、报错、不依赖上层


        public void Insert()
        {

        }

        public void Delete()
        {

        }

        public void Update()
        {
            //查询语句
            //   string sqlCmd = "update UserInfos set col=colVal.....";
        }

        public void SelectAll()
        {
            //查询语句
            string sqlCmd = "select * from UserInfos";
            SqlDataReader reader = BaseSQLServer.ExecuteReader(sqlCmd, null);
        }

        public List<UserInfo> Select(UserInfo userInfo)
        {
            //DAL防崩验证，只要保证流程不崩溃，如果不验证null，后续可能会报空值异常，程序崩溃。
            if (userInfo == null) return null;

            //查询语句
            string sqlCmd = "select UserId,UserName,UserPwd,IsDeleted from UserInfos where UserName=@userName and UserPwd=@userPwd";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@userName",userInfo.UserName),
                new SqlParameter("@userPwd",userInfo.UserPwd),
            };

            try
            {
                using (SqlDataReader reader = BaseSQLServer.ExecuteReader(sqlCmd, parameters))
                {
                    if (reader != null)
                    {
                        List<UserInfo> userInfos = new List<UserInfo>();
                        while (reader.Read())
                        {
                            //读取reader中的每一行数据
                            int id = Convert.ToInt32(reader["UserId"]);
                            string name = reader["UserName"].ToString();
                            string pwd = reader["UserPwd"].ToString();
                            int isDeleted = Convert.ToInt32(reader["IsDeleted"]);

                            userInfos.Add(new UserInfo()
                            {
                                UserId = id,
                                UserName = name,
                                UserPwd = pwd,
                                IsDeleted = isDeleted
                            });
                        }
                        reader.Close();//这里使用close之后，配合command.ExecuteReader(CommandBehavior.CloseConnection);会自动释放sqlconnect，所以和using不能同时使用，会重复释放。
                        return userInfos.Count > 0 ? userInfos : userInfos = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("DAL异常：" + ex.Message);
            }
            return null;
        }

    }
}
