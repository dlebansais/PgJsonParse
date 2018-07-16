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

        public override OtherRequirementType Type { get { return OtherRequirementType.Or; } }
        public override string Key { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public IPgAbilityRequirementCollection OrList { get { return GetObjectList(12, ref _OrList, PgAbilityRequirementCollection.CreateItem, () => new PgAbilityRequirementCollection()); } } private IPgAbilityRequirementCollection _OrList;
        public string ErrorMsg { get { return GetString(16); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
            { "List", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => OrList } },
            { "ErrorMsg", new FieldParser() {
                Type = FieldType.String,
                GetString = () => ErrorMsg } },
        }; } }
    }
}
