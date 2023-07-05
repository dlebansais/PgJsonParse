namespace Preprocessor;

using System.Collections.Generic;

public class PowerDictionary : Dictionary<int, Power>, IDictionaryValueBuilder<Power, RawPower>
{
    public Power FromRaw(RawPower rawPower) => new Power(rawPower);
    public RawPower ToRaw(Power power) => power.ToRawPower();
}
