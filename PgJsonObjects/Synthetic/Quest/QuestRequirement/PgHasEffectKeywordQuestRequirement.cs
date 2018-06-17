namespace PgJsonObjects
{
    public class PgHasEffectKeywordQuestRequirement : GenericPgObject<PgHasEffectKeywordQuestRequirement>, IPgHasEffectKeywordQuestRequirement
    {
        public PgHasEffectKeywordQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgHasEffectKeywordQuestRequirement CreateItem(byte[] data, int offset)
        {
            return new PgHasEffectKeywordQuestRequirement(data, offset);
        }

        public EffectKeyword Keyword { get { return GetEnum<EffectKeyword>(4); } }
    }
}
