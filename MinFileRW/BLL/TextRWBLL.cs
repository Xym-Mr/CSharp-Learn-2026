using MinFileRW.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinFileRW.BLL
{
    internal class TextRWBLL
    {
        private TextRWDAL textRWDAL = new TextRWDAL();

        public string TextRead()
        {
            return this.textRWDAL.TextRead();
        }


        public bool TextWrite(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt)) return false;
            this.textRWDAL.TextWrite(txt);
            return true;
        }
    }
}
