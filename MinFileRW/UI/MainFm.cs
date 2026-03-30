using MinFileRW.BLL;
using MinFileRW.Model;
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

        }



        private TextRWBLL textRWBLL = new TextRWBLL();
        private void btnTxtWrite_Click(object sender, EventArgs e)
        {
            string msg = this.tbTxtWriteMsg.Text.Trim();

            if (string.IsNullOrWhiteSpace(msg))
            {
                MessageBox.Show("写入框内容为空", "写入警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.textRWBLL.TextWrite(msg))
            {
                MessageBox.Show("信息写入成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tbTxtWriteMsg.Clear();
                this.tbTxtWriteMsg.Focus();
                return;
            }
            else
            {
                MessageBox.Show("信息写入失败", "写入异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnTxtRead_Click(object sender, EventArgs e)
        {
            this.tbTxtReadMsg.Clear();
            this.tbTxtReadMsg.Text = this.textRWBLL.TextRead();
        }


        private CSVRWBLL csvRWBLL = new CSVRWBLL();
        private void btnCSVRead_Click(object sender, EventArgs e)
        {
            this.tbCSVReadMsg.Clear();
            string res = this.csvRWBLL.CSVRead();
            if (string.IsNullOrWhiteSpace(res))
            {
                MessageBox.Show("内容读取为空", "CSV警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.tbCSVReadMsg.Text = res;
        }

        private void btnCSVWrite_Click(object sender, EventArgs e)
        {
            //约定UI输入时按,分隔开
            string str = this.tbCSVWriteMsg.Text = "李四,女,20,河北,技术调试,2019-10-4,9000";

            this.csvRWBLL.CSVWrite(str);
        }
    }
}
