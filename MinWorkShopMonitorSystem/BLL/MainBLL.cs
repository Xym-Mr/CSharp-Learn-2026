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
        public string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }

    }
}
