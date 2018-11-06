using Presentation;

namespace Converters
{
#if CSHTML5
#else
    [ValueConversion(typeof(object), typeof(object))]
#endif
    public class NullToObjectConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            CustomCompositeCollection CollectionOfItems = parameter as CustomCompositeCollection;
            return CollectionOfItems[value == null ? 0 : 1];
        }
    }
}
