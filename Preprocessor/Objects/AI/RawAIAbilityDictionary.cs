namespace Preprocessor;

using System.Collections.Generic;

internal class RawAIAbilityDictionary : Dictionary<string, RawAIAbility>, IDictionaryValueBuilder<RawAIAbility, RawAIAbility1>
{
    public RawAIAbility ToItem(RawAIAbility1 fromRawAIAbility1) => new(fromRawAIAbility1);
    public RawAIAbility1 ToRawItem(RawAIAbility fromRawAIAbility) => fromRawAIAbility.ToRawAIAbility1();
}
