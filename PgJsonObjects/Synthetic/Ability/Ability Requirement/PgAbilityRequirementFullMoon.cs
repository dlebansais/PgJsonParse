namespace PgJsonObjects
{
    public class PgAbilityRequirementFullMoon: GenericPgObject, IPgAbilityRequirementFullMoon
    {
        public PgAbilityRequirementFullMoon(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementFullMoon(data, offset);
        }
    }
}
