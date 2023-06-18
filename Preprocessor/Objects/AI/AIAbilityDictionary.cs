namespace Preprocessor;

using System.Collections.Generic;

internal class AIAbilityDictionary : Dictionary<string, AIAbility>, IDictionaryValueBuilder<AIAbility, RawAIAbility>
{
    public AIAbility FromRaw(RawAIAbility fromRawAIAbility1) => new(fromRawAIAbility1);
    public RawAIAbility ToRaw(AIAbility fromRawAIAbility) => fromRawAIAbility.ToRawAIAbility();
}
