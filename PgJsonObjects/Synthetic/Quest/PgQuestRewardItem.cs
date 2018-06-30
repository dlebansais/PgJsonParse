using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestRewardItem : GenericPgObject<PgQuestRewardItem>, IPgQuestRewardItem
    {
        public PgQuestRewardItem(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestRewardItem CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestRewardItem CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestRewardItem(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public IPgItem QuestItem { get { return GetObject(4, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
        public int StackSize { get { return RawStackSize.HasValue ? RawStackSize.Value : 1; } }
        public int? RawStackSize { get { return GetInt(8); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
