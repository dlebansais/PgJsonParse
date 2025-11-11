namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class ConditionalKeyword
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    public string? EffectKeywordMustExist { get; set; }

    public string? EffectKeywordMustNotExist { get; set; }

    public bool? IsDefault { get; set; }

    public string? Keyword { get; set; }
}
