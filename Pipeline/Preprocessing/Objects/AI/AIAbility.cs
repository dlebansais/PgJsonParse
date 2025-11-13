namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class AIAbility : IHasKey<int>, IHasParentKey<string>
{
    public AIAbility(string key, RawAIAbility rawAIAbility)
    {
        Cue = rawAIAbility.cue;
        CueVal = rawAIAbility.cueVal;
        Favorite = rawAIAbility.favorite;
        JsonKey = key;
        MaxLevel = rawAIAbility.maxLevel;
        MaxRange = rawAIAbility.maxRange;
        MinLevel = rawAIAbility.minLevel;
        MinRange = rawAIAbility.minRange;
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public string ParentKey { get; set; } = string.Empty;

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public string? Cue { get; set; }

    public int? CueVal { get; set; }

    public bool? Favorite { get; set; }

    [JsonIgnore]
    public string JsonKey { get; set; }

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
