using PgJsonObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int), typeof(string))]
    public class EnumToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string MapName = parameter as string;
            Type MapType = typeof(TextMaps);
            FieldInfo MapField = MapType.GetField(MapName);
            IDictionary StringMap = MapField.GetValue(null) as IDictionary;

            string Text = "";

            IEnumerable AsCollection;
            if ((AsCollection = value as IEnumerable) != null)
            {
                foreach (object Enum in AsCollection)
                {
                    if (Text.Length > 0)
                        Text += ", ";

                    Text += StringMap[Enum] as string;
                }
            }
            else
                Text = StringMap[value] as string;

            return Text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
