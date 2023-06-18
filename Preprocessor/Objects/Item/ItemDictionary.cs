namespace Preprocessor;

using System.Collections.Generic;

internal class ItemDictionary : Dictionary<int, Item>, IDictionaryValueBuilder<Item, Item>
{
    public Item FromRaw(Item fromRawItem) => fromRawItem;
    public Item ToRaw(Item fromRawItem) => fromRawItem;
}
