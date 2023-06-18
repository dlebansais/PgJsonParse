namespace Preprocessor;

internal class Effect
{
    public Effect(Effect1 fromRawEffect1)
    {
        AbilityKeywords = fromRawEffect1.AbilityKeywords;
        Description = fromRawEffect1.Desc;
        DisplayMode = fromRawEffect1.DisplayMode;
        Duration = Preprocessor.ToNumberOrString(fromRawEffect1.Duration, out IsDurationNumber);
        IconId = fromRawEffect1.IconId;
        Keywords = fromRawEffect1.Keywords;
        Name = fromRawEffect1.Name;
        Particle = fromRawEffect1.Particle;
        SpewText = fromRawEffect1.SpewText;
        StackingPriority = fromRawEffect1.StackingPriority;
        StackingType = fromRawEffect1.StackingType;
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

    public Effect1 ToRawEffect1()
    {
        Effect1 Result = new();

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

    private bool IsDurationNumber;
}
