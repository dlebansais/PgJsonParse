using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveGuildGiveItem : GenericPgObject<PgQuestObjectiveGuildGiveItem>, IPgQuestObjectiveGuildGiveItem
    {
        public PgQuestObjectiveGuildGiveItem(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveGuildGiveItem CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveGuildGiveItem CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveGuildGiveItem(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public IPgItem QuestItem { get { return GetObject(4, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
        public IPgGameNpc DeliverNpc { get { return GetObject(8, ref _DeliverNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _DeliverNpc;
        public ItemCollection ItemList { get { return GetObjectList(12, ref _ItemList, ItemCollection.CreateItem, () => new ItemCollection()); } } private ItemCollection _ItemList;
        public ItemKeyword ItemKeyword { get { return GetEnum<ItemKeyword>(16); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
