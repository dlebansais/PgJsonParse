namespace PgJsonObjects
{
    public class PgAbilityRequirementSingleRace: GenericPgObject, IPgAbilityRequirementSingleRace
    {
        public PgAbilityRequirementSingleRace(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Race AllowedRace { get { return GetEnum<Race>(0); } }
    }
}
