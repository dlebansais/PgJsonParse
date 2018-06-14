namespace PgJsonObjects
{
    public class PgAbilityRequirementGardenPlantMax: GenericPgObject, IPgAbilityRequirementGardenPlantMax
    {
        public PgAbilityRequirementGardenPlantMax(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public int Max { get { return RawMax.HasValue ? RawMax.Value : 0; } }
        public int? RawMax { get { return GetInt(0); } }
        public AbilityTypeTag TypeTag { get; }
    }
}
