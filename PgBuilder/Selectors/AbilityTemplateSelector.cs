namespace Selectors
{
    using System.Windows.Controls;
    using System.Windows;
    using PgBuilder;
    using System.Windows.Media;

    public class AbilityTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement Element = (FrameworkElement)container;
            GetMainWindowParent(Element, out MainWindow MainWindowParent);

            DataTemplate Result;
            if (MainWindowParent.IsLargeView)
                Result = (DataTemplate)MainWindowParent.FindResource("AbilityBigImageTemplate");
            else
                Result = (DataTemplate)MainWindowParent.FindResource("AbilitySlotTemplate");

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
