using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SpecialValue : GenericJsonObject<SpecialValue>
    {
        #region Direct Properties
        public string Label { get; private set; }
        public string Suffix { get; private set; }
        public double Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        private double? RawValue;
        public Dictionary<string, Attribute> AttributesThatDeltaTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatModTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatModBaseTable { get; } = new Dictionary<string, Attribute>();
        public bool DisplayAsPercent { get { return RawDisplayAsPercent.HasValue && RawDisplayAsPercent.Value; } }
        private bool? RawDisplayAsPercent;
        public bool SkipIfZero { get { return RawSkipIfZero.HasValue && RawSkipIfZero.Value; } }
        private bool? RawSkipIfZero;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return Label; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldValueHandler> FieldTable {  get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Label", ParseFieldLabel },
            { "Suffix", ParseFieldSuffix },
            { "Value", ParseFieldValue },
            { "AttributesThatDelta", ParseFieldAttributesThatDelta },
            { "AttributesThatMod", ParseFieldAttributesThatMod },
            { "AttributesThatModBase", ParseFieldAttributesThatModBase },
            { "DisplayAsPercent", ParseFieldDisplayAsPercent },
            { "SkipIfZero", ParseFieldSkipIfZero },
        };

        private static void ParseFieldLabel(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "SpecialValue Label", This.ParseLabel);
        }

        private void ParseLabel(string RawLabel, ParseErrorInfo ErrorInfo)
        {
            Label = RawLabel;
        }

        private static void ParseFieldSuffix(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "SpecialValue Suffix", This.ParseSuffix);
        }

        private void ParseSuffix(string RawSuffix, ParseErrorInfo ErrorInfo)
        {
            Suffix = RawSuffix;
        }

        private static void ParseFieldValue(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "SpecialValue Value", This.ParseValue);
        }

        private void ParseValue(double RawValue, ParseErrorInfo ErrorInfo)
        {
            this.RawValue = RawValue;
        }

        private static void ParseFieldAttributesThatDelta(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "SpecialValue AttributesThatDelta", This.ParseAttributesThatDelta);
        }

        private bool ParseAttributesThatDelta(string RawAttributesThatDelta, ParseErrorInfo ErrorInfo)
        {
            RawAttributesThatDeltaList.Add(RawAttributesThatDelta);
            return true;
        }

        private static void ParseFieldAttributesThatMod(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "SpecialValue AttributesThatMod", This.ParseAttributesThatMod);
        }

        private bool ParseAttributesThatMod(string RawAttributesThatMod, ParseErrorInfo ErrorInfo)
        {
            RawAttributesThatModList.Add(RawAttributesThatMod);
            return true;
        }

        private static void ParseFieldAttributesThatModBase(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "SpecialValue AttributesThatModBase", This.ParseAttributesThatModBase);
        }

        private bool ParseAttributesThatModBase(string RawAttributesThatModBase, ParseErrorInfo ErrorInfo)
        {
            RawAttributesThatModBaseList.Add(RawAttributesThatModBase);
            return true;
        }

        private static void ParseFieldDisplayAsPercent(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueBool(Value, ErrorInfo, "SpecialValue DisplayAsPercent", This.ParseDisplayAsPercent);
        }

        private void ParseDisplayAsPercent(bool RawDisplayAsPercent, ParseErrorInfo ErrorInfo)
        {
            this.RawDisplayAsPercent = RawDisplayAsPercent;
        }

        private static void ParseFieldSkipIfZero(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueBool(Value, ErrorInfo, "SpecialValue SkipIfZero", This.ParseSkipIfZero);
        }

        private void ParseSkipIfZero(bool RawSkipIfZero, ParseErrorInfo ErrorInfo)
        {
            this.RawSkipIfZero = RawSkipIfZero;
        }

        private List<string> RawAttributesThatDeltaList { get; } = new List<string>();
        private bool RawAttributesThatDeltaListIsEmpty;
        private List<string> RawAttributesThatModList { get; } = new List<string>();
        private bool RawAttributesThatModListIsEmpty;
        private List<string> RawAttributesThatModBaseList { get; } = new List<string>();
        private bool RawAttributesThatModBaseListIsEmpty;
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(null);

            Generator.AddString("Label", Label);
            Generator.AddString("Suffix", Suffix);
            Generator.AddDouble("Value", RawValue);
            Generator.AddList("AttributesThatDelta", RawAttributesThatDeltaList, RawAttributesThatDeltaListIsEmpty);
            Generator.AddList("AttributesThatMod", RawAttributesThatModList, RawAttributesThatModListIsEmpty);
            Generator.AddList("AttributesThatModBase", RawAttributesThatModBaseList, RawAttributesThatModBaseListIsEmpty);
            Generator.AddBoolean("DisplayAsPercent", RawDisplayAsPercent);
            Generator.AddBoolean("SkipIfZero", RawSkipIfZero);

            Generator.CloseObject();
        }
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
    }
}
