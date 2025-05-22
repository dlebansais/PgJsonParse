namespace Preprocessor;

using System.Collections.Generic;

public class AbilityDynamicDotArray : List<AbilityDynamicDot>, IDictionaryValueBuilder<AbilityDynamicDot, RawAbilityDynamicDot>
{
    public AbilityDynamicDot FromRaw(RawAbilityDynamicDot rawAbilityDynamicDots) => new(rawAbilityDynamicDots);
    public RawAbilityDynamicDot ToRaw(AbilityDynamicDot abilityDynamicDots) => abilityDynamicDots.ToRawAbilityDynamicDot();
}
