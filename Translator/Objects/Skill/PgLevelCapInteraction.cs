namespace PgJsonObjects
{
    public class PgLevelCapInteraction
    {
        public int OtherLevel { get { return RawOtherLevel.Value; } }
        public int? RawOtherLevel { get; private set; }
        public int Level { get { return RawLevel.Value; } }
        public int? RawLevel { get; private set; }
        public PgSkill Skill { get; private set; }
    }
}
