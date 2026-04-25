using SDLC_MinSingleDeviceDataMonitorSystem.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_MinSingleDeviceDataMonitorSystem.DAL
{
    //串口驱动
    internal class SerialPortManager
    {
        private SerialPort _serialPort = new SerialPort();

        public bool Open(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            this.Close();

            this._serialPort.PortName = portName;
            this._serialPort.BaudRate = baudRate;
            this._serialPort.Parity = parity;
            this._serialPort.DataBits = dataBits;
            this._serialPort.StopBits = stopBits;

            try
            {
                this._serialPort.Open();
                return this._serialPort.IsOpen;
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHandle.Catch(ex));
            }
        }

        public void Close()
        {
            if (this.IsOpen()) this._serialPort.Close();
            this._serialPort.Dispose();
        }

        public bool IsOpen()
        {
            return this._serialPort.IsOpen;
        }

        public void DisPose()
        {
            this.Close();
            this._serialPort.Dispose();
        }

        public byte[] SendAndReceive(byte[] sendData, int timeOutMs)
        {
            try
            {
                //清空缓存
                this._serialPort.DiscardInBuffer();
                this._serialPort.DiscardOutBuffer();

                if (sendData?.Length <= 0) throw new ArgumentException("发送的字节数组为空或数据长度为0", nameof(sendData));

                //发送报文
                this._serialPort.Write(sendData, 0, sendData.Length);

                //判断接收超时?
                Stopwatch sw = Stopwatch.StartNew();//创建高精度计时器并开始计时

                while ((this._serialPort.BytesToRead == 0))
                {
                    if (sw.ElapsedMilliseconds > timeOutMs) throw new Exception("通讯超时");
                }
                Thread.Sleep(100);
                //这个延时是为了简单的保证接受从站完整的报文字节，而不是只接受至少一个报文字节
                //标准的modbus协议规定相邻两字符间隔时间＞3.5T即表示报文传输结束

                int length = this._serialPort.BytesToRead;
                byte[] receiveData = new byte[length];
                this._serialPort.Read(receiveData, 0, length);
                return receiveData;
            }
            catch (Exception ex)
            {
                throw new Exception(ExceptionHandle.Catch(ex));
            }
        }
    }
}
