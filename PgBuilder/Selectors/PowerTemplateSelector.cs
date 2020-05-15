namespace Selectors
{
    using System.Windows.Controls;
    using System.Windows;
    using PgBuilder;
    using System.Windows.Media;

    public class PowerTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement Element = (FrameworkElement)container;
            GetMainWindowParent(Element, out MainWindow MainWindowParent);
            Power PowerItem = (Power)item;

            FrameworkElement ResourceElement;
            if (MainWindowParent != null)
                ResourceElement = MainWindowParent;
            else
                ResourceElement = Element;

            DataTemplate Result;
            if (PowerItem.IsFirst && ResourceElement == Element)
                Result = (DataTemplate)ResourceElement.FindResource("ModNameFirstTemplate");
            else
                Result = (DataTemplate)ResourceElement.FindResource("ModNameTemplate");

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
