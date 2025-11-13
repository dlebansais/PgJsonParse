namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class ItemEffect : IHasKey<int>, IHasParentKey<int>
{
    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public int ParentKey { get; set; }

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public decimal? AttributeEffect { get; set; }

    public string? AttributeName { get; set; }

    public string? Description { get; set; }
}
