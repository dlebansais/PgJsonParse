using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgGuildQuestCompletedQuestRequirement : GenericPgObject, IPgGuildQuestCompletedQuestRequirement
    {
        public PgGuildQuestCompletedQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public List<Quest> QuestList { get { return GetObject(0, ref _QuestList); } } private List<Quest> _QuestList;
    }
}
