using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementOr: PgAbilityRequirement<PgAbilityRequirementOr>, IPgAbilityRequirementOr
    {
        public PgAbilityRequirementOr(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementOr CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementOr CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementOr(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public AbilityRequirementCollection OrList { get { return GetObjectList(8, ref _OrList, AbilityRequirementCollection.CreateItem, () => new AbilityRequirementCollection()); } } private AbilityRequirementCollection _OrList;
        public string ErrorMsg { get { return GetString(12); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(16, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.Or) } },
            { "List", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => OrList } },
            { "ErrorMsg", new FieldParser() {
                Type = FieldType.String,
                GetString = () => ErrorMsg } },
        }; } }
    }
}
