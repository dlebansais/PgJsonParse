namespace Translator;

public class PowerTierToEffect
{
    public string[]? AbilityKeywords { get; set; }
    public string? Description { get; set; }
    public PowerTierCombatEffect[]? DynamicCombatEffects { get; set; }
    public int? Effect { get; set; }
    public PowerTierCombatEffect[]? StaticCombatEffects { get; set; }
    public int? Tier { get; set; }
    public PowerTierToEffect? Xtra { get; set; }
}
