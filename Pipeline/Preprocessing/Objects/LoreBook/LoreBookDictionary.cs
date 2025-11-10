namespace Preprocessor;

using System.Collections.Generic;

public class LoreBookDictionary : Dictionary<int, LoreBook>, IDictionaryValueBuilderInt<LoreBook, LoreBook>
{
    public LoreBook FromRaw(int key, LoreBook loreBook) => loreBook;
    public LoreBook ToRaw(LoreBook loreBook) => loreBook;
}
