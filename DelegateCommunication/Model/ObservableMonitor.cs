using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateCommunication.Model
{
    /// <summary>
    /// 自定义的事件参数类
    /// </summary>
    public class StatusChangedEventArgs : EventArgs
    {
        public string Status { get; }

        public StatusChangedEventArgs(string status)
        {
            Status = status;
        }
    }

    /// <summary>
    /// 这是实现属性值监控的实现类，继承IComparable是为了使用
    /// </summary>
    internal class ObservableMonitor<T> : ObservableObject where T : IComparable<T>
    {
        /// <summary>
        /// T 数据当前值字段
        /// </summary>
        private T _currentValue;
        /// <summary>
        /// T 数据下限设定值
        /// </summary>
        private T _minLimit;
        /// <summary>
        /// T 数据上限设定值
        /// </summary>
        private T _maxLimit;
        /// <summary>
        /// T 数据显示名称
        /// </summary>
        private string _displayName;
        /// <summary>
        /// T 数据单位符号
        /// </summary>
        private string _unit;

        /// <summary>
        /// 只读 单位符号
        /// </summary>
        public string Unit
        {
            get { return _unit; }
        }

        /// <summary>
        /// 只读 数据名称
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
        }

        public T MaxLimit
        {
            get { return _maxLimit; }
            set { SetField(ref _maxLimit, value); }
        }

        public T MinLimit
        {
            get { return _minLimit; }
            set { SetField(ref _minLimit, value); }
        }

        public T CurrentValue
        {
            get { return _currentValue; }
            set { SetField(ref _currentValue, value); }
        }

        public string Status
        {
            get
            {
                if (CurrentValue.CompareTo(MinLimit) < 0) return "低于下限值";//低下限报警
                if (CurrentValue.CompareTo(MaxLimit) > 0) return "高于上限值";//超上限报警
                else return "正常";//正常范围内                                            
            }
        }


        public ObservableMonitor(T initialValue, T minLimit, T maxLimit, string displayName, string unit = "")
        {
            _currentValue = initialValue;
            _minLimit = minLimit;
            _maxLimit = maxLimit;
            _displayName = displayName;
            _unit = unit;
        }

        /// <summary>
        /// 对外提供一个事件，参数为自定义参数类型
        /// </summary>
        public event EventHandler<StatusChangedEventArgs> StatusChanged;

        /// <summary>
        /// 重写基类中的虚方法，重新按需定义业务逻辑
        /// </summary>
        /// <param name="propertyName"></param>
        public override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);//执行基类的事件触发

            if (propertyName == nameof(CurrentValue) || propertyName == nameof(MinLimit) || propertyName == nameof(MaxLimit))//筛选一下可以触发事件的属性,DisplayName\Unit不触发
            {
                StatusChanged?.Invoke(this, new StatusChangedEventArgs(Status));
            }
        }
    }
}
