using PgJsonParse;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    /// <summary>
    /// Takes an enum, and select one of several objects to return. value 0 selects the first object, and so on.
    /// </summary>
    [ValueConversion(typeof(object), typeof(object))]
    public class EnumToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type target_type, object parameter, CultureInfo culture)
        {
            int EnumValue = (int)value;
            CustomCompositeCollection ObjectList = parameter as CustomCompositeCollection;
            return ObjectList[EnumValue];
        }

        public object ConvertBack(object value, Type target_type, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
