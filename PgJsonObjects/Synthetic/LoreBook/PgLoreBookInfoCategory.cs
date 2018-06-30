using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgLoreBookInfoCategory : GenericPgObject<PgLoreBookInfoCategory>, IPgLoreBookInfoCategory
    {
        public PgLoreBookInfoCategory(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgLoreBookInfoCategory CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgLoreBookInfoCategory CreateNew(byte[] data, ref int offset)
        {
            return new PgLoreBookInfoCategory(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string Title { get { return GetString(4); } }
        public string SubTitle { get { return GetString(8); } }
        public string SortTitle { get { return GetString(12); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(16, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
