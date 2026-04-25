using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_MinSingleDeviceDataMonitorSystem.Model
{
    internal class ConfigInfo
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public int DataBit { get; set; }
        public string Parity { get; set; }
        public int StopBit { get; set; }
        public byte SlaveId { get; set; }
        public ushort StartAddress { get; set; }
        public ushort RegisterCount { get; set; }
        public int ReadInterval { get; set; }
    }
}
