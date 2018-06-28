using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Attribute : MainJsonObject<Attribute>, IPgAttribute
    {
        #region Direct Properties
        public string Label { get; private set; }
        public List<int> IconIdList { get; } = new List<int>();
        public string Tooltip { get; private set; }
        public double DefaultValue { get { return RawDefaultValue.HasValue ? RawDefaultValue.Value : 0; } }
        public double? RawDefaultValue { get; private set; }
        public DisplayType DisplayType { get; private set; }
        public DisplayRule DisplayRule { get; private set; }
        public bool IsHidden { get { return RawIsHidden.HasValue && RawIsHidden.Value; } }
        public bool? RawIsHidden { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return Label; } }
        public List<string> IconFileNameList { get; } = new List<string>();

        public bool IsLabelWithPercent
        {
            get
            {
                return Label.EndsWith("%");
            }
        }

        public string LabelRippedOfPercent
        {
            get
            {
                string Result = IsLabelWithPercent ? Label.Substring(0, Label.Length - 1) : Label;
                return Result.Trim();
            }
        }

        public override void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo)
        {
            foreach (int Id in IconIdList)
                IconFileNameList.Add("icon_" + Id);
        }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Label", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Label = value,
                GetString = () => Label } },
            { "IconIds", new FieldParser() {
                Type = FieldType.SimpleIntegerArray,
                ParseSimpleIntegerArray = ParseIconId,
                GetIntegerArray = () => IconIdList } },
            { "Tooltip", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Tooltip = value,
                GetString = () => Tooltip } },
            { "DisplayType", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => DisplayType = StringToEnumConversion<DisplayType>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<DisplayType>.ToString(DisplayType, null, DisplayType.Internal_None) } },
            { "IsHidden", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsHidden = value,
                GetBool = () => RawIsHidden } },
            { "DisplayRule", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => DisplayRule = StringToEnumConversion<DisplayRule>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<DisplayRule>.ToString(DisplayRule, null, DisplayRule.Internal_None) } },
            { "DefaultValue", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawDefaultValue = value,
                GetFloat = () => RawDefaultValue } },
        }; } }

        private void ParseIconId(int value, ParseErrorInfo ErrorInfo)
        {
            IconIdList.Add(value);
            ErrorInfo.AddIconId(value);
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";
                /*
                AddWithFieldSeparator(ref Result, Label);
                AddWithFieldSeparator(ref Result, Tooltip);

                if (DisplayRule != DisplayRule.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.DisplayRuleTextMap[DisplayRule]);
                */

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }

        public static bool ConnectTable(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> AttributeTable, List<string> ConnectedList, Dictionary<string, Attribute> ConnectedTable)
        {
            bool Connected = false;

            foreach (string s in ConnectedList)
                if (AttributeTable.ContainsKey(s))
                {
                    Connected = true;
                    if (ConnectedTable.ContainsKey(s))
                        ErrorInfo.AddDuplicateString("Attribute", s);
                    else
                        ConnectedTable.Add(s, AttributeTable[s] as Attribute);
                }
                else
                {
                    if (s != "COCKATRICEDEBUFF_COST_DELTA" && s != "LAMIADEBUFF_COST_DELTA")
                        ErrorInfo.AddMissingKey(s);
                }

            return Connected;
        }

        public static IPgAttribute ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> AttributeTable, string RawAttributeName, IPgAttribute ParsedAttribute, ref bool IsRawAttributeParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawAttributeParsed)
                return ParsedAttribute;

            IsRawAttributeParsed = true;

            if (RawAttributeName == null)
                return null;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in AttributeTable)
            {
                Attribute AttributeValue = Entry.Value as Attribute;
                if (Entry.Key == RawAttributeName)
                {
                    IsConnected = true;
                    //Entry.Value.AddLinkBack(LinkBack);
                    return AttributeValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawAttributeName);

            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Attribute"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<int>> StoredIntListTable = new Dictionary<int, List<int>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(Label, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddIntList(IconIdList, data, ref offset, BaseOffset, 8, StoredIntListTable);
            AddString(Tooltip, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddDouble(RawDefaultValue, data, ref offset, BaseOffset, 16);
            AddEnum(DisplayType, data, ref offset, BaseOffset, 20);
            AddEnum(DisplayRule, data, ref offset, BaseOffset, 22);
            AddBool(RawIsHidden, data, ref offset, ref BitOffset, BaseOffset, 24, 0);
            CloseBool(ref offset, ref BitOffset);

            FinishSerializing(data, ref offset, BaseOffset, 26, StoredStringtable, null, null, null, StoredIntListTable, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
