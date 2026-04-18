using MinWorkShopMonitorSystem.BLL;
using MinWorkShopMonitorSystem.DAL;
using MinWorkShopMonitorSystem.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem.AppService
{
    /// <summary>
    /// 只负责建立关联关系
    /// </summary>
    internal class ModbusRtuController
    {
        public static ModbusRtuPollBLL GetModbusRtuPoll(ModbusRtuInfo modbusRtuInfo)
        {
            if (modbusRtuInfo == null) throw new ArgumentNullException(nameof(modbusRtuInfo), "数据对象为空");
            var tranport = new ModbusSerialPortDAL(modbusRtuInfo.PortName, modbusRtuInfo.BaudRate, (Parity)Enum.Parse(typeof(Parity), modbusRtuInfo.Parity), modbusRtuInfo.DataBits, (StopBits)Enum.Parse(typeof(StopBits), modbusRtuInfo.StopBits));

            return new ModbusRtuPollBLL(tranport);
        }
    }
}
