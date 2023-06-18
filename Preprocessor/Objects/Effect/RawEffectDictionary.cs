namespace Preprocessor;

using System.Collections.Generic;

internal class RawEffectDictionary : Dictionary<string, RawEffect>, IDictionaryValueBuilder<RawEffect, RawEffect1>
{
    public RawEffect ToItem(RawEffect1 fromRawEffect1) => new(fromRawEffect1);
    public RawEffect1 ToRawItem(RawEffect fromRawEffect) => fromRawEffect.ToRawEffect1();
}
