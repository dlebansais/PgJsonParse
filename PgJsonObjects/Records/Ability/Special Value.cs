using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SpecialValue : GenericJsonObject<SpecialValue>, IPgSpecialValue
    {
        #region Direct Properties
        public string Label { get; private set; }
        public string Suffix { get; private set; }
        public double Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public double? RawValue { get; private set; }
        public bool DisplayAsPercent { get { return RawDisplayAsPercent.HasValue && RawDisplayAsPercent.Value; } }
        public bool? RawDisplayAsPercent { get; private set; }
        public bool SkipIfZero { get { return RawSkipIfZero.HasValue && RawSkipIfZero.Value; } }
        public bool? RawSkipIfZero { get; private set; }
        private Dictionary<string, Attribute> AttributesThatDeltaTable { get; } = new Dictionary<string, Attribute>();
        private Dictionary<string, Attribute> AttributesThatModTable { get; } = new Dictionary<string, Attribute>();
        private Dictionary<string, Attribute> AttributesThatModBaseTable { get; } = new Dictionary<string, Attribute>();
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return Label; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Label", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Label = value,
                GetString = () => Label } },
            { "Suffix", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Suffix = value,
                GetString = () => Suffix } },
            { "Value", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawValue = value,
                GetFloat = () => RawValue } },
            { "AttributesThatDelta", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaListIsEmpty = true,
                GetStringArray = () => RawAttributesThatDeltaList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaListIsEmpty } },
            { "AttributesThatMod", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModListIsEmpty = true,
                GetStringArray = () => RawAttributesThatModList,
                GetArrayIsEmpty = () => RawAttributesThatModListIsEmpty } },
            { "AttributesThatModBase", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModBaseList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModBaseListIsEmpty = true,
                GetStringArray = () => RawAttributesThatModBaseList,
                GetArrayIsEmpty = () => RawAttributesThatModBaseListIsEmpty } },
            /*{ "DisplayAsPercent", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawDisplayAsPercent = value,
                GetBool = () => RawDisplayAsPercent } },*/
            { "DisplayType", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawDisplayAsPercent = (value == "AsPercent"),
                GetString = () => RawDisplayAsPercent.HasValue ? (RawDisplayAsPercent.Value ? "AsPercent" : "AsInt") : null } },
            { "SkipIfZero", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawSkipIfZero = value,
                GetBool = () => RawSkipIfZero } },
        }; } }

        private List<string> RawAttributesThatDeltaList { get; } = new List<string>();
        private bool RawAttributesThatDeltaListIsEmpty;
        private List<string> RawAttributesThatModList { get; } = new List<string>();
        private bool RawAttributesThatModListIsEmpty;
        private List<string> RawAttributesThatModBaseList { get; } = new List<string>();
        private bool RawAttributesThatModBaseListIsEmpty;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Label);
                AddWithFieldSeparator(ref Result, Suffix);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> AttributeTable = AllTables[typeof(Attribute)];

            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaList, AttributesThatDeltaTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModList, AttributesThatModTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModBaseList, AttributesThatModBaseTable);

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "SpecialValue"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();

            AddString(Label, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(Suffix, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddDouble(RawValue, data, ref offset, BaseOffset, 8);
            AddBool(RawDisplayAsPercent, data, ref offset, ref BitOffset, BaseOffset, 12, 0);
            AddBool(RawSkipIfZero, data, ref offset, ref BitOffset, BaseOffset, 12, 2);
            CloseBool(ref offset, ref BitOffset);

            FinishSerializing(data, ref offset, BaseOffset, 14, StoredStringtable, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
