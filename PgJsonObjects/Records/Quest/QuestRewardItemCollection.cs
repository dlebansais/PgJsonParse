using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardItemCollection : List<IPgQuestRewardItem>, IPgQuestRewardItemCollection, ISerializableJsonObjectCollection
    {
    }
}
