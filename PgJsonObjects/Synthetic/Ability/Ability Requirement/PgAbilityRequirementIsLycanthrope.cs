namespace PgJsonObjects
{
    public class PgAbilityRequirementIsLycanthrope: GenericPgObject<PgAbilityRequirementIsLycanthrope>, IPgAbilityRequirementIsLycanthrope
    {
        public PgAbilityRequirementIsLycanthrope(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsLycanthrope CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsLycanthrope(data, offset);
        }
    }
}
