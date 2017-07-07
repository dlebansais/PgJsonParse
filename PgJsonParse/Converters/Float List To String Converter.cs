using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(IList), typeof(string))]
    public class FloatListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IList ListValue = value as IList;
            if (ListValue == null)
                return "";

            string Result = "";

            foreach (object Item in ListValue)
                if (Item is float)
                {
                    if (Result.Length > 0)
                        Result += ", ";

                    Result += (float)Item;
                }

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}
