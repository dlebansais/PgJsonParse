namespace Preprocessor;

using System.Collections.Generic;

internal class RawItemDictionary : Dictionary<int, RawItem>, IDictionaryValueBuilder<RawItem, RawItem>
{
    public RawItem ToItem(RawItem fromRawItem) => fromRawItem;
    public RawItem ToRawItem(RawItem fromRawItem) => fromRawItem;
}
