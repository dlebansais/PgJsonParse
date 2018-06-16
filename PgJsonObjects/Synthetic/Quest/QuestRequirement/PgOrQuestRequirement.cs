using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgOrQuestRequirement : GenericPgObject, IPgOrQuestRequirement
    {
        public PgOrQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public List<QuestRequirement> OrList { get { return GetObjectList(0, ref _OrList); } } private List<QuestRequirement> _OrList;
    }
}
