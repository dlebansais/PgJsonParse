namespace PgJsonObjects
{
    public class PgAbilityRequirementDruidEventState: GenericPgObject, IPgAbilityRequirementDruidEventState
    {
        public PgAbilityRequirementDruidEventState(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public DisallowedState DisallowedState { get { return GetEnum<DisallowedState>(0); } }
    }
}
