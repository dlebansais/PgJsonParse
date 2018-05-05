using PgJsonParse;
using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(object), typeof(object))]
    public class NullToObjectConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            CustomCompositeCollection CollectionOfItems = parameter as CustomCompositeCollection;
            return CollectionOfItems[value == null ? 0 : 1];
        }
    }
}
