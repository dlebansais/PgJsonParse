namespace Preprocessor;

using System.Collections.Generic;

internal class RawLoreBookCategoryDictionary : Dictionary<string, RawLoreBookCategory>, IDictionaryValueBuilder<RawLoreBookCategory, RawLoreBookCategory>
{
    public RawLoreBookCategory ToItem(RawLoreBookCategory fromRawLoreBookCategory) => fromRawLoreBookCategory;
    public RawLoreBookCategory ToRawItem(RawLoreBookCategory fromRawLoreBookCategory) => fromRawLoreBookCategory;
}
