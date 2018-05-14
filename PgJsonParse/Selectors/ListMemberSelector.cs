﻿#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endif

namespace PgJsonParse
{
    public class ListMemberSelector : Presentation.DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            FrameworkElement parent = element;
            bool IsFirst = false;
            bool IsLast = false;

            for (;;)
            {
                parent = VisualTreeHelper.GetParent(parent) as FrameworkElement;
                if (parent == null)
                    break;

                ItemsControl ParentList;
                if ((ParentList = parent as ItemsControl) != null)
                {
                    int ItemIndex = ParentList.Items.IndexOf(item);
                    IsFirst = (ItemIndex == 0);
                    IsLast = (ItemIndex == ParentList.Items.Count - 1);
                    break;
                }
            }

            DataTemplate Result;

            if (IsFirst)
            {
                Result = TryFindResource(element, "ItemTemplateFirst") as DataTemplate;
                if (Result != null)
                    return Result;
            }

            if (IsLast)
            {
                Result = TryFindResource(element, "ItemTemplateLast") as DataTemplate;
                if (Result != null)
                    return Result;
            }

            Result = TryFindResource(element, "ItemTemplate") as DataTemplate;
            if (Result != null)
                return Result;
            else
                return base.SelectTemplate(item, container);
        }
    }
}
