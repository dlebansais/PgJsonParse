namespace Preprocessor;

using System.Collections.Generic;

internal class QuestDictionary : Dictionary<int, Quest>, IDictionaryValueBuilder<Quest, Quest>
{
    public Quest FromRaw(Quest quest) => quest;
    public Quest ToRaw(Quest quest) => quest;
}
