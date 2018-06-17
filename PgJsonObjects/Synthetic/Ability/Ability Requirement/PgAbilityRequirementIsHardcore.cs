namespace PgJsonObjects
{
    public class PgAbilityRequirementIsHardcore: GenericPgObject<PgAbilityRequirementIsHardcore>, IPgAbilityRequirementIsHardcore
    {
        public PgAbilityRequirementIsHardcore(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsHardcore CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsHardcore(data, offset);
        }
    }
}
