namespace PgJsonObjects
{
    public class PgAbilityRequirementSingleRace: GenericPgObject<PgAbilityRequirementSingleRace>, IPgAbilityRequirementSingleRace
    {
        public PgAbilityRequirementSingleRace(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementSingleRace CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementSingleRace(data, offset);
        }

        public Race AllowedRace { get { return GetEnum<Race>(4); } }
    }
}
