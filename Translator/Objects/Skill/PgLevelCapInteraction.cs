namespace PgObjects
{
    public class PgLevelCapInteraction
    {
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
        public int RangeUnlock { get { return RawRangeUnlock.HasValue ? RawRangeUnlock.Value : 0; } }
        public int? RawRangeUnlock { get; set; }
        public PgSkill Skill { get; set; } = null!;
    }
}
