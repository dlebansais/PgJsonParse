namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class ConditionalKeyword : IHasKey<int>, IHasParentKey<int>
{
    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public int ParentKey { get; set; }

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public string? EffectKeywordMustExist { get; set; }

    public string? EffectKeywordMustNotExist { get; set; }

    public bool? IsDefault { get; set; }

    public string? Keyword { get; set; }
}
