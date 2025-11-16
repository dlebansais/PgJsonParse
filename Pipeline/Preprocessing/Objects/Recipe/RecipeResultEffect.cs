namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class RecipeResultEffect : IHasKey<int>, IHasParentKey<int>
{
    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public int ParentKey { get; set; }

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public string? Ability { get; set; }

    public decimal? AddedQuantity { get; set; }

    public int? AdditionalEnchantments { get; set; }

    public Time? AdjustedReuseTime { get; set; }

    public string? Advancement { get; set; }

    public string? AreaName { get; set; }

    public string? Augment { get; set; }

    public string? Boost { get; set; }

    public int? BoostLevel { get; set; }

    public string? BoostedAnimal { get; set; }

    public int? BrewLine { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? BrewParts { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? BrewResults { get; set; }

    public int? BrewStrength { get; set; }

    public string? Color { get; set; }

    public int? ConsumedEnhancementPoints { get; set; }

    public int? ConsumedUses { get; set; }

    public int? Delta { get; set; }

    public int? DurationInSeconds { get; set; }

    public string? Effect { get; set; }

    public string? Enhancement { get; set; }

    public bool? IsCamouflaged { get; set; }

    public string? Item { get; set; }

    public string? Keyword { get; set; }

    public int? MaxHitCount { get; set; }

    public int? MaxLevel { get; set; }

    public int? MeditationId { get; set; }

    public int? MinLevel { get; set; }

    public string? MoonPhase { get; set; }

    public string? Other { get; set; }

    public int? PowerLevel { get; set; }

    public string? PowerWaxType { get; set; }

    public string? Recipe { get; set; }

    public Time? RepairCooldown { get; set; }

    public int? RepairMaxEfficiency { get; set; }

    public int? RepairMinEfficiency { get; set; }

    public string? Skill { get; set; }

    public string? Slot { get; set; }

    public int? Tier { get; set; }

    public required string Type { get; init; }

    public void SetColorFormat(ColorFormat colorFormat)
    {
        ColorFormat = colorFormat;
    }

    public ColorFormat GetColorFormat()
    {
        return ColorFormat;
    }

    private ColorFormat ColorFormat = new ColorFormat(string.Empty, default, default, default, default);
}
