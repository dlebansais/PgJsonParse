namespace PgJsonObjects
{
    public class PgAbilityRequirementIsNotInCombat: GenericPgObject<PgAbilityRequirementIsNotInCombat>, IPgAbilityRequirementIsNotInCombat
    {
        public PgAbilityRequirementIsNotInCombat(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsNotInCombat CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsNotInCombat(data, offset);
        }
    }
}
