using MinFileRW.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinFileRW.DAL
{
    internal class TextRWDAL
    {
        private BaseTextRW baseTextRW = new BaseTextRW();

        public bool TextWrite(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt)) return false;
            this.baseTextRW.WriteText(txt);
            return true;
        }

        public string TextRead()
        {
            return this.baseTextRW.TxtRead();
        }
    }
}
