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
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Label", ParseFieldLabel },
            { "IconIds", ParseFieldIconIds },
            { "Tooltip", ParseFieldTooltip },
            { "DisplayType", ParseFieldDisplayType },
            { "IsHidden", ParseFieldIsHidden },
            { "DisplayRule", ParseFieldDisplayRule },
            { "DefaultValue", ParseFieldDefaultValue },
        };

        private static void ParseFieldLabel(Attribute This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Attribute Label", This.ParseLabel);
        }

        private void ParseLabel(string RawLabel, ParseErrorInfo ErrorInfo)
        {
            Label = RawLabel;
        }

        private static void ParseFieldIconIds(Attribute This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueIntegerArray(Value, ErrorInfo, "Attribute IconIds", This.ParseIconId);
        }

        private bool ParseIconId(long RawIconId, ParseErrorInfo ErrorInfo)
        {
            IconIdList.Add((int)RawIconId);
            ErrorInfo.AddIconId((int)RawIconId);
            return true;
        }

        private static void ParseFieldTooltip(Attribute This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Attribute Tooltip", This.ParseTooltip);
        }

        private void ParseTooltip(string RawTooltip, ParseErrorInfo ErrorInfo)
        {
            Tooltip = RawTooltip;
        }

        private static void ParseFieldDisplayType(Attribute This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Attribute DisplayType", This.ParseDisplayType);
        }

        private void ParseDisplayType(string RawDisplayType, ParseErrorInfo ErrorInfo)
        {
            DisplayType ParsedDisplayType;
            StringToEnumConversion<DisplayType>.TryParse(RawDisplayType, out ParsedDisplayType, ErrorInfo);
            DisplayType = ParsedDisplayType;
        }

        private static void ParseFieldIsHidden(Attribute This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueBool(Value, ErrorInfo, "Attribute IsHidden", This.ParseIsHidden);
        }

        private void ParseIsHidden(bool RawIsHidden, ParseErrorInfo ErrorInfo)
        {
            this.RawIsHidden = RawIsHidden;
        }

        private static void ParseFieldDisplayRule(Attribute This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Attribute DisplayRule", This.ParseDisplayRule);
        }

        private void ParseDisplayRule(string RawDisplayRule, ParseErrorInfo ErrorInfo)
        {
            DisplayRule ParsedDisplayRule;
            StringToEnumConversion<DisplayRule>.TryParse(RawDisplayRule, out ParsedDisplayRule, ErrorInfo);
            DisplayRule = ParsedDisplayRule;
        }

        private static void ParseFieldDefaultValue(Attribute This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "Attribute DefaultValue", This.ParseDefaultValue);
        }

        private void ParseDefaultValue(double RawDefaultValue, ParseErrorInfo ErrorInfo)
        {
            this.RawDefaultValue = RawDefaultValue;
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
