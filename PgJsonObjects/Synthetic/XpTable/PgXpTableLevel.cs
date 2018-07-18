using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgXpTableLevel : GenericPgObject<PgXpTableLevel>, IPgXpTableLevel
    {
        public PgXpTableLevel(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgXpTableLevel CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgXpTableLevel CreateNew(byte[] data, ref int offset)
        {
            return new PgXpTableLevel(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public int Level { get { return RawLevel.Value; } }
        public int? RawLevel { get { return GetInt(4); } }
        public int Xp { get { return RawXp.Value; } }
        public int? RawXp { get { return GetInt(8); } }
        public int TotalXp { get { return RawTotalXp.Value; } }
        public int? RawTotalXp { get { return GetInt(12); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(16, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }

        public override string SortingName { get { return null; } }
    }
}
