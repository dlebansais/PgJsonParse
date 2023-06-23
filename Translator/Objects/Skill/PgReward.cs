namespace PgObjects
{
    using System.Collections.Generic;

    public class PgReward
    {
        public int RewardLevel { get { return RawRewardLevel.HasValue ? RawRewardLevel.Value : 0; } }
        public int? RawRewardLevel { get; set; }
        public List<Race> RaceRestrictionList { get; set; } = new List<Race>();
        public string[]? Ability_Keys { get; set; }
        public string? BonusLevelSkill_Key { get; set; }
        public string? Recipe_Key { get; set; }
        public string Notes { get; set; } = string.Empty;

        public void SetLevel(int level)
        {
            Level = level;
        }

        public void SetRaces(string[] races)
        {
            Races = races;
        }

        private int Level;
        private string[] Races = new string[0];
    }
}
