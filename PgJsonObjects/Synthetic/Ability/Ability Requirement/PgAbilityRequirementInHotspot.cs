using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementInHotspot: PgAbilityRequirement<PgAbilityRequirementInHotspot>, IPgAbilityRequirementInHotspot
    {
        public PgAbilityRequirementInHotspot(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementInHotspot CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementInHotspot CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementInHotspot(data, ref offset);
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.InHotspot; } }
        public override string Key { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public string Name { get { return GetString(12); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => Name } },
        }; } }
    }
}
