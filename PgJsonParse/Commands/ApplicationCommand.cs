using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace PgJsonParse
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
