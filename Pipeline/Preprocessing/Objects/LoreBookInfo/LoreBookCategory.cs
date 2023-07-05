namespace Preprocessor;

public class LoreBookCategory
{
    public LoreBookCategory(RawLoreBookCategory rawLoreBookCategory)
    {
        SortTitle = rawLoreBookCategory.SortTitle;
        SubTitle = rawLoreBookCategory.SubTitle;
        Title = rawLoreBookCategory.Title;
    }

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
