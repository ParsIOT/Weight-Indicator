using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    class SavetheChart
    {
        private void startSaving(LiveCharts.Wpf.CartesianChart myChart1)
        {

            var viewbox = new Viewbox();
            viewbox.Child = myChart1;
            viewbox.Measure(myChart1.RenderSize);
            viewbox.Arrange(new Rect(new Point(0, 0), myChart1.RenderSize));
            myChart1.Update(true, true);
            viewbox.UpdateLayout();
            SaveToPng(myChart1, "mychart.png");
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

    }
}
