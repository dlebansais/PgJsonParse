namespace Preprocessor;

using System.Collections.Generic;

internal class RawQuestDictionary : Dictionary<int, RawQuest>, IDictionaryValueBuilder<RawQuest, RawQuest>
{
    public RawQuest ToItem(RawQuest fromRawQuest) => fromRawQuest;
    public RawQuest ToRawItem(RawQuest fromRawQuest) => fromRawQuest;
}
