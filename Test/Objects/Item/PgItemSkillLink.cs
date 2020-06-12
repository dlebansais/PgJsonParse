namespace PgObjects
{
    using System.Collections.Generic;

    public class PgItemSkillLink
    {
        public Dictionary<PgSkill, int> SkillTable { get; } = new Dictionary<PgSkill, int>();
    }
}
