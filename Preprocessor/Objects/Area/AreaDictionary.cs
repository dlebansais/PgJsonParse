namespace Preprocessor;

using System.Collections.Generic;

internal class AreaDictionary : Dictionary<string, Area>, IDictionaryValueBuilder<Area, Area>
{
    public Area FromRaw(Area fromRawArea) => fromRawArea;
    public Area ToRaw(Area fromRawArea) => fromRawArea;
}
