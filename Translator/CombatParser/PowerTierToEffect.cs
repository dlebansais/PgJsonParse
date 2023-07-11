namespace Translator;

public class PowerTierToEffect
{
    public int? Tier { get; set; }
    public int? Effect { get; set; }
    public string[]? AbilityKeywords { get; set; }
    public PowerTierCombatEffect[]? StaticCombatEffects { get; set; }
    public PowerTierCombatEffect[]? DynamicCombatEffects { get; set; }
}
