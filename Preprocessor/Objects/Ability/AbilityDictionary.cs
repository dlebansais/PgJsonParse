namespace Preprocessor;

using System.Collections.Generic;

internal class AbilityDictionary : Dictionary<int, Ability>, IDictionaryValueBuilder<Ability, RawAbility>
{
    public Ability FromRaw(RawAbility fromRawAbility1) => new(fromRawAbility1);
    public RawAbility ToRaw(Ability fromRawAbility) => fromRawAbility.ToRawAbility();
}
