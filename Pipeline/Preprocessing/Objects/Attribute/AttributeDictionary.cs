namespace Preprocessor;

using System.Collections.Generic;

public class AttributeDictionary : Dictionary<string, Attribute>, IDictionaryValueBuilder<Attribute, RawAttribute>
{
    public Attribute FromRaw(RawAttribute rawAttribute) => new Attribute(rawAttribute);
    public RawAttribute ToRaw(Attribute attribute) => attribute.ToRawAttribute();
}
