namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class SkillAdvancementHint : IHasKey<int>, IHasParentKey<string>
{
    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public string ParentKey { get; set; } = string.Empty;

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public string? Hint { get; set; }

    public int? Level { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? Npcs { get; set; }
}
