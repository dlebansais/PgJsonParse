namespace PgJsonObjects
{
    public class PgQuestObjectiveDeliver : GenericPgObject<PgQuestObjectiveDeliver>, IPgQuestObjectiveDeliver
    {
        public PgQuestObjectiveDeliver(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveDeliver CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveDeliver CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveDeliver(data, ref offset);
        }

        public IPgGameNpc DeliverNpc { get { return GetObject(0, ref _DeliverNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _DeliverNpc;
        public IPgItem QuestItem { get { return GetObject(4, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
        public int NumToDeliver { get { return RawNumToDeliver.HasValue ? RawNumToDeliver.Value : 0; } }
        public int? RawNumToDeliver { get { return GetInt(8); } }
    }
}
