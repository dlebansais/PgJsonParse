using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPlayerTitle : GenericPgObject, IPgPlayerTitle
    {
        public PgPlayerTitle(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string Title { get { return GetString(0); } }
        public string RawTitle { get { return GetString(4); } }
        public string Tooltip { get { return GetString(8); } }
        public List<TitleKeyword> KeywordList { get { return GetEnumList(12, ref _KeywordList); } } private List<TitleKeyword> _KeywordList;
    }
}
