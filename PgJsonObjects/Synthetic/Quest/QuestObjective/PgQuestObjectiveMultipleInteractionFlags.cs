using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveMultipleInteractionFlags : GenericPgObject, IPgQuestObjectiveMultipleInteractionFlags
    {
        public PgQuestObjectiveMultipleInteractionFlags(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public List<string> InteractionFlagList { get { return GetObjectList(0, ref _InteractionFlagList); } } private List<string> _InteractionFlagList;
    }
}
