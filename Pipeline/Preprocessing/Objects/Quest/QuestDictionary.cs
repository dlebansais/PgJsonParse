namespace Preprocessor;

using System.Collections.Generic;

public class QuestDictionary : Dictionary<int, Quest>, IDictionaryValueBuilder<Quest, RawQuest>
{
    public Quest FromRaw(RawQuest rawQuest) => new(rawQuest);
    public RawQuest ToRaw(Quest quest) => quest.ToRawQuest();
}
