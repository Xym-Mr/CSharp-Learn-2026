using DelegateCommunication.DAL;
using DelegateCommunication.Model;
using DelegateCommunication.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCommunication.BLL
{
    internal class CommunicationBLL
    {
        private ModbusRtuDAL modbusRtuDAL = new ModbusRtuDAL();

        public ushort[] ReadHoldingRegisters(byte slaveId, byte startAddr, byte quantity)
        {
            return this.modbusRtuDAL.ReadHoldingRegisters(slaveId, startAddr, quantity);
        }

        internal void Close()
        {
            this.modbusRtuDAL.Close();
        }

        internal bool Open(ModbusRtuInfo modbusRtuInfo)
        {
            return this.modbusRtuDAL.Open(modbusRtuInfo);
        }
    }
}
