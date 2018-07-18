using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgSpecialValue : GenericPgObject<PgSpecialValue>, IPgSpecialValue
    {
        public PgSpecialValue(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgSpecialValue CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgSpecialValue CreateNew(byte[] data, ref int offset)
        {
            return new PgSpecialValue(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string Label { get { return GetString(4); } }
        public string Suffix { get { return GetString(8); } }
        public double Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public double? RawValue { get { return GetDouble(12); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(16, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public IPgAttributeCollection AttributesThatDeltaList { get { return GetObjectList(20, ref _AttributesThatDeltaList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatDeltaList;
        public IPgAttributeCollection AttributesThatModList { get { return GetObjectList(24, ref _AttributesThatModList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatModList;
        public IPgAttributeCollection AttributesThatModBaseList { get { return GetObjectList(28, ref _AttributesThatModBaseList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatModBaseList;
        public bool DisplayAsPercent { get { return RawDisplayAsPercent.HasValue && RawDisplayAsPercent.Value; } }
        public bool? RawDisplayAsPercent { get { return GetBool(32, 0); } }
        public bool SkipIfZero { get { return RawSkipIfZero.HasValue && RawSkipIfZero.Value; } }
        public bool? RawSkipIfZero { get { return GetBool(32, 2); } }
        public bool RawAttributesThatDeltaListIsEmpty { get { return GetBool(32, 4).Value; } }
        public bool RawAttributesThatModListIsEmpty { get { return GetBool(32, 6).Value; } }
        public bool RawAttributesThatModBaseListIsEmpty { get { return GetBool(32, 8).Value; } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Label", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Label } },
            { "Suffix", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Suffix } },
            { "Value", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawValue } },
            { "AttributesThatDelta", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatDeltaList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaListIsEmpty } },
            { "AttributesThatMod", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatModList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModListIsEmpty } },
            { "AttributesThatModBase", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatModBaseList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModBaseListIsEmpty } },
            { "DisplayType", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawDisplayAsPercent.HasValue ? (RawDisplayAsPercent.Value ? "AsPercent" : "AsInt") : null } },
            { "SkipIfZero", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawSkipIfZero } },
        }; } }

        #region Indirect Properties
        public override string SortingName { get { return Label; } }
        #endregion
    }
}
