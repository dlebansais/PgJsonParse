namespace Preprocessor;

using System.Collections.Generic;

internal class RawNpcDictionary : Dictionary<string, RawNpc>, IDictionaryValueBuilder<RawNpc, RawNpc>
{
    public RawNpc ToItem(RawNpc fromRawNpc) => fromRawNpc;
    public RawNpc ToRawItem(RawNpc fromRawNpc) => fromRawNpc;
}
