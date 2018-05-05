using PgJsonParse;
using Presentation;
using System.Windows.Data;

namespace Converters
{
    /// <summary>
    /// Takes an enum, and select one of several objects to return. value 0 selects the first object, and so on.
    /// </summary>
    [ValueConversion(typeof(object), typeof(object))]
    public class EnumToObjectConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            int EnumValue = (int)value;
            CustomCompositeCollection ObjectList = parameter as CustomCompositeCollection;
            return ObjectList[EnumValue];
        }
    }
}
