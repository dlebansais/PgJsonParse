using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class IntThresholdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int IndexValue = (int)value;

            int ThresholdValue;
            if (parameter is string)
            {
                if (!int.TryParse(parameter as string, out ThresholdValue))
                    return false;
            }
            else
                ThresholdValue = (int)parameter;

            return IndexValue >= ThresholdValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}
