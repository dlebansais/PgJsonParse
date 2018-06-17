namespace PgJsonObjects
{
    public class PgAbilityRequirementIsLycanthrope: GenericPgObject, IPgAbilityRequirementIsLycanthrope
    {
        public PgAbilityRequirementIsLycanthrope(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsLycanthrope(data, offset);
        }
    }
}
