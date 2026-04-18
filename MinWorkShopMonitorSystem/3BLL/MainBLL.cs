using MinWorkShopMonitorSystem.DAL;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem.BLL
{
    internal class MainBLL
    {
        private MainDAL mainDAL = new MainDAL();


        public string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// 获取指定路径的json配置文件内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T GetJsonConfig<T>(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path), "文件路径不能为空");
            return this.mainDAL.GetJsonConfig<T>(path);
        }

        /// <summary>
        /// 将对象数据保存至指定json配置文件中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="t"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SaveJsonConfig<T>(string path, T t)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path), "文件路径不能为空");
            if (t == null) throw new ArgumentNullException(nameof(t), "数据对象为空");
            this.mainDAL.SaveJsonConfig(path, t);
        }

        public List<string> IniReadRangeInfo(string path, string section, string key)
        {
            return this.mainDAL.IniReadRangeInfo(path, section, key);
        }


    }
}
