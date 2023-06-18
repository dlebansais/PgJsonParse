namespace Preprocessor;

using System.Collections.Generic;

internal class LoreBookCategoryDictionary : Dictionary<string, LoreBookCategory>, IDictionaryValueBuilder<LoreBookCategory, LoreBookCategory>
{
    public LoreBookCategory ToItem(LoreBookCategory fromRawLoreBookCategory) => fromRawLoreBookCategory;
    public LoreBookCategory ToRawItem(LoreBookCategory fromRawLoreBookCategory) => fromRawLoreBookCategory;
}
