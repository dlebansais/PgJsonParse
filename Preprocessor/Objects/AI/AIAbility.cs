namespace Preprocessor;

internal class AIAbility
{
    public AIAbility(RawAIAbility fromRawAIAbility)
    {
        Cue = fromRawAIAbility.cue;
        CueVal = fromRawAIAbility.cueVal;
        MaxLevel = fromRawAIAbility.maxLevel;
        MaxRange = fromRawAIAbility.maxRange;
        MinLevel = fromRawAIAbility.minLevel;

        if (fromRawAIAbility.minDistance is null)
        {
            MinRange = fromRawAIAbility.minRange;
            IsMinDistance = false;
        }
        else
        {
            MinRange = fromRawAIAbility.minDistance;
            IsMinDistance = true;
        }
    }

    public string? Cue { get; set; }
    public int? CueVal { get; set; }
    public int? MaxLevel { get; set; }
    public decimal? MaxRange { get; set; }
    public int? MinLevel { get; set; }
    public decimal? MinRange { get; set; }

    public RawAIAbility ToRawAIAbility()
    {
        RawAIAbility Result = new();

        Result.cue = Cue;
        Result.cueVal = CueVal;
        Result.maxLevel = MaxLevel;
        Result.maxRange = MaxRange;

        if (IsMinDistance)
            Result.minDistance = MinRange;
        else
            Result.minRange = MinRange;

        Result.minLevel = MinLevel;

        return Result;
    }

    public bool IsMinDistance { get; }
}
