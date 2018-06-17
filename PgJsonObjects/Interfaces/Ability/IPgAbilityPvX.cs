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
        SpecialValueCollection SpecialValueList { get; }
        DoTCollection DoTList { get; }
        int TauntDelta { get; }
        int? RawTauntDelta { get; }
        int TempTauntDelta { get; }
        int? RawTempTauntDelta { get; }
        int RageCost { get; }
        int? RawRageCost { get; }
        double RageCostMod { get; }
        double? RawRageCostMod { get; }
        List<PreEffect> SelfPreEffectList { get; }
    }
}
