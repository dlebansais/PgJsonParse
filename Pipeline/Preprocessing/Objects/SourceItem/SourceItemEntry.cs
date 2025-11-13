namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class SourceItemEntry : IHasKey<int>, IHasParentKey<int>
{
    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public int ParentKey { get; set; }

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public string? FriendlyName { get; set; }

    public int? HangOutId { get; set; }

    public int? ItemTypeId { get; set; }

    public string? Npc { get; set; }

    public int? QuestId { get; set; }

    public int? RecipeId { get; set; }

    public string? Type { get; set; }
}
