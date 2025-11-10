namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class Time
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    public int? Days { get; set; }

    public int? Hours { get; set; }

    public int? Minutes { get; set; }

    public int? Seconds { get; set; }
}
