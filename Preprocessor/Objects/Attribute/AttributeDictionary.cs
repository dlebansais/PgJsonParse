namespace Preprocessor;

using System.Collections.Generic;

internal class AttributeDictionary : Dictionary<string, Attribute>, IDictionaryValueBuilder<Attribute, Attribute>
{
    public Attribute ToItem(Attribute fromRawAttribute) => fromRawAttribute;
    public Attribute ToRawItem(Attribute fromRawAttribute) => fromRawAttribute;
}
