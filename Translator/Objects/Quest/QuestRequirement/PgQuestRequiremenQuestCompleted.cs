namespace PgJsonObjects
{
    public class PgQuestRequirementQuestCompleted : PgQuestRequirement
    {
        public PgQuestCollection QuestList { get; set; } = new PgQuestCollection();
    }
}
