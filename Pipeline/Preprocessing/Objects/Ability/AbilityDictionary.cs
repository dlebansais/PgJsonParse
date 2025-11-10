namespace Preprocessor;

using System.Collections.Generic;

public class AbilityDictionary : Dictionary<int, Ability>, IDictionaryValueBuilderInt<Ability, RawAbility>
{
    public Ability FromRaw(int key, RawAbility rawAbility) => new(key, rawAbility);
    public RawAbility ToRaw(Ability ability) => ability.ToRawAbility();
}
