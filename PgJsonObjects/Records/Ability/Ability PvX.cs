using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityPvX : GenericJsonObject<AbilityPvX>
    {
        #region Constants
        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
        {
            { "Damage", ParseFieldDamage },
            { "ExtraDamageIfTargetVulnerable", ParseFieldExtraDamageIfTargetVulnerable },
            { "HealthSpecificDamage", ParseFieldHealthSpecificDamage },
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
        #endregion

        #region Properties
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
        public Dictionary<string, Attribute> AttributesThatDeltaDamageTable { get; private set; }
        public Dictionary<string, Attribute> AttributesThatModDamageTable { get; private set; }
        public Dictionary<string, Attribute> AttributesThatModBaseDamageTable { get; private set; }
        public Dictionary<string, Attribute> AttributesThatDeltaTauntTable { get; private set; }
        public Dictionary<string, Attribute> AttributesThatModTauntTable { get; private set; }
        public Dictionary<string, Attribute> AttributesThatDeltaRageTable { get; private set; }
        public Dictionary<string, Attribute> AttributesThatModRageTable { get; private set; }
        public Dictionary<string, Attribute> AttributesThatDeltaRangeTable { get; private set; }
        public List<SpecialValue> SpecialValueList { get; private set; }
        public int TauntDelta { get { return RawTauntDelta.HasValue ? RawTauntDelta.Value : 0; } }
        private int? RawTauntDelta;
        public int TempTauntDelta { get { return RawTempTauntDelta.HasValue ? RawTempTauntDelta.Value : 0; } }
        private int? RawTempTauntDelta;
        public int RageCost { get { return RawRageCost.HasValue ? RawRageCost.Value : 0; } }
        private int? RawRageCost;
        public double RageCostMod { get { return RawRageCostMod.HasValue ? RawRageCostMod.Value : 0; } }
        private double? RawRageCostMod;

        protected override string SortingName { get { return null; } }
        #endregion

        #region Client Interface
        private static void ParseFieldDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseDamage((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX Damage");
        }

        private void ParseDamage(int RawDamage, ParseErrorInfo ErrorInfo)
        {
            this.RawDamage = RawDamage;
        }

        private static void ParseFieldExtraDamageIfTargetVulnerable(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseExtraDamageIfTargetVulnerable((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX ExtraDamageIfTargetVulnerable");
        }

        private void ParseExtraDamageIfTargetVulnerable(int RawExtraDamageIfTargetVulnerable, ParseErrorInfo ErrorInfo)
        {
            this.RawExtraDamageIfTargetVulnerable = RawExtraDamageIfTargetVulnerable;
        }

        private static void ParseFieldHealthSpecificDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseHealthSpecificDamage((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX HealthSpecificDamage");
        }

        private void ParseHealthSpecificDamage(int RawHealthSpecificDamage, ParseErrorInfo ErrorInfo)
        {
            this.RawHealthSpecificDamage = RawHealthSpecificDamage;
        }

        private static void ParseFieldArmorSpecificDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseArmorSpecificDamage((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX ArmorSpecificDamage");
        }

        private void ParseArmorSpecificDamage(int RawArmorSpecificDamage, ParseErrorInfo ErrorInfo)
        {
            this.RawArmorSpecificDamage = RawArmorSpecificDamage;
        }

        private static void ParseFieldRange(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRange((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX Range");
        }

        private void ParseRange(int RawRange, ParseErrorInfo ErrorInfo)
        {
            this.RawRange = RawRange;
        }

        private static void ParseFieldPowerCost(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParsePowerCost((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX PowerCost");
        }

        private void ParsePowerCost(int RawPowerCost, ParseErrorInfo ErrorInfo)
        {
            this.RawPowerCost = RawPowerCost;
        }

        private static void ParseFieldMetabolismCost(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseMetabolismCost((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX MetabolismCost");
        }

        private void ParseMetabolismCost(int RawMetabolismCost, ParseErrorInfo ErrorInfo)
        {
            this.RawMetabolismCost = RawMetabolismCost;
        }

        private static void ParseFieldArmorMitigationRatio(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseArmorMitigationRatio((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX ArmorMitigationRatio");
        }

        private void ParseArmorMitigationRatio(int RawArmorMitigationRatio, ParseErrorInfo ErrorInfo)
        {
            this.RawArmorMitigationRatio = RawArmorMitigationRatio;
        }

        private static void ParseFieldAoE(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseAoE((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AoE");
        }

        private void ParseAoE(int RawAoE, ParseErrorInfo ErrorInfo)
        {
            this.RawAoE = RawAoE;
        }

        private static void ParseFieldRageBoost(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRageBoost((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX RageBoost");
        }

        private void ParseRageBoost(int RawRageBoost, ParseErrorInfo ErrorInfo)
        {
            this.RawRageBoost = RawRageBoost;
        }

        private static void ParseFieldRageMultiplier(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRageMultiplier((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseRageMultiplier(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX RageMultiplier");
        }

        private void ParseRageMultiplier(double RawRageMultiplier, ParseErrorInfo ErrorInfo)
        {
            this.RawRageMultiplier = RawRageMultiplier;
        }

        private static void ParseFieldAccuracy(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseAccuracy((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseAccuracy(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX Accuracy");
        }

        private void ParseAccuracy(double RawAccuracy, ParseErrorInfo ErrorInfo)
        {
            this.RawAccuracy = RawAccuracy;
        }

        private static void ParseFieldAttributesThatDeltaDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawAttributesThatDeltaDamage;
            if ((RawAttributesThatDeltaDamage = Value as ArrayList) != null)
                This.ParseAttributesThatDeltaDamage(RawAttributesThatDeltaDamage, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatDeltaDamage");
        }

        private void ParseAttributesThatDeltaDamage(ArrayList RawAttributesThatDeltaDamage, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatDeltaDamage, RawAttributesThatDeltaDamageList, "AttributesThatDeltaDamage", ErrorInfo, out RawAttributesThatDeltaDamageListIsEmpty);
        }

        private static void ParseFieldAttributesThatModDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawAttributesThatModDamage;
            if ((RawAttributesThatModDamage = Value as ArrayList) != null)
                This.ParseAttributesThatModDamage(RawAttributesThatModDamage, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatModDamage");
        }

        private void ParseAttributesThatModDamage(ArrayList RawAttributesThatModDamage, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatModDamage, RawAttributesThatModDamageList, "AttributesThatModDamage", ErrorInfo, out RawAttributesThatModDamageListIsEmpty);
        }

        private static void ParseFieldAttributesThatModBaseDamage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawAttributesThatModBaseDamage;
            if ((RawAttributesThatModBaseDamage = Value as ArrayList) != null)
                This.ParseAttributesThatModBaseDamage(RawAttributesThatModBaseDamage, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatModBaseDamage");
        }

        private void ParseAttributesThatModBaseDamage(ArrayList RawAttributesThatModBaseDamage, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatModBaseDamage, RawAttributesThatModBaseDamageList, "AttributesThatModBaseDamage", ErrorInfo, out RawAttributesThatModBaseDamageListIsEmpty);
        }

        private static void ParseFieldAttributesThatDeltaTaunt(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawAttributesThatDeltaTaunt;
            if ((RawAttributesThatDeltaTaunt = Value as ArrayList) != null)
                This.ParseAttributesThatDeltaTaunt(RawAttributesThatDeltaTaunt, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatDeltaTaunt");
        }

        private void ParseAttributesThatDeltaTaunt(ArrayList RawAttributesThatDeltaTaunt, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatDeltaTaunt, RawAttributesThatDeltaTauntList, "AttributesThatDeltaTaunt", ErrorInfo, out RawAttributesThatDeltaTauntListIsEmpty);
        }

        private static void ParseFieldAttributesThatModTaunt(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawAttributesThatModTaunt;
            if ((RawAttributesThatModTaunt = Value as ArrayList) != null)
                This.ParseAttributesThatModTaunt(RawAttributesThatModTaunt, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatModTaunt");
        }

        private void ParseAttributesThatModTaunt(ArrayList RawAttributesThatModTaunt, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatModTaunt, RawAttributesThatModTauntList, "AttributesThatModTaunt", ErrorInfo, out RawAttributesThatModTauntListIsEmpty);
        }

        private static void ParseFieldAttributesThatDeltaRage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawAttributesThatDeltaRage;
            if ((RawAttributesThatDeltaRage = Value as ArrayList) != null)
                This.ParseAttributesThatDeltaRage(RawAttributesThatDeltaRage, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatDeltaRage");
        }

        private void ParseAttributesThatDeltaRage(ArrayList RawAttributesThatDeltaRage, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatDeltaRage, RawAttributesThatDeltaRageList, "AttributesThatDeltaRage", ErrorInfo, out RawAttributesThatDeltaRageListIsEmpty);
        }

        private static void ParseFieldAttributesThatModRage(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawAttributesThatModRage;
            if ((RawAttributesThatModRage = Value as ArrayList) != null)
                This.ParseAttributesThatModRage(RawAttributesThatModRage, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatModRage");
        }

        private void ParseAttributesThatModRage(ArrayList RawAttributesThatModRage, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatModRage, RawAttributesThatModRageList, "AttributesThatModRage", ErrorInfo, out RawAttributesThatModRageListIsEmpty);
        }

        private static void ParseFieldAttributesThatDeltaRange(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawAttributesThatDeltaRange;
            if ((RawAttributesThatDeltaRange = Value as ArrayList) != null)
                This.ParseAttributesThatDeltaRange(RawAttributesThatDeltaRange, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX AttributesThatDeltaRange");
        }

        private void ParseAttributesThatDeltaRange(ArrayList RawAttributesThatDeltaRange, ParseErrorInfo ErrorInfo)
        {
            ParseStringTable(RawAttributesThatDeltaRange, RawAttributesThatDeltaRangeList, "AttributesThatDeltaRange", ErrorInfo, out RawAttributesThatDeltaRangeListIsEmpty);
        }

        private static void ParseFieldSpecialValues(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> AsDictionaryArray;
            ArrayList AsObjectArray;

            if ((AsDictionaryArray = Value as Dictionary<string, object>) != null)
                This.ParseSpecialValue(AsDictionaryArray, "SpecialValues", ErrorInfo);

            else if ((AsObjectArray = Value as ArrayList) != null)
            {
                foreach (object RawItem in AsObjectArray)
                    if ((AsDictionaryArray = RawItem as Dictionary<string, object>) != null)
                        This.ParseSpecialValue(AsDictionaryArray, null, ErrorInfo);
                    else
                        ErrorInfo.AddInvalidObjectFormat("AbilityPvX SpecialValues Array");
            }

            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX SpecialValues");
        }

        private void ParseSpecialValue(Dictionary<string, object> RawSpecialValue, string ObjectKey, ParseErrorInfo ErrorInfo)
        {
            SpecialValue ParsedSpecialValue;
            JsonObjectParser<SpecialValue>.InitAsSubitem("SpecialValue", RawSpecialValue, out ParsedSpecialValue, ErrorInfo);

            SpecialValueList.Add(ParsedSpecialValue);
        }

        private static void ParseFieldTauntDelta(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseTauntDelta((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX TauntDelta");
        }

        private void ParseTauntDelta(int RawTauntDelta, ParseErrorInfo ErrorInfo)
        {
            this.RawTauntDelta = RawTauntDelta;
        }

        private static void ParseFieldTempTauntDelta(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseTempTauntDelta((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX TempTauntDelta");
        }

        private void ParseTempTauntDelta(int RawTempTauntDelta, ParseErrorInfo ErrorInfo)
        {
            this.RawTempTauntDelta = RawTempTauntDelta;
        }

        private static void ParseFieldRageCost(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRageCost((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX RageCost");
        }

        private void ParseRageCost(int RawRageCost, ParseErrorInfo ErrorInfo)
        {
            this.RawRageCost = RawRageCost;
        }

        private static void ParseFieldRageCostMod(AbilityPvX This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRageCostMod((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseRageCostMod(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityPvX RageCostMod");
        }

        private void ParseRageCostMod(double RawRageCostMod, ParseErrorInfo ErrorInfo)
        {
            this.RawRageCostMod = RawRageCostMod;
        }

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddInteger("Damage", RawDamage);
            Generator.AddInteger("ExtraDamageIfTargetVulnerable", RawExtraDamageIfTargetVulnerable);
            Generator.AddInteger("HealthSpecificDamage", RawHealthSpecificDamage);
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

        private void GenerateListContent(JsonGenerator Generator, string ArrayName, List<string> ConnectedList, bool IsListEmpty)
        {
            if (ConnectedList.Count > 0 || IsListEmpty)
            {
                Generator.OpenArray(ArrayName);

                foreach (string s in ConnectedList)
                    Generator.AddString(null, s);

                Generator.CloseArray();
            }
        }

        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (SpecialValue Item in SpecialValueList)
                    Result += Item.TextContent + JsonGenerator.FieldSeparator;

                return Result;
            }
        }

        private List<string> RawAttributesThatDeltaDamageList;
        private bool RawAttributesThatDeltaDamageListIsEmpty;
        private List<string> RawAttributesThatModDamageList;
        private bool RawAttributesThatModDamageListIsEmpty;
        private List<string> RawAttributesThatModBaseDamageList;
        private bool RawAttributesThatModBaseDamageListIsEmpty;
        private List<string> RawAttributesThatDeltaTauntList;
        private bool RawAttributesThatDeltaTauntListIsEmpty;
        private List<string> RawAttributesThatModTauntList;
        private bool RawAttributesThatModTauntListIsEmpty;
        private List<string> RawAttributesThatDeltaRageList;
        private bool RawAttributesThatDeltaRageListIsEmpty;
        private List<string> RawAttributesThatModRageList;
        private bool RawAttributesThatModRageListIsEmpty;
        private List<string> RawAttributesThatDeltaRangeList;
        private bool RawAttributesThatDeltaRangeListIsEmpty;
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "AbilityPvX"; } }

        protected override void InitializeFields()
        {
            RawAttributesThatDeltaDamageList = new List<string>();
            AttributesThatDeltaDamageTable = new Dictionary<string, Attribute>();
            RawAttributesThatModDamageList = new List<string>();
            AttributesThatModDamageTable = new Dictionary<string, Attribute>();
            RawAttributesThatModBaseDamageList = new List<string>();
            AttributesThatModBaseDamageTable = new Dictionary<string, Attribute>();
            RawAttributesThatDeltaTauntList = new List<string>();
            AttributesThatDeltaTauntTable = new Dictionary<string, Attribute>();
            RawAttributesThatModTauntList = new List<string>();
            AttributesThatModTauntTable = new Dictionary<string, Attribute>();
            RawAttributesThatDeltaRageList = new List<string>();
            AttributesThatDeltaRageTable = new Dictionary<string, Attribute>();
            RawAttributesThatModRageList = new List<string>();
            AttributesThatModRageTable = new Dictionary<string, Attribute>();
            RawAttributesThatDeltaRangeList = new List<string>();
            AttributesThatDeltaRangeTable = new Dictionary<string, Attribute>();
            SpecialValueList = new List<SpecialValue>();
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaDamageList, AttributesThatDeltaDamageTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModDamageList, AttributesThatModDamageTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModBaseDamageList, AttributesThatModBaseDamageTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaTauntList, AttributesThatDeltaTauntTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModTauntList, AttributesThatModTauntTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaRageList, AttributesThatDeltaRageTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModRageList, AttributesThatModRageTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaRangeList, AttributesThatDeltaRangeTable);

            foreach (SpecialValue Item in SpecialValueList)
                IsConnected |= Item.Connect(ErrorInfo, this, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            return IsConnected;
        }
        #endregion
    }
}
