namespace Preprocessor;

internal class RawAI
{
    public RawAIAbilityDictionary? Abilities { get; set; }
    public string? Comment { get; set; }
    public string? Description { get; set; }
    public bool? Flying { get; set; }
    public bool? Melee { get; set; }
    public decimal? MinDelayBetweenAbilities { get; set; }
    public string? MobilityType { get; set; }
    public bool? ServerDriven { get; set; }
    public bool? Swimming { get; set; }
    public bool? UncontrolledPet { get; set; }
    public bool? UseAbilitiesWithoutEnemyTarget { get; set; }
}
