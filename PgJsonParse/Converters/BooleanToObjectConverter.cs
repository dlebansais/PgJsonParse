using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    /// <summary>
    /// Takes a boolean, and select one of two objects to return. False selects the first object, True the second.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(object))]
    public class BooleanToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type target_type, object parameter, CultureInfo culture)
        {
            bool BooleanValue = (bool)value;
            CompositeCollection ObjectList = parameter as CompositeCollection;
            return BooleanValue ? ObjectList[1] : ObjectList[0];
        }

        public object ConvertBack(object value, Type target_type, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
