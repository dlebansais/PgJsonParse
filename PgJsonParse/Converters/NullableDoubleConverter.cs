using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int?), typeof(string))]
    public class NullableDoubleConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            if (value == null)
                return "";

            double DoubleValue = (double)value;

            string Result = "";

            string AsString;
            if ((AsString = parameter as string) != null)
                if (AsString == "+" && DoubleValue > 0)
                    Result += "+";

            Result += DoubleValue.ToString();

            return Result;
        }
    }
}
