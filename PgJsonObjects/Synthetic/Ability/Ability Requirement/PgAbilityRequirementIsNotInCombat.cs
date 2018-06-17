namespace PgJsonObjects
{
    public class PgAbilityRequirementIsNotInCombat: GenericPgObject, IPgAbilityRequirementIsNotInCombat
    {
        public PgAbilityRequirementIsNotInCombat(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsNotInCombat(data, offset);
        }
    }
}
