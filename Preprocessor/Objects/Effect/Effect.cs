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
        Particle = rawEffect.Particle;
        SpewText = rawEffect.SpewText;
        StackingPriority = rawEffect.StackingPriority;
        StackingType = rawEffect.StackingType;
    }

    public string[]? AbilityKeywords { get; set; }
    public string? Description { get; set; }
    public string? DisplayMode { get; set; }
    public int? Duration { get; set; }
    public int? IconId { get; set; }
    public string[]? Keywords { get; set; }
    public string? Name { get; set; }
    public string? Particle { get; set; }
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
        Result.Particle = Particle;
        Result.SpewText = SpewText;
        Result.StackingPriority = StackingPriority;
        Result.StackingType = StackingType;

        return Result;
    }

    private readonly bool IsDurationNumber;
}
