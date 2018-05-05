using PgJsonParse;
using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int), typeof(object))]
    public class IndexToObjectConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            int IndexValue = (int)value;

            CustomCompositeCollection CollectionOfItems = parameter as CustomCompositeCollection;

            return IndexValue < 0 ? CollectionOfItems[0] : CollectionOfItems[1];
        }
    }
}
