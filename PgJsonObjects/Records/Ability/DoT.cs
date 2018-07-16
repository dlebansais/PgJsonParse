using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class DoT : GenericJsonObject<DoT>, IPgDoT
    {
        #region Direct Properties
        public int DamagePerTick { get { return RawDamagePerTick.HasValue ? RawDamagePerTick.Value : 0; } }
        public int? RawDamagePerTick { get; private set; }
        public int NumTicks { get { return RawNumTicks.HasValue ? RawNumTicks.Value : 0; } }
        public int? RawNumTicks { get; private set; }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get; private set; }
        public List<DoTSpecialRule> SpecialRuleList { get; } = new List<DoTSpecialRule>();
        public string RawPreface { get; private set; }
        public DamageType DamageType { get; private set; }
        public IPgAttributeCollection AttributesThatDeltaList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatModList { get; private set; } = null;
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "DamagePerTick", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawDamagePerTick = value,
                GetInteger = () => RawDamagePerTick } },
            { "NumTicks", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawNumTicks = value,
                GetInteger = () => RawNumTicks } },
            { "Duration", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawDuration = value,
                GetInteger = () => RawDuration } },
            { "DamageType", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => DamageType = StringToEnumConversion<DamageType>.Parse(value, null, DamageType.Internal_None, DamageType.Internal_Empty, errorInfo),
                GetString = () => StringToEnumConversion<DamageType>.ToString(DamageType, null, DamageType.Internal_None, DamageType.Internal_Empty) } },
            { "SpecialRules", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<DoTSpecialRule>.ParseList(value, SpecialRuleList, errorInfo),
                GetStringArray = () => StringToEnumConversion<DoTSpecialRule>.ToStringList(SpecialRuleList) } },
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
            { "Preface", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawPreface = value,
                GetString = () => RawPreface } },
        }; } }

        private List<string> RawAttributesThatDeltaList { get; } = new List<string>();
        public bool RawAttributesThatDeltaListIsEmpty { get; private set; }
        private List<string> RawAttributesThatModList { get; } = new List<string>();
        public bool RawAttributesThatModListIsEmpty { get; private set; }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (DamageType != DamageType.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.DamageTypeTextMap[DamageType]);

                foreach (DoTSpecialRule Item in SpecialRuleList)
                    AddWithFieldSeparator(ref Result, TextMaps.DoTSpecialRuleTextMap[Item]);

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
        protected override string FieldTableName { get { return "DoT"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();
            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddInt(RawDamagePerTick, data, ref offset, BaseOffset, 4);
            AddInt(RawNumTicks, data, ref offset, BaseOffset, 8);
            AddInt(RawDuration, data, ref offset, BaseOffset, 12);
            AddEnumList(SpecialRuleList, data, ref offset, BaseOffset, 16, StoredEnumListTable);
            AddString(RawPreface, data, ref offset, BaseOffset, 20, StoredStringtable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 24, StoredStringListTable);
            AddObjectList(AttributesThatDeltaList, data, ref offset, BaseOffset, 28, StoredObjectListTable);
            AddObjectList(AttributesThatModList, data, ref offset, BaseOffset, 32, StoredObjectListTable);
            AddEnum(DamageType, data, ref offset, BaseOffset, 36);
            AddBool(RawAttributesThatDeltaListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 38, 0);
            AddBool(RawAttributesThatModListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 38, 2);
            CloseBool(ref offset, ref BitOffset);

            FinishSerializing(data, ref offset, BaseOffset, 40, StoredStringtable, null, null, StoredEnumListTable, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
