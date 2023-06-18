namespace Preprocessor;

using System.Collections.Generic;

internal class AIDictionary : Dictionary<string, AI>, IDictionaryValueBuilder<AI, AI>
{
    public AI ToItem(AI fromRawAI) => fromRawAI;
    public AI ToRawItem(AI fromRawAI) => fromRawAI;
}
