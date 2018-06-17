namespace PgJsonObjects
{
    public class PgAbilityRequirementFullMoon: GenericPgObject<PgAbilityRequirementFullMoon>, IPgAbilityRequirementFullMoon
    {
        public PgAbilityRequirementFullMoon(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementFullMoon CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementFullMoon(data, offset);
        }
    }
}
