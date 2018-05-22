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
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX Damage", This.ParseDamage);
        }

        private void ParseDamage(long RawDamage, ParseErrorInfo ErrorInfo)
        {
            this.RawDamage = (int)RawDamage;
        }

        private static void ParseFieldExtraDamageIfTargetVulnerable(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX ExtraDamageIfTargetVulnerable", This.ParseExtraDamageIfTargetVulnerable);
        }

        private void ParseExtraDamageIfTargetVulnerable(long RawExtraDamageIfTargetVulnerable, ParseErrorInfo ErrorInfo)
        {
            this.RawExtraDamageIfTargetVulnerable = (int)RawExtraDamageIfTargetVulnerable;
        }

        private static void ParseFieldHealthSpecificDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX HealthSpecificDamage", This.ParseHealthSpecificDamage);
        }

        private void ParseHealthSpecificDamage(long RawHealthSpecificDamage, ParseErrorInfo ErrorInfo)
        {
            this.RawHealthSpecificDamage = (int)RawHealthSpecificDamage;
        }

        private static void ParseFieldArmorSpecificDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX ArmorSpecificDamage", This.ParseArmorSpecificDamage);
        }

        private void ParseArmorSpecificDamage(long RawArmorSpecificDamage, ParseErrorInfo ErrorInfo)
        {
            this.RawArmorSpecificDamage = (int)RawArmorSpecificDamage;
        }

        private static void ParseFieldRange(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX Range", This.ParseRange);
        }

        private void ParseRange(long RawRange, ParseErrorInfo ErrorInfo)
        {
            this.RawRange = (int)RawRange;
        }

        private static void ParseFieldPowerCost(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX PowerCost", This.ParsePowerCost);
        }

        private void ParsePowerCost(long RawPowerCost, ParseErrorInfo ErrorInfo)
        {
            this.RawPowerCost = (int)RawPowerCost;
        }

        private static void ParseFieldMetabolismCost(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX MetabolismCost", This.ParseMetabolismCost);
        }

        private void ParseMetabolismCost(long RawMetabolismCost, ParseErrorInfo ErrorInfo)
        {
            this.RawMetabolismCost = (int)RawMetabolismCost;
        }

        private static void ParseFieldArmorMitigationRatio(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX ArmorMitigationRatio", This.ParseArmorMitigationRatio);
        }

        private void ParseArmorMitigationRatio(long RawArmorMitigationRatio, ParseErrorInfo ErrorInfo)
        {
            this.RawArmorMitigationRatio = (int)RawArmorMitigationRatio;
        }

        private static void ParseFieldAoE(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX AoE", This.ParseAoE);
        }

        private void ParseAoE(long RawAoE, ParseErrorInfo ErrorInfo)
        {
            this.RawAoE = (int)RawAoE;
        }

        private static void ParseFieldRageBoost(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX RageBoost", This.ParseRageBoost);
        }

        private void ParseRageBoost(long RawRageBoost, ParseErrorInfo ErrorInfo)
        {
            this.RawRageBoost = (int)RawRageBoost;
        }

        private static void ParseFieldRageMultiplier(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "AbilityPvX RageMultiplier", This.ParseRageMultiplier);
        }

        private void ParseRageMultiplier(double RawRageMultiplier, ParseErrorInfo ErrorInfo)
        {
            this.RawRageMultiplier = RawRageMultiplier;
        }

        private static void ParseFieldAccuracy(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "AbilityPvX Accuracy", This.ParseAccuracy);
        }

        private void ParseAccuracy(double RawAccuracy, ParseErrorInfo ErrorInfo)
        {
            this.RawAccuracy = RawAccuracy;
        }

        private static void ParseFieldAttributesThatDeltaDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "AbilityPvX AttributesThatDeltaDamage", This.ParseAttributesThatDeltaDamage);
        }

        private bool ParseAttributesThatDeltaDamage(string RawAttributesThatDeltaDamage, ParseErrorInfo ErrorInfo)
        {
            RawAttributesThatDeltaDamageList.Add(RawAttributesThatDeltaDamage);
            return true;
        }

        private static void ParseFieldAttributesThatModDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "AbilityPvX AttributesThatModDamage", This.ParseAttributesThatModDamage);
        }

        private bool ParseAttributesThatModDamage(string RawAttributesThatModDamage, ParseErrorInfo ErrorInfo)
        {
            RawAttributesThatModDamageList.Add(RawAttributesThatModDamage);
            return true;
        }

        private static void ParseFieldAttributesThatModBaseDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "AbilityPvX AttributesThatModBaseDamage", This.ParseAttributesThatModBaseDamage);
        }

        private bool ParseAttributesThatModBaseDamage(string RawAttributesThatModBaseDamage, ParseErrorInfo ErrorInfo)
        {
            RawAttributesThatModBaseDamageList.Add(RawAttributesThatModBaseDamage);
            return true;
        }

        private static void ParseFieldAttributesThatDeltaTaunt(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "AbilityPvX AttributesThatDeltaTaunt", This.ParseAttributesThatDeltaTaunt);
        }

        private bool ParseAttributesThatDeltaTaunt(string RawAttributesThatDeltaTaunt, ParseErrorInfo ErrorInfo)
        {
            RawAttributesThatDeltaTauntList.Add(RawAttributesThatDeltaTaunt);
            return true;
        }

        private static void ParseFieldAttributesThatModTaunt(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "AbilityPvX AttributesThatModTaunt", This.ParseAttributesThatModTaunt);
        }

        private bool ParseAttributesThatModTaunt(string RawAttributesThatModTaunt, ParseErrorInfo ErrorInfo)
        {
            RawAttributesThatModTauntList.Add(RawAttributesThatModTaunt);
            return true;
        }

        private static void ParseFieldAttributesThatDeltaRage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "AbilityPvX AttributesThatDeltaRage", This.ParseAttributesThatDeltaRage);
        }

        private bool ParseAttributesThatDeltaRage(string RawAttributesThatDeltaRage, ParseErrorInfo ErrorInfo)
        {
            RawAttributesThatDeltaRageList.Add(RawAttributesThatDeltaRage);
            return true;
        }

        private static void ParseFieldAttributesThatModRage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "AbilityPvX AttributesThatModRage", This.ParseAttributesThatModRage);
        }

        private bool ParseAttributesThatModRage(string RawAttributesThatModRage, ParseErrorInfo ErrorInfo)
        {
            RawAttributesThatModRageList.Add(RawAttributesThatModRage);
            return true;
        }

        private static void ParseFieldAttributesThatDeltaRange(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "AbilityPvX AttributesThatDeltaRange", This.ParseAttributesThatDeltaRange);
        }

        private bool ParseAttributesThatDeltaRange(string RawAttributesThatDeltaRange, ParseErrorInfo ErrorInfo)
        {
            RawAttributesThatDeltaRangeList.Add(RawAttributesThatDeltaRange);
            return true;
        }

        private static void ParseFieldSpecialValues(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringObjectOrArray(Value, ErrorInfo, "AbilityPvX SpecialValues", This.ParseSpecialValue);
        }

        private void ParseSpecialValue(JsonObject RawSpecialValue, ParseErrorInfo ErrorInfo)
        {
            SpecialValue ParsedSpecialValue;
            JsonObjectParser<SpecialValue>.InitAsSubitem("SpecialValue", RawSpecialValue, out ParsedSpecialValue, ErrorInfo);

            SpecialValueList.Add(ParsedSpecialValue);
        }

        private static void ParseFieldTauntDelta(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX TauntDelta", This.ParseTauntDelta);
        }

        private void ParseTauntDelta(long RawTauntDelta, ParseErrorInfo ErrorInfo)
        {
            this.RawTauntDelta = (int)RawTauntDelta;
        }

        private static void ParseFieldTempTauntDelta(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX TempTauntDelta", This.ParseTempTauntDelta);
        }

        private void ParseTempTauntDelta(long RawTempTauntDelta, ParseErrorInfo ErrorInfo)
        {
            this.RawTempTauntDelta = (int)RawTempTauntDelta;
        }

        private static void ParseFieldRageCost(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "AbilityPvX RageCost", This.ParseRageCost);
        }

        private void ParseRageCost(long RawRageCost, ParseErrorInfo ErrorInfo)
        {
            this.RawRageCost = (int)RawRageCost;
        }

        private static void ParseFieldRageCostMod(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueFloat(Value, ErrorInfo, "AbilityPvX RageCostMod", This.ParseRageCostMod);
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
