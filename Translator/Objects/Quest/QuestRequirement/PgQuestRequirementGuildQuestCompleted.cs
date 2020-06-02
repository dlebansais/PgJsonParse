namespace PgJsonObjects
{
    public class PgQuestRequirementGuildQuestCompleted : PgQuestRequirement
    {
        public PgQuestCollection QuestList { get; set; } = new PgQuestCollection();
    }
}
