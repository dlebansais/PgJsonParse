namespace Preprocessor;

using System.Collections.Generic;

public class ItemUseDictionary : Dictionary<int, ItemUse>, IDictionaryValueBuilder<ItemUse, ItemUse>
{
    public ItemUse FromRaw(ItemUse itemUse) => itemUse;
    public ItemUse ToRaw(ItemUse itemUse) => itemUse;
}
