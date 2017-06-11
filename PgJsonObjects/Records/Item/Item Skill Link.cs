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
        public bool HasLevel { get { return SkillLevel.HasValue && SkillLevel.Value > 0; } }
        public string ParsedLevel { get { return SkillLevel.HasValue && SkillLevel.Value > 0 ? SkillLevel.Value.ToString() : ""; } }
        public bool IsParsed { get; private set; }

        public void SetLink(Skill Link)
        {
            this.Link = Link;
            IsParsed = true;
        }
    }
}
