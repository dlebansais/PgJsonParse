#if CSHARP_XAML_FOR_HTML5
using System;
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
        private static int ItemIndex = -1;
        private static int ItemCount = -1;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            FrameworkElement parent = element;
            bool IsFirst = false;
            bool IsLast = false;
            string TemplateHostName = "";

            if (!string.IsNullOrEmpty(Name))
            {
                TemplateHostName = Name;

                if (ItemIndex < 0 || ItemCount < 0)
                    IsFirst = true;
                else if (ItemIndex + 1 >= ItemCount)
                    IsLast = true;
            }

            for (;;)
            {
                parent = VisualTreeHelper.GetParent(parent) as FrameworkElement;
                if (parent == null)
                    break;

                ItemsControl ParentList;
                if ((ParentList = parent as ItemsControl) != null)
                {
                    if (string.IsNullOrEmpty(TemplateHostName) && !string.IsNullOrEmpty(parent.Name) && parent.Name.StartsWith("ctrl_"))
                        TemplateHostName = parent.Name.Substring(5);

                    ItemIndex = ParentList.Items.IndexOf(item);
                    IsFirst = (ItemIndex == 0);

                    if (IsFirst)
                        ItemCount = ParentList.Items.Count;

                    IsLast = (ItemIndex == ParentList.Items.Count - 1);
                    if (IsLast)
                    {
                        if (ItemCount == 1)
                            IsLast = false;

                        ItemIndex = -1;
                        ItemCount = -1;
                    }
                    else
                        ItemIndex++;
                    break;
                }
            }

            DataTemplate Result;

            if (IsFirst)
            {
                Result = FindTemplate(element, TemplateHostName + "_" + "ItemTemplateFirst");
                if (Result != null)
                    return Result;
            }

            if (IsLast)
            {
                Result = FindTemplate(element, TemplateHostName + "_" + "ItemTemplateLast");
                if (Result != null)
                    return Result;
            }

            Result = FindTemplate(element, TemplateHostName + "_" + "ItemTemplate");
            if (Result != null)
                return Result;
            else
                return base.SelectTemplate(item, container);
        }
    }
}
