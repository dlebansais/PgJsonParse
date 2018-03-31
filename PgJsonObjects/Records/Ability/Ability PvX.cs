using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityPvX : GenericJsonObject<AbilityPvX>
    {
        #region Direct Properties
        public int Damage { get { return RawDamage.HasValue ? RawDamage.Value : 0; } }
        private int? RawDamage;
        public int ExtraDamageIfTargetVulnerable { get { return RawExtraDamageIfTargetVulnerable.HasValue ? RawExtraDamageIfTargetVulnerable.Value : 0; } }
        private int? RawExtraDamageIfTargetVulnerable;
        public int HealthSpecificDamage { get { return RawHealthSpecificDamage.HasValue ? RawHealthSpecificDamage.Value : 0; } }
        private int? RawHealthSpecificDamage;
        public int ArmorSpecificDamage { get { return RawArmorSpecificDamage.HasValue ? RawArmorSpecificDamage.Value : 0; } }
        private int? RawArmorSpecificDamage;
        public int Range { get { return RawRange.HasValue ? RawRange.Value : 0; } }
        private int? RawRange;
        public int PowerCost { get { return RawPowerCost.HasValue ? RawPowerCost.Value : 0; } }
        private int? RawPowerCost;
        public int MetabolismCost { get { return RawMetabolismCost.HasValue ? RawMetabolismCost.Value : 0; } }
        private int? RawMetabolismCost;
        public int ArmorMitigationRatio { get { return RawArmorMitigationRatio.HasValue ? RawArmorMitigationRatio.Value : 0; } }
        private int? RawArmorMitigationRatio;
        public bool IsAoE { get { return RawAoE.HasValue; } }
        public int AoE { get { return RawAoE.HasValue ? RawAoE.Value : 0; } }
        private int? RawAoE;
        public int RageBoost { get { return RawRageBoost.HasValue ? RawRageBoost.Value : 0; } }
        private int? RawRageBoost;
        public double RageMultiplier { get { return RawRageMultiplier.HasValue ? RawRageMultiplier.Value : 1.0; } }
        private double? RawRageMultiplier;
        public double Accuracy { get { return RawAccuracy.HasValue ? RawAccuracy.Value : 0; } }
        private double? RawAccuracy;
        public Dictionary<string, Attribute> AttributesThatDeltaDamageTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatModDamageTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatModBaseDamageTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatDeltaTauntTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatModTauntTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatDeltaRageTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatModRageTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatDeltaRangeTable { get; } = new Dictionary<string, Attribute>();
        public List<SpecialValue> SpecialValueList { get; } = new List<SpecialValue>();
        public int TauntDelta { get { return RawTauntDelta.HasValue ? RawTauntDelta.Value : 0; } }
        private int? RawTauntDelta;
        public int TempTauntDelta { get { return RawTempTauntDelta.HasValue ? RawTempTauntDelta.Value : 0; } }
        private int? RawTempTauntDelta;
        public int RageCost { get { return RawRageCost.HasValue ? RawRageCost.Value : 0; } }
        private int? RawRageCost;
        public double RageCostMod { get { return RawRageCostMod.HasValue ? RawRageCostMod.Value : 0; } }
        private double? RawRageCostMod;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Damage", ParseFieldDamage },
            { "HealthSpecificDamage", ParseFieldHealthSpecificDamage },
            { "ExtraDamageIfTargetVulnerable", ParseFieldExtraDamageIfTargetVulnerable },
            { "ArmorSpecificDamage", ParseFieldArmorSpecificDamage },
            { "Range", ParseFieldRange },
            { "PowerCost", ParseFieldPowerCost },
            { "MetabolismCost", ParseFieldMetabolismCost },
            { "ArmorMitigationRatio", ParseFieldArmorMitigationRatio },
            { "AoE", ParseFieldAoE },
            { "RageBoost", ParseFieldRageBoost },
            { "RageMultiplier", ParseFieldRageMultiplier },
            { "Accuracy", ParseFieldAccuracy },
            { "AttributesThatDeltaDamage", ParseFieldAttributesThatDeltaDamage},
            { "AttributesThatModDamage", ParseFieldAttributesThatModDamage },
            { "AttributesThatModBaseDamage", ParseFieldAttributesThatModBaseDamage },
            { "AttributesThatDeltaTaunt", ParseFieldAttributesThatDeltaTaunt },
            { "AttributesThatModTaunt", ParseFieldAttributesThatModTaunt },
            { "AttributesThatDeltaRage", ParseFieldAttributesThatDeltaRage },
            { "AttributesThatModRage", ParseFieldAttributesThatModRage },
            { "AttributesThatDeltaRange", ParseFieldAttributesThatDeltaRange },
            { "SpecialValues", ParseFieldSpecialValues },
            { "TauntDelta", ParseFieldTauntDelta },
            { "TempTauntDelta", ParseFieldTempTauntDelta },
            { "RageCost", ParseFieldRageCost },
            { "RageCostMod", ParseFieldRageCostMod },
        };

        private static void ParseFieldDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX Damage", This.ParseDamage);
        }

        private void ParseDamage(long RawDamage, ParseErrorInfo ErrorInfo)
        {
            this.RawDamage = (int)RawDamage;
        }

        private static void ParseFieldExtraDamageIfTargetVulnerable(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX ExtraDamageIfTargetVulnerable", This.ParseExtraDamageIfTargetVulnerable);
        }

        private void ParseExtraDamageIfTargetVulnerable(long RawExtraDamageIfTargetVulnerable, ParseErrorInfo ErrorInfo)
        {
            this.RawExtraDamageIfTargetVulnerable = (int)RawExtraDamageIfTargetVulnerable;
        }

        private static void ParseFieldHealthSpecificDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX HealthSpecificDamage", This.ParseHealthSpecificDamage);
        }

        private void ParseHealthSpecificDamage(long RawHealthSpecificDamage, ParseErrorInfo ErrorInfo)
        {
            this.RawHealthSpecificDamage = (int)RawHealthSpecificDamage;
        }

        private static void ParseFieldArmorSpecificDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX ArmorSpecificDamage", This.ParseArmorSpecificDamage);
        }

        private void ParseArmorSpecificDamage(long RawArmorSpecificDamage, ParseErrorInfo ErrorInfo)
        {
            this.RawArmorSpecificDamage = (int)RawArmorSpecificDamage;
        }

        private static void ParseFieldRange(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX Range", This.ParseRange);
        }

        private void ParseRange(long RawRange, ParseErrorInfo ErrorInfo)
        {
            this.RawRange = (int)RawRange;
        }

        private static void ParseFieldPowerCost(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX PowerCost", This.ParsePowerCost);
        }

        private void ParsePowerCost(long RawPowerCost, ParseErrorInfo ErrorInfo)
        {
            this.RawPowerCost = (int)RawPowerCost;
        }

        private static void ParseFieldMetabolismCost(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX MetabolismCost", This.ParseMetabolismCost);
        }

        private void ParseMetabolismCost(long RawMetabolismCost, ParseErrorInfo ErrorInfo)
        {
            this.RawMetabolismCost = (int)RawMetabolismCost;
        }

        private static void ParseFieldArmorMitigationRatio(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX ArmorMitigationRatio", This.ParseArmorMitigationRatio);
        }

        private void ParseArmorMitigationRatio(long RawArmorMitigationRatio, ParseErrorInfo ErrorInfo)
        {
            this.RawArmorMitigationRatio = (int)RawArmorMitigationRatio;
        }

        private static void ParseFieldAoE(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX AoE", This.ParseAoE);
        }

        private void ParseAoE(long RawAoE, ParseErrorInfo ErrorInfo)
        {
            this.RawAoE = (int)RawAoE;
        }

        private static void ParseFieldRageBoost(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX RageBoost", This.ParseRageBoost);
        }

        private void ParseRageBoost(long RawRageBoost, ParseErrorInfo ErrorInfo)
        {
            this.RawRageBoost = (int)RawRageBoost;
        }

        private static void ParseFieldRageMultiplier(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            JsonInteger AsJsonInteger;
            JsonFloat AsJsonFloat;

            if ((AsJsonInteger = Value as JsonInteger) != null)
                This.ParseRageMultiplier(AsJsonInteger.Number, ErrorInfo);

            else if ((AsJsonFloat = Value as JsonFloat) != null)
                This.ParseRageMultiplier(AsJsonFloat.Number, ErrorInfo);

            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX RageMultiplier");
        }

        private void ParseRageMultiplier(double RawRageMultiplier, ParseErrorInfo ErrorInfo)
        {
            this.RawRageMultiplier = RawRageMultiplier;
        }

        private static void ParseFieldAccuracy(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            JsonInteger AsJsonInteger;
            JsonFloat AsJsonFloat;

            if ((AsJsonInteger = Value as JsonInteger) != null)
                This.ParseAccuracy(AsJsonInteger.Number, ErrorInfo);

            else if ((AsJsonFloat = Value as JsonFloat) != null)
                This.ParseAccuracy(AsJsonFloat.Number, ErrorInfo);

            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX Accuracy");
        }

        private void ParseAccuracy(double RawAccuracy, ParseErrorInfo ErrorInfo)
        {
            this.RawAccuracy = RawAccuracy;
        }

        private static void ParseFieldAttributesThatDeltaDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            JsonArray RawAttributesThatDeltaDamage;
            if ((RawAttributesThatDeltaDamage = Value as JsonArray) != null)
                This.ParseAttributesThatDeltaDamage(RawAttributesThatDeltaDamage, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatDeltaDamage");
        }

        private void ParseAttributesThatDeltaDamage(JsonArray RawAttributesThatDeltaDamage, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatDeltaDamage, RawAttributesThatDeltaDamageList, "AttributesThatDeltaDamage", ErrorInfo, out RawAttributesThatDeltaDamageListIsEmpty);
        }

        private static void ParseFieldAttributesThatModDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            JsonArray RawAttributesThatModDamage;
            if ((RawAttributesThatModDamage = Value as JsonArray) != null)
                This.ParseAttributesThatModDamage(RawAttributesThatModDamage, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatModDamage");
        }

        private void ParseAttributesThatModDamage(JsonArray RawAttributesThatModDamage, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatModDamage, RawAttributesThatModDamageList, "AttributesThatModDamage", ErrorInfo, out RawAttributesThatModDamageListIsEmpty);
        }

        private static void ParseFieldAttributesThatModBaseDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            JsonArray RawAttributesThatModBaseDamage;
            if ((RawAttributesThatModBaseDamage = Value as JsonArray) != null)
                This.ParseAttributesThatModBaseDamage(RawAttributesThatModBaseDamage, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatModBaseDamage");
        }

        private void ParseAttributesThatModBaseDamage(JsonArray RawAttributesThatModBaseDamage, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatModBaseDamage, RawAttributesThatModBaseDamageList, "AttributesThatModBaseDamage", ErrorInfo, out RawAttributesThatModBaseDamageListIsEmpty);
        }

        private static void ParseFieldAttributesThatDeltaTaunt(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            JsonArray RawAttributesThatDeltaTaunt;
            if ((RawAttributesThatDeltaTaunt = Value as JsonArray) != null)
                This.ParseAttributesThatDeltaTaunt(RawAttributesThatDeltaTaunt, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatDeltaTaunt");
        }

        private void ParseAttributesThatDeltaTaunt(JsonArray RawAttributesThatDeltaTaunt, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatDeltaTaunt, RawAttributesThatDeltaTauntList, "AttributesThatDeltaTaunt", ErrorInfo, out RawAttributesThatDeltaTauntListIsEmpty);
        }

        private static void ParseFieldAttributesThatModTaunt(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            JsonArray RawAttributesThatModTaunt;
            if ((RawAttributesThatModTaunt = Value as JsonArray) != null)
                This.ParseAttributesThatModTaunt(RawAttributesThatModTaunt, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatModTaunt");
        }

        private void ParseAttributesThatModTaunt(JsonArray RawAttributesThatModTaunt, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatModTaunt, RawAttributesThatModTauntList, "AttributesThatModTaunt", ErrorInfo, out RawAttributesThatModTauntListIsEmpty);
        }

        private static void ParseFieldAttributesThatDeltaRage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            JsonArray RawAttributesThatDeltaRage;
            if ((RawAttributesThatDeltaRage = Value as JsonArray) != null)
                This.ParseAttributesThatDeltaRage(RawAttributesThatDeltaRage, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatDeltaRage");
        }

        private void ParseAttributesThatDeltaRage(JsonArray RawAttributesThatDeltaRage, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatDeltaRage, RawAttributesThatDeltaRageList, "AttributesThatDeltaRage", ErrorInfo, out RawAttributesThatDeltaRageListIsEmpty);
        }

        private static void ParseFieldAttributesThatModRage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            JsonArray RawAttributesThatModRage;
            if ((RawAttributesThatModRage = Value as JsonArray) != null)
                This.ParseAttributesThatModRage(RawAttributesThatModRage, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatModRage");
        }

        private void ParseAttributesThatModRage(JsonArray RawAttributesThatModRage, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatModRage, RawAttributesThatModRageList, "AttributesThatModRage", ErrorInfo, out RawAttributesThatModRageListIsEmpty);
        }

        private static void ParseFieldAttributesThatDeltaRange(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            JsonArray RawAttributesThatDeltaRange;
            if ((RawAttributesThatDeltaRange = Value as JsonArray) != null)
                This.ParseAttributesThatDeltaRange(RawAttributesThatDeltaRange, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatDeltaRange");
        }

        private void ParseAttributesThatDeltaRange(JsonArray RawAttributesThatDeltaRange, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatDeltaRange, RawAttributesThatDeltaRangeList, "AttributesThatDeltaRange", ErrorInfo, out RawAttributesThatDeltaRangeListIsEmpty);
        }

        private static void ParseFieldSpecialValues(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            JsonObject AsJObject;
            JsonArray AsJArray;

            if ((AsJObject = Value as JsonObject) != null)
                This.ParseSpecialValue(AsJObject, "SpecialValues", ErrorInfo);

            else if ((AsJArray = Value as JsonArray) != null)
            {
                foreach (object RawItem in AsJArray)
                    if ((AsJObject = RawItem as JsonObject) != null)
                        This.ParseSpecialValue(AsJObject, null, ErrorInfo);
                    else
                        ErrorInfo.AddInvalidObjectFormat("AbilityPvX SpecialValues Array");
            }

            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX SpecialValues");
        }

        private void ParseSpecialValue(JsonObject RawSpecialValue, string ObjectKey, ParseErrorInfo ErrorInfo)
        {
            SpecialValue ParsedSpecialValue;
            JsonObjectParser<SpecialValue>.InitAsSubitem("SpecialValue", RawSpecialValue, out ParsedSpecialValue, ErrorInfo);

            SpecialValueList.Add(ParsedSpecialValue);
        }

        private static void ParseFieldTauntDelta(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX TauntDelta", This.ParseTauntDelta);
        }

        private void ParseTauntDelta(long RawTauntDelta, ParseErrorInfo ErrorInfo)
        {
            this.RawTauntDelta = (int)RawTauntDelta;
        }

        private static void ParseFieldTempTauntDelta(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX TempTauntDelta", This.ParseTempTauntDelta);
        }

        private void ParseTempTauntDelta(long RawTempTauntDelta, ParseErrorInfo ErrorInfo)
        {
            this.RawTempTauntDelta = (int)RawTempTauntDelta;
        }

        private static void ParseFieldRageCost(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityPvX RageCost", This.ParseRageCost);
        }

        private void ParseRageCost(long RawRageCost, ParseErrorInfo ErrorInfo)
        {
            this.RawRageCost = (int)RawRageCost;
        }

        private static void ParseFieldRageCostMod(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            JsonInteger AsJsonInteger;
            JsonFloat AsJsonFloat;

            if ((AsJsonInteger = Value as JsonInteger) != null)
                This.ParseRageCostMod(AsJsonInteger.Number, ErrorInfo);

            else if ((AsJsonFloat = Value as JsonFloat) != null)
                This.ParseRageCostMod(AsJsonFloat.Number, ErrorInfo);

            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX RageCostMod");
        }

        private void ParseRageCostMod(double RawRageCostMod, ParseErrorInfo ErrorInfo)
        {
            this.RawRageCostMod = RawRageCostMod;
        }

        private List<string> RawAttributesThatDeltaDamageList { get; } = new List<string>();
        private bool RawAttributesThatDeltaDamageListIsEmpty;
        private List<string> RawAttributesThatModDamageList { get; } = new List<string>();
        private bool RawAttributesThatModDamageListIsEmpty;
        private List<string> RawAttributesThatModBaseDamageList { get; } = new List<string>();
        private bool RawAttributesThatModBaseDamageListIsEmpty;
        private List<string> RawAttributesThatDeltaTauntList { get; } = new List<string>();
        private bool RawAttributesThatDeltaTauntListIsEmpty;
        private List<string> RawAttributesThatModTauntList { get; } = new List<string>();
        private bool RawAttributesThatModTauntListIsEmpty;
        private List<string> RawAttributesThatDeltaRageList { get; } = new List<string>();
        private bool RawAttributesThatDeltaRageListIsEmpty;
        private List<string> RawAttributesThatModRageList { get; } = new List<string>();
        private bool RawAttributesThatModRageListIsEmpty;
        private List<string> RawAttributesThatDeltaRangeList { get; } = new List<string>();
        private bool RawAttributesThatDeltaRangeListIsEmpty;
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddInteger("Damage", RawDamage);
            Generator.AddInteger("HealthSpecificDamage", RawHealthSpecificDamage);
            Generator.AddInteger("ExtraDamageIfTargetVulnerable", RawExtraDamageIfTargetVulnerable);
            Generator.AddInteger("ArmorSpecificDamage", RawArmorSpecificDamage);
            Generator.AddInteger("Range", RawRange);
            Generator.AddInteger("PowerCost", RawPowerCost);
            Generator.AddInteger("MetabolismCost", RawMetabolismCost);
            Generator.AddInteger("ArmorMitigationRatio", RawArmorMitigationRatio);
            Generator.AddInteger("AoE", RawAoE);
            Generator.AddInteger("RageBoost", RawRageBoost);
            Generator.AddDouble("RageMultiplier", RawRageMultiplier);
            Generator.AddDouble("Accuracy", RawAccuracy);
            Generator.AddList("AttributesThatDeltaDamage", RawAttributesThatDeltaDamageList);
            Generator.AddList("AttributesThatModDamage", RawAttributesThatModDamageList);
            Generator.AddList("AttributesThatModBaseDamage", RawAttributesThatModBaseDamageList);
            Generator.AddList("AttributesThatDeltaTaunt", RawAttributesThatDeltaTauntList);
            Generator.AddList("AttributesThatModTaunt", RawAttributesThatModTauntList);
            Generator.AddList("AttributesThatDeltaRage", RawAttributesThatDeltaRageList);
            Generator.AddList("AttributesThatModRage", RawAttributesThatModRageList);
            Generator.AddList("AttributesThatDeltaRange", RawAttributesThatDeltaRangeList);

            if (SpecialValueList.Count > 0)
            {
                Generator.OpenArray("SpecialValues");

                foreach (SpecialValue Item in SpecialValueList)
                    Item.GenerateObjectContent(Generator);

                Generator.CloseArray();
            }

            Generator.AddInteger("TauntDelta", RawTauntDelta);
            Generator.AddInteger("TempTauntDelta", RawTempTauntDelta);
            Generator.AddInteger("RageCost", RawRageCost);
            Generator.AddDouble("RageCostMod", RawRageCostMod);

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (SpecialValue Item in SpecialValueList)
                {
                    string FieldContent = "";

                    if (Item.Label != null)
                        FieldContent += Item.Label;

                    if (Item.Suffix != null)
                    {
                        if (FieldContent.Length > 0)
                            FieldContent += " ";

                        FieldContent += Item.Suffix;
                    }

                    if (FieldContent.Length > 0)
                        AddWithFieldSeparator(ref Result, FieldContent);
                }

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> AttributeTable = AllTables[typeof(Attribute)];

            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaDamageList, AttributesThatDeltaDamageTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModDamageList, AttributesThatModDamageTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModBaseDamageList, AttributesThatModBaseDamageTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaTauntList, AttributesThatDeltaTauntTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModTauntList, AttributesThatModTauntTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaRageList, AttributesThatDeltaRageTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModRageList, AttributesThatModRageTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaRangeList, AttributesThatDeltaRangeTable);

            foreach (SpecialValue Item in SpecialValueList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "AbilityPvX"; } }
        #endregion
    }
}
