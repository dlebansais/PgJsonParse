namespace PgObjects
{
    public class PgQuestRequirementOr : PgQuestRequirement
    {
        public PgQuestRequirementCollection OrList { get; } = new PgQuestRequirementCollection();
    }
}
