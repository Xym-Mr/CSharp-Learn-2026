using DelegateCommunication.BLL;
using DelegateCommunication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace DelegateCommunication.UI
{
    public partial class MainFm : Form
    {
        public MainFm()
        {
            InitializeComponent();
        }

        private void MainFm_Load(object sender, EventArgs e)
        {
            IniControls();

            IniModbusSettingInfo();

            IniTimer();
        }

        /// <summary>
        /// 初始化定时器设置
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void IniTimer()
        {
            this.timer = new System.Timers.Timer();
            this.timer.Interval = 1000;
            this.timer.AutoReset = true;
            this.timer.Elapsed += Timer_Elapsed;
            this.timer.SynchronizingObject = this;//这个一定要加，不然定时器内部方法调用时出现异常会被退掉，无法被try{}catch{}捕捉到
        }


        /// <summary>
        /// 1S定时循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                ushort[] res = this.communicationBLL.ReadHoldingRegisters(1, 0, 5);
                if (res?.Length > 0 && res?.Length == 5)
                {
                    this.Invoke(() =>
                    {
                        this.lab40001.Text = res[0].ToString();
                        this.lab40002.Text = res[1].ToString();
                        this.lab40003.Text = res[2].ToString();
                        this.lab40004.Text = res[3].ToString();
                        this.lab40005.Text = res[4].ToString();
                    });
                }
            }
            catch (Exception ex)
            {
                this.timer.Stop();
                this.IsOpenPort = false;
                MessageBox.Show("4区数据读取出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 初始化Modbus通讯的配置信息
        /// </summary>
        private void IniModbusSettingInfo()
        {
            this.modbusRtuInfo = new ModbusRtuInfo()
            {
                PortName = "COM19",
                BaudRate = 9600,
                Parity = System.IO.Ports.Parity.None,
                DataBit = 8,
                StopBits = System.IO.Ports.StopBits.One
            };
        }

        /// <summary>
        /// 初始化UI控件
        /// </summary>
        private void IniControls()
        {
            foreach (var lab in this.gbModbusPoll.Controls.OfType<Label>())
            {
                if (lab.Name.Contains("4000"))
                {
                    lab.Text = "";
                }
            }
        }

        private CommunicationBLL communicationBLL = new CommunicationBLL();
        private ModbusRtuInfo modbusRtuInfo;
        private System.Timers.Timer timer;

        private bool _isOpenPort = false;

        public bool IsOpenPort
        {
            get { return _isOpenPort; }
            set
            {
                if (value)
                {
                    this.btnOpenSerialPort.Text = "数据采集中";
                    this.btnOpenSerialPort.BackColor = Color.Green;
                }
                else
                {
                    this.btnOpenSerialPort.Text = "打开串口";
                    this.btnOpenSerialPort.BackColor = SystemColors.Control;
                    this.IniControls();
                }
                _isOpenPort = value;
            }
        }



        private void btnOpenSerialPort_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsOpenPort)
                {
                    this.IsOpenPort = this.communicationBLL.Open(this.modbusRtuInfo);

                    if (this.IsOpenPort)
                        this.timer.Start();
                    else MessageBox.Show("串口打开失败");
                    return;
                }
                else
                {
                    //这里关闭窗口，关闭定时器
                    this.timer.Stop();
                    this.communicationBLL.Close();
                    this.IsOpenPort = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("串口打开错误：" + ex.Message);
            }
        }
    }
}
