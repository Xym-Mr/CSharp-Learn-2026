using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCommunication.Model
{
    internal class ModbusRtuInfo
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; } = 9600;
        public Parity Parity { get; set; } = Parity.None;
        public int DataBit { get; set; } = 8;
        public StopBits StopBits { get; set; } = StopBits.One;
    }
}
