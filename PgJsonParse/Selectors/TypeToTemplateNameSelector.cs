using System;
using Presentation;
#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace PgJsonParse
{
    public class TypeToTemplateNameSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
                return null;

            FrameworkElement element = container as FrameworkElement;
            Type ItemType = item.GetType();

            DataTemplate Result = null;

            while (ItemType != null && Result == null)
            {
                string TemplateName = ItemType.Name + "Template";

                Result = FindTemplate(element, TemplateName);
                if (Result == null)
                    ItemType = ItemType.BaseType;
            }

            if (Result != null)
                return Result;
            else
                return base.SelectTemplate(item, container);
        }
    }
}
