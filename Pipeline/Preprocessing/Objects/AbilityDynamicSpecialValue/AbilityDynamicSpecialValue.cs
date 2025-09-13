namespace Preprocessor;

public class AbilityDynamicSpecialValue
{
    public AbilityDynamicSpecialValue(RawAbilityDynamicSpecialValue rawAAbilityDynamicSpecialValue)
    {
        AttributesThatDelta = rawAAbilityDynamicSpecialValue.AttributesThatDelta;
        Label = rawAAbilityDynamicSpecialValue.Label;
        RequiredAbilityKeywords = rawAAbilityDynamicSpecialValue.ReqAbilityKeywords;
        RequiredEffectKeywords = rawAAbilityDynamicSpecialValue.ReqEffectKeywords;
        SkipIfZero = rawAAbilityDynamicSpecialValue.SkipIfZero;
        Suffix = rawAAbilityDynamicSpecialValue.Suffix;
        Value = rawAAbilityDynamicSpecialValue.Value;
    }

    public string[]? AttributesThatDelta { get; set; }
    public string? Label { get; set; }
    public string[]? RequiredAbilityKeywords { get; set; }
    public string[]? RequiredEffectKeywords { get; set; }
    public bool? SkipIfZero { get; set; }
    public string? Suffix { get; set; }
    public int? Value { get; set; }

    public RawAbilityDynamicSpecialValue ToRawAbilityDynamicSpecialValue()
    {
        RawAbilityDynamicSpecialValue Result = new();

        Result.AttributesThatDelta = AttributesThatDelta;
        Result.Label = Label;
        Result.ReqAbilityKeywords = RequiredAbilityKeywords;
        Result.ReqEffectKeywords = RequiredEffectKeywords;
        Result.SkipIfZero = SkipIfZero;
        Result.Suffix = Suffix;
        Result.Value = Value;

        return Result;
    }
}
