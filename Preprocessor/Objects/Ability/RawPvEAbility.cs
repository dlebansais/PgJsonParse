namespace Preprocessor;

internal class RawPvEAbility
{
    public RawPvEAbility(RawPvEAbility1 fromRawPvEAbility1)
    {
        Accuracy = fromRawPvEAbility1.Accuracy;
        AoE = fromRawPvEAbility1.AoE;
        ArmorMitigationRatio = fromRawPvEAbility1.ArmorMitigationRatio;
        ArmorSpecificDamage = fromRawPvEAbility1.ArmorSpecificDamage;
        AttributesThatDeltaAccuracy = fromRawPvEAbility1.AttributesThatDeltaAccuracy;
        AttributesThatDeltaDamage = fromRawPvEAbility1.AttributesThatDeltaDamage;
        AttributesThatDeltaRage = fromRawPvEAbility1.AttributesThatDeltaRage;
        AttributesThatDeltaRange = fromRawPvEAbility1.AttributesThatDeltaRange;
        AttributesThatDeltaTaunt = fromRawPvEAbility1.AttributesThatDeltaTaunt;
        AttributesThatDeltaTempTaunt = fromRawPvEAbility1.AttributesThatDeltaTempTaunt;
        AttributesThatModBaseDamage = fromRawPvEAbility1.AttributesThatModBaseDamage;
        AttributesThatModCritDamage = fromRawPvEAbility1.AttributesThatModCritDamage;
        AttributesThatModDamage = fromRawPvEAbility1.AttributesThatModDamage;
        AttributesThatModRage = fromRawPvEAbility1.AttributesThatModRage;
        AttributesThatModTaunt = fromRawPvEAbility1.AttributesThatModTaunt;
        CritDamageMod = fromRawPvEAbility1.CritDamageMod;
        Damage = fromRawPvEAbility1.Damage;
        DoTs = fromRawPvEAbility1.DoTs;
        ExtraDamageIfTargetVulnerable = fromRawPvEAbility1.ExtraDamageIfTargetVulnerable;
        HealthSpecificDamage = fromRawPvEAbility1.HealthSpecificDamage;
        PowerCost = fromRawPvEAbility1.PowerCost;
        RageBoost = fromRawPvEAbility1.RageBoost;
        RageCost = fromRawPvEAbility1.RageCost;
        RageCostMod = fromRawPvEAbility1.RageCostMod;
        RageMultiplier = fromRawPvEAbility1.RageMultiplier;
        Range = fromRawPvEAbility1.Range;

        if (fromRawPvEAbility1.SelfEffectOnCrit is not null)
        {
            SelfEffectsOnCrit = fromRawPvEAbility1.SelfEffectOnCrit;
            HasSelfEffectOnCrit = true;
        }
        else
        {
            SelfEffectsOnCrit = fromRawPvEAbility1.SelfEffectsOnCrit;
            HasSelfEffectOnCrit = false;
        }

        SelfPreEffects = fromRawPvEAbility1.SelfPreEffects;
        SpecialValues = fromRawPvEAbility1.SpecialValues;
        TauntDelta = fromRawPvEAbility1.TauntDelta;
        TempTauntDelta = fromRawPvEAbility1.TempTauntDelta;
    }

    public decimal? Accuracy { get; set; }
    public int? AoE { get; set; }
    public int? ArmorMitigationRatio { get; set; }
    public int? ArmorSpecificDamage { get; set; }
    public string[]? AttributesThatDeltaAccuracy { get; set; }
    public string[]? AttributesThatDeltaDamage { get; set; }
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
    public RawDoT[]? DoTs { get; set; }
    public int? ExtraDamageIfTargetVulnerable { get; set; }
    public int? HealthSpecificDamage { get; set; }
    public int PowerCost { get; set; }
    public int? RageBoost { get; set; }
    public int? RageCost { get; set; }
    public decimal? RageCostMod { get; set; }
    public int? RageMultiplier { get; set; }
    public int Range { get; set; }
    public string[]? SelfEffectsOnCrit { get; set; }
    public string[]? SelfPreEffects { get; set; }
    public RawSpecialValue[]? SpecialValues { get; set; }
    public int? TauntDelta { get; set; }
    public int? TempTauntDelta { get; set; }

    public RawPvEAbility1 ToRawPvEAbility1()
    {
        RawPvEAbility1 Result = new();

        Result.Accuracy = Accuracy;
        Result.AoE = AoE;
        Result.ArmorMitigationRatio = ArmorMitigationRatio;
        Result.ArmorSpecificDamage = ArmorSpecificDamage;
        Result.AttributesThatDeltaAccuracy = AttributesThatDeltaAccuracy;
        Result.AttributesThatDeltaDamage = AttributesThatDeltaDamage;
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

        if (HasSelfEffectOnCrit)
            Result.SelfEffectOnCrit = SelfEffectsOnCrit;
        else
            Result.SelfEffectsOnCrit = SelfEffectsOnCrit;

        Result.SelfPreEffects = SelfPreEffects;
        Result.SpecialValues = SpecialValues;
        Result.TauntDelta = TauntDelta;
        Result.TempTauntDelta = TempTauntDelta;

        return Result;
    }

    public bool HasSelfEffectOnCrit { get; }
}
