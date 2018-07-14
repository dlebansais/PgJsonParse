using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsFullMoon: PgAbilityRequirement<PgAbilityRequirementIsFullMoon>, IPgAbilityRequirementIsFullMoon
    {
        public PgAbilityRequirementIsFullMoon(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsFullMoon CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementIsFullMoon CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementIsFullMoon(data, ref offset);
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.FullMoon; } }
        public override string Key { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
        }; } }
    }
}
