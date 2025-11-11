namespace Preprocessor;

using System;

public record JsonFile(string FileName,
                       bool IsPretty,
                       Func<string, string, bool, (bool, object)> PreprocessingMethod,
                       Action<object> FixingMethod,
                       Action<string, object> SerializingMethod,
                       Action<IFreeSql, object> DatabaseInserterMethod,
                       Func<IFreeSql, object> DatabaseVerifierMethod);
