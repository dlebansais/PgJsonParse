namespace Preprocessor;

using System.Collections.Generic;

public class QuestDictionary : Dictionary<int, Quest>, IDictionaryValueBuilderInt<Quest, RawQuest>
{
    public Quest FromRaw(int key, RawQuest rawQuest) => new(key, rawQuest);
    public RawQuest ToRaw(Quest quest) => quest.ToRawQuest();
}
