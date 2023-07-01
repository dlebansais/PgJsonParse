namespace Preprocessor;

using System.Collections.Generic;

internal class SourceAbilityDictionary : Dictionary<int, SourceAbility>, IDictionaryValueBuilder<SourceAbility, RawSourceAbility>
{
    public SourceAbility FromRaw(RawSourceAbility rawSourceAbility) => new(rawSourceAbility);
    public RawSourceAbility ToRaw(SourceAbility sourceAbility) => sourceAbility.ToRawSourceAbility();
}
