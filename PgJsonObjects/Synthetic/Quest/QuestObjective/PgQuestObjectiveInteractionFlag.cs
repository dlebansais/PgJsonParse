using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveInteractionFlag : GenericPgObject<PgQuestObjectiveInteractionFlag>, IPgQuestObjectiveInteractionFlag
    {
        public PgQuestObjectiveInteractionFlag(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveInteractionFlag CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveInteractionFlag CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveInteractionFlag(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string InteractionFlag { get { return GetString(4); } }
        public string InteractionTarget { get { return GetString(8); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
