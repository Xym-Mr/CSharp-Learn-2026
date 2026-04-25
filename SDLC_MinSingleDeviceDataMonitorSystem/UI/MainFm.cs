using SDLC_MinSingleDeviceDataMonitorSystem.BLL;
using SDLC_MinSingleDeviceDataMonitorSystem.Common;
using SDLC_MinSingleDeviceDataMonitorSystem.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;

namespace SDLC_MinSingleDeviceDataMonitorSystem._1UI
{
    public partial class MainFm : Form
    {
        public MainFm()
        {
            InitializeComponent();
            this.Load += MainFm_Load;
            this.btnOpen.Click += BtnOpen_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnMonitor.Click += BtnMonitor_Click;

        }

        private ConfigManager _configManager = new ConfigManager();
        private ScheduleManager _scheduleManager;
        private ConfigInfo _configInfo = null;
        private string _configPath;

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                this.btnSave.Enabled = !value;
                this.btnOpen.Enabled = !value;

                this.btnMonitor.BackColor = value == true ? Color.Green : SystemColors.Control;
                this.btnMonitor.ForeColor = value == true ? Color.White : SystemColors.ControlText;
                _isRunning = value;
            }
        }

        private bool _isOpen;

        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                this.btnSave.Enabled = !value;
                this.btnMonitor.Enabled = value;

                this.btnOpen.BackColor = value == true ? Color.Green : SystemColors.Control;
                this.btnOpen.ForeColor = value == true ? Color.White : SystemColors.ControlText;
                _isOpen = value;
            }
        }

        private void MainFm_Load(object? sender, EventArgs e)
        {
            try
            {
                //下拉框绑定
                BindingConfig();

                //获取app.config
                this._configPath = this._configManager.GetAbsolutePath("JsonConfigPath");

                //加载旧配置信息
                this._configInfo = this._configManager.LoadConfig<ConfigInfo>(this._configPath);
                //不存在则默认值
                if (this._configInfo == null) this._configInfo = this._configManager.GetDefaultConfig();

                //配置信息加载到对应控件
                UpdateConfigControlInfo(this._configInfo);

                this._scheduleManager = new ScheduleManager(this._configInfo.ReadInterval, this._configInfo.SlaveId, (byte)this._configInfo.StartAddress, this._configInfo.RegisterCount);

                this._scheduleManager.OnReceiveData += new Action<byte, List<ushort>>(ReceiveDataChanged);
                this._scheduleManager.OnReceiveError += new Action<string>(ReceiveError);

                this.IsOpen = false;
                this.IsRunning = false;
            }
            catch (Exception ex)
            {
                ExceptionHandle.ShowMessage(ex.Message);
            }
        }

        private void UpdateConfigControlInfo(ConfigInfo configInfo)
        {
            this.cbPortName.Text = configInfo.PortName;
            this.cbBaudRate.Text = configInfo.BaudRate.ToString();
            this.cbParity.Text = configInfo.Parity.ToString();
            this.cbDataBits.Text = configInfo.DataBit.ToString();
            this.cbStopBits.Text = configInfo.StopBit.ToString();

            this.tbSlaveID.Text = configInfo.SlaveId.ToString();
            this.tbStartAddress.Text = configInfo.StartAddress.ToString();
            this.tbQuantity.Text = configInfo.RegisterCount.ToString();
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                //获取UI输入
                string portName = this.cbPortName.Text;
                int baudRate = int.Parse(this.cbBaudRate.Text);
                Parity parity = (Parity)Enum.Parse(typeof(Parity), this.cbParity.Text.Trim());
                int dataBits = int.Parse(this.cbDataBits.Text);
                StopBits stopBits = (StopBits)Enum.Parse(typeof(StopBits), this.cbStopBits.Text.Trim());

                byte slaveId = Convert.ToByte(this.tbSlaveID.Text);
                ushort startAddress = Convert.ToUInt16(this.tbStartAddress.Text);
                ushort quantity = Convert.ToUInt16(this.tbQuantity.Text);

                //验证输入有效性
                if (string.IsNullOrWhiteSpace(portName)) throw new ArgumentNullException(nameof(portName), "串口输入为空");
                if (baudRate < 1200) throw new ArgumentOutOfRangeException(nameof(baudRate), baudRate, "不是有效的波特率");
                if (!Enum.IsDefined(typeof(Parity), parity)) throw new ArgumentOutOfRangeException(nameof(parity), parity, "不是有效的校验位");
                if (!Enum.IsDefined(typeof(StopBits), stopBits)) throw new ArgumentOutOfRangeException(nameof(stopBits), stopBits, "不是有效的停止位");
                if (!(quantity > 0)) throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "读取数量设置要大于0");

                //model
                this._configInfo = new ConfigInfo()
                {
                    PortName = portName,
                    BaudRate = baudRate,
                    Parity = parity.ToString(),
                    DataBit = dataBits,
                    StopBit = (int)stopBits,
                    SlaveId = slaveId,
                    StartAddress = startAddress,
                    RegisterCount = quantity,
                    ReadInterval = 1500
                };

                this._configManager.SaveConfig<ConfigInfo>(this._configPath, this._configInfo);
                ExceptionHandle.ShowMessage("配置文件保存成功", ExceptionHandle.InfoType.Success);
            }
            catch (Exception ex)
            {
                ExceptionHandle.ShowMessage(ex.Message);
            }
        }

        private void BtnOpen_Click(object? sender, EventArgs e)
        {
            try
            {
                if (this._scheduleManager.IsOpen())
                {
                    this._scheduleManager.Close();
                    this.IsOpen = this._scheduleManager.IsOpen();
                    return;
                }

                this._scheduleManager.Open(this._configInfo);
                this.IsOpen = this._scheduleManager.IsOpen();
            }
            catch (Exception ex)
            {
                ExceptionHandle.ShowMessage(ex.Message);
            }
        }

        private void BtnMonitor_Click(object? sender, EventArgs e)
        {
            if (this._scheduleManager.IsRunning())
            {
                this._scheduleManager.StopMonitor();
                this.IsRunning = this._scheduleManager.IsRunning();
                return;
            }
            this._scheduleManager.StartMonitor();
            this.IsRunning = this._scheduleManager.IsRunning();
        }



        /// <summary>
        /// 读取到数据值事件方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="datas"></param>
        private void ReceiveDataChanged(byte id, List<ushort> datas)
        {
            var copyDatas = new List<ushort>(datas);//完全复制参数，不直接使用datas，避免与其他线程竞争datas引用，保证线程安全。
            try
            {
                int startAddress = this._configInfo.StartAddress;
                int quantity = this._configInfo.RegisterCount;
                List<DGVDataInfo> list = new List<DGVDataInfo>();//list是方法内部的局部变量，当方法高频使用时，会频繁创建实例对象，存在内存波动，但是list的引用是this.dgvDataMonitor.DataSource = list;，因为 this.dgvDataMonitor.DataSource = null;接触了旧对象的引用，所以就对象会被自动回收不会造成内存泄漏。

                if (quantity == copyDatas.Count)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        list.Add(new DGVDataInfo()
                        {
                            SlaveId = id,
                            RegisterAddress = startAddress + 40001 + i,
                            CurValue = copyDatas[i] / 10.0
                        });
                    }
                }

                this.Invoke(() =>
                {
                    this.dgvDataMonitor.DataSource = null;
                    this.dgvDataMonitor.DataSource = list;
                });
            }
            catch (Exception ex)
            {
                this._scheduleManager.StopMonitor();
                this._isRunning = this._scheduleManager.IsRunning();
                ExceptionHandle.ShowMessage(ex.Message);
            }
        }



        /// <summary>
        /// 数据读取时发生错误事件方法
        /// </summary>
        /// <param name="errMsg"></param>
        private void ReceiveError(string errMsg)
        {
            this.Invoke(() =>
            {
                this.IsRunning = this._scheduleManager.IsRunning();
            });
            ExceptionHandle.ShowMessage(errMsg);
        }

        /// <summary>
        /// 下拉框初始值绑定
        /// </summary>
        private void BindingConfig()
        {
            this.IniCombobox();
            this.cbPortName.DataSource = SerialPort.GetPortNames().ToList();
            this.cbBaudRate.DataSource = new string[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" };
            this.cbParity.DataSource = new string[] { "None", "Odd", "Even", "Mark", "Space" };
            this.cbDataBits.DataSource = new string[] { "5", "6", "7", "8" };
            this.cbStopBits.DataSource = new int[] { 0, 1, 2, 3 };
        }

        private void IniCombobox()
        {
            foreach (var cb in this.gbConfigSetting.Controls.OfType<ComboBox>())
            {
                cb.Items.Clear();
                cb.DataSource = null;
            }
        }

        private void MainFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._scheduleManager.StopMonitor();
            this._scheduleManager.Close();
            this._configManager.SaveConfig<ConfigInfo>(this._configPath, this._configInfo);
        }
    }
}
