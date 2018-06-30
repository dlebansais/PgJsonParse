using System.Collections.Generic;

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

        public override string Key { get { return GetString(0); } }
        public IPgItem QuestItem { get { return GetObject(4, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
        public ItemCollection TargetItemList { get { return GetObjectList(8, ref _TargetItemList, ItemCollection.CreateItem, () => new ItemCollection()); } } private ItemCollection _TargetItemList;
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public ItemKeyword Target { get { return GetEnum<ItemKeyword>(16); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
