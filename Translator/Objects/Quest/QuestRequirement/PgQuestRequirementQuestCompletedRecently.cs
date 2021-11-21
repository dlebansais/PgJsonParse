namespace PgObjects
{
    public class PgQuestRequirementQuestCompletedRecently : PgQuestRequirement
    {
        public PgQuestCollection QuestList { get; set; } = new PgQuestCollection();
    }
}
