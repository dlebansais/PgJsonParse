using PgJsonObjects;
using Presentation;
using System;
using System.Collections;
using System.Reflection;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int), typeof(string))]
    public class EnumToTextConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
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
    }
}
