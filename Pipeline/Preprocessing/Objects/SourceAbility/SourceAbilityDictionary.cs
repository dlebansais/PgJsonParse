namespace Preprocessor;

using System.Collections.Generic;

public class SourceAbilityDictionary : Dictionary<int, SourceAbility>, IDictionaryValueBuilder<SourceAbility, RawSourceAbility>
{
    public SourceAbility FromRaw(RawSourceAbility rawSourceAbility) => new(rawSourceAbility);
    public RawSourceAbility ToRaw(SourceAbility sourceAbility) => sourceAbility.ToRawSourceAbility();
}
