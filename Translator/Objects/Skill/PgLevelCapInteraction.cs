namespace PgJsonObjects
{
    public class PgLevelCapInteraction
    {
        public int Level { get { return RawLevel.Value; } }
        public int? RawLevel { get; set; }
        public int OtherLevel { get { return RawOtherLevel.Value; } }
        public int? RawOtherLevel { get; set; }
        public PgSkill Skill { get; set; }
    }
}
