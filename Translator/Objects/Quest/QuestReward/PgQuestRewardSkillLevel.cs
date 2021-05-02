namespace PgObjects
{
    public class PgQuestRewardSkillLevel : PgQuestReward
    {
        public PgSkill Skill { get; set; } = null!;
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
    }
}
