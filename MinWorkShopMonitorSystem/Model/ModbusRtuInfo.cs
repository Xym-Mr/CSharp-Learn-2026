using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem.Model
{
    /// <summary>
    /// ModbusRtu通讯配置信息实体
    /// </summary>
    internal class ModbusRtuInfo
    {
        private string _portName;
        /// <summary>
        /// 串口号
        /// </summary>
        public string PortName
        {
            get { return _portName; }
            set { _portName = value; }
        }

        private int _baudRate;
        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        private string _parity;
        /// <summary>
        /// 校验位
        /// </summary>
        public string Parity
        {
            get { return _parity; }
            set { _parity = value; }
        }

        private int _dataBits;
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits
        {
            get { return _dataBits; }
            set { _dataBits = value; }
        }

        private string _stopBits;
        /// <summary>
        /// 停止位
        /// </summary>
        public string StopBits
        {
            get { return _stopBits; }
            set { _stopBits = value; }
        }
    }
}
