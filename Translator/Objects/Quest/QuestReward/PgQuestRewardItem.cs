namespace PgObjects
{
    public class PgQuestRewardItem : PgQuestReward
    {
        public PgItem Item { get; set; } = null!;
        public int StackSize { get { return RawStackSize.HasValue ? RawStackSize.Value : 1; } }
        public int? RawStackSize { get; set; }
    }
}
