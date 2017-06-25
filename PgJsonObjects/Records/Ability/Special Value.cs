using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SpecialValue : GenericJsonObject<SpecialValue>
    {
        #region Constants
        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
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
        #endregion

        #region Properties
        public string Label { get; private set; }
        public string Suffix { get; private set; }
        public double Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        private double? RawValue;
        public Dictionary<string, Attribute> AttributesThatDeltaTable { get; private set; }
        public Dictionary<string, Attribute> AttributesThatModTable { get; private set; }
        public Dictionary<string, Attribute> AttributesThatModBaseTable { get; private set; }
        public bool DisplayAsPercent { get { return RawDisplayAsPercent.HasValue && RawDisplayAsPercent.Value; } }
        private bool? RawDisplayAsPercent;
        public bool SkipIfZero { get { return RawSkipIfZero.HasValue && RawSkipIfZero.Value; } }
        private bool? RawSkipIfZero;
        #endregion

        #region Client Interface
        private static void ParseFieldLabel(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawLabel;
            if ((RawLabel = Value as string) != null)
                This.ParseLabel(RawLabel, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("SpecialValue Label");
        }

        private void ParseLabel(string RawLabel, ParseErrorInfo ErrorInfo)
        {
            Label = RawLabel;
        }

        private static void ParseFieldSuffix(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSuffix;
            if ((RawSuffix = Value as string) != null)
                This.ParseSuffix(RawSuffix, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("SpecialValue Suffix");
        }

        private void ParseSuffix(string RawSuffix, ParseErrorInfo ErrorInfo)
        {
            Suffix = RawSuffix;
        }

        private static void ParseFieldValue(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseValue((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseValue(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("SpecialValue Value");
        }

        private void ParseValue(double RawValue, ParseErrorInfo ErrorInfo)
        {
            this.RawValue = RawValue;
        }

        private static void ParseFieldAttributesThatDelta(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawAttributesThatDelta;
            if ((RawAttributesThatDelta = Value as ArrayList) != null)
                This.ParseAttributesThatDelta(RawAttributesThatDelta, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("SpecialValue AttributesThatDelta");
        }

        private void ParseAttributesThatDelta(ArrayList RawAttributesThatDelta, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatDelta, RawAttributesThatDeltaList, "AttributesThatDelta", ErrorInfo, out RawAttributesThatDeltaListIsEmpty);
        }

        private static void ParseFieldAttributesThatMod(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawAttributesThatMod;
            if ((RawAttributesThatMod = Value as ArrayList) != null)
                This.ParseAttributesThatMod(RawAttributesThatMod, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("SpecialValue AttributesThatMod");
        }

        private void ParseAttributesThatMod(ArrayList RawAttributesThatMod, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatMod, RawAttributesThatModList, "AttributesThatMod", ErrorInfo, out RawAttributesThatModListIsEmpty);
        }

        private static void ParseFieldAttributesThatModBase(SpecialValue This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawAttributesThatModBase;
            if ((RawAttributesThatModBase = Value as ArrayList) != null)
                This.ParseAttributesThatModBase(RawAttributesThatModBase, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("SpecialValue AttributesThatModBase");
        }

        private void ParseAttributesThatModBase(ArrayList RawAttributesThatModBase, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatModBase, RawAttributesThatModBaseList, "AttributesThatModBase", ErrorInfo, out RawAttributesThatModBaseListIsEmpty);
        }

        private static void ParseFieldDisplayAsPercent(SpecialValue This, object DisplayAsPercent, ParseErrorInfo ErrorInfo)
        {
            if (DisplayAsPercent is bool)
                This.ParseDisplayAsPercent((bool)DisplayAsPercent, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("SpecialValue DisplayAsPercent");
        }

        private void ParseDisplayAsPercent(bool RawDisplayAsPercent, ParseErrorInfo ErrorInfo)
        {
            this.RawDisplayAsPercent = RawDisplayAsPercent;
        }

        private static void ParseFieldSkipIfZero(SpecialValue This, object SkipIfZero, ParseErrorInfo ErrorInfo)
        {
            if (SkipIfZero is bool)
                This.ParseSkipIfZero((bool)SkipIfZero, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("SpecialValue SkipIfZero");
        }

        private void ParseSkipIfZero(bool RawSkipIfZero, ParseErrorInfo ErrorInfo)
        {
            this.RawSkipIfZero = RawSkipIfZero;
        }

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("Label", Label);
            Generator.AddString("Suffix", Suffix);
            Generator.AddDouble("Value", RawValue);
            Generator.AddList("AttributesThatDelta", RawAttributesThatDeltaList, RawAttributesThatDeltaListIsEmpty);
            Generator.AddList("AttributesThatMod", RawAttributesThatModList, RawAttributesThatModListIsEmpty);
            Generator.AddList("AttributesThatModBase", RawAttributesThatModBaseList, RawAttributesThatModBaseListIsEmpty);

            Generator.CloseObject();
        }

        public override string TextContent
        {
            get
            {
                string Result = "";

                Result += Label + JsonGenerator.FieldSeparator;
                Result += Suffix + JsonGenerator.FieldSeparator;

                return Result;
            }
        }

        private List<string> RawAttributesThatDeltaList;
        private bool RawAttributesThatDeltaListIsEmpty;
        private List<string> RawAttributesThatModList;
        private bool RawAttributesThatModListIsEmpty;
        private List<string> RawAttributesThatModBaseList;
        private bool RawAttributesThatModBaseListIsEmpty;
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "SpecialValue"; } }

        protected override void InitializeFields()
        {
            RawAttributesThatDeltaList = new List<string>();
            AttributesThatDeltaTable = new Dictionary<string, Attribute>();
            RawAttributesThatModList = new List<string>();
            AttributesThatModTable = new Dictionary<string, Attribute>();
            RawAttributesThatModBaseList = new List<string>();
            AttributesThatModBaseTable = new Dictionary<string, Attribute>();
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaList, AttributesThatDeltaTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModList, AttributesThatModTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModBaseList, AttributesThatModBaseTable);

            return IsConnected;
        }
        #endregion
    }
}
