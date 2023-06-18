namespace Preprocessor;

using System.Collections.Generic;

internal class RawPlayerTitleDictionary : Dictionary<int, RawPlayerTitle>, IDictionaryValueBuilder<RawPlayerTitle, RawPlayerTitle>
{
    public RawPlayerTitle ToItem(RawPlayerTitle fromRawPlayerTitle) => fromRawPlayerTitle;
    public RawPlayerTitle ToRawItem(RawPlayerTitle fromRawPlayerTitle) => fromRawPlayerTitle;
}
