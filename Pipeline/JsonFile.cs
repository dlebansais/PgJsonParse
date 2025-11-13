namespace Preprocessor;

using System;
using System.Collections.Generic;

public record JsonFile(string FileName,
                       bool IsPretty,
                       Func<string, string, bool, (bool, object)> PreprocessingMethod,
                       Action<object> FixingMethod,
                       Action<string, object> SerializingMethod,
                       Action<IFreeSql, object> DatabaseInsertMethod,
                       List<Action<IFreeSql, List<object>>> DatabaseAdditionalInserters,
                       Action<object> DatabaseFixingMethod,
                       Func<IFreeSql, object, object> DatabaseSelectMethod,
                       List<Action<IFreeSql, List<object>, bool>> DatabaseAdditionalSelectors);
