using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem._4DAL
{
    internal interface IMobusTransport : IDisposable
    {
        /// <summary>
        /// 发送和接受
        /// </summary>
        byte[] SendReceive(byte[] sendBuffer);

        bool Open();

        void Close();
    }
}
