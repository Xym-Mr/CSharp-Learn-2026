using MinWorkShopMonitorSystem.BLL;
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

        private MainBLL mainBll = new MainBLL();


        private void MainFm_Load(object? sender, EventArgs e)
        {
            try
            {
                //控件初始化
                DefaultUIControls();

                //加载配置文件
                LoadCommunicationSourse();

                //控件初始数据绑定
                IniUIControlsBindingData();
            }
            catch (Exception ex)
            {
                Log.Error("程序启动出错", ex);
                MessageBox.Show($"初始化错误，请重启程序！\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// 控件初始数据绑定
        /// </summary>
        private void IniUIControlsBindingData()
        {
            BindingPort();
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
        /// 从文件中读取Modbus各参数的取值范围，用于后续文本框绑定。
        /// </summary>
        private void LoadCommunicationSourse()
        {

        }

        /// <summary>
        /// 初始化UI控件
        /// </summary>
        private void DefaultUIControls()
        {
            DefaultCommunicationPanel();
            DefaultLogPanel();
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
