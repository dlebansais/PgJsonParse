namespace Preprocessor;

using System.Collections.Generic;

public class AreaDictionary : Dictionary<string, Area>, IDictionaryValueBuilderString<Area, Area>
{
    public Area FromRaw(string key, Area area) => new(key, area);
    public Area ToRaw(Area area) => area;
}
