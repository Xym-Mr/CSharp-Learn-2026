using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem.Utility
{
    /// <summary>
    /// AppConfig配置文件读写类
    /// </summary>
    internal class AppConfigManager
    {
        public static string GetAbsolutePath(string key)
        {
            string path = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(path))
                throw new InvalidOperationException($"配置项 '{key}' 未找到");

            return Path.IsPathRooted(path) ? path : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }
    }
}
