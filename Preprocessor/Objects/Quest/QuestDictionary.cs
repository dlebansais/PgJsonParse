namespace Preprocessor;

using System.Collections.Generic;

internal class QuestDictionary : Dictionary<int, Quest>, IDictionaryValueBuilder<Quest, Quest>
{
    public Quest FromRaw(Quest fromRawQuest) => fromRawQuest;
    public Quest ToRaw(Quest fromRawQuest) => fromRawQuest;
}
