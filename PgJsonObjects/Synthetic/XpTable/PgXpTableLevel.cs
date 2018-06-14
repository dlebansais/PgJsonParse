namespace PgJsonObjects
{
    public class PgXpTableLevel : GenericPgObject, IPgXpTableLevel
    {
        public PgXpTableLevel(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public int Level { get { return RawLevel.Value; } }
        public int? RawLevel { get { return GetInt(0); } }
        public int Xp { get { return RawXp.Value; } }
        public int? RawXp { get { return GetInt(4); } }
        public int TotalXp { get { return RawTotalXp.Value; } }
        public int? RawTotalXp { get { return GetInt(8); } }
    }
}
