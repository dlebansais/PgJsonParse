using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestCollection : List<IPgQuest>, IPgQuestCollection, ISerializableJsonObjectCollection
    {
    }
}
