using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgIsWardenQuestRequirement : PgQuestRequirement<PgIsWardenQuestRequirement>, IPgIsWardenQuestRequirement
    {
        public PgIsWardenQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgIsWardenQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgIsWardenQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgIsWardenQuestRequirement(data, ref offset);
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
        }; } }
    }
}
