using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MinFileRW.Utility
{
    internal class JsonConfigManager
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private readonly string _configFilePath;
        //读写锁：读（共享锁），写（互斥锁）相较于lock更适合读多写少的场景
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();


        public JsonConfigManager(string filePath)
        {
            _configFilePath = filePath;//字段私有不对外暴露，只读、构造函数初始化字段，外部无法通过其他途径变更字段值，保证字段值和类实例对象深度一对一绑定，。

            EnsureDirectoryExists();
        }

        /// <summary>
        /// 读取配置文件数据信息并反序列化为指定类型对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        public T ReadConfig<T>()
        {
            _lock.EnterReadLock();

            try
            {
                if (File.Exists(_configFilePath))
                {
                    var json = File.ReadAllText(_configFilePath);
                    return JsonSerializer.Deserialize<T>(json);
                }
                return default;
            }
            catch { throw; }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void WriteConfig<T>(T t)
        {
            _lock.EnterWriteLock();
            try
            {
                var json = JsonSerializer.Serialize(t);
                File.WriteAllText(_configFilePath, json);
            }
            catch { throw; }
            finally { _lock.ExitWriteLock(); }
        }

        /// <summary>
        /// 确保文件路径存在
        /// </summary>
        /// <param name="configFilePath"></param>
        private void EnsureDirectoryExists()
        {
            //获取文件路径信息
            var directory = Path.GetDirectoryName(_configFilePath);

            if (string.IsNullOrWhiteSpace(directory)) throw new ArgumentException("配置文件路径不正确，无法创建文件夹",nameof(directory) );

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);//创建文件夹
        }
    }
}
