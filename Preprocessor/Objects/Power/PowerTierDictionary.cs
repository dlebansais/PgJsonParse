namespace Preprocessor;

using System.Collections.Generic;

internal class PowerTierDictionary : Dictionary<int, PowerTier>, IDictionaryValueBuilder<PowerTier, PowerTier>
{
    public PowerTier FromRaw(PowerTier item) => item;
    public PowerTier ToRaw(PowerTier item) => item;
}
