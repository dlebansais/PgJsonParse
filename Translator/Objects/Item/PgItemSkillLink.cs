namespace PgObjects
{
    using System.Collections.Generic;

    public class PgItemSkillLink
    {
        public Dictionary<PgSkill, int> SkillTable { get; set; } = new Dictionary<PgSkill, int>();
    }
}
