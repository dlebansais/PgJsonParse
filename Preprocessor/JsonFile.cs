namespace Preprocessor;

using System;

internal record JsonFile(string FileName, bool IsPretty, Func<string, bool, bool> PreprocessingMethod);
