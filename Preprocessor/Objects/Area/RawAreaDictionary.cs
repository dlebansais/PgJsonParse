namespace Preprocessor;

using System.Collections.Generic;

internal class RawAreaDictionary : Dictionary<string, RawArea>, IDictionaryValueBuilder<RawArea, RawArea>
{
    public RawArea ToItem(RawArea fromRawArea) => fromRawArea;
    public RawArea ToRawItem(RawArea fromRawArea) => fromRawArea;
}
