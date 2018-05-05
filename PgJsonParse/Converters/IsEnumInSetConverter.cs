﻿using PgJsonParse;
using Presentation;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(object), typeof(bool))]
    public class IsEnumInSetConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
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
    }
}
