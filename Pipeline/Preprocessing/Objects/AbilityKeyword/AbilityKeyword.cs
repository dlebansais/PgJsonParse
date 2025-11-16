namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class AbilityKeyword
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AttributesThatDeltaAccuracy { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AttributesThatDeltaCritChance { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AttributesThatDeltaDamage { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AttributesThatDeltaPowerCost { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AttributesThatDeltaRange { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AttributesThatDeltaResetTime { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AttributesThatModCritDamage { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AttributesThatModDamage { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? MustHaveAbilityKeywords { get; set; }
    
    public string? MustHaveActiveSkill { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? MustHaveEffectKeywords { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? MustNotHaveAbilityKeywords { get; set; }
}
