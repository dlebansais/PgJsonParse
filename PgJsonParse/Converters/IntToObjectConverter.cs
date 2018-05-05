using PgJsonParse;
using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(int), typeof(object))]
    public class IntToObjectConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            int IndexValue = (int)value;

            CustomCompositeCollection CollectionOfItems = parameter as CustomCompositeCollection;

            if (IndexValue < 0)
                return CollectionOfItems[0];
            else if (IndexValue >= CollectionOfItems.Count)
                return CollectionOfItems[CollectionOfItems.Count - 1];
            else
                return CollectionOfItems[IndexValue];
        }
    }
}
