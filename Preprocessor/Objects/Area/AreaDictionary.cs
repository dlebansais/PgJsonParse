namespace Preprocessor;

using System.Collections.Generic;

internal class AreaDictionary : Dictionary<string, Area>, IDictionaryValueBuilder<Area, Area>
{
    public Area FromRaw(Area area) => area;
    public Area ToRaw(Area area) => area;
}
