using MinWorkShopMonitorSystem.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem.DAL
{
    internal class MainDAL
    {
        /// <summary>
        /// Json文件读取，返回T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public T GetJsonConfig<T>(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path), "文件路径不能为空");
            return new JsonConfigManager(path).Read<T>();
        }

        /// <summary>
        /// Json文件读取，返回List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public List<T> GetJsonConfigToList<T>(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path), "文件路径不能为空");
            return new JsonConfigManager(path).ReadtoList<T>();
        }

        /// <summary>
        ///Json文件写入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="t"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SaveJsonConfig<T>(string path, T t)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path), "文件路径不能为空");
            if (t == null) throw new ArgumentNullException(nameof(t), "数据对象为空");
            new JsonConfigManager(path).Write<T>(t);
        }

        /// <summary>
        /// 默认带string.Empty缺省值的INI读取
        /// </summary>
        /// <param name="path"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string IniRead(string path, string section, string key)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path), "文件路径不能为空");
            return new IniConfigManager(path).IniRead(section, key);
        }

        /// <summary>
        /// INI写入
        /// </summary>
        /// <param name="path"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void IniWrite(string path, string section, string key, string val)
        {
            new IniConfigManager(path).IniWrite(section, key, val);
        }

        /// <summary>
        /// 带指定缺省值的INI读取
        /// </summary>
        /// <param name="path"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string IniRead(string path, string section, string key, string defaultVal)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path), "文件路径不能为空");
            return new IniConfigManager(path).IniRead(section, key, defaultVal);
        }

        /// <summary>
        /// 将INI配置文件中以逗号分隔的字符串转化为List<string>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> IniReadRangeInfo(string path, string section, string key)
        {
            string res = IniRead(path, section, key);
            if (!string.IsNullOrWhiteSpace(res))
            {
                return res.Trim().Split(',').ToList();
            }
            return null;
        }

    }
}
