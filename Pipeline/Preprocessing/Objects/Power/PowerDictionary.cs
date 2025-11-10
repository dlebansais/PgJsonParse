namespace Preprocessor;

using System.Collections.Generic;

public class PowerDictionary : Dictionary<int, Power>, IDictionaryValueBuilderInt<Power, RawPower>
{
    public Power FromRaw(int key, RawPower rawPower) => new(key, rawPower);
    public RawPower ToRaw(Power power) => power.ToRawPower();
}
