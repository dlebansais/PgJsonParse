using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveItem : GenericPgObject, IPgQuestObjectiveItem
    {
        public PgQuestObjectiveItem(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Item QuestItem { get { return GetObject(0, ref _QuestItem); } } private Item _QuestItem;
        public List<Item> TargetItemList { get { return GetObjectList(4, ref _TargetItemList); } } private List<Item> _TargetItemList;
        public ItemKeyword Target { get { return GetEnum<ItemKeyword>(8); } }
    }
}
