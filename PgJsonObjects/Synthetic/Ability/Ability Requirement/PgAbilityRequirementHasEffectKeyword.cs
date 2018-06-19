namespace PgJsonObjects
{
    public class PgAbilityRequirementHasEffectKeyword: GenericPgObject<PgAbilityRequirementHasEffectKeyword>, IPgAbilityRequirementHasEffectKeyword
    {
        public PgAbilityRequirementHasEffectKeyword(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementHasEffectKeyword CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementHasEffectKeyword CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementHasEffectKeyword(data, ref offset);
        }

        public AbilityKeyword Keyword { get { return GetEnum<AbilityKeyword>(4); } }
    }
}
