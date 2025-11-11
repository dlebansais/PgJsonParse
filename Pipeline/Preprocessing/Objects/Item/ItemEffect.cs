namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class ItemEffect
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public string? Key { get; set; }

    public decimal? AttributeEffect { get; set; }

    public string? AttributeName { get; set; }

    public string? Description { get; set; }
}
