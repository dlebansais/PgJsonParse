namespace Preprocessor;

using System.Collections.Generic;

public class AttributeDictionary : Dictionary<string, Attribute>, IDictionaryValueBuilder<Attribute, Attribute>
{
    public Attribute FromRaw(Attribute attribute) => attribute;
    public Attribute ToRaw(Attribute attribute) => attribute;
}
