using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveGuildGiveItem : GenericPgObject, IPgQuestObjectiveGuildGiveItem
    {
        public PgQuestObjectiveGuildGiveItem(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Item QuestItem { get { return GetObject(0, ref _QuestItem); } } private Item _QuestItem;
        public GameNpc DeliverNpc { get { return GetObject(4, ref _DeliverNpc); } } private GameNpc _DeliverNpc;
        public List<Item> ItemList { get { return GetObject(8, ref _ItemList); } } private List<Item> _ItemList;
        public ItemKeyword ItemKeyword { get { return GetEnum<ItemKeyword>(12); } }
    }
}
