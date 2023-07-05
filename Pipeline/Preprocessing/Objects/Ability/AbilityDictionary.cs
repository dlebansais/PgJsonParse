namespace Preprocessor;

using System.Collections.Generic;

public class AbilityDictionary : Dictionary<int, Ability>, IDictionaryValueBuilder<Ability, RawAbility>
{
    public Ability FromRaw(RawAbility rawAbility) => new(rawAbility);
    public RawAbility ToRaw(Ability ability) => ability.ToRawAbility();
}
