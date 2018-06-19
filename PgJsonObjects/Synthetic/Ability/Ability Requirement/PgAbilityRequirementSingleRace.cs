namespace PgJsonObjects
{
    public class PgAbilityRequirementSingleRace: GenericPgObject<PgAbilityRequirementSingleRace>, IPgAbilityRequirementSingleRace
    {
        public PgAbilityRequirementSingleRace(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementSingleRace CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementSingleRace CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementSingleRace(data, ref offset);
        }

        public Race AllowedRace { get { return GetEnum<Race>(4); } }
    }
}
