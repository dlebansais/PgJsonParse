namespace PgJsonObjects
{
    public interface ISearchableObject
    {
        string SortingName { get; }

        string GetSearchResultContentTemplateName();
        string GetSearchResultTitleTemplateName();
    }
}
