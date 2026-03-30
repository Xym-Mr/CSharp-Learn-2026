using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinFileRW.Model
{
    /// <summary>
    /// 对应CSV中的每一行数据Model
    /// </summary>
    internal class CSVData
    {
        public string PName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string City { get; set; }
        public string ZhiYe { get; set; }
        public string JoinDate { get; set; }
        public string XinZi { get; set; }
    }
}
