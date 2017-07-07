using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    public class MultiStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string Result = "";

            foreach (object Value in values)
                Result += Value.ToString();

            return Result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
