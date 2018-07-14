using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementPetCount: PgAbilityRequirement<PgAbilityRequirementPetCount>, IPgAbilityRequirementPetCount
    {
        public PgAbilityRequirementPetCount(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementPetCount CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementPetCount CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementPetCount(data, ref offset);
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.PetCount; } }
        public override string Key { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public double MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public double? RawMaxCount { get { return GetDouble(12); } }
        public RecipeKeyword PetTypeTag { get { return GetEnum<RecipeKeyword>(16); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
            { "PetTypeTag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<RecipeKeyword>.ToString(PetTypeTag) } },
            { "MaxCount", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMaxCount } },
        }; } }
    }
}
