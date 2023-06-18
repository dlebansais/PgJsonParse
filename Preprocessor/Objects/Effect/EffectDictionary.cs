namespace Preprocessor;

using System.Collections.Generic;

internal class EffectDictionary : Dictionary<string, Effect>, IDictionaryValueBuilder<Effect, Effect1>
{
    public Effect ToItem(Effect1 fromRawEffect1) => new(fromRawEffect1);
    public Effect1 ToRawItem(Effect fromRawEffect) => fromRawEffect.ToRawEffect1();
}
