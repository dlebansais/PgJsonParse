namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class QuestRewardItem : IHasKey<int>, IHasParentKey<int>
{
    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public int ParentKey { get; set; }

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public string? Item { get; set; }

    public int? StackSize { get; set; }
}
