namespace PgObjects
{
    public class PgQuestRewardLevel : PgQuestReward
    {
        public PgSkill Skill { get; set; }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
    }
}
