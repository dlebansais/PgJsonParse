﻿namespace PgObjects
{
    public class PgQuestRewardSkillLevel : PgQuestReward
    {
        public string? Skill_Key { get; set; }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
    }
}
