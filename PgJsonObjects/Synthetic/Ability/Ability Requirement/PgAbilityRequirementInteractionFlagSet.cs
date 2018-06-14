namespace PgJsonObjects
{
    public class PgAbilityRequirementInteractionFlagSet: GenericPgObject, IPgAbilityRequirementInteractionFlagSet
    {
        public PgAbilityRequirementInteractionFlagSet(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string InteractionFlag { get { return GetString(0); } }
    }
}
