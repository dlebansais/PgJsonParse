using PgJsonObjects;
using Presentation;
#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace PgJsonParse
{
    public class ConsumedItemSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            ConsumedItemByKeyword AsConsumedItemByKeyword;
            ConsumedItemDirect AsConsumedItemDirect;

            if ((AsConsumedItemByKeyword = item as ConsumedItemByKeyword) != null)
            {
                DataTemplate Result = FindTemplate(element, "ConsumedItemByKeywordTemplate");
                return Result;
            }

            else if ((AsConsumedItemDirect = item as ConsumedItemDirect) != null)
            {
                DataTemplate Result = FindTemplate(element, "ConsumedItemDirectTemplate");
                return Result;
            }

            else
                return base.SelectTemplate(item, container);
        }
    }
}
