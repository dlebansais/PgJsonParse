namespace PgJsonObjects
{
    public class PgAbilityRequirementIsAdmin: GenericPgObject, IPgAbilityRequirementIsAdmin
    {
        public PgAbilityRequirementIsAdmin(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsAdmin(data, offset);
        }
    }
}
