using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_MinSingleDeviceDataMonitorSystem.Model
{
    internal class DGVDataInfo
    {
        public int SlaveId { get; set; }
        public int RegisterAddress { get; set; }
        public double CurValue { get; set; }
    }
}
