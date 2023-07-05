namespace Preprocessor;

public class Behavior
{
    public Behavior(RawBehavior rawBehavior)
    {
        MetabolismCost = rawBehavior.MetabolismCost;
        MinStackSizeNeeded = rawBehavior.MinStackSizeNeeded;
        UseAnimation = rawBehavior.UseAnimation;
        UseDelay = rawBehavior.UseDelay;
        UseDelayAnimation = rawBehavior.UseDelayAnimation;
        UseRequirements = rawBehavior.UseRequirements;
        UseVerb = ParseUseVerb(rawBehavior.UseVerb);
    }

    private static string? ParseUseVerb(string? rawUseVerb)
    {
        return rawUseVerb;
    }

    public int? MetabolismCost { get; set; }
    public int? MinStackSizeNeeded { get; set; }
    public string? UseAnimation { get; set; }
    public decimal? UseDelay { get; set; }
    public string? UseDelayAnimation { get; set; }
    public string[]? UseRequirements { get; set; }
    public string? UseVerb { get; set; }

    public RawBehavior ToRawBehavior()
    {
        RawBehavior Result = new();

        Result.MetabolismCost = MetabolismCost;
        Result.MinStackSizeNeeded = MinStackSizeNeeded;
        Result.UseAnimation = UseAnimation;
        Result.UseDelay = UseDelay;
        Result.UseDelayAnimation = UseDelayAnimation;
        Result.UseRequirements = UseRequirements;
        Result.UseVerb = ToRawUseVerb(UseVerb);

        return Result;
    }

    private static string? ToRawUseVerb(string? useVerb)
    {
        return useVerb;
    }
}
