using System;
using System.Windows;
using System.Windows.Controls;

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

                Result = element.TryFindResource(TemplateName) as DataTemplate;
                if (Result == null)
                    ItemType = ItemType.BaseType;
            }

            if (Result != null)
                return Result;
            else
                return null;
        }
    }
}
