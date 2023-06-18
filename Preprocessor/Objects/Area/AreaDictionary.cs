namespace Preprocessor;

using System.Collections.Generic;

internal class AreaDictionary : Dictionary<string, Area>, IDictionaryValueBuilder<Area, Area>
{
    public Area ToItem(Area fromRawArea) => fromRawArea;
    public Area ToRawItem(Area fromRawArea) => fromRawArea;
}
