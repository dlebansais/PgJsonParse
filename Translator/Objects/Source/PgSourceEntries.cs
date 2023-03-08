namespace PgObjects
{
    using System.Collections.Generic;

    public class PgSourceEntries
    {
        public string Key { get; set; } = string.Empty;
        public List<PgSource> EntryList { get; set; } = new List<PgSource>();
    }
}
