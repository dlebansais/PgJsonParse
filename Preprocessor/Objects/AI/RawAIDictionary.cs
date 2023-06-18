namespace Preprocessor;

using System.Collections.Generic;

internal class RawAIDictionary : Dictionary<string, RawAI>, IDictionaryValueBuilder<RawAI, RawAI>
{
    public RawAI ToItem(RawAI fromRawAI) => fromRawAI;
    public RawAI ToRawItem(RawAI fromRawAI) => fromRawAI;
}
