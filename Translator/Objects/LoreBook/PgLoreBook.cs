namespace PgObjects
{
    using System.Collections.Generic;

    public class PgLoreBook : PgObject
    {
        public string Key { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string LocationHint { get; set; } = string.Empty;
        public PgLoreBookInfoCategory Category { get; set; }
        public List<LoreBookKeyword> KeywordList { get; set; } = new List<LoreBookKeyword>();
        public bool IsClientLocal { get { return RawIsClientLocal.HasValue ? RawIsClientLocal.Value : false; } }
        public bool? RawIsClientLocal { get; set; }
        public LoreBookVisibility Visibility { get; set; }
        public string InternalName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;

        public override int ObjectIconId { get { return LoreBookIconId; } }
        public override string ObjectName { get { return Title; } }
        public override string ToString() { return Title; }
    }
}
