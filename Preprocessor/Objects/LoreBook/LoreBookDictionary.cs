namespace Preprocessor;

using System.Collections.Generic;

internal class LoreBookDictionary : Dictionary<int, LoreBook>, IDictionaryValueBuilder<LoreBook, LoreBook>
{
    public LoreBook ToItem(LoreBook fromRawLoreBook) => fromRawLoreBook;
    public LoreBook ToRawItem(LoreBook fromRawLoreBook) => fromRawLoreBook;
}
