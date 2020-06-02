namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgPlayerTitle
    {
        public string Title { get; set; } = string.Empty;
        public string RawTitle { get; set; } = string.Empty;
        public string Tooltip { get; set; } = string.Empty;
        public List<TitleKeyword> KeywordList { get; } = new List<TitleKeyword>();
    }
}
