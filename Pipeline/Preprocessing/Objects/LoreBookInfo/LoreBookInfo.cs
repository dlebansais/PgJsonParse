namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class LoreBookInfo
{
    [JsonIgnore]
    [Column(IsPrimary = true, IsIdentity = true)]
    public string? Key { get; set; }

    public LoreBookCategoryDictionary? Categories { get; set; }
}
