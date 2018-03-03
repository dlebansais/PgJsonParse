using PgJsonParse;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int), typeof(object))]
    public class IndexToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int IndexValue = (int)value;

            CustomCompositeCollection CollectionOfItems = parameter as CustomCompositeCollection;

            return IndexValue < 0 ? CollectionOfItems[0] : CollectionOfItems[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}
