using DelegateCommunication.Model;
using DelegateCommunication.Tools;
using DelegateCommunication.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCommunication.DAL
{
    internal class ModbusRtuDAL
    {
        private MobusRtu mobusRtu = new MobusRtu();


        public ushort[] ReadHoldingRegisters(byte slaveId, byte startAddr, byte quantity)
        {
            return this.mobusRtu.ReadHoldingRegisters(slaveId, startAddr, quantity);
        }

        internal void Close()
        {
            if (this.mobusRtu == null) throw new ArgumentNullException(nameof(this.mobusRtu), "对象为空，无法执行关闭操作");

            if (this.mobusRtu.IsOpen) this.mobusRtu.Close();
        }

        internal bool Open(ModbusRtuInfo modbusRtuInfo)
        {
            if (modbusRtuInfo == null) throw new ArgumentNullException(nameof(modbusRtuInfo), "串口参数对象为空");
            return this.mobusRtu.Open(modbusRtuInfo.PortName, modbusRtuInfo.BaudRate, modbusRtuInfo.Parity, modbusRtuInfo.DataBit, modbusRtuInfo.StopBits);
        }
    }
}
