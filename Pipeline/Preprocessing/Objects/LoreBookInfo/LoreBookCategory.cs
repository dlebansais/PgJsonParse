namespace Preprocessor;

public class LoreBookCategory
{
    public LoreBookCategory(string key)
    {
        Key = key;
    }

    public LoreBookCategory(string key, RawLoreBookCategory rawLoreBookCategory)
        : this(key)
    {
        SortTitle = rawLoreBookCategory.SortTitle;
        SubTitle = rawLoreBookCategory.SubTitle;
        Title = rawLoreBookCategory.Title;
    }

    public string Key { get; set; }
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
