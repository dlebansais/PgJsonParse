using PgJsonParse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(object), typeof(bool))]
    public class IsEnumInSetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<int> EnumList = new List<int>();

            IEnumerable AsCollection;
            if ((AsCollection = value as IEnumerable) != null)
            {
                foreach (int Enum in AsCollection)
                    if (!EnumList.Contains(Enum))
                        EnumList.Add(Enum);
            }
            else
                EnumList.Add((int)value);

            CustomCompositeCollection CollectionOfItems = parameter as CustomCompositeCollection;

            foreach (object Item in CollectionOfItems)
            {
                int ItemValue = (int)Item;
                if (EnumList.Contains(ItemValue))
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
