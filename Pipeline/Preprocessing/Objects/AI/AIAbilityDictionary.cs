namespace Preprocessor;

using System.Collections.Generic;

public class AIAbilityDictionary : Dictionary<string, AIAbility>, IDictionaryValueBuilderString<AIAbility, RawAIAbility>
{
    public AIAbility FromRaw(string key, RawAIAbility rawAIAbility) => new(key, rawAIAbility);
    public RawAIAbility ToRaw(AIAbility aiAbility) => aiAbility.ToRawAIAbility();
}
