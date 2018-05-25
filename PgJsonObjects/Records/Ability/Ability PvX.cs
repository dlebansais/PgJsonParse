using PgJsonReader;
using System;
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
        public List<DoT> DoTList { get; } = new List<DoT>();
        public int TauntDelta { get { return RawTauntDelta.HasValue ? RawTauntDelta.Value : 0; } }
        private int? RawTauntDelta;
        public int TempTauntDelta { get { return RawTempTauntDelta.HasValue ? RawTempTauntDelta.Value : 0; } }
        private int? RawTempTauntDelta;
        public int RageCost { get { return RawRageCost.HasValue ? RawRageCost.Value : 0; } }
        private int? RawRageCost;
        public double RageCostMod { get { return RawRageCostMod.HasValue ? RawRageCostMod.Value : 0; } }
        private double? RawRageCostMod;
        public List<PreEffect> SelfPreEffectList { get; } = new List<PreEffect>();
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Damage", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawDamage = value,
                GetInteger = () => RawDamage } },
            { "ExtraDamageIfTargetVulnerable", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawExtraDamageIfTargetVulnerable = value,
                GetInteger = () => RawExtraDamageIfTargetVulnerable  } },
            { "HealthSpecificDamage", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawHealthSpecificDamage = value,
                GetInteger = () => RawHealthSpecificDamage } },
            { "ArmorSpecificDamage", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawArmorSpecificDamage = value,
                GetInteger = () => RawArmorSpecificDamage } },
            { "Range", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawRange = value,
                GetInteger = () => RawRange } },
            { "PowerCost", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawPowerCost = value,
                GetInteger = () => RawPowerCost } },
            { "MetabolismCost", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMetabolismCost = value,
                GetInteger = () => RawMetabolismCost } },
            { "ArmorMitigationRatio", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawArmorMitigationRatio = value,
                GetInteger = () => RawArmorMitigationRatio } },
            { "AoE", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawAoE = value,
                GetInteger = () => RawAoE } },
            { "RageBoost", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawRageBoost = value,
                GetInteger = () => RawRageBoost  } },
            { "RageMultiplier", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawRageMultiplier = value,
                GetFloat = () => RawRageMultiplier } },
            { "Accuracy", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawAccuracy = value,
                GetFloat = () => RawAccuracy } },
            { "AttributesThatDeltaDamage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaDamageList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaDamageListIsEmpty = true,
                GetStringArray = () => RawAttributesThatDeltaDamageList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaDamageListIsEmpty } },
            { "AttributesThatModDamage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModDamageList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModDamageListIsEmpty = true,
                GetStringArray = () => RawAttributesThatModDamageList,
                GetArrayIsEmpty = () => RawAttributesThatModDamageListIsEmpty } },
            { "AttributesThatModBaseDamage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModBaseDamageList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModBaseDamageListIsEmpty = true,
                GetStringArray = () => RawAttributesThatModBaseDamageList,
                GetArrayIsEmpty = () => RawAttributesThatModBaseDamageListIsEmpty } },
            { "AttributesThatDeltaTaunt", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaTauntList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaTauntListIsEmpty = true,
                GetStringArray = () => RawAttributesThatDeltaTauntList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaTauntListIsEmpty } },
            { "AttributesThatModTaunt", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModTauntList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModTauntListIsEmpty = true,
                GetStringArray = () => RawAttributesThatModTauntList,
                GetArrayIsEmpty = () => RawAttributesThatModTauntListIsEmpty } },
            { "AttributesThatDeltaRage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaRageList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaRageListIsEmpty = true,
                GetStringArray = () => RawAttributesThatDeltaRageList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaRageListIsEmpty } },
            { "AttributesThatModRage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModRageList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModRageListIsEmpty = true,
                GetStringArray = () => RawAttributesThatModRageList,
                GetArrayIsEmpty = () => RawAttributesThatModRageListIsEmpty } },
            { "AttributesThatDeltaRange", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaRangeList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaRangeListIsEmpty = true,
                GetStringArray = () => RawAttributesThatDeltaRangeList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaRangeListIsEmpty } },
            { "SpecialValues", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<SpecialValue>.ParseList("SpecialValue", value, SpecialValueList, errorInfo),
                SetArrayIsEmpty = () => RawAttributesThatDeltaRangeListIsEmpty = true,
                GetObjectArray = () => SpecialValueList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaRangeListIsEmpty } },
            { "TauntDelta", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawTauntDelta = value,
                GetInteger = () => RawTauntDelta } },
            { "TempTauntDelta", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawTempTauntDelta = value,
                GetInteger = () => RawTempTauntDelta } },
            { "RageCost", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawRageCost = value,
                GetInteger = () => RawRageCost } },
            { "RageCostMod", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawRageCostMod = value,
                GetFloat = () => RawRageCostMod } },
            { "DoTs", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<DoT>.ParseList("DoTs", value, DoTList, errorInfo),
                GetObjectArray = () => DoTList } },
            { "SelfPreEffects", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<PreEffect>.ParseList(value, SelfPreEffectList, errorInfo),
                GetStringArray = () => StringToEnumConversion<PreEffect>.ToStringList(SelfPreEffectList) } },
        }; } }

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
        /*
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
            Generator.AddList("AttributesThatDeltaDamage", RawAttributesThatDeltaDamageList, RawAttributesThatDeltaDamageListIsEmpty);
            Generator.AddList("AttributesThatModDamage", RawAttributesThatModDamageList, RawAttributesThatModDamageListIsEmpty);
            Generator.AddList("AttributesThatModBaseDamage", RawAttributesThatModBaseDamageList, RawAttributesThatModBaseDamageListIsEmpty);
            Generator.AddList("AttributesThatDeltaTaunt", RawAttributesThatDeltaTauntList, RawAttributesThatDeltaTauntListIsEmpty);
            Generator.AddList("AttributesThatModTaunt", RawAttributesThatModTauntList, RawAttributesThatModTauntListIsEmpty);
            Generator.AddList("AttributesThatDeltaRage", RawAttributesThatDeltaRageList, RawAttributesThatDeltaRageListIsEmpty);
            Generator.AddList("AttributesThatModRage", RawAttributesThatModRageList, RawAttributesThatModRageListIsEmpty);
            Generator.AddList("AttributesThatDeltaRange", RawAttributesThatDeltaRangeList, RawAttributesThatDeltaRangeListIsEmpty);

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
        }*/
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

                foreach (PreEffect Item in SelfPreEffectList)
                    AddWithFieldSeparator(ref Result, TextMaps.PreEffectTextMap[Item]);

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
