namespace Preprocessor;

internal class AIAbility
{
    public AIAbility(RawAIAbility rawAIAbility)
    {
        Cue = rawAIAbility.cue;
        CueVal = rawAIAbility.cueVal;
        MaxLevel = rawAIAbility.maxLevel;
        MaxRange = rawAIAbility.maxRange;
        MinLevel = rawAIAbility.minLevel;

        MinDistance = rawAIAbility.minDistance;
        MinRange = rawAIAbility.minRange;
        /*
        if (rawAIAbility.minDistance is null)
        {
            MinRange = rawAIAbility.minRange;
            IsMinDistance = false;
        }
        else
        {
            MinRange = rawAIAbility.minDistance;
            IsMinDistance = true;
        }*/
    }

    public string? Cue { get; set; }
    public int? CueVal { get; set; }
    public int? MaxLevel { get; set; }
    public decimal? MaxRange { get; set; }
    public decimal? MinDistance { get; set; }
    public int? MinLevel { get; set; }
    public decimal? MinRange { get; set; }

    public RawAIAbility ToRawAIAbility()
    {
        RawAIAbility Result = new();

        Result.cue = Cue;
        Result.cueVal = CueVal;
        Result.maxLevel = MaxLevel;
        Result.maxRange = MaxRange;

        /*
        if (IsMinDistance)
            Result.minDistance = MinRange;
        else
            Result.minRange = MinRange;
        */
        Result.minDistance = MinDistance;
        Result.minRange = MinRange;

        Result.minLevel = MinLevel;

        return Result;
    }

    //private readonly bool IsMinDistance;
}
