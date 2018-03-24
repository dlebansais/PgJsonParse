using PgJsonParse;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(bool), typeof(int))]
    public class ObjectToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool[] ItemValues = parameter as bool[];

            if (value is bool)
            {
                bool BooleanValue = (bool)value;
                return ((BooleanValue && ItemValues[0]) || (!BooleanValue && !ItemValues[0])) ? 0 : 1;
            }
            else if (value is int)
            {
                int IntValue = (int)value;
                return ((IntValue != 0 && ItemValues[0]) || (IntValue == 0 && !ItemValues[0])) ? 0 : 1;
            }
            else
                return ((value != null && ItemValues[0]) || (value == null && !ItemValues[0])) ? 0 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
