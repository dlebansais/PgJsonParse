namespace Preprocessor;

using System.Collections.Generic;

internal class AIAbilityDictionary : Dictionary<string, AIAbility>, IDictionaryValueBuilder<AIAbility, AIAbility1>
{
    public AIAbility ToItem(AIAbility1 fromRawAIAbility1) => new(fromRawAIAbility1);
    public AIAbility1 ToRawItem(AIAbility fromRawAIAbility) => fromRawAIAbility.ToRawAIAbility1();
}
