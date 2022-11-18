namespace PgObjects
{
    public class PgQuestObjectiveCollectItem : PgQuestObjective
    {
        public string? Item_Key { get; set; }
        public PgQuestObjectiveRequirementCollection QuestObjectiveRequirementList { get; set; } = new PgQuestObjectiveRequirementCollection();
    }
}
