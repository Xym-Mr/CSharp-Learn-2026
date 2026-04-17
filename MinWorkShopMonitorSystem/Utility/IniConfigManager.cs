using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem.Utility
{
    /// <summary>
    /// Ini配置文件读写类
    /// </summary>
    internal class IniConfigManager
    {
        private readonly string _configFilePath;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public IniConfigManager(string path)
        {
            _configFilePath = path;
            EnsureDirectoryExists();
        }

        public bool IniWrite(string section, string key, string val)
        {
            _lock.EnterWriteLock();
            try
            {
                return WritePrivateProfileString(section, key, val, _configFilePath);
            }
            catch { throw; }
            finally { _lock.ExitWriteLock(); }
        }

        public string IniRead(string section, string key)
        {
            _lock.EnterReadLock();
            try
            {
                StringBuilder sb = new StringBuilder(1024);
                GetPrivateProfileString(section, key, string.Empty, sb, 1024, _configFilePath);
                return sb.ToString();
            }
            catch { throw; }
            finally { _lock.ExitReadLock(); }

        }

        public string IniRead(string section, string key, string defaultVal)
        {
            _lock.EnterReadLock();
            try
            {
                StringBuilder sb = new StringBuilder(1024);
                GetPrivateProfileString(section, key, defaultVal, sb, 1024, _configFilePath);
                return sb.ToString();
            }
            catch { throw; }
            finally { _lock.ExitWriteLock(); }
        }

        /// <summary>
        /// 申明ini文件的写操作函数WritePrivateProfileString()
        /// </summary>
        /// <param name="section">INI文件中的段落</param>
        /// <param name="key">INI文件中的关键字</param>
        /// <param name="val">INI文件中关键字的数值</param>
        /// <param name="filepath">INI文件的完整的路径和名称</param>
        /// <returns>操作成功与否</returns>
        [DllImport("Kernel32.dll")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filepath);

        /// <summary>
        /// 申明ini文件的读操作函数GetPrivateProfileString()
        /// </summary>
        /// <param name="section">INI文件中的段落名称</param>
        /// <param name="key">INI文件中的关键字</param>
        /// <param name="def">无法读取时候时候的缺省数值</param>
        /// <param name="retVal">读取数值</param>
        /// <param name="size">数值的大小</param>
        /// <param name="filePath">INI文件的完整路径和名称</param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);



        private void EnsureDirectoryExists()
        {
            //获取文件路径信息
            var directory = Path.GetDirectoryName(_configFilePath);

            if (string.IsNullOrWhiteSpace(directory)) throw new ArgumentException("配置文件路径不正确，无法创建文件夹", nameof(directory));

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);//创建文件夹
        }
    }
}
