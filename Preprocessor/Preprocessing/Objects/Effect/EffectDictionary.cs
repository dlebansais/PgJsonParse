namespace Preprocessor;

using System.Collections.Generic;

internal class EffectDictionary : Dictionary<int, Effect>, IDictionaryValueBuilder<Effect, RawEffect>
{
    public Effect FromRaw(RawEffect rawEffect) => new(rawEffect);
    public RawEffect ToRaw(Effect effect) => effect.ToRawEffect();
}
