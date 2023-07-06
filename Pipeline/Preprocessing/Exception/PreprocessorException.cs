namespace Preprocessor;

using System;
using System.Runtime.CompilerServices;

public class PreprocessorException : Exception
{
    public PreprocessorException([CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        : base($"Aborting at {memberName}, line {lineNumber}")
    {
    }

    public PreprocessorException(object reference, [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        : base($"Aborting at {reference.GetType().Name}::{memberName}, line {lineNumber}")
    {
    }

    public PreprocessorException(object key, Exception innerException)
        : base($"Failed at key: {key}", innerException)
    {
    }
}
