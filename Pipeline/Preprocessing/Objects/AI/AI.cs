namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class AI
{
    [JsonIgnore]
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Key { get; set; }

    public AIAbilityDictionary? Abilities { get; set; }

    public string? Comment { get; set; }

    public string? Description { get; set; }

    public int? FlyOffset { get; set; }

    public bool? Flying { get; set; }

    public decimal? MinDelayBetweenAbilities { get; set; }

    public string? MobilityType { get; set; }

    public bool? ServerDriven { get; set; }

    public string? Strategy { get; set; }

    public bool? Swimming { get; set; }

    public bool? UncontrolledPet { get; set; }

    public bool? UseAbilitiesWithoutEnemyTarget { get; set; }
}
