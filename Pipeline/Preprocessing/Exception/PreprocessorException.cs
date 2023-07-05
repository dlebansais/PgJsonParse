namespace Preprocessor;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

public class PreprocessorException : Exception
{
    public PreprocessorException(string message)
        : base(message)
    {
    }

    public PreprocessorException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void Throw([CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        throw new PreprocessorException($"Aborting at {memberName}, line {lineNumber}");
    }

    public static void Throw(object reference, [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
    {
        throw new PreprocessorException($"Aborting at {reference.GetType().Name}::{memberName}, line {lineNumber}");
    }

    [DoesNotReturn]
    public static void Rethrow(object key, Exception innerException)
    {
        throw new PreprocessorException($"Failed at key: {key}", innerException);
    }
}
