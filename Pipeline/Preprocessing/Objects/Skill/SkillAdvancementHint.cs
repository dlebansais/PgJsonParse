namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class SkillAdvancementHint
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public int Key { get; set; }

    public string? Hint { get; set; }

    public int? Level { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? Npcs { get; set; }
}
