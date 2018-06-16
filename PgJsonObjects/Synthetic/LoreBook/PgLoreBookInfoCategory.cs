namespace PgJsonObjects
{
    public class PgLoreBookInfoCategory : GenericPgObject, IPgLoreBookInfoCategory
    {
        public PgLoreBookInfoCategory(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string Title { get { return GetString(0); } }
        public string SubTitle { get { return GetString(4); } }
        public string SortTitle { get { return GetString(8); } }
    }
}
