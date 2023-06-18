namespace Preprocessor;

using System.Collections.Generic;

internal class ItemUseDictionary : Dictionary<int, ItemUse>, IDictionaryValueBuilder<ItemUse, ItemUse>
{
    public ItemUse ToItem(ItemUse fromRawItemUse) => fromRawItemUse;
    public ItemUse ToRawItem(ItemUse fromRawItemUse) => fromRawItemUse;
}
