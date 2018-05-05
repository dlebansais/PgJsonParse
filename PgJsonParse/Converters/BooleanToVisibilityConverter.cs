using Presentation;
using System.Windows.Data;
#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace Converters
{
    [ValueConversion(typeof(bool), typeof(object))]
    public class BooleanToVisibilityConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            bool BoolValue = (bool)value;
            return BoolValue ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
