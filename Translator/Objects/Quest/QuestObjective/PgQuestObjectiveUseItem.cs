namespace PgObjects
{
    public class PgQuestObjectiveUseItem : PgQuestObjective
    {
        public PgItem Item { get; set; } = null!;
        public PgQuestObjectiveRequirement QuestObjectiveRequirement { get; set; } = null!;
    }
}
