namespace Preprocessor;

using System.Collections.Generic;

public class AttributeDictionary : Dictionary<string, Attribute>, IDictionaryValueBuilderString<Attribute, RawAttribute>
{
    public Attribute FromRaw(string key, RawAttribute rawAttribute) => new(key, rawAttribute);
    public RawAttribute ToRaw(Attribute attribute) => attribute.ToRawAttribute();
}
