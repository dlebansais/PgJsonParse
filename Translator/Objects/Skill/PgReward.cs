﻿namespace PgObjects
{
    using System.Collections.Generic;

    public class PgReward
    {
        public int RewardLevel { get { return RawRewardLevel.HasValue ? RawRewardLevel.Value : 0; } }
        public int? RawRewardLevel { get; set; }
        public List<Race> RaceRestrictionList { get; set; } = new List<Race>();
        public PgAbility Ability { get; set; } = null!;
        public PgSkill BonusLevelSkill { get; set; } = null!;
        public PgRecipe Recipe { get; set; } = null!;
        public string Notes { get; set; } = string.Empty;
    }
}
