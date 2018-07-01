using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementAppearance: GenericPgObject<PgAbilityRequirementAppearance>, IPgAbilityRequirementAppearance
    {
        public PgAbilityRequirementAppearance(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementAppearance CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementAppearance CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementAppearance(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public List<Appearance> AppearanceList { get { return GetEnumList(8, ref _AppearanceList); } } private List<Appearance> _AppearanceList;
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Appearance", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<Appearance>.ToStringList(AppearanceList) } },
        }; } }
    }
}
