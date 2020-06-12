namespace PgObjects
{
    using System.Collections.Generic;

    public class PgAbilityPvX
    {
        public int Damage { get { return RawDamage.HasValue ? RawDamage.Value : 0; } }
        public int? RawDamage { get; set; }
        public int HealthSpecificDamage { get { return RawHealthSpecificDamage.HasValue ? RawHealthSpecificDamage.Value : 0; } }
        public int? RawHealthSpecificDamage { get; set; }
        public int ExtraDamageIfTargetVulnerable { get { return RawExtraDamageIfTargetVulnerable.HasValue ? RawExtraDamageIfTargetVulnerable.Value : 0; } }
        public int? RawExtraDamageIfTargetVulnerable { get; set; }
        public int ArmorSpecificDamage { get { return RawArmorSpecificDamage.HasValue ? RawArmorSpecificDamage.Value : 0; } }
        public int? RawArmorSpecificDamage { get; set; }
        public int Range { get { return RawRange.HasValue ? RawRange.Value : 0; } }
        public int? RawRange { get; set; }
        public int PowerCost { get { return RawPowerCost.HasValue ? RawPowerCost.Value : 0; } }
        public int? RawPowerCost { get; set; }
        public int MetabolismCost { get { return RawMetabolismCost.HasValue ? RawMetabolismCost.Value : 0; } }
        public int? RawMetabolismCost { get; set; }
        public int ArmorMitigationRatio { get { return RawArmorMitigationRatio.HasValue ? RawArmorMitigationRatio.Value : 0; } }
        public int? RawArmorMitigationRatio { get; set; }
        public int AoE { get { return RawAoE.HasValue ? RawAoE.Value : 0; } }
        public int? RawAoE { get; set; }
        public List<PreEffect> SelfPreEffectList { get; } = new List<PreEffect>();
        public int RageBoost { get { return RawRageBoost.HasValue ? RawRageBoost.Value : 0; } }
        public int? RawRageBoost { get; set; }
        public float RageMultiplier { get { return RawRageMultiplier.HasValue ? RawRageMultiplier.Value : 1.0F; } }
        public float? RawRageMultiplier { get; set; }
        public float Accuracy { get { return RawAccuracy.HasValue ? RawAccuracy.Value : 0; } }
        public float? RawAccuracy { get; set; }
        public PgAttributeCollection AttributesThatDeltaDamageList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModDamageList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModBaseDamageList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaTauntList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModTauntList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaRageList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModRageList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaRangeList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatDeltaAccuracyList { get; } = new PgAttributeCollection();
        public PgAttributeCollection AttributesThatModCritDamageList { get; } = new PgAttributeCollection();
        public PgSpecialValueCollection SpecialValueList { get; } = new PgSpecialValueCollection();
        public int TauntDelta { get { return RawTauntDelta.HasValue ? RawTauntDelta.Value : 0; } }
        public int? RawTauntDelta { get; set; }
        public int TempTauntDelta { get { return RawTempTauntDelta.HasValue ? RawTempTauntDelta.Value : 0; } }
        public int? RawTempTauntDelta { get; set; }
        public int RageCost { get { return RawRageCost.HasValue ? RawRageCost.Value : 0; } }
        public int? RawRageCost { get; set; }
        public float RageCostMod { get { return RawRageCostMod.HasValue ? RawRageCostMod.Value : 0; } }
        public float? RawRageCostMod { get; set; }
        public PgDoTCollection DoTList { get; } = new PgDoTCollection();
        public float CritDamageMod { get { return RawCritDamageMod.HasValue ? RawCritDamageMod.Value : 0; } }
        public float? RawCritDamageMod { get; set; }
        public List<SelfEffect> SelfEffectOnCritList { get; } = new List<SelfEffect>();
    }
}
