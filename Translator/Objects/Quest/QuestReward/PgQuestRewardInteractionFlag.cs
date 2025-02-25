namespace PgObjects
{
    public class PgQuestRewardInteractionFlag : PgQuestReward
    {
        public bool IsSet { get; set; }
        public InteractionFlag InteractionFlag { get; set; }
    }
}
