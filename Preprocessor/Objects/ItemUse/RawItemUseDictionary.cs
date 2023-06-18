namespace Preprocessor;

using System.Collections.Generic;

internal class RawItemUseDictionary : Dictionary<int, RawItemUse>, IDictionaryValueBuilder<RawItemUse, RawItemUse>
{
    public RawItemUse ToItem(RawItemUse fromRawItemUse) => fromRawItemUse;
    public RawItemUse ToRawItem(RawItemUse fromRawItemUse) => fromRawItemUse;
}
