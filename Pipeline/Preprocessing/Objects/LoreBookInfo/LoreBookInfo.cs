namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class LoreBookInfo : IHasKey<int>
{
    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [Column(IsIgnore = true)]
    public LoreBookCategoryDictionary? Categories { get; set; }

    [JsonIgnore]
    public LoreBookCategory[]? CategoryArray { get; set; }
}
