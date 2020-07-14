namespace PgObjects
{
    using System.Collections.Generic;

    public class PgPlayerTitle : PgObject
    {
        public string Key { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Tooltip { get; set; } = string.Empty;
        public List<TitleKeyword> KeywordList { get; set; } = new List<TitleKeyword>();

        public override int ObjectIconId { get { return PlayerTitleIconId; } }
        public override string ObjectName { get { return Title; } }
        public override string ToString() { return Title; }
    }
}
