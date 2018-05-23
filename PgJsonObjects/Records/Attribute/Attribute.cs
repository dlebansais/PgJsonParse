using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Attribute : GenericJsonObject<Attribute>
    {
        #region Direct Properties
        public string Label { get; private set; }
        public List<int> IconIdList { get; } = new List<int>();
        private bool IsIconIdListEmpty = true;
        public string Tooltip { get; private set; }
        public DisplayType DisplayType { get; private set; }
        public bool IsHidden { get { return RawIsHidden.HasValue && RawIsHidden.Value; } }
        private bool? RawIsHidden;
        public DisplayRule DisplayRule { get; private set; }
        public double? RawDefaultValue { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return Label; } }
        public List<string> IconFileNameList { get; } = new List<string>();

        public bool IsLabelWithPercent
        {
            get { return Label.EndsWith("%"); }
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
            { "Label", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { Label = value; }} },
            { "IconIds", new FieldParser() { Type = FieldType.SimpleIntegerArray, ParserSimpleIntegerArray = ParseIconId } },
            { "Tooltip", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { Tooltip = value; }} },
            { "DisplayType", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { DisplayType = StringToEnumConversion<DisplayType>.Parse(value, errorInfo); }} },
            { "IsHidden", new FieldParser() { Type = FieldType.Bool, ParserBool = (bool value, ParseErrorInfo errorInfo) => { RawIsHidden = value; }} },
            { "DisplayRule", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { DisplayRule = StringToEnumConversion<DisplayRule>.Parse(value, errorInfo); }} },
            { "DefaultValue", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawDefaultValue = value; }} },
        }; } }

        private void ParseIconId(int RawIconId, ParseErrorInfo ErrorInfo)
        {
            IconIdList.Add((int)RawIconId);
            ErrorInfo.AddIconId((int)RawIconId);
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("Label", Label);

            if (!IsIconIdListEmpty)
            {
                Generator.OpenArray("IconIds");

                foreach (int IconId in IconIdList)
                    Generator.AddInteger(null, IconId);

                Generator.CloseArray();
            }

            Generator.AddString("Tooltip", Tooltip);
            Generator.AddString("DisplayType", StringToEnumConversion<DisplayType>.ToString(DisplayType, null, DisplayType.Internal_None));
            Generator.AddBoolean("IsHidden", RawIsHidden);

            Generator.CloseObject();
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

        public static Attribute ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> AttributeTable, string RawAttributeName, Attribute ParsedAttribute, ref bool IsRawAttributeParsed, ref bool IsConnected, GenericJsonObject LinkBack)
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
    }
}
