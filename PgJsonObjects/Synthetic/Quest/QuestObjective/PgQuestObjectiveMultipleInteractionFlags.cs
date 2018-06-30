using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveMultipleInteractionFlags : GenericPgObject<PgQuestObjectiveMultipleInteractionFlags>, IPgQuestObjectiveMultipleInteractionFlags
    {
        public PgQuestObjectiveMultipleInteractionFlags(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveMultipleInteractionFlags CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveMultipleInteractionFlags CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveMultipleInteractionFlags(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public List<string> InteractionFlagList { get { return GetStringList(4, ref _InteractionFlagList); } } private List<string> _InteractionFlagList;
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
