using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Converters
{
    [ValueConversion(typeof(uint?), typeof(Brush))]
    public class RgbColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            uint? rgbColor = value as uint?;

            if (rgbColor.HasValue)
                return new SolidColorBrush(Color.FromRgb((byte)((rgbColor >> 16) & 0xFF), (byte)((rgbColor >> 8) & 0xFF), (byte)((rgbColor >> 0) & 0xFF)));
            else
                return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
