﻿namespace Preprocessor;

using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

public class PvEAbility
{
    public PvEAbility(RawPvEAbility rawPvEAbility)
    {
        Accuracy = rawPvEAbility.Accuracy;
        AoE = rawPvEAbility.AoE;
        ArmorMitigationRatio = rawPvEAbility.ArmorMitigationRatio;
        ArmorSpecificDamage = rawPvEAbility.ArmorSpecificDamage;
        AttributesThatDeltaAccuracy = rawPvEAbility.AttributesThatDeltaAccuracy;
        AttributesThatDeltaAoE = rawPvEAbility.AttributesThatDeltaAoE;
        AttributesThatDeltaDamage = rawPvEAbility.AttributesThatDeltaDamage;
        AttributesThatDeltaDamageIfTargetIsVulnerable = rawPvEAbility.AttributesThatDeltaDamageIfTargetIsVulnerable;
        AttributesThatDeltaRage = rawPvEAbility.AttributesThatDeltaRage;
        AttributesThatDeltaRange = rawPvEAbility.AttributesThatDeltaRange;
        AttributesThatDeltaTaunt = rawPvEAbility.AttributesThatDeltaTaunt;
        AttributesThatDeltaTempTaunt = rawPvEAbility.AttributesThatDeltaTempTaunt;
        AttributesThatModBaseDamage = rawPvEAbility.AttributesThatModBaseDamage;
        AttributesThatModCritDamage = rawPvEAbility.AttributesThatModCritDamage;
        AttributesThatModDamage = rawPvEAbility.AttributesThatModDamage;
        AttributesThatModRage = rawPvEAbility.AttributesThatModRage;
        AttributesThatModTaunt = rawPvEAbility.AttributesThatModTaunt;
        CritDamageMod = rawPvEAbility.CritDamageMod;
        Damage = rawPvEAbility.Damage;
        DoTs = rawPvEAbility.DoTs;
        ExtraDamageIfTargetVulnerable = rawPvEAbility.ExtraDamageIfTargetVulnerable;
        HealthSpecificDamage = rawPvEAbility.HealthSpecificDamage;
        PowerCost = rawPvEAbility.PowerCost;
        RageBoost = rawPvEAbility.RageBoost;
        RageCost = rawPvEAbility.RageCost;
        RageCostMod = rawPvEAbility.RageCostMod;
        RageMultiplier = rawPvEAbility.RageMultiplier;
        Range = rawPvEAbility.Range;
        SpecialValues = rawPvEAbility.SpecialValues;
        TauntDelta = rawPvEAbility.TauntDelta;
        TauntMod = rawPvEAbility.TauntMod;
        TempTauntDelta = rawPvEAbility.TempTauntDelta;
    }

    public decimal? Accuracy { get; set; }
    public int? AoE { get; set; }
    public int? ArmorMitigationRatio { get; set; }
    public int? ArmorSpecificDamage { get; set; }
    public string[]? AttributesThatDeltaAccuracy { get; set; }
    public string[]? AttributesThatDeltaAoE { get; set; }
    public string[]? AttributesThatDeltaDamage { get; set; }
    public string[]? AttributesThatDeltaDamageIfTargetIsVulnerable { get; set; }
    public string[]? AttributesThatDeltaRage { get; set; }
    public string[]? AttributesThatDeltaRange { get; set; }
    public string[]? AttributesThatDeltaTaunt { get; set; }
    public string[]? AttributesThatDeltaTempTaunt { get; set; }
    public string[]? AttributesThatModBaseDamage { get; set; }
    public string[]? AttributesThatModCritDamage { get; set; }
    public string[]? AttributesThatModDamage { get; set; }
    public string[]? AttributesThatModRage { get; set; }
    public string[]? AttributesThatModTaunt { get; set; }
    public decimal? CritDamageMod { get; set; }
    public int? Damage { get; set; }
    public DoT[]? DoTs { get; set; }
    public int? ExtraDamageIfTargetVulnerable { get; set; }
    public int? HealthSpecificDamage { get; set; }
    public int PowerCost { get; set; }
    public int? RageBoost { get; set; }
    public int? RageCost { get; set; }
    public decimal? RageCostMod { get; set; }
    public int? RageMultiplier { get; set; }
    public int Range { get; set; }
    public SpecialValue[]? SpecialValues { get; set; }
    public int? TauntDelta { get; set; }
    public decimal? TauntMod { get; set; }
    public int? TempTauntDelta { get; set; }

    public RawPvEAbility ToRawPvEAbility()
    {
        RawPvEAbility Result = new();

        Result.Accuracy = Accuracy;
        Result.AoE = AoE;
        Result.ArmorMitigationRatio = ArmorMitigationRatio;
        Result.ArmorSpecificDamage = ArmorSpecificDamage;
        Result.AttributesThatDeltaAccuracy = AttributesThatDeltaAccuracy;
        Result.AttributesThatDeltaAoE = AttributesThatDeltaAoE;
        Result.AttributesThatDeltaDamage = AttributesThatDeltaDamage;
        Result.AttributesThatDeltaDamageIfTargetIsVulnerable = AttributesThatDeltaDamageIfTargetIsVulnerable;
        Result.AttributesThatDeltaRage = AttributesThatDeltaRage;
        Result.AttributesThatDeltaRange = AttributesThatDeltaRange;
        Result.AttributesThatDeltaTaunt = AttributesThatDeltaTaunt;
        Result.AttributesThatDeltaTempTaunt = AttributesThatDeltaTempTaunt;
        Result.AttributesThatModBaseDamage = AttributesThatModBaseDamage;
        Result.AttributesThatModCritDamage = AttributesThatModCritDamage;
        Result.AttributesThatModDamage = AttributesThatModDamage;
        Result.AttributesThatModRage = AttributesThatModRage;
        Result.AttributesThatModTaunt = AttributesThatModTaunt;
        Result.CritDamageMod = CritDamageMod;
        Result.Damage = Damage;
        Result.DoTs = DoTs;
        Result.ExtraDamageIfTargetVulnerable = ExtraDamageIfTargetVulnerable;
        Result.HealthSpecificDamage = HealthSpecificDamage;
        Result.PowerCost = PowerCost;
        Result.RageBoost = RageBoost;
        Result.RageCost = RageCost;
        Result.RageCostMod = RageCostMod;
        Result.RageMultiplier = RageMultiplier;
        Result.Range = Range;
        Result.SpecialValues = SpecialValues;
        Result.TauntDelta = TauntDelta;
        Result.TauntMod = TauntMod;
        Result.TempTauntDelta = TempTauntDelta;

        return Result;
    }
}
