namespace PgJsonObjects
{
    public class PgQuestRequirementGuildQuestCompleted : PgQuestRequirement
    {
        public PgQuestCollection QuestList { get; } = new PgQuestCollection();
    }
}
