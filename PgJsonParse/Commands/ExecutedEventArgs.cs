using System;

namespace PgJsonParse
{
    public class ExecutedEventArgs : EventArgs
    {
        public ExecutedEventArgs(object parameter)
        {
            Parameter = parameter;
        }

        public object Parameter { get; }
    }
}
