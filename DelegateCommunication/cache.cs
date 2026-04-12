using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCommunication
{
    public class ModbusRTU_Industrial_Final
    {
        private readonly SerialPort _sp = new SerialPort();
        private bool _isConnected;
        private DateTime _lastReconnectTime;

        public bool Connect(string com, int baud = 9600)
        {
            try
            {
                if (_sp.IsOpen) _sp.Close();
                _sp.PortName = com;
                _sp.BaudRate = baud;
                _sp.DataBits = 8;
                _sp.StopBits = StopBits.One;
                _sp.Parity = Parity.None;
                _sp.Open();
                _isConnected = true;
                return true;
            }
            catch
            {
                _isConnected = false;
                return false;
            }
        }


        /// <summary>
        /// 【工业标准】精确计时接收完整帧，无瑕疵
        /// </summary>
        private bool ReadFullModbusFrame_Industrial(out byte[] frame)
        {
            frame = null;
            var timeout = TimeSpan.FromMilliseconds(800);
            var start = DateTime.Now;

            try
            {
                // 等待 3 字节头
                while (_sp.BytesToRead < 3)
                {
                    if (DateTime.Now - start > timeout) return false;
                    Thread.Sleep(1);
                }

                byte[] header = new byte[3];
                ReadExact(header, 3);
                int dataLen = header[2];
                int need = dataLen + 2;

                // 等待剩余字节
                while (_sp.BytesToRead < need)
                {
                    if (DateTime.Now - start > timeout) return false;
                    Thread.Sleep(1);
                }

                byte[] body = new byte[need];
                ReadExact(body, need);

                frame = new byte[3 + need];
                Buffer.BlockCopy(header, 0, frame, 0, 3);
                Buffer.BlockCopy(body, 0, frame, 3, need);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 精确读取 N 字节（工业标准）
        /// </summary>
        private void ReadExact(byte[] buffer, int len)
        {
            int recv = 0;
            while (recv < len)
            {
                int n = _sp.Read(buffer, recv, len - recv);
                if (n == 0) throw new IOException("接收失败");
                recv += n;
            }
        }



        public void Close()
        {
            _isConnected = false;
            if (_sp.IsOpen) _sp.Close();
        }
    }

}
