﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(bool), typeof(object))]
    public class BooleanInvertedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool BoolValue = (bool)value;
            return !BoolValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
