namespace PgJsonObjects
{
    public class PgAbilityRequirementDruidEventState: GenericPgObject, IPgAbilityRequirementDruidEventState
    {
        public PgAbilityRequirementDruidEventState(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementDruidEventState(data, offset);
        }

        public DisallowedState DisallowedState { get { return GetEnum<DisallowedState>(4); } }
    }
}
