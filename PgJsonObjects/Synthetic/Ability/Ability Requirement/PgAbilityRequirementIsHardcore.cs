namespace PgJsonObjects
{
    public class PgAbilityRequirementIsHardcore: GenericPgObject, IPgAbilityRequirementIsHardcore
    {
        public PgAbilityRequirementIsHardcore(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementIsHardcore(data, offset);
        }
    }
}
