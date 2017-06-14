using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int), typeof(object))]
    public class IntToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int IndexValue = (int)value;

            CompositeCollection CollectionOfItems = parameter as CompositeCollection;

            if (IndexValue < 0)
                return CollectionOfItems[0];
            else if (IndexValue >= CollectionOfItems.Count)
                return CollectionOfItems[CollectionOfItems.Count - 1];
            else
                return CollectionOfItems[IndexValue];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}
