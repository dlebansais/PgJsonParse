using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestCompletedQuestRequirement : GenericPgObject, IPgQuestCompletedQuestRequirement
    {
        public PgQuestCompletedQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public List<Quest> QuestList { get { return GetObjectList(0, ref _QuestList); } } private List<Quest> _QuestList;
    }
}
