namespace PgObjects
{
    public class PgQuestRewardDeltaScriptAtomicInt : PgQuestReward
    {
        public int Amount { get { return RawAmount.HasValue ? RawAmount.Value : 0; } }
        public int? RawAmount { get; set; }
        public InteractionFlag InteractionFlag { get; set; }
    }
}
