namespace PgJsonObjects
{
    public class LevelCapInteraction
    {
        public LevelCapInteraction(PowerSkill OtherSkill, int OtherLevel, int Level)
        {
            this.OtherSkill = OtherSkill;
            this.OtherLevel = OtherLevel;
            this.Level = Level;
            Link = null;
            IsParsed = false;
        }

        public PowerSkill OtherSkill { get; private set; }
        public int OtherLevel { get; private set; }
        public int Level { get; private set; }
        public Skill Link { get; private set; }
        public bool IsParsed { get; private set; }

        public void SetLink(Skill Link)
        {
            this.Link = Link;
            IsParsed = true;
        }
    }
}
