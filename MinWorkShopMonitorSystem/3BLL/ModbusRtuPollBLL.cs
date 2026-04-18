using Microsoft.VisualBasic.Devices;
using MinWorkShopMonitorSystem._4DAL;
using MinWorkShopMonitorSystem.DAL;
using MinWorkShopMonitorSystem.Tools;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///BLL 层的本质是“业务规则的实现”，即使这些规则涉及技术细节，只要它们是解决业务问题的必要逻辑，就属于 BLL
namespace MinWorkShopMonitorSystem.BLL
{
    /// <summary>
    /// ModbusRtu主站通讯类，只关注从站数据读写操作的报文传输
    /// 
    /// 聚焦于“如何用 Modbus 协议表达业务需求”  
    /// 报文构建
    /// 1、接收UI层的业务请求-->通过Modbus读取输入寄存器的值
    /// 2、根据ModbusRTU协议规范，将业务请求（“读取寄存器”）转换为符合协议格式的字节数组
    /// 
    /// 报文解析
    /// 1、将接收到的字节数组解析为业务可理解的数据（如从响应中提取寄存器值）
    /// 2、响应UI层的业务请求-->返回寄存器的值
    /// </summary>
    internal class ModbusRtuPollBLL
    {
        private readonly IMobusTransport _transport = null;
        private readonly static object _lock = new object();

        public ModbusRtuPollBLL(IMobusTransport transport)
        {
            _transport = transport;//使用这个通讯类强制需要注入一个serialport对象
        }

        /// <summary>
        /// 读取从站3区只读输入寄存器的值
        /// </summary>
        public ushort[] ReadInputRegisters(byte slaveId, byte startAddr, byte count)
        {
            ushort[] resData = null;

            // 构建报文
            byte[] send = new byte[8] { slaveId, 0x04, (byte)(startAddr >> 8), startAddr, (byte)(count >> 8), count, 0, 0 };
            byte[] crc = CRCCheck.CRC16(send, 6);
            send[6] = crc[0];
            send[7] = crc[1];

            //发送、接收报文
            byte[] reciver = _transport.SendReceive(send);

            if (reciver == null) throw new ArgumentNullException(nameof(reciver), "3区数据读取为空");
            try
            {
                //解析报文
                // Tx: 000000 - 01 04 00 00 00 0A 70 0D
                // Rx: 000001 - 01 04 14 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 95 81
                if (reciver.Length > 5)
                {
                    if (reciver[0] == slaveId && reciver[1] == 0x04)
                    {
                        ushort dataLen = reciver[2];//获取数据字节个数/2=读取的寄存器个数
                        if (reciver.Length == dataLen + 5)
                        {
                            if (CRCCheck.CheckCRC(reciver))
                            {
                                byte[] data = new byte[dataLen];
                                Array.Copy(reciver, 3, data, 0, dataLen);//截取数据

                                //解析数据
                                resData = new ushort[dataLen / 2];//数据字节个数/2=读取的寄存器个数
                                for (int i = 0; i < data.Length; i += 2)
                                {
                                    resData[i / 2] = (ushort)(data[i] * 256 + data[i + 1]);
                                }
                                return resData;
                            }
                        }
                    }
                }
                throw new ArgumentOutOfRangeException(nameof(reciver), reciver, "读取数据不正确，请检查从站设备连接");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 读取从站4区保持寄存器的值
        /// </summary>
        /// <param name="slaveId"></param>
        /// <param name="startAddr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ushort[] ReadHoldingRegisters(byte slaveId, byte startAddr, byte count)
        {
            ushort[] resData = null;

            // 构建报文
            byte[] send = new byte[8] { slaveId, 0x03, (byte)(startAddr >> 8), startAddr, (byte)(count >> 8), count, 0, 0 };
            byte[] crc = CRCCheck.CRC16(send, 6);
            send[6] = crc[0];
            send[7] = crc[1];

            //发送、接收报文
            byte[] reciver = _transport.SendReceive(send);

            if (reciver == null) throw new ArgumentNullException(nameof(reciver), "3区数据读取为空");
            try
            {
                //解析报文
                //Tx: 000004 - 01 03 00 00 00 0A C5 CD
                //Rx: 000005 - 01 03 14 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 A3 67
                if (reciver.Length > 5)
                {
                    if (reciver[0] == slaveId && reciver[1] == 0x03)
                    {
                        ushort dataLen = reciver[2];//获取数据字节个数/2=读取的寄存器个数
                        if (reciver.Length == dataLen + 5)
                        {
                            if (CRCCheck.CheckCRC(reciver))
                            {
                                byte[] data = new byte[dataLen];
                                Array.Copy(reciver, 3, data, 0, dataLen);//截取数据

                                //解析数据
                                resData = new ushort[dataLen / 2];//数据字节个数/2=读取的寄存器个数
                                for (int i = 0; i < data.Length; i += 2)
                                {
                                    resData[i / 2] = (ushort)(data[i] * 256 + data[i + 1]);
                                }
                                return resData;
                            }
                        }
                    }
                }
                throw new ArgumentOutOfRangeException(nameof(reciver), reciver, "读取数据不正确，请检查从站设备连接");
            }
            catch
            {
                throw;
            }

        }

        public bool Open()
        {
            return _transport.Open();
        }

        public void Close()
        {
            _transport.Close();
        }

    }
}
