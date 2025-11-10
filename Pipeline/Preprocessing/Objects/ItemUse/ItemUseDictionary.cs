namespace Preprocessor;

using System.Collections.Generic;

public class ItemUseDictionary : Dictionary<int, ItemUse>, IDictionaryValueBuilderInt<ItemUse, ItemUse>
{
    public ItemUse FromRaw(int key, ItemUse itemUse) => itemUse;
    public ItemUse ToRaw(ItemUse itemUse) => itemUse;
}
