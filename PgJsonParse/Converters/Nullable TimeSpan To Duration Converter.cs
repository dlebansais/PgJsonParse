using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(TimeSpan?), typeof(string))]
    public class NullableTimeSpanToDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan? TimeSpanValue = value as TimeSpan?;
            if (!TimeSpanValue.HasValue)
                return "";

            TimeSpan Duration = TimeSpanValue.Value;
            return PgJsonObjects.Tools.TimeSpanToString(Duration);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}
