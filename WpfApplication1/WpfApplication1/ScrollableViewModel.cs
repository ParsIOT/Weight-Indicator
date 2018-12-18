using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Policy;
using LiveCharts.Defaults;
using LiveCharts.Geared;
using System.Threading;


namespace WpfApplication1
{
    public partial class ScrollableViewModel : INotifyPropertyChanged
    {
        private Func<double, string> _formatter;
        private double _from;
        private double _to;
        static Thread[] mythread = new Thread[5];
        private int y = 0;
        private Random rnd = new Random();
        public List<int> datas = new List<int>() {0, 100, 200, 300, 1400, 500, 700};
        


        public ScrollableViewModel()
        {
            var now = DateTime.Now;
            var trend = -30000d;
            var l = new List<double>();
            var r = new Random();
           

            for (var i = 0; i < 100; i++)
            {
                now = now.AddHours(1);
                l.Add(i);

                if (r.NextDouble() > 0.4)
                {
                    trend += r.NextDouble() * 10;
                }
                else
                {
                    trend -= r.NextDouble() * 10;
                }
            }

            Formatter = x => (x).ToString("G"); 

//            Values = l.AsGearedValues().WithQuality(Quality.High);

                
            From = 0;
            To = 200;
        }

        

        public object Mapper { get; set; }
        public GearedValues<double> Values { get; set; }
        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }
        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }

        public Func<double, string> Formatter
        {
            get { return _formatter; }
            set
            {
                _formatter = value;
                OnPropertyChanged("Formatter");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}