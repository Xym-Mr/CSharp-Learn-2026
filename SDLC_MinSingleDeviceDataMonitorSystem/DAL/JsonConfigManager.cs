using SDLC_MinSingleDeviceDataMonitorSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SDLC_MinSingleDeviceDataMonitorSystem.DAL
{
    internal class JsonConfigManager
    {
        private readonly string _configFilePath;

        public JsonConfigManager(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path), "文件路径不能为空");

            _configFilePath = path;
            CheckDirectory();
        }

        /// <summary>
        /// 检查文件路径是否存在
        /// </summary>
        private void CheckDirectory()
        {
            string directory = Path.GetDirectoryName(_configFilePath);

            if (string.IsNullOrWhiteSpace(_configFilePath)) throw new ArgumentNullException(nameof(_configFilePath), "文件路径为空");

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        }


        /// <summary>
        /// 读取Json配置文件，返回一个实体类对象
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <returns></returns>
        public T LoadConfig<T>()
        {
            try
            {
                if (File.Exists(_configFilePath))
                {
                    string json = File.ReadAllText(_configFilePath);
                    return JsonSerializer.Deserialize<T>(json);
                }
                return default;
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHandle.Catch(ex));
            }
        }

        /// <summary>
        /// 将实体类对象数据写入到Json配置文件中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void SaveConfig<T>(T obj)
        {
            try
            {
                string json = JsonSerializer.Serialize<T>(obj);
                File.WriteAllText(_configFilePath, json);
            }
            catch (Exception ex)
            { throw new Exception(ExceptionHandle.Catch(ex)); }
        }
    }
}
