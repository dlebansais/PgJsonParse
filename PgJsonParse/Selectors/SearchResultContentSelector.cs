using PgJsonObjects;
using Presentation;
#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace PgJsonParse
{
    public class SearchResultContentSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (item is IBackLinkable AsSearchable)
            {
                DataTemplate Result = FindTemplate(element, AsSearchable.GetSearchResultContentTemplateName());
                return Result;
            }
            else
                return base.SelectTemplate(item, container);
        }
    }
}
