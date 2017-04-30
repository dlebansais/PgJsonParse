namespace PgJsonObjects
{
    public class ItemSkillLink
    {
        public ItemSkillLink(string SkillName, int? SkillLevel)
        {
            this.SkillName = SkillName;
            this.SkillLevel = SkillLevel;
            Link = null;
            IsParsed = false;
        }

        public string SkillName { get; private set; }
        public int? SkillLevel { get; private set; }
        public Skill Link { get; private set; }
        public bool IsParsed { get; private set; }

        public void SetLink(Skill Link)
        {
            this.Link = Link;
            IsParsed = true;
        }
    }
}
