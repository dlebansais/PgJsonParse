using PgJsonParse;
using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(bool), typeof(object))]
    public class NullableBooleanToObjectConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            int IntValue;

            if (!(value is bool))
                IntValue = 2;
            else
                IntValue = ((bool)value) ? 1 : 0;

            CustomCompositeCollection CollectionOfItems = parameter as CustomCompositeCollection;

            return CollectionOfItems[IntValue];
        }
    }
}
