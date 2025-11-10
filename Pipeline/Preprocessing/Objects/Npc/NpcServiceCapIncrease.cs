namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class NpcServiceCapIncrease
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    public string? Favor { get; set; }

    public string? Purchase { get; set; }

    public int Value { get; set; }
}
