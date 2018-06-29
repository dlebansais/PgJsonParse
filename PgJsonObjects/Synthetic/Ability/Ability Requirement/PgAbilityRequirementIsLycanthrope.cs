namespace PgJsonObjects
{
    public class PgAbilityRequirementIsLycanthrope: GenericPgObject<PgAbilityRequirementIsLycanthrope>, IPgAbilityRequirementIsLycanthrope
    {
        public PgAbilityRequirementIsLycanthrope(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsLycanthrope CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementIsLycanthrope CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementIsLycanthrope(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
    }
}
