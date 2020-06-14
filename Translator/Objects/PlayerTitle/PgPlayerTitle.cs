namespace PgObjects
{
    using System.Collections.Generic;

    public class PgPlayerTitle
    {
        public string Key { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Tooltip { get; set; } = string.Empty;
        public List<TitleKeyword> KeywordList { get; set; } = new List<TitleKeyword>();
    }
}
