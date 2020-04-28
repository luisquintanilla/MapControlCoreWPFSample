using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Microsoft.Toolkit.Wpf.UI.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace MapControlCoreWPFSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SatelliteMap_Loaded(object sender, RoutedEventArgs e)
        {
            // Specify a known location.
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 };
            var cityCenter = new Geopoint(cityPosition);
        }

        private async void GetImageAsync(object sender, RoutedEventArgs e)
        {
            //SatelliteMap.InvalidateVisual();
            SatelliteMap
            Rect bounds = VisualTreeHelper.GetDescendantBounds(this);

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext context = visual.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(this);
                context.DrawRectangle(vb, null, new Rect(new System.Windows.Point(), bounds.Size));
            }

            renderTarget.Render(visual);
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(renderTarget));
            using (var fs = File.Create("image.jpg"))
            {
                encoder.Save(fs);
            }
        }

    }
}
