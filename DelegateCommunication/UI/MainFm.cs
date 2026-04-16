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


            dataMonitor.OnPropertyChanged += CheckDatas;

            Task.Run(() =>
            {
                while (true)
                {
                    Random random = new Random();
                    dataMonitor.Humidity = random.Next(0, 600) / 10.0;
                    dataMonitor.Temperature = random.Next(50, 400) / 10.0;


                    Debug.WriteLine($"湿度值{dataMonitor.Humidity}");
                    Debug.WriteLine($"温度值{dataMonitor.Temperature}");

                    Thread.Sleep(500);
                }
            });

            IniAndMonitorFields();
        }

        private List<ObservableMonitor<double>> Sensors;

        private void IniAndMonitorFields()
        {
            IniSensors();

            BindingSensors();

            StartMonitor();
        }

        /// <summary>
        /// 模拟数据采集，采集到的数据赋值给对应的属性-->Monitor类中的SetField()方法-->Observable基类中更新对应的属性值并发布接口中的事件PropertyChanged-->执行monitor类中重新的OnPropertyChanged方法-->发布monitor类中StatusChanged事件-->main类中的Sensor_StatusChanged（）；
        /// </summary>
        private void StartMonitor()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    foreach (var sensor in Sensors)
                    {
                        // 模拟数据波动（±10%范围内随机变化）
                        var variation = (sensor.CurrentValue * 0.1) * (new Random().NextDouble() - 0.5) * 2;
                        var newValue = sensor.CurrentValue + variation;

                        // 确保新值在合理范围内（避免极端值）
                        newValue = Math.Max(sensor.MinLimit * 0.8, Math.Min(sensor.MaxLimit * 1.2, newValue));

                        sensor.CurrentValue = newValue;//这里给属性赋值
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        private void BindingSensors()
        {
            foreach (var sensor in Sensors)
            {
                sensor.StatusChanged += Sensor_StatusChanged;//为每一个数据对象的StatusChanged事件关联方法
            }
        }

        private void IniSensors()
        {
            // 初始化数据，配置初始值、上下限值、名称、单位
            Sensors = new List<ObservableMonitor<double>>
            {
                new ObservableMonitor<double>(25.0, 18.0, 28.0, "车间温度", "°C"),
                new ObservableMonitor<double>(3.5, 3.0, 4.5, "电源电压", "V"),
                new ObservableMonitor<double>(80, 70, 95, "电机转速", "RPM"),
                new ObservableMonitor<double>(0.5, 0.1, 1.0, "液位高度", "m"),
                // ... 996 个类似配置，来自 JSON 配置文件或数据库
            };
        }

        private void Sensor_StatusChanged(object? sender, StatusChangedEventArgs e)
        {
            var s = sender as ObservableMonitor<double>;
            Debug.WriteLine($"{s.DisplayName}: {e.Status} ({s.CurrentValue}{s.Unit})");
        }

        private DataMonitor dataMonitor = new DataMonitor();

        private void CheckDatas(string propertyName, double h, int status)
        {
            Debug.WriteLine($"属性名{propertyName}，状态值：{status}");
            string msg = "";
            if (status > 0)
            {
                msg = $"{propertyName}超上限，{propertyName}值为{h}";
            }
            else if (status < 0)
            {
                msg = $"{propertyName}低下限，{propertyName}值为{h}";
            }
            else
            {
                msg = $"{propertyName}正常，{propertyName}值为{h}";
            }

            Debug.WriteLine(msg);

            this.Invoke(() =>
                {
                    this.labSatus.Text = msg;
                });

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
