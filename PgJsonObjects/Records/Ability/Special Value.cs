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
        public IPgAttributeCollection AttributesThatDeltaList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatModList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatModBaseList { get; private set; } = null;
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
                GetStringArray = () => AttributesThatDeltaList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaListIsEmpty } },
            { "AttributesThatMod", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModListIsEmpty = true,
                GetStringArray = () => AttributesThatModList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModListIsEmpty } },
            { "AttributesThatModBase", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModBaseList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModBaseListIsEmpty = true,
                GetStringArray = () => AttributesThatModBaseList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModBaseListIsEmpty } },
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
        public bool RawAttributesThatDeltaListIsEmpty { get; private set; }
        private List<string> RawAttributesThatModList { get; } = new List<string>();
        public bool RawAttributesThatModListIsEmpty { get; private set; }
        private List<string> RawAttributesThatModBaseList { get; } = new List<string>();
        public bool RawAttributesThatModBaseListIsEmpty { get; private set; }
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

            AttributesThatDeltaList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatDeltaList, AttributesThatDeltaList, ref IsConnected);
            AttributesThatModList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatModList, AttributesThatModList, ref IsConnected);
            AttributesThatModBaseList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatModBaseList, AttributesThatModBaseList, ref IsConnected);

            return IsConnected;
        }

        private IPgAttributeCollection ConnectAttributes(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> AttributeTable, List<string> RawAttributes, IPgAttributeCollection Attributes, ref bool IsConnected)
        {
            if (Attributes == null)
            {
                Attributes = new AttributeCollection();
                foreach (string RawAttribute in RawAttributes)
                {
                    IPgAttribute ConnectedAttribute = null;
                    bool IsAttributeParsed = false;
                    ConnectedAttribute = Attribute.ConnectSingleProperty(ErrorInfo, AttributeTable, RawAttribute, ConnectedAttribute, ref IsAttributeParsed, ref IsConnected, this);
                    if (ConnectedAttribute != null)
                        Attributes.Add(ConnectedAttribute);
                }
            }

            return Attributes;
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
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();
            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(Label, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddString(Suffix, data, ref offset, BaseOffset, 8, StoredStringtable);
            AddDouble(RawValue, data, ref offset, BaseOffset, 12);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 16, StoredStringListTable);
            AddObjectList(AttributesThatDeltaList, data, ref offset, BaseOffset, 20, StoredObjectListTable);
            AddObjectList(AttributesThatModList, data, ref offset, BaseOffset, 24, StoredObjectListTable);
            AddObjectList(AttributesThatModBaseList, data, ref offset, BaseOffset, 28, StoredObjectListTable);
            AddBool(RawDisplayAsPercent, data, ref offset, ref BitOffset, BaseOffset, 32, 0);
            AddBool(RawSkipIfZero, data, ref offset, ref BitOffset, BaseOffset, 32, 2);
            AddBool(RawAttributesThatDeltaListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 32, 4);
            AddBool(RawAttributesThatModListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 32, 6);
            AddBool(RawAttributesThatModBaseListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 32, 8);
            CloseBool(ref offset, ref BitOffset);

            FinishSerializing(data, ref offset, BaseOffset, 34, StoredStringtable, null, null, null, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
