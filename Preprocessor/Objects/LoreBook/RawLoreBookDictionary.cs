namespace Preprocessor;

using System.Collections.Generic;

internal class RawLoreBookDictionary : Dictionary<int, RawLoreBook>, IDictionaryValueBuilder<RawLoreBook, RawLoreBook>
{
    public RawLoreBook ToItem(RawLoreBook fromRawLoreBook) => fromRawLoreBook;
    public RawLoreBook ToRawItem(RawLoreBook fromRawLoreBook) => fromRawLoreBook;
}
