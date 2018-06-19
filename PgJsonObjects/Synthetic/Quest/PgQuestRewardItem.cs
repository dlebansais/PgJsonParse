namespace PgJsonObjects
{
    public class PgQuestRewardItem : GenericPgObject<PgQuestRewardItem>, IPgQuestRewardItem
    {
        public PgQuestRewardItem(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestRewardItem CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestRewardItem CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestRewardItem(data, ref offset);
        }

        public IPgItem QuestItem { get { return GetObject(0, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
        public int StackSize { get { return RawStackSize.HasValue ? RawStackSize.Value : 1; } }
        public int? RawStackSize { get { return GetInt(4); } }
    }
}
