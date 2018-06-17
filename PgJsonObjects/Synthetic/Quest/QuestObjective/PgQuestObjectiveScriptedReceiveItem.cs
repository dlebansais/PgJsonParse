namespace PgJsonObjects
{
    public class PgQuestObjectiveScriptedReceiveItem : GenericPgObject<PgQuestObjectiveScriptedReceiveItem>, IPgQuestObjectiveScriptedReceiveItem
    {
        public PgQuestObjectiveScriptedReceiveItem(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveScriptedReceiveItem CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveScriptedReceiveItem(data, offset);
        }

        public GameNpc DeliverNpc { get { return GetObject(0, ref _DeliverNpc); } } private GameNpc _DeliverNpc;
        public Item QuestItem { get { return GetObject(4, ref _QuestItem); } } private Item _QuestItem;
    }
}
