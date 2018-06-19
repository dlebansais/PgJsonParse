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

        public IPgGameNpc DeliverNpc { get { return GetObject(0, ref _DeliverNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _DeliverNpc;
        public IPgItem QuestItem { get { return GetObject(4, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
    }
}
