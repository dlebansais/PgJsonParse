namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class QuestPreGive
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    public string? Ability { get; set; }

    public string? Description { get; set; }

    public string? InteractionFlag { get; set; }

    public string? Item { get; set; }

    public string? QuestGroup { get; set; }

    public required string T { get; init; }
}
