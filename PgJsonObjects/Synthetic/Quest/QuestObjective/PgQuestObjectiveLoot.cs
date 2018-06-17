namespace PgJsonObjects
{
    public class PgQuestObjectiveLoot : GenericPgObject, IPgQuestObjectiveLoot
    {
        public PgQuestObjectiveLoot(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Item QuestItem { get { return GetObject(0, ref _QuestItem); } } private Item _QuestItem;
        public ItemCollection ItemList { get { return GetObjectList(4, ref _ItemList, ItemCollection.CreateItem, () => new ItemCollection()); } } private ItemCollection _ItemList;
        public ItemKeyword ItemTarget { get { return GetEnum<ItemKeyword>(8); } }
        public MonsterTypeTag MonsterTypeTag { get { return GetEnum<MonsterTypeTag>(10); } }
    }
}
