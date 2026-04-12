using DelegateCommunication.DAL;
using DelegateCommunication.Model;
using DelegateCommunication.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCommunication.Utility
{
    /// <summary>
    /// 协议解析类，这个应该跟utility中的协议类是一一对应的
    /// </summary>
    public class MobusRtu
    {
        private SerialPort port = new SerialPort();
        private readonly object _lock = new object();
        public double outTime = 0.0;

        public bool IsOpen
        {
            get { return port.IsOpen; }
        }

        public void Close()
        {
            if (port.IsOpen)
            {
                port.Close();
                port.Dispose();
            }
        }

        public bool Open(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            //1、空判断：ArgumentNullException:方法参数为 null，但代码不允许 null
            if (string.IsNullOrWhiteSpace(portName)) throw new ArgumentNullException(nameof(portName), "串口端口名称不能为空");

            //2、格式判断：ArgumentException:参数有问题，但不是null、也不是范围问题（是格式错误等），是ArgumentNullException，ArgumentOutOfRangeException的基类
            if (!portName.Trim().Contains("com", StringComparison.OrdinalIgnoreCase)) throw new ArgumentException("串口名称必须包含COM", nameof(portName));

            //3、数值范围判断：ArgumentOutOfRangeException:参数不为 null，但值不合法（超出范围、无效枚举等）
            if (baudRate <= 0) throw new ArgumentOutOfRangeException(nameof(baudRate), baudRate, "波特率设置不正确！");

            //4、参数合法性判断：校验枚举是否合法
            if (dataBits <= 0) throw new ArgumentOutOfRangeException(nameof(dataBits), dataBits, "数据位设置不正确");
            if (!Enum.IsDefined(typeof(StopBits), stopBits)) throw new ArgumentOutOfRangeException(nameof(stopBits), stopBits, "停止位不是有效的枚举值");
            if (!Enum.IsDefined(typeof(Parity), parity)) throw new ArgumentOutOfRangeException(nameof(parity), parity, "传入的 Parity不是有效的枚举值");

            try
            {
                if (port.IsOpen) port.Close();

                port.PortName = portName.Trim();
                port.BaudRate = baudRate;
                port.Parity = parity;
                port.DataBits = dataBits;
                port.StopBits = stopBits;

                port.Open();

                outTime = GetOutTime(port.BaudRate);
                return true;
            }
            catch
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// 读取从站设备4区保持寄存器数据
        /// </summary>
        /// <param name="slaveId">从站地址</param>
        /// <param name="startAddr">起始地址</param>
        /// <param name="quantity">读取个数</param>
        /// <returns>ushort[]</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ushort[] ReadHoldingRegisters(byte slaveId, byte startAddr, byte quantity)
        {
            ushort[] resData = null;

            // 构建报文
            byte[] send = new byte[8] { slaveId, 0x03, (byte)(startAddr >> 8), startAddr, (byte)(quantity >> 8), quantity, 0, 0 };
            byte[] crc = CRCTool.CRC16(send, 6);
            send[6] = crc[0];
            send[7] = crc[1];

            if (port == null) throw new ArgumentNullException(nameof(port), "串口为空，请先打开串口");
            if (!port.IsOpen) throw new ArgumentException("串口未被打开", nameof(port));
            byte[] reciver = null;

            lock (_lock)
            {
                this.port.DiscardInBuffer();
                this.port.DiscardInBuffer();

                //发送报文
                this.port.Write(send, 0, send.Length);

                Thread.Sleep(100);

                //接收报文
                reciver = new byte[this.port.BytesToRead];
                this.port.Read(reciver, 0, reciver.Length);
            }

            if (reciver == null) throw new ArgumentNullException(nameof(reciver), "4区数据读取为空");

            try
            {
                //解析报文
                //Tx: 000001 - 01 03 00 00 00 0A C5 CD
                //Rx: 000001 - 01 03 14 [22 C4 00 00 00 00 00 00 00 00 00 00 22 BF 00 00 00 00 00 00] 5D C9
                if (reciver.Length > 5)
                {
                    if ((byte)reciver[0] == slaveId && (byte)reciver[1] == 0x03)
                    {
                        ushort dataLen = reciver[2];
                        if (reciver.Length == dataLen + 5)
                        {
                            if (CRCTool.CheckCRC(reciver))
                            {
                                byte[] data = new byte[dataLen];
                                Array.Copy(reciver, 3, data, 0, dataLen);//截取数据

                                //解析数据
                                resData = new ushort[dataLen / 2];
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
        /// 根据不同波特率计算相邻两字符间超时时间
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private double GetOutTime(int baudRate)
        {
            double outMs = 0.0;//(ms)
            if (baudRate <= 0) throw new ArgumentOutOfRangeException(nameof(baudRate), baudRate, "波特率设置需要大于0");
            if (baudRate > 19200) outMs = 1.75;//以上波特率，固定1.75Ms
            else outMs = 3.5 * 11 * 1000 / baudRate;//波特率：bit/s
            //毫秒（ms）：
            // T35(ms) = (3.5 × 11【1个字符事件（标准 RTU）：1起始 + 8数据 + 1校验（可选）+1停止 → 共11位】 × 1000) / BaudRate ≈ **38500 / 波特率 * *

            return outMs + 0.1;//增加0.1余量，可不加
        }

    }
}
