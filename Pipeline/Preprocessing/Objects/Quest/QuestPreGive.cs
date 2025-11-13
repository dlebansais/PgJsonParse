namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class QuestPreGive : IHasKey<int>, IHasParentKey<int>
{
    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public int ParentKey { get; set; }

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public string? Ability { get; set; }

    public string? Description { get; set; }

    public string? InteractionFlag { get; set; }

    public string? Item { get; set; }

    public string? QuestGroup { get; set; }

    public required string T { get; init; }
}
