namespace PgJsonObjects
{
    public class PgAbilityRequirementDruidEventState: GenericPgObject<PgAbilityRequirementDruidEventState>, IPgAbilityRequirementDruidEventState
    {
        public PgAbilityRequirementDruidEventState(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementDruidEventState CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementDruidEventState CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementDruidEventState(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public DisallowedState DisallowedState { get { return GetEnum<DisallowedState>(8); } }
    }
}
