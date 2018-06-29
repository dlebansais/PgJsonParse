namespace PgJsonObjects
{
    public class PgAbilityRequirementInteractionFlagSet: GenericPgObject<PgAbilityRequirementInteractionFlagSet>, IPgAbilityRequirementInteractionFlagSet
    {
        public PgAbilityRequirementInteractionFlagSet(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementInteractionFlagSet CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementInteractionFlagSet CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementInteractionFlagSet(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public string InteractionFlag { get { return GetString(8); } }
    }
}
