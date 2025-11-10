namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class QuestFailEffect
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    public string? InteractionFlag { get; set; }

    public required string Type { get; init; }
}
