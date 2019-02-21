﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityPvX : GenericPgObject<PgAbilityPvX>, IPgAbilityPvX
    {
        public PgAbilityPvX(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityPvX CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityPvX CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityPvX(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public int Damage { get { return RawDamage.HasValue ? RawDamage.Value : 0; } }
        public int? RawDamage { get { return GetInt(4); } }
        public int ExtraDamageIfTargetVulnerable { get { return RawExtraDamageIfTargetVulnerable.HasValue ? RawExtraDamageIfTargetVulnerable.Value : 0; } }
        public int? RawExtraDamageIfTargetVulnerable { get { return GetInt(8); } }
        public int HealthSpecificDamage { get { return RawHealthSpecificDamage.HasValue ? RawHealthSpecificDamage.Value : 0; } }
        public int? RawHealthSpecificDamage { get { return GetInt(12); } }
        public int ArmorSpecificDamage { get { return RawArmorSpecificDamage.HasValue ? RawArmorSpecificDamage.Value : 0; } }
        public int? RawArmorSpecificDamage { get { return GetInt(16); } }
        public int Range { get { return RawRange.HasValue ? RawRange.Value : 0; } }
        public int? RawRange { get { return GetInt(20); } }
        public int PowerCost { get { return RawPowerCost.HasValue ? RawPowerCost.Value : 0; } }
        public int? RawPowerCost { get { return GetInt(24); } }
        public int MetabolismCost { get { return RawMetabolismCost.HasValue ? RawMetabolismCost.Value : 0; } }
        public int? RawMetabolismCost { get { return GetInt(28); } }
        public int ArmorMitigationRatio { get { return RawArmorMitigationRatio.HasValue ? RawArmorMitigationRatio.Value : 0; } }
        public int? RawArmorMitigationRatio { get { return GetInt(32); } }
        public int AoE { get { return RawAoE.HasValue ? RawAoE.Value : 0; } }
        public int? RawAoE { get { return GetInt(36); } }
        public int RageBoost { get { return RawRageBoost.HasValue ? RawRageBoost.Value : 0; } }
        public int? RawRageBoost { get { return GetInt(40); } }
        public double RageMultiplier { get { return RawRageMultiplier.HasValue ? RawRageMultiplier.Value : 1.0; } }
        public double? RawRageMultiplier { get { return GetDouble(44); } }
        public double Accuracy { get { return RawAccuracy.HasValue ? RawAccuracy.Value : 0; } }
        public double? RawAccuracy { get { return GetDouble(48); } }
        public IPgSpecialValueCollection SpecialValueList { get { return GetObjectList(52, ref _SpecialValueList, PgSpecialValueCollection.CreateItem, () => new PgSpecialValueCollection()); } } private IPgSpecialValueCollection _SpecialValueList;
        public IPgDoTCollection DoTList { get { return GetObjectList(56, ref _DoTList, PgDoTCollection.CreateItem, () => new PgDoTCollection()); } } private IPgDoTCollection _DoTList;
        public int TauntDelta { get { return RawTauntDelta.HasValue ? RawTauntDelta.Value : 0; } }
        public int? RawTauntDelta { get { return GetInt(60); } }
        public int TempTauntDelta { get { return RawTempTauntDelta.HasValue ? RawTempTauntDelta.Value : 0; } }
        public int? RawTempTauntDelta { get { return GetInt(64); } }
        public int RageCost { get { return RawRageCost.HasValue ? RawRageCost.Value : 0; } }
        public int? RawRageCost { get { return GetInt(68); } }
        public double RageCostMod { get { return RawRageCostMod.HasValue ? RawRageCostMod.Value : 0; } }
        public double? RawRageCostMod { get { return GetDouble(72); } }
        public List<PreEffect> SelfPreEffectList { get { return GetEnumList(76, ref _SelfPreEffectList); } } private List<PreEffect> _SelfPreEffectList;
        protected override List<string> FieldTableOrder { get { return GetStringList(80, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public IPgAttributeCollection AttributesThatDeltaDamageList { get { return GetObjectList(84, ref _AttributesThatDeltaDamageList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatDeltaDamageList;
        public IPgAttributeCollection AttributesThatModDamageList { get { return GetObjectList(88, ref _AttributesThatModDamageList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatModDamageList;
        public IPgAttributeCollection AttributesThatModBaseDamageList { get { return GetObjectList(92, ref _AttributesThatModBaseDamageList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatModBaseDamageList;
        public IPgAttributeCollection AttributesThatDeltaTauntList { get { return GetObjectList(96, ref _AttributesThatDeltaTauntList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatDeltaTauntList;
        public IPgAttributeCollection AttributesThatModTauntList { get { return GetObjectList(100, ref _AttributesThatModTauntList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatModTauntList;
        public IPgAttributeCollection AttributesThatDeltaRageList { get { return GetObjectList(104, ref _AttributesThatDeltaRageList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatDeltaRageList;
        public IPgAttributeCollection AttributesThatModRageList { get { return GetObjectList(108, ref _AttributesThatModRageList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatModRageList;
        public IPgAttributeCollection AttributesThatDeltaRangeList { get { return GetObjectList(112, ref _AttributesThatDeltaRangeList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatDeltaRangeList;
        public IPgAttributeCollection AttributesThatDeltaDamageLastList { get { return GetObjectList(116, ref _AttributesThatDeltaDamageLastList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatDeltaDamageLastList;
        public IPgAttributeCollection AttributesThatDeltaAccuracyList { get { return GetObjectList(112, ref _AttributesThatDeltaAccuracyList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatDeltaAccuracyList;
        public bool RawAttributesThatDeltaDamageListIsEmpty { get { return GetBool(120, 0).Value; } }
        public bool RawAttributesThatModDamageListIsEmpty { get { return GetBool(120, 2).Value; } }
        public bool RawAttributesThatModBaseDamageListIsEmpty { get { return GetBool(120, 4).Value; } }
        public bool RawAttributesThatDeltaTauntListIsEmpty { get { return GetBool(120, 6).Value; } }
        public bool RawAttributesThatModTauntListIsEmpty { get { return GetBool(120, 8).Value; } }
        public bool RawAttributesThatDeltaRageListIsEmpty { get { return GetBool(120, 10).Value; } }
        public bool RawAttributesThatModRageListIsEmpty { get { return GetBool(120, 12).Value; } }
        public bool RawAttributesThatDeltaRangeListIsEmpty { get { return GetBool(120, 14).Value; } }
        public bool RawAttributesThatDeltaDamageLastListIsEmpty { get { return GetBool(122, 0).Value; } }
        public bool RawAttributesThatDeltaAccuracyListIsEmpty { get { return GetBool(120, 14).Value; } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Damage", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawDamage } },
            { "HealthSpecificDamage", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawHealthSpecificDamage } },
            { "ExtraDamageIfTargetVulnerable", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawExtraDamageIfTargetVulnerable  } },
            { "ArmorSpecificDamage", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawArmorSpecificDamage } },
            { "Range", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawRange } },
            { "PowerCost", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawPowerCost } },
            { "MetabolismCost", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawMetabolismCost } },
            { "ArmorMitigationRatio", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawArmorMitigationRatio } },
            { "AoE", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawAoE } },
            { "SelfPreEffects", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<PreEffect>.ToStringList(SelfPreEffectList) } },
            { "RageBoost", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawRageBoost  } },
            { "RageMultiplier", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawRageMultiplier } },
            { "Accuracy", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawAccuracy } },
            { "AttributesThatDeltaDamage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatDeltaDamageList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaDamageListIsEmpty } },
            { "AttributesThatModDamage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatModDamageList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModDamageListIsEmpty } },
            { "AttributesThatModBaseDamage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatModBaseDamageList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModBaseDamageListIsEmpty } },
            { "AttributesThatDeltaTaunt", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatDeltaTauntList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaTauntListIsEmpty } },
            { "AttributesThatModTaunt", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatModTauntList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModTauntListIsEmpty } },
            { "AttributesThatDeltaRage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatDeltaRageList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaRageListIsEmpty } },
            { "AttributesThatModRage", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatModRageList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModRageListIsEmpty } },
            { "AttributesThatDeltaRange", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatDeltaRangeList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaRangeListIsEmpty } },
            { "AttributesThatDeltaDamageLast", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatDeltaDamageLastList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaDamageLastListIsEmpty } },
            { "AttributesThatDeltaAccuracy", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatDeltaAccuracyList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaAccuracyListIsEmpty } },
            { "SpecialValues", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => SpecialValueList } },
            { "TauntDelta", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawTauntDelta } },
            { "TempTauntDelta", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawTempTauntDelta } },
            { "RageCost", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawRageCost } },
            { "RageCostMod", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawRageCostMod } },
            { "DoTs", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => DoTList } },
        }; } }

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion
    }
}
