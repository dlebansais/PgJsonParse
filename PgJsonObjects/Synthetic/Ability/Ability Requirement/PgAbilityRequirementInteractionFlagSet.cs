namespace PgJsonObjects
{
    public class PgAbilityRequirementInteractionFlagSet: GenericPgObject<PgAbilityRequirementInteractionFlagSet>, IPgAbilityRequirementInteractionFlagSet
    {
        public PgAbilityRequirementInteractionFlagSet(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementInteractionFlagSet CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementInteractionFlagSet(data, offset);
        }

        public string InteractionFlag { get { return GetString(4); } }
    }
}
