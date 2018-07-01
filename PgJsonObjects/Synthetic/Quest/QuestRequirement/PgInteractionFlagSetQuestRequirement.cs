using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgInteractionFlagSetQuestRequirement : PgQuestRequirement<PgInteractionFlagSetQuestRequirement>, IPgInteractionFlagSetQuestRequirement
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

        public string InteractionFlag { get { return GetString(PropertiesOffset + 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "InteractionFlag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InteractionFlag } },
        }; } }
    }
}
