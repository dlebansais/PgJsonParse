namespace Preprocessor;

public class AIAbility
{
    public AIAbility(RawAIAbility rawAIAbility)
    {
        Cue = rawAIAbility.cue;
        CueVal = rawAIAbility.cueVal;
        Favorite = rawAIAbility.favorite;
        MaxLevel = rawAIAbility.maxLevel;
        MaxRange = rawAIAbility.maxRange;
        MinLevel = rawAIAbility.minLevel;

        if (rawAIAbility.minDistance is null)
        {
            MinRange = rawAIAbility.minRange;
            IsMinDistance = false;
        }
        else if (rawAIAbility.minRange is null)
        {
            MinRange = rawAIAbility.minDistance;
            IsMinDistance = true;
        }
        else
            throw new PreprocessorException(this);
    }

    public string? Cue { get; set; }
    public int? CueVal { get; set; }
    public bool? Favorite { get; set; }
    public int? MaxLevel { get; set; }
    public decimal? MaxRange { get; set; }
    public int? MinLevel { get; set; }
    public decimal? MinRange { get; set; }

    public RawAIAbility ToRawAIAbility()
    {
        RawAIAbility Result = new();

        Result.cue = Cue;
        Result.cueVal = CueVal;
        Result.favorite = Favorite;
        Result.maxLevel = MaxLevel;
        Result.maxRange = MaxRange;

        if (IsMinDistance)
            Result.minDistance = MinRange;
        else
            Result.minRange = MinRange;

        Result.minLevel = MinLevel;

        return Result;
    }

    private readonly bool IsMinDistance;
}
