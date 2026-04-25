using SDLC_MinSingleDeviceDataMonitorSystem.Common;
using SDLC_MinSingleDeviceDataMonitorSystem.DAL;
using SDLC_MinSingleDeviceDataMonitorSystem.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_MinSingleDeviceDataMonitorSystem.BLL
{
    /// <summary>
    /// 定时器调度
    /// </summary>
    internal class ScheduleManager
    {
        private System.Timers.Timer _timer = new System.Timers.Timer();
        private ModbusProtocal _modbusprotocal = new ModbusProtocal();
        private SerialPortManager _serialPortManager = new SerialPortManager();
        private byte _slaveId;
        private ushort _startAddress;
        private ushort _qunatity;

        private bool _isRunning = false;



        /// <summary>
        /// 接收到数据时触发事件
        /// </summary>
        public Action<byte, List<ushort>> OnReceiveData;
        /// <summary>
        /// 数据接受出错时触发事件
        /// </summary>
        public Action<string> OnReceiveError;

        public ScheduleManager(double intervalMs, byte slaveId, ushort startAddress, ushort qunatity)
        {
            this._timer.Interval = intervalMs;
            this._slaveId = slaveId;
            this._startAddress = startAddress;
            this._qunatity = qunatity;
        }


        public void Open(ConfigInfo configInfo)
        {
            if (configInfo == null) throw new ArgumentNullException(nameof(configInfo), "连接参数为空");

            try
            {
                if (this._serialPortManager.IsOpen()) this._serialPortManager.Close();
                this._serialPortManager.Open(configInfo.PortName, configInfo.BaudRate, (Parity)Enum.Parse(typeof(Parity), configInfo.Parity), configInfo.DataBit, (StopBits)configInfo.StopBit);
            }
            catch (Exception ex)
            {
                ExceptionHandle.Catch(ex);
            }
        }

        public void Close()
        {
            this._serialPortManager.Close();
        }

        public bool IsOpen()
        {
            try
            {
                return this._serialPortManager.IsOpen();
            }
            catch (Exception ex)
            {
                ExceptionHandle.Catch(ex);
                return false;
            }
        }

        /// <summary>
        /// 开启定时器
        /// </summary>
        public void StartMonitor()
        {
            try
            {
                this._timer.Elapsed += _timer_Elapsed;
                this._timer.Start();
                this._isRunning = true;
            }
            catch (Exception ex)
            {
                this._timer.Elapsed -= _timer_Elapsed;
                ExceptionHandle.Catch(ex);
            }
        }

        /// <summary>
        /// 关闭定时器
        /// </summary>
        public void StopMonitor()
        {
            try
            {
                this._timer.Stop();
            }
            catch (Exception ex)
            {
                ExceptionHandle.Catch(ex);
            }
            finally
            {
                this._timer.Elapsed -= _timer_Elapsed;
                this._isRunning = false;
            }
        }

        public bool IsRunning()
        {
            return this._isRunning;
        }

        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            //this.StopMonitor();
            //1.构建 03 报文
            //2.调用串口发送并接收
            //3.校验 CRC
            //4.解析数据
            //5.成功 → 回调 OnDataReceived
            //6.失败 → 回调 OnError
            try
            {
                byte[] sendBuffer = this._modbusprotocal.BuildReadInputRegisterFrame(this._slaveId, this._startAddress, this._qunatity);

                byte[] receiveBuffer = this._serialPortManager.SendAndReceive(sendBuffer, 100);

                ushort[] receiveData = this._modbusprotocal.ParseInputRegisterResponseFrame(this._slaveId, receiveBuffer);

                if (receiveData?.Count() > 0) this.OnReceiveData?.Invoke(this._slaveId, receiveData.ToList());
                else this.OnReceiveError?.Invoke("数据解析为空");
            }
            catch (Exception ex)
            {
                this._isRunning = false;
                this._timer.Stop();
                this._timer.Elapsed -= _timer_Elapsed;
                ExceptionHandle.Catch(ex);

                this.OnReceiveError?.Invoke("解析错误:"+ex.Message);
            }
        }
    }
}
