using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgOrQuestRequirement
    {
        List<QuestRequirement> OrList { get; }
    }
}
