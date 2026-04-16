using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DelegateCommunication.BLL
{
    internal class DataMonitor
    {
        internal delegate void PropertyChangedDelete(string propertyName, double val, int status);
        public PropertyChangedDelete OnPropertyChanged;

        private double _humidity;

        public double Humidity
        {
            get { return _humidity; }
            set
            {
                if (_humidity != value)
                {
                    _humidity = value;
                    CheckPropertyValue(value);
                }
            }
        }

        private double _temperature;

        public double Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                CheckPropertyValue(value);
            }
        }



        private void CheckPropertyValue(double val, [CallerMemberName] string propertyName = "")
        {
            // 定义每个属性的阈值（可通过配置或外部注入）
            var thresholds = new Dictionary<string, double>
        {
            { "Humidity", 80 },
            { "Temperature", 30 }
        };

            if (thresholds.TryGetValue(propertyName, out double threshold))
            {
                int i = 0;
                if (val > threshold)
                    i = 1;//超上限
                else if (val < 10) i = -1;//低下限
                else i = 0;//正常
                //触发超限事件
                OnPropertyChanged?.Invoke(propertyName, val, i);
            }

        }
    }



}
