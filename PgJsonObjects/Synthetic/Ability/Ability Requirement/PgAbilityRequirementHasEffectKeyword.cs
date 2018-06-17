namespace PgJsonObjects
{
    public class PgAbilityRequirementHasEffectKeyword: GenericPgObject, IPgAbilityRequirementHasEffectKeyword
    {
        public PgAbilityRequirementHasEffectKeyword(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementHasEffectKeyword(data, offset);
        }

        public AbilityKeyword Keyword { get { return GetEnum<AbilityKeyword>(4); } }
    }
}
