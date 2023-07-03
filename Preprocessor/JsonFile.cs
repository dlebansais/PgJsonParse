﻿namespace Preprocessor;

using System;

internal record JsonFile(string FileName, bool IsPretty, Func<string, string, bool, (bool, object)> PreprocessingMethod, Action<object> FixingMethod, Action<string, object> SerializingMethod);
