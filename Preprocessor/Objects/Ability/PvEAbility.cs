namespace Preprocessor;

internal class PvEAbility
{
    public PvEAbility(RawPvEAbility fromRawPvEAbility)
    {
        Accuracy = fromRawPvEAbility.Accuracy;
        AoE = fromRawPvEAbility.AoE;
        ArmorMitigationRatio = fromRawPvEAbility.ArmorMitigationRatio;
        ArmorSpecificDamage = fromRawPvEAbility.ArmorSpecificDamage;
        AttributesThatDeltaAccuracy = fromRawPvEAbility.AttributesThatDeltaAccuracy;
        AttributesThatDeltaDamage = fromRawPvEAbility.AttributesThatDeltaDamage;
        AttributesThatDeltaRage = fromRawPvEAbility.AttributesThatDeltaRage;
        AttributesThatDeltaRange = fromRawPvEAbility.AttributesThatDeltaRange;
        AttributesThatDeltaTaunt = fromRawPvEAbility.AttributesThatDeltaTaunt;
        AttributesThatDeltaTempTaunt = fromRawPvEAbility.AttributesThatDeltaTempTaunt;
        AttributesThatModBaseDamage = fromRawPvEAbility.AttributesThatModBaseDamage;
        AttributesThatModCritDamage = fromRawPvEAbility.AttributesThatModCritDamage;
        AttributesThatModDamage = fromRawPvEAbility.AttributesThatModDamage;
        AttributesThatModRage = fromRawPvEAbility.AttributesThatModRage;
        AttributesThatModTaunt = fromRawPvEAbility.AttributesThatModTaunt;
        CritDamageMod = fromRawPvEAbility.CritDamageMod;
        Damage = fromRawPvEAbility.Damage;
        DoTs = fromRawPvEAbility.DoTs;
        ExtraDamageIfTargetVulnerable = fromRawPvEAbility.ExtraDamageIfTargetVulnerable;
        HealthSpecificDamage = fromRawPvEAbility.HealthSpecificDamage;
        PowerCost = fromRawPvEAbility.PowerCost;
        RageBoost = fromRawPvEAbility.RageBoost;
        RageCost = fromRawPvEAbility.RageCost;
        RageCostMod = fromRawPvEAbility.RageCostMod;
        RageMultiplier = fromRawPvEAbility.RageMultiplier;
        Range = fromRawPvEAbility.Range;

        if (fromRawPvEAbility.SelfEffectOnCrit is not null)
        {
            SelfEffectsOnCrit = fromRawPvEAbility.SelfEffectOnCrit;
            HasSelfEffectOnCrit = true;
        }
        else
        {
            SelfEffectsOnCrit = fromRawPvEAbility.SelfEffectsOnCrit;
            HasSelfEffectOnCrit = false;
        }

        SelfPreEffects = fromRawPvEAbility.SelfPreEffects;
        SpecialValues = fromRawPvEAbility.SpecialValues;
        TauntDelta = fromRawPvEAbility.TauntDelta;
        TempTauntDelta = fromRawPvEAbility.TempTauntDelta;
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
    public DoT[]? DoTs { get; set; }
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
    public SpecialValue[]? SpecialValues { get; set; }
    public int? TauntDelta { get; set; }
    public int? TempTauntDelta { get; set; }

    public RawPvEAbility ToRawPvEAbility()
    {
        RawPvEAbility Result = new();

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
