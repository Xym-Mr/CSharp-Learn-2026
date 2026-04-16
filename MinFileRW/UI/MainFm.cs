using MinFileRW.Model;
using MinFileRW.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinFileRW.UI
{
    public partial class MainFm : Form
    {
        public MainFm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Load += MainFm_Load;
        }

        private void MainFm_Load(object? sender, EventArgs e)
        {
            //for (int i = 0; i < 20; i++)
            //{
            //    Task.Run(() =>
            //    {
            //        while (true)
            //        {
            //            Log.Info("这是我在线程里面测试的日志信息");

            //            Thread.Sleep(500);

            //            try
            //            {
            //                throw new Exception("这是线程错误测试");
            //            }
            //            catch (Exception ex)
            //            {
            //                Log.Error("线程测试错误", ex);
            //            }
            //        }
            //    });
            //}


            string configPath =Path.Combine( AppContext.BaseDirectory,"configs\\Modbus.config");

            using (var configManager = new TxtConfigManger(configPath))
            {
                try
                {
                    // 写入配置
                    var configData = new Dictionary<string, string>
                    {
                        { "ServerIP", "192.168.1.100" },
                        { "Port", "8080" },
                        { "Timeout", "30" }
                    };
                    configManager.WriteConfig(configData);
                    Debug.WriteLine("配置已写入");

                    // 读取配置
                    var readConfig = configManager.ReadConfig();
                    Debug.WriteLine("\n读取到的配置:");
                    foreach (var item in readConfig)
                    {
                        Debug.WriteLine($"{item.Key}: {item.Value}");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"系统错误: {ex.Message}");
                }
            }
        }



        private void btnTxtWrite_Click(object sender, EventArgs e)
        {

        }

        private void btnTxtRead_Click(object sender, EventArgs e)
        {

        }



        private void btnCSVRead_Click(object sender, EventArgs e)
        {

        }

        private void btnCSVWrite_Click(object sender, EventArgs e)
        {

        }
    }
}
