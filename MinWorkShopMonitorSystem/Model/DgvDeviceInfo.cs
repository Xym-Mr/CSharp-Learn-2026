using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MinWorkShopMonitorSystem.Model
{
    internal class DgvDeviceInfo : INotifyPropertyChanged
    {
        public int SlaveDeviceID { get; set; }
        private int _address;
        public int Address
        {
            get { return _address; }
            set { _address = value + 40001; }
        }
        public string DataName { get; set; }
        public string Unit { get; set; }
        public float High { get; set; }
        public float Low { get; set; }

        //public float CurrentValue { get; set; }

        private double _currentValue;

        public double CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
                OnPropertyChanged();
            }
        }

        public bool IsAlarm
        {
            get
            {
                return (this.CurrentValue < Low || this.CurrentValue > High);
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}