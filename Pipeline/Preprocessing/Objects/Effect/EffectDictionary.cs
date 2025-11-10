namespace Preprocessor;

using System.Collections.Generic;

public class EffectDictionary : Dictionary<int, Effect>, IDictionaryValueBuilderInt<Effect, RawEffect>
{
    public Effect FromRaw(int key, RawEffect rawEffect) => new(key, rawEffect);
    public RawEffect ToRaw(Effect effect) => effect.ToRawEffect();
}
