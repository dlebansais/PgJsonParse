namespace PgJsonObjects
{
    public class PgAbilityRequirementHasEffectKeyword: GenericPgObject, IPgAbilityRequirementHasEffectKeyword
    {
        public PgAbilityRequirementHasEffectKeyword(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public AbilityKeyword Keyword { get { return GetEnum<AbilityKeyword>(4); } }
    }
}
