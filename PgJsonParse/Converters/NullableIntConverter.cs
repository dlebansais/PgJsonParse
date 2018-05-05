using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int?), typeof(string))]
    public class NullableIntConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            if (value == null)
                return "";

            int IntValue = (int)value;

            string Result = "";

            string AsString;
            if ((AsString = parameter as string) != null)
                if (AsString == "+" && IntValue > 0)
                    Result += "+";

            Result += IntValue.ToString();

            return Result;
        }
    }
}
