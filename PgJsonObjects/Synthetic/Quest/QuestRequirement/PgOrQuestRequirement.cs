namespace PgJsonObjects
{
    public class PgOrQuestRequirement : GenericPgObject<PgOrQuestRequirement>, IPgOrQuestRequirement
    {
        public PgOrQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgOrQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgOrQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgOrQuestRequirement(data, ref offset);
        }

        public QuestRequirementCollection OrList { get { return GetObjectList(8, ref _OrList, QuestRequirementCollection.CreateItem, () => new QuestRequirementCollection()); } } private QuestRequirementCollection _OrList;
    }
}
