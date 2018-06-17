namespace PgJsonObjects
{
    public class PgQuestCompletedQuestRequirement : GenericPgObject, IPgQuestCompletedQuestRequirement
    {
        public PgQuestCompletedQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public QuestCollection QuestList { get { return GetObjectList(4, ref _QuestList, QuestCollection.CreateItem, () => new QuestCollection()); } } private QuestCollection _QuestList;
    }
}
