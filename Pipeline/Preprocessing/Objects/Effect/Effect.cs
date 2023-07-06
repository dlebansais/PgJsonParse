﻿namespace Preprocessor;

public class Effect
{
    public Effect(RawEffect rawEffect)
    {
        AbilityKeywords = rawEffect.AbilityKeywords;
        Description = ParseDescription(rawEffect.Desc, out IsTypoFixed);
        DisplayMode = rawEffect.DisplayMode;
        Duration = Preprocessor.ToNumberOrString(rawEffect.Duration, out IsDurationNumber);
        IconId = rawEffect.IconId;
        Keywords = rawEffect.Keywords;
        Name = rawEffect.Name;
        Particle = EffectParticle.Parse(rawEffect.Particle);
        SpewText = rawEffect.SpewText;
        StackingPriority = rawEffect.StackingPriority;
        StackingType = ParseStackingType(rawEffect.StackingType);
    }

    private static string? ParseDescription(string? rawDescription, out bool isTypoFixed)
    {
        isTypoFixed = false;

        if (rawDescription is null)
            return null;

        string Result = rawDescription;

        if (rawDescription.Contains(" anf  "))
        {
            Result = Result.Replace(" anf  ", " and ");
            isTypoFixed = true;
        }

        return Result;
    }

    private static string? ParseStackingType(string? rawStackingType)
    {
        switch (rawStackingType)
        {
            case null:
                return null;
            case "Lamia's Gaze":
                return "LamiasGaze";
            case "1":
                return "One";
            default:
                return rawStackingType;
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
        Result.Desc = ToRawDescription(Description, IsTypoFixed);
        Result.DisplayMode = DisplayMode;
        Result.Duration = Preprocessor.FromNumberOrString(Duration, IsDurationNumber);
        Result.IconId = IconId;
        Result.Keywords = Keywords;
        Result.Name = Name;
        Result.Particle = EffectParticle.ToString(Particle);
        Result.SpewText = SpewText;
        Result.StackingPriority = StackingPriority;
        Result.StackingType = ToRawStackingType(StackingType);

        return Result;
    }

    private static string? ToRawDescription(string? description, bool isTypoFixed)
    {
        if (description is null)
            return null;

        string Result = description;

        if (isTypoFixed)
            Result = Result.Replace(" and ", " anf  ");

        return Result;
    }

    private string? ToRawStackingType(string? stackingType)
    {
        switch (stackingType)
        {
            case null:
                return null;
            case "LamiasGaze":
                return "Lamia's Gaze";
            case "One":
                return "1";
            default:
                return stackingType;
        }
    }

    private readonly bool IsDurationNumber;
    private readonly bool IsTypoFixed;
}
