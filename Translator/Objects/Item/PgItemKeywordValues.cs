namespace PgObjects
{
    using System.Collections.Generic;

    public class PgItemKeywordValues
    {
        public ItemKeyword Keyword { get; set; }
        public List<float> Values { get; set; } = new();
    }
}
