using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgDoT : GenericPgObject<PgDoT>, IPgDoT
    {
        public PgDoT(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgDoT CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgDoT CreateNew(byte[] data, ref int offset)
        {
            return new PgDoT(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public int DamagePerTick { get { return RawDamagePerTick.HasValue ? RawDamagePerTick.Value : 0; } }
        public int? RawDamagePerTick { get { return GetInt(4); } }
        public int NumTicks { get { return RawNumTicks.HasValue ? RawNumTicks.Value : 0; } }
        public int? RawNumTicks { get { return GetInt(8); } }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get { return GetInt(12); } }
        public List<DoTSpecialRule> SpecialRuleList { get { return GetEnumList(16, ref _SpecialRuleList); } } private List<DoTSpecialRule> _SpecialRuleList;
        public string RawPreface { get { return GetString(20); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(24, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public AttributeCollection AttributesThatDeltaList { get { return GetObjectList(28, ref _AttributesThatDeltaList, AttributeCollection.CreateItem, () => new AttributeCollection()); } } private AttributeCollection _AttributesThatDeltaList;
        public AttributeCollection AttributesThatModList { get { return GetObjectList(32, ref _AttributesThatModList, AttributeCollection.CreateItem, () => new AttributeCollection()); } } private AttributeCollection _AttributesThatModList;
        public DamageType DamageType { get { return GetEnum<DamageType>(36); } }
        public bool RawAttributesThatDeltaListIsEmpty { get { return GetBool(38, 0).Value; } }
        public bool RawAttributesThatModListIsEmpty { get { return GetBool(38, 2).Value; } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "DamagePerTick", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawDamagePerTick } },
            { "NumTicks", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumTicks } },
            { "Duration", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawDuration } },
            { "DamageType", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<DamageType>.ToString(DamageType, null, DamageType.Internal_None, DamageType.Internal_Empty) } },
            { "SpecialRules", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<DoTSpecialRule>.ToStringList(SpecialRuleList) } },
            { "AttributesThatDelta", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => GetAttributeKeys(AttributesThatDeltaList),
                GetArrayIsEmpty = () => RawAttributesThatDeltaListIsEmpty } },
            { "AttributesThatMod", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => GetAttributeKeys(AttributesThatModList),
                GetArrayIsEmpty = () => RawAttributesThatModListIsEmpty } },
            { "Preface", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawPreface } },
        }; } }

        private List<string> GetAttributeKeys(AttributeCollection attributes)
        {
            List<string> Result = new List<string>();

            foreach (IPgAttribute Item in attributes)
                Result.Add(Item.Key);

            return Result;
        }
    }
}
