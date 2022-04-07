namespace PgObjects
{
    public class PgQuestRewardItem : PgQuestReward
    {
        public string? Item_Key { get; set; }
        public int StackSize { get { return RawStackSize.HasValue ? RawStackSize.Value : 1; } }
        public int? RawStackSize { get; set; }
    }
}
