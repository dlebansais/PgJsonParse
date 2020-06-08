namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgQuestRewardXp : PgQuestReward
    {
        public Dictionary<PgSkill, int> XpTable { get; } = new Dictionary<PgSkill, int>();
    }
}
