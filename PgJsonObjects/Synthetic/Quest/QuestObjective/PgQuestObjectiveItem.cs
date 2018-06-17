namespace PgJsonObjects
{
    public class PgQuestObjectiveItem : GenericPgObject, IPgQuestObjectiveItem
    {
        public PgQuestObjectiveItem(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Item QuestItem { get { return GetObject(0, ref _QuestItem); } } private Item _QuestItem;
        public ItemCollection TargetItemList { get { return GetObjectList(4, ref _TargetItemList, ItemCollection.CreateItem, () => new ItemCollection()); } } private ItemCollection _TargetItemList;
        public ItemKeyword Target { get { return GetEnum<ItemKeyword>(8); } }
    }
}
