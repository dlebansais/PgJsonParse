using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PgJsonParse
{
    public class ListMemberSelector : DataTemplateSelector
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
                Result = element.TryFindResource("ItemTemplateFirst") as DataTemplate;
                if (Result != null)
                    return Result;
            }

            if (IsLast)
            {
                Result = element.TryFindResource("ItemTemplateLast") as DataTemplate;
                if (Result != null)
                    return Result;
            }

            Result = element.TryFindResource("ItemTemplate") as DataTemplate;
            return Result;
        }
    }
}
