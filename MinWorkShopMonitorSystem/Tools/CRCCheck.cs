using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem.Tools
{
    internal class CRCCheck
    {
        #region CRC 工具
        public static byte[] CRC16(byte[] data, int length)
        {
            ushort crc = 0xFFFF;
            for (int i = 0; i < length; i++)
            {
                crc ^= data[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 1) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                        crc >>= 1;
                }
            }
            return new[] { (byte)crc, (byte)(crc >> 8) };
        }

        public static bool CheckCRC(byte[] frame)
        {
            if (frame.Length < 4) return false;
            var calc = CRC16(frame, frame.Length - 2);
            return calc[0] == frame[frame.Length - 2] && calc[1] == frame[frame.Length - 1];
        }
        #endregion
    }
}
