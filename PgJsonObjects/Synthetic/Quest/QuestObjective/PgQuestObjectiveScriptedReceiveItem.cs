using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveScriptedReceiveItem : GenericPgObject<PgQuestObjectiveScriptedReceiveItem>, IPgQuestObjectiveScriptedReceiveItem
    {
        public PgQuestObjectiveScriptedReceiveItem(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveScriptedReceiveItem CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveScriptedReceiveItem CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveScriptedReceiveItem(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public IPgGameNpc DeliverNpc { get { return GetObject(4, ref _DeliverNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _DeliverNpc;
        public IPgItem QuestItem { get { return GetObject(8, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
