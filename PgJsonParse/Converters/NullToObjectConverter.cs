using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    /// <summary>
    /// Takes a reference, and select one of two objects to return. Null selects the first object, non-null the second.
    /// </summary>
    [ValueConversion(typeof(object), typeof(object))]
    public class NullToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CompositeCollection CollectionOfItems = parameter as CompositeCollection;
            return CollectionOfItems[value == null ? 0 : 1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
