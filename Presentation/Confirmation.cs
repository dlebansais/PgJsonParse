#if CSHTML5
using Windows.UI.Xaml;

namespace Presentation
{
    public static class Confirmation
    {
        public static MessageBoxResult Show(string messageBoxText, string caption, bool allowCancel, ConfirmationType type)
        {
            return MessageBox.Show(messageBoxText, caption, allowCancel ? MessageBoxButton.OKCancel : MessageBoxButton.OK);
        }
    }
}
#else
using System;
using System.Windows;

namespace Presentation
{
    public static class Confirmation
    {
        public static MessageBoxResult Show(string messageBoxText, string caption, bool allowCancel, ConfirmationType type)
        {
            MessageBoxImage image;

            switch (type)
            {
                case ConfirmationType.None:
                    image = MessageBoxImage.None;
                    break;
                case ConfirmationType.Info:
                    image = MessageBoxImage.Information;
                    break;
                case ConfirmationType.Warning:
                    image = MessageBoxImage.Warning;
                    break;
                case ConfirmationType.Error:
                    image = MessageBoxImage.Error;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return MessageBox.Show(messageBoxText, caption, allowCancel ? MessageBoxButton.OKCancel : MessageBoxButton.OK,  image);
        }
    }
}
#endif
