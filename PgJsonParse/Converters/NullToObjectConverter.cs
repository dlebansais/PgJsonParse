using PgJsonParse;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(object), typeof(object))]
    public class NullToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CustomCompositeCollection CollectionOfItems = parameter as CustomCompositeCollection;
            return CollectionOfItems[value == null ? 0 : 1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
