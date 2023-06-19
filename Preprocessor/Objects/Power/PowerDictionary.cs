namespace Preprocessor;

using System.Collections.Generic;

internal class PowerDictionary : Dictionary<int, Power>, IDictionaryValueBuilder<Power, Power>
{
    public Power FromRaw(Power item) => item;
    public Power ToRaw(Power item) => item;
}
