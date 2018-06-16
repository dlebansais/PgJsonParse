using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgLoreBook : GenericPgObject, IPgLoreBook
    {
        public PgLoreBook(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string Title { get { return GetString(0); } }
        public string LocationHint { get { return GetString(4); } }
        public List<LoreBookKeyword> KeywordList { get { return GetEnumList(8, ref _KeywordList); } } private List<LoreBookKeyword> _KeywordList;
        public LoreBookCategory Category { get { return GetEnum<LoreBookCategory>(12); } }
        public LoreBookVisibility Visibility { get { return GetEnum<LoreBookVisibility>(14); } }
        public string InternalName { get { return GetString(16); } }
        public string Text { get { return GetString(20); } }
        public bool IsClientLocal { get { return RawIsClientLocal.HasValue ? RawIsClientLocal.Value : false; } }
        public bool? RawIsClientLocal { get { return GetBool(24, 0); } }
    }
}
