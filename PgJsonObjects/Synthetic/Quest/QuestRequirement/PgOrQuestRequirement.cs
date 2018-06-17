namespace PgJsonObjects
{
    public class PgOrQuestRequirement : GenericPgObject<PgOrQuestRequirement>, IPgOrQuestRequirement
    {
        public PgOrQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgOrQuestRequirement CreateItem(byte[] data, int offset)
        {
            return new PgOrQuestRequirement(data, offset);
        }

        public QuestRequirementCollection OrList { get { return GetObjectList(8, ref _OrList, QuestRequirementCollection.CreateItem, () => new QuestRequirementCollection()); } } private QuestRequirementCollection _OrList;
    }
}
