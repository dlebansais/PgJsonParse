namespace Preprocessor;

using System.Collections.Generic;

public class SourceItemDictionary : Dictionary<int, SourceItem>, IDictionaryValueBuilder<SourceItem, RawSourceItem>
{
    public SourceItem FromRaw(RawSourceItem rawSourceItem) => new(rawSourceItem);
    public RawSourceItem ToRaw(SourceItem sourceItem) => sourceItem.ToRawSourceItem();
}
