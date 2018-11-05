#if CSHTML5
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Presentation
{
    public class PresentationEnvironment
    {
        public static string UserRootFolder { get { return "ApplicationData"; } }

        public static FrameworkElement EnumChildren(DependencyObject root, string targetName)
        {
            if (root == null)
                return null;

            FrameworkElement AsFrameworkElement = root as FrameworkElement;
            if (AsFrameworkElement != null && AsFrameworkElement.Name == targetName)
                return AsFrameworkElement;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
            {
                FrameworkElement child = EnumChildren(VisualTreeHelper.GetChild(root, i), targetName);
                if (child != null)
                    return child;
            }

            return null;
        }
    }
}
#else
using System;
using System.Windows;
using System.Windows.Media;

namespace Presentation
{
    public class PresentationEnvironment
    {
        public static string UserRootFolder { get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); } }

        public static FrameworkElement EnumChildren(DependencyObject root, string targetName)
        {
            if (root == null)
                return null;

            FrameworkElement AsFrameworkElement = root as FrameworkElement;
            if (AsFrameworkElement != null && AsFrameworkElement.Name == targetName)
                return AsFrameworkElement;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
            {
                FrameworkElement child = EnumChildren(VisualTreeHelper.GetChild(root, i), targetName);
                if (child != null)
                    return child;
            }

            return null;
        }
    }
}
#endif
