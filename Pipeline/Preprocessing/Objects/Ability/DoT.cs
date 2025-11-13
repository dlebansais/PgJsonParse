namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class DoT : IHasKey<int>, IHasParentKey<int>
{
    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public int ParentKey { get; set; }

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AttributesThatDelta { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AttributesThatMod { get; set; }

    public int? DamagePerTick { get; set; }

    public string? DamageType { get; set; }

    public int? Duration { get; set; }

    public int? NumTicks { get; set; }

    public string? Preface { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? SpecialRules { get; set; }
}
