namespace PgObjects
{
    public class PgXpTableLevel
    {
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
        public int Xp { get { return RawXp.HasValue ? RawXp.Value : 0; } }
        public int? RawXp { get; set; }
        public int TotalXp { get { return RawTotalXp.HasValue ? RawTotalXp.Value : 0; } }
        public int? RawTotalXp { get; set; }
    }
}
