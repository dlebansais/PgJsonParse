namespace Preprocessor;

using System.Collections.Generic;

public class SourceItemDictionary : Dictionary<int, SourceItem>, IDictionaryValueBuilderInt<SourceItem, RawSourceItem>
{
    public SourceItem FromRaw(int key, RawSourceItem rawSourceItem) => new(key, rawSourceItem);
    public RawSourceItem ToRaw(SourceItem sourceItem) => sourceItem.ToRawSourceItem();
}
