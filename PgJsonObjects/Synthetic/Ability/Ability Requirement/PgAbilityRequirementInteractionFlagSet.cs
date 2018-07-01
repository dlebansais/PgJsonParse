using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementInteractionFlagSet: GenericPgObject<PgAbilityRequirementInteractionFlagSet>, IPgAbilityRequirementInteractionFlagSet
    {
        public PgAbilityRequirementInteractionFlagSet(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementInteractionFlagSet CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementInteractionFlagSet CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementInteractionFlagSet(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public string InteractionFlag { get { return GetString(8); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.InteractionFlagSet) } },
            { "InteractionFlag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InteractionFlag } },
        }; } }
    }
}
