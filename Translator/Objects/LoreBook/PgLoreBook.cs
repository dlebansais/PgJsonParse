namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgLoreBook
    {
        public string Title { get; set; } = string.Empty;
        public string LocationHint { get; set; } = string.Empty;
        public List<LoreBookKeyword> KeywordList { get; } = new List<LoreBookKeyword>();
        public LoreBookCategory Category { get; set; }
        public LoreBookVisibility Visibility { get; set; }
        public string InternalName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public bool IsClientLocal { get { return RawIsClientLocal.HasValue ? RawIsClientLocal.Value : false; } }
        public bool? RawIsClientLocal { get; set; }
        public bool IsKeywordListEmpty { get { return RawIsKeywordListEmpty.HasValue ? RawIsKeywordListEmpty.Value : false; } }
        public bool? RawIsKeywordListEmpty { get; set; }
    }
}
