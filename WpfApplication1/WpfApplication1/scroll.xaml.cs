using System;
using LiveCharts.Events;
using WpfApplication1;

namespace Geared.Wpf.Scrollable
{
    /// <summary>
    /// Interaction logic for ScrollingWindow.xaml
    /// </summary>
    public partial class ScrollableView : IDisposable
    {
        public ScrollableView()
        {
            InitializeComponent();
        }

        private void Axis_OnRangeChanged(RangeChangedEventArgs eventargs)
        {
            var vm = (ScrollableViewModel)DataContext;

            var currentRange = eventargs.Range;
            Console.Write(currentRange);
            Console.Write("    ");

            if (currentRange < TimeSpan.TicksPerDay * 2)
            {
                vm.Formatter = x => (x /8).ToString("G");
                Console.Write(currentRange);
                Console.Write("    ");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 60)
            {
                vm.Formatter = x => (x * 100).ToString("G");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 540)
            {
                vm.Formatter = x => (x * 1000).ToString("G");
                return;
            }

            vm.Formatter = x => (x * 10000).ToString("G");
        }

        public void Dispose()
        {
            var vm = (ScrollableViewModel)DataContext;
            vm.Values.Dispose();
        }
    }
}