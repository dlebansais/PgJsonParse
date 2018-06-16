using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgGuildQuestCompletedQuestRequirement
    {
        List<Quest> QuestList { get; }
    }
}
