using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementSingleAppearance: GenericPgObject<PgAbilityRequirementSingleAppearance>, IPgAbilityRequirementSingleAppearance
    {
        public PgAbilityRequirementSingleAppearance(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementSingleAppearance CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementSingleAppearance CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementSingleAppearance(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public Appearance Appearance { get { return GetEnum<Appearance>(8); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Appearance", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<Appearance>.ToString(Appearance) } },
        }; } }
    }
}
