namespace Preprocessor;

internal class Effect
{
    public Effect(RawEffect rawEffect)
    {
        AbilityKeywords = rawEffect.AbilityKeywords;
        Description = rawEffect.Desc;
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

    private string? ParseStackingType(string? rawContent)
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
        Result.Desc = Description;
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
}
