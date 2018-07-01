using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgIsLongTimeAnimalQuestRequirement : PgQuestRequirement<PgIsLongTimeAnimalQuestRequirement>, IPgIsLongTimeAnimalQuestRequirement
    {
        public PgIsLongTimeAnimalQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgIsLongTimeAnimalQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgIsLongTimeAnimalQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgIsLongTimeAnimalQuestRequirement(data, ref offset);
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
        }; } }
    }
}
