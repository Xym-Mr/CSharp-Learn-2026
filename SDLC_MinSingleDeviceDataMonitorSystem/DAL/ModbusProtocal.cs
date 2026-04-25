using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLC_MinSingleDeviceDataMonitorSystem.DAL
{
    /// <summary>
    /// Modbus协议构建与解析
    /// </summary>
    internal class ModbusProtocal
    {
        /// <summary>
        /// 读取连续的输入寄存器3区的值
        /// </summary>
        /// <param name="slaveId"></param>
        /// <param name="startAddress"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public byte[] BuildReadInputRegisterFrame(byte slaveId, ushort startAddress, ushort quantity)
        {
            //读取01从站，从0开始的10个连续的输入寄存器的值完整发送报文：
            //Tx:01 04 00 00 00 0A 70 0D
            //拼接基础报文
            byte[] buffer = new byte[8] { slaveId, 0x04, (byte)(startAddress >> 8), (byte)startAddress, (byte)(quantity >> 8), (byte)quantity, 0, 0 };
            //追加CRC16校验码
            byte[] crc = CRC16(buffer, 6);
            buffer[6] = crc[0];
            buffer[7] = crc[1];
            //返回完整的报文
            return buffer;
        }

        /// <summary>
        /// 解析从站返回的数据报文
        /// </summary>
        /// <param name="slaveId"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public ushort[] ParseInputRegisterResponseFrame(byte slaveId ,byte[] bytes)
        {
            //读取01从站，从0开始的10个连续的输入寄存器的值完整响应报文：
            //Rx:01 04 14 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 95 81
            if (!(bytes?.Count() > 0)) throw new ArgumentNullException(nameof(bytes), "响应数据为空");
            if (bytes.Length < 5) throw new ArgumentOutOfRangeException(nameof(bytes), bytes.Length, "数据不完整");
            byte Id = bytes[0];
            byte funCode = bytes[1];
            byte quantity = bytes[2];
            if (slaveId != Id || funCode != 0x04 || bytes.Length != quantity + 5) return null;

            ushort[] resData = null;

            if (CheckCRC(bytes))
            {
                byte[] data = new byte[quantity];
                Array.Copy(bytes, 3, data, 0, quantity);//截取数据

                //解析数据
                resData = new ushort[quantity / 2];//数据字节个数/2=读取的寄存器个数
                for (int i = 0; i < data.Length; i += 2)
                {
                    resData[i / 2] = (ushort)(data[i] * 256 + data[i + 1]);
                }
                return resData;
            }
            throw new ArgumentException("CRC校验不通过", nameof(bytes));
        }

        /// <summary>
        /// 检查字节数组中校验码是否正确？
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public bool CheckCRC(byte[] frame)
        {
            if (frame.Length < 4) return false;
            var calc = CRC16(frame, frame.Length - 2);
            return calc[0] == frame[frame.Length - 2] && calc[1] == frame[frame.Length - 1];
        }



        /// <summary>
        /// 获取字节数组中指定长度字节的CRC校验码
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private byte[] CRC16(byte[] data, int length)
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


    }
}
