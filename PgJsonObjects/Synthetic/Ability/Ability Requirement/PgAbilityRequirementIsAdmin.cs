namespace PgJsonObjects
{
    public class PgAbilityRequirementIsAdmin: GenericPgObject<PgAbilityRequirementIsAdmin>, IPgAbilityRequirementIsAdmin
    {
        public PgAbilityRequirementIsAdmin(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsAdmin CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementIsAdmin CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementIsAdmin(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
    }
}
