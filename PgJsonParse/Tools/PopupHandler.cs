using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Tools
{
    public static class PopupHandler
    {
        public static void SetOpenPopupButton(ToggleButton Button)
        {
            OpenPopupButton = Button;
        }

        public static void ClearOpenPopupButton()
        {
            OpenPopupButton = null;
        }

        public static void OnPopupClosed(FrameworkElement inputElement)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Mouse.AddMouseUpHandler(inputElement, OnMouseUp);
                Mouse.Capture(inputElement);
            }
            else
                inputElement.Dispatcher.BeginInvoke(new Action(ClosePopups));
        }

        private static void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement inputElement = sender as FrameworkElement;
            OnMouseUp(inputElement);
            e.Handled = false;
        }

        public static void OnMouseUp(FrameworkElement inputElement)
        {
            Mouse.RemoveMouseUpHandler(inputElement, OnMouseUp);
            Mouse.Capture(null);
            inputElement.Dispatcher.BeginInvoke(new Action(ClosePopups));
        }

        public static void OnPopupSelectionChanged()
        {
            ClosePopups();
        }

        public static void OnPopupMouseLeftButtonUp()
        {
            ClosePopups();
        }

        public static void ClosePopups()
        {
            if (OpenPopupButton != null && OpenPopupButton.IsChecked.HasValue && OpenPopupButton.IsChecked.Value == true)
            {
                OpenPopupButton.IsChecked = false;
                ClearOpenPopupButton();
            }
        }

        private static ToggleButton OpenPopupButton;
    }
}
