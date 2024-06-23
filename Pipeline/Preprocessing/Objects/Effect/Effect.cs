using System.Collections.Generic;

namespace Preprocessor;

public class Effect
{
    public Effect(RawEffect rawEffect)
    {
        AbilityKeywords = rawEffect.AbilityKeywords;
        Description = ParseDescription(rawEffect.Desc, rawEffect.AbilityKeywords, rawEffect.Name, out EffectDescriptionFix);
        DisplayMode = rawEffect.DisplayMode;
        Duration = Preprocessor.ToNumberOrString(rawEffect.Duration, out IsDurationNumber);
        IconId = rawEffect.IconId;
        Keywords = ParseKeywords(rawEffect.Keywords);
        Name = rawEffect.Name;
        Particle = EffectParticle.Parse(rawEffect.Particle);
        SpewText = rawEffect.SpewText;
        StackingPriority = rawEffect.StackingPriority;
        StackingType = ParseStackingType(rawEffect.StackingType);
    }

    private static string[]? ParseKeywords(string[]? rawKeywords)
    {
        if (rawKeywords is null)
            return null;

        List<string> Result = new();
        foreach (var Keyword in rawKeywords)
            Result.Add(ParseKeyword(Keyword));

        return Result.ToArray();
    }

    private static string ParseKeyword(string rawKeyword)
    {
        if (rawKeyword == "-")
            return "Hyphen";
        else
            return rawKeyword;
    }

    private static string? ParseDescription(string? rawDescription, string[]? rawAbilityKeywords, string? rawName, out EffectDescriptionFix effectDescriptionFix)
    {
        effectDescriptionFix = EffectDescriptionFix.None;

        if (rawDescription is null)
            return null;

        string Result;

        if (rawDescription.Contains(" anf  "))
        {
            Result = rawDescription.Replace(" anf  ", " and ");
            effectDescriptionFix = EffectDescriptionFix.TypoAnf;
        }
        else if (rawDescription == "Restores 4 Armor" && rawAbilityKeywords is not null && rawAbilityKeywords.Length > 0 && rawAbilityKeywords[0] == "DoeEyes")
        {
            Result = "Restores 4 Power";
            effectDescriptionFix = EffectDescriptionFix.DoeEyes;
        }
        else if (rawDescription.StartsWith("Self Destruct") && rawName is not null && rawName.StartsWith("TSys_BatChemGolemRageAcidTossBoost"))
        {
            Result = rawDescription.Replace("Self Destruct", "Rage Acid Toss");
            effectDescriptionFix = EffectDescriptionFix.GolemAcidToss;
        }
        else if (rawDescription.StartsWith("`"))
        {
            Result = rawDescription.Substring(1);
            effectDescriptionFix = EffectDescriptionFix.TypoQuote;
        }
        else
            Result = rawDescription;

        return Result;
    }

    private static string? ParseStackingType(string? rawContent)
    {
        switch (rawContent)
        {
            case null:
                return null;
            case "Lamia's Gaze":
                return "LamiasGaze";
            case "1":
                return "One";
            default:
                return rawContent;
        }
    }

    public string[]? AbilityKeywords { get; set; }
    public string? Description { get; set; }
    public string? DisplayMode { get; set; }
    public int? Duration { get; set; }
    public int? IconId { get; set; }
    public string[]? Keywords { get; set; }
    public string? Name { get; set; }
    public EffectParticle? Particle { get; set; }
    public string? SpewText { get; set; }
    public int? StackingPriority { get; set; }
    public string? StackingType { get; set; }

    public RawEffect ToRawEffect()
    {
        RawEffect Result = new();

        Result.AbilityKeywords = AbilityKeywords;
        Result.Desc = ToRawDescription(Description, EffectDescriptionFix);
        Result.DisplayMode = DisplayMode;
        Result.Duration = Preprocessor.FromNumberOrString(Duration, IsDurationNumber);
        Result.IconId = IconId;
        Result.Keywords = ToRawKeywords(Keywords);
        Result.Name = Name;
        Result.Particle = EffectParticle.ToString(Particle);
        Result.SpewText = SpewText;
        Result.StackingPriority = StackingPriority;
        Result.StackingType = ToRawStackingType(StackingType);

        return Result;
    }

    private static string[]? ToRawKeywords(string[]? keywords)
    {
        if (keywords is null)
            return null;

        List<string> Result = new();
        foreach (var Keyword in keywords)
            Result.Add(ToRawKeyword(Keyword));

        return Result.ToArray();
    }

    private static string ToRawKeyword(string keyword)
    {
        if (keyword == "Hyphen")
            return "-";
        else
            return keyword;
    }

    private static string? ToRawDescription(string? description, EffectDescriptionFix effectDescriptionFix)
    {
        string? Result;

        switch (effectDescriptionFix)
        {
            default:
                Result = description;
                break;
            case EffectDescriptionFix.TypoAnf:
                Result = description?.Replace(" and ", " anf  ");
                break;
            case EffectDescriptionFix.DoeEyes:
                Result = "Restores 4 Armor";
                break;
            case EffectDescriptionFix.GolemAcidToss:
                Result = description?.Replace("Rage Acid Toss", "Self Destruct");
                break;
            case EffectDescriptionFix.TypoQuote:
                Result = $"`{description}";
                break;
        }

        return Result;
    }

    private string? ToRawStackingType(string? rawContent)
    {
        switch (rawContent)
        {
            case null:
                return null;
            case "LamiasGaze":
                return "Lamia's Gaze";
            case "One":
                return "1";
            default:
                return rawContent;
        }
    }

    private readonly bool IsDurationNumber;
    private readonly EffectDescriptionFix EffectDescriptionFix;
}
