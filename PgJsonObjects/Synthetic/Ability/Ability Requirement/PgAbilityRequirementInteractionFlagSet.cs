namespace PgJsonObjects
{
    public class PgAbilityRequirementInteractionFlagSet: GenericPgObject, IPgAbilityRequirementInteractionFlagSet
    {
        public PgAbilityRequirementInteractionFlagSet(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementInteractionFlagSet(data, offset);
        }

        public string InteractionFlag { get { return GetString(4); } }
    }
}
