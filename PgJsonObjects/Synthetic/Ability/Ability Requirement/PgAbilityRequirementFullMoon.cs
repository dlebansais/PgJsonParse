namespace PgJsonObjects
{
    public class PgAbilityRequirementFullMoon: GenericPgObject<PgAbilityRequirementFullMoon>, IPgAbilityRequirementFullMoon
    {
        public PgAbilityRequirementFullMoon(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementFullMoon CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementFullMoon CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementFullMoon(data, ref offset);
        }
    }
}
