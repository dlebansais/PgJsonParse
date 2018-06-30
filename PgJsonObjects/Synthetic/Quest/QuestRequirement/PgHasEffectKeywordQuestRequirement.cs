using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgHasEffectKeywordQuestRequirement : GenericPgObject<PgHasEffectKeywordQuestRequirement>, IPgHasEffectKeywordQuestRequirement
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

        public override string Key { get { return GetString(4); } }
        public EffectKeyword Keyword { get { return GetEnum<EffectKeyword>(8); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
