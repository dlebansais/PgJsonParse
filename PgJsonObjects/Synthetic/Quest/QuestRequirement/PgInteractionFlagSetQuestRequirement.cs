using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgInteractionFlagSetQuestRequirement : GenericPgObject<PgInteractionFlagSetQuestRequirement>, IPgInteractionFlagSetQuestRequirement
    {
        public PgInteractionFlagSetQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgInteractionFlagSetQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgInteractionFlagSetQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgInteractionFlagSetQuestRequirement(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public string InteractionFlag { get { return GetString(8); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
