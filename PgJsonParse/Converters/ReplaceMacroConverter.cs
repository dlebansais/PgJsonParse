using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class ReplaceMacroConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            if (value == null)
                return null;

            string StringValue = value as string;

            StringValue = StringValue.Replace("%NAME%", "Target");

            return StringValue;
        }
    }
}
