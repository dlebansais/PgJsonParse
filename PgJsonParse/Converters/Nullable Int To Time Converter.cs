using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int?), typeof(string))]
    public class NullableIntToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int? IntValue = (int?)value;
            if (!IntValue.HasValue)
                return "";
            else
                return IntValue.Value.ToString("D02") + ":00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}
