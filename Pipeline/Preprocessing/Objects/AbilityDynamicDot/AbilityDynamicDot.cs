namespace Preprocessor;

public class AbilityDynamicDot
{
    public AbilityDynamicDot(RawAbilityDynamicDot rawAbilityDynamicDot)
    {
        AttributesThatDelta = rawAbilityDynamicDot.AttributesThatDelta;
        DamagePerTick = rawAbilityDynamicDot.DamagePerTick;
        DamageType = rawAbilityDynamicDot.DamageType;
        Duration = rawAbilityDynamicDot.Duration;
        NumTicks = rawAbilityDynamicDot.NumTicks;
        RequiredAbilityKeywords = rawAbilityDynamicDot.ReqAbilityKeywords;
        RequiredActiveSkill = rawAbilityDynamicDot.ReqActiveSkill;
        RequiredEffectKeywords = rawAbilityDynamicDot.ReqEffectKeywords;
        SpecialRules = rawAbilityDynamicDot.SpecialRules;
    }

    public string[]? AttributesThatDelta { get; set; }
    public int? DamagePerTick { get; set; }
    public string? DamageType { get; set; }
    public int? Duration { get; set; }
    public int? NumTicks { get; set; }
    public string[]? RequiredAbilityKeywords { get; set; }
    public string? RequiredActiveSkill { get; set; }
    public string[]? RequiredEffectKeywords { get; set; }
    public string[]? SpecialRules { get; set; }

    public RawAbilityDynamicDot ToRawAbilityDynamicDot()
    {
        RawAbilityDynamicDot Result = new();

        Result.AttributesThatDelta = AttributesThatDelta;
        Result.DamagePerTick = DamagePerTick;
        Result.DamageType = DamageType;
        Result.Duration = Duration;
        Result.NumTicks = NumTicks;
        Result.ReqAbilityKeywords = RequiredAbilityKeywords;
        Result.ReqActiveSkill = RequiredActiveSkill;
        Result.ReqEffectKeywords = RequiredEffectKeywords;
        Result.SpecialRules = SpecialRules;

        return Result;
    }
}
