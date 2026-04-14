using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MinFileRW.Utility
{
    /// <summary>
    /// 规范日志类（线程安全 + 按天分文件 + 日志分级）
    /// </summary>
    internal class Log
    {
        // 静态锁：保证多线程写入不混乱
        private static readonly object _lockObj = new object();
        // 日志根目录-AppContext.BaseDirectory跨平台通用，带反斜杠\
        private static readonly string _basePath = Path.Combine(AppContext.BaseDirectory, "Logs");

        private static void WriteLog(LogLevel logLevel, string message, Exception ex = null)
        {
            try
            {
                string childPath = Path.Combine(_basePath, $"{DateTime.Now:yyyy-MM}Logs");
                if (!Directory.Exists(childPath)) { Directory.CreateDirectory(childPath); }
                //日志文件名-按天命名
                string fileName = $"{DateTime.Now:yyyy-MM-dd}.log";
                //文件全路径根目录\Logs\yyyy-MMLogs\yyyy-MM-dd.log
                string logPath = Path.Combine(childPath, fileName);
                //拼接日志内容
                string logMsg = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}]  [{logLevel}]  [ThreadId:{Environment.CurrentManagedThreadId}]  {message}";
                if (ex != null) { logMsg += $"\n异常信息：{ex.Message}\n堆栈：{ex.StackTrace}"; }

                //写入文件，加锁保证线程安全
                lock (_lockObj)
                {
                    // 使用文件追加方法（会自动创建文件,写完后自动关闭文件）
                    File.AppendAllText(logPath, logMsg + Environment.NewLine);//尾部添加换行
                }
            }
            catch
            {
                // 日志本身异常不能WriteLog，避免递归调用影响业务崩溃
                Console.Out.WriteLine(message);

                //写入另一个文件中或者写入缓存中
            }
        }


        /// <summary>
        /// 日志级别(Public)
        /// </summary>
        public enum LogLevel
        {
            Debug, Info, Warn, Error, Fatal
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg, [CallerMemberName] string methodName = "")
        {
            msg = $"[Method:{methodName}]{Environment.NewLine + msg}";
            WriteLog(LogLevel.Debug, msg);
        }

        /// <summary>
        /// 消息日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg, [CallerMemberName] string methodName = "")
        {
            msg = $"[Method:{methodName}]{Environment.NewLine + msg}";
            WriteLog(LogLevel.Info, msg);
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg, [CallerMemberName] string methodName = "")
        {
            msg = $"[Method:{methodName}]{Environment.NewLine + msg}";
            WriteLog(LogLevel.Warn, msg);
        }

        /// <summary>
        /// 业务错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Error(string msg, Exception ex, [CallerMemberName] string methodName = "")
        {
            msg = $"[Method:{methodName}]{Environment.NewLine + msg}";
            WriteLog(LogLevel.Error, msg, ex);
        }

        /// <summary>
        /// 系统级错误日志-可能导致程序崩溃
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Fatal(string msg, Exception ex = null, [CallerMemberName] string methodName = "")
        {
            msg = $"[Method:{methodName}]{Environment.NewLine + msg}";
            WriteLog(LogLevel.Fatal, msg, ex);
        }


    }
}
