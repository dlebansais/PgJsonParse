namespace PgJsonObjects
{
    public class PgAbilityRequirementIsAdmin: GenericPgObject<PgAbilityRequirementIsAdmin>, IPgAbilityRequirementIsAdmin
    {
        public PgAbilityRequirementIsAdmin(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsAdmin CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsAdmin(data, offset);
        }
    }
}
