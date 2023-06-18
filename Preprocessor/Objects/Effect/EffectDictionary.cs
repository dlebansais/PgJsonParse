namespace Preprocessor;

using System.Collections.Generic;

internal class EffectDictionary : Dictionary<string, Effect>, IDictionaryValueBuilder<Effect, RawEffect>
{
    public Effect FromRaw(RawEffect fromRawEffect1) => new(fromRawEffect1);
    public RawEffect ToRaw(Effect fromRawEffect) => fromRawEffect.ToRawEffect1();
}
