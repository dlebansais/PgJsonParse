namespace PgJsonObjects
{
    public class PgAbilityRequirementDruidEventState: GenericPgObject<PgAbilityRequirementDruidEventState>, IPgAbilityRequirementDruidEventState
    {
        public PgAbilityRequirementDruidEventState(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementDruidEventState CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementDruidEventState(data, offset);
        }

        public DisallowedState DisallowedState { get { return GetEnum<DisallowedState>(4); } }
    }
}
