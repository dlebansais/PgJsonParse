using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgAbilityPvX
    {
        int Damage { get; }
        int? RawDamage { get; }
        int ExtraDamageIfTargetVulnerable { get; }
        int? RawExtraDamageIfTargetVulnerable { get; }
        int HealthSpecificDamage { get; }
        int? RawHealthSpecificDamage { get; }
        int ArmorSpecificDamage { get; }
        int? RawArmorSpecificDamage { get; }
        int Range { get; }
        int? RawRange { get; }
        int PowerCost { get; }
        int? RawPowerCost { get; }
        int MetabolismCost { get; }
        int? RawMetabolismCost { get; }
        int ArmorMitigationRatio { get; }
        int? RawArmorMitigationRatio { get; }
        int AoE { get; }
        int? RawAoE { get; }
        int RageBoost { get; }
        int? RawRageBoost { get; }
        double RageMultiplier { get; }
        double? RawRageMultiplier { get; }
        double Accuracy { get; }
        double? RawAccuracy { get; }
        IPgSpecialValueCollection SpecialValueList { get; }
        IPgDoTCollection DoTList { get; }
        int TauntDelta { get; }
        int? RawTauntDelta { get; }
        int TempTauntDelta { get; }
        int? RawTempTauntDelta { get; }
        int RageCost { get; }
        int? RawRageCost { get; }
        double RageCostMod { get; }
        double? RawRageCostMod { get; }
        double CritDamageMod { get; }
        double? RawCritDamageMod { get; }
        List<PreEffect> SelfPreEffectList { get; }
        IPgAttributeCollection AttributesThatDeltaDamageList { get; }
        IPgAttributeCollection AttributesThatModDamageList { get; }
        IPgAttributeCollection AttributesThatModBaseDamageList { get; }
        IPgAttributeCollection AttributesThatDeltaTauntList { get; }
        IPgAttributeCollection AttributesThatModTauntList { get; }
        IPgAttributeCollection AttributesThatDeltaRageList { get; }
        IPgAttributeCollection AttributesThatModRageList { get; }
        IPgAttributeCollection AttributesThatDeltaRangeList { get; }
        IPgAttributeCollection AttributesThatDeltaDamageLastList { get; }
        IPgAttributeCollection AttributesThatDeltaAccuracyList { get; }
        IPgAttributeCollection AttributesThatModCritDamageList { get; }
        bool RawAttributesThatDeltaDamageListIsEmpty { get; }
        bool RawAttributesThatModDamageListIsEmpty { get; }
        bool RawAttributesThatModBaseDamageListIsEmpty { get; }
        bool RawAttributesThatDeltaTauntListIsEmpty { get; }
        bool RawAttributesThatModTauntListIsEmpty { get; }
        bool RawAttributesThatDeltaRageListIsEmpty { get; }
        bool RawAttributesThatModRageListIsEmpty { get; }
        bool RawAttributesThatDeltaRangeListIsEmpty { get; }
        bool RawAttributesThatDeltaDamageLastListIsEmpty { get; }
        bool RawAttributesThatDeltaAccuracyListIsEmpty { get; }
        bool RawAttributesThatModCritDamageListIsEmpty { get; }
        List<SelfEffect> SelfEffectOnCritList { get; }
    }
}
