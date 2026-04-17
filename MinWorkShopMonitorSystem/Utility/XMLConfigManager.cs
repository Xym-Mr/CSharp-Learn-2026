using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem.Utility
{
    /// <summary>
    /// XML配置文件读写类
    /// </summary>
    internal class XMLConfigManager
    {
        private readonly string _configFilePath;
        private readonly ReaderWriterLockSlim _lockSlim = new ReaderWriterLockSlim();

        public XMLConfigManager(string path)
        {
            _configFilePath = path;
            EnsureDirectoryExists();
        }

        public void XMLRead()
        {

        }

        public void XMLWrite()
        {

        }



        private void EnsureDirectoryExists()
        {
            //获取文件路径信息
            var directory = Path.GetDirectoryName(_configFilePath);

            if (string.IsNullOrWhiteSpace(directory)) throw new ArgumentException("配置文件路径不正确，无法创建文件夹", nameof(directory));

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);//创建文件夹
        }
    }
}
