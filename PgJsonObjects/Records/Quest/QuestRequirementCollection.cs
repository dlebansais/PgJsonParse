using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRequirementCollection : List<IPgQuestRequirement>, IPgQuestRequirementCollection, ISerializableJsonObjectCollection
    {
    }
}
