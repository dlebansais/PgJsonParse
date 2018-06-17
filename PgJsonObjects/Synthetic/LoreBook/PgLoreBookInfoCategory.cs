namespace PgJsonObjects
{
    public class PgLoreBookInfoCategory : GenericPgObject<PgLoreBookInfoCategory>, IPgLoreBookInfoCategory
    {
        public PgLoreBookInfoCategory(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgLoreBookInfoCategory CreateItem(byte[] data, int offset)
        {
            return new PgLoreBookInfoCategory(data, offset);
        }

        public string Title { get { return GetString(0); } }
        public string SubTitle { get { return GetString(4); } }
        public string SortTitle { get { return GetString(8); } }
    }
}
