namespace PgJsonObjects
{
    public class PgGuildQuestCompletedQuestRequirement : GenericPgObject<PgGuildQuestCompletedQuestRequirement>, IPgGuildQuestCompletedQuestRequirement
    {
        public PgGuildQuestCompletedQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgGuildQuestCompletedQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgGuildQuestCompletedQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgGuildQuestCompletedQuestRequirement(data, ref offset);
        }

        public QuestCollection QuestList { get { return GetObjectList(4, ref _QuestList, QuestCollection.CreateItem, () => new QuestCollection()); } } private QuestCollection _QuestList;
    }
}
