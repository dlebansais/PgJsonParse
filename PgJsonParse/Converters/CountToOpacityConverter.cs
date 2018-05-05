using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int), typeof(double))]
    public class CountToOpacityConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            int IndexValue = (int)value;
            if (IndexValue <= 0)
                return 0.0;
            else
                return 1.0;
        }
    }
}
