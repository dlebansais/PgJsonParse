namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class SpecialValue : IHasKey<int>, IHasParentKey<int>
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
    public string[]? AttributesThatDeltaBase { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? AttributesThatMod { get; set; }
    
    public string? DisplayType { get; set; }
    
    public string? Label { get; set; }
    
    public string? SkipIfThisAttributeIsZero { get; set; }
    
    public bool? SkipIfZero { get; set; }
    
    public string? Suffix { get; set; }
    
    public decimal? Value { get; set; }
}
