namespace Preprocessor;

using System.Collections.Generic;

public class ItemDictionary : Dictionary<int, Item>, IDictionaryValueBuilderInt<Item, RawItem>
{
    public Item FromRaw(int key, RawItem rawItem) => new(key, rawItem);
    public RawItem ToRaw(Item item) => item.ToRawItem();
}
