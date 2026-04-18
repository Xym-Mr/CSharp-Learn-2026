using MinWorkShopMonitorSystem._4DAL;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///DAL 层的本质是“与外部系统（如硬件、网络）的交互”，它负责将抽象请求转换为具体介质可理解的信号
namespace MinWorkShopMonitorSystem.DAL
{
    /// <summary>
    /// 串口操作类，负责连断收发操作
    /// 
    /// 聚焦于“如何通过串口发送和接收字节”
    /// 1、连接，断开，参数配置这些操作直接与硬件交互，属于“基础设施”范畴
    /// 2、字节收发提供“透明通道”，不关心字节的含义是 Modbus 报文还是其他数据）
    /// </summary>
    internal class ModbusSerialPortDAL : IMobusTransport
    {
        private readonly SerialPort _serialPort;
        private readonly string _portName;
        private readonly int _baudRate;
        private readonly Parity _parity;
        private readonly int _dataBits;
        private readonly StopBits _stopBits;
        private readonly static object _lock = new object();

        public ModbusSerialPortDAL(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            _portName = portName;
            _baudRate = baudRate;
            _parity = parity;
            _dataBits = dataBits;
            _stopBits = stopBits;

            if (_serialPort == null) _serialPort = new SerialPort();
            if (_serialPort.IsOpen) _serialPort.Close();
            _serialPort.PortName = _portName;
            _serialPort.BaudRate = _baudRate;
            _serialPort.Parity = _parity;
            _serialPort.DataBits = _dataBits;
            _serialPort.StopBits = _stopBits;
        }

        public bool Open()
        {
            if (_serialPort.IsOpen) _serialPort.Close();
            _serialPort.Open();
            return _serialPort.IsOpen;
        }

        public void Close()
        {
            if (_serialPort == null) return;
            if (_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Close();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    this.Dispose();
                }

            }
        }

        /// <summary>
        /// 发送和接收字节数组
        /// </summary>
        /// <param name="sendBuffer"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public byte[] SendReceive(byte[] sendBuffer)
        {
            if (!_serialPort.IsOpen) throw new InvalidOperationException("数据发送失败：请先打开串口");
            if (sendBuffer?.Count() <= 4) throw new ArgumentOutOfRangeException(nameof(sendBuffer), "请检查报文数据格式准确性");

            lock (_lock)
            {
                this._serialPort.DiscardInBuffer();
                this._serialPort.DiscardOutBuffer();

                //发送报文
                _serialPort.Write(sendBuffer, 0, sendBuffer.Length);

                Thread.Sleep(50);//暂时先这么写，我暂时也不知道怎么实现标准的3.5T字符间隔
                //接收报文
                byte[] receiveBuffer = new byte[_serialPort.BytesToRead];
                _serialPort.Read(receiveBuffer, 0, receiveBuffer.Length);
                return receiveBuffer;
            }
        }

        public void Dispose() => _serialPort?.Dispose();
    }

}
