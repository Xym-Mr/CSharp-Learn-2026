using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCommunication.Model
{
    /// <summary>
    /// 一个抽象类
    /// </summary>
    internal abstract class ObservableObject : INotifyProperty
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 定义一个虚方法，外部可以Override的方式扩展，实现PropertyChanged.invoke();
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 定义一个实例方法，供外部调用，实现属性赋值和PropertyChanged事件触发
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool SetField<T>(ref T field, T val, [CallerMemberName]string propertyName="")
        {
            if (EqualityComparer<T>.Default.Equals(field, val)) return false;//比较两个默认实例对象是否相等？相等时不更新值
            field = val;//更新值
            OnPropertyChanged(propertyName);//通知(发布)事件
            return true;
        }
    }
}
