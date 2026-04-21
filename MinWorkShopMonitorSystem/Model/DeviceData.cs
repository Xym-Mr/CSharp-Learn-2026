using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem.Model
{
    internal class DeviceData
    {
        /// <summary>
        /// 设备的从站ID
        /// </summary>
        public int SlaveDeviceId { get; set; }

        /// <summary>
        /// 温度值
        /// </summary>
        public float Temperature { get; set; }

        /// <summary>
        /// 湿度值
        /// </summary>
        public float Humidity { get; set; }

    }
}
