namespace Preprocessor;

using System.Collections.Generic;

internal class LoreBookDictionary : Dictionary<int, LoreBook>, IDictionaryValueBuilder<LoreBook, LoreBook>
{
    public LoreBook FromRaw(LoreBook fromRawLoreBook) => fromRawLoreBook;
    public LoreBook ToRaw(LoreBook fromRawLoreBook) => fromRawLoreBook;
}
