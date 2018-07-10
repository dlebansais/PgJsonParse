using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsVegetarian: PgAbilityRequirement<PgAbilityRequirementIsVegetarian>, IPgAbilityRequirementIsVegetarian
    {
        public PgAbilityRequirementIsVegetarian(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsVegetarian CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementIsVegetarian CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementIsVegetarian(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.IsVegetarian) } },
        }; } }
    }
}
