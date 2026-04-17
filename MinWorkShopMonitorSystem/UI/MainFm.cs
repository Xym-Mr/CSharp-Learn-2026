using MinWorkShopMonitorSystem.BLL;
using MinWorkShopMonitorSystem.Model;
using MinWorkShopMonitorSystem.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        #region 变量
        private MainBLL mainBll = new MainBLL();
        /// <summary>
        /// modbus通讯文件路径
        /// </summary>
        private string modbusRtuConfigPath = "";
        /// <summary>
        /// 参数取值范围文件路径
        /// </summary>
        private string modbusRangeConfigPath = "";
        /// <summary>
        /// modbus对象
        /// </summary>
        private ModbusRtuInfo modbusRtuInfo = null;
        /// <summary>
        /// 波特率取值范围集合
        /// </summary>
        private List<string> baudRateRange;
        /// <summary>
        /// 校验位取值范围集合
        /// </summary>
        private List<string> parityRange;
        /// <summary>
        /// 数据位取值范围集合
        /// </summary>
        private List<string> dataBitsRange;
        /// <summary>
        /// 停止位取值范围集合
        /// </summary>
        private List<string> stopBitsRange;
        #endregion

        private void MainFm_Load(object? sender, EventArgs e)
        {
            try
            {
                //控件默认状态初始化
                DefaultUIControls();

                //加载文件路径信息
                LoadAppConfigInfo();

                //加载配置文件
                LoadParamINIConfig();
                LoadModbusRtuJsonConfig();

                //控件初始数据绑定
                IniUIControlsBindingData();
                IniGbCommunicationInfo();
            }
            catch (Exception ex)
            {
                Log.Error("程序启动出错", ex);
                MessageBox.Show($"初始化错误，请重启程序！\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// 从AppConfig中获取个配置文件的存放路径
        /// </summary>
        private void LoadAppConfigInfo()
        {
            this.modbusRtuConfigPath = AppConfigManager.GetAbsolutePath("JsonFilePath");
            this.modbusRangeConfigPath = AppConfigManager.GetAbsolutePath("INIFilePath");
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
                this.cbStopBits.Text = this.modbusRtuInfo.StopBits.ToString();
            }
        }

        /// <summary>
        /// 读取配置文件中的ModbusRtu通讯信息
        /// </summary>
        private void LoadModbusRtuJsonConfig()
        {
            modbusRtuInfo = this.mainBll.GetJsonConfig<ModbusRtuInfo>(this.modbusRtuConfigPath);
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
            if (this.stopBitsRange?.Count() > 0)
            {
                this.cbStopBits.DataSource = this.stopBitsRange;
            }
            else
            {
                Log.Warn("停止位初始数据加载失败，请检查相关文件信息");
                MessageBox.Show("停止位初始数据加载失败，请检查相关文件信息", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            if (this.parityRange?.Count() > 0)
            {
                this.cbParity.DataSource = this.parityRange;
            }
            else
            {
                Log.Warn("校验位初始数据加载失败，请检查相关文件信息");
                MessageBox.Show("校验位初始数据加载失败，请检查相关文件信息", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 绑定波特率下拉框数据
        /// </summary>
        private void BindingBaudRate()
        {
            if (this.baudRateRange?.Count() > 0)
            {
                this.cbBaudRate.DataSource= this.baudRateRange;
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
            this.parityRange = this.mainBll.IniReadRangeInfo(this.modbusRangeConfigPath, "Parity", "Range");
            this.dataBitsRange = this.mainBll.IniReadRangeInfo(this.modbusRangeConfigPath, "DataBits", "Range");
            this.stopBitsRange = this.mainBll.IniReadRangeInfo(this.modbusRangeConfigPath, "StopBits", "Range");
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
            this.tbLogInfos.Clear();
        }

        /// <summary>
        /// 重置通讯面板控件信息
        /// </summary>
        private void DefaultCommunicationPanel()
        {
            foreach (ComboBox cb in this.gbCommunication.Controls.OfType<ComboBox>())
            {
                cb.Items.Clear();
                cb.DataSource = null;
            }

        }
    }
}
