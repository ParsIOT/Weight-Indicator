using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Timers;
using LiveCharts;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using System.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MaterialCards.xaml
    /// </summary>
    public partial class chart : UserControl, INotifyPropertyChanged
    {
        static Thread[] mythread = new Thread[5];
        static Semaphore mutex = new Semaphore(1, 2);
        public List<int> datas = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private double _lastLecture;
        private Random rnd = new Random();

        public chart()
        {
            InitializeComponent();
            Console.ReadLine();



            LastHourSeries = new SeriesCollection                                            //the data that is bined to xaml 
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(datas[0]),                      //its default values
                        new ObservableValue(datas[1]),
                        new ObservableValue(datas[2]),
                        new ObservableValue(datas[3]),
                        new ObservableValue(datas[4]),
                        new ObservableValue(datas[5]),
                        new ObservableValue(datas[6]),
                        new ObservableValue(datas[7]),
                        new ObservableValue(datas[8]),
                        new ObservableValue(datas[9]),
                        new ObservableValue(datas[10]),                      //its default values
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
            DataContext = this;
            mythread[0] = new Thread(producer);
            mythread[0].Start();
            mythread[1] = new Thread(consumer);
            mythread[1].Start();
        }



        void producer()
        {
            while (true)
            {
                mutex.WaitOne();
                Data = rnd.Next(1, 13);
                mutex.Release();
                Thread.Sleep(500);
            }
            
        }

        void consumer()
        {

            while (true)
            {
                mutex.WaitOne();
                LastHourSeries[0].Values.RemoveAt(0);               //we must also  have changes on our bined data(LastHourSeries)
                LastHourSeries[0].Values.Add(new ObservableValue(Data));
                mutex.Release();
                Thread.Sleep(500);

            }
            
            
        }


        public int Data                                     //the function to access datas list
        {
            get { return datas[19]; }                          //if you call Data() it will return last number
            set                                               // if you set Data=value it will
            {
                datas.RemoveAt(0);                                  //will remove index 1
                datas.Add(value);                                      //and append  the value to datas list
                LastLecture = value;


            }
        }



        private void BuildPngOnClick(object sender, RoutedEventArgs e)
        {
           
            var viewbox = new Viewbox();
            viewbox.Child = myChart1;
            viewbox.Measure(myChart1.RenderSize);
            viewbox.Arrange(new Rect(new Point(0, 0), myChart1.RenderSize));
            myChart1.Update(true, true); //force chart redraw
            viewbox.UpdateLayout();

            SaveToPng(myChart1, "mychart.png");
            //png file was created at the root directory.
        }

        private void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName)) encoder.Save(stream);
        }
    



        public SeriesCollection LastHourSeries { get; set; }
        public double LastLecture
        {
            get { return _lastLecture; }
            set
            {
                _lastLecture = value;
                OnPropertyChanged("LastLecture");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateOnclick(object sender, RoutedEventArgs e)
        {
//            TimePowerChart.Update(true);
        }

        
    }
}