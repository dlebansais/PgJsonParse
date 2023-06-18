namespace Preprocessor;

using System.Collections.Generic;

internal class QuestDictionary : Dictionary<int, Quest>, IDictionaryValueBuilder<Quest, Quest>
{
    public Quest ToItem(Quest fromRawQuest) => fromRawQuest;
    public Quest ToRawItem(Quest fromRawQuest) => fromRawQuest;
}
