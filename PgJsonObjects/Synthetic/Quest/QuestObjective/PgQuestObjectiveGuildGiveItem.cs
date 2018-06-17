﻿namespace PgJsonObjects
{
    public class PgQuestObjectiveGuildGiveItem : GenericPgObject<PgQuestObjectiveGuildGiveItem>, IPgQuestObjectiveGuildGiveItem
    {
        public PgQuestObjectiveGuildGiveItem(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveGuildGiveItem CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveGuildGiveItem(data, offset);
        }

        public Item QuestItem { get { return GetObject(0, ref _QuestItem); } } private Item _QuestItem;
        public GameNpc DeliverNpc { get { return GetObject(4, ref _DeliverNpc); } } private GameNpc _DeliverNpc;
        public ItemCollection ItemList { get { return GetObjectList(8, ref _ItemList, ItemCollection.CreateItem, () => new ItemCollection()); } } private ItemCollection _ItemList;
        public ItemKeyword ItemKeyword { get { return GetEnum<ItemKeyword>(12); } }
    }
}
