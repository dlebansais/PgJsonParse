namespace PgJsonObjects
{
    public class PgItemSkillLink
    {
        public string SkillName { get; set; }
        public int SkillLevel { get { return RawSkillLevel.HasValue ? RawSkillLevel.Value : 0; } }
        public int? RawSkillLevel { get; set; }
        public PgSkill Link { get; set; }
    }
}
