#if CSHTML5
using System;
using System.Diagnostics;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Presentation
{
    public class ApplicationCommand : ICommand
    {
        public static void SubscribeToGlobalCommand(string resourceName, EventHandler handler)
        {
            ApplicationCommand Command = Application.Current.Resources[resourceName] as ApplicationCommand;
            Command.Subscribe(handler);
        }

        public static void SubscribeToGuiCommand(string resourceName, EventHandler handler)
        {
            ApplicationCommand Command = Application.Current.Resources[resourceName] as ApplicationCommand;
            Command.Subscribe(handler);
        }

        public static void SubscribeToControlCommand(Control control, string resourceName, EventHandler handler)
        {
            ApplicationCommand Command = control.Resources[resourceName] as ApplicationCommand;
            Command.Subscribe(handler);
        }

        public void Subscribe(EventHandler handler)
        {
            Debug.Assert(!IsSubscribed);

            if (!IsSubscribed)
            {
                Executed += handler;

                IsSubscribed = true;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler CanExecuteChanged;
        public event EventHandler Executed;

        public bool CanExecute(object parameter)
        {
            return IsSubscribed;
        }

        public void Execute(object parameter)
        {
            Executed?.Invoke(this, new ExecutedEventArgs(parameter));
        }

        private bool IsSubscribed;
    }
}
#else
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Presentation
{
    public class ApplicationCommand : ICommand
    {
        public static void SubscribeToGlobalCommand(string resourceName, EventHandler handler)
        {
            ApplicationCommand Command = (ApplicationCommand)Application.Current.Resources[resourceName];
            Command.Subscribe(handler);
        }

        public static void SubscribeToGuiCommand(string resourceName, EventHandler handler)
        {
            ApplicationCommand Command = (ApplicationCommand)Application.Current.Resources[resourceName];
            Command.Subscribe(handler);
        }

        public static void SubscribeToControlCommand(Control control, string resourceName, EventHandler handler)
        {
            ApplicationCommand Command = (ApplicationCommand)control.Resources[resourceName];
            Command.Subscribe(handler);
        }

        public void Subscribe(EventHandler handler)
        {
            Debug.Assert(!IsSubscribed);

            if (!IsSubscribed)
            {
                Executed += handler;

                IsSubscribed = true;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler CanExecuteChanged;
        public event EventHandler Executed;

        public bool CanExecute(object parameter)
        {
            return IsSubscribed;
        }

        public void Execute(object parameter)
        {
            Executed?.Invoke(this, new ExecutedEventArgs(parameter));
        }

        private bool IsSubscribed;
    }
}
#endif
