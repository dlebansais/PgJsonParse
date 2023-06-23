namespace Preprocessor;

using System;

internal record JsonFile(string FileName, bool IsPretty, Func<string, bool, (bool, object)> PreprocessingMethod, Action<string, object> SerializingMethod);
