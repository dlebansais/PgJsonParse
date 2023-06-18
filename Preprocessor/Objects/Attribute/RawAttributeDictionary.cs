namespace Preprocessor;

using System.Collections.Generic;

internal class RawAttributeDictionary : Dictionary<string, RawAttribute>, IDictionaryValueBuilder<RawAttribute, RawAttribute>
{
    public RawAttribute ToItem(RawAttribute fromRawAttribute) => fromRawAttribute;
    public RawAttribute ToRawItem(RawAttribute fromRawAttribute) => fromRawAttribute;
}
