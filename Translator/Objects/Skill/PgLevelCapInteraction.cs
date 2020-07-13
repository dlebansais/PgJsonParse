namespace PgObjects
{
    public class PgLevelCapInteraction
    {
        public int Level { get { return RawLevel.Value; } }
        public int? RawLevel { get; set; }
        public int RangeUnlock { get { return RawRangeUnlock.Value; } }
        public int? RawRangeUnlock { get; set; }
        public PgSkill Skill { get; set; }
    }
}
