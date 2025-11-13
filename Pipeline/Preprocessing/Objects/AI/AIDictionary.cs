namespace Preprocessor;

using System.Collections.Generic;

public class AIDictionary : Dictionary<string, AI>, IDictionaryValueBuilderString<AI, RawAI>
{
    public AI FromRaw(string key, RawAI rawAi) => new(key, rawAi);
    public RawAI ToRaw(AI ai) => ai.ToRawAI();
}
