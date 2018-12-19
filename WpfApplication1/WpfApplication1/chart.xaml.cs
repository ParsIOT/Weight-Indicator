using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using System.Threading;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApplication1
{
    public partial class chart : UserControl
    {
        private BackgroundWorker worker = null;
        public List<int> datas = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public List<SolidColorBrush> colors = new List<SolidColorBrush>() { Brushes.White, Brushes.CornflowerBlue};
        private int cntr = 0;

        private Random rnd = new Random();

        public chart()
        {
            InitializeComponent();
            DataContext = this;
            Console.ReadLine();
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            LastHourSeries = new SeriesCollection                            
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(datas[0]),                      
                        new ObservableValue(datas[1]),
                        new ObservableValue(datas[2]),
                        new ObservableValue(datas[3]),
                        new ObservableValue(datas[4]),
                        new ObservableValue(datas[5]),
                        new ObservableValue(datas[6]),
                        new ObservableValue(datas[7]),
                        new ObservableValue(datas[8]),
                        new ObservableValue(datas[9]),
                        new ObservableValue(datas[10]),                   
                        new ObservableValue(datas[11]),
                        new ObservableValue(datas[12]),
                        new ObservableValue(datas[13]),
                        new ObservableValue(datas[14]),
                        new ObservableValue(datas[15]),
                        new ObservableValue(datas[16]),
                        new ObservableValue(datas[17]),
                        new ObservableValue(datas[18]),
                        new ObservableValue(datas[19]),
                    }
                }
            };
        }

        public int Data
        {
            get { return datas[19]; }
            set
            {
                datas.RemoveAt(0);
                datas.Add(value);
                LastHourSeries[0].Values.RemoveAt(0);
                LastHourSeries[0].Values.Add(new ObservableValue(value));
            }
        }

        public SeriesCollection LastHourSeries { get; set; }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            worker.RunWorkerAsync(10000);
        }

        private void Cancell_OnClick(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int max = (int)e.Argument;
            while(true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }
                int ProgressPercentage = rnd.Next(1, 10);
                (sender as BackgroundWorker).ReportProgress(ProgressPercentage);
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    (ThreadStart)delegate()
                    {
                        weight.Text = Data.ToString();
                    }
                );
                Thread.Sleep(1000);

            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Data = e.ProgressPercentage;
            Border1.Background = colors[cntr];
            cntr++;
            cntr = cntr % 2;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }

    }
}