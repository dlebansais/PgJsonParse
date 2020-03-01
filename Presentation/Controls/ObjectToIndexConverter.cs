#if CSHTML5
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Presentation
{
    internal class ObjectToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool)
            {
                bool BooleanValue = (bool)value;

                bool BooleanExpectedValue;
                if (parameter is bool)
                    BooleanExpectedValue = (bool)parameter;
                else
                    bool.TryParse(parameter as string, out BooleanExpectedValue);

                return BooleanValue == BooleanExpectedValue ? Visibility.Visible : Visibility.Collapsed;
            }

            else if (value is int)
            {
                int IntValue = (int)value;

                int IntExpectedValue;
                if (parameter is int)
                    IntExpectedValue = (int)parameter;
                else
                    int.TryParse(parameter as string, out IntExpectedValue);

                return IntValue == IntExpectedValue ? Visibility.Visible : Visibility.Collapsed;
            }

            else
                return ((value != null && parameter != null) || (value == null && parameter == null)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
#else
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Presentation
{
    internal class ObjectToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool BooleanValue)
            {
                bool ExpectedBooleanValue;
                if (parameter is bool)
                    ExpectedBooleanValue = (bool)parameter;
                else if (bool.TryParse(parameter as string, out bool ConvertedBooleanValue))
                    ExpectedBooleanValue = ConvertedBooleanValue;
                else
                    ExpectedBooleanValue = false;

                return BooleanValue == ExpectedBooleanValue ? Visibility.Visible : Visibility.Collapsed;
            }
            else if (value is int IntValue)
            {
                int ExpectedIntValue;

                if (parameter is int)
                    ExpectedIntValue = (int)parameter;
                else if (int.TryParse(parameter as string, out int ConvertedIntValue))
                    ExpectedIntValue = ConvertedIntValue;
                else
                    ExpectedIntValue = 0;

                return IntValue == ExpectedIntValue ? Visibility.Visible : Visibility.Collapsed;
            }
            else
                return ((value != null && parameter != null) || (value == null && parameter == null)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
#endif
