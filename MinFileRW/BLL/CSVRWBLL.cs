using MinFileRW.DAL;
using MinFileRW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinFileRW.BLL
{
    internal class CSVRWBLL
    {
        private CSVRWDAL csvRWDAL = new CSVRWDAL();

        public string CSVRead()
        {
            return this.csvRWDAL.CSVRead();
        }

        public bool CSVWrite(string str)
        {
            string[] strs = str.Split(',');

            List<CSVData> datas = new List<CSVData>()
            {
            new CSVData()
            {
                PName= strs[0],
                Gender= strs[1],
                Age= strs[2],
                City= strs[3],
                ZhiYe= strs[4],
                JoinDate= strs[5],
                XinZi= strs[6]
            }
            };
            return this.csvRWDAL.CSVWrite(datas);
        }
    }
}
