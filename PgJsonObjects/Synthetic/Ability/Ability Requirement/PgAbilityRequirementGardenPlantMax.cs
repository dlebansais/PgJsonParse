using System.Collections.Generic;

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

        public override string Key { get { return GetString(4); } }
        public int Max { get { return RawMax.HasValue ? RawMax.Value : 0; } }
        public int? RawMax { get { return GetInt(8); } }
        public AbilityTypeTag TypeTag { get { return GetEnum<AbilityTypeTag>(12); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
