using Presentation;

namespace Converters
{
#if CSHTML5
#else
    [ValueConversion(typeof(bool), typeof(object))]
#endif
    public class BooleanToObjectConverter : GenericValueConverter
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
