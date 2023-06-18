namespace Preprocessor;

using System.Collections.Generic;

internal class ItemUseDictionary : Dictionary<int, ItemUse>, IDictionaryValueBuilder<ItemUse, ItemUse>
{
    public ItemUse FromRaw(ItemUse fromRawItemUse) => fromRawItemUse;
    public ItemUse ToRaw(ItemUse fromRawItemUse) => fromRawItemUse;
}
