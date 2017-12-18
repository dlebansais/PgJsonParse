using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    /// <summary>
    /// Returns the int corresponding to an enum.
    /// </summary>
    [ValueConversion(typeof(object), typeof(int))]
    public class EnumToIntConverter : IValueConverter
    {
        public object Convert(object value, Type target_type, object parameter, CultureInfo culture)
        {
            int IntValue = (int)value;
            return IntValue;
        }

        public object ConvertBack(object value, Type target_type, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
