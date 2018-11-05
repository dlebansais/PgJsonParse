#if CSHTML5
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Presentation
{
    public abstract class DataTemplateSelector : ContentControl
    {
        public virtual DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return null;
        }

        protected DataTemplate FindTemplate(FrameworkElement element, string resourceName)
        {
            if (Application.Current.MainWindow.Content is RootControl AsRootControl)
                if (AsRootControl.Resources.Contains(resourceName))
                    return AsRootControl.Resources[resourceName] as DataTemplate;

            return null;
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            ContentTemplate = SelectTemplate(newContent, this);
        }
    }
}
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Presentation
{
    public abstract class DataTemplateSelector : ContentControl
    {
        public virtual DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return null;
        }

        protected DataTemplate FindTemplate(FrameworkElement element, string resourceName)
        {
            while (element != null)
            {
                if (element.Resources.Contains(resourceName))
                    return element.Resources[resourceName] as DataTemplate;

                TabControl AsTabControl;
                if ((AsTabControl = element as TabControl) != null)
                {
                    foreach (TabItem TabItem in AsTabControl.Items)
                    {
                        if (TabItem.Resources.Contains(resourceName))
                            return TabItem.Resources[resourceName] as DataTemplate;
                    }
                }

                FrameworkElement Parent = VisualTreeHelper.GetParent(element) as FrameworkElement;
                if (Parent != null)
                    element = Parent;
                else
                    element = null;
            }

            return null;
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            ContentTemplate = SelectTemplate(newContent, this);
        }
    }
}
#endif
