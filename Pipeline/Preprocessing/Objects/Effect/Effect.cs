namespace Preprocessor;

using System.Collections.Generic;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class Effect : IHasKey<int>
{
    const string MaxRagePattern = "Increases target's Max Rage by";
    const string LookAtMyHammerPattern = "Boosts Slashing, Piercing, and Crushing Mitigation";
    const string DeflectiveSpinPattern = "Universal Direct Elite Mitigation";
    const string TornadoAttackPattern = "and increase target's Electricity Vulnerability";

    public Effect(int key)
    {
        Key = key;
    }

    public Effect(int key, RawEffect rawEffect)
        : this(key)
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
        else if (rawDescription.StartsWith(MaxRagePattern) && rawDescription[MaxRagePattern.Length] != ' ')
            Result = rawDescription.Substring(0, MaxRagePattern.Length) + " " + rawDescription.Substring(MaxRagePattern.Length);
        else if (rawDescription.StartsWith(LookAtMyHammerPattern))
            Result = rawDescription.Replace("for 12 seconds", "for 15 seconds");
        else if (rawDescription.StartsWith(DeflectiveSpinPattern))
            Result = rawDescription.Replace("for 15 seconds", "for 10 seconds");
        else if (rawDescription.Contains(TornadoAttackPattern))
            Result = rawDescription.Replace("+10% for 10 seconds", "+15% for 10 seconds");
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
            default:
                return rawContent;
        }
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AbilityKeywords { get; set; }
    
    public string? Description { get; set; }
    
    public string? DisplayMode { get; set; }
    
    public int? Duration { get; set; }
    
    public int? IconId { get; set; }

    [Column(MapType = typeof(string))]
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
                if (description is not null && description.StartsWith(MaxRagePattern + " "))
                    Result = description.Substring(0, MaxRagePattern.Length) + description.Substring(MaxRagePattern.Length + 1);
                else if (description is not null && description.StartsWith(LookAtMyHammerPattern))
                    Result = description.Replace("for 15 seconds", "for 12 seconds");
                else if (description is not null && description.StartsWith(DeflectiveSpinPattern))
                    Result = description.Replace("for 10 seconds", "for 15 seconds");
                else if (description is not null && description.Contains(TornadoAttackPattern))
                    Result = description.Replace("+15% for 10 seconds", "+10% for 10 seconds");
                else
                    Result = description;
                break;
            case EffectDescriptionFix.TypoAnf:
                Result = description?.Replace(" and ", " anf  ");
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
            default:
                return rawContent;
        }
    }

    private readonly bool IsDurationNumber;
    private readonly EffectDescriptionFix EffectDescriptionFix;
}
