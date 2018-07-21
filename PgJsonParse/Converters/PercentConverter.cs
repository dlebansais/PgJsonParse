using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(object), typeof(string))]
    public class PercentConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            float FloatValue;
            double DoubleValue;

            if (value is float)
            {
                FloatValue = (float)value;
                return ((int)(FloatValue * 100)).ToString() + "%";
            }

            else if (value is double)
            {
                DoubleValue = (double)value;
                return ((int)(DoubleValue * 100)).ToString() + "%";
            }

            return null;
        }
    }
}
