using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCommunication.Model
{
    /// <summary>
    /// 接口
    /// </summary>
    internal interface INotifyProperty
    {
        /// <summary>
        /// 定义了一个系统现成的事件
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;
    }
}
