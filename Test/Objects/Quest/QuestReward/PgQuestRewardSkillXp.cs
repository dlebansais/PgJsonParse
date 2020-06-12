namespace PgObjects
{
    using System.Collections.Generic;

    public class PgQuestRewardSkillXp : PgQuestReward
    {
        public Dictionary<PgSkill, int> XpTable { get; } = new Dictionary<PgSkill, int>();
    }
}
