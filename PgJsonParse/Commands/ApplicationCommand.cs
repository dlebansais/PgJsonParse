using System;
using System.Windows;
using System.Windows.Input;

namespace PgJsonParse
{
    public class ApplicationCommand : ICommand
    {
        public static void Subscribe(FrameworkElement element, string resourceName, EventHandler handler)
        {
            ApplicationCommand Command = element.Resources[resourceName] as ApplicationCommand;
            Command.Subscribe(handler);
        }

        private void Subscribe(EventHandler handler)
        {
            Executed += handler;

            IsSubscribed = true;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
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
