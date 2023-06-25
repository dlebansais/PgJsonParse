﻿namespace Preprocessor;

using System.Collections.Generic;

internal class ItemDictionary : Dictionary<int, Item>, IDictionaryValueBuilder<Item, RawItem>
{
    public Item FromRaw(RawItem rawItem) => new(rawItem);
    public RawItem ToRaw(Item item) => item.ToRawItem();
}
