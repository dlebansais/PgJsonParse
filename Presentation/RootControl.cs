using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Presentation
{
    public class RootControl : Window
    {
        public RootControl(RootControlMode mode)
        {
            switch (mode)
            {
                case RootControlMode.ResizedWithCaption:
                    break;

                case RootControlMode.CustomShape:
                    Background = Brushes.Transparent;
                    WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    ResizeMode = ResizeMode.NoResize;
                    SizeToContent = SizeToContent.WidthAndHeight;
                    WindowStyle = WindowStyle.None;
                    AllowsTransparency = true;
                    break;

                default:
                    throw new System.InvalidOperationException();
            }

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, OnClose));
        }

        public RootControlMode Mode { get; set; }

        private void OnClose(object sender, EventArgs e)
        {
            Close();
        }
    }
}
