using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    public class OrConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            CompositeCollection CollectionOfItems = parameter as CompositeCollection;

            bool Result = false;
            foreach (object Value in values)
            {
                if (Value is bool)
                    Result |= (bool)Value;
            }

            return CollectionOfItems[Result ? 1 : 0];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
