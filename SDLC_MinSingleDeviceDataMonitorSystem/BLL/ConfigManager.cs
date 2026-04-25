using SDLC_MinSingleDeviceDataMonitorSystem.DAL;
using SDLC_MinSingleDeviceDataMonitorSystem.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_MinSingleDeviceDataMonitorSystem.BLL
{
    internal class ConfigManager
    {
        public T LoadConfig<T>(string path)
        {
            return new JsonConfigManager(path).LoadConfig<T>();
        }

        public void SaveConfig<T>(string path, T t)
        {
            new JsonConfigManager(path).SaveConfig<T>(t);
        }

        public ConfigInfo GetDefaultConfig()
        {
            return new ConfigInfo()
            {
                PortName = "Com19",
                BaudRate = 9600,
                Parity = Parity.None.ToString(),
                DataBit = 8,
                StopBit = 1,
                SlaveId = 1,
                StartAddress = 0,
                RegisterCount = 10,
                ReadInterval = 50
            };
        }

        public string GetAbsolutePath(string key)
        {
            string path = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(path))
                throw new InvalidOperationException($"配置项 '{key}' 未找到");

            return Path.IsPathRooted(path) ? path : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

    }
}
