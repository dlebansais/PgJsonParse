using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(object), typeof(bool))]
    public class IsEnumInSetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int IntValue = (int)value;
            CompositeCollection CollectionOfItems = parameter as CompositeCollection;

            foreach (object Item in CollectionOfItems)
            {
                int ItemValue = (int)Item;
                if (ItemValue == IntValue)
                    return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
