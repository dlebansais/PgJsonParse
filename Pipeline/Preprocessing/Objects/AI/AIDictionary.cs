namespace Preprocessor;

using System.Collections.Generic;

public class AIDictionary : Dictionary<string, AI>, IDictionaryValueBuilderString<AI, AI>
{
    public AI FromRaw(string key, AI ai) => ai;
    public AI ToRaw(AI ai) => ai;
}
