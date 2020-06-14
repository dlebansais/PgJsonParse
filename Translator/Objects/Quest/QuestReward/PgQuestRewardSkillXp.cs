namespace PgObjects
{
    using System.Collections.Generic;

    public class PgQuestRewardSkillXp : PgQuestReward
    {
        public Dictionary<PgSkill, int> XpTable { get; set; } = new Dictionary<PgSkill, int>();
    }
}
