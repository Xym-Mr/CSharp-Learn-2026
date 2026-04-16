using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinFileRW.Utility
{
    internal class CsvConfigManager
    {
        private readonly string _configPath;
        //读写锁
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly Encoding _encoding = Encoding.UTF8;


        public CsvConfigManager(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path), "配置文件路径不能为空");

            EnsureDirectoryExists();
        }

        //public void ReadConfig()
        //{
        //    if (!File.Exists(_configPath)) throw new ArgumentException("指定文件不存在，数据读取失败", nameof(_configPath));
        //    _lock.EnterReadLock();

        //    try
        //    {
        //        string[] lines = File.ReadAllLines(_configPath, _encoding);

        //        //逗号分隔字段，字段用“”包裹，换行符\r\n区分行

        //        //if (lines.Length > 0)
        //        //{
        //        //    for
        //        //}
        //    }
        //    catch { throw; }
        //    finally
        //    {
        //        _lock.ExitReadLock();
        //    }
        //}







        /// <summary>
        /// 确保文件夹存在
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void EnsureDirectoryExists()
        {
            var directory = Path.GetDirectoryName(_configPath);

            if (string.IsNullOrWhiteSpace(directory)) throw new ArgumentException("路径信息不正确，无法创建文件夹", nameof(directory));
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        }
    }
}
