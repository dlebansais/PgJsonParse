using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPlayerTitle : MainPgObject<PgPlayerTitle>, IPgPlayerTitle
    {
        public PgPlayerTitle(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgPlayerTitle CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgPlayerTitle CreateNew(byte[] data, ref int offset)
        {
            return new PgPlayerTitle(data, ref offset);
        }

        public string Title { get { return GetString(0); } }
        public string RawTitle { get { return GetString(4); } }
        public string Tooltip { get { return GetString(8); } }
        public List<TitleKeyword> KeywordList { get { return GetEnumList(12, ref _KeywordList); } } private List<TitleKeyword> _KeywordList;
    }
}
