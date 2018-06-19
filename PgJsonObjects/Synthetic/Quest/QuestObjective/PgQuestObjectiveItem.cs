namespace PgJsonObjects
{
    public class PgQuestObjectiveItem : GenericPgObject<PgQuestObjectiveItem>, IPgQuestObjectiveItem
    {
        public PgQuestObjectiveItem(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveItem CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveItem CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveItem(data, ref offset);
        }

        public IPgItem QuestItem { get { return GetObject(0, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
        public ItemCollection TargetItemList { get { return GetObjectList(4, ref _TargetItemList, ItemCollection.CreateItem, () => new ItemCollection()); } } private ItemCollection _TargetItemList;
        public ItemKeyword Target { get { return GetEnum<ItemKeyword>(8); } }
    }
}
