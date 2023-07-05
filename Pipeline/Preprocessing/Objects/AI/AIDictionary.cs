namespace Preprocessor;

using System.Collections.Generic;

public class AIDictionary : Dictionary<string, AI>, IDictionaryValueBuilder<AI, AI>
{
    public AI FromRaw(AI ai) => ai;
    public AI ToRaw(AI ai) => ai;
}
