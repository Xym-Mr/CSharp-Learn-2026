using MinFileRW.Model;
using MinFileRW.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace MinFileRW.DAL
{
    /// <summary>
    /// 数据获取和解析
    /// </summary>
    internal class CSVRWDAL
    {
        private BaseCSVRW baseCSVRW = new BaseCSVRW();

        #region CSV基本数据格式
        ///以英文逗号','分割字段
        ///换行符或空格分割行
        ///第一行通常为表头
        ///格式统一
        #endregion

        ///根据需要去解析数据，这里只要解析成行列组合去除逗号即可
        public string CSVRead()
        {
            List<string> datas = baseCSVRW.CSVRead();

            if (datas?.Count == 0) return null;
            else
            {
                string res = "";
                foreach (var data in datas)
                {
                    string[] rows = data.Split(',');

                    if (res.Length > 0)
                    {
                        res += "\r\n";
                    }

                    foreach (var col in rows)
                    {
                        res += col.Trim('"') + "\t";
                    }
                }
                return res;
            }
        }

        public bool CSVWrite(List<CSVData> datas)
        {
            string dataMsg = null;
            if (datas?.Count > 0)
            {
                //将数据集合解析成CSV格式的字符串

                //拼接表头
                if (dataMsg == null)
                {
                    dataMsg = "\"姓名\",\"性别\",\"年龄\",\"城市\",\"职业\",\"入职日期\",\"月薪\"";
                    this.baseCSVRW.CSVWrite(dataMsg);
                }

                foreach (var data in datas)
                {
                    if (dataMsg.Length > 0)
                    {
                        dataMsg = "\r\n";
                        this.baseCSVRW.CSVWrite(dataMsg);
                    }
                    dataMsg = $"\"{data.PName}\",\"{data.Gender}\",\"{data.Age}\",\"{data.City}\",\"{data.ZhiYe}\",\"{data.JoinDate}\",\"{data.XinZi}\"";
                    this.baseCSVRW.CSVWrite(dataMsg);

                }
                dataMsg = null;

                return true;
            }
            else return false;
        }
    }
}
