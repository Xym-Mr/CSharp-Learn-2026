using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_MinSingleDeviceDataMonitorSystem.Common
{
    internal class ExceptionHandle
    {
        internal enum InfoType
        {
            Success, Warn, Error
        }

        /// <summary>
        /// 异常捕获并返回规范提示
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        public static string Catch(Exception ex, [CallerMemberName]string module="")
        {
            //这里可以是记录日志（文件/数据库）
            //Log()

            //返回规范、友好的信息字符串（按需自定义）
            return $"[{module}]异常：{ex.Message}";
        }

        public static void ShowMessage(string msg, InfoType infoType = InfoType.Error)
        {
            //这里可以自定义显示框的样式和行为(例如可以定时对话框自动关闭等)，并全局使用相同的显示逻辑，解耦便于维护、复用。
            switch (infoType)
            {
                case InfoType.Success: MessageBox.Show(msg, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case InfoType.Warn: MessageBox.Show(msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); break;
                case InfoType.Error: MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); break;
                default: MessageBox.Show(msg, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
            }
        }
    }
}
