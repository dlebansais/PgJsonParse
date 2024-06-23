namespace PgObjects
{
    using System.Collections.Generic;

    public class PgPlayerTitle : PgObject
    {
        public string Title { get; set; } = string.Empty;
        public string Tooltip { get; set; } = string.Empty;
        public List<TitleKeyword> KeywordList { get; set; } = new List<TitleKeyword>();
        public uint Color { get { return RawColor.HasValue ? RawColor.Value : 0; } }
        public uint? RawColor { get; set; }
        public bool IsAccountWide { get { return RawIsAccountWide.HasValue && RawIsAccountWide.Value; } }
        public bool? RawIsAccountWide { get; set; }

        public override int ObjectIconId { get { return PlayerTitleIconId; } }
        public override string ObjectName { get { return Title; } }
        public override string ToString() { return Title; }
    }
}
