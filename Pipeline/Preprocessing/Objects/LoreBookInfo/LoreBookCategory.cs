namespace Preprocessor;

using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class LoreBookCategory : IHasKey<string>, IHasParentKey<int>
{
    public LoreBookCategory(string key, RawLoreBookCategory rawLoreBookCategory)
    {
        Key = key;
        SortTitle = rawLoreBookCategory.SortTitle;
        SubTitle = rawLoreBookCategory.SubTitle;
        Title = rawLoreBookCategory.Title;
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public string Key { get; set; }

    [JsonIgnore]
    public int ParentKey { get; set; }

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public string? SortTitle { get; set; }

    public string? SubTitle { get; set; }

    public string? Title { get; set; }

    public RawLoreBookCategory ToRawLoreBookCategory()
    {
        RawLoreBookCategory Result = new();

        Result.SortTitle = SortTitle;
        Result.SubTitle = SubTitle;
        Result.Title = Title;

        return Result;
    }
}
