namespace Preprocessor;

using System.Collections.Generic;

internal class ItemDictionary : Dictionary<int, Item>, IDictionaryValueBuilder<Item, Item>
{
    public Item FromRaw(Item item) => item;
    public Item ToRaw(Item item) => item;
}
