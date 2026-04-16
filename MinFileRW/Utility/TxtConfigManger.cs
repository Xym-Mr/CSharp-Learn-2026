using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinFileRW.Utility
{
    /// <summary>
    /// 配置文件管理器，线程安全
    /// </summary>
    internal class TxtConfigManger : IDisposable
    {
        //UTF-8编码
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _configPath;
        //读写锁
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// 初始化配置管理器
        /// </summary>
        /// <param name="path">配置文件路径</param>
        /// <exception cref="ArgumentException"></exception>
        public TxtConfigManger(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("文件路径不能为空", nameof(path));

            _configPath = path;

            EnsureDirectoryExists();
        }

        /// <summary>
        /// 读取配置文件，线程安全
        /// </summary>
        public Dictionary<string, string> ReadConfig()
        {
            _lock.EnterReadLock();//读锁

            var config = new Dictionary<string, string>();

            try
            {
                if (File.Exists(_configPath))
                {
                    var lines = File.ReadAllLines(_configPath, _encoding);//UTF-8编码读取所有的行数据
                    foreach (var line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#')) continue;//空行或者#开头的注释行略过
                        //数据行,格式解析
                        var parts = line.Split('=', 2);//按照=号分割为最多2个元素
                        if (parts.Length == 2)
                        {
                            var key = parts[0].Trim();
                            var value = parts[1].Trim();
                            config[key] = value;
                        }
                    }
                }
                return config;
            }
            catch
            {
                throw;//把异常往上层抛，这里不处理。
            }
            finally
            {
                _lock.ExitReadLock();//释放阅读锁
            }
        }

        /// <summary>
        /// 写入配置文件（线程安全）
        /// </summary>
        /// <param name="config">键值对形式的配置</param>
        public void WriteConfig(Dictionary<string, string> config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config), "配置不能为null");

            _lock.EnterWriteLock();
            try
            {
                var lines = new List<string>();
                foreach (var kvp in config)
                {
                    lines.Add($"{kvp.Key.Trim()} = {kvp.Value.Trim()}");
                }

                File.WriteAllLines(_configPath, lines, _encoding);
            }
            catch
            {
                throw;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }    

        public void Dispose()
        {
            _lock?.Dispose();
        }

        /// <summary>
        /// 确保配置文件所在目录存在
        /// </summary>
        private void EnsureDirectoryExists()
        {
            var directory = Path.GetDirectoryName(_configPath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);//创建文件夹
            }
        }
    }
}
