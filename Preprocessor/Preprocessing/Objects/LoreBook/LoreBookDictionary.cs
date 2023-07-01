﻿namespace Preprocessor;

using System.Collections.Generic;

internal class LoreBookDictionary : Dictionary<int, LoreBook>, IDictionaryValueBuilder<LoreBook, LoreBook>
{
    public LoreBook FromRaw(LoreBook loreBook) => loreBook;
    public LoreBook ToRaw(LoreBook loreBook) => loreBook;
}