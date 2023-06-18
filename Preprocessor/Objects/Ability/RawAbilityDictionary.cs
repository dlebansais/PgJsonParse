namespace Preprocessor;

using System.Collections.Generic;

internal class RawAbilityDictionary : Dictionary<int, RawAbility>, IDictionaryValueBuilder<RawAbility, RawAbility1>
{
    public RawAbility ToItem(RawAbility1 fromRawAbility1) => new(fromRawAbility1);
    public RawAbility1 ToRawItem(RawAbility fromRawAbility) => fromRawAbility.ToRawAbility1();
}
