using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgHangOutCompletedQuestRequirement : PgQuestRequirement<PgHangOutCompletedQuestRequirement>, IPgHangOutCompletedQuestRequirement
    {
        public PgHangOutCompletedQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgHangOutCompletedQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgHangOutCompletedQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgHangOutCompletedQuestRequirement(data, ref offset);
        }

        public string HangOut { get { return GetString(PropertiesOffset + 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "HangOut", new FieldParser() {
                Type = FieldType.String,
                GetString = () => HangOut } },
        }; } }
    }
}
