using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveMultipleInteractionFlags : GenericPgObject, IPgQuestObjectiveMultipleInteractionFlags
    {
        public PgQuestObjectiveMultipleInteractionFlags(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveMultipleInteractionFlags(data, offset);
        }

        public List<string> InteractionFlagList { get { return GetStringList(0, ref _InteractionFlagList); } } private List<string> _InteractionFlagList;
    }
}
