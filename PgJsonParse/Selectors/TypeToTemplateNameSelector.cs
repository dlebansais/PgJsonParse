using System;
using Presentation;
using PgJsonObjects;
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
            if (item is ConsumedItemDirect)
                return null;

            if (item as IObjectContentGenerator == null)
                return null;

            FrameworkElement element = container as FrameworkElement;
            Type ItemType = item.GetType();

            DataTemplate Result = null;

            while (ItemType != null && Result == null)
            {
                string ItemTypeName = ItemType.Name;
                if (ItemTypeName.Contains("`"))
                    ItemTypeName = ItemTypeName.Substring(0, ItemTypeName.IndexOf("`"));
                string TemplateName = ItemTypeName + "Template";

                if (TemplateName.StartsWith("Pg"))
                    TemplateName = TemplateName.Substring(2);
                else if (TemplateName.StartsWith("Json"))
                    TemplateName = TemplateName.Substring(4);

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
