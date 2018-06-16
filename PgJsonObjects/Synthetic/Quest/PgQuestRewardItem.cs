namespace PgJsonObjects
{
    public class PgQuestRewardItem : GenericPgObject, IPgQuestRewardItem
    {
        public PgQuestRewardItem(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Item QuestItem { get { return GetObject(0, ref _QuestItem); } } private Item _QuestItem;
        public int StackSize { get { return RawStackSize.HasValue ? RawStackSize.Value : 1; } }
        public int? RawStackSize { get { return GetInt(4); } }
    }
}
