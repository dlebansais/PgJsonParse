namespace PgJsonObjects
{
    public class PgAbilityRequirementIsHardcore: GenericPgObject<PgAbilityRequirementIsHardcore>, IPgAbilityRequirementIsHardcore
    {
        public PgAbilityRequirementIsHardcore(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsHardcore CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementIsHardcore CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementIsHardcore(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
    }
}
