using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveCollection : List<IPgQuestObjective>, IPgQuestObjectiveCollection, ISerializableJsonObjectCollection
    {
    }
}
