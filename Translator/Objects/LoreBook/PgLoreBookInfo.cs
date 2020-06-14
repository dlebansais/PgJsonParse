namespace PgObjects
{
    public class PgLoreBookInfo
    {
        public string Key { get; set; } = string.Empty;
        public PgLoreBookInfoCategoryCollection Categories { get; set; } = new PgLoreBookInfoCategoryCollection();
    }
}
