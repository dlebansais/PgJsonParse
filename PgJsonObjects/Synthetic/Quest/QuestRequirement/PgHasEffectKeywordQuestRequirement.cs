namespace PgJsonObjects
{
    public class PgHasEffectKeywordQuestRequirement : GenericPgObject, IPgHasEffectKeywordQuestRequirement
    {
        public PgHasEffectKeywordQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public EffectKeyword Keyword { get { return GetEnum<EffectKeyword>(0); } }
    }
}
