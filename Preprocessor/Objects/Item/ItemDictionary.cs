namespace Preprocessor;

using System.Collections.Generic;

internal class ItemDictionary : Dictionary<int, Item>, IDictionaryValueBuilder<Item, Item>
{
    public Item ToItem(Item fromRawItem) => fromRawItem;
    public Item ToRawItem(Item fromRawItem) => fromRawItem;
}
