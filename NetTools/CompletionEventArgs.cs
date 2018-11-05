using System;

namespace NetTools
{
    public class CompletionEventArgs : EventArgs
    {
        public CompletionEventArgs(DatabaseOperation operation)
        {
            Operation = operation;
        }

        public DatabaseOperation Operation { get; private set; }
    }
}
