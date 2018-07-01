using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgHasEffectKeywordQuestRequirement : PgQuestRequirement<PgHasEffectKeywordQuestRequirement>, IPgHasEffectKeywordQuestRequirement
    {
        public PgHasEffectKeywordQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgHasEffectKeywordQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgHasEffectKeywordQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgHasEffectKeywordQuestRequirement(data, ref offset);
        }

        public EffectKeyword Keyword { get { return GetEnum<EffectKeyword>(PropertiesOffset + 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Keyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<EffectKeyword>.ToString(Keyword, null, EffectKeyword.Internal_None) } },
        }; } }
    }
}
