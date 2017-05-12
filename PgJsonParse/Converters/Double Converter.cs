using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(double), typeof(string))]
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double DoubleValue;

            if (value is double)
                DoubleValue = (double)value;

            else
            {
                double? NullableDoubleValue = (double?)value;
                if (NullableDoubleValue == null)
                    return "";

                DoubleValue = NullableDoubleValue.Value;
            }

            string Result = "";

            string AsString;
            if ((AsString = parameter as string) != null)
                if (AsString == "+" && DoubleValue > 0)
                    Result += "+";

            Result += DoubleValue.ToString();

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
