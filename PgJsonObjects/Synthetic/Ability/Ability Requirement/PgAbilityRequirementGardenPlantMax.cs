namespace PgJsonObjects
{
    public class PgAbilityRequirementGardenPlantMax: GenericPgObject, IPgAbilityRequirementGardenPlantMax
    {
        public PgAbilityRequirementGardenPlantMax(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementGardenPlantMax(data, offset);
        }

        public int Max { get { return RawMax.HasValue ? RawMax.Value : 0; } }
        public int? RawMax { get { return GetInt(4); } }
        public AbilityTypeTag TypeTag { get { return GetEnum<AbilityTypeTag>(8); } }
    }
}
