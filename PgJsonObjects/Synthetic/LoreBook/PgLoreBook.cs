using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgLoreBook : MainPgObject<PgLoreBook>, IPgLoreBook
    {
        public PgLoreBook(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 34;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgLoreBook CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgLoreBook CreateNew(byte[] data, ref int offset)
        {
            return new PgLoreBook(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string Title { get { return GetString(4); } }
        public string LocationHint { get { return GetString(8); } }
        public List<LoreBookKeyword> KeywordList { get { return GetEnumList(12, ref _KeywordList); } } private List<LoreBookKeyword> _KeywordList;
        public LoreBookCategory Category { get { return GetEnum<LoreBookCategory>(16); } }
        public LoreBookVisibility Visibility { get { return GetEnum<LoreBookVisibility>(18); } }
        public string InternalName { get { return GetString(20); } }
        public string Text { get { return GetString(24); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(28, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public bool IsClientLocal { get { return RawIsClientLocal.HasValue ? RawIsClientLocal.Value : false; } }
        public bool? RawIsClientLocal { get { return GetBool(32, 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
