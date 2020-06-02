using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityPvX : GenericJsonObject<AbilityPvX>, IPgAbilityPvX
    {
        #region Direct Properties
        public int Damage { get { return RawDamage.HasValue ? RawDamage.Value : 0; } }
        public int? RawDamage { get; private set; }
        public int ExtraDamageIfTargetVulnerable { get { return RawExtraDamageIfTargetVulnerable.HasValue ? RawExtraDamageIfTargetVulnerable.Value : 0; } }
        public int? RawExtraDamageIfTargetVulnerable { get; private set; }
        public int HealthSpecificDamage { get { return RawHealthSpecificDamage.HasValue ? RawHealthSpecificDamage.Value : 0; } }
        public int? RawHealthSpecificDamage { get; private set; }
        public int ArmorSpecificDamage { get { return RawArmorSpecificDamage.HasValue ? RawArmorSpecificDamage.Value : 0; } }
        public int? RawArmorSpecificDamage { get; private set; }
        public int Range { get { return RawRange.HasValue ? RawRange.Value : 0; } }
        public int? RawRange { get; private set; }
        public int PowerCost { get { return RawPowerCost.HasValue ? RawPowerCost.Value : 0; } }
        public int? RawPowerCost { get; private set; }
        public int MetabolismCost { get { return RawMetabolismCost.HasValue ? RawMetabolismCost.Value : 0; } }
        public int? RawMetabolismCost { get; private set; }
        public int ArmorMitigationRatio { get { return RawArmorMitigationRatio.HasValue ? RawArmorMitigationRatio.Value : 0; } }
        public int? RawArmorMitigationRatio { get; private set; }
        public int AoE { get { return RawAoE.HasValue ? RawAoE.Value : 0; } }
        public int? RawAoE { get; private set; }
        public int RageBoost { get { return RawRageBoost.HasValue ? RawRageBoost.Value : 0; } }
        public int? RawRageBoost { get; private set; }
        public double RageMultiplier { get { return RawRageMultiplier.HasValue ? RawRageMultiplier.Value : 1.0; } }
        public double? RawRageMultiplier { get; private set; }
        public double Accuracy { get { return RawAccuracy.HasValue ? RawAccuracy.Value : 0; } }
        public double? RawAccuracy { get; private set; }
        public IPgSpecialValueCollection SpecialValueList { get; } = new SpecialValueCollection();
        public IPgDoTCollection DoTList { get; } = new DoTCollection();
        public int TauntDelta { get { return RawTauntDelta.HasValue ? RawTauntDelta.Value : 0; } }
        public int? RawTauntDelta { get; private set; }
        public int TempTauntDelta { get { return RawTempTauntDelta.HasValue ? RawTempTauntDelta.Value : 0; } }
        public int? RawTempTauntDelta { get; private set; }
        public int RageCost { get { return RawRageCost.HasValue ? RawRageCost.Value : 0; } }
        public int? RawRageCost { get; private set; }
        public double RageCostMod { get { return RawRageCostMod.HasValue ? RawRageCostMod.Value : 0; } }
        public double? RawRageCostMod { get; private set; }
        public double CritDamageMod { get { return RawCritDamageMod.HasValue ? RawCritDamageMod.Value : 0; } }
        public double? RawCritDamageMod { get; private set; }
        public List<PreEffect> SelfPreEffectList { get; } = new List<PreEffect>();
        public IPgAttributeCollection AttributesThatDeltaDamageList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatModDamageList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatModBaseDamageList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatDeltaTauntList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatModTauntList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatDeltaRageList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatModRageList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatDeltaRangeList { get; private set; } = null;
        //public IPgAttributeCollection AttributesThatDeltaDamageLastList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatDeltaAccuracyList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatModCritDamageList { get; private set; } = null;
        public List<SelfEffect> SelfEffectOnCritList { get; } = new List<SelfEffect>();
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Damage", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawDamage = value,
                GetInteger = () => RawDamage } },
            { "HealthSpecificDamage", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawHealthSpecificDamage = value,
                GetInteger = () => RawHealthSpecificDamage } },
            { "ExtraDamageIfTargetVulnerable", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawExtraDamageIfTargetVulnerable = value,
                GetInteger = () => RawExtraDamageIfTargetVulnerable  } },
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
            { "SelfPreEffects", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = ParseSelfPreEffects,
                GetStringArray = GetSelfPreEffects } },
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
                GetStringArray = () => AttributesThatDeltaDamageList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaDamageListIsEmpty } },
            { "AttributesThatModDamage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModDamageList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModDamageListIsEmpty = true,
                GetStringArray = () => AttributesThatModDamageList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModDamageListIsEmpty } },
            { "AttributesThatModBaseDamage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModBaseDamageList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModBaseDamageListIsEmpty = true,
                GetStringArray = () => AttributesThatModBaseDamageList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModBaseDamageListIsEmpty } },
            { "AttributesThatDeltaTaunt", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaTauntList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaTauntListIsEmpty = true,
                GetStringArray = () => AttributesThatDeltaTauntList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaTauntListIsEmpty } },
            { "AttributesThatModTaunt", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModTauntList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModTauntListIsEmpty = true,
                GetStringArray = () => AttributesThatModTauntList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModTauntListIsEmpty } },
            { "AttributesThatDeltaRage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaRageList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaRageListIsEmpty = true,
                GetStringArray = () => AttributesThatDeltaRageList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaRageListIsEmpty } },
            { "AttributesThatModRage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModRageList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModRageListIsEmpty = true,
                GetStringArray = () => AttributesThatModRageList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModRageListIsEmpty } },
            { "AttributesThatDeltaRange", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaRangeList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaRangeListIsEmpty = true,
                GetStringArray = () => AttributesThatDeltaRangeList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaRangeListIsEmpty } },
            { "AttributesThatDeltaAccuracy", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaAccuracyList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaAccuracyListIsEmpty = true,
                GetStringArray = () => AttributesThatDeltaAccuracyList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaAccuracyListIsEmpty } },
            { "AttributesThatModCritDamage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModCritDamageList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModCritDamageListIsEmpty = true,
                GetStringArray = () => AttributesThatModCritDamageList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModCritDamageListIsEmpty } },
            { "SpecialValues", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<SpecialValue>.ParseList("SpecialValue", value, SpecialValueList, errorInfo),
                GetObjectArray = () => SpecialValueList } },
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
            { "CritDamageMod", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawCritDamageMod = value,
                GetFloat = () => RawCritDamageMod } },
            { "SelfEffectsOnCrit", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<SelfEffect>.ParseList(value, SelfEffectOnCritList, errorInfo),
                GetStringArray = () => StringToEnumConversion<SelfEffect>.ToStringList(SelfEffectOnCritList) } },
        }; } }

        private void ParseSelfPreEffects(string value, ParseErrorInfo errorInfo)
        {
            int StartIndex = value.IndexOf('(');
            int EndIndex = value.IndexOf(')');
            if (StartIndex > 0 && EndIndex > StartIndex + 1 && EndIndex + 1 == value.Length)
            {
                string Prefix = value.Substring(0, StartIndex);
                if (Prefix == "EnhanceZombie")
                {
                    string Enhancement = value.Substring(StartIndex + 1, EndIndex - StartIndex - 1);
                    StringToEnumConversion<PreEffect>.ParseList(Enhancement, SelfPreEffectList, errorInfo);
                    return;
                }
            }

            StringToEnumConversion<PreEffect>.ParseList(value, SelfPreEffectList, errorInfo);
        }

        private List<string> GetSelfPreEffects()
        {
            List<string> Result = new List<string>();

            foreach (PreEffect Item in SelfPreEffectList)
            {
                string s;
                
                if (Item >= PreEffect.SuperZombie1 && Item <= PreEffect.SuperZombie7)
                    s = $"EnhanceZombie({StringToEnumConversion<PreEffect>.ToString(Item)})";
                else
                    s = StringToEnumConversion<PreEffect>.ToString(Item);

                Result.Add(s);
            }

            return Result;
        }

        private List<string> RawAttributesThatDeltaDamageList { get; } = new List<string>();
        public bool RawAttributesThatDeltaDamageListIsEmpty { get; private set; }
        private List<string> RawAttributesThatModDamageList { get; } = new List<string>();
        public bool RawAttributesThatModDamageListIsEmpty { get; private set; }
        private List<string> RawAttributesThatModBaseDamageList { get; } = new List<string>();
        public bool RawAttributesThatModBaseDamageListIsEmpty { get; private set; }
        private List<string> RawAttributesThatDeltaTauntList { get; } = new List<string>();
        public bool RawAttributesThatDeltaTauntListIsEmpty { get; private set; }
        private List<string> RawAttributesThatModTauntList { get; } = new List<string>();
        public bool RawAttributesThatModTauntListIsEmpty { get; private set; }
        private List<string> RawAttributesThatDeltaRageList { get; } = new List<string>();
        public bool RawAttributesThatDeltaRageListIsEmpty { get; private set; }
        private List<string> RawAttributesThatModRageList { get; } = new List<string>();
        public bool RawAttributesThatModRageListIsEmpty { get; private set; }
        private List<string> RawAttributesThatDeltaRangeList { get; } = new List<string>();
        public bool RawAttributesThatDeltaRangeListIsEmpty { get; private set; }
        //private List<string> RawAttributesThatDeltaDamageLastList { get; } = new List<string>();
        //public bool RawAttributesThatDeltaDamageLastListIsEmpty { get; private set; }
        private List<string> RawAttributesThatDeltaAccuracyList { get; } = new List<string>();
        public bool RawAttributesThatDeltaAccuracyListIsEmpty { get; private set; }
        private List<string> RawAttributesThatModCritDamageList { get; } = new List<string>();
        public bool RawAttributesThatModCritDamageListIsEmpty { get; private set; }
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

                foreach (SelfEffect Item in SelfEffectOnCritList)
                    AddWithFieldSeparator(ref Result, TextMaps.SelfEffectTextMap[Item]);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IJsonKey> AttributeTable = AllTables[typeof(Attribute)];

            AttributesThatDeltaDamageList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatDeltaDamageList, AttributesThatDeltaDamageList, ref IsConnected);
            AttributesThatModDamageList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatModDamageList, AttributesThatModDamageList, ref IsConnected);
            AttributesThatModBaseDamageList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatModBaseDamageList, AttributesThatModBaseDamageList, ref IsConnected);
            AttributesThatDeltaTauntList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatDeltaTauntList, AttributesThatDeltaTauntList, ref IsConnected);
            AttributesThatModTauntList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatModTauntList, AttributesThatModTauntList, ref IsConnected);
            AttributesThatDeltaRageList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatDeltaRageList, AttributesThatDeltaRageList, ref IsConnected);
            AttributesThatModRageList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatModRageList, AttributesThatModRageList, ref IsConnected);
            AttributesThatDeltaRangeList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatDeltaRangeList, AttributesThatDeltaRangeList, ref IsConnected);
            //AttributesThatDeltaDamageLastList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatDeltaDamageLastList, AttributesThatDeltaDamageLastList, ref IsConnected);
            AttributesThatDeltaAccuracyList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatDeltaAccuracyList, AttributesThatDeltaAccuracyList, ref IsConnected);
            AttributesThatModCritDamageList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatModCritDamageList, AttributesThatModCritDamageList, ref IsConnected);

            foreach (SpecialValue Item in SpecialValueList)
                IsConnected |= Item.Connect(ErrorInfo, Parent, AllTables);

            foreach (DoT Item in DoTList)
                IsConnected |= Item.Connect(ErrorInfo, Parent, AllTables);

            return IsConnected;
        }

        private IPgAttributeCollection ConnectAttributes(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> AttributeTable, List<string> RawAttributes, IPgAttributeCollection Attributes, ref bool IsConnected)
        {
            if (Attributes == null)
            {
                Attributes = new AttributeCollection();
                foreach (string RawAttribute in RawAttributes)
                {
                    IPgAttribute ConnectedAttribute = null;
                    bool IsAttributeParsed = false;
                    ConnectedAttribute = Attribute.ConnectSingleProperty(ErrorInfo, AttributeTable, RawAttribute, ConnectedAttribute, ref IsAttributeParsed, ref IsConnected);
                    if (ConnectedAttribute != null)
                        Attributes.Add(ConnectedAttribute);
                }
            }

            return Attributes;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "AbilityPvX"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();
            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddInt(RawDamage, data, ref offset, BaseOffset, 4);
            AddInt(RawExtraDamageIfTargetVulnerable, data, ref offset, BaseOffset, 8);
            AddInt(RawHealthSpecificDamage, data, ref offset, BaseOffset, 12);
            AddInt(RawArmorSpecificDamage, data, ref offset, BaseOffset, 16);
            AddInt(RawRange, data, ref offset, BaseOffset, 20);
            AddInt(RawPowerCost, data, ref offset, BaseOffset, 24);
            AddInt(RawMetabolismCost, data, ref offset, BaseOffset, 28);
            AddInt(RawArmorMitigationRatio, data, ref offset, BaseOffset, 32);
            AddInt(RawAoE, data, ref offset, BaseOffset, 36);
            AddInt(RawRageBoost, data, ref offset, BaseOffset, 40);
            AddDouble(RawRageMultiplier, data, ref offset, BaseOffset, 44);
            AddDouble(RawAccuracy, data, ref offset, BaseOffset, 48);
            AddObjectList(SpecialValueList, data, ref offset, BaseOffset, 52, StoredObjectListTable);
            AddObjectList(DoTList, data, ref offset, BaseOffset, 56, StoredObjectListTable);
            AddInt(RawTauntDelta, data, ref offset, BaseOffset, 60);
            AddInt(RawTempTauntDelta, data, ref offset, BaseOffset, 64);
            AddInt(RawRageCost, data, ref offset, BaseOffset, 68);
            AddDouble(RawRageCostMod, data, ref offset, BaseOffset, 72);
            AddEnumList(SelfPreEffectList, data, ref offset, BaseOffset, 76, StoredEnumListTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 80, StoredStringListTable);
            AddObjectList(AttributesThatDeltaDamageList, data, ref offset, BaseOffset, 84, StoredObjectListTable);
            AddObjectList(AttributesThatModDamageList, data, ref offset, BaseOffset, 88, StoredObjectListTable);
            AddObjectList(AttributesThatModBaseDamageList, data, ref offset, BaseOffset, 92, StoredObjectListTable);
            AddObjectList(AttributesThatDeltaTauntList, data, ref offset, BaseOffset, 96, StoredObjectListTable);
            AddObjectList(AttributesThatModTauntList, data, ref offset, BaseOffset, 100, StoredObjectListTable);
            AddObjectList(AttributesThatDeltaRageList, data, ref offset, BaseOffset, 104, StoredObjectListTable);
            AddObjectList(AttributesThatModRageList, data, ref offset, BaseOffset, 108, StoredObjectListTable);
            AddObjectList(AttributesThatDeltaRangeList, data, ref offset, BaseOffset, 112, StoredObjectListTable);
            //AddObjectList(AttributesThatDeltaDamageLastList, data, ref offset, BaseOffset, 116, StoredObjectListTable);
            offset += 4;
            AddObjectList(AttributesThatDeltaAccuracyList, data, ref offset, BaseOffset, 120, StoredObjectListTable);
            AddObjectList(AttributesThatModCritDamageList, data, ref offset, BaseOffset, 124, StoredObjectListTable);
            AddBool(RawAttributesThatDeltaDamageListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 128, 0);
            AddBool(RawAttributesThatModDamageListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 128, 2);
            AddBool(RawAttributesThatModBaseDamageListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 128, 4);
            AddBool(RawAttributesThatDeltaTauntListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 128, 6);
            AddBool(RawAttributesThatModTauntListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 128, 8);
            AddBool(RawAttributesThatDeltaRageListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 128, 10);
            AddBool(RawAttributesThatModRageListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 128, 12);
            AddBool(RawAttributesThatDeltaRangeListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 128, 14);
            //AddBool(RawAttributesThatDeltaDamageLastListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 130, 0);
            BitOffset += 2;
            AddBool(RawAttributesThatDeltaAccuracyListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 130, 2);
            AddBool(RawAttributesThatModCritDamageListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 130, 4);
            CloseBool(ref offset, ref BitOffset);
            AddDouble(RawCritDamageMod, data, ref offset, BaseOffset, 132);
            AddEnumList(SelfEffectOnCritList, data, ref offset, BaseOffset, 136, StoredEnumListTable);

            FinishSerializing(data, ref offset, BaseOffset, 140, StoredStringtable, null, null, StoredEnumListTable, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
