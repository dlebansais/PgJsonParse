namespace Preprocessor;

using System;
using System.Text.RegularExpressions;

public class PvEAbility
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

        SelfPreEffects = ParseSelfPreEffects(fromRawPvEAbility.SelfPreEffects);
        SpecialValues = fromRawPvEAbility.SpecialValues;
        TauntDelta = fromRawPvEAbility.TauntDelta;
        TempTauntDelta = fromRawPvEAbility.TempTauntDelta;
    }

    private static SelfPreEffect[]? ParseSelfPreEffects(string[]? content)
    {
        if (content is null)
            return null;

        SelfPreEffect[] Result = new SelfPreEffect[content.Length];
        for (int i = 0; i < Result.Length; i++)
            Result[i] = ParseSelfPreEffect(content[i]);

        return Result;
    }

    private static SelfPreEffect ParseSelfPreEffect(string content)
    {
        // Search for an expression between parentheses.
        string ParameterPattern = @"\(([^)]+)\)";
        Match ParameterMatch = Regex.Match(content, ParameterPattern, RegexOptions.IgnoreCase);
        if (!ParameterMatch.Success)
            return new SelfPreEffect { Name = content };

        string Name = content.Substring(0, ParameterMatch.Index);
        string InsideParameterString = content.Substring(ParameterMatch.Index + 1, content.Length - ParameterMatch.Index - 2);

        SelfPreEffect Result = new() { Name = Name };

        switch (Name)
        {
            case "EnhanceZombie":
                Result.Enhancement = InsideParameterString;
                break;
            case "ConfigGalvanize":
                if (InsideParameterString[0] != ',')
                    PreprocessorException.Throw();

                Result.Value = int.Parse(InsideParameterString.Substring(1));
                break;
            default:
                PreprocessorException.Throw();
                break;
        }

        return Result;
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
    public SelfPreEffect[]? SelfPreEffects { get; set; }
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

        Result.SelfPreEffects = SelfPreEffectsToStrings(SelfPreEffects);
        Result.SpecialValues = SpecialValues;
        Result.TauntDelta = TauntDelta;
        Result.TempTauntDelta = TempTauntDelta;

        return Result;
    }

    private static string[]? SelfPreEffectsToStrings(SelfPreEffect[]? selfPreEffectArray)
    {
        if (selfPreEffectArray is null)
            return null;

        string[] Result = new string[selfPreEffectArray.Length];
        for (int i = 0; i < Result.Length; i++)
            Result[i] = SelfPreEffectToString(selfPreEffectArray[i]);

        return Result;
    }

    private static string SelfPreEffectToString(SelfPreEffect selfPreEffect)
    {
        switch (selfPreEffect.Name)
        {
            case "EnhanceZombie":
                return $"{selfPreEffect.Name}({selfPreEffect.Enhancement})";
            case "ConfigGalvanize":
                return $"{selfPreEffect.Name}(,{selfPreEffect.Value})";
            default:
                return selfPreEffect.Name ?? throw new NullReferenceException();
        }
    }

    private readonly bool HasSelfEffectOnCrit;
}
