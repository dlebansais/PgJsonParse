namespace Preprocessor;

using System.Collections.Generic;

internal class AIAbilityDictionary : Dictionary<string, AIAbility>, IDictionaryValueBuilder<AIAbility, RawAIAbility>
{
    public AIAbility FromRaw(RawAIAbility rawAIAbility) => new(rawAIAbility);
    public RawAIAbility ToRaw(AIAbility aiAbility) => aiAbility.ToRawAIAbility();
}
