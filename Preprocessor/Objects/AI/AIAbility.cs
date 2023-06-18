namespace Preprocessor;

internal class AIAbility
{
    public AIAbility(AIAbility1 fromRawAIAbility1)
    {
        Cue = fromRawAIAbility1.cue;
        CueVal = fromRawAIAbility1.cueVal;
        MaxLevel = fromRawAIAbility1.maxLevel;
        MaxRange = fromRawAIAbility1.maxRange;
        MinLevel = fromRawAIAbility1.minLevel;

        if (fromRawAIAbility1.minDistance is null)
        {
            MinRange = fromRawAIAbility1.minRange;
            IsMinDistance = false;
        }
        else
        {
            MinRange = fromRawAIAbility1.minDistance;
            IsMinDistance = true;
        }
    }

    public string? Cue { get; set; }
    public int? CueVal { get; set; }
    public int? MaxLevel { get; set; }
    public decimal? MaxRange { get; set; }
    public int? MinLevel { get; set; }
    public decimal? MinRange { get; set; }

    public AIAbility1 ToRawAIAbility1()
    {
        AIAbility1 Result = new();

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
