namespace Preprocessor;

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
        SelfEffectsOnCrit = rawPvEAbility.SelfEffectsOnCrit;
        SelfPreEffects = ParseSelfPreEffects(rawPvEAbility.SelfPreEffects);
        SpecialValues = rawPvEAbility.SpecialValues;
        TauntDelta = rawPvEAbility.TauntDelta;
        TauntMod = rawPvEAbility.TauntMod;
        TempTauntDelta = rawPvEAbility.TempTauntDelta;
    }

    private static SelfPreEffect[]? ParseSelfPreEffects(string[]? rawSelfPreEffects)
    {
        if (rawSelfPreEffects is null)
            return null;

        List<SelfPreEffect> Result = new();
        foreach (string SelfPreEffects in rawSelfPreEffects)
            Result.Add(ParseSelfPreEffect(SelfPreEffects));

        return Result.ToArray();
    }

    private static SelfPreEffect ParseSelfPreEffect(string rawSelfPreEffect)
    {
        // Search for an expression between parentheses.
        string ParameterPattern = @"\(([^)]+)\)";
        Match ParameterMatch = Regex.Match(rawSelfPreEffect, ParameterPattern, RegexOptions.IgnoreCase);
        if (!ParameterMatch.Success)
            return new SelfPreEffect { Name = rawSelfPreEffect };

        string Name = rawSelfPreEffect.Substring(0, ParameterMatch.Index);
        string InsideParameterString = rawSelfPreEffect.Substring(ParameterMatch.Index + 1, rawSelfPreEffect.Length - ParameterMatch.Index - 2);

        SelfPreEffect Result = new() { Name = Name };

        switch (Name)
        {
            case "EnhanceZombie":
                Result.Enhancement = InsideParameterString;
                break;
            case "ConfigGalvanize":
                if (InsideParameterString[0] != ',')
                    throw new PreprocessorException();

                Result.Value = int.Parse(InsideParameterString.Substring(1));
                break;
            default:
                throw new PreprocessorException();
        }

        return Result;
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
    public string[]? SelfEffectsOnCrit { get; set; }
    public SelfPreEffect[]? SelfPreEffects { get; set; }
    public SpecialValue[]? SpecialValues { get; set; }
    public int? TauntDelta { get; set; }
    public int? TauntMod { get; set; }
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
        Result.SelfEffectsOnCrit = SelfEffectsOnCrit;
        Result.SelfPreEffects = SelfPreEffectsToStrings(SelfPreEffects);
        Result.SpecialValues = SpecialValues;
        Result.TauntDelta = TauntDelta;
        Result.TauntMod = TauntMod;
        Result.TempTauntDelta = TempTauntDelta;

        return Result;
    }

    private static string[]? SelfPreEffectsToStrings(SelfPreEffect[]? selfPreEffects)
    {
        if (selfPreEffects is null)
            return null;

        List<string> Result = new();
        foreach (SelfPreEffect SelfPreEffect in selfPreEffects)
            Result.Add(SelfPreEffectToString(SelfPreEffect));

        return Result.ToArray();
    }

    private static string SelfPreEffectToString(SelfPreEffect selfPreEffect)
    {
        string Result;

        switch (selfPreEffect.Name)
        {
            case "EnhanceZombie":
                Result = $"{selfPreEffect.Name}({selfPreEffect.Enhancement})";
                break;
            case "ConfigGalvanize":
                Result = $"{selfPreEffect.Name}(,{selfPreEffect.Value})";
                break;
            default:
                Result = selfPreEffect.Name;
                break;
        }

        Debug.Assert(Result != string.Empty);

        return Result;
    }
}
