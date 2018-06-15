using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveLoot : GenericPgObject, IPgQuestObjectiveLoot
    {
        public PgQuestObjectiveLoot(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Item QuestItem { get { return GetObject(0, ref _QuestItem); } } private Item _QuestItem;
        public List<Item> ItemList { get { return GetObjectList(4, ref _ItemList); } } private List<Item> _ItemList;
        public ItemKeyword ItemTarget { get { return GetEnum<ItemKeyword>(8); } }
        public MonsterTypeTag MonsterTypeTag { get { return GetEnum<MonsterTypeTag>(10); } }
    }
}
