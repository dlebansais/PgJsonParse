using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardLevelCollection : List<IPgQuestRewardLevel>, IPgQuestRewardLevelCollection, ISerializableJsonObjectCollection
    {
    }
}
