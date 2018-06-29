﻿namespace PgJsonObjects
{
    public class PgQuestObjectiveLoot : GenericPgObject<PgQuestObjectiveLoot>, IPgQuestObjectiveLoot
    {
        public PgQuestObjectiveLoot(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveLoot CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveLoot CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveLoot(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public IPgItem QuestItem { get { return GetObject(4, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
        public ItemCollection ItemList { get { return GetObjectList(8, ref _ItemList, ItemCollection.CreateItem, () => new ItemCollection()); } } private ItemCollection _ItemList;
        public ItemKeyword ItemTarget { get { return GetEnum<ItemKeyword>(12); } }
        public MonsterTypeTag MonsterTypeTag { get { return GetEnum<MonsterTypeTag>(14); } }
    }
}
