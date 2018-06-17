namespace PgJsonObjects
{
    public class PgAbilityRequirementHasEffectKeyword: GenericPgObject<PgAbilityRequirementHasEffectKeyword>, IPgAbilityRequirementHasEffectKeyword
    {
        public PgAbilityRequirementHasEffectKeyword(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementHasEffectKeyword CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementHasEffectKeyword(data, offset);
        }

        public AbilityKeyword Keyword { get { return GetEnum<AbilityKeyword>(4); } }
    }
}
