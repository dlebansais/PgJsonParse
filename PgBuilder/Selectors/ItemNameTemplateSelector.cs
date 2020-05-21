namespace Selectors
{
    using System.Windows.Controls;
    using System.Windows;
    using PgBuilder;
    using System.Windows.Media;

    public class ItemNameTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement ResourceElement = (FrameworkElement)container;
            ItemInfo ItemInfo = (ItemInfo)item;

            DataTemplate Result;
            if (ItemInfo == ItemInfo.NoItem)
                Result = (DataTemplate)ResourceElement.FindResource("ItemNameNoItemTemplate");
            else
                Result = (DataTemplate)ResourceElement.FindResource("ItemNameTemplate");

            return Result;
        }

        public bool GetMainWindowParent(FrameworkElement element, out MainWindow parent)
        {
            if (element is MainWindow MainWindowParent)
            {
                parent = MainWindowParent;
                return true;
            }
            else if (VisualTreeHelper.GetParent(element) is FrameworkElement ParentElement)
                return GetMainWindowParent(ParentElement, out parent);
            else
            {
                parent = null;
                return false;
            }
        }
    }
}
