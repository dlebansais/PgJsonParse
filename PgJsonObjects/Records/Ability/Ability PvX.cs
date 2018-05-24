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
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Damage", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawDamage = value; }} },
            { "ExtraDamageIfTargetVulnerable", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawExtraDamageIfTargetVulnerable = value; }} },
            { "HealthSpecificDamage", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawHealthSpecificDamage = value; }} },
            { "ArmorSpecificDamage", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawArmorSpecificDamage = value; }} },
            { "Range", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawRange = value; }} },
            { "PowerCost", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawPowerCost = value; }} },
            { "MetabolismCost", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawMetabolismCost = value; }} },
            { "ArmorMitigationRatio", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawArmorMitigationRatio = value; }} },
            { "AoE", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawAoE = value; }} },
            { "RageBoost", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawRageBoost = value; }} },
            { "RageMultiplier", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawRageMultiplier = value; }} },
            { "Accuracy", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawAccuracy = value; }} },
            { "AttributesThatDeltaDamage", new FieldParser() { Type = FieldType.SimpleStringArray, ParserSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { RawAttributesThatDeltaDamageList.Add(value); }, ParserSetArrayIsEmpty = () => RawAttributesThatDeltaDamageListIsEmpty = true } },
            { "AttributesThatModDamage", new FieldParser() { Type = FieldType.SimpleStringArray, ParserSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { RawAttributesThatModDamageList.Add(value); }, ParserSetArrayIsEmpty = () => RawAttributesThatModDamageListIsEmpty = true } },
            { "AttributesThatModBaseDamage", new FieldParser() { Type = FieldType.SimpleStringArray, ParserSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { RawAttributesThatModBaseDamageList.Add(value); }, ParserSetArrayIsEmpty = () => RawAttributesThatModBaseDamageListIsEmpty = true } },
            { "AttributesThatDeltaTaunt", new FieldParser() { Type = FieldType.SimpleStringArray, ParserSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { RawAttributesThatDeltaTauntList.Add(value); }, ParserSetArrayIsEmpty = () => RawAttributesThatDeltaTauntListIsEmpty = true } },
            { "AttributesThatModTaunt", new FieldParser() { Type = FieldType.SimpleStringArray, ParserSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { RawAttributesThatModTauntList.Add(value); }, ParserSetArrayIsEmpty = () => RawAttributesThatModTauntListIsEmpty = true } },
            { "AttributesThatDeltaRage", new FieldParser() { Type = FieldType.SimpleStringArray, ParserSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { RawAttributesThatDeltaRageList.Add(value); }, ParserSetArrayIsEmpty = () => RawAttributesThatDeltaRageListIsEmpty = true } },
            { "AttributesThatModRage", new FieldParser() { Type = FieldType.SimpleStringArray, ParserSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { RawAttributesThatModRageList.Add(value); }, ParserSetArrayIsEmpty = () => RawAttributesThatModRageListIsEmpty = true } },
            { "AttributesThatDeltaRange", new FieldParser() { Type = FieldType.SimpleStringArray, ParserSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { RawAttributesThatDeltaRangeList.Add(value); }, ParserSetArrayIsEmpty = () => RawAttributesThatDeltaRangeListIsEmpty = true } },
            { "SpecialValues", new FieldParser() { Type = FieldType.ObjectArray, ParserObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => { JsonObjectParser<SpecialValue>.ParseList("SpecialValue", value, SpecialValueList, errorInfo); }, ParserSetArrayIsEmpty = () => RawAttributesThatDeltaRangeListIsEmpty = true } },
            { "TauntDelta", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawTauntDelta = value; }} },
            { "TempTauntDelta", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawTempTauntDelta = value; }} },
            { "RageCost", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawRageCost = value; }} },
            { "RageCostMod", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawRageCostMod = value; }} },
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
