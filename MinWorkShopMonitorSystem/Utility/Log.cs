using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem.Utility
{
    /// <summary>
    /// 日志类
    /// </summary>
    internal class Log
    {
        /// <summary>
        /// 日志基础路径
        /// </summary>
        private readonly static string logBasePath = Path.Combine(AppContext.BaseDirectory, "logs");
        /// <summary>
        /// 线程安全锁
        /// </summary>
        private readonly static object _lock = new object();

        /// <summary>
        /// 日志级别
        /// </summary>
        private enum LogLevel { Debug, Info, Warn, Error, Fatal }
        #region 对外部暴露公开方法
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="methodName"></param>
        public static void Debug(string msg, [CallerMemberName] string methodName = "")
        {
            WriteLog(msg, LogLevel.Debug, methodName, null);
        }
        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="methodName"></param>
        public static void Info(string msg, [CallerMemberName] string methodName = "")
        {
            WriteLog(msg, LogLevel.Info, methodName, null);
        }
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="methodName"></param>
        public static void Warn(string msg, [CallerMemberName] string methodName = "")
        {
            WriteLog(msg, LogLevel.Warn, methodName, null);
        }
        /// <summary>
        /// 业务错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <param name="methodName"></param>
        public static void Error(string msg, Exception ex, [CallerMemberName] string methodName = "")
        {
            WriteLog(msg, LogLevel.Error, methodName, ex);
        }
        /// <summary>
        /// 系统级致命错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <param name="methodName"></param>
        public static void Falat(string msg, Exception ex, [CallerMemberName] string methodName = "")
        {
            WriteLog(msg, LogLevel.Fatal, methodName, ex);
        }
        #endregion

        //私有的实现方法
        private static void WriteLog(string msg, LogLevel level, string methodName, Exception ex = null)
        {
            try
            {
                string directory = Path.Combine(logBasePath, $"{DateTime.Now:yyyy-MM-dd}logs");
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);//创建文件目录
                string fileName = $"{DateTime.Now:HH:mm:ss}.log";//文件全名
                //全路径：APP启动路径\logs\yyyy-MM-ddlogs\HH:mm:ss.log
                string logFullPath = Path.Combine(directory, fileName);

                //拼接日志信息
                string logMsg = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}]  [{level}]  [ThreadId:{Environment.CurrentManagedThreadId}] [Method:{methodName}] {msg}";
                if (ex != null) logMsg += $"\r\n异常信息:{ex.Message}\r\n堆栈:{ex.StackTrace}";

                //线程安全锁
                lock (_lock)
                {
                    File.AppendAllText(logFullPath, logMsg + Environment.NewLine);
                }
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("日志写入时发生错误......");
            }
        }
    }
}
