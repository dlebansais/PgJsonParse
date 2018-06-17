namespace PgJsonObjects
{
    public class PgGuildQuestCompletedQuestRequirement : GenericPgObject, IPgGuildQuestCompletedQuestRequirement
    {
        public PgGuildQuestCompletedQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public QuestCollection QuestList { get { return GetObjectList(4, ref _QuestList, QuestCollection.CreateItem, () => new QuestCollection()); } } private QuestCollection _QuestList;
    }
}
