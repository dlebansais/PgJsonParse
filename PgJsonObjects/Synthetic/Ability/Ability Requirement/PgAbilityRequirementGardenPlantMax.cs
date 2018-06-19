namespace PgJsonObjects
{
    public class PgAbilityRequirementGardenPlantMax: GenericPgObject<PgAbilityRequirementGardenPlantMax>, IPgAbilityRequirementGardenPlantMax
    {
        public PgAbilityRequirementGardenPlantMax(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementGardenPlantMax CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementGardenPlantMax CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementGardenPlantMax(data, ref offset);
        }

        public int Max { get { return RawMax.HasValue ? RawMax.Value : 0; } }
        public int? RawMax { get { return GetInt(4); } }
        public AbilityTypeTag TypeTag { get { return GetEnum<AbilityTypeTag>(8); } }
    }
}
