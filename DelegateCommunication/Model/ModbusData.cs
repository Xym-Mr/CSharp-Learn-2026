using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCommunication.Model
{
    internal class ModbusData
    {
        public ushort SlaveId { get; set; }
        public ushort FunCode { get; set; }
        public ushort StartAddress { get; set; }
        public ushort Length { get; set; }
        public ushort[] Data { get; set; }
    }
}
