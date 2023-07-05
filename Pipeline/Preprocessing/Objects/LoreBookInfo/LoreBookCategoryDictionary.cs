namespace Preprocessor;

using System.Collections.Generic;

public class LoreBookCategoryDictionary : Dictionary<string, LoreBookCategory>, IDictionaryValueBuilder<LoreBookCategory, RawLoreBookCategory>
{
    public LoreBookCategory FromRaw(RawLoreBookCategory rawLoreBookCategory) => new(rawLoreBookCategory);
    public RawLoreBookCategory ToRaw(LoreBookCategory loreBookCategory) => loreBookCategory.ToRawLoreBookCategory();
}
