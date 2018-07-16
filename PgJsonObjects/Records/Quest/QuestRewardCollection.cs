using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardCollection : List<IPgQuestReward>, IPgQuestRewardCollection, ISerializableJsonObjectCollection
    {
    }
}
