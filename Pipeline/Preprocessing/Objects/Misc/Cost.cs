namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class Cost
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public string? Key { get; set; }

    public string? Currency { get; set; }

    public int? Price { get; set; }
}
