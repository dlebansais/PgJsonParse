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

        public int Level { get { return RawLevel.Value; } }
        public int? RawLevel { get { return GetInt(0); } }
        public int Xp { get { return RawXp.Value; } }
        public int? RawXp { get { return GetInt(4); } }
        public int TotalXp { get { return RawTotalXp.Value; } }
        public int? RawTotalXp { get { return GetInt(8); } }
    }
}
