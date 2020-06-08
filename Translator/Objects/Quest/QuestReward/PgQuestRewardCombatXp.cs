namespace PgJsonObjects
{
    public class PgQuestRewardCombatXp : PgQuestReward
    {
        public int Xp { get { return RawXp.HasValue ? RawXp.Value : 0; } }
        public int? RawXp { get; set; }
    }
}
