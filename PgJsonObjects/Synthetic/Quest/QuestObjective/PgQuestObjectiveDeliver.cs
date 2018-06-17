namespace PgJsonObjects
{
    public class PgQuestObjectiveDeliver : GenericPgObject, IPgQuestObjectiveDeliver
    {
        public PgQuestObjectiveDeliver(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveDeliver(data, offset);
        }

        public GameNpc DeliverNpc { get { return GetObject(0, ref _DeliverNpc); } } private GameNpc _DeliverNpc;
        public Item QuestItem { get { return GetObject(4, ref _QuestItem); } } private Item _QuestItem;
        public int NumToDeliver { get { return RawNumToDeliver.HasValue ? RawNumToDeliver.Value : 0; } }
        public int? RawNumToDeliver { get { return GetInt(8); } }
    }
}
