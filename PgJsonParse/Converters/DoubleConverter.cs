using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(double), typeof(string))]
    public class DoubleConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            double DoubleValue;

            if (value is double)
                DoubleValue = (double)value;

            else
            {
                double? NullableDoubleValue = (double?)value;
                if (NullableDoubleValue == null)
                    return "";

                DoubleValue = NullableDoubleValue.Value;
            }

            string AsString;
            if ((AsString = parameter as string) != null)
            {
                if (AsString == "+" && DoubleValue > 0)
                    return "+" + InvariantCulture.DoubleToString(DoubleValue);

                else if (AsString == "%" && DoubleValue >= 0 && DoubleValue <= 1.0)
                    return InvariantCulture.DoubleToString(DoubleValue * 100) + "%";
            }

            return DoubleValue.ToString();
        }
    }
}
