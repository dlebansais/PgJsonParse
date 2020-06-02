namespace PgJsonObjects
{
    public class PgQuestRewardItem
    {
        public PgItem QuestItem { get; set; }
        public int StackSize { get { return RawStackSize.HasValue ? RawStackSize.Value : 1; } }
        public int? RawStackSize { get; set; }
    }
}
