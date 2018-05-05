using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class IntThresholdConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            int IndexValue = (int)value;

            int ThresholdValue;
            if (parameter is string)
            {
                if (!int.TryParse(parameter as string, out ThresholdValue))
                    return false;
            }
            else
                ThresholdValue = (int)parameter;

            return IndexValue >= ThresholdValue;
        }
    }
}
