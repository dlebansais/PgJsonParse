namespace PgJsonObjects
{
    public class ItemSkillLink
    {
        public ItemSkillLink(string SkillName, int? SkillLevel)
        {
            this.SkillName = SkillName;
            RawSkillLevel = SkillLevel;
            Link = null;
            IsParsed = false;
        }

        public string SkillName { get; private set; }
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get; private set; }
        public Skill Link { get; private set; }

        public bool HasLevel { get { return RawSkillLevel.HasValue && RawSkillLevel.Value > 0; } }
        public string ParsedLevel { get { return RawSkillLevel.HasValue && RawSkillLevel.Value > 0 ? RawSkillLevel.Value.ToString() : ""; } }
        public bool IsParsed { get; private set; }

        public void SetLink(Skill Link)
        {
            this.Link = Link;
            IsParsed = true;
        }
    }
}
