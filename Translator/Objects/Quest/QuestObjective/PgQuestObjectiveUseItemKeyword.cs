namespace PgObjects
{
    public class PgQuestObjectiveUseItemKeyword : PgQuestObjective
    {
        public ItemKeyword Keyword { get; set; }
        public PgQuestObjectiveRequirement QuestObjectiveRequirement { get; set; } = null!;
    }
}
