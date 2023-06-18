namespace Preprocessor;

internal class RawEffect
{
    public RawEffect(RawEffect1 fromRawEffect1)
    {
        AbilityKeywords = fromRawEffect1.AbilityKeywords;
        Description = fromRawEffect1.Desc;
        DisplayMode = fromRawEffect1.DisplayMode;
        Duration = fromRawEffect1.Duration;
        IconId = fromRawEffect1.IconId;
        Keywords = fromRawEffect1.Keywords;
        Name = fromRawEffect1.Name;
        Particle = fromRawEffect1.Particle;
        SpewText = fromRawEffect1.SpewText;
        StackingPriority = fromRawEffect1.StackingPriority;
        StackingType = fromRawEffect1.StackingType;

        IsDurationNumber = true;
    }

    public RawEffect(RawEffect2 fromRawEffect2)
    {
        AbilityKeywords = fromRawEffect2.AbilityKeywords;
        Description = fromRawEffect2.Desc;
        DisplayMode = fromRawEffect2.DisplayMode;
        Duration = fromRawEffect2.Duration is null ? null : int.Parse(fromRawEffect2.Duration);
        IconId = fromRawEffect2.IconId;
        Keywords = fromRawEffect2.Keywords;
        Name = fromRawEffect2.Name;
        Particle = fromRawEffect2.Particle;
        SpewText = fromRawEffect2.SpewText;
        StackingPriority = fromRawEffect2.StackingPriority;
        StackingType = fromRawEffect2.StackingType;

        IsDurationNumber = false;
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

    public RawEffect1 ToRawEffect1()
    {
        RawEffect1 Result = new();

        Result.AbilityKeywords = AbilityKeywords;
        Result.Desc = Description;
        Result.DisplayMode = DisplayMode;
        Result.Duration = Duration;
        Result.IconId = IconId;
        Result.Keywords = Keywords;
        Result.Name = Name;
        Result.Particle = Particle;
        Result.SpewText = SpewText;
        Result.StackingPriority = StackingPriority;
        Result.StackingType = StackingType;

        return Result;
    }

    public RawEffect2 ToRawEffect2()
    {
        RawEffect2 Result = new();

        Result.AbilityKeywords = AbilityKeywords;
        Result.Desc = Description;
        Result.DisplayMode = DisplayMode;
        Result.Duration = Duration?.ToString();
        Result.IconId = IconId;
        Result.Keywords = Keywords;
        Result.Name = Name;
        Result.Particle = Particle;
        Result.SpewText = SpewText;
        Result.StackingPriority = StackingPriority;
        Result.StackingType = StackingType;

        return Result;
    }

    public bool IsDurationNumber { get; }
}
