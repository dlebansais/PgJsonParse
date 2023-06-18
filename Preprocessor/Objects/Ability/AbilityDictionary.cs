namespace Preprocessor;

using System.Collections.Generic;

internal class AbilityDictionary : Dictionary<int, Ability>, IDictionaryValueBuilder<Ability, Ability1>
{
    public Ability ToItem(Ability1 fromRawAbility1) => new(fromRawAbility1);
    public Ability1 ToRawItem(Ability fromRawAbility) => fromRawAbility.ToRawAbility1();
}
