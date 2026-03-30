using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinFileRW.Utility
{
    /// <summary>
    /// txt文本操作类
    /// </summary>
    internal class BaseTextRW
    {
        private string filePath = Application.StartupPath + "Files\\Test.txt";

        public string TxtRead()
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath, Encoding.UTF8);
            }
            else return null;
        }

        public void WriteText(string text)
        {
            string msg = null;
            if (!File.Exists(filePath)) msg = text;
            else msg = "\r\n" + text;
            File.AppendAllText(filePath, msg, Encoding.UTF8);
        }
    }
}
