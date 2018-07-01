using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementDruidEventState: GenericPgObject<PgAbilityRequirementDruidEventState>, IPgAbilityRequirementDruidEventState
    {
        public PgAbilityRequirementDruidEventState(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementDruidEventState CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementDruidEventState CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementDruidEventState(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public DisallowedState DisallowedState { get { return GetEnum<DisallowedState>(8); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;


        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.DruidEventState) } },
            { "DisallowedStates", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = () => GenericJsonObject.CreateSingleOrEmptyStringList(StringToEnumConversion<DisallowedState>.ToString(DisallowedState)) } },
        }; } }
    }
}
