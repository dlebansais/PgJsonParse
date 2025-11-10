namespace Preprocessor;

using System.Collections.Generic;

public class LoreBookCategoryDictionary : Dictionary<string, LoreBookCategory>, IDictionaryValueBuilderString<LoreBookCategory, RawLoreBookCategory>
{
    public LoreBookCategory FromRaw(string key, RawLoreBookCategory rawLoreBookCategory) => new(key, rawLoreBookCategory);
    public RawLoreBookCategory ToRaw(LoreBookCategory loreBookCategory) => loreBookCategory.ToRawLoreBookCategory();
}
