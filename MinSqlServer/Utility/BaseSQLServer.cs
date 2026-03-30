using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSqlServer.Utility
{
    /// <summary>
    /// 数据库最核心的操作；连接、读写，不做其他的分析、处理
    /// </summary>
    internal class BaseSQLServer
    {
        //连接字符串
        private static readonly string sqlConnectString = "server=DESKTOP-Q07T3CM\\SQLEXPRESS;database=JiaGouXunLianDB0;user=sa;pwd=sa;TrustServerCertificate=True";

        private static SqlConnection SQLServerConnect()
        {
            SqlConnection con = new SqlConnection(sqlConnectString);
            con.Open();
            return con;
        }

        #region 需求分析--执行数据库操作
        /// 1、建立数据库连接(核心实现)
        /// 2、传入sqlcommand,sqlparams
        /// 3、执行数据库操作(核心实现)
        /// 4、结果输出
        /// 
        /// 增加：insert+param+res void、删除：delet+param++res void、修改：update+param+res void 
        /// 查询：select+param/null+res data
        #endregion

        /// <summary>
        /// 查询操作，返回reader。
        /// </summary>
        public static SqlDataReader ExecuteReader(string sqlCmd, SqlParameter[] parameters)
        {
            //using (SqlConnection con = SQLServerConnect())
            //{
            SqlConnection con = SQLServerConnect();

            if (con.State != ConnectionState.Open) throw new Exception("数据库连接未打开");

            SqlCommand command = con.CreateCommand();

            command.CommandText = sqlCmd;
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(parameters);

            //执行SQL语句行并返回受影响的行数--》增、删、改
            command.ExecuteNonQuery();

            //执行SQL语句行并返回一个带SQL连接的reader
            return command.ExecuteReader(CommandBehavior.CloseConnection);
            //CommandBehavior.CloseConnection，当外部把 reader 释放（Close/Dispose）时，系统会自动把对应的 SqlConnection 关闭！
            //}//SqlDataReader 是 连接式读取（SequentialAccess），所以这里不能断开连接，不然数据读不出来

        }


        /// <summary>
        /// 增、删、改操作，返回受影响的行数。
        /// </summary>
        /// <param name="sqlCmd"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sqlCmd, SqlParameter[] parameters)
        {
            using (SqlConnection con = SQLServerConnect())
            {
                SqlCommand command = con.CreateCommand();
                command.CommandText = sqlCmd;
                command.CommandType = CommandType.Text;
                command.Parameters.AddRange(parameters);

                //执行SQL语句行并返回受影响的行数--》增、删、改
                return command.ExecuteNonQuery();
            }//执行完SQL语句之后自动断开数据库连接
        }
    }
}
