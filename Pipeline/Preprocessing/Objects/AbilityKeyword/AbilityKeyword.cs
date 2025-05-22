namespace Preprocessor;

public class AbilityKeyword
{
    public string[]? AttributesThatDeltaAccuracy { get; set; }
    public string[]? AttributesThatDeltaCritChance { get; set; }
    public string[]? AttributesThatDeltaDamage { get; set; }
    public string[]? AttributesThatDeltaPowerCost { get; set; }
    public string[]? AttributesThatDeltaRange { get; set; }
    public string[]? AttributesThatDeltaResetTime { get; set; }
    public string[]? AttributesThatModCritDamage { get; set; }
    public string[]? AttributesThatModDamage { get; set; }
    public string[]? MustHaveAbilityKeywords { get; set; }
    public string? MustHaveActiveSkill { get; set; }
    public string[]? MustHaveEffectKeywords { get; set; }
    public string[]? MustNotHaveAbilityKeywords { get; set; }
}
