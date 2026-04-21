using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem.Model
{
    internal class SlaveDeviceInfo
    {
        public int SlaveDeviceID { get; set; }
        public int Address { get; set; }
        public string DataName { get; set; }
        public string Unit { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
    }
}
