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
        MinRange = rawAIAbility.minRange;
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
        Result.minRange = MinRange;
        Result.minLevel = MinLevel;

        return Result;
    }
}
