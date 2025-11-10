namespace Preprocessor;

using System.Collections.Generic;

public class SourceAbilityDictionary : Dictionary<int, SourceAbility>, IDictionaryValueBuilderInt<SourceAbility, RawSourceAbility>
{
    public SourceAbility FromRaw(int key, RawSourceAbility rawSourceAbility) => new(key, rawSourceAbility);
    public RawSourceAbility ToRaw(SourceAbility sourceAbility) => sourceAbility.ToRawSourceAbility();
}
