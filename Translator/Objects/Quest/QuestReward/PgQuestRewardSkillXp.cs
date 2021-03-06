﻿namespace PgObjects
{
    public class PgQuestRewardSkillXp : PgQuestReward
    {
        public PgSkill Skill { get; set; } = null!;
        public int Xp { get { return RawXp.HasValue ? RawXp.Value : 0; } }
        public int? RawXp { get; set; }
    }
}
