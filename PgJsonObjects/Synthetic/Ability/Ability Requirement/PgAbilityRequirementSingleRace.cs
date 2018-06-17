namespace PgJsonObjects
{
    public class PgAbilityRequirementSingleRace: GenericPgObject, IPgAbilityRequirementSingleRace
    {
        public PgAbilityRequirementSingleRace(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementSingleRace(data, offset);
        }

        public Race AllowedRace { get { return GetEnum<Race>(4); } }
    }
}
