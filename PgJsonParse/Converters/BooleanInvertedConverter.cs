using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(bool), typeof(object))]
    public class BooleanInvertedConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            bool BoolValue = (bool)value;
            return !BoolValue;
        }
    }
}
