namespace Preprocessor;

using System.Collections.Generic;

public class AbilityKeywordArray : List<AbilityKeyword>, IDictionaryValueBuilder<AbilityKeyword, AbilityKeyword>
{
    public AbilityKeyword FromRaw(AbilityKeyword abilityKeyword) => abilityKeyword;
    public AbilityKeyword ToRaw(AbilityKeyword abilityKeyword) => abilityKeyword;
}
