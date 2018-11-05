#if CSHTML5
using System;
using Windows.UI.Xaml.Data;

namespace Presentation
{
    public abstract class GenericValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Convert(value, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }

        protected abstract object Convert(object value, object parameter);
    }
}
#else
using System;
using System.Globalization;
using System.Windows.Data;

namespace Presentation
{
    public abstract class GenericValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }

        protected abstract object Convert(object value, object parameter);
    }
}
#endif
