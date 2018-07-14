using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementInteractionFlagSet: PgAbilityRequirement<PgAbilityRequirementInteractionFlagSet>, IPgAbilityRequirementInteractionFlagSet
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

        public override OtherRequirementType Type { get { return OtherRequirementType.InteractionFlagSet; } }
        public override string Key { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public string InteractionFlag { get { return GetString(12); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
            { "InteractionFlag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InteractionFlag } },
        }; } }
    }
}
