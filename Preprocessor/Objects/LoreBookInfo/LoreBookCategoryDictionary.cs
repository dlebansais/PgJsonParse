namespace Preprocessor;

using System.Collections.Generic;

internal class LoreBookCategoryDictionary : Dictionary<string, LoreBookCategory>, IDictionaryValueBuilder<LoreBookCategory, LoreBookCategory>
{
    public LoreBookCategory FromRaw(LoreBookCategory loreBookCategory) => loreBookCategory;
    public LoreBookCategory ToRaw(LoreBookCategory loreBookCategory) => loreBookCategory;
}
