using System.Windows;
using System.Windows.Controls;

namespace PgJsonParse
{
    public class TypeToTemplateNameSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            string TemplateName = item.GetType().Name + "Template";

            DataTemplate Result = element.TryFindResource(TemplateName) as DataTemplate;
            return Result;
        }
    }
}
