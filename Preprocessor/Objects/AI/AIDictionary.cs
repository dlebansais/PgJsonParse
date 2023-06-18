namespace Preprocessor;

using System.Collections.Generic;

internal class AIDictionary : Dictionary<string, AI>, IDictionaryValueBuilder<AI, AI>
{
    public AI FromRaw(AI fromRawAI) => fromRawAI;
    public AI ToRaw(AI fromRawAI) => fromRawAI;
}
