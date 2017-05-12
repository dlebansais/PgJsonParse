using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int), typeof(string))]
    public class IntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int IntValue;

            if (value is int)
                IntValue = (int)value;

            else
            {
                int? NullableIntValue = (int?)value;
                if (NullableIntValue == null)
                    return "";

                IntValue = NullableIntValue.Value;
            }

            string Result = "";

            string AsString;
            if ((AsString = parameter as string) != null)
                if (AsString == "+" && IntValue > 0)
                    Result += "+";

            Result += IntValue.ToString();

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
