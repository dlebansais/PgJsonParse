namespace Preprocessor;

using System.Collections.Generic;

internal class LoreBookCategoryDictionary : Dictionary<string, LoreBookCategory>, IDictionaryValueBuilder<LoreBookCategory, LoreBookCategory>
{
    public LoreBookCategory FromRaw(LoreBookCategory fromRawLoreBookCategory) => fromRawLoreBookCategory;
    public LoreBookCategory ToRaw(LoreBookCategory fromRawLoreBookCategory) => fromRawLoreBookCategory;
}
