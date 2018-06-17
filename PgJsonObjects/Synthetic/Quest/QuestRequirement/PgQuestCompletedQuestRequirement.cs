namespace PgJsonObjects
{
    public class PgQuestCompletedQuestRequirement : GenericPgObject<PgQuestCompletedQuestRequirement>, IPgQuestCompletedQuestRequirement
    {
        public PgQuestCompletedQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestCompletedQuestRequirement CreateItem(byte[] data, int offset)
        {
            return new PgQuestCompletedQuestRequirement(data, offset);
        }

        public QuestCollection QuestList { get { return GetObjectList(4, ref _QuestList, QuestCollection.CreateItem, () => new QuestCollection()); } } private QuestCollection _QuestList;
    }
}
