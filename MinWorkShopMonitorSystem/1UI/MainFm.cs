using MinWorkShopMonitorSystem.AppService;
using MinWorkShopMonitorSystem.BLL;
using MinWorkShopMonitorSystem.Model;
using MinWorkShopMonitorSystem.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinWorkShopMonitorSystem.UI
{
    public partial class MainFm : Form
    {
        public MainFm()
        {
            InitializeComponent();

            this.Load += MainFm_Load;
        }

        #region 字段属性
        private bool _isMonitor = false;

        public bool IsMonitor
        {
            get { return _isMonitor; }
            set
            {
                this.SwitchGbCommunicationControls(value);
                _isMonitor = value;
            }
        }

        private bool _isOpen = false;

        public bool IsOpen
        {
            get { return _isOpen; ; }
            set
            {
                SwitchBtnStstus(value);
                _isOpen = value;
            }
        }
        #endregion

        #region 委托事件
        private delegate void DataReadChangeDlg(SlaveInfo slaveInfo, ushort[] ushorts);
        /// <summary>
        /// 自定义委托事件
        /// </summary>
        private event DataReadChangeDlg OnDataReadChanged;

        private delegate void DGVDataChangDlg(int slaveId, int address, double ushorts);
        /// <summary>
        /// 自定义委托事件
        /// </summary>
        private event DGVDataChangDlg OnDGVDataChang;
        #endregion

        #region 变量
        private System.Timers.Timer MonitorTimer = null;
        private MainBLL mainBll = new MainBLL();
        private ModbusRtuPollBLL modbusRtuPoll = null;
        /// <summary>
        /// modbus通讯文件路径
        /// </summary>
        private string modbusRtuConfigPath = "";
        /// <summary>
        /// 从配置文件读取到的通讯参数对象
        /// </summary>
        private ModbusRtuInfo modbusRtuInfo = null;
        /// <summary>
        /// 参数取值范围文件路径
        /// </summary>
        private string modbusRangeConfigPath = "";
        private string slaveDeviceInfoPath;
        private string slaveInfoPath;
        /// <summary>
        /// 波特率取值范围集合
        /// </summary>
        private List<string> baudRateRange;
        /// <summary>
        /// 数据位取值范围集合
        /// </summary>
        private List<string> dataBitsRange;
        /// <summary>
        /// 配置文件中设定的设备信息集合
        /// </summary>
        private List<SlaveDeviceInfo> slaveDeviceInfos = null;
        private BindingList<DgvDeviceInfo> dgvDeviceInfos;
        private List<SlaveInfo> slaveInfos;
        #endregion

        private void MainFm_Load(object? sender, EventArgs e)
        {
            try
            {
                //控件默认状态初始化
                DefaultUIControls();
                this.IsMonitor = false;
                this.IsOpen = false;

                //定时器初始化
                IniTimer();

                //加载文件路径信息
                LoadAppConfigInfo();

                //加载配置文件
                LoadParamINIConfig();
                LoadModbusRtuJsonConfig();
                LoadSlaveDeviceInfos();
                LoadSlaveInfos();

                //控件初始数据绑定
                IniUIControlsBindingData();
                IniGbCommunicationInfo();
            }
            catch (Exception ex)
            {
                this.LogMonitorInfo($"程序启动错误，请根据错误信息反馈至管理员!：{ex.Message}");
                Log.Error("程序启动出错", ex);
                MessageBox.Show($"初始化错误，请重启程序！\r\n{ex.Message}");
            }
        }

        #region 私有方法
        private void LoadSlaveInfos()
        {
            this.slaveInfos = this.mainBll.GetJsonConfigTolist<SlaveInfo>(this.slaveInfoPath);
        }

        /// <summary>
        /// 从配置文件中加载从站设备的设定信息（从站ID，PLC地址，参数限值设定）
        /// </summary>
        private void LoadSlaveDeviceInfos()
        {
            this.slaveDeviceInfos = this.mainBll.GetJsonConfigTolist<SlaveDeviceInfo>(this.slaveDeviceInfoPath);

            IniDGVDeviceInfos();
        }

        private void IniDGVDeviceInfos()
        {
            try
            {
                if (this.slaveDeviceInfos?.Count() > 0)
                {
                    dgvDeviceInfos = new BindingList<DgvDeviceInfo>();
                    foreach (var info in this.slaveDeviceInfos)
                    {
                        dgvDeviceInfos.Add(new DgvDeviceInfo()
                        {
                            SlaveDeviceID = info.SlaveDeviceID,
                            Address = info.Address,
                            DataName = info.DataName,
                            Unit = info.Unit,
                            High = info.High,
                            Low = info.Low,
                            CurrentValue = 0
                        });
                    }

                    this.OnDGVDataChang += MainFm_OnDGVDataChang;
                    dgvDatas.CellFormatting += DgvDatas_CellFormatting; 
                    this.slaveDeviceInfos.Clear();
                }
                this.dgvDatas.DataSource = this.dgvDeviceInfos;
            }
            catch (Exception ex)
            {
                dgvDatas.CellFormatting -= DgvDatas_CellFormatting;
                this.OnDGVDataChang -= MainFm_OnDGVDataChang;
                Log.Error("表格初始化失败", ex);
            }
        }

        private void DgvDatas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDatas.Columns[e.ColumnIndex].Name == "IsAlarm")
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvDatas.Rows.Count)
                    return;
                var device = dgvDatas.Rows[e.RowIndex].DataBoundItem as DgvDeviceInfo;
                if (device == null) return;

                // 只有DGV自己绘制时才变色，效率提升100倍
                e.CellStyle.BackColor = device.IsAlarm ? Color.Red : Color.White;
            }
        }

        /// <summary>
        /// 定时器初始化
        /// </summary>
        private void IniTimer()
        {
            this.MonitorTimer = new System.Timers.Timer();
            this.MonitorTimer.Interval = 1000;
            this.MonitorTimer.AutoReset = true;
            this.MonitorTimer.Elapsed += MonitorTimer_Elapsed;
        }

        /// <summary>
        /// 从AppConfig中获取个配置文件的存放路径
        /// </summary>
        private void LoadAppConfigInfo()
        {
            this.modbusRtuConfigPath = AppConfigManager.GetAbsolutePath("JsonFilePath");
            this.modbusRangeConfigPath = AppConfigManager.GetAbsolutePath("INIFilePath");
            this.slaveDeviceInfoPath = AppConfigManager.GetAbsolutePath("SlaveDeviceInfoPath");
            this.slaveInfoPath = AppConfigManager.GetAbsolutePath("SlaveInfoPath");
        }

        /// <summary>
        /// 将通信对象的数据加载到对应的combobox
        /// </summary>
        private void IniGbCommunicationInfo()
        {
            if (this.modbusRtuInfo != null)
            {
                this.cbPortName.Text = this.modbusRtuInfo.PortName;
                this.cbBaudRate.Text = this.modbusRtuInfo.BaudRate.ToString();
                this.cbParity.Text = this.modbusRtuInfo.Parity;
                this.cbDataBits.Text = this.modbusRtuInfo.DataBits.ToString();
                this.cbStopBits.Text = this.modbusRtuInfo.StopBits;
            }
        }

        /// <summary>
        /// 读取配置文件中的ModbusRtu通讯信息
        /// </summary>
        private void LoadModbusRtuJsonConfig()
        {
            this.modbusRtuInfo = this.mainBll.GetJsonConfig<ModbusRtuInfo>(this.modbusRtuConfigPath);
        }

        /// <summary>
        /// 控件初始数据绑定
        /// </summary>
        private void IniUIControlsBindingData()
        {
            BindingPort();
            BindingBaudRate();
            BindingPrity();
            BindingDataBits();
            BindingStopBits();
        }

        /// <summary>
        /// 绑定停止位下拉框数据
        /// </summary>
        private void BindingStopBits()
        {
            var enumList = Enum.GetValues(typeof(StopBits)).Cast<StopBits>().Select(s => new EnumItem() { EnumName = s.ToString(), EnumNumber = (int)s }).ToList();

            this.cbStopBits.DataSource = enumList;
            this.cbStopBits.DisplayMember = "EnumName";
            this.cbStopBits.ValueMember = "EnumNumber";
            this.cbStopBits.SelectedIndex = 0;

            Log.Info("停止位数据绑定成功");
        }

        /// <summary>
        /// 绑定数据位下拉框数据
        /// </summary>
        private void BindingDataBits()
        {
            if (this.dataBitsRange?.Count() > 0)
            {
                this.cbDataBits.DataSource = this.dataBitsRange;
            }
            else
            {
                Log.Warn("数据位初始数据加载失败，请检查相关文件信息");
                MessageBox.Show("数据位初始数据加载失败，请检查相关文件信息", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 绑定校验位下拉框数据
        /// </summary>
        private void BindingPrity()
        {
            var enumList = Enum.GetValues(typeof(Parity)).Cast<Parity>().Select(p => new EnumItem() { EnumName = p.ToString(), EnumNumber = (int)p }).ToList();

            this.cbParity.DataSource = enumList;
            this.cbParity.DisplayMember = "EnumName";
            this.cbParity.ValueMember = "EnumNumber";
            this.cbParity.SelectedIndex = 0;

            Log.Info("校验位数据绑定成功");
        }

        /// <summary>
        /// 绑定波特率下拉框数据
        /// </summary>
        private void BindingBaudRate()
        {
            if (this.baudRateRange?.Count() > 0)
            {
                this.cbBaudRate.DataSource = this.baudRateRange;
            }
            else
            {
                Log.Warn("波特率初始数据加载失败，请检查相关文件信息");
                MessageBox.Show("波特率初始数据加载失败，请检查相关文件信息", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 串口号下拉框绑定
        /// </summary>
        private void BindingPort()
        {
            string[] ports = this.mainBll.GetPortNames();
            if (ports?.Count() > 0)
            {
                this.cbPortName.DataSource = ports;
                this.cbPortName.SelectedIndex = 0;
            }
            else
            {
                Log.Warn("本机串口号加载为空");
                MessageBox.Show("本机串口号加载为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 从INI文件中读取Modbus各参数的取值范围，用于后续文本框绑定。
        /// </summary>
        private void LoadParamINIConfig()
        {
            if (string.IsNullOrWhiteSpace(this.modbusRangeConfigPath)) throw new ArgumentNullException(nameof(this.modbusRangeConfigPath), "文件路径不正确，加载失败");
            this.baudRateRange = this.mainBll.IniReadRangeInfo(this.modbusRangeConfigPath, "BaudRate", "Range");
            this.dataBitsRange = this.mainBll.IniReadRangeInfo(this.modbusRangeConfigPath, "DataBits", "Range");
        }

        /// <summary>
        /// 重置UI控件
        /// </summary>
        private void DefaultUIControls()
        {
            //重置通讯面板控件信息
            DefaultCommunicationPanel();
            //重置运行日志面板控件信息
            DefaultLogPanel();
            //重置数据监控面板控件信息
            DefaultDatasPanel();
        }

        /// <summary>
        /// 重置数据面板控件信息
        /// </summary>
        private void DefaultDatasPanel()
        {
            this.dgvDatas.DataSource = null;
            this.dgvDatas.AutoGenerateColumns = true;//自动添加列
            this.dgvDatas.BackgroundColor = Color.White;
        }

        /// <summary>
        /// 重置日志面板控件信息
        /// </summary>
        private void DefaultLogPanel()
        {
            this.lbLoginfos.HorizontalScrollbar = true;
            this.lbLoginfos.IntegralHeight = false;
            this.lbLoginfos.Items.Clear();
        }

        /// <summary>
        /// 重置通讯面板控件信息
        /// </summary>
        private void DefaultCommunicationPanel()
        {
            foreach (ComboBox cb in this.gbCommunication.Controls.OfType<ComboBox>())
            {
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                cb.Items.Clear();
                cb.DataSource = null;
            }
        }

        /// <summary>
        /// 切换通讯相关控件的使能状态
        /// </summary>
        /// <param name="isMonitor">通讯进行中</param>
        private void SwitchGbCommunicationControls(bool isMonitor)
        {
            this.cbPortName.Enabled = !isMonitor;
            this.cbBaudRate.Enabled = !isMonitor;
            this.cbParity.Enabled = !isMonitor;
            this.cbDataBits.Enabled = !isMonitor;
            this.cbStopBits.Enabled = !isMonitor;
            this.btnSetting.Enabled = !isMonitor;

            this.btnMonitorEnable.Text = isMonitor == true ? "Monitoering..." : "MonitoerEnable";

            this.btnMonitorEnable.BackColor = isMonitor == true ? Color.Green : SystemColors.Control;
            this.btnMonitorEnable.ForeColor = isMonitor == true ? Color.White : SystemColors.ControlText;
        }

        private void SwitchBtnStstus(bool isOpen)
        {
            this.btnMonitorEnable.Enabled = isOpen;
            this.btnSetting.BackColor = isOpen ? Color.Green : SystemColors.Control;
            this.btnSetting.ForeColor = isOpen ? Color.White : SystemColors.ControlText;
            this.btnSetting.Text = isOpen ? "IsOpened" : "IsClosed";
        }

        private void LogMonitorInfo(string msg)
        {
            this.lbLoginfos.Items.Insert(0, $"[{DateTime.Now:HH-mm-ss}] {msg}");

            if (this.lbLoginfos.Items.Count > 20)
            {
                this.lbLoginfos.Items.RemoveAt(this.lbLoginfos.Items.Count - 1);
            }
        }
        #endregion

        /// <summary>
        /// 定时读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonitorTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (this.slaveInfos?.Count > 0)
                {
                    foreach (var info in this.slaveInfos)
                    {
                        ushort[] resData = this.modbusRtuPoll.ReadInputRegisters((byte)info.SlaveID, (byte)info.StartAddress, (byte)info.Quantity);

                        OnDataReadChanged?.Invoke(info, resData);
                    }
                }
                else
                {
                    this.MonitorTimer.Stop();
                    this.IsMonitor = false;
                    LogMonitorInfo("从站点位未正确配置，无法读取，将停止数据采集");
                    Log.Warn("从站点位未正确配置，无法读取，将停止数据采集");
                }
            }
            catch (Exception ex)
            {
                this.MonitorTimer.Stop();
                this.Invoke(() =>
                {
                    this.IsMonitor = false;
                    LogMonitorInfo("定时器采集数据错误" + ex.Message);
                });
                Log.Error("定时器采集数据错误", ex);
                Debug.WriteLine("定时器采集数据错误");
            }
        }

        private void TestVoid()
        {
            try
            {
                if (this.slaveInfos?.Count > 0)
                {
                    foreach (var info in this.slaveInfos)
                    {
                        ushort[] resData = this.modbusRtuPoll.ReadInputRegisters((byte)info.SlaveID, (byte)info.StartAddress, (byte)info.Quantity);

                        OnDataReadChanged?.Invoke(info, resData);
                    }
                }
                else
                {
                    Log.Warn("从站点位未正确配置，无法读取，将停止数据采集");
                    LogMonitorInfo("从站点位未正确配置，无法读取，将停止数据采集");
                    this.MonitorTimer.Stop();
                    this.IsMonitor = false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("定时器采集数据错误", ex);
                LogMonitorInfo("定时器采集数据错误" + ex.Message);
                this.MonitorTimer.Stop();
                this.IsMonitor = false;
            }
        }

        private void btnMonitorEnable_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsMonitor)
                {
                    this.MonitorTimer.Stop();
                    this.IsMonitor = false;
                    this.OnDataReadChanged -= MainFm_OnDataChanged;
                    this.LogMonitorInfo($"数据监控{this.IsMonitor}");
                    return;
                }
                else
                {
                    this.OnDataReadChanged += MainFm_OnDataChanged;
                    //开始定时读取
                    this.MonitorTimer.Start();
                    //TestVoid();
                    this.IsMonitor = true;
                    this.LogMonitorInfo($"数据监控{this.IsMonitor}");
                }
            }
            catch (Exception ex)
            {
                this.OnDataReadChanged -= MainFm_OnDataChanged;
                this.LogMonitorInfo($"数据监控开启失败:{ex.Message}");
                Log.Error("数据采集异常", ex);
                MessageBox.Show("数据采集启动异常，请检查通讯参数", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// 处理从站中读取到的数据
        /// </summary>
        /// <param name="slaveInfo"></param>
        /// <param name="ushorts"></param>
        private void MainFm_OnDataChanged(SlaveInfo slaveInfo, ushort[] ushorts)
        {
            try
            {
                if (slaveInfo == null || ushorts?.Count() <= 0)
                {
                    Log.Warn("采集的数据为空，无法解析");
                    LogMonitorInfo("采集的数据为空，无法解析");
                    return;
                }

                int slaveId = slaveInfo.SlaveID;
                int quantity = slaveInfo.Quantity;
                int startAddress = slaveInfo.StartAddress;

                if (quantity != ushorts.Length)
                {
                    Log.Warn("设置和采集数据不匹配，无法解析");
                    LogMonitorInfo("设置和采集数据不匹配，无法解析");
                    return;
                }

                for (int i = startAddress; i < quantity; i++)
                {
                    OnDGVDataChang?.Invoke(slaveId, (ushort)i, ushorts[i] / 10.0);
                }
            }
            catch (Exception ex)
            {
                this.OnDataReadChanged -= MainFm_OnDataChanged;
                this.LogMonitorInfo($"从站数据处理异常:{ex.Message}");
                Log.Error("从站数据处理异常", ex);
            }
        }

        /// <summary>
        /// 刷新DGV数据显示
        /// </summary>
        /// <param name="slaveId"></param>
        /// <param name="address"></param>
        /// <param name="ushorts"></param>
        //private void MainFm_OnDGVDataChang(int slaveId, int address, double ushorts)
        //{
        //    try
        //    {
        //        for (int i = 0; i < this.dgvDatas.Rows.Count; i++)
        //        {
        //            DgvDeviceInfo dgvDeviceInfo = (DgvDeviceInfo)this.dgvDatas.Rows[i].DataBoundItem;
        //            if (dgvDeviceInfo != null)
        //            {
        //                if (dgvDeviceInfo.SlaveDeviceID == slaveId && dgvDeviceInfo.Address == (address + 40001))
        //                {
        //                    dgvDeviceInfo.CurrentValue = ushorts;
        //                }
        //                this.dgvDatas.Rows[i].Cells["IsAlarm"].Style.BackColor = dgvDeviceInfo.IsAlarm ? Color.Red : Color.White;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("DGV数据处理异常", ex);
        //        this.LogMonitorInfo($"DGV数据处理异常:{ex.Message}");
        //        this.OnDGVDataChang -= MainFm_OnDGVDataChang;
        //    }
        //}

        private void MainFm_OnDGVDataChang(int slaveId, int address, double ushorts)
        {
            if (this.dgvDatas.InvokeRequired)
            {
                this.dgvDatas.Invoke(() =>
                {
                    MainFm_OnDGVDataChang(slaveId, address, ushorts);
                });
                return;
            }

            try
            {
                foreach (DgvDeviceInfo info in this.dgvDatas.DataSource as BindingList<DgvDeviceInfo>)
                {
                    if (info.SlaveDeviceID == slaveId && info.Address == (address + 40001))
                    {
                        info.CurrentValue = ushorts;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("DGV数据处理异常", ex);
                this.LogMonitorInfo($"DGV数据处理异常:{ex.Message}");
                this.OnDGVDataChang -= MainFm_OnDGVDataChang;
            }
        }



        private void btnSetting_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsOpen)
                {
                    //停止计时
                    this.MonitorTimer.Stop();
                    //关闭串口
                    this.modbusRtuPoll.Close();
                    this.IsOpen = false;
                    this.LogMonitorInfo($"通讯连接{this.IsOpen}");
                    return;
                }
                else
                {
                    string portName = this.cbPortName.Text.Trim();
                    int baudRate = int.Parse(this.cbBaudRate.Text.Trim());
                    Parity parity = (Parity)Enum.Parse(typeof(Parity), this.cbParity.Text.Trim());
                    int dataBits = int.Parse(this.cbDataBits.Text.Trim());
                    StopBits stopBits = (StopBits)Enum.Parse(typeof(StopBits), this.cbStopBits.Text.Trim());

                    if (string.IsNullOrWhiteSpace(portName))
                    {
                        MessageBox.Show("串口号不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.cbPortName.Focus();
                        return;
                    }
                    if (baudRate < 1200)
                    {
                        MessageBox.Show("波特率最小设置为1200", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.cbBaudRate.Focus();
                        return;
                    }
                    if (dataBits <= 0)
                    {
                        MessageBox.Show("数据位设置不正确", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.cbDataBits.Focus();
                        return;
                    }
                    if (!Enum.IsDefined<Parity>(parity))
                    {
                        MessageBox.Show("校验位设置不正确", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.cbParity.Focus();
                        return;
                    }
                    if (!Enum.IsDefined<StopBits>(stopBits))
                    {
                        MessageBox.Show("停止位设置不正确", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.cbStopBits.Focus();
                        return;
                    }

                    if (this.modbusRtuInfo != null)
                    {
                        if (this.modbusRtuInfo.PortName == portName && this.modbusRtuInfo.BaudRate == baudRate && this.modbusRtuInfo.Parity == parity.ToString() && this.modbusRtuInfo.DataBits == dataBits && this.modbusRtuInfo.StopBits == stopBits.ToString())
                        {
                            MessageBox.Show("设定参数与当前活动配置相同，准备打开串口!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    this.modbusRtuInfo = new ModbusRtuInfo()
                    {
                        PortName = portName,
                        BaudRate = baudRate,
                        Parity = parity.ToString(),
                        DataBits = dataBits,
                        StopBits = stopBits.ToString()
                    };

                    this.modbusRtuPoll = ModbusRtuController.GetModbusRtuPoll(this.modbusRtuInfo);
                    this.IsOpen = this.modbusRtuPoll.Open();
                    this.LogMonitorInfo($"通讯连接{this.IsOpen}");
                    Log.Info($"串口打开{this.IsOpen}!PortName={portName},BaudRate={baudRate},Parity={parity},DataBits={dataBits},StopBits={stopBits}");

                    //保存写入配置文件
                    this.mainBll.SaveJsonConfig<ModbusRtuInfo>(this.modbusRtuConfigPath, this.modbusRtuInfo);
                    this.LogMonitorInfo($"通讯参数保存成功!PortName={portName},BaudRate={baudRate},Parity={parity},DataBits={dataBits},StopBits={stopBits}");
                    Log.Info($"通讯参数保存成功!PortName={portName},BaudRate={baudRate},Parity={parity},DataBits={dataBits},StopBits={stopBits}");
                    return;
                }
            }
            catch (Exception ex)
            {
                this.LogMonitorInfo($"通讯连接错误：{ex.Message}");
                Log.Error("通讯参数保存失败", ex);
                MessageBox.Show("通讯参数保存失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
