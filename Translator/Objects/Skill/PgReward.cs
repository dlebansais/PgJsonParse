namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgReward
    {
        public int RewardLevel { get { return RawRewardLevel.HasValue ? RawRewardLevel.Value : 0; } }
        public int? RawRewardLevel { get; set; }
        public List<Race> RaceRestrictionList { get; } = new List<Race>();
        public PgAbility Ability { get; set; }
        public string Notes { get; set; } = string.Empty;
        public PgRecipe Recipe { get; set; }
        public PgSkill BonusSkill { get; set; }
    }
}
