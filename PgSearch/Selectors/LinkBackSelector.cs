using PgJsonObjects;
using Presentation;
using System;
using System.Collections.Generic;
#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace PgSearch
{
    public class LinkBackSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            if (!(item is KeyValuePair<Type, List<IBackLinkable>>)) // For "disconnected item"...
                return null;

            KeyValuePair<Type, List<IBackLinkable>> ItemKeyValue = (KeyValuePair<Type, List<IBackLinkable>>)item;
            Type Key = ItemKeyValue.Key;
            string TypeName = Key.Name;
            if (TypeName.StartsWith("Pg"))
                TypeName = TypeName.Substring(2);
            else if (TypeName.StartsWith("Json"))
                TypeName = TypeName.Substring(4);

            DataTemplate Result = FindTemplate(element, $"LinkBack{TypeName}Template");
            if (Result != null)
                return Result;
            else
                return null;
        }
    }
}
