namespace PgJsonObjects
{
    public class PgXpTableLevel
    {
        public int Level { get { return RawLevel.Value; } }
        public int? RawLevel { get; set; }
        public int Xp { get { return RawXp.Value; } }
        public int? RawXp { get; set; }
        public int TotalXp { get { return RawTotalXp.Value; } }
        public int? RawTotalXp { get; set; }
    }
}
