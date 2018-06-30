using System.Collections.Generic;

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
        public SpecialValueCollection SpecialValueList { get { return GetObjectList(52, ref _SpecialValueList, SpecialValueCollection.CreateItem, () => new SpecialValueCollection()); } } private SpecialValueCollection _SpecialValueList;
        public DoTCollection DoTList { get { return GetObjectList(56, ref _DoTList, DoTCollection.CreateItem, () => new DoTCollection()); } } private DoTCollection _DoTList;
        public int TauntDelta { get { return RawTauntDelta.HasValue ? RawTauntDelta.Value : 0; } }
        public int? RawTauntDelta { get { return GetInt(60); } }
        public int TempTauntDelta { get { return RawTempTauntDelta.HasValue ? RawTempTauntDelta.Value : 0; } }
        public int? RawTempTauntDelta { get { return GetInt(64); } }
        public int RageCost { get { return RawRageCost.HasValue ? RawRageCost.Value : 0; } }
        public int? RawRageCost { get { return GetInt(68); } }
        public double RageCostMod { get { return RawRageCostMod.HasValue ? RawRageCostMod.Value : 0; } }
        public double? RawRageCostMod { get { return GetDouble(72); } }
        public List<PreEffect> SelfPreEffectList { get { return GetEnumList(76, ref _SelfPreEffectList); } } private List<PreEffect> _SelfPreEffectList;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
