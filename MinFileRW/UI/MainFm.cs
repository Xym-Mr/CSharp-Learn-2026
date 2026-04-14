using MinFileRW.Model;
using MinFileRW.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            for (int i = 0; i < 20; i++)
            {
            Task.Run(() =>
            {
                while (true)
                {
                    Log.Info("这是我在线程里面测试的日志信息");

                    Thread.Sleep(500);

                    try
                    {
                        throw new Exception("这是线程错误测试");
                    }
                    catch (Exception ex)
                    {
                        Log.Error("线程测试错误", ex);
                    }
                }
            });
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
