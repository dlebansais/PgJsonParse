namespace Preprocessor;

using System.Collections.Generic;

internal class AttributeDictionary : Dictionary<string, Attribute>, IDictionaryValueBuilder<Attribute, Attribute>
{
    public Attribute FromRaw(Attribute fromRawAttribute) => fromRawAttribute;
    public Attribute ToRaw(Attribute fromRawAttribute) => fromRawAttribute;
}
