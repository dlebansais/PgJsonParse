namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class SourceRecipeEntry
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public string? Key { get; set; }

    public int? HangOutId { get; set; }

    public int? ItemTypeId { get; set; }

    public string? Npc { get; set; }

    public int? QuestId { get; set; }

    public string? Skill { get; set; }

    public string? Type { get; set; }
}
