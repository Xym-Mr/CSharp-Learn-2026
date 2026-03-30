using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinFileRW.Utility
{
    /// <summary>
    /// CSV文件读写
    /// </summary>
    internal class BaseCSVRW
    {
        //"D:\Program Files\Microsoft Visual Studio\2022\ProjectFiles\JIAGOULIANXI\MinJiaGou\MinFileRW\bin\Debug\net8.0-windows\Files\Test.csv"
        private string filePath = Application.StartupPath + "Files\\Test.csv";//文件路径

        public List<string> CSVRead()
        {
            List<string> res = null;
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    res = new List<string>();
                    while (sr.Read() > 0)
                    {
                        res.Add(sr.ReadLine());
                    }
                }
            }
            return res;
        }

        public void CSVWrite(string msg)
        {
            File.AppendAllText(filePath, msg, Encoding.UTF8);
        }


    }
}
