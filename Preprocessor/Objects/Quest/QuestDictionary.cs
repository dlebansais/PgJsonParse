﻿namespace Preprocessor;

using System.Collections.Generic;

internal class QuestDictionary : Dictionary<int, Quest>, IDictionaryValueBuilder<Quest, RawQuest>
{
    public Quest FromRaw(RawQuest rawQuest) => new(rawQuest);
    public RawQuest ToRaw(Quest npc) => npc.ToRawQuest();
}
