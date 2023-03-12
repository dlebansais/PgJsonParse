namespace PgObjects
{
    public class PgQuestRewardCombatXp : PgQuestReward
    {
        public int Xp { get { return RawXp.HasValue ? RawXp.Value : 0; } }
        public int? RawXp { get; set; }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; set; }
    }
}
