namespace PgJsonObjects
{
    public class PgOrQuestRequirement : GenericPgObject, IPgOrQuestRequirement
    {
        public PgOrQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public QuestRequirementCollection OrList { get { return GetObjectList(8, ref _OrList, QuestRequirementCollection.CreateItem, () => new QuestRequirementCollection()); } } private QuestRequirementCollection _OrList;
    }
}
