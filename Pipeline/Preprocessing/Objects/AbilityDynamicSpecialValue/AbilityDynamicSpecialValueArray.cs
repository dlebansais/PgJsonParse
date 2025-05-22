namespace Preprocessor;

using System.Collections.Generic;

public class AbilityDynamicSpecialValueArray : List<AbilityDynamicSpecialValue>, IDictionaryValueBuilder<AbilityDynamicSpecialValue, RawAbilityDynamicSpecialValue>
{
    public AbilityDynamicSpecialValue FromRaw(RawAbilityDynamicSpecialValue rawAbilityDynamicSpecialValues) => new(rawAbilityDynamicSpecialValues);
    public RawAbilityDynamicSpecialValue ToRaw(AbilityDynamicSpecialValue abilityDynamicSpecialValues) => abilityDynamicSpecialValues.ToRawAbilityDynamicSpecialValue();
}
